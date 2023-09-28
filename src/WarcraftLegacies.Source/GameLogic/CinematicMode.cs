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
      
      foreach (var unit in CreateGroup().EnumUnitsInRect(WCSharp.Shared.Data.Rectangle.WorldBounds).EmptyToList())
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
      foreach (var unit in CreateGroup().EnumUnitsInRect(WCSharp.Shared.Data.Rectangle.WorldBounds).EmptyToList())
        unit.PauseEx(false);
    }

    private static void PlayFactionMusic()
    {
      foreach (var player in WCSharp.Shared.Util.EnumeratePlayers())
        if (GetLocalPlayer() == player)
          PlayThematicMusic(player.GetFaction()?.CinematicMusic);
    }
  }
}