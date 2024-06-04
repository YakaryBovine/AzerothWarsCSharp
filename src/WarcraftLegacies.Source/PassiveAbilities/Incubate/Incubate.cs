using MacroTools;
using MacroTools.PassiveAbilitySystem;
using WarcraftLegacies.Shared;
using WarcraftLegacies.Shared.Models.Abilities;
using WCSharp.Buffs;

namespace WarcraftLegacies.Source.PassiveAbilities.Incubate
{
  /// <summary>
  /// Causes summoned units mature after some time, allowing it to hatch.
  /// </summary>
  public sealed class Incubate : PassiveAbility, IEffectOnSummonedUnit
  {
    private readonly int _abilityTypeId;

    /// <summary>
    /// How long the egg takes to mature.
    /// </summary>
    public required LeveledAbilityField<float> MaturationDuration { get; init; }
    
    /// <summary>The unit type ID of the unit that gets hatched.</summary>
    public required int HatchedUnitTypeId { get; init; }
    
    /// <inheritdoc />
    public Incubate(int unitTypeId, int abilityTypeId) : base(unitTypeId)
    {
      _abilityTypeId = abilityTypeId;
    }

    /// <inheritdoc />
    public void OnSummonedUnit()
    {
      var summonedUnit = GetSummonedUnit();
      UnitRemoveType(summonedUnit, UNIT_TYPE_SUMMONED);
      var duration = MaturationDuration.Base +
                     MaturationDuration.PerLevel * GetUnitAbilityLevel(GetSummoningUnit(), _abilityTypeId);
      var immatureEggBuff = new ImmatureEggBuff(summonedUnit)
      {
        Duration = duration,
        HatchedUnitTypeId = HatchedUnitTypeId,
        MatureEggUnitTypeId = UNIT_ZB4E_MATURE_EGG_INCUBATE
      };
      BuffSystem.Add(immatureEggBuff);
    }
  }
}