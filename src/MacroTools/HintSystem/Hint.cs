using System.Collections.Generic;
using System.Linq;
using MacroTools.Extensions;
using static War3Api.Common;

namespace MacroTools.HintSystem
{
  public sealed class Hint
  {
    private const float HintInterval = 180;
    
    private static readonly List<Hint> Unread = new();
    private readonly string _msg;
    private static bool _initialized;

    public Hint(string msg)
    {
      _msg = msg;
    }

    public static void Register(Hint hint)
    {
      if (!_initialized)
      {
        Initialize();
      }
      Unread.Add(hint);
    }

    private void Display()
    {
      foreach (var player in WCSharp.Shared.Util.EnumeratePlayers())
        player.DisplayHint(_msg);
      Unread.Remove(this);
    }

    private static void DisplayRandom()
    {
      if (Unread.Count > 0)
      {
        Unread.ElementAt(GetRandomInt(0, Unread.Count - 1)).Display();
      }
    }

    private static void DisplayRandomHints()
    {
      foreach (var player in WCSharp.Shared.Util.EnumeratePlayers())
      {
        if (GetLocalPlayer() == player)
        {
          DisplayRandom();
        }
      }
    }

    private static void Initialize()
    {
      _initialized = true;
      var trig = CreateTrigger();
      TriggerRegisterTimerEvent(trig, HintInterval, true);
      TriggerAddAction(trig, DisplayRandomHints);
    }
  }
}