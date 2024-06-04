using WarcraftLegacies.Shared.Models.Factions;
using static WarcraftLegacies.Shared.Constants;

namespace WarcraftLegacies.Shared.Config.Factions
{
  public static class BlackEmpireObjectLimitData
  {
    private const int Unlimited = 200;

    public static IEnumerable<ObjectLimit> GetAllObjectLimits()
    {
      yield return new(UNIT_N0AR_TWISTING_HALLS_YOGG_T1, Unlimited);
      yield return new(UNIT_N0AS_WHISPERING_LABYRINTH_YOGG_T2, Unlimited);
      yield return new(UNIT_N0AT_CATHEDRAL_OF_MADNESS_YOGG_T3, Unlimited);
      yield return new(UNIT_N0AU_PULSATING_PORTAL_YOGG_FARM, Unlimited);
      yield return new(UNIT_H06U_SIPHONING_CRYSTAL_YOGG_RESEARCH, Unlimited);
      yield return new(UNIT_N0AW_INCUBATION_POOL_YOGG_BARRACKS, Unlimited);
      yield return new(UNIT_N0B3_SHADOW_LAIR_YOGG_MAGIC, Unlimited);
      yield return new(UNIT_ST5K_PIT_OF_TORMENT_YOGG_SPECIALIST, Unlimited);
      yield return new(UNIT_N0AX_MUTATION_CIRCLE_YOGG_SPECIALIST, Unlimited);
      yield return new(UNIT_N0B2_OMINOUS_VAULT_YOGG_SHOP, Unlimited);
      yield return new(UNIT_N0AV_ALTAR_OF_MADNESS_YOGG_ALTAR, Unlimited);
      yield return new(UNIT_H09E_MADNESS_POOL_YOGG_TOWER, Unlimited);
      yield return new(UNIT_N0AY_ACID_SPITTER_YOGG_TOWER, Unlimited);
      yield return new(UNIT_N0B0_IMPROVED_ACID_SPITTER_YOGG_TOWER, Unlimited);
      yield return new(UNIT_N0AZ_SLEEPLESS_WATCHER_YOGG_TOWER, Unlimited);
      yield return new(UNIT_N0B1_IMPROVED_SLEEPLESS_WATCHER_YOGG_TOWER, Unlimited);
      yield return new(UNIT_N0B5_SCAVENGER_YOGG_WORKER, Unlimited);
      yield return new(UNIT_N0B4_REAPER_YOGG, 6);
      yield return new(UNIT_O01G_BRUTE_YOGG, Unlimited);
      yield return new(UNIT_O04V_LURKER_YOGG, Unlimited);
      yield return new(UNIT_U029_STYGIAN_HULK_YOGG, 8);
      yield return new(UNIT_SHZ5_AQIR_BLACK_EMPIRE, 6);
      yield return new(UNIT_N077_MINDLASHER_YOGG, Unlimited);
      yield return new(UNIT_O04Y_FATEWEAVER_YOGG, Unlimited);
      yield return new(UNIT_U02F_FORGOTTEN_ONE_YOGG, 2);
      yield return new(UNIT_BHN1_HERALD_YOGG, 6);
      yield return new(UNIT_O04Z_FLYING_HORROR_YOGG, 12);
      yield return new(UNIT_N0AH_DEFORMED_CHIMERA_YOGG, 4);
      yield return new(UNIT_H09F_DEEP_FIEND_YOGG, 12);
      yield return new(UNIT_USHP_HAUNTED_HARBOR_SCOURGE_SHIPYARD, Unlimited); //Undead Shipyard
      yield return new(UNIT_H0AT_SCOUT_SHIP_UNDEAD, Unlimited); //Undead Transport Ship
      yield return new(UNIT_H0AW_FRIGATE_UNDEAD, Unlimited); //Scout
      yield return new(UNIT_UBOT_HAUNTED_TRANSPORT_SHIP_SCOURGE_LEGION, Unlimited); //Frigate
      yield return new(UNIT_H0AM_FIRESHIP_UNDEAD, Unlimited); //Fireship
      yield return new(UNIT_H0AZ_GALLEY_UNDEAD, Unlimited); //Galley
      yield return new(UNIT_H0AQ_BOARDING_VESSEL_UNDEAD, Unlimited); //Boarding
      yield return new(UNIT_H0BB_JUGGERNAUT_UNDEAD, Unlimited); //Juggernaut
      yield return new(UNIT_H0B9_BOMBARD_UNDEAD, 6); //Bombard
      yield return new(UNIT_U00P_LIEUTENANT_OF_N_ZOTH, 1);
      yield return new(UNIT_U02B_N_RAQI_ABERRATION, 1);
      yield return new(UNIT_E01D_MOUTH_OF_N_ZOTH_YOGG, 1);
      yield return new(UPGRADE_RBEF_FATEWEAVER_MASTER_TRAINING_BLACKEMPIRE, Unlimited);
      yield return new(UPGRADE_RBEM_MINDLASHER_MASTER_TRAINING_BLACKEMPIRE, Unlimited);
      yield return new(UPGRADE_RBEH_HERALD_MASTER_TRAINING_BLACKEMPIRE, Unlimited);
      yield return new(UPGRADE_RBEP_PARALYSING_FEAR_BLACK_EMPIRE, Unlimited);
      yield return new(UPGRADE_RBEV_SHADOW_VEIL_BLACK_EMPIRE, Unlimited);
      yield return new(UPGRADE_RBEA_ACCELERATED_CYCLE_BLACK_EMPIRE_STYGIAN_HULK, Unlimited);
      yield return new(UPGRADE_RBFC_CURSED_FLESH_BLACK_EMPIRE_AQIR_AND_FORGOTTEN_ONE, Unlimited);
      yield return new(UPGRADE_RBES_UNWORLDLY_SCYTHE_BLACK_EMPIRE_REAPER, Unlimited);
    }
  }
}
