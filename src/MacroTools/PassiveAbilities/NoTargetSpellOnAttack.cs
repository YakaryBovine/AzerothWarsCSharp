using MacroTools.PassiveAbilitySystem;
using MacroTools.SpellSystem;
using static War3Api.Common;

namespace MacroTools.PassiveAbilities
{
  /// <summary>
  /// When the unit deals damage, it has a chance to cast a dummy spell without a target.
  /// </summary>
  public sealed class NoTargetSpellOnAttack : PassiveAbility, IAppliesEffectOnDamage
  {
    /// <summary>
    /// The unit type ID which has this <see cref="PassiveAbility"/> should also have an ability with this ID.
    /// </summary>
    public int AbilityTypeId { get; }
    
    /// <summary>
    /// The dummy spell to cast on attack.
    /// </summary>
    public int DummyAbilityId { get; init; }

    /// <summary>
    /// An order ID that can be used to cast the specified dummy ability.
    /// </summary>
    public int DummyOrderId { get; init; }
    
    /// <summary>
    /// The percentage chance that the effect will occur on attack.
    /// </summary>
    public float ProcChance { get; init; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="NoTargetSpellOnAttack"/> class.
    /// </summary>
    /// <param name="unitTypeId"><inheritdoc /></param>
    /// <param name="abilityTypeId">The ability the provided unit type has which represents this object.</param>
    public NoTargetSpellOnAttack(int unitTypeId, int abilityTypeId) : base(unitTypeId)
    {
      AbilityTypeId = abilityTypeId;
    }

    /// <inheritdoc />

    public void OnDealsDamage()
    {
      var caster = GetEventDamageSource();
      if (!BlzGetEventIsAttack() || GetUnitAbilityLevel(caster, AbilityTypeId) == 0)
        return;

      if (GetRandomReal(0, 1) < ProcChance)
      {
        DoSpellNoTarget(caster);
      }
    }
    private void DoSpellNoTarget(unit caster) => DummyCast.DummyCastNoTarget(caster, DummyAbilityId,
      DummyOrderId, GetUnitAbilityLevel(caster, AbilityTypeId));
  }
}