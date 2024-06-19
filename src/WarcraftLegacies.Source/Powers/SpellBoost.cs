using System;
using System.Collections.Generic;
using System.Linq;
using MacroTools;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.QuestSystem;
using WCSharp.Events;

namespace WarcraftLegacies.Source.Powers
{
  public sealed class SpellBoost : Power
  {
    private readonly int _bonusDamageAmountPercentage;
    private readonly List<Objective> _objectives = new();
    private readonly List<Capital> _sunwell;
    private bool _isActive;
    private readonly List<player> _playersWithPower = new();

    private bool IsActive
    {
      get => _isActive;
      set
      {
        _isActive = value;
        var prefix = IsActive ? "" : "|cffc0c0c0";
        Description = $"{prefix}When a unit you control cast an ability that would deal damage, it deals an additional {_bonusDamageAmountPercentage}% damage on top. Only active while your team controls the Sunwell.";
        var researchLevel = _isActive ? 1 : 0;
        foreach (var player in _playersWithPower)
          SetPlayerTechResearched(player, ResearchId, researchLevel);
      }
    }

    public string Effect { get; init; } = "";

    public int ResearchId { get; init; }

    public SpellBoost(int bonusDamageAmountPercentage, List<Capital> sunwell)
    {
      _bonusDamageAmountPercentage = bonusDamageAmountPercentage;
      Name = "SpellBoost";
      _sunwell = sunwell;
    }

    public override void OnAdd(player whichPlayer)
    {
      PlayerUnitEvents.Register(CustomPlayerUnitEvents.PlayerDealsDamage, OnDamage, GetPlayerId(whichPlayer));
      _playersWithPower.Add(whichPlayer);
    }

    public override void OnAdd(Faction whichFaction)
    {
      var sunwell = _sunwell.First(); 
      AddObjective(new ObjectiveControlCapital(sunwell, false)
      {
        EligibleFactions = new List<Faction> { whichFaction }
      });
      RefreshIsActive();
    }
    public override void OnRemove(player whichPlayer)
    {
      PlayerUnitEvents.Unregister(CustomPlayerUnitEvents.PlayerDealsDamage, OnDamage, GetPlayerId(whichPlayer));
      _playersWithPower.Remove(whichPlayer);
      SetPlayerTechResearched(whichPlayer, ResearchId, 0);
    }

    public override void OnRemove(Faction whichFaction)
    {
      foreach (var objective in _objectives)
        RemoveObjective(objective);

      _objectives.Clear();
    }


    private void OnDamage()
    {
      var damagingUnit = GetEventDamageSource();
      Console.WriteLine($"[Debug] Damage Source: {damagingUnit}");

      if (!IsActive)
      {
        Console.WriteLine("[Debug] SpellBoost is not active.");
        return;
      }

      var attackType = BlzGetEventAttackType();
      Console.WriteLine($"[Debug] Attack Type: {attackType}");

    
      if (attackType == ATTACK_TYPE_MAGIC || attackType == ATTACK_TYPE_NORMAL)
      {
        var originalDamage = GetEventDamage();
        var bonusDamage = (int)(originalDamage * (_bonusDamageAmountPercentage / 100.0f));
        BlzSetEventDamage(originalDamage + bonusDamage);

        Console.WriteLine($"[Debug] Original Damage: {originalDamage}");
        Console.WriteLine($"[Debug] Bonus Damage: {bonusDamage}");
        Console.WriteLine($"[Debug] Total Damage: {originalDamage + bonusDamage}");

        AddSpecialEffectTarget(Effect, damagingUnit, "origin").SetLifespan(1);

        // debug lines for attack type clarification
        if (attackType == ATTACK_TYPE_MAGIC)
        {
          Console.WriteLine("[Debug] The attack was of type MAGIC.");
        }
        else if (attackType == ATTACK_TYPE_NORMAL)
        {
          Console.WriteLine("[Debug] The attack was of type NORMAL.");
        }

        // Debugging for MAGIC or NORMAL attack damage
        Console.WriteLine("[Debug] Spell or Magic Attack Damage Applied: {bonusDamage}");
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
