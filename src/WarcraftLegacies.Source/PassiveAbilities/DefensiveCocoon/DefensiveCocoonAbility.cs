using System.Collections.Generic;
using MacroTools.Extensions;
using MacroTools.PassiveAbilitySystem;
using WarcraftLegacies.Source.PassiveAbilities.Vengeance;
using WCSharp.Buffs;

namespace WarcraftLegacies.Source.PassiveAbilities.DefensiveCocoon
{
  /// <summary>
  /// When the ability holder takes lethal damage, they transform into a Spirit of Vengeance.
  /// If they attack enough times while a Spirit, they revive with some health.
  /// Otherwise, they die.
  /// </summary>
  public sealed class DefensiveCocoonAbility : TakeDamagePassiveAbility
  {
    /// <summary>
    /// If set, this needs to be researched for the ability to work.
    /// </summary>
    public required int RequiredResearch { get; init; }
    
    /// <summary>
    /// The amount of health the cocoon has, as a percentage of the ability wielders maximum hit points.
    /// </summary>
    public required float MaximumHealthPercentage { private get; init; }
    
    /// <summary>
    /// How long the cocoon lasts.
    /// </summary>
    public required float Duration { private get; init; }
    
    /// <summary>
    /// A unit type ID with the model of the cocoon.
    /// </summary>
    public required int EggId { private get; init; }
    
    /// <summary>
    /// The visual effect that occurs.
    /// </summary>
    public required string ReviveEffect { private get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="VengeanceAbility"/> class.
    /// </summary>
    /// <param name="damagedUnitTypeIds">The unit type IDs that can take damage to trigger this effect.</param>
    /// <param name="abilityTypeId">The ability whose level is used to determine the magnitude of the effect.</param>
    public DefensiveCocoonAbility(IEnumerable<int> damagedUnitTypeIds, int abilityTypeId) : base(damagedUnitTypeIds, abilityTypeId)
    {
    }
    
    /// <inheritdoc />
    public override void OnTakesDamage()
    {
      var damaged = GetTriggerUnit();
      var abilityLevel = GetUnitAbilityLevel(damaged, AbilityTypeId);
      if (!ShouldBecomeEgg(abilityLevel, damaged)) 
        return;
      
      BlzSetEventDamage(0);
      var vengeanceBuff = new DefensiveCocoonBuff(damaged, damaged)
      {
        Duration = Duration,
        EggId = EggId,
        ReviveEffect = ReviveEffect,
        MaximumHitPoints = (int)(damaged.GetMaximumHitPoints() * MaximumHealthPercentage)
      };
      BuffSystem.Add(vengeanceBuff);
    }

    private bool ShouldBecomeEgg(int abilityLevel, unit target) =>
      GetPlayerTechCount(target.OwningPlayer(), RequiredResearch, false) > 0 && 
      abilityLevel != 0 &&
      BlzGetUnitSkin(target) != EggId && 
      GetEventDamage() >= GetUnitState(target, UNIT_STATE_LIFE) &&
      !IsUnitIllusion(target);
  }
}