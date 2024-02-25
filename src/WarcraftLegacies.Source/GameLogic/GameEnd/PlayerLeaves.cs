using System;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.GameLogic.GameEnd
{
  /// <summary>
  /// When a player leaves the game, their <see cref="Faction"/> is defeated.
  /// </summary>
  public static class PlayerLeaves
  {
    private static void PlayerLeavesGame()
    {
      try
      {
        var triggerPlayer = GetTriggerPlayer();

        var playerFaction = triggerPlayer.GetFaction();
        
        foreach (var player in WCSharp.Shared.Util.EnumeratePlayers())
          DisplayTextToPlayer(player, 0, 0,
            playerFaction != null
              ? $"{playerFaction.ColoredName} has left the game."
              : $"{GetPlayerName(triggerPlayer)} has left the game.");

        if (playerFaction != null && playerFaction.ScoreStatus != ScoreStatus.Defeated)
          playerFaction.Defeat();
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Failed to execute {nameof(PlayerLeaves)}: {ex}");
      }
    }

    /// <summary>
    /// Sets up <see cref="PlayerLeaves"/>.
    /// </summary>
    public static void Setup()
    {
      var trigger = CreateTrigger();
      foreach (var player in WCSharp.Shared.Util.EnumeratePlayers())
        TriggerRegisterPlayerEvent(trigger, player, EVENT_PLAYER_LEAVE);
      TriggerAddAction(trigger, PlayerLeavesGame);
    }
  }
}