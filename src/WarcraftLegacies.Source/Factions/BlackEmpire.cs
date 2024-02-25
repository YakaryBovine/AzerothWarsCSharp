using MacroTools;
using MacroTools.FactionSystem;
using WarcraftLegacies.Source.Setup;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Factions
{
  public class BlackEmpire : Faction
  {
    private readonly PreplacedUnitSystem _preplacedUnitSystem;
    private readonly AllLegendSetup _allLegendSetup;
    private readonly ArtifactSetup _artifactSetup;

    /// <inheritdoc />
    public BlackEmpire(PreplacedUnitSystem preplacedUnitSystem, AllLegendSetup allLegendSetup, ArtifactSetup artifactSetup) : base("BlackEmpire", PLAYER_COLOR_TURQUOISE, "|C0000FFFF",
      @"ReplaceableTextures\CommandButtons\BTNNzothIcon.blp")
    {
      _preplacedUnitSystem = preplacedUnitSystem;
      _allLegendSetup = allLegendSetup;
      _artifactSetup = artifactSetup;
      ControlPointDefenderUnitTypeId = Constants.UNIT_N0DV_CONTROL_POINT_DEFENDER_BLACK_EMPIRE_TOWER;
    }
    
    /// <inheritdoc />
    public override void OnRegistered()
    {
      RegisterObjectLimits();
      SharedFactionConfigSetup.AddSharedFactionConfig(this);
    }

    private void RegisterObjectLimits()
    {
      ModObjectLimit(Constants.UNIT_N0AR_TWISTING_HALLS_YOGG_T1, UNLIMITED);
      ModObjectLimit(Constants.UNIT_N0AS_WHISPERING_LABYRINTH_YOGG_T2, UNLIMITED);
      ModObjectLimit(Constants.UNIT_N0AT_CATHEDRAL_OF_MADNESS_YOGG_T3, UNLIMITED);
      ModObjectLimit(Constants.UNIT_N0AU_PULSATING_PORTAL_YOGG_FARM, UNLIMITED);
      ModObjectLimit(Constants.UNIT_H06U_SIPHONING_CRYSTAL_YOGG_RESEARCH, UNLIMITED);
      ModObjectLimit(Constants.UNIT_N0AW_INCUBATION_POOL_YOGG_BARRACKS, UNLIMITED);
      ModObjectLimit(Constants.UNIT_N0B3_SHADOW_LAIR_YOGG_MAGIC, UNLIMITED);
      ModObjectLimit(Constants.UNIT_N0AX_PIT_OF_TORMENT_YOGG_SPECIALIST, UNLIMITED);
      ModObjectLimit(Constants.UNIT_N0B2_OMINOUS_VAULT_YOGG_SHOP, UNLIMITED);
      ModObjectLimit(Constants.UNIT_N0AV_ALTAR_OF_MADNESS_YOGG_ALTAR, UNLIMITED);
      ModObjectLimit(Constants.UNIT_H09E_MADNESS_POOL_YOGG_TOWER, UNLIMITED);
      ModObjectLimit(Constants.UNIT_N0AY_ACID_SPITTER_YOGG_TOWER, UNLIMITED);
      ModObjectLimit(Constants.UNIT_N0B0_IMPROVED_ACID_SPITTER_YOGG_TOWER, UNLIMITED);
      ModObjectLimit(Constants.UNIT_N0AZ_SLEEPLESS_WATCHER_YOGG_TOWER, UNLIMITED);
      ModObjectLimit(Constants.UNIT_N0B1_IMPROVED_SLEEPLESS_WATCHER_YOGG_TOWER, UNLIMITED);

      // All Nzoth Units listed below - with total limits 

      ModObjectLimit(Constants.UNIT_N0B5_SCAVENGER_YOGG_WORKER, UNLIMITED);
      ModObjectLimit(Constants.UNIT_N0B4_REAPER_YOGG, 6);

      ModObjectLimit(Constants.UNIT_O01G_FACELESS_MARAUDER_YOGG, UNLIMITED);
      ModObjectLimit(Constants.UNIT_O04V_SOUL_HUNTER_YOGG, UNLIMITED);
      ModObjectLimit(Constants.UNIT_U029_STYGIAN_HULK_YOGG, 12);

      ModObjectLimit(Constants.UNIT_N077_SORCERER_YOGG, UNLIMITED);
      ModObjectLimit(Constants.UNIT_O04Y_FATEWEAVER_YOGG, UNLIMITED);
      ModObjectLimit(Constants.UNIT_U02F_FORGOTTEN_ONE_YOGG, 2);
      ModObjectLimit(Constants.UNIT_O04Z_FLYING_HORROR_YOGG, 12);
      ModObjectLimit(Constants.UNIT_N0AH_DEFORMED_CHIMERA_YOGG, 4);
      ModObjectLimit(Constants.UNIT_H09F_GLADIATOR_YOGG, UNLIMITED  );

     // All Nzoth Ships

      ModObjectLimit(FourCC("ushp"), UNLIMITED); //Undead Shipyard
      ModObjectLimit(FourCC("ubot"), UNLIMITED); //Undead Transport Ship
      ModObjectLimit(FourCC("h0AT"), UNLIMITED); //Scout
      ModObjectLimit(FourCC("h0AW"), UNLIMITED); //Frigate
      ModObjectLimit(FourCC("h0AM"), UNLIMITED); //Fireship
      ModObjectLimit(FourCC("h0AZ"), UNLIMITED); //Galley
      ModObjectLimit(FourCC("h0AQ"), UNLIMITED); //Boarding
      ModObjectLimit(FourCC("h0BB"), UNLIMITED); //Juggernaut
      ModObjectLimit(FourCC("h0B9"), 6); //Bombard


      // All Nzoth Heroes listed below - with total limits

      ModObjectLimit(Constants.UNIT_U02A_N_RAQI_ABERRATION_YOGG, 1);
      ModObjectLimit(Constants.UNIT_U02B_YOGG_SARON_CHAMPION, 1);
      ModObjectLimit(Constants.UNIT_E01D_N_RAQI_ARCANIST_YOGG, 1);

      ModObjectLimit(FourCC("R00K"), UNLIMITED); //Power Infusion
    }
  }
}