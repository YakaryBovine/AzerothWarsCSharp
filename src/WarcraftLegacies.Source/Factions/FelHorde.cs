using System.Collections.Generic;
using MacroTools;
using MacroTools.FactionSystem;
using WarcraftLegacies.Shared.Config.Factions;
using WarcraftLegacies.Source.FactionMechanics.Fel_Horde;
using WarcraftLegacies.Source.Quests.Fel_Horde;
using WarcraftLegacies.Source.Setup;
using WCSharp.Shared.Data;

namespace WarcraftLegacies.Source.Factions
{
  public sealed class FelHorde : Faction
  {
    private readonly PreplacedUnitSystem _preplacedUnitSystem;
    private readonly AllLegendSetup _allLegendSetup;

    /// <inheritdoc />
    
    public FelHorde(PreplacedUnitSystem preplacedUnitSystem, AllLegendSetup allLegendSetup) : base("Fel Horde",
      PLAYER_COLOR_GREEN, "|c0020c000", @"ReplaceableTextures\CommandButtons\BTNPitLord.blp")
    {
      TraditionalTeam = TeamSetup.Outland;
      _preplacedUnitSystem = preplacedUnitSystem;
      _allLegendSetup = allLegendSetup;
      UndefeatedResearch = FourCC("R05L");
      StartingGold = 200;
      CinematicMusic = "Doom";
      ControlPointDefenderUnitTypeId = UNIT_N0AA_CONTROL_POINT_DEFENDER_FEL_HORDE;
      IntroText = @"You are playing as the bloodthirsty Fel Horde.

You begin in Nagrand, cut off from your forces in Hellfire Citadel. You must raise an army and quickly conquer Outland.

Once Outland is under your control, gather your hordes and prepare to invade Azeroth through the Dark Portal.

The Alliance is gathering outside the Dark Portal to stop you, so prepare for a very hard breakout.";
      FoodMaximum = 250;
      GoldMines = new List<unit>
      {
        _preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(-2735, -30242))
      };
      Nicknames = new List<string>
      {
        "fh",
        "fel"
      };
    }

    /// <inheritdoc />
    public override void OnRegistered()
    {
      RegisterObjectLimits();
      RegisterQuests();
      JuggernautDeath.Setup(_preplacedUnitSystem);
      SharedFactionConfigSetup.AddSharedFactionConfig(this);
    }

    private void RegisterObjectLimits()
    {
      foreach (var (objectTypeId, objectLimit) in FelHordeObjectLimitData.GetAllObjectLimits())
        ModObjectLimit(FourCC(objectTypeId), objectLimit);

      ModAbilityAvailability(ABILITY_A0MZ_DEMONIC_CONSTRUCTION_TEAL_DEMOLISHERS, -1);
      ModAbilityAvailability(ABILITY_A0GM_FOR_THE_HORDE_PINK_GREY_MAIN_BUILDINGS, -1);
    }

    private void RegisterQuests()
    {
      var questHellfireCitadel = AddQuest(new QuestHellfireCitadel(Regions.HellfireUnlock));
      AddQuest(new QuestRuinsofShadowmoon(Regions.ShadowmoonBaseUnlock));
      AddQuest(new QuestBlackrock(Regions.BlackrockUnlock, Regions.DarkPortalUnlock, new[] { questHellfireCitadel }));
      AddQuest(new QuestFelHordeKillIronforge(_allLegendSetup.Ironforge.GreatForge));
      AddQuest(new QuestFelHordeKillStormwind(_allLegendSetup.Stormwind.StormwindKeep));
      AddQuest(new QuestGuldansLegacy());
      AddQuest(new QuestDarkPortal(
        _preplacedUnitSystem.GetUnit(UNIT_N036_DARK_PORTAL_WAYGATE, Regions.Dark_Portal_Entrance_1.Center),
        _preplacedUnitSystem.GetUnit(UNIT_N036_DARK_PORTAL_WAYGATE, Regions.Dark_Portal_Entrance_2.Center),
        _preplacedUnitSystem.GetUnit(UNIT_N036_DARK_PORTAL_WAYGATE, Regions.Dark_Portal_Entrance_3.Center),
        _preplacedUnitSystem.GetUnit(UNIT_N036_DARK_PORTAL_WAYGATE, Regions.Dark_Portal_Exit_1.Center),
        _preplacedUnitSystem.GetUnit(UNIT_N036_DARK_PORTAL_WAYGATE, Regions.Dark_Portal_Exit_2.Center),
        _preplacedUnitSystem.GetUnit(UNIT_N036_DARK_PORTAL_WAYGATE, Regions.Dark_Portal_Exit_3.Center)));
    }
  }
}