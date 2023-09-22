using System;
using System.Collections.Generic;
using MacroTools.ControlPointSystem;
using MacroTools.DialogueSystem;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace MacroTools.Extensions
{
  /// <summary>
  /// A set of helpful extension methods for dealing with Warcraft 3's native player class.
  /// </summary>
  public static class PlayerExtensions
  {
    /// <summary>
    /// Queues the <see cref="IHasPlayableDialogue"/> for this player, playing it the next time there is nothing else playing.
    /// </summary>
    public static void QueueDialogue(this player whichPlayer, IHasPlayableDialogue dialogue) =>
      PlayerData.ByHandle(whichPlayer).QueueDialogue(dialogue);

    /// <summary>
    /// Set the number of extra <see cref="ControlPoint.ControlLevel"/>s the player gets each turn.
    /// </summary>
    /// <returns></returns>
    public static player SetControlLevelPerTurnBonus(this player whichPlayer, float value)
    {
      PlayerData.ByHandle(whichPlayer).ControlLevelPerTurnBonus = value;
      return whichPlayer;
    }

    /// <summary>
    /// Returns the number of extra <see cref="ControlPoint.ControlLevel"/>s the player gets each turn.
    /// </summary>
    public static float GetControlLevelPerTurnBonus(this player whichPlayer) =>
      PlayerData.ByHandle(whichPlayer).ControlLevelPerTurnBonus;

    /// <summary>
    /// Pings the minimap for the player.
    /// </summary>
    /// <param name="whichPlayer">Who to display the ping to.</param>
    /// <param name="position">Where to ping.</param>
    /// <param name="duration">How long the ping should last.</param>
    /// <returns>The same player that was passed in.</returns>
    public static player PingLocation(this player whichPlayer, Point position, float duration)
    {
      if (GetLocalPlayer() == whichPlayer)
        PingMinimap(position.X, position.Y, duration);
      return whichPlayer;
    }

    /// <summary>
    /// Replaces the minimap background texture with the specified one.
    /// </summary>
    public static void ChangeMinimapTerrainTexture(this player whichPlayer, string texturePath)
    {
      if (GetLocalPlayer() == whichPlayer)
        BlzChangeMinimapTerrainTex(texturePath);
    }

    /// <summary>
    /// Applies a particular camera field to the player's view.
    /// </summary>
    public static void ApplyCameraField(this player whichPlayer, camerafield whichField, float value, float duration)
    {
      if (GetLocalPlayer() != whichPlayer)
        return;
      SetCameraField(whichField, value, duration);
    }

    /// <summary>
    ///   Changes the player's view to come out of a certain camera.
    /// </summary>
    public static void SetupCamera(this player whichPlayer, camerasetup whichSetup, bool doPan, float duration)
    {
      if (GetLocalPlayer() != whichPlayer)
        return;
      CameraSetupApplyForceDuration(whichSetup, doPan, duration);
    }

    /// <summary>
    ///   Prevents the player from moving their camera out of the provided area.
    /// </summary>
    public static void SetCameraLimits(this player whichPlayer, rect rect)
    {
      if (GetLocalPlayer() != whichPlayer) return;
      var minX = GetRectMinX(rect);
      var minY = GetRectMinY(rect);
      var maxX = GetRectMaxX(rect);
      var maxY = GetRectMaxY(rect);
      SetCameraBounds(minX, minY, minX, maxY, maxX, maxY, maxX, minY);
    }

    /// <summary>
    /// Sets a player's alliance state towards another player.
    /// </summary>
    public static void SetAllianceState(this player sourcePlayer, player otherPlayer, AllianceState allianceState)
    {
      // Prevent players from attempting to ally with themselves.
      if (sourcePlayer == otherPlayer) return;

      switch (allianceState)
      {
        case AllianceState.Unallied:
          sourcePlayer.SetPlayerAllianceStateAlly(otherPlayer, false);
          sourcePlayer.SetPlayerAllianceStateVision(otherPlayer, false);
          sourcePlayer.SetPlayerAllianceStateControl(otherPlayer, false);
          sourcePlayer.SetPlayerAllianceStateFullControl(otherPlayer, false);
          break;
        case AllianceState.AlliedVision:
          sourcePlayer.SetPlayerAllianceStateAlly(otherPlayer, true);
          sourcePlayer.SetPlayerAllianceStateVision(otherPlayer, true);
          sourcePlayer.SetPlayerAllianceStateControl(otherPlayer, false);
          sourcePlayer.SetPlayerAllianceStateFullControl(otherPlayer, false);
          break;
        case AllianceState.AlliedAdvUnits:
          sourcePlayer.SetPlayerAllianceStateAlly(otherPlayer, true);
          sourcePlayer.SetPlayerAllianceStateVision(otherPlayer, true);
          sourcePlayer.SetPlayerAllianceStateControl(otherPlayer, true);
          sourcePlayer.SetPlayerAllianceStateFullControl(otherPlayer, true);
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(allianceState), allianceState, null);
      }
    }

    /// <summary>
    /// Increases or decreases a specific player state for the player.
    /// </summary>
    public static void AdjustPlayerState(this player player, playerstate playerState, int value) =>
      SetPlayerState(player, playerState, GetPlayerState(player, playerState) + value);

    /// <summary>
    /// Returns the maximum number of units the player can build of the given type, or the maximum research level
    /// the player can achieve for the given type.
    /// </summary>
    /// <param name="player">The player in question.</param>
    /// <param name="objectId">The unit type ID or research ID we want to know about.</param>
    public static int GetObjectLimit(this player player, int objectId) =>
      PlayerData.ByHandle(player).GetObjectLimit(objectId);

    /// <summary>
    /// Returns the number of times a player has researched the specified object.
    /// </summary>
    public static int GetObjectLevel(this player player, int objectId) =>
      PlayerData.ByHandle(player).GetObjectLevel(objectId);

    /// <summary>
    /// Returns the number of <see cref="ControlPoint"/>s a player controls.
    /// </summary>
    public static int GetControlPointCount(this player player) => PlayerData.ByHandle(player).ControlPoints.Count;

    /// <summary>
    /// Returns the number of <see cref="ControlPoint"/>s a player controls.
    /// </summary>
    public static List<ControlPoint> GetControlPoints(this player player) => PlayerData.ByHandle(player).ControlPoints;

    /// <summary>
    /// Adds an amount of gold to a player.
    /// </summary>
    public static void AddGold(this player player, float gold) => PlayerData.ByHandle(player).AddGold(gold);

    /// <summary>
    /// Adds an amount of lumber to a player.
    /// </summary>
    public static void AddLumber(this player player, float lumber) => PlayerData.ByHandle(player).AddLumber(lumber);

    /// <summary>
    /// Returns the player's <see cref="Team"/>.
    /// </summary>
    public static Team? GetTeam(this player player) => PlayerData.ByHandle(player).Team;

    /// <summary>
    /// Sets the player's <see cref="Team"/>.
    /// </summary>
    public static void SetTeam(this player player, Team whichTeam) => PlayerData.ByHandle(player).Team = whichTeam;

    /// <summary>
    /// Returns the player's <see cref="Faction"/>.
    /// </summary>
    public static Faction? GetFaction(this player player) => PlayerData.ByHandle(player).Faction;

    /// <summary>
    /// Sets the player's <see cref="Faction"/>.
    /// </summary>
    public static void SetFaction(this player player, Faction faction) => PlayerData.ByHandle(player).Faction = faction;

    /// <summary>
    /// Returns the player's gold income, including any bonuses.
    /// </summary>
    public static float GetTotalIncome(this player player) => PlayerData.ByHandle(player).TotalIncome;

    /// <summary>
    /// Returns the player's bonus gold income.
    /// </summary>
    public static float GetBonusIncome(this player player) => PlayerData.ByHandle(player).BonusIncome;

    /// <summary>
    /// Returns the player's gold income, without any bonuses.
    /// </summary>
    public static float GetBaseIncome(this player player) => PlayerData.ByHandle(player).BaseIncome;

    /// <summary>
    /// Returns the player's lumber income.
    /// </summary>
    public static float GetLumberIncome(this player player) => PlayerData.ByHandle(player).LumberIncome;

    /// <summary>
    /// Modifies the player's bonus income.
    /// </summary>
    public static void AddBonusIncome(this player player, float value) =>
      PlayerData.ByHandle(player).BonusIncome += value;

    /// <summary>
    /// Modifies the player's lumber income.
    /// </summary>
    public static void AddLumberIncome(this player player, float value)
    {
      PlayerData.ByHandle(player).LumberIncome += value;
    }

    /// <summary>
    /// Rescues all <paramref name="units"/> for <paramref name="newOwningPlayer"/>.
    /// </summary>
    /// <param name="newOwningPlayer">The player who should own the units after being rescued</param>
    /// <param name="units">The units to rescue.</param>
    public static void RescueGroup(this player? newOwningPlayer, List<unit> units)
    {
      newOwningPlayer ??= Player(PLAYER_NEUTRAL_AGGRESSIVE);

      foreach (var unit in units)
      
        unit.Rescue(newOwningPlayer);
    }

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

    /// <summary>
    ///   Determines whether or not the player can see or use the specified ability.
    /// </summary>
    internal static void SetAbilityAvailability(this player player, int ability, bool value) =>
      SetPlayerAbilityAvailable(player, ability, value);

    internal static void SetObjectLevel(this player player, int objectId, int level) =>
      PlayerData.ByHandle(player).SetObjectLevel(objectId, level);

    internal static void ModObjectLimit(this player player, int objectId, int limit) =>
      PlayerData.ByHandle(player).ModObjectLimit(objectId, limit);

    internal static void SetObjectLimit(this player player, int objectId, int limit) =>
      PlayerData.ByHandle(player).SetObjectLimit(objectId, limit);

    internal static void SetColor(this player whichPlayer, playercolor color, bool changeExisting)
    {
      SetPlayerColor(whichPlayer, color);
      if (!changeExisting) return;
      foreach (var unit in CreateGroup().EnumUnitsOfPlayer(whichPlayer).EmptyToList())
        SetUnitColor(unit, color);
    }

    internal static PlayerData GetPlayerData(this player player) =>
      PlayerData.ByHandle(player);

    private static void SetPlayerAllianceStateAlly(this player sourcePlayer, player otherPlayer, bool flag)
    {
      SetPlayerAlliance(sourcePlayer, otherPlayer, ALLIANCE_PASSIVE, flag);
      SetPlayerAlliance(sourcePlayer, otherPlayer, ALLIANCE_HELP_REQUEST, flag);
      SetPlayerAlliance(sourcePlayer, otherPlayer, ALLIANCE_HELP_RESPONSE, flag);
      SetPlayerAlliance(sourcePlayer, otherPlayer, ALLIANCE_SHARED_XP, flag);
      SetPlayerAlliance(sourcePlayer, otherPlayer, ALLIANCE_SHARED_SPELLS, flag);
    }

    private static void SetPlayerAllianceStateVision(this player sourcePlayer, player otherPlayer, bool flag) =>
      SetPlayerAlliance(sourcePlayer, otherPlayer, ALLIANCE_SHARED_VISION, flag);

    private static void SetPlayerAllianceStateControl(this player sourcePlayer, player otherPlayer, bool flag) =>
      SetPlayerAlliance(sourcePlayer, otherPlayer, ALLIANCE_SHARED_CONTROL, flag);

    private static void SetPlayerAllianceStateFullControl(this player sourcePlayer, player otherPlayer, bool flag) =>
      SetPlayerAlliance(sourcePlayer, otherPlayer, ALLIANCE_SHARED_ADVANCED_CONTROL, flag);

    /// <summary>
    /// Displays a nicely formatted hint to the player.
    /// </summary>
    public static void DisplayHint(this player whichPlayer, string msg)
    {
      DisplayTextToPlayer(whichPlayer, 0, 0, $"\n|cff00ff00HINT|r - {msg}");
      if (GetLocalPlayer() == whichPlayer)
        StartSound(SoundLibrary.Hint);
    }

    /// <summary>
    /// Displays a nicely formatted hint to the player that one of their heroes has gained some stats.
    /// </summary>
    public static void DisplayHeroReward(this unit whichUnit, int strength, int agility, int intelligence,
      int experience)
    {
      var display = $"\n|cff00ff00HERO REWARD EARNED -{GetHeroProperName(whichUnit)}|r";
      if (strength > 0)
        display = $"{display}\n+{strength} Strength";
      if (agility > 0)
        display = $"{display}\n+{agility} Agility";
      if (intelligence > 0)
        display = $"{display}\n+{intelligence} Intelligence";
      if (experience > 0)
        display = $"{display}\n+{experience} Experience";
      DisplayTextToPlayer(GetOwningPlayer(whichUnit), 0, 0, display);
      if (GetLocalPlayer() == GetOwningPlayer(whichUnit))
      {
        StartSound(SoundLibrary.Hint);
      }
    }

    /// <summary>
    /// Displays a nicely formatted hint to the player that one of their unit limits has increased.
    /// </summary>
    public static void DisplayUnitLimit(this Faction whichFaction, int unitTypeId)
    {
      DisplayTextToPlayer(whichFaction.Player, 0, 0,
        $"\n|cff00ff00UNIT LIMIT CHANGED - {GetObjectName(unitTypeId)}|r\nYou can now train up to {whichFaction.GetObjectLimit(unitTypeId)} {GetObjectName(unitTypeId)}s.");
      if (GetLocalPlayer() == whichFaction.Player)
        StartSound(SoundLibrary.Hint);
    }

    /// <summary>
    /// Displays a nicely formatted hint to the player that they have acquired a research.
    /// </summary>
    public static void DisplayResearchAcquired(this player whichPlayer, int researchId, int researchLevel)
    {
      DisplayTextToPlayer(whichPlayer, 0, 0,
        $"\n|cff00ff00RESEARCH ACQUIRED - {GetObjectName(researchId)}|r\n{BlzGetAbilityExtendedTooltip(researchId, researchLevel)}");
      if (GetLocalPlayer() == whichPlayer)
        StartSound(SoundLibrary.Hint);
    }

    /// <summary>
    /// Displays a nicely formatted hint to the player that they have unlocked a new unit type.
    /// </summary>
    public static void DisplayUnitTypeAcquired(this player whichPlayer, int unitId, string flavor)
    {
      DisplayTextToPlayer(whichPlayer, 0, 0,
        $"\n|cff00ff00NEW UNIT ACQUIRED - {GetObjectName(unitId)}\n|r{flavor}");
      if (GetLocalPlayer() == whichPlayer)
        StartSound(SoundLibrary.Hint);
    }

    /// <summary>
    /// Alerts the player that one of their researches has been refunded.
    /// </summary>
    public static void DisplayRefundedResearch(this player whichPlayer, int researchTypeId)
    {
      DisplayTextToPlayer(whichPlayer, 0, 0,
        $"\n|cff008000REFUND|r - You cannot research {GetObjectName(researchTypeId)}. All resources spent on it have been refunded.");
    }

    /// <summary>
    /// Indicates to the player that they have acquired a new <see cref="Power"/>.
    /// </summary>
    public static void DisplayPowerAcquired(this player whichPlayer, Power power)
    {
      DisplayTextToPlayer(whichPlayer, 0, 0,
        $"\n|cff00ff00NEW POWER ACQUIRED - {power.Name}\n|r{power.Description}");
      if (GetLocalPlayer() == whichPlayer)
        StartSound(SoundLibrary.Hint);
    }

    /// <summary>
    /// Indicates to the player that a <see cref="LegendaryHero"/> has been summoned.
    /// </summary>
    public static void DisplayLegendaryHeroSummoned(this player whichPlayer, LegendaryHero legendaryHero,
      string message)
    {
      DisplayTextToPlayer(whichPlayer, 0, 0,
        $"\n|cffd45e19LEGENDARY FOE SUMMONED - {legendaryHero.Name}\n|r{message}");
      StartSound(SoundLibrary.Warning);
    }

    /// <summary>
    /// Checks whether or not the given player has the specified alliance towards another player.
    /// </summary>
    public static bool GetAllianceState(this player whichPlayer, player otherPlayer, alliancetype allianceType) =>
      GetPlayerAlliance(whichPlayer, otherPlayer, allianceType);
  }
}