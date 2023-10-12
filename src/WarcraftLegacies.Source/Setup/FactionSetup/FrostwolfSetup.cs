using MacroTools;
using MacroTools.FactionSystem;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Setup.FactionSetup
{
  public static class FrostwolfSetup
  {
    public static Faction? Frostwolf { get; private set; }
    
    public static void Setup(PreplacedUnitSystem preplacedUnitSystem)
    {
      Frostwolf = new Faction("Frostwolf", PLAYER_COLOR_RED, "|c00ff0303",
        @"ReplaceableTextures\CommandButtons\BTNThrall.blp")
      {
        UndefeatedResearch = Constants.UPGRADE_R05V_FROSTWOLF_EXISTS,
        StartingGold = 200,
        StartingLumber = 700,
        CinematicMusic = "SadMystery",
        ControlPointDefenderUnitTypeId = Constants.UNIT_N0B6_CONTROL_POINT_DEFENDER_FROSTWOLF,
        IntroText = @"You are playing as the honorable |cffff0000Frostwolf Clan|r.

You begin in the Salt Flats, separated from your ally, the Warsong Clan in the North.

Salvage the wrecked ships, establish a base and gather your troops to move inland and assist your ally against the Night Elf threat."

      };

      Frostwolf.ModObjectLimit(Constants.UNIT_OGRE_GREAT_HALL_FROSTWOLF_T1, Faction.UNLIMITED);
      Frostwolf.ModObjectLimit(Constants.UNIT_OSTR_STRONGHOLD_FROSTWOLF_T2, Faction.UNLIMITED);
      Frostwolf.ModObjectLimit(Constants.UNIT_OFRT_FORTRESS_FROSTWOLF_T3, Faction.UNLIMITED);
      Frostwolf.ModObjectLimit(Constants.UNIT_OALT_ALTAR_OF_STORMS_FROSTWOLF_ALTAR, Faction.UNLIMITED);
      Frostwolf.ModObjectLimit(Constants.UNIT_OBAR_WAR_CAMP_FROSTWOLF_BARRACKS, Faction.UNLIMITED);
      Frostwolf.ModObjectLimit(Constants.UNIT_OFOR_WAR_MILL_FROSTWOLF_RESEARCH, Faction.UNLIMITED);
      Frostwolf.ModObjectLimit(Constants.UNIT_OTTO_TAUREN_TOTEM_FROSTWOLF_SPECIALIST, Faction.UNLIMITED);
      Frostwolf.ModObjectLimit(Constants.UNIT_OSLD_SPIRIT_LODGE_FROSTWOLF_MAGIC, Faction.UNLIMITED);
      Frostwolf.ModObjectLimit(Constants.UNIT_OTRB_BURROW_FROSTWOLF_FARM, Faction.UNLIMITED);
      Frostwolf.ModObjectLimit(Constants.UNIT_OWTW_WATCH_TOWER_FROSTWOLF_TOWER, Faction.UNLIMITED);
      Frostwolf.ModObjectLimit(Constants.UNIT_O002_IMPROVED_WATCH_TOWER_FROSTWOLF_TOWER_2, Faction.UNLIMITED);
      Frostwolf.ModObjectLimit(Constants.UNIT_OVLN_VOODOO_LOUNGE_FROSTWOLF_SHOP, Faction.UNLIMITED);
      Frostwolf.ModObjectLimit(Constants.UNIT_OSHY_HORDE_PIER_FROSTWOLF_SHIPYARD, Faction.UNLIMITED);
      Frostwolf.ModObjectLimit(Constants.UNIT_OOSC_PACK_KODO_FROSTWOLF, Faction.UNLIMITED);
      Frostwolf.ModObjectLimit(Constants.UNIT_OBEA_BEASTIARY_FROSTWOLF_SPECIALIST, Faction.UNLIMITED);

      Frostwolf.ModObjectLimit(FourCC("opeo"), Faction.UNLIMITED); //Peon
      Frostwolf.ModObjectLimit(FourCC("ogru"), Faction.UNLIMITED); //Grunt
      Frostwolf.ModObjectLimit(FourCC("otau"), Faction.UNLIMITED); //Tauren
      Frostwolf.ModObjectLimit(FourCC("ohun"), Faction.UNLIMITED); //Troll Headhunter
      Frostwolf.ModObjectLimit(FourCC("ocat"), 6); //Catapult
      Frostwolf.ModObjectLimit(FourCC("otbr"), 12); //Troll Batrider
      Frostwolf.ModObjectLimit(FourCC("odoc"), Faction.UNLIMITED); //Troll Witch Doctor
      Frostwolf.ModObjectLimit(FourCC("oshm"), Faction.UNLIMITED); //Shaman
      Frostwolf.ModObjectLimit(FourCC("ospw"), Faction.UNLIMITED); //Spirit Walker
      Frostwolf.ModObjectLimit(FourCC("o00A"), 6); //Far Seer

      Frostwolf.ModObjectLimit(Constants.UNIT_O06T_TAUREN_GLADIATOR_FROSTWOLF, 6);
      Frostwolf.ModObjectLimit(Constants.UNIT_H0CN_PACKLEADER_FROSTWOLF, 4);
      Frostwolf.ModObjectLimit(Constants.UNIT_H0CO_MAMMOTH_WRANGLER_FROSTWOLF, 2);
      Frostwolf.ModObjectLimit(Constants.UNIT_N049_WANDERER_FROSTWOLF, 4);

      //Ship
      Frostwolf.ModObjectLimit(FourCC("obot"), Faction.UNLIMITED); //Transport Ship
      Frostwolf.ModObjectLimit(FourCC("h0AS"), Faction.UNLIMITED); //Scout
      Frostwolf.ModObjectLimit(FourCC("h0AP"), Faction.UNLIMITED); //Frigate
      Frostwolf.ModObjectLimit(FourCC("h0B2"), Faction.UNLIMITED); //Fireship
      Frostwolf.ModObjectLimit(FourCC("h0AY"), Faction.UNLIMITED); //Galley
      Frostwolf.ModObjectLimit(FourCC("h0B5"), Faction.UNLIMITED); //Boarding
      Frostwolf.ModObjectLimit(FourCC("h0BC"), Faction.UNLIMITED); //Juggernaut
      Frostwolf.ModObjectLimit(FourCC("h0AO"), 6); //Bombard

      Frostwolf.ModObjectLimit(FourCC("h00C"), 1); //Drek)thar
      Frostwolf.ModObjectLimit(FourCC("Othr"), 1); //Thrall
      Frostwolf.ModObjectLimit(FourCC("Ocbh"), 1); //Cairne
      Frostwolf.ModObjectLimit(FourCC("Orkn"), 1); //Voljin
      Frostwolf.ModObjectLimit(FourCC("Orex"), 1); //Rexxar

      Frostwolf.ModObjectLimit(FourCC("Rows"), Faction.UNLIMITED); //Improved Pulverize
      Frostwolf.ModObjectLimit(FourCC("Rost"), Faction.UNLIMITED); //Shaman Adept Training
      Frostwolf.ModObjectLimit(FourCC("Rowd"), Faction.UNLIMITED); //Witch Doctor Adept Training
      Frostwolf.ModObjectLimit(FourCC("Rowt"), Faction.UNLIMITED); //Spirit Walker Adept Training
      Frostwolf.ModObjectLimit(FourCC("Rolf"), Faction.UNLIMITED); //Liquid Fire
      Frostwolf.ModObjectLimit(FourCC("Rosp"), Faction.UNLIMITED); //Spiked Barricades
      Frostwolf.ModObjectLimit(FourCC("Rorb"), Faction.UNLIMITED); //reinforced Defenses
      Frostwolf.ModObjectLimit(FourCC("R00H"), Faction.UNLIMITED); //Animal Companion
      Frostwolf.ModObjectLimit(FourCC("R00R"), Faction.UNLIMITED); //Improved Chain Lightning
      Frostwolf.ModObjectLimit(FourCC("R00W"), Faction.UNLIMITED); //Toughened Hides
      Frostwolf.ModObjectLimit(FourCC("R01Z"), Faction.UNLIMITED); //Battle Stations
      Frostwolf.SetObjectLevel(FourCC("R01Z"), 1); //Battle Stations
      Frostwolf.ModObjectLimit(Constants.UPGRADE_R023_BATRIDERS_FROSTWOLF, Faction.UNLIMITED);

      Frostwolf.ModAbilityAvailability(Constants.ABILITY_A0PF_FEL_ENERGY_TEAL_FORTRESSES, -1);
      Frostwolf.ModAbilityAvailability(Constants.ABILITY_ANTR_TROLL_REGENERATION_PINK_WITCH_DOCTOR_TROLL_HEADHUNTER_TROLL_BATRIDER_DARKSPEAR_WARLORD_TROLL_BERSERKER_ICON, -1);
      Frostwolf.ModAbilityAvailability(Constants.ABILITY_A0M4_BATTLE_STATIONS_PINK_GREY_ORC_BURROW_BLOODPACT, -1);
      Frostwolf.ModAbilityAvailability(Constants.ABILITY_ABTL_BATTLE_STATIONS_FROSTWOLF_WARSONG_BURROW, 1);

      
      Frostwolf.ModObjectLimit(Constants.UPGRADE_R09N_FLIGHT_PATH_WARSONG, 1);

      Frostwolf.AddGoldMine(preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(-8793, -11350)));
      Frostwolf.AddGoldMine(preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(-15828, -3120)));
      
      FactionManager.Register(Frostwolf);
    }
  }
}