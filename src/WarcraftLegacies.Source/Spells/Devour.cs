using MacroTools;
using MacroTools.Extensions;
using MacroTools.SpellSystem;
using WarcraftLegacies.Shared;
using WarcraftLegacies.Shared.Models.Abilities;
using WCSharp.Shared.Data;

namespace WarcraftLegacies.Source.Spells
{
  /// <summary>
  /// Instantly kills the target unit, or damages it if it's Resistant. Also heals the caster.
  /// </summary>
  public sealed class Devour : Spell
  {
    /// <summary>
    /// How much to heal the caster for.
    /// </summary>
    public float PercentageOfMaxHealth { get; init; } = 0.5f;

    /// <summary>
    /// How much damage to deal to resistant targets.
    /// </summary>
    public LeveledAbilityField<float> Damage { get; init; } = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="Devour"/> class.
    /// </summary>
    /// <param name="id"><inheritdoc /></param>
    public Devour(int id) : base(id)
    {
    }

    /// <inheritdoc />
    public override void OnCast(unit caster, unit target, Point targetPoint)
    {
      if (target.IsResistant())
        target.TakeDamage(caster, Damage.Base + Damage.PerLevel * GetAbilityLevel(caster), damageType: DAMAGE_TYPE_MAGIC);
      else
        target.TakeDamage(caster, target.GetHitPoints(), damageType: DAMAGE_TYPE_UNIVERSAL, attackType: ATTACK_TYPE_CHAOS);
      
      SetUnitState(caster, UNIT_STATE_LIFE, GetUnitState(caster, UNIT_STATE_LIFE) + GetUnitState(caster, UNIT_STATE_MAX_LIFE)*PercentageOfMaxHealth);
    }
  }
}