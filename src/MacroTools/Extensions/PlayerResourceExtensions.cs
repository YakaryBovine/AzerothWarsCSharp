using static War3Api.Common;

namespace MacroTools.Extensions
{
  public static class PlayerResourceExtensions
  {
    /// <summary>
    /// Increases or decreases a specific player state for the player.
    /// </summary>
    public static void AdjustPlayerState(this player player, playerstate playerState, int value) =>
      SetPlayerState(player, playerState, GetPlayerState(player, playerState) + value);

    /// <summary>Adds an amount of gold to a player.</summary>
    public static void AddGold(this player player, float gold) => PlayerData.ByHandle(player).AddGold(gold);

    /// <summary>Adds an amount of lumber to a player.</summary>
    public static void AddLumber(this player player, float lumber) => PlayerData.ByHandle(player).AddLumber(lumber);

    /// <summary>Sets the player's gold to a specific value.</summary>
    public static void SetGold(this player player, float gold) => PlayerData.ByHandle(player).SetGold(gold);

    /// <summary>Sets the player's lumber to a specific value.</summary>
    public static void SetLumber(this player player, float lumber) => PlayerData.ByHandle(player).SetLumber(lumber);

    /// <summary>Returns the player's gold, including any partial gold.</summary>
    public static float GetGold(this player player) => PlayerData.ByHandle(player).GetGold();

    /// <summary>Returns the player's lumber, including any partial lumber.</summary>
    public static float GetLumber(this player player) => PlayerData.ByHandle(player).GetLumber();

    /// <summary>
    /// Returns the amount of food the player is using.
    /// </summary>
    public static int GetFoodUsed(this player whichPlayer) =>
      GetPlayerState(whichPlayer, PLAYER_STATE_RESOURCE_FOOD_USED);

    /// <summary>
    /// Returns the player's food cap.
    /// </summary>
    public static int GetFoodCap(this player whichPlayer) =>
      GetPlayerState(whichPlayer, PLAYER_STATE_RESOURCE_FOOD_CAP);

    /// <summary>
    /// Returns player's food cap ceiling.
    /// </summary>
    public static int GetFoodCapCeiling(this player whichPlayer) =>
      GetPlayerState(whichPlayer, PLAYER_STATE_FOOD_CAP_CEILING);
    
    /// <summary>Removes all of the player's resources.</summary>
    public static player RemoveAllResources(this player whichPlayer)
    {
      whichPlayer.SetGold(0);
      whichPlayer.SetLumber(0);
      return whichPlayer;
    }
  }
}