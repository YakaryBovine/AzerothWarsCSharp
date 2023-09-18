using MacroTools.Extensions;
using WCSharp.Buffs;
using WCSharp.Events;
using static War3Api.Common;

namespace MacroTools.Buffs
{
  /// <summary>
  /// The unit becomes a Spirit of Vengeance when this is applied, healing for an amount and gaining bonus damage.
  /// If the unit attacks some number of times before the buff expires, it revives.
  /// Otherwise, it dies.
  /// </summary>
  public sealed class VengeanceBuff : PassiveBuff
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="VengeanceBuff"/> class.
    /// </summary>
    public VengeanceBuff(unit caster, unit target) : base(caster, target)
    {
    }

    /// <summary>
    /// How much to heal the caster whey exit the vengeance form.
    /// </summary>
    public float Heal { private get; init; }
    
    /// <summary>
    /// How much extra damage the vengeance form has.
    /// </summary>
    public int BonusDamage { private get; init; }
    
    /// <summary>
    /// The effect when the vengeance form is exited ans the hero is revived.
    /// </summary>
    public string? ReviveEffect { private get; init; }
    
    /// <summary>
    /// How many hits the hero needs to make to revive out of the vengeance form.
    /// </summary>
    public int HitsReviveThreshold { private get; init; }
    
    /// <summary>
    /// The unit type ID of the vengeance form.
    /// </summary>
    public int AlternateFormId { private get; init; }

    /// <summary>
    ///   The unit type ID of the unit before it was transformed.
    /// </summary>
    private int OriginalFormId { get; set; }

    /// <summary>
    ///   How many times this unit has struck any other unit since gaining Vengeance.
    /// </summary>
    private int HitsDone { get; set; }

    private void OnInflictsDamage()
    {
      var isAttackerAlliedToVictim = GetPlayerAlliance(GetEventDamageSource().OwningPlayer(),
        GetTriggerUnit().OwningPlayer(), ALLIANCE_PASSIVE);
      if (!BlzGetEventIsAttack() || isAttackerAlliedToVictim) 
        return;
      
      HitsDone++;
      if (HitsDone >= HitsReviveThreshold) 
        Active = false;
    }

    /// <inheritdoc />
    public override void OnApply()
    {
      OriginalFormId = GetUnitTypeId(Target);
      BlzSetUnitSkin(Target, AlternateFormId);
      DestroyEffect(AddSpecialEffect(ReviveEffect, GetUnitX(Target), GetUnitY(Target)));
      SetUnitState(Target, UNIT_STATE_LIFE, Heal);
      BlzSetUnitBaseDamage(Target, BlzGetUnitBaseDamage(Target, 0) + BonusDamage, 0);
      PlayerUnitEvents.Register(UnitTypeEvent.Damaging, OnInflictsDamage, OriginalFormId);
    }

    /// <inheritdoc />
    public override void OnDispose()
    {
      BlzSetUnitBaseDamage(Target, BlzGetUnitBaseDamage(Target, 0) - BonusDamage, 0);
      BlzSetUnitSkin(Caster, OriginalFormId);
      PlayerUnitEvents.Unregister(UnitTypeEvent.Damaging, OnInflictsDamage, OriginalFormId);
      if (HitsDone >= HitsReviveThreshold)
        DestroyEffect(AddSpecialEffect(ReviveEffect, GetUnitX(Caster), GetUnitY(Caster)));
      else
        KillUnit(Caster);
    }
  }
}