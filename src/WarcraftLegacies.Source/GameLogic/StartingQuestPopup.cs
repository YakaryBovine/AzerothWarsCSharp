using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.QuestSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.GameLogic
{
  /// <summary>
  /// Responsible for displaying each <see cref="Faction"/>'s starting <see cref="QuestData"/>.
  /// </summary>
  public static class StartingQuestPopup
  {
    /// <summary>
    /// Displays each <see cref="Faction"/>'s starting <see cref="QuestData"/> to the occupying player
    /// after a period of time has elapsed.
    /// </summary>
    /// <param name="timeToDisplay">Number of seconds after which to display the quests.</param>
    public static void Setup(float timeToDisplay)
    {
      var trig = CreateTrigger();
      TriggerRegisterTimerEvent(trig, timeToDisplay, false);
      TriggerAddAction(trig, () =>
      {
        foreach (var player in WCSharp.Shared.Util.EnumeratePlayers())
        {
          var playerFaction = player.GetFaction();
          if (playerFaction?.StartingQuest != null)
            playerFaction.DisplayDiscovered(playerFaction.StartingQuest);
        }
      });
    }
  }
}