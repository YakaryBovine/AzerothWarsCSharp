using WarcraftLegacies.Shared.Models.Factions;
using static Constants;

namespace WarcraftLegacies.Shared.Config.Factions
{
  public static class AhnqirajObjectLimitData
  {
    private const int Unlimited = 200;

    public static IEnumerable<ObjectLimit> GetAllObjectLimits()
    {
      yield return new(UNIT_U020_MONUMENT_C_THUN_T1, Unlimited);
      yield return new(UNIT_U021_RUINED_TEMPLE_C_THUN_T2, Unlimited);
      yield return new(UNIT_U022_NEXUS_C_THUN_T3, Unlimited);
      yield return new(UNIT_N071_PILLAR_OF_C_THUN_C_THUN_PILLARS, Unlimited);
      yield return new(UNIT_U01G_SPIRIT_HALL_C_THUN_RESEARCH, Unlimited);
      yield return new(UNIT_O00R_HATCHERY_C_THUN_BARRACK, Unlimited);
      yield return new(UNIT_O00D_PYRAMID_C_THUN_MAGIC, Unlimited);
      yield return new(UNIT_U01H_ANCIENT_CATACOMBS_C_THUN_SPECIALIST, Unlimited);
      yield return new(UNIT_U01I_CHAMBER_OF_WONDERS_C_THUN_SHOP, Unlimited);
      yield return new(UNIT_U01F_ALTAR_OF_THE_OLD_ONES_C_THUN_ALTAR, Unlimited);
      yield return new(UNIT_STZ5_LIGHTHOUSE_C_THUN_EMPTY, Unlimited);
      yield return new(UNIT_TP73_TUNNEL_CTHUN, Unlimited);
      yield return new(UNIT_U019_WORKER_C_THUN_WORKER, Unlimited);
      yield return new(UNIT_O000_ROYALTY_C_THUN_ELITES, 6);
      yield return new(UNIT_N06I_SOLDIER_C_THUN_SILITHID_WARRIOR, Unlimited);
      yield return new(UNIT_O00L_CREMATOGASTER_C_THUN_SILITHID_REAVER, Unlimited);
      yield return new(UNIT_U013_SUPER_MAJOR_C_THUN, 12);
      yield return new(UNIT_N060_TUNNELER_C_THUN_SILITHID_TUNNELER, Unlimited);
      yield return new(UNIT_N05V_SHADOW_WEAVER_C_THUN_FACELESS_SHADOW_WEAVER, Unlimited);
      yield return new(UNIT_O001_TOL_VIR_STATUE_C_THUN_TOL_VIR_STATUE, 6);
      yield return new(UNIT_O02N_ELATE_CTHUN, 24);
      yield return new(UNIT_H01N_VILE_CORRUPTER_C_THUN, 4);
      yield return new(UNIT_SL2O_OBSIDIAN_ERADICATOR_CTHUN, 12);
      yield return new(UNIT_USHP_HAUNTED_HARBOR_SCOURGE_SHIPYARD, Unlimited);
      yield return new(UNIT_UBOT_HAUNTED_TRANSPORT_SHIP_SCOURGE_LEGION, Unlimited);
      yield return new(UNIT_H0AT_SCOUT_SHIP_UNDEAD, Unlimited);
      yield return new(UNIT_H0AW_FRIGATE_UNDEAD, Unlimited);
      yield return new(UNIT_H0AM_FIRESHIP_UNDEAD, Unlimited);
      yield return new(UNIT_H0AZ_GALLEY_UNDEAD, Unlimited);
      yield return new(UNIT_H0AQ_BOARDING_VESSEL_UNDEAD, Unlimited);
      yield return new(UNIT_H0BB_JUGGERNAUT_UNDEAD, Unlimited);
      yield return new(UNIT_H0B9_BOMBARD_UNDEAD, 6);
      yield return new(UNIT_U02S_ANCIENT_SAND_WORM, 1);
      yield return new(UNIT_E005_THE_PROPHET, 1);
      yield return new(UNIT_U00Z_OBSIDIAN_DESTROYER, 1);
      yield return new(UPGRADE_RS01_TUNNELER_MASTER_TRAINING_CTHUN, Unlimited);
      yield return new(UPGRADE_RZ02_SHADOW_WEAVER_MASTER_TRAINING_CTHUN, Unlimited);
      yield return new(UPGRADE_RL11_TOL_VIR_STATUE_MASTER_TRAINING_CTHUN, Unlimited);
      yield return new(UPGRADE_RYW5_IMPROVED_SWARM_BEETLE_CTHUN_WARRIOR, Unlimited);
      yield return new(UPGRADE_RTL3_IMPROVED_SEED_OF_MADNESS_CTHUN_WARRIOR, Unlimited);
      yield return new(UPGRADE_RHL9_WEB_CTHUN, Unlimited);
      yield return new(UPGRADE_R003_PROGENESIS_C_THUN, Unlimited);
      yield return new(UPGRADE_ZB12_CLEAVING_ATTACK_C_THUN, Unlimited);
      yield return new(UPGRADE_ZB14_ELONGATED_SNOUTS_C_THUN_SILITHID_WASP, Unlimited);
      yield return new(UPGRADE_ZBEH_DEFENSIVE_COCOOON_AHN_QIRAJ, Unlimited);
      yield return new(UPGRADE_ZBRI_RAPID_INCUBATION_AHN_QIRAJ, Unlimited);
      yield return new(UPGRADE_ZBHS_SHAPED_OBSIDIAN_C_THUN, Unlimited);
      yield return new(UPGRADE_ZBML_SPELL_CONDUCTION_C_THUN, Unlimited);
      yield return new(UPGRADE_RDBD_DEEP_BURROW_C_THUN, Unlimited);
    }
  }
}