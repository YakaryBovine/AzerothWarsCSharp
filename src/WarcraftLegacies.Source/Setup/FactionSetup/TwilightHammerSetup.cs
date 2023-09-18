using MacroTools;
using MacroTools.FactionSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Setup.FactionSetup
{
  public static class TwilightHammerSetup
  {
    public static Faction? TwilightHammer { get; private set; }
    
    public static void Setup(PreplacedUnitSystem preplacedUnitSystem)
    {
      TwilightHammer = new Faction("TwilightHammer", PLAYER_COLOR_LAVENDER, "|cff9680b4",
        @"ReplaceableTextures\CommandButtons\BTNChogall.blp")
      {
        ControlPointDefenderUnitTypeId = Constants.UNIT_N0DX_CONTROL_POINT_DEFENDER_TWILIGHT,
      };

      TwilightHammer.ModObjectLimit(Constants.UNIT_O039_GREAT_HALL_TWILIGHT_T1, Faction.UNLIMITED);
      TwilightHammer.ModObjectLimit(Constants.UNIT_O03A_STRONGHOLD_TWILIGHT_T2, Faction.UNLIMITED);
      TwilightHammer.ModObjectLimit(Constants.UNIT_O03B_FORTRESS_TWILIGHT_T3, Faction.UNLIMITED);
      TwilightHammer.ModObjectLimit(Constants.UNIT_O03K_BURROW_TWILIGHT_FARM, Faction.UNLIMITED);
      TwilightHammer.ModObjectLimit(Constants.UNIT_O03J_VOIDFORGE_TWILIGHT_RESEARCH, Faction.UNLIMITED);
      TwilightHammer.ModObjectLimit(Constants.UNIT_O03D_CULT_GATHERING_TWILIGHT_BARRACKS, Faction.UNLIMITED);
      TwilightHammer.ModObjectLimit(Constants.UNIT_O03E_GRIM_TOTEM_TWILIGHT_MAGIC, Faction.UNLIMITED);
      TwilightHammer.ModObjectLimit(Constants.UNIT_O03F_DRAGON_HATCHERY_TWILIGHT_SPECIALIST, Faction.UNLIMITED);
      TwilightHammer.ModObjectLimit(Constants.UNIT_O03C_ALTAR_OF_VISIONS_TWILIGHT_ALTAR, Faction.UNLIMITED);
      TwilightHammer.ModObjectLimit(Constants.UNIT_U00Y_BLACK_VAULT_TWILIGHT_SHOP, Faction.UNLIMITED);
      TwilightHammer.ModObjectLimit(Constants.UNIT_O03G_SENTRY_TOWER_TWILIGHT_TOWER, Faction.UNLIMITED);
      TwilightHammer.ModObjectLimit(Constants.UNIT_O03H_IMPROVED_TWILIGHT_LOOKOUT_TWILIGHT_TOWER_2, Faction.UNLIMITED);

      // All Nzoth Units listed below - with total limits 

      TwilightHammer.ModObjectLimit(Constants.UNIT_O04B_CULTIST_TWILIGHT_HAMMER_WORKER, Faction.UNLIMITED);
      TwilightHammer.ModObjectLimit(Constants.UNIT_O04I_BATTLEMASTER_TWILIGHT, 6);

      TwilightHammer.ModObjectLimit(Constants.UNIT_O01H_TWILIGHT_HAMMER_ENFORCER_TWILIGHT, Faction.UNLIMITED);
      TwilightHammer.ModObjectLimit(Constants.UNIT_N087_PHASE_GRENADIER_TWILIGHT, Faction.UNLIMITED);
      TwilightHammer.ModObjectLimit(Constants.UNIT_O04K_SKULL_CATAPULT_TWILIGHT, 6);

      TwilightHammer.ModObjectLimit(Constants.UNIT_U01T_HERALD_TWILIGHT, Faction.UNLIMITED);
      TwilightHammer.ModObjectLimit(Constants.UNIT_N07X_OGRE_MAGI_TWILIGHT, Faction.UNLIMITED);
      TwilightHammer.ModObjectLimit(Constants.UNIT_N09O_ORCISH_DEATH_KNIGHT_TWILIGHT, 6);

      TwilightHammer.ModObjectLimit(Constants.UNIT_N083_TWILIGHT_DRAGONSPAWN_TWILIGHT, Faction.UNLIMITED);
      TwilightHammer.ModObjectLimit(Constants.UNIT_N051_NETHER_DRAGON_TWILIGHT, 4);
      TwilightHammer.ModObjectLimit(Constants.UNIT_O04J_VOID_RIDER_TWILIGHT, 8);

      // All Nzoth Ships

      //Ship
      TwilightHammer.ModObjectLimit(FourCC("o03I"), Faction.UNLIMITED); //Shipyard
      TwilightHammer.ModObjectLimit(FourCC("obot"), Faction.UNLIMITED); //Transport Ship
      TwilightHammer.ModObjectLimit(FourCC("h0AS"), Faction.UNLIMITED); //Scout
      TwilightHammer.ModObjectLimit(FourCC("h0AP"), Faction.UNLIMITED); //Frigate
      TwilightHammer.ModObjectLimit(FourCC("h0B2"), Faction.UNLIMITED); //Fireship
      TwilightHammer.ModObjectLimit(FourCC("h0AY"), Faction.UNLIMITED); //Galley
      TwilightHammer.ModObjectLimit(FourCC("h0B5"), Faction.UNLIMITED); //Boarding
      TwilightHammer.ModObjectLimit(FourCC("h0BC"), Faction.UNLIMITED); //Juggernaut
      TwilightHammer.ModObjectLimit(FourCC("h0AO"), 6); //Bombard


      // All Nzoth Heroes listed below - with total limits

      TwilightHammer.ModObjectLimit(Constants.UNIT_H08Q_HIGH_PRIESTESS_TWILIGHT_HAMMER, 1);
      TwilightHammer.ModObjectLimit(Constants.UNIT_O04H_CHAMPION_OF_THE_TWILIGHT_S_HAMMER_TWILIGHT, 1);
      TwilightHammer.ModObjectLimit(Constants.UNIT_O06M_ASCENDANT_COUNCILLOR_OF_FLAME_TWILIGHT, 1);
      TwilightHammer.ModObjectLimit(Constants.UNIT_O01P_LEADER_OF_THE_TWILIGHT_S_HAMMER_TWILIGHT_HAMMER, 1);

      TwilightHammer.ModObjectLimit(FourCC("R00K"), Faction.UNLIMITED); //Power Infusion

      FactionManager.Register(TwilightHammer);
    }
  }
}