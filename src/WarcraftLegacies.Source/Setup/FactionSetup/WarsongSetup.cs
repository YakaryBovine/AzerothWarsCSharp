using MacroTools;
using MacroTools.FactionSystem;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Setup.FactionSetup
{
  public static class WarsongSetup
  {
    public static Faction? WarsongClan { get; private set; }

    public static void Setup(PreplacedUnitSystem preplacedUnitSystem)
    {
      WarsongClan = new Faction("Warsong", PLAYER_COLOR_ORANGE, "|c00ff8000",
        @"ReplaceableTextures\CommandButtons\BTNHellScream.blp")
      {
        UndefeatedResearch = FourCC("R05W"),
        StartingGold = 200,
        StartingLumber = 700,
        CinematicMusic = "DarkAgents",
        ControlPointDefenderUnitTypeId = Constants.UNIT_N0D6_CONTROL_POINT_DEFENDER_WARSONG,
        IntroText = @"You are playing as the brutal |cffd45e19Warsong clan|r.

You begin in the eaves of Ashenvale, isolated from your ally, the Frostwolf Clan in the South. 

The Warchief expects a large amount of lumber from you. Begin by harvesting with your Peons, and expanding into the Barrens and Durotar.

The Night Elves are aware of your presence and are gathering a mighty host against you. Unlock Orgrimmar as soon as possible to defend against the Night Elf assault."
      };

      WarsongClan.ModObjectLimit(FourCC("o00C"), Faction.UNLIMITED); //Great Hall
      WarsongClan.ModObjectLimit(FourCC("o02R"), Faction.UNLIMITED); //Stronghold
      WarsongClan.ModObjectLimit(FourCC("o02S"), Faction.UNLIMITED); //Fortress
      WarsongClan.ModObjectLimit(FourCC("o020"), Faction.UNLIMITED); //Altar of Storms
      WarsongClan.ModObjectLimit(FourCC("o01S"), Faction.UNLIMITED); //Barracks
      WarsongClan.ModObjectLimit(FourCC("o009"), Faction.UNLIMITED); //War Mill
      WarsongClan.ModObjectLimit(FourCC("o006"), Faction.UNLIMITED); //Ogre Barrack
      WarsongClan.ModObjectLimit(FourCC("o05G"), Faction.UNLIMITED); //Siege Workshop
      WarsongClan.ModObjectLimit(FourCC("o02Q"), Faction.UNLIMITED); //Bestiary
      WarsongClan.ModObjectLimit(FourCC("o028"), Faction.UNLIMITED); //Orc Burrow
      WarsongClan.ModObjectLimit(FourCC("n03E"), Faction.UNLIMITED); //Watch Tower
      WarsongClan.ModObjectLimit(FourCC("o01H"), Faction.UNLIMITED); //Troll Shrine
      WarsongClan.ModObjectLimit(FourCC("n0AL"), Faction.UNLIMITED); //Improved Watch Tower
      WarsongClan.ModObjectLimit(FourCC("o02T"), Faction.UNLIMITED); //Shipyard
      WarsongClan.ModObjectLimit(FourCC("o01T"), Faction.UNLIMITED); //Goblin Hardware Shop

      WarsongClan.ModObjectLimit(FourCC("o04L"), Faction.UNLIMITED); //Peon
      WarsongClan.ModObjectLimit(FourCC("o02M"), Faction.UNLIMITED); //Grunt
      WarsongClan.ModObjectLimit(FourCC("orai"), Faction.UNLIMITED); //Raider
      WarsongClan.ModObjectLimit(FourCC("n08E"), Faction.UNLIMITED); //Hexbinder
      WarsongClan.ModObjectLimit(FourCC("otbk"), Faction.UNLIMITED); //Troll Berseker
      WarsongClan.ModObjectLimit(FourCC("nogn"), Faction.UNLIMITED); //Stonemaul Ogre Magi
      WarsongClan.ModObjectLimit(FourCC("o00I"), 6); //Horde War Machine
      WarsongClan.ModObjectLimit(FourCC("e01M"), 4); //Azerite Siege Engine
      WarsongClan.ModObjectLimit(FourCC("okod"), 4); //Kodo Beast
      WarsongClan.ModObjectLimit(FourCC("o00G"), 6); //Blademaster
      WarsongClan.ModObjectLimit(FourCC("n03F"), 6); //Korkron elite
      WarsongClan.ModObjectLimit(FourCC("owyv"), 8); //Wind Rider

      //Ship
      WarsongClan.ModObjectLimit(FourCC("obot"), Faction.UNLIMITED); //Transport Ship
      WarsongClan.ModObjectLimit(FourCC("h0AS"), Faction.UNLIMITED); //Scout
      WarsongClan.ModObjectLimit(FourCC("h0AP"), Faction.UNLIMITED); //Frigate
      WarsongClan.ModObjectLimit(FourCC("h0B2"), Faction.UNLIMITED); //Fireship
      WarsongClan.ModObjectLimit(FourCC("h0AY"), Faction.UNLIMITED); //Galley
      WarsongClan.ModObjectLimit(FourCC("h0B5"), Faction.UNLIMITED); //Boarding
      WarsongClan.ModObjectLimit(FourCC("h0BC"), Faction.UNLIMITED); //Juggernaut
      WarsongClan.ModObjectLimit(FourCC("h0AO"), 6); //Bombard

      WarsongClan.ModObjectLimit(FourCC("Ogrh"), 1); //Grom
      WarsongClan.ModObjectLimit(FourCC("Obla"), 1); //Varok
      WarsongClan.ModObjectLimit(FourCC("O06L"), 1); //Garrosh
      WarsongClan.ModObjectLimit(FourCC("n0CN"), 1); //Gibbs
      WarsongClan.ModObjectLimit(Constants.UNIT_OPGH_CORRUPTOR_OF_THE_WARSONG_CLAN_WARSONG_BLOODPACT, 1); //Blood Pact Grom   Fixes Perma Death Grom Blood Pact bug

      WarsongClan.ModObjectLimit(FourCC("Robs"), Faction.UNLIMITED); //Berserker Strength
      WarsongClan.ModObjectLimit(FourCC("Rotr"), Faction.UNLIMITED); //Troll Regeneration
      WarsongClan.ModObjectLimit(FourCC("R01J"), Faction.UNLIMITED); //Ensnare
      WarsongClan.ModObjectLimit(FourCC("R02I"), Faction.UNLIMITED); //Ogre Magi Adept Training
      WarsongClan.ModObjectLimit(FourCC("R03Q"), Faction.UNLIMITED); //Warlock Adept Training
      WarsongClan.ModObjectLimit(FourCC("Rorb"), Faction.UNLIMITED); //Reinforced Defenses
      WarsongClan.ModObjectLimit(FourCC("Rosp"), Faction.UNLIMITED); //Spiked Barricades
      WarsongClan.ModObjectLimit(FourCC("R016"), Faction.UNLIMITED); //Warlords
      WarsongClan.ModObjectLimit(FourCC("R019"), Faction.UNLIMITED); //Improved Shockwave
      WarsongClan.ModObjectLimit(Constants.UPGRADE_R01Z_BATTLE_STATIONS_ORC, Faction.UNLIMITED);
      WarsongClan.SetObjectLevel(Constants.UPGRADE_R01Z_BATTLE_STATIONS_ORC, 1);
      WarsongClan.ModObjectLimit(Constants.UPGRADE_R00D_FOR_THE_HORDE_FROSTWOLF, Faction.UNLIMITED);
      WarsongClan.ModObjectLimit(Constants.UPGRADE_ROVS_ENVENOMED_SPEARS_WARSONG, Faction.UNLIMITED);
      WarsongClan.ModObjectLimit(Constants.UPGRADE_R017_IMPROVED_IGNORE_PAIN_WARSONG, Faction.UNLIMITED);
      WarsongClan.ModObjectLimit(Constants.UPGRADE_R01H_WIND_RIDERS_WARSONG, Faction.UNLIMITED);


      WarsongClan.ModObjectLimit(Constants.UPGRADE_R09N_FLIGHT_PATH_WARSONG, 1);

      WarsongClan.ModAbilityAvailability(Constants.ABILITY_A0PF_FEL_ENERGY_TEAL_FORTRESSES, -1);
      WarsongClan.ModAbilityAvailability(Constants.ABILITY_ANTR_TROLL_REGENERATION_PINK_WITCH_DOCTOR_TROLL_HEADHUNTER_TROLL_BATRIDER_DARKSPEAR_WARLORD_TROLL_BERSERKER_ICON, -1);
      WarsongClan.ModAbilityAvailability(Constants.ABILITY_ABTL_BATTLE_STATIONS_FROSTWOLF_WARSONG_BURROW, 1);
      WarsongClan.ModAbilityAvailability(Constants.ABILITY_A0M4_BATTLE_STATIONS_PINK_GREY_ORC_BURROW_BLOODPACT, -1);

      WarsongClan.ModObjectLimit(Constants.UPGRADE_R09O_DRINK_THE_BLOOD_OF_MANNOROTH, 1);
      WarsongClan.ModObjectLimit(Constants.UPGRADE_R09P_REVERT_BLOODPACT, 1);

      WarsongClan.AddGoldMine(preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(-8455, -2777)));

      FactionManager.Register(WarsongClan);
    }
  }
}