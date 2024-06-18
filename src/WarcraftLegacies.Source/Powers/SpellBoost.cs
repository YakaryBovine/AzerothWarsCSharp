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

    /// <summary>The effect that appears when a unit deals bonus damage.</summary>
    public string Effect { get; init; } = "";

    /// <summary>ID of the research associated with this power.</summary>
    public int ResearchId { get; init; }

    public SpellBoost(int bonusDamageAmountPercentage, List<Capital> sunwell)
    {
      _bonusDamageAmountPercentage = bonusDamageAmountPercentage;
      Name = "SpellBoost";
      _sunwell = sunwell;
    }

    /// <inheritdoc />
    public override void OnAdd(player whichPlayer)
    {
      PlayerUnitEvents.Register(CustomPlayerUnitEvents.PlayerDealsDamage, OnDamage, GetPlayerId(whichPlayer));
      _playersWithPower.Add(whichPlayer);
    }

    /// <inheritdoc />
    public override void OnAdd(Faction whichFaction)
    {
      var sunwell = _sunwell.First(); // Assuming `_sunwell` is a list of capitals
      AddObjective(new ObjectiveControlCapital(sunwell, false)
      {
        EligibleFactions = new List<Faction> { whichFaction }
      });
      RefreshIsActive();
    }

    /// <inheritdoc />
    public override void OnRemove(player whichPlayer)
    {
      PlayerUnitEvents.Unregister(CustomPlayerUnitEvents.PlayerDealsDamage, OnDamage, GetPlayerId(whichPlayer));
      _playersWithPower.Remove(whichPlayer);
      SetPlayerTechResearched(whichPlayer, ResearchId, 0);
    }

    /// <inheritdoc />
    public override void OnRemove(Faction whichFaction)
    {
      foreach (var objective in _objectives)
        RemoveObjective(objective);

      _objectives.Clear();
    }


    private void OnDamage()
    {
      var damagingUnit = GetEventDamageSource();
      if (!IsActive)
        return;

      var damageType = BlzGetEventDamageType();
      if (damageType != DAMAGE_TYPE_MAGIC)
        return;

      var bonusDamage = (int)(GetEventDamage() * 0.2f);
      BlzSetEventDamage(GetEventDamage() + bonusDamage);
      AddSpecialEffectTarget(Effect, damagingUnit, "origin")
          .SetLifespan(1);

      var playerId = GetPlayerId(GetOwningPlayer(damagingUnit));
      DisplayTimedTextToPlayer(GetLocalPlayer(), 0, 0, 5, "Bonus damage applied to your ability!");
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