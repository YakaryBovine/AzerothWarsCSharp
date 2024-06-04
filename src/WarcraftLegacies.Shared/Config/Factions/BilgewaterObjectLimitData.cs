using WarcraftLegacies.Shared.Models.Factions;
using static WarcraftLegacies.Shared.Constants;

namespace WarcraftLegacies.Shared.Config.Factions
{
  public static class BilgewaterObjectLimitData
  {
    private const int Unlimited = 200;

    public static IEnumerable<ObjectLimit> GetAllObjectLimits()
    {
      yield return new(UNIT_O03L_GREAT_HALL_GOBLIN_T1, Unlimited);
      yield return new(UNIT_O03M_STRONGHOLD_GOBLIN_T2, Unlimited);
      yield return new(UNIT_O03N_FORTRESS_GOBLIN_T3, Unlimited);
      yield return new(UNIT_O03O_ALTAR_OF_INDUSTRY_GOBLIN_ALTAR, Unlimited);
      yield return new(UNIT_O03P_MERCENARY_DEN_GOBLIN_BARRACKS, Unlimited);
      yield return new(UNIT_O05T_BOOT_CAMP_GOBLIN_SIEGE, Unlimited);
      yield return new(UNIT_O03Q_SCRAPYARD_GOBLIN_RESEARCH, Unlimited);
      yield return new(UNIT_O03S_LABORATORY_GOBLIN_MAGIC, Unlimited);
      yield return new(UNIT_O01M_ENGINEER_S_GUILD_GOBLIN_SPECIALIST, Unlimited);
      yield return new(UNIT_O03T_BUNKER_GOBLIN_FARM, Unlimited);
      yield return new(UNIT_O03U_ROCKET_TOWER_GOBLIN_TOWER, Unlimited);
      yield return new(UNIT_O03W_IMPROVED_ROCKET_TOWER_GOBLIN_TOWER_2, Unlimited);
      yield return new(UNIT_O03X_AUCTION_HOUSE_GOBLIN_SHOP, Unlimited);
      yield return new(UNIT_O03V_BILGEWATER_HARBOR_GOBLIN_SHIPYARD, Unlimited);
      yield return new(UNIT_H011_INTERCONTINENTAL_ARTILLERY_GOBLIN_TOWER, 1);
      yield return new(UNIT_O06G_OIL_RIG_CONSTRUCTOR_GOBLIN, Unlimited);
      yield return new(UNIT_H0AS_SCOUT_SHIP_HORDE, Unlimited);
      yield return new(UNIT_H0AP_FRIGATE_HORDE, Unlimited);
      yield return new(UNIT_H0B2_FIRESHIP_HORDE, Unlimited);
      yield return new(UNIT_H0AY_GALLEY_HORDE, Unlimited);
      yield return new(UNIT_H0B5_BOARDING_VESSEL_HORDE, Unlimited);
      yield return new(UNIT_H0BC_JUGGERNAUT_HORDE, Unlimited);
      yield return new(UNIT_H0AO_BOMBARD_HORDE, 6);
      yield return new(UNIT_O02I_BUILDER_GOBLIN_WORKER, Unlimited);
      yield return new(UNIT_N099_OGRE_MERCENARY_GOBLIN, Unlimited);
      yield return new(UNIT_H08X_SAPPERS_GOBLIN, 8);
      yield return new(UNIT_H08Y_GUNNER_GOBLIN, Unlimited);
      yield return new(UNIT_U02R_HOBGOBLIN_GOBLIN, Unlimited);
      yield return new(UNIT_H09I_PERSONAL_TANK_GOBLIN, 12);
      yield return new(UNIT_H09J_GRENADIER_GOBLIN, 12);
      yield return new(UNIT_ODOC_WITCH_DOCTOR_FROSTWOLF, Unlimited);
      yield return new(UNIT_O04O_ALCHEMIST_GOBLIN, Unlimited);
      yield return new(UNIT_O04Q_TINKER_GOBLIN, 6);
      yield return new(UNIT_ODES_FRIGATE_WARSONG_FROSTWOLF_FEL_HORDE, Unlimited);
      yield return new(UNIT_OJGN_JUGGERNAUT_WARSONG_FROSTWOLF_FEL_HORDE, 6);
      yield return new(UNIT_N062_SHREDDER_GOBLIN, 12);
      yield return new(UNIT_H08Z_ASSAULT_TANK_GOBLIN, 5);
      yield return new(UNIT_H091_WAR_ZEPPELIN_GOBLIN, 6);
      yield return new(UNIT_H09H_SIEGE_WALKER_GOBLIN, 5);
      yield return new(UNIT_NZEP_TRADING_ZEPPELIN_WARSONG, 16);
      yield return new(UNIT_O04S_TRADER_GOBLIN, 10);
      yield return new(UNIT_O04N_TRADE_PRINCE_OF_THE_BILGEWATER_CARTEL_GOBLIN, 1);
      yield return new(UNIT_NTIN_CHIEF_ENGINEER_GOBLIN, 1);
      yield return new(UNIT_VH01_BARON_OF_GADGETZAN_GOBLIN, 1);
      yield return new(UPGRADE_R07M_ALCHEMIST_GRANDMASTER_TRAINING_GOBLIN, Unlimited);
      yield return new(UPGRADE_R097_FORTIFIED_HULLS_GOBLIN, Unlimited);
    }
  }
}