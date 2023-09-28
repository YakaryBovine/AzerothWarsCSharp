using MacroTools.CommandSystem;
using System.Linq;
using WCSharp.Shared;
using static War3Api.Common;

namespace MacroTools.Cheats
{
  /// <summary>
  /// Responsible for determining whether or not the map should be run in test mode.
  /// </summary>
  public static class TestMode
  {
    /// <summary>
    /// If true, cheats are active for all players.
    /// </summary>
    public static bool AreCheatsActive { get; private set; }

    /// <summary>
    /// Returns true if <see cref="GetTriggerPlayer"/> has cheats enabled.
    /// </summary>
    public static bool CheatCondition()
    {
      var triggerPlayerName = GetPlayerName(GetTriggerPlayer());
      return triggerPlayerName is "YakaryBovine#6863" or "Lordsebas#11619" || AreCheatsActive;
    }

    private static void CreateInfoQuests(CommandManager commandManager)
    {
      var newQuest = CreateQuest();
      QuestSetTitle(newQuest, "Cheats");
      var description = commandManager.GetAllCommands().Aggregate("",
        (current, command) => $"{current} -{command.CommandText}: {command.Description}\n");
      QuestSetDescription(newQuest, description);
      QuestSetDiscovered(newQuest, true);
      QuestSetRequired(newQuest, true);
      QuestSetIconPath(newQuest, @"ReplaceableTextures\CommandButtons\BTNStaffOfTeleportation.blp");
      QuestSetCompleted(newQuest, false);
    }

    /// <summary>
    /// Sets up <see cref="TestMode"/>.
    /// </summary>
    public static void Setup(CommandManager commandManager)
    {
      AreCheatsActive = Util.EnumeratePlayers().Count(player =>
        GetPlayerSlotState(player) == PLAYER_SLOT_STATE_PLAYING && GetPlayerController(player) == MAP_CONTROL_USER) < 2;

      if (AreCheatsActive)
        CreateInfoQuests(commandManager);
    }
  }
}