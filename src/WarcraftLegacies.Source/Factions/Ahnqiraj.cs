﻿using MacroTools;
using MacroTools.FactionSystem;
using WarcraftLegacies.Source.Setup;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Factions
{
  public class Ahnqiraj : Faction
  {
    private readonly PreplacedUnitSystem _preplacedUnitSystem;
    private readonly AllLegendSetup _allLegendSetup;
    private readonly ArtifactSetup _artifactSetup;

    /// <inheritdoc />
    public Ahnqiraj(PreplacedUnitSystem preplacedUnitSystem, AllLegendSetup allLegendSetup, ArtifactSetup artifactSetup) : base("Ahn'qiraj", PLAYER_COLOR_WHEAT, "|cffaaa050",
      @"ReplaceableTextures\CommandButtons\BTNCthunIcon.blp")
    {
      _preplacedUnitSystem = preplacedUnitSystem;
      _allLegendSetup = allLegendSetup;
      _artifactSetup = artifactSetup;
      ControlPointDefenderUnitTypeId = Constants.UNIT_N0DW_CONTROL_POINT_DEFENDER_CTHUN_TOWER;
    }
    
        
    /// <inheritdoc />
    public override void OnRegistered()
    {
      RegisterObjectLimits();
      RegisterQuests();
    }

    private void RegisterObjectLimits()
    {
      ModObjectLimit(Constants.UNIT_U020_MONUMENT_C_THUN_BUILDING, Faction.UNLIMITED);
      ModObjectLimit(Constants.UNIT_U021_RUINED_TEMPLE_C_THUN_BUILDING, Faction.UNLIMITED);
      ModObjectLimit(Constants.UNIT_U022_NEXUS_C_THUN_BUILDING, Faction.UNLIMITED);
      ModObjectLimit(Constants.UNIT_N071_PILLAR_OF_C_THUN_C_THUN_PILLARS, Faction.UNLIMITED);
      ModObjectLimit(Constants.UNIT_U01G_SPIRIT_HALL_C_THUN_BUILDING, Faction.UNLIMITED);
      ModObjectLimit(Constants.UNIT_O00R_HATCHERY_C_THUN_BUILDING, Faction.UNLIMITED);
      ModObjectLimit(Constants.UNIT_O00D_PYRAMID_C_THUN_BUILDING, Faction.UNLIMITED);
      ModObjectLimit(Constants.UNIT_U01H_ANCIENT_CATACOMBS_C_THUN_BUILDING, Faction.UNLIMITED);
      ModObjectLimit(Constants.UNIT_U01I_CHAMBER_OF_WONDERS_C_THUN_BUILDING_ITEM, Faction.UNLIMITED);
      ModObjectLimit(Constants.UNIT_U01F_ALTAR_OF_THE_OLD_ONES_C_THUN_BUILDING, Faction.UNLIMITED);


      ModObjectLimit(Constants.UNIT_U019_DRONE_C_THUN_WORKER, Faction.UNLIMITED);
      ModObjectLimit(Constants.UNIT_O000_SILITHID_COLOSSUS_C_THUN_ELITES, 6);

      ModObjectLimit(Constants.UNIT_N06I_SILITHID_WARRIOR_C_THUN_SILITHID_WARRIOR, Faction.UNLIMITED);
      ModObjectLimit(Constants.UNIT_O00L_SILITHID_REAVER_C_THUN_SILITHID_REAVER, Faction.UNLIMITED);
      ModObjectLimit(Constants.UNIT_U013_GIANT_SCARAB_C_THUN_GIANT_SCARAB, Faction.UNLIMITED);

      ModObjectLimit(Constants.UNIT_N060_SILITHID_TUNNELER_C_THUN_SILITHID_TUNNELER, Faction.UNLIMITED);
      ModObjectLimit(Constants.UNIT_N05V_SHADOW_WEAVER_C_THUN_FACELESS_SHADOW_WEAVER, Faction.UNLIMITED);
      ModObjectLimit(Constants.UNIT_O001_TOL_VIR_STATUE_C_THUN_TOL_VIR_STATUE, 6);

      ModObjectLimit(Constants.UNIT_H01K_SILITHID_OVERLORD_C_THUN_OVERLORD, 12);
      ModObjectLimit(Constants.UNIT_O02N_SILITHID_WASP_CTHUN, 24);
      ModObjectLimit(Constants.UNIT_H01N_VILE_CORRUPTER_C_THUN_FACELESS_CORRUPTOR, 4);

      ModObjectLimit(FourCC("ushp"), Faction.UNLIMITED); //Undead Shipyard
      ModObjectLimit(FourCC("ubot"), Faction.UNLIMITED); //Undead Transport Ship
      ModObjectLimit(FourCC("h0AT"), Faction.UNLIMITED); //Scout
      ModObjectLimit(FourCC("h0AW"), Faction.UNLIMITED); //Frigate
      ModObjectLimit(FourCC("h0AM"), Faction.UNLIMITED); //Fireship
      ModObjectLimit(FourCC("h0AZ"), Faction.UNLIMITED); //Galley
      ModObjectLimit(FourCC("h0AQ"), Faction.UNLIMITED); //Boarding
      ModObjectLimit(FourCC("h0BB"), Faction.UNLIMITED); //Juggernaut
      ModObjectLimit(FourCC("h0B9"), 6); //Bombard

      ModObjectLimit(Constants.UNIT_U02S_ANCIENT_SAND_WORM, 1);
      ModObjectLimit(Constants.UNIT_E005_THE_PROPHET, 1);
      ModObjectLimit(Constants.UNIT_U00Z_OBSIDIAN_DESTROYER, 1);

      ModObjectLimit(FourCC("R00K"), Faction.UNLIMITED); //Power Infusion
    }

    private void RegisterQuests()
    {
      throw new System.NotImplementedException();
    }
  }
}