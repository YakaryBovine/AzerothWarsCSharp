using System;
using MacroTools.Extensions;
using static War3Api.Common;

namespace WarcraftLegacies.Source.GameLogic
{
  /// <summary>
  /// Display intro text to all players after some period of time has elapsed.
  /// </summary>
  public static class DisplayIntroText
  {
    /// <summary>
    /// Displays intro text to all players after some period of time has elapsed.
    /// </summary>
    /// <param name="displayTime">The time after which to display intro text, in seconds.</param>
    public static void Setup(float displayTime)
    {
      CreateTimer().Start(displayTime, false, () =>
      {
        try
        {
          foreach (var player in WCSharp.Shared.Util.EnumeratePlayers())
            DisplayTextToPlayer(player, 0, 0, player.GetFaction()?.IntroText ?? "");
          
          DestroyTimer(GetExpiredTimer());
        }
        catch (Exception ex)
        {
          Console.WriteLine($"Error displaying intro text {ex}");
        }
      });
      foreach (var player in WCSharp.Shared.Util.EnumeratePlayers())
        DisplayTimedTextToPlayer(player, 0, 0, displayTime - 1, @"|cffffcc00Warcraft Legacies|r
  |cffaaaaaaJoin our Discord:|r discord.gg/pnWZs69
  |cffff0000Support our Patreon:|r https://www.patreon.com/lordsebas

  If you are a new player, look at the Quest (F9) tab to see your objectives
  ");
    }
  }
}