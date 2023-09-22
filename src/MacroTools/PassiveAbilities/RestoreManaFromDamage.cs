using System.Collections.Generic;
using MacroTools.Extensions;
using MacroTools.PassiveAbilitySystem;
using static War3Api.Common;

namespace MacroTools.PassiveAbilities
{
  /// <summary>
  /// The unit with this ability gains mana based on the damage they deal.
  /// </summary>
  public sealed class RestoreManaFromDamage : PassiveAbility, IAppliesEffectOnDamage
  {
    private readonly int _abilityTypeId;

    /// <summary>
    /// This effect appears on the unit when they restore mana from this ability.
    /// </summary>
    public string? Effect { get; init; } = "";
    
    /// <summary>
    /// The amount of mana this unit gains per point of damage they deal.
    /// </summary>
    public LeveledAbilityField<float> ManaPerDamage { get; init; } = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="RestoreManaFromDamage"/> class.
    /// </summary>
    /// <param name="unitTypeId"><inheritdoc /></param>
    /// <param name="abilityTypeId">The Warcraft 3 ability representing this instance.</param>
    public RestoreManaFromDamage(int unitTypeId, int abilityTypeId) : base(unitTypeId) => _abilityTypeId = abilityTypeId;

    /// <summary>
    /// Initializes a new instance of the <see cref="RestoreManaFromDamage"/> class.
    /// </summary>
    /// <param name="unitTypeIds"><inheritdoc /></param>
    /// <param name="abilityTypeId">The Warcraft 3 ability representing this instance.</param>
    public RestoreManaFromDamage(IEnumerable<int> unitTypeIds, int abilityTypeId) : base(unitTypeIds) => _abilityTypeId = abilityTypeId;

    /// <inheritdoc />
    public void OnDealsDamage()
    {
      var damager = GetEventDamageSource();
      var manaPerDamage = GetEventDamage() * (ManaPerDamage.Base + ManaPerDamage.PerLevel * GetUnitAbilityLevel(damager, _abilityTypeId));
      damager.RestoreMana(manaPerDamage);
      AddSpecialEffectTarget(Effect, damager, "origin")
        .SetLifespan();
    }
  }
}