using MacroTools.Extensions;
using MacroTools.Libraries;
using MacroTools.SpellSystem;
using WCSharp.Shared.Data;
using static War3Api.Common; 

namespace MacroTools.Hazards
{
  public sealed class SolarJudgementHazard : Hazard
  {
    public float Radius { get; init; }
    public float BoltRadius { get; init; }
    public float BoltDamage { get; init; }
    public float UndeadDamageMultiplier { get; init; } = 1;
    public float HealMultiplier { get; init; } = 1;
    public string? EffectPath { get; init; }
    public string? EffectHealPath { get; init; }

    private void DoBolt(float x, float y)
    {
      DestroyEffect(AddSpecialEffect(EffectPath, x, y));
      var unitsInRange = CreateGroup().EnumUnitsInRange(new Point(x, y), BoltRadius).EmptyToList();
      foreach (var target in unitsInRange)
      {
        if (CastFilters.IsTargetEnemyAndAlive(Caster, target))
        {
          var damageMult = IsUnitType(target, UNIT_TYPE_UNDEAD) ? UndeadDamageMultiplier : 1f;
          UnitDamageTarget(Caster, target, BoltDamage * damageMult, false, false, ATTACK_TYPE_NORMAL, DAMAGE_TYPE_MAGIC,
            WEAPON_TYPE_WHOKNOWS);
        }
        if (CastFilters.IsTargetAllyAndAlive(Caster, target))
        {
          SetUnitState(target, UNIT_STATE_LIFE, GetUnitState(target, UNIT_STATE_LIFE) + BoltDamage * HealMultiplier);
          DestroyEffect(AddSpecialEffectTarget(EffectHealPath, target, "origin"));
        }
      }
    }
    
    private void DoRandomBolts()
    {
      var randomAngle = GetRandomReal(0, 2 * MathEx.Pi);
      var randomRadius = GetRandomReal(0, Radius);
      DoBolt(Position.X + randomRadius * Cos(randomAngle), Position.Y + randomRadius * Sin(randomAngle));
    }
    
    protected override void OnPeriodic() => DoRandomBolts();

    public SolarJudgementHazard(unit caster, float x, float y) : base(caster, x, y)
    {
    }
  }
}