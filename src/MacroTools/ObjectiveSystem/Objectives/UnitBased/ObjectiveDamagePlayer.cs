using MacroTools.Extensions;
using MacroTools.QuestSystem;
using WCSharp.Events;
using static War3Api.Common;

namespace MacroTools.ObjectiveSystem.Objectives.UnitBased
{
  /// <summary>
  /// An <see cref="Objective"/> that is completed when the holder deals any damage to any unit of a particular <see cref="player"/>.
  /// </summary>
  public sealed class ObjectiveDamagePlayer : Objective
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="ObjectiveDamagePlayer"/> class.
    /// </summary>
    /// <param name="playerToDamage"></param>
    public ObjectiveDamagePlayer(player playerToDamage)
    {
      PlayerUnitEvents.Register(CustomPlayerUnitEvents.PlayerTakesDamage, () =>
      {
        if (IsPlayerOnSameTeamAsAnyEligibleFaction(GetEventDamageSource().OwningPlayer()))
          Progress = QuestProgress.Complete;
      }, GetPlayerId(playerToDamage));
    }
  }
}