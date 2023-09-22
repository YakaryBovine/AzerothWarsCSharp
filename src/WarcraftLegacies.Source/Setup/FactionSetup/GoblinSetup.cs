using MacroTools;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.Powers;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Setup.FactionSetup
{
  /// <summary>
  /// Setup for the Goblin <see cref="Faction"/>
  /// </summary>
  public static class GoblinSetup
  {
    public static Faction Goblin { get; private set; }

    public static void Setup(PreplacedUnitSystem preplacedUnitSystem)
    {
      Goblin = new Faction("Bilgewater", PLAYER_COLOR_LIGHT_GRAY, "|cff808080",
        @"ReplaceableTextures\CommandButtons\BTNHeroTinker.blp")
      {
        StartingGold = 200,
        StartingLumber = 700,
        ControlPointDefenderUnitTypeId = Constants.UNIT_O01C_CONTROL_POINT_DEFENDER_GOBLIN,
        StartingUnits = Regions.GoblinStartPos.PrepareUnitsForRescue(RescuePreparationMode.Invulnerable),
        IntroText = @"You are playing as the industrious |cff808080Bilgewater Cartel|r.

You begin in Tanaris with a very small business venture. Expand onto Kalimdor to grow your trade empire.

Your advanced units require Oil to function. Use oil ships to find oil deposits in the Great Sea and build platforms on them.

The Trading Center in Kezan will unlock the ability to train Traders. Be sure to protect the Trading Center once you unlock it, as it will form the backbone of your Goblin Empire."
      };

      Goblin.ModObjectLimit(FourCC("o03L"), Faction.UNLIMITED); //Great Hall
      Goblin.ModObjectLimit(FourCC("o03M"), Faction.UNLIMITED); //Stronghold
      Goblin.ModObjectLimit(FourCC("o03N"), Faction.UNLIMITED); //Fortress
      Goblin.ModObjectLimit(FourCC("o03O"), Faction.UNLIMITED); //Altar of Storms
      Goblin.ModObjectLimit(FourCC("o03P"), Faction.UNLIMITED); //Barracks
      Goblin.ModObjectLimit(FourCC("o05T"), Faction.UNLIMITED); //Boot Camp
      Goblin.ModObjectLimit(FourCC("o03Q"), Faction.UNLIMITED); //War Mill
      Goblin.ModObjectLimit(FourCC("o03S"), Faction.UNLIMITED); //Tauren Totem
      Goblin.ModObjectLimit(FourCC("o01M"), Faction.UNLIMITED); //Spirit Lodge
      Goblin.ModObjectLimit(FourCC("o03T"), Faction.UNLIMITED); //Orc Burrow
      Goblin.ModObjectLimit(FourCC("o03U"), Faction.UNLIMITED); //Watch Tower
      Goblin.ModObjectLimit(FourCC("o03W"), Faction.UNLIMITED); //Improved Watch Tower
      Goblin.ModObjectLimit(FourCC("o03X"), Faction.UNLIMITED); //Voodoo Lounge
      Goblin.ModObjectLimit(FourCC("o03V"), Faction.UNLIMITED); //Shipyard
      Goblin.ModObjectLimit(FourCC("h011"), 1); //Artillery

      //Ship
      Goblin.ModObjectLimit(Constants.UNIT_O06G_OIL_RIG_CONSTRUCTOR_GOBLIN, Faction.UNLIMITED);
      Goblin.ModObjectLimit(FourCC("h0AS"), Faction.UNLIMITED); //Scout
      Goblin.ModObjectLimit(FourCC("h0AP"), Faction.UNLIMITED); //Frigate
      Goblin.ModObjectLimit(FourCC("h0B2"), Faction.UNLIMITED); //Fireship
      Goblin.ModObjectLimit(FourCC("h0AY"), Faction.UNLIMITED); //Galley
      Goblin.ModObjectLimit(FourCC("h0B5"), Faction.UNLIMITED); //Boarding
      Goblin.ModObjectLimit(FourCC("h0BC"), Faction.UNLIMITED); //Juggernaut
      Goblin.ModObjectLimit(FourCC("h0AO"), 6); //Bombard

      Goblin.ModObjectLimit(Constants.UNIT_O02I_BUILDER_GOBLIN_WORKER, Faction.UNLIMITED);
      Goblin.ModObjectLimit(Constants.UNIT_N099_OGRE_MERCENARY_GOBLIN, Faction.UNLIMITED);
      Goblin.ModObjectLimit(Constants.UNIT_H08X_SAPPERS_GOBLIN, 8);
      Goblin.ModObjectLimit(Constants.UNIT_H08Y_GUNNER_GOBLIN, Faction.UNLIMITED);
      Goblin.ModObjectLimit(Constants.UNIT_U02R_HOBGOBLIN_GOBLIN, Faction.UNLIMITED);
      Goblin.ModObjectLimit(Constants.UNIT_H09I_PERSONAL_TANK_GOBLIN, 12);
      Goblin.ModObjectLimit(Constants.UNIT_H09J_GRENADIER_GOBLIN, 12);
      Goblin.ModObjectLimit(Constants.UNIT_ODOC_WITCH_DOCTOR_FROSTWOLF, Faction.UNLIMITED);
      Goblin.ModObjectLimit(Constants.UNIT_O04O_ALCHEMIST_GOBLIN, Faction.UNLIMITED);
      Goblin.ModObjectLimit(Constants.UNIT_O04Q_TINKER_GOBLIN, 6);
      Goblin.ModObjectLimit(Constants.UNIT_ODES_FRIGATE_WARSONG_FROSTWOLF_FEL_HORDE, Faction.UNLIMITED);
      Goblin.ModObjectLimit(Constants.UNIT_OJGN_JUGGERNAUT_WARSONG_FROSTWOLF_FEL_HORDE, 6);
      Goblin.ModObjectLimit(Constants.UNIT_N062_SHREDDER_GOBLIN, 12);
      Goblin.ModObjectLimit(Constants.UNIT_H08Z_ASSAULT_TANK_GOBLIN, 5);
      Goblin.ModObjectLimit(Constants.UNIT_H091_WAR_ZEPPELIN_GOBLIN, 6);
      Goblin.ModObjectLimit(Constants.UNIT_H09H_SIEGE_WALKER_GOBLIN, 5);
      Goblin.ModObjectLimit(Constants.UNIT_NZEP_TRADING_ZEPPELIN_WARSONG, 16);
      Goblin.ModObjectLimit(Constants.UNIT_O04S_TRADER_GOBLIN, 10);

      Goblin.ModObjectLimit(Constants.UNIT_O04N_TRADE_PRINCE_OF_THE_BILGEWATER_CARTEL_GOBLIN, 1);
      Goblin.ModObjectLimit(Constants.UNIT_NTIN_CHIEF_ENGINEER_GOBLIN, 1);
      Goblin.ModObjectLimit(Constants.UNIT_NALC_BARON_OF_GADGETZAN_GOBLIN, 1);

      Goblin.ModObjectLimit(Constants.UPGRADE_R07M_ALCHEMIST_GRANDMASTER_TRAINING_GOBLIN, Faction.UNLIMITED);
      Goblin.ModObjectLimit(Constants.UPGRADE_R097_FORTIFIED_HULLS_GOBLIN, Faction.UNLIMITED);

      var oilPower = new OilPower
      {
        Name = "Oil Tycoon",
        IconName = "OilStation",
        StartingRandomOilPoolCount = 3,
        MaximumOilPoolCount = 15,
        OilPoolMinimumValue = 1500,
        OilPoolMaximumValue = 9000,
        OilPoolBorderDistance = 600,
        ForcedStartingOilPoolSpawnLocations = new [] { new Point(-4825f, -282f) }
      };
      Goblin.AddPower(oilPower);

      Goblin.AddGoldMine(preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(-8615, -12869)));
      FactionManager.Register(Goblin);
    }
  }
}