using static War3Api.Common;

namespace MacroTools.Extensions
{
  public static class PlayerSoundExtensions
  {
    /// <summary>
    /// Plays the specified sound for the player.
    /// </summary>
    /// <returns></returns>
    public static player PlaySound(this player whichPlayer, sound sound)
    {
      if (GetLocalPlayer() == whichPlayer)
        StartSound(sound);
        
      return whichPlayer;
    }

    /// <summary>
    /// Plays the specified music for the player.
    /// </summary>
    public static player PlayMusicThematic(this player whichPlayer, string musicPath)
    {
      if (GetLocalPlayer() == whichPlayer)
        PlayThematicMusic(musicPath);
      
      return whichPlayer;
    }
  }
}