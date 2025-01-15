using MacroTools.Extensions;
using MacroTools.FactionSystem;
using WCSharp.Events;
using static War3Api.Common;

namespace MacroTools
{
  /// <summary>
  /// Provides a set of custom player unit events for the <see cref="PlayerUnitEvents"/> system.
  /// </summary>
  public static class CustomPlayerUnitEvents
  {
    /// <summary>
    /// A specific player finishes training any unit.
    /// </summary>
    public static string PlayerFinishesTraining => nameof(PlayerFinishesTraining);

    /// <summary>
    /// A specific player deals damage with any of their units.
    /// </summary>
    public static string PlayerDealsDamage => nameof(PlayerDealsDamage);
    
    /// <summary>
    /// A specific player takes damage with any of their units.
    /// </summary>
    public static string PlayerTakesDamage => nameof(PlayerTakesDamage);

    /// <summary>
    /// A unit owned by a specific player dies.
    /// </summary>
    public static string PlayerUnitDies => nameof(PlayerUnitDies);

    /// <summary>
    /// A unit owned by a specific <see cref="Faction"/> kills a unit.
    /// </summary>
    public static string FactionUnitKills => nameof(FactionUnitKills);

    /// <summary>
    /// A unit owned by a specific player casts any spell.
    /// </summary>
    public static string PlayerSpellEffect => nameof(PlayerSpellEffect);

    static CustomPlayerUnitEvents()
    {
      PlayerUnitEvents.AddCustomEvent(PlayerFinishesTraining, () => GetPlayerId(GetOwningPlayer(GetTrainedUnit())),
        EVENT_PLAYER_UNIT_TRAIN_FINISH);
      PlayerUnitEvents.AddCustomEvent(PlayerDealsDamage, () => GetPlayerId(GetOwningPlayer(GetEventDamageSource())),
        EVENT_PLAYER_UNIT_DAMAGED);
      PlayerUnitEvents.AddCustomEvent(PlayerTakesDamage, () => GetPlayerId(GetOwningPlayer(GetTriggerUnit())),
        EVENT_PLAYER_UNIT_DAMAGED);
      PlayerUnitEvents.AddCustomEvent(PlayerUnitDies, () => GetPlayerId(GetOwningPlayer(GetTriggerUnit())),
        EVENT_PLAYER_UNIT_DEATH);
      PlayerUnitEvents.AddCustomEvent(FactionUnitKills, () => GetKillingUnit().OwningPlayer().GetFaction().Id,
        EVENT_PLAYER_UNIT_DEATH);
      PlayerUnitEvents.AddCustomEvent(PlayerSpellEffect, () => GetPlayerId(GetOwningPlayer(GetTriggerUnit())),
        EVENT_PLAYER_UNIT_SPELL_EFFECT);
    }
  }
}