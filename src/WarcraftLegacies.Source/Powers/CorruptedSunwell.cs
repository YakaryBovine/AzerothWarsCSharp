using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.ObjectiveSystem;
using MacroTools;
using System;
using System.Collections.Generic;
using System.Linq;
using WCSharp.Events;
using MacroTools.QuestSystem;

namespace WarcraftLegacies.Source.Powers
{
  public sealed class CorruptedSunwell : Power
  {
    private bool _isActive;
    private readonly List<Objective> _objectives = new();
    private readonly List<Capital> _sunwell;
    private readonly List<player> _playersWithPower = new();
    private readonly int _damageAmountPercentage;

    // Fix the IsActive property
    private bool IsActive
    {
      get => _isActive;
      set
      {
        _isActive = value;
        var prefix = IsActive ? "" : "|cffc0c0c0";
        Description = $"{prefix}While active, 20% of the mana spent on spells damages your own units. This power remains active until the Corrupted Sunwell is destroyed.";
        var researchLevel = IsActive ? 1 : 0;
        foreach (var player in _playersWithPower)
        {
          SetPlayerTechResearched(player, ResearchId, researchLevel);
        }
      }
    }

    public string Effect { get; init; } = "";

    public int ResearchId { get; init; }

    public CorruptedSunwell(int damageAmountPercentage, List<Capital> sunwell)
    {
      _damageAmountPercentage = damageAmountPercentage;
      Name = "Corrupted Sunwell";
      _sunwell = sunwell;
    }

    public override void OnAdd(player whichPlayer)
    {
      PlayerUnitEvents.Register(CustomPlayerUnitEvents.PlayerUnitCast, OnAbilityCast, GetPlayerId(whichPlayer)); // Changed to PlayerUnitCast event
      _playersWithPower.Add(whichPlayer);
    }

    public override void OnAdd(Faction whichFaction)
    {
      foreach (var sunwell in _sunwell)
      {
        // Changed to ObjectiveControlCapital constructor
        AddObjective(new ObjectiveControlCapital(sunwell, false)
        {
          EligibleFactions = new List<Faction> { whichFaction }
        });
      }
      RefreshIsActive();
    }

    public override void OnRemove(player whichPlayer)
    {
      PlayerUnitEvents.Unregister(CustomPlayerUnitEvents.PlayerUnitCast, OnAbilityCast, GetPlayerId(whichPlayer)); // Changed to PlayerUnitCast event
      _playersWithPower.Remove(whichPlayer);
      SetPlayerTechResearched(whichPlayer, ResearchId, 0);
    }

    public override void OnRemove(Faction whichFaction)
    {
      foreach (var objective in _objectives)
      {
        RemoveObjective(objective);
      }

      _objectives.Clear();
    }

    private void OnAbilityCast()
    {
      var castingUnit = GetTriggerUnit();
      var abilityId = GetSpellAbilityId();
      var level = GetUnitAbilityLevel(castingUnit, abilityId);
      var manaCost = BlzGetUnitAbilityManaCost(castingUnit, abilityId, level);
      var maxMana = BlzGetUnitMaxMana(castingUnit);

      var damage = (int)(manaCost * _damageAmountPercentage / 100);
      if (damage <= 0 || maxMana <= 0)
        return;

      foreach (var player in _playersWithPower)
      {
        if (GetPlayerId(player) == GetPlayerId(GetOwningPlayer(castingUnit)))
        {
          UnitDamageTarget(castingUnit, castingUnit, damage, false, false, ATTACK_TYPE_NORMAL, DAMAGE_TYPE_NORMAL, WEAPON_TYPE_WHOKNOWS);
          break;
        }
      }
    }

    private void AddObjective(Objective objective)
    {
      _objectives.Add(objective);
      objective.ProgressChanged += OnObjectiveProgressChanged;
    }

    private void RemoveObjective(Objective objective)
    {
      _objectives.Remove(objective);
      objective.ProgressChanged -= OnObjectiveProgressChanged;
    }

    private void OnObjectiveProgressChanged(object? sender, Objective objective) => RefreshIsActive();

    private void RefreshIsActive() => IsActive = _objectives.Any(x => x.Progress == QuestProgress.Complete);
  }
}