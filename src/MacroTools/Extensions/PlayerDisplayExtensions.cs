using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.Sound;
using static War3Api.Common;

namespace MacroTools.Extensions
{
  public static class PlayerDisplayExtensions
  {
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
        StartSound(SoundLibrary.Hint);
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
    public static player DisplayPowerAcquired(this player whichPlayer, Power power)
    {
      DisplayTextToPlayer(whichPlayer, 0, 0,
        $"\n|cff00ff00NEW POWER ACQUIRED - {power.Name}\n|r{power.Description}");
      if (GetLocalPlayer() == whichPlayer)
        StartSound(SoundLibrary.Hint);

      return whichPlayer;
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
    /// Pings the minimap using some sensible defaults.
    /// </summary>
    public static player PingMinimapSimple(this player whichPlayer, float x, float y, float duration, int red = 255,
      int green = 255, int blue = 255, bool extraEffects = false)
    {
      if (GetLocalPlayer() == whichPlayer)
        PingMinimapEx(x, y, duration, red, green, blue, extraEffects);

      return whichPlayer;
    }
  }
}