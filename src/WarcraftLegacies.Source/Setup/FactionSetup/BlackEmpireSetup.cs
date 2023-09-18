using MacroTools;
using MacroTools.FactionSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Setup.FactionSetup
{
  public static class BlackEmpireSetup
  {
    public static Faction? BlackEmpire { get; private set; }
    
    public static void Setup(PreplacedUnitSystem preplacedUnitSystem)
    {
      BlackEmpire = new Faction("BlackEmpire", PLAYER_COLOR_TURQUOISE, "|C0000FFFF",
        @"ReplaceableTextures\CommandButtons\BTNNzothIcon.blp")
      {
        ControlPointDefenderUnitTypeId = Constants.UNIT_N0DV_CONTROL_POINT_DEFENDER_BLACK_EMPIRE_TOWER,
      };

      BlackEmpire.ModObjectLimit(Constants.UNIT_N0AR_TWISTING_HALLS_YOGG_T1, Faction.UNLIMITED);
      BlackEmpire.ModObjectLimit(Constants.UNIT_N0AS_WHISPERING_LABYRINTH_YOGG_T2, Faction.UNLIMITED);
      BlackEmpire.ModObjectLimit(Constants.UNIT_N0AT_CATHEDRAL_OF_MADNESS_YOGG_T3, Faction.UNLIMITED);
      BlackEmpire.ModObjectLimit(Constants.UNIT_N0AU_PULSATING_PORTAL_YOGG_FARM, Faction.UNLIMITED);
      BlackEmpire.ModObjectLimit(Constants.UNIT_H06U_SIPHONING_CRYSTAL_YOGG_RESEARCH, Faction.UNLIMITED);
      BlackEmpire.ModObjectLimit(Constants.UNIT_N0AW_INCUBATION_POOL_YOGG_BARRACKS, Faction.UNLIMITED);
      BlackEmpire.ModObjectLimit(Constants.UNIT_N0B3_SHADOW_LAIR_YOGG_MAGIC, Faction.UNLIMITED);
      BlackEmpire.ModObjectLimit(Constants.UNIT_N0AX_PIT_OF_TORMENT_YOGG_SPECIALIST, Faction.UNLIMITED);
      BlackEmpire.ModObjectLimit(Constants.UNIT_N0B2_OMINOUS_VAULT_YOGG_SHOP, Faction.UNLIMITED);
      BlackEmpire.ModObjectLimit(Constants.UNIT_N0AV_ALTAR_OF_MADNESS_YOGG_ALTAR, Faction.UNLIMITED);
      BlackEmpire.ModObjectLimit(Constants.UNIT_H09E_MADNESS_POOL_YOGG_TOWER, Faction.UNLIMITED);
      BlackEmpire.ModObjectLimit(Constants.UNIT_N0AY_ACID_SPITTER_YOGG_TOWER, Faction.UNLIMITED);
      BlackEmpire.ModObjectLimit(Constants.UNIT_N0B0_IMPROVED_ACID_SPITTER_YOGG_TOWER, Faction.UNLIMITED);
      BlackEmpire.ModObjectLimit(Constants.UNIT_N0AZ_SLEEPLESS_WATCHER_YOGG_TOWER, Faction.UNLIMITED);
      BlackEmpire.ModObjectLimit(Constants.UNIT_N0B1_IMPROVED_SLEEPLESS_WATCHER_YOGG_TOWER, Faction.UNLIMITED);

      // All Nzoth Units listed below - with total limits 

      BlackEmpire.ModObjectLimit(Constants.UNIT_N0B5_SCAVENGER_YOGG_WORKER, Faction.UNLIMITED);
      BlackEmpire.ModObjectLimit(Constants.UNIT_N0B4_REAPER_YOGG, 6);

      BlackEmpire.ModObjectLimit(Constants.UNIT_O01G_FACELESS_MARAUDER_YOGG, Faction.UNLIMITED);
      BlackEmpire.ModObjectLimit(Constants.UNIT_O04V_SOUL_HUNTER_YOGG, Faction.UNLIMITED);
      BlackEmpire.ModObjectLimit(Constants.UNIT_U029_STYGIAN_HULK_YOGG, 12);

      BlackEmpire.ModObjectLimit(Constants.UNIT_N077_SORCERER_YOGG, Faction.UNLIMITED);
      BlackEmpire.ModObjectLimit(Constants.UNIT_O04Y_FATEWEAVER_YOGG, Faction.UNLIMITED);
      BlackEmpire.ModObjectLimit(Constants.UNIT_U02F_FORGOTTEN_ONE_YOGG, 2);
      BlackEmpire.ModObjectLimit(Constants.UNIT_O04Z_FLYING_HORROR_YOGG, 12);
      BlackEmpire.ModObjectLimit(Constants.UNIT_N0AH_DEFORMED_CHIMERA_YOGG, 4);
      BlackEmpire.ModObjectLimit(Constants.UNIT_H09F_GLADIATOR_YOGG, Faction.UNLIMITED  );

     // All Nzoth Ships

      BlackEmpire.ModObjectLimit(FourCC("ushp"), Faction.UNLIMITED); //Undead Shipyard
      BlackEmpire.ModObjectLimit(FourCC("ubot"), Faction.UNLIMITED); //Undead Transport Ship
      BlackEmpire.ModObjectLimit(FourCC("h0AT"), Faction.UNLIMITED); //Scout
      BlackEmpire.ModObjectLimit(FourCC("h0AW"), Faction.UNLIMITED); //Frigate
      BlackEmpire.ModObjectLimit(FourCC("h0AM"), Faction.UNLIMITED); //Fireship
      BlackEmpire.ModObjectLimit(FourCC("h0AZ"), Faction.UNLIMITED); //Galley
      BlackEmpire.ModObjectLimit(FourCC("h0AQ"), Faction.UNLIMITED); //Boarding
      BlackEmpire.ModObjectLimit(FourCC("h0BB"), Faction.UNLIMITED); //Juggernaut
      BlackEmpire.ModObjectLimit(FourCC("h0B9"), 6); //Bombard


      // All Nzoth Heroes listed below - with total limits

      BlackEmpire.ModObjectLimit(Constants.UNIT_U02A_N_RAQI_ABERRATION_YOGG, 1);
      BlackEmpire.ModObjectLimit(Constants.UNIT_U02B_YOGG_SARON_CHAMPION, 1);
      BlackEmpire.ModObjectLimit(Constants.UNIT_E01D_N_RAQI_ARCANIST_YOGG, 1);

      BlackEmpire.ModObjectLimit(FourCC("R00K"), Faction.UNLIMITED); //Power Infusion

      FactionManager.Register(BlackEmpire);
    }
  }
}