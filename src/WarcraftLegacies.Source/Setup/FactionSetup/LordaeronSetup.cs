using MacroTools;
using MacroTools.FactionSystem;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Setup.FactionSetup
{
  public static class LordaeronSetup
  {
    public static Faction? Lordaeron { get; private set; }

    public static void Setup(PreplacedUnitSystem preplacedUnitSystem)
    {
      Lordaeron = new Faction("Lordaeron", PLAYER_COLOR_BLUE, "|c000042ff",
        @"ReplaceableTextures\CommandButtons\BTNArthas.blp")
      {
        StartingGold = 200,
        StartingLumber = 700,
        UndefeatedResearch = FourCC("R05M"),
        CinematicMusic = "Comradeship",
        ControlPointDefenderUnitTypeId = Constants.UNIT_H03W_CONTROL_POINT_DEFENDER_LORDAERON,
        IntroText = @"You are playing as the great |cff4242ebKingdom of Lordaeron|r.

You begin in Andorhal, isolated from your forces in the rest of the Kingdom, and the Plague of Undeath is coming.

Secure your major settlements by clearing out clusters of enemies and fortify your Kingdom as much as possible.

If you survive the Plague, sail to the frozen wasteland of Northrend and take the fight to the Lich King."
      };

      //Structures
      Lordaeron.ModObjectLimit(FourCC("htow"), Faction.UNLIMITED); //Town Hall
      Lordaeron.ModObjectLimit(FourCC("hkee"), Faction.UNLIMITED); //Keep
      Lordaeron.ModObjectLimit(FourCC("hcas"), Faction.UNLIMITED); //Castle
      Lordaeron.ModObjectLimit(FourCC("hhou"), Faction.UNLIMITED); //Farm
      Lordaeron.ModObjectLimit(FourCC("halt"), Faction.UNLIMITED); //Altar of Kings
      Lordaeron.ModObjectLimit(FourCC("hbar"), Faction.UNLIMITED); //Barracks
      Lordaeron.ModObjectLimit(FourCC("hbla"), Faction.UNLIMITED); //Blacksmith
      Lordaeron.ModObjectLimit(FourCC("h035"), Faction.UNLIMITED); //Chapel
      Lordaeron.ModObjectLimit(FourCC("hwtw"), Faction.UNLIMITED); //Scout Tower
      Lordaeron.ModObjectLimit(FourCC("hgtw"), Faction.UNLIMITED); //Guard Tower
      Lordaeron.ModObjectLimit(FourCC("h006"), Faction.UNLIMITED); //Guard Tower (Improved)
      Lordaeron.ModObjectLimit(FourCC("hctw"), Faction.UNLIMITED); //Cannon Tower
      Lordaeron.ModObjectLimit(FourCC("h007"), Faction.UNLIMITED); //Cannon Tower (Improved)
      Lordaeron.ModObjectLimit(FourCC("hshy"), Faction.UNLIMITED); //Alliance Shipyard
      Lordaeron.ModObjectLimit(FourCC("nmrk"), Faction.UNLIMITED); //Marketplace
      Lordaeron.ModObjectLimit(FourCC("h06C"), Faction.UNLIMITED); //Aviary
      Lordaeron.ModObjectLimit(FourCC("h094"), Faction.UNLIMITED); //Siege Camp

      //Units
      Lordaeron.ModObjectLimit(FourCC("hpea"), Faction.UNLIMITED); //Peasant
      Lordaeron.ModObjectLimit(FourCC("hfoo"), Faction.UNLIMITED); //Footman
      Lordaeron.ModObjectLimit(FourCC("hkni"), Faction.UNLIMITED); //Knight
      Lordaeron.ModObjectLimit(FourCC("nchp"), Faction.UNLIMITED); //Cleric
      Lordaeron.ModObjectLimit(FourCC("h00F"), 6); //Paladin
      Lordaeron.ModObjectLimit(FourCC("nwe2"), 6); //Thunder Eagle
      Lordaeron.ModObjectLimit(FourCC("h01C"), Faction.UNLIMITED); //Longbowman
      Lordaeron.ModObjectLimit(FourCC("n03K"), Faction.UNLIMITED); //Chaplain
      Lordaeron.ModObjectLimit(FourCC("hcth"), 12); //Silver Hand Squire
      Lordaeron.ModObjectLimit(FourCC("h02Q"), 6); //Pegasus Knight
      Lordaeron.ModObjectLimit(FourCC("e017"), 8); //Scorpion
      Lordaeron.ModObjectLimit(FourCC("o02F"), 6); //Mangonel
      Lordaeron.ModObjectLimit(FourCC("h09Y"), 2); //Throne Guard

      //Ships
      Lordaeron.ModObjectLimit(FourCC("hbot"), Faction.UNLIMITED); //Alliance Transport Ship
      Lordaeron.ModObjectLimit(FourCC("h0AR"), Faction.UNLIMITED); //Alliance Scout
      Lordaeron.ModObjectLimit(FourCC("h0AX"), Faction.UNLIMITED); //Alliance Frigate
      Lordaeron.ModObjectLimit(FourCC("h0B3"), Faction.UNLIMITED); //Alliance Fireship
      Lordaeron.ModObjectLimit(FourCC("h0B0"), Faction.UNLIMITED); //Alliance Galley
      Lordaeron.ModObjectLimit(FourCC("h0B6"), Faction.UNLIMITED); //Alliance Boarding
      Lordaeron.ModObjectLimit(FourCC("h0AN"), Faction.UNLIMITED); //Alliance Juggernaut
      Lordaeron.ModObjectLimit(FourCC("h0B7"), 6); //Alliance Bombard

      //Demis
      Lordaeron.ModObjectLimit(FourCC("h012"), 1); //Falric

      Lordaeron.ModObjectLimit(FourCC("Hart"), 1); //Arthas
      Lordaeron.ModObjectLimit(FourCC("Huth"), 1); //Uther
      Lordaeron.ModObjectLimit(FourCC("H01J"), 1); //Mograine
      Lordaeron.ModObjectLimit(FourCC("Hlgr"), 1); //Garithos

      Lordaeron.ModObjectLimit(FourCC("Harf"), 1); //Arthas

      //Upgrades
      Lordaeron.ModObjectLimit(Constants.UPGRADE_R02E_LIGHT_S_PRAISE_MASTER_TRAINING_ARATHOR_LORDAERON, Faction.UNLIMITED);
      Lordaeron.ModObjectLimit(Constants.UPGRADE_R00I_MAGE_MASTER_TRAINING_LORDAERON, Faction.UNLIMITED);
      Lordaeron.ModObjectLimit(Constants.UPGRADE_RHSE_MAGIC_SENTRY_PURPLE_GREEN_DARK_GREEN_RESEARCH, Faction.UNLIMITED);
      Lordaeron.ModObjectLimit(Constants.UPGRADE_RHAN_ANIMAL_WAR_TRAINING_DARK_GREEN_PURPLE_RESEARCH, Faction.UNLIMITED);
      Lordaeron.ModObjectLimit(Constants.UPGRADE_RHLH_IMPROVED_LUMBER_HARVESTING_ADVANCED_LUMBER_HARVESTING_YELLOW_PURPLE_ORANGE_GREEN_DARK_GREEN_RESEARCH, Faction.UNLIMITED);
      Lordaeron.ModObjectLimit(Constants.UPGRADE_RHAC_IMPROVED_MASONRY_ADVANCED_MASONRY_IMBUED_MASONRY_YELLOW_PURPLE_ORANGE_GREEN_DARK_GREEN_RESEARCH, Faction.UNLIMITED);
      Lordaeron.ModObjectLimit(Constants.UPGRADE_R04D_SEAL_OF_RIGHTEOUSNESS_LORDAERON, Faction.UNLIMITED);
      Lordaeron.ModObjectLimit(Constants.UPGRADE_R01P_ENSNARE_LORDAERON, Faction.UNLIMITED);
      Lordaeron.ModObjectLimit(Constants.UPGRADE_R06Q_PALADIN_MASTER_TRAINING_LORDAERON, Faction.UNLIMITED);
      Lordaeron.ModObjectLimit(Constants.UPGRADE_R04A_RAPID_FIRE_LORDAERON, Faction.UNLIMITED);
      Lordaeron.ModObjectLimit(Constants.UPGRADE_R00B_VETERAN_FOOTMEN_LORDAERON, Faction.UNLIMITED);
      Lordaeron.ModObjectLimit(Constants.UPGRADE_R01Q_PEGASUS_TAMING_LORDAERON, Faction.UNLIMITED);

      //Todo: these probably should be in some kind of ability library, not here
      Lordaeron.ModAbilityAvailability(Constants.ABILITY_A0N2_GRASPING_VINES_TREANTS, -1);
      Lordaeron.ModAbilityAvailability(Constants.ABILITY_A0GD_SUMMON_GARRISON_STORMWIND, -1);
      Lordaeron.ModAbilityAvailability(Constants.ABILITY_A0GC_REPLENISH_MANA_ORANGE_KEEPS_CAPITALS, -1);
      Lordaeron.ModAbilityAvailability(Constants.ABILITY_A0K5_DWARVEN_MASONRY_CASTLES_YELLOW, -1);
      
      Lordaeron.AddGoldMine(preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(13617, 8741)));

      FactionManager.Register(Lordaeron);
    }
  }
}