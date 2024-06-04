using System.Collections.Generic;
using MacroTools.Extensions;
using MacroTools.PassiveAbilitySystem;
using WarcraftLegacies.Shared.Models.Abilities;
using static War3Api.Common;

namespace MacroTools.PassiveAbilities
{
  /// <summary>
  /// Causes the unit to summon a number of units whenever they cast a spell.
  /// </summary>
  public sealed class SummonUnitOnCast : PassiveAbility
  {
    private readonly int _abilityTypeId;

    /// <summary>
    /// How long the summoned unit should last.
    /// </summary>
    public float Duration { get; init; }
    
    /// <summary>
    /// The unit type to summon on death.
    /// </summary>
    public int SummonUnitTypeId { get; init; }

    /// <summary>
    /// How many units to summon.
    /// </summary>
    public LeveledAbilityField<int> SummonCount { get; init; } = new();

    /// <summary>
    /// The special effect that appears when the ability triggers.
    /// </summary>
    public string SpecialEffectPath { get; init; } = "";
    
    /// <summary>
    /// The percentage chance that the effect will occur on cast.
    /// </summary>
    public float ProcChance { get; init; }
    
    /// <summary>
    /// Only spells in this list willl summon a unit.
    /// </summary>
    public List<int> AbilityWhitelist { get; init; } = new();
    
    /// <inheritdoc />
    public SummonUnitOnCast(int unitTypeId, int abilityTypeId) : base(unitTypeId)
    {
      _abilityTypeId = abilityTypeId;
    }
    
    /// <inheritdoc />
    public override void OnSpellEffect()
    {
      var triggerUnit = GetTriggerUnit();
      var abilityLevel = GetUnitAbilityLevel(triggerUnit, _abilityTypeId);
      
      if (GetRandomReal(0, 1) > ProcChance)
        return;

      if (abilityLevel == 0 || !AbilityWhitelist.Contains(GetSpellAbilityId()))
        return;
      
      var casterPosition = triggerUnit.GetPosition();
      var summonCount = SummonCount.Base + SummonCount.PerLevel * abilityLevel;

      for (var i = 0; i < summonCount; i++)
      {
        var summonedUnit = CreateUnit(triggerUnit.OwningPlayer(), SummonUnitTypeId, casterPosition.X, casterPosition.Y, triggerUnit.GetFacing())
          .AddType(UNIT_TYPE_SUMMONED)
          .SetTimedLife(Duration);
        var summonedUnitX = GetUnitX(summonedUnit);
        var summonedUnitY = GetUnitY(summonedUnit);
        AddSpecialEffect(SpecialEffectPath, summonedUnitX, summonedUnitY)
          .SetLifespan(1);
      }
    }
  }
}