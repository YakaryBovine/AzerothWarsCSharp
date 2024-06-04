using WarcraftLegacies.Shared.Models.Factions;
using static WarcraftLegacies.Shared.Constants;

namespace WarcraftLegacies.Shared.Config.Factions
{
  public static class ScourgeObjectLimitData
  {
    private const int Unlimited = 200;

    public static IEnumerable<ObjectLimit> GetAllObjectLimits()
    {
      yield return new(UNIT_UNPL_NECROPOLIS_SCOURGE_T1, Unlimited);
      yield return new(UNIT_UNP1_HALLS_OF_THE_DEAD_SCOURGE_T2, Unlimited);
      yield return new(UNIT_UNP2_BLACK_CITADEL_SCOURGE_T3, Unlimited);
      yield return new(UNIT_UZIG_ZIGGURAT_SCOURGE_FARM, Unlimited);
      yield return new(UNIT_UZG1_SPIRIT_TOWER_SCOURGE_TOWER, Unlimited);
      yield return new(UNIT_UZG2_NERUBIAN_TOWER_SCOURGE_TOWER, Unlimited);
      yield return new(UNIT_UAOD_ALTAR_OF_DARKNESS_SCOURGE_ALTAR, Unlimited);
      yield return new(UNIT_USEP_CRYPT_SCOURGE_BARRACKS, Unlimited);
      yield return new(UNIT_UGRV_GRAVEYARD_SCOURGE_RESEARCH, Unlimited);
      yield return new(UNIT_USLH_SLAUGHTERHOUSE_SCOURGE_SPECIALIST, Unlimited);
      yield return new(UNIT_UTOD_TEMPLE_OF_THE_DAMNED_SCOURGE_MAGIC, Unlimited);
      yield return new(UNIT_UBON_BONEYARD_SCOURGE_SIEGE, Unlimited);
      yield return new(UNIT_UTOM_TOMB_OF_RELICS_SCOURGE_SHOP, Unlimited);
      yield return new(UNIT_USHP_HAUNTED_HARBOR_SCOURGE_SHIPYARD, Unlimited);
      yield return new(UNIT_U002_IMPROVED_SPIRIT_TOWER_SCOURGE_TOWER, Unlimited);
      yield return new(UNIT_U003_IMPROVED_NERUBIAN_TOWER_SCOURGE_TOWER, Unlimited);

      //Units
      yield return new(UNIT_UACO_ACOLYTE_SCOURGE_WORKER, Unlimited);
      yield return new(UNIT_USHD_SHADE_SCOURGE, Unlimited);
      yield return new(UNIT_UGHO_GHOUL_SCOURGE, Unlimited);
      yield return new(UNIT_UABO_ABOMINATION_SCOURGE, Unlimited);
      yield return new(UNIT_UMTW_MEAT_WAGON_SCOURGE, 8);
      yield return new(UNIT_UCRY_CRYPT_FIEND_SCOURGE, Unlimited);
      yield return new(UNIT_UGAR_GARGOYLE_SCOURGE, 12);
      yield return new(UNIT_H02G_VRYKUL_WARRIOR_VRYKUL, Unlimited);
      yield return new(UNIT_H061_VRYKUL_BERSERK_VRYKUL, 12);
      yield return new(UNIT_H00P_MAMMOTH_RIDER_VRYKUL, 4);
      yield return new(UNIT_UBAN_BANSHEE_SCOURGE, Unlimited);
      yield return new(UNIT_UNEC_NECROMANCER_SCOURGE, Unlimited);
      yield return new(UNIT_UOBS_OBSIDIAN_STATUE_SCOURGE, 4);
      yield return new(UNIT_UFRO_FROST_WYRM_SCOURGE, 4);
      yield return new(UNIT_H00H_DEATH_KNIGHT_SCOURGE_ELITE, 6);
      yield return new(UNIT_UBSP_OBSIDIAN_DESTROYER_SCOURGE, 6);
      yield return new(UNIT_NFGL_PLAGUE_TITAN_SCOURGE, 2);

      //Ship
      yield return new(UNIT_UBOT_HAUNTED_TRANSPORT_SHIP_SCOURGE_LEGION, Unlimited);
      yield return new(UNIT_H0AT_SCOUT_SHIP_UNDEAD, Unlimited);
      yield return new(UNIT_H0AW_FRIGATE_UNDEAD, Unlimited);
      yield return new(UNIT_H0AM_FIRESHIP_UNDEAD, Unlimited);
      yield return new(UNIT_H0AZ_GALLEY_UNDEAD, Unlimited);
      yield return new(UNIT_H0AQ_BOARDING_VESSEL_UNDEAD, Unlimited);
      yield return new(UNIT_H0BB_JUGGERNAUT_UNDEAD, Unlimited);
      yield return new(UNIT_H0B9_BOMBARD_UNDEAD, 6);

      //Demi-Heroes
      yield return new(UNIT_UBDD_SAPPHIRON_SCOURGE_DEMI, 1);
      yield return new(UNIT_UANB_KING_OF_AZJOL_NERUB_SCOURGE, 1);
      yield return new(UNIT_U001_MASTER_OF_THE_CULT_OF_THE_DAMNED_SCOURGE_NECROMANCER, 1);
      yield return new(UNIT_U00M_MASTER_OF_THE_CULT_OF_THE_DAMNED_SCOURGE_GHOST, 1);
      yield return new(UNIT_U00A_SCOURGE_COMMANDER_SCOURGE, 1);
      yield return new(UNIT_UKTL_ARCHLICH_OF_THE_SCOURGE_SCOURGE_LICH, 1);
      yield return new(UNIT_UEAR_CHAMPION_OF_THE_SCOURGE_SCOURGE, 1);

      //Upgrades
      yield return new(UPGRADE_RUBA_BANSHEE_GRANDMASTER_TRAINING_SCOURGE_RESEARCH, Unlimited);
      //todo list.Add( yield return new(FourCCyield return new("Rubu"); UNLIMITED));
      yield return new(UPGRADE_RUEX_EXHUME_CORPSES_SCOURGE, Unlimited);
      yield return new(UPGRADE_RUFB_FREEZING_BREATH_SCOURGE, Unlimited);
      yield return new(UPGRADE_RUGF_GHOUL_FRENZY_SCOURGE, Unlimited);
      yield return new(UPGRADE_RUNE_NECROMANCER_GRANDMASTER_TRAINING_SCOURGE, Unlimited);
      yield return new(UPGRADE_RUWB_WEB_RED_RESEARCH, Unlimited);
      yield return new(UPGRADE_R00Q_CHILLING_AURA_SCOURGE, Unlimited);
      yield return new(UPGRADE_R01X_EPIDEMIC_SCOURGE, Unlimited);
      yield return new(UPGRADE_R01D_HOWL_OF_TERROR_SCOURGE, Unlimited);
      yield return new(UPGRADE_R06N_IMPROVED_ORB_OF_ANNIHILATION_SCOURGE, Unlimited);
      yield return new(UPGRADE_RUSL_SKELETAL_LONGEVITY, Unlimited);
      yield return new(UPGRADE_RUSM_SKELETAL_MASTERY, Unlimited);

      yield return new(UPGRADE_R07X_MAKE_ARTHAS_THE_LICH_KING_SCOURGE, Unlimited);
    }
  }
}