using System.Collections.Generic;
using MacroTools.Missiles;
using MacroTools.PassiveAbilitySystem;
using WarcraftLegacies.Shared.Models.Abilities;
using WCSharp.Missiles;
using static War3Api.Common;

namespace MacroTools.PassiveAbilities
{
  /// <summary>
  /// The hero gains arcane orbs when casting abilities, which then collide with enemy units to deal damage.
  /// </summary>
  public sealed class DefensiveOrbs : PassiveAbility
  {
    /// <summary>
    /// The radius in which the orbs orbit.
    /// </summary>
    public float OrbitRadius { get; init; }

    /// <summary>
    /// The time it takes for the orbs to complete a full rotation.
    /// </summary>
    public float OrbitalPeriod { get; init; }

    /// <summary>
    /// The appearance of the orbs.
    /// </summary>
    public string OrbEffectPath { get; init; } = "";

    /// <summary>
    /// The amount of damage dealt by each orb when they explode.
    /// </summary>
    public LeveledAbilityField<float> Damage { get; init; } = new();

    /// <summary>
    /// The collision radius for the orbs.
    /// </summary>
    public LeveledAbilityField<float> CollisionRadius { get; init; } = new();

    /// <summary>
    /// How long orbs last before detonating.
    /// </summary>
    public float OrbDuration { get; init; }

    /// <summary>
    /// Only abilities in this list generate defensive orbs.
    /// </summary>
    public List<int> AbilityWhitelist { get; init; } = new();

    private readonly int _abilityTypeId;

    /// <inheritdoc />
    public DefensiveOrbs(int unitTypeId, int abilityTypeId) : base(unitTypeId)
    {
      _abilityTypeId = abilityTypeId;
    }
    
    /// <inheritdoc />
    public override void OnSpellEffect()
    {
      var caster = GetTriggerUnit();
      var abilityLevel = GetUnitAbilityLevel(caster, _abilityTypeId);
      if (abilityLevel == 0 || !AbilityWhitelist.Contains(GetSpellAbilityId()))
        return;
      var newOrb = new DefensiveOrbMissile(caster, caster)
      {
        OrbitalPeriod = OrbitalPeriod,
        CollisionRadius = CollisionRadius.Base + CollisionRadius.PerLevel * abilityLevel,
        EffectString = OrbEffectPath,
        Range = OrbitRadius,
        Damage = Damage.Base + Damage.PerLevel * abilityLevel,
        Duration = OrbDuration
      };
      MissileSystem.Add(newOrb);
    }
  }
}