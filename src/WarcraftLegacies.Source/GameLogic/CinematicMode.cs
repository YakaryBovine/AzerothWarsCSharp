using System.Collections.Generic;
using MacroTools.Extensions;
using static War3Api.Common;

namespace WarcraftLegacies.Source.GameLogic
{
  /// <summary>
  /// Used to engage cinematic mode, which prevents players from taking actions and manipulates sound
  /// and weather effects for a cinematic experience.
  /// </summary>
  public static class CinematicMode
  {
    private static timer? _cinematicTimer;
    private static timer? _musicTimer;
    private static CinematicState _state = CinematicState.Inactive;
    private static List<unit>? _pausedUnits = new();
    
    /// <summary>
    /// Initiates cinematic mode.
    /// </summary>
    /// <param name="duration">How long cinematic mode should last.</param>
    public static void Setup(float duration)
    {
      _cinematicTimer = CreateTimer();
      TimerStart(_cinematicTimer, duration, false, TimerEnd);

      _musicTimer = CreateTimer();
      TimerStart(_musicTimer, 2.1f, false, PlayFactionMusic);

      FogEnable(false);
      FogMaskEnable(false);

      _pausedUnits = CreateGroup()
        .EnumUnitsInRect(WCSharp.Shared.Data.Rectangle.WorldBounds)
        .EmptyToList();
      
      foreach (var unit in _pausedUnits)
        unit.PauseEx(true);

      _state = CinematicState.Active;
    }

    /// <summary>
    /// Ends cinematic mode early for all players.
    /// </summary>
    public static void EndEarly()
    {
      DestroyTimer(_cinematicTimer);
      TimerEnd();
    }

    private static void TimerEnd()
    {
      if (_state != CinematicState.Active)
        return;

      FogEnable(true);

      SetMapMusic("music", true, 0);

      VolumeGroupReset();
      VolumeGroupSetVolume(SOUND_VOLUMEGROUP_AMBIENTSOUNDS, 0.4f);

      DestroyTimer(_cinematicTimer);
      DestroyTimer(_musicTimer);
      _state = CinematicState.Finished;

      if (_pausedUnits != null)
      {
        foreach (var unit in _pausedUnits)
          unit.PauseEx(false);
        _pausedUnits.Clear();
        _pausedUnits = null;
      }
    }

    private static void PlayFactionMusic()
    {
      foreach (var player in WCSharp.Shared.Util.EnumeratePlayers())
        player.PlayMusicThematic(player.GetFaction().CinematicMusic);
    }
  }
}