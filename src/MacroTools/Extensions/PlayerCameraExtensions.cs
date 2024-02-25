using static War3Api.Common;
using WCSharp.Shared.Data;

namespace MacroTools.Extensions
{
  public static class PlayerCameraExtensions
  {
    /// <summary>
    /// Applies a particular camera field to the player's view.
    /// </summary>
    public static player ApplyCameraField(this player whichPlayer, camerafield whichField, float value, float duration)
    {
      if (GetLocalPlayer() == whichPlayer) 
        SetCameraField(whichField, value, duration);

      return whichPlayer;
    }

    public static player RepositionCamera(this player whichPlayer, float x, float y)
    {
      if (GetLocalPlayer() == whichPlayer)
        SetCameraPosition(x, y);

      return whichPlayer;
    }

    public static player RepositionCamera(this player whichPlayer, Point position)
    {
      if (GetLocalPlayer() == whichPlayer)
        SetCameraPosition(position.X, position.Y);

      return whichPlayer;
    }
  }
}