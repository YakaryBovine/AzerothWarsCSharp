using MacroTools;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Setup.FactionSetup
{
  public static class LegionSetup
  {
    public static Faction? Legion { get; private set; }
    
    public static void Setup(PreplacedUnitSystem preplacedUnitSystem)
    {
      Legion = new Faction("Legion", PLAYER_COLOR_PEANUT, "|CFFBF8F4F",
        @"ReplaceableTextures\CommandButtons\BTNKiljaedin.blp")
      {
        UndefeatedResearch = FourCC("R04T"),
        StartingGold = 200,
        StartingLumber = 700,
        FoodMaximum = 250,
        CinematicMusic = "DarkAgents",
        ControlPointDefenderUnitTypeId = Constants.UNIT_U01U_CONTROL_POINT_DEFENDER_LEGION,
        StartingCameraPosition = Regions.LegionStartPos.Center,
        StartingUnits = Regions.LegionStartPos.PrepareUnitsForRescue(RescuePreparationMode.Invulnerable),
        IntroText = @"You are playing as the mighty |cffa2722dBurning Legion|r.

You begin isolated on Argus. Once the Planet is under control, you will unlock 2 teleporters to Northrend and Alterac.

On Azeroth, the Scourge will need your assistance to destroy the Kingdoms of Lordaeron, Dalaran and Quel'thalas.

Your primary objective is to summon the great host of the Burning Legion. Invade the city of Dalaran, where the book of Medivh is kept, and use it to open the Demon-gate to Argus."
      };
      
      //Structures
      Legion.ModObjectLimit(FourCC("u00H"), Faction.UNLIMITED); //Legion Defensive Pylon
      Legion.ModObjectLimit(FourCC("u00I"), Faction.UNLIMITED); //Improved Defensive Pylon
      Legion.ModObjectLimit(FourCC("u00F"), Faction.UNLIMITED); //Dormant Spire
      Legion.ModObjectLimit(FourCC("u00C"), Faction.UNLIMITED); //Legion Bastion
      Legion.ModObjectLimit(FourCC("u00N"), Faction.UNLIMITED); //Burning Citadel
      Legion.ModObjectLimit(FourCC("n040"), Faction.UNLIMITED); //Armory
      Legion.ModObjectLimit(FourCC("u009"), Faction.UNLIMITED); //Undead Shipyard
      Legion.ModObjectLimit(FourCC("u00E"), Faction.UNLIMITED); //Generator
      Legion.ModObjectLimit(FourCC("u01N"), Faction.UNLIMITED); //Burning Altar
      Legion.ModObjectLimit(FourCC("u015"), Faction.UNLIMITED); //Unholy Reliquary
      Legion.ModObjectLimit(FourCC("ndmg"), 6); //Demon Gate
      Legion.ModObjectLimit(FourCC("n04N"), Faction.UNLIMITED); //Infernal Machine Factory
      Legion.ModObjectLimit(FourCC("u006"), 2); //Summoning Circle
      Legion.ModObjectLimit(FourCC("n04Q"), 2); //Nether Pit
      Legion.ModObjectLimit(Constants.UNIT_U00F_DORMANT_SPIRE_LEGION_T1, Faction.UNLIMITED);
      Legion.ModObjectLimit(Constants.UNIT_U00C_LEGION_BASTION_LEGION_T2, Faction.UNLIMITED);
      Legion.ModObjectLimit(Constants.UNIT_U00N_BURNING_CITADEL_LEGION_T3, Faction.UNLIMITED);

      //Units
      Legion.ModObjectLimit(FourCC("u00D"), Faction.UNLIMITED); //Legion Herald
      Legion.ModObjectLimit(FourCC("u007"), 6); //Dreadlord
      Legion.ModObjectLimit(FourCC("n04P"), Faction.UNLIMITED); //Warlock
      Legion.ModObjectLimit(FourCC("ninc"), Faction.UNLIMITED); //Burning archer
      Legion.ModObjectLimit(FourCC("n04K"), Faction.UNLIMITED); //Succubus
      Legion.ModObjectLimit(FourCC("n04J"), Faction.UNLIMITED); //Felstalker
      Legion.ModObjectLimit(FourCC("n0DO"), 12); //Doom Guard
      Legion.ModObjectLimit(FourCC("n04O"), 6); //Doom lord
      Legion.ModObjectLimit(FourCC("n04L"), 6); //Infernal Juggernaut
      Legion.ModObjectLimit(FourCC("o04P"), 6); //Nathrezim
      Legion.ModObjectLimit(FourCC("ninf"), 6); //Infernal
      Legion.ModObjectLimit(FourCC("n04H"), Faction.UNLIMITED); //Fel Guard
      Legion.ModObjectLimit(FourCC("n04U"), 4); //Dragon
      Legion.ModObjectLimit(FourCC("n03L"), 4); //Barge

      //Ship
      Legion.ModObjectLimit(FourCC("ubot"), Faction.UNLIMITED); //Undead Transport Ship
      Legion.ModObjectLimit(FourCC("h0AT"), Faction.UNLIMITED); //Scout
      Legion.ModObjectLimit(FourCC("h0AW"), Faction.UNLIMITED); //Frigate
      Legion.ModObjectLimit(FourCC("h0AM"), Faction.UNLIMITED); //Fireship
      Legion.ModObjectLimit(FourCC("h0AZ"), Faction.UNLIMITED); //Galley
      Legion.ModObjectLimit(FourCC("h0AQ"), Faction.UNLIMITED); //Boarding
      Legion.ModObjectLimit(FourCC("h0BB"), Faction.UNLIMITED); //Juggernaut
      Legion.ModObjectLimit(FourCC("h0B9"), 6); //Bombard

      Legion.ModObjectLimit(FourCC("n05R"), 1); //Felguard
      Legion.ModObjectLimit(FourCC("n06H"), 1); //Pit Fiend
      Legion.ModObjectLimit(FourCC("n07B"), 1); //Queen
      Legion.ModObjectLimit(FourCC("n07D"), 1); //Maiden
      Legion.ModObjectLimit(FourCC("n07o"), 1); //Terror
      Legion.ModObjectLimit(FourCC("n07N"), 1); //Lord

      //Researches
      Legion.ModObjectLimit(FourCC("R02C"), Faction.UNLIMITED); //Acute Sensors
      Legion.ModObjectLimit(FourCC("R02A"), Faction.UNLIMITED); //Chaos Infusion
      Legion.ModObjectLimit(FourCC("R028"), Faction.UNLIMITED); //Shadow Priest Adept Training
      Legion.ModObjectLimit(FourCC("R042"), Faction.UNLIMITED); //Nathrezim Adept Training
      Legion.ModObjectLimit(FourCC("R027"), Faction.UNLIMITED); //Warlock Adept Training
      Legion.ModObjectLimit(FourCC("R01Y"), Faction.UNLIMITED); //Astral Walk
      Legion.ModObjectLimit(FourCC("R04G"), Faction.UNLIMITED); //Improved Carrion Swarm
      Legion.ModObjectLimit(FourCC("R03Z"), Faction.UNLIMITED); //War Plating
      Legion.ModObjectLimit(FourCC("R040"), Faction.UNLIMITED); //Flying horrors
      Legion.ModObjectLimit(Constants.UPGRADE_R096_REMATERIALIZATION_LEGION, 1);
      Legion.ModObjectLimit(Constants.UPGRADE_R04R_FORTIFIED_HULLS_UNIVERSAL_UPGRADE, 1 );
      Legion.ModObjectLimit(Constants.UPGRADE_R09X_IMPROVED_SHADOW_INFUSION_LEGION, 1);

      //Heroes
      Legion.ModObjectLimit(FourCC("U00L"), 1); //Anetheron
      Legion.ModObjectLimit(Constants.UNIT_UMAL_THE_CUNNING_LEGION, 1);
      Legion.ModObjectLimit(Constants.UNIT_UTIC_THE_DARKENER_LEGION, 1);

      Legion.AddGoldMine(preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(19331f, -30663)));
      
      FactionManager.Register(Legion);
    }
  }
}