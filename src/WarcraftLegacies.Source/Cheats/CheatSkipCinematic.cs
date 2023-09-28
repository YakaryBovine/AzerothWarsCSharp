using System;
using MacroTools.Cheats;
using WarcraftLegacies.Source.GameLogic;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Cheats
{
  public sealed class CheatSkipCinematic
  {
    public static void Init()
    {
      var timer = CreateTimer();
      TimerStart(timer, 1, false, DelayedSetup);
    }

    private static void Actions()
    {
      if (!TestMode.CheatCondition()) return;
      try
      {
        CinematicMode.EndEarly();
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Failed to execute {nameof(CheatSkipCinematic)}: {ex}");
      }
      finally
      {
        DestroyTrigger(GetTriggeringTrigger());
      }
    }

    private static void DelayedSetup()
    {
      var trig = CreateTrigger();
      foreach (var player in WCSharp.Shared.Util.EnumeratePlayers())
        TriggerRegisterPlayerEvent(trig, player, EVENT_PLAYER_END_CINEMATIC);
      TriggerAddAction(trig, Actions);
    }
  }
}