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
  public sealed class FontOfPower : Power
  {
    private readonly List<Objective> _objectives = new();
    private readonly List<Capital> _fontsOfPower;
    private bool _isActive;
    private readonly List<player> _playersWithPower = new();

    private bool IsActive
    {
      get => _isActive;
      set
      {
        _isActive = value;
        var prefix = IsActive ? "" : "|cffc0c0c0";
        Description = $"{prefix}All units deal 10% extra damage and regain 15% of the mana cost of abilities. Only active while your team controls the Sunwell, Black Temple, or Nordrassil.";
        var researchLevel = _isActive ? 1 : 0;
        foreach (var player in _playersWithPower)
          player.GetFaction()?.SetObjectLevel(ResearchId, researchLevel);
      }
    }

    /// <summary>Active when the <see cref="Power"/> is active, inactive otherwise.</summary>
    public int ResearchId { get; init; }

    public FontOfPower(List<Capital> fontsOfPower)
    {
      _fontsOfPower = fontsOfPower;
    }

    /// <inheritdoc />
    public override void OnAdd(player whichPlayer)
    {
      PlayerUnitEvents.Register(CustomPlayerUnitEvents.PlayerDealsDamage, OnDamage, GetPlayerId(whichPlayer));
      PlayerUnitEvents.Register(CustomPlayerUnitEvents.PlayerSpellEffect, OnSpellCast, GetPlayerId(whichPlayer));
      _playersWithPower.Add(whichPlayer);
    }

    /// <inheritdoc />
    public override void OnAdd(Faction whichFaction)
    {
      whichFaction.ModObjectLimit(ResearchId, Faction.UNLIMITED);
      foreach (var fontOfPower in _fontsOfPower)
        AddObjective(new ObjectiveControlCapital(fontOfPower, false)
        {
          EligibleFactions = new List<Faction> { whichFaction }
        });
      RefreshIsActive();
    }

    /// <inheritdoc />
    public override void OnRemove(player whichPlayer)
    {
      PlayerUnitEvents.Unregister(CustomPlayerUnitEvents.PlayerDealsDamage, OnDamage, GetPlayerId(whichPlayer));
      PlayerUnitEvents.Unregister(CustomPlayerUnitEvents.PlayerSpellEffect, OnSpellCast, GetPlayerId(whichPlayer));
      _playersWithPower.Remove(whichPlayer);
    }

    /// <inheritdoc />
    public override void OnRemove(Faction whichFaction)
    {
      whichFaction.ModObjectLimit(ResearchId, -Faction.UNLIMITED);
      whichFaction.SetObjectLevel(ResearchId, 0);
      
      foreach (var objective in _objectives)
        RemoveObjective(objective);

      _objectives.Clear();
    }

    /// <summary>
    /// Gets all fonts that currently count for the activeness of this Power.
    /// </summary>
    public IEnumerable<Capital> GetActiveFonts() =>
      _fontsOfPower.Where(x => x.OwningPlayer != null && UnitAlive(x.Unit) && _playersWithPower.Contains(x.OwningPlayer));

    private void OnDamage()
    {
      if (!IsActive) 
        return;

      BlzSetEventDamage(GetEventDamage() * 1.1f);
    }

    private void OnSpellCast()
    {
      if (!IsActive) 
        return;

      var castingUnit = GetTriggerUnit();
      if (castingUnit == null) 
        return;

      var abilityId = GetSpellAbilityId();
      var abilityLevel = GetUnitAbilityLevel(castingUnit, abilityId);
      var manaCost = BlzGetUnitAbilityManaCost(castingUnit, abilityId, abilityLevel - 1);

      if (manaCost <= 0)
        return;
      
      var manaRefund = manaCost * 0.15f;
      
      SetUnitState(castingUnit, UNIT_STATE_MANA, GetUnitState(castingUnit, UNIT_STATE_MANA) + manaRefund);
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
