using WarcraftLegacies.Shared.Models.Factions;
using static WarcraftLegacies.Shared.Constants;

namespace WarcraftLegacies.Shared.Config.Factions
{
  public static class ScarletCrusadeObjectLimitData
  {
    private const int Unlimited = 200;

    public static IEnumerable<ObjectLimit> GetAllObjectLimits()
    {
      yield return new(UNIT_H0BM_TOWN_HALL_CRUSADE_T1, Unlimited);
      yield return new(UNIT_H0BN_KEEP_CRUSADE_T2, Unlimited);
      yield return new(UNIT_H0BO_CASTLE_CRUSADE_T3, Unlimited);
      yield return new(UNIT_H0BP_FARMSTEAD_CRUSADE_FARM, Unlimited);
      yield return new(UNIT_H0A3_BLACKSMITH_CRUSADE_RESEARCH, Unlimited);
      yield return new(UNIT_H09X_SHIPYARD_CRUSADE_SHIPYARD, Unlimited);
      yield return new(UNIT_H0AG_HALL_OF_SWORDS_CRUSADE_BARRACKS, Unlimited);
      yield return new(UNIT_H0BE_STUDIUM_CRUSADE_MAGIC, Unlimited);
      yield return new(UNIT_H0BL_ROOKERY_CRUSADE_BEAST, Unlimited);
      yield return new(UNIT_N035_DIVINE_BASTION_CRUSADE_SPECIAL, 1);
      yield return new(UNIT_H0BI_BOMBARD_TOWER_CRUSADE_TOWER, Unlimited);
      yield return new(UNIT_H0BJ_IMPROVED_BOMBARD_TOWER_CRUSADE_TOWER, Unlimited);
      yield return new(UNIT_H0BQ_ALTAR_OF_CRUSADERS_CRUSADE_ALTAR, Unlimited);
      yield return new(UNIT_N0D8_VENDOR_HALL_CRUSADE_SHOP, Unlimited);
      yield return new(UNIT_H022_FARMER_DALARAN_WORKER, Unlimited);
      yield return new(UNIT_H06B_TEMPLAR_LORDAERON, 6);
      yield return new(UNIT_H08I_CRUSADER_SCARLET, Unlimited);
      yield return new(UNIT_H09P_LONGBOWMAN_SCARLET, Unlimited);
      yield return new(UNIT_H08L_CAVALIER_SCARLET, Unlimited);
      yield return new(UNIT_N068_INQUISITOR_SCARLET, Unlimited);
      yield return new(UNIT_H08K_MONK_SCARLET, Unlimited);
      yield return new(UNIT_N09N_BISHOP_OF_THE_LIGHT_SCARLET, 6);
      yield return new(UNIT_H09Q_CRIMSON_COMMANDER_SCARLET, 8);
      yield return new(UNIT_O06V_EAGLE_RIDER_SCARLET, 6);
      yield return new(UNIT_E01L_GRYPHON_MARKSMAN_SCARLET, 6);
      yield return new(UNIT_O04C_SERAPHIM_SCARLET, 6);
      yield return new(UNIT_O00X_ARCHANGEL_SCARLET, 3);
      yield return new(UNIT_H08G_GRAND_CRUSADER_SCARLET, 1);
      yield return new(UNIT_H0A2_SCARLET_COMMANDER_SCARLET, 1);
      yield return new(UNIT_H08H_HIGH_INQUISITOR_SCARLET, 1);
      yield return new(UNIT_H00Y_HIGH_GENERAL_SCARLET, 1);
      yield return new(UNIT_HBOT_TRANSPORT_SHIP_ALLIANCE, Unlimited);
      yield return new(UNIT_H0AR_SCOUT_SHIP_ALLIANCE, Unlimited);
      yield return new(UNIT_H0AX_FRIGATE_ALLIANCE, Unlimited);
      yield return new(UNIT_H0B3_FIRESHIP_ALLIANCE, Unlimited);
      yield return new(UNIT_H0B0_GALLEY_ALLIANCE, Unlimited);
      yield return new(UNIT_H0B6_BOARDING_VESSEL_ALLIANCE, Unlimited);
      yield return new(UNIT_H0AN_JUGGERNAUT_ALLIANCE, Unlimited);
      yield return new(UNIT_H0B7_BOMBARD_ALLIANCE, 6);
      yield return new(UPGRADE_R05D_MONK_MASTER_TRAINING_SCARLET, Unlimited);
      yield return new(UPGRADE_R04F_INQUISITOR_MASTER_TRAINING_SCARLET_CRUSADE, Unlimited);
      yield return new(UPGRADE_RHSE_MAGIC_SENTRY, Unlimited);
      yield return new(
        UPGRADE_RHAC_IMPROVED_MASONRY_ADVANCED_MASONRY_IMBUED_MASONRY_YELLOW_PURPLE_ORANGE_GREEN_DARK_GREEN_RESEARCH,
        Unlimited);
    }
  }
}