using MacroTools;
using MacroTools.FactionSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Setup.FactionSetup
{
  public static class CthunSetup
  {
    public static Faction? Cthun { get; private set; }
    
    public static void Setup(PreplacedUnitSystem preplacedUnitSystem)
    {
      Cthun = new Faction("Ahn'qiraj", PLAYER_COLOR_WHEAT, "|cffaaa050",
        @"ReplaceableTextures\CommandButtons\BTNCthunIcon.blp")
      {
        ControlPointDefenderUnitTypeId = Constants.UNIT_N0DW_CONTROL_POINT_DEFENDER_CTHUN_TOWER,
      };

      Cthun.ModObjectLimit(Constants.UNIT_U020_MONUMENT_C_THUN_BUILDING, Faction.UNLIMITED);
      Cthun.ModObjectLimit(Constants.UNIT_U021_RUINED_TEMPLE_C_THUN_BUILDING, Faction.UNLIMITED);
      Cthun.ModObjectLimit(Constants.UNIT_U022_NEXUS_C_THUN_BUILDING, Faction.UNLIMITED);
      Cthun.ModObjectLimit(Constants.UNIT_N071_PILLAR_OF_C_THUN_C_THUN_PILLARS, Faction.UNLIMITED);
      Cthun.ModObjectLimit(Constants.UNIT_U01G_SPIRIT_HALL_C_THUN_BUILDING, Faction.UNLIMITED);
      Cthun.ModObjectLimit(Constants.UNIT_O00R_HATCHERY_C_THUN_BUILDING, Faction.UNLIMITED);
      Cthun.ModObjectLimit(Constants.UNIT_O00D_PYRAMID_C_THUN_BUILDING, Faction.UNLIMITED);
      Cthun.ModObjectLimit(Constants.UNIT_U01H_ANCIENT_CATACOMBS_C_THUN_BUILDING, Faction.UNLIMITED);
      Cthun.ModObjectLimit(Constants.UNIT_U01I_CHAMBER_OF_WONDERS_C_THUN_BUILDING_ITEM, Faction.UNLIMITED);
      Cthun.ModObjectLimit(Constants.UNIT_U01F_ALTAR_OF_THE_OLD_ONES_C_THUN_BUILDING, Faction.UNLIMITED);


      Cthun.ModObjectLimit(Constants.UNIT_U019_DRONE_C_THUN_WORKER, Faction.UNLIMITED);
      Cthun.ModObjectLimit(Constants.UNIT_O000_SILITHID_COLOSSUS_C_THUN_ELITES, 6);

      Cthun.ModObjectLimit(Constants.UNIT_N06I_SILITHID_WARRIOR_C_THUN_SILITHID_WARRIOR, Faction.UNLIMITED);
      Cthun.ModObjectLimit(Constants.UNIT_O00L_SILITHID_REAVER_C_THUN_SILITHID_REAVER, Faction.UNLIMITED);
      Cthun.ModObjectLimit(Constants.UNIT_U013_GIANT_SCARAB_C_THUN_GIANT_SCARAB, Faction.UNLIMITED);

      Cthun.ModObjectLimit(Constants.UNIT_N060_SILITHID_TUNNELER_C_THUN_SILITHID_TUNNELER, Faction.UNLIMITED);
      Cthun.ModObjectLimit(Constants.UNIT_N05V_SHADOW_WEAVER_C_THUN_FACELESS_SHADOW_WEAVER, Faction.UNLIMITED);
      Cthun.ModObjectLimit(Constants.UNIT_O001_TOL_VIR_STATUE_C_THUN_TOL_VIR_STATUE, 6);

      Cthun.ModObjectLimit(Constants.UNIT_H01K_SILITHID_OVERLORD_C_THUN_OVERLORD, 12);
      Cthun.ModObjectLimit(Constants.UNIT_O02N_SILITHID_WASP_CTHUN, 24);
      Cthun.ModObjectLimit(Constants.UNIT_H01N_VILE_CORRUPTER_C_THUN_FACELESS_CORRUPTOR, 4);

      Cthun.ModObjectLimit(FourCC("ushp"), Faction.UNLIMITED); //Undead Shipyard
      Cthun.ModObjectLimit(FourCC("ubot"), Faction.UNLIMITED); //Undead Transport Ship
      Cthun.ModObjectLimit(FourCC("h0AT"), Faction.UNLIMITED); //Scout
      Cthun.ModObjectLimit(FourCC("h0AW"), Faction.UNLIMITED); //Frigate
      Cthun.ModObjectLimit(FourCC("h0AM"), Faction.UNLIMITED); //Fireship
      Cthun.ModObjectLimit(FourCC("h0AZ"), Faction.UNLIMITED); //Galley
      Cthun.ModObjectLimit(FourCC("h0AQ"), Faction.UNLIMITED); //Boarding
      Cthun.ModObjectLimit(FourCC("h0BB"), Faction.UNLIMITED); //Juggernaut
      Cthun.ModObjectLimit(FourCC("h0B9"), 6); //Bombard

      Cthun.ModObjectLimit(Constants.UNIT_U02S_ANCIENT_SAND_WORM, 1);
      Cthun.ModObjectLimit(Constants.UNIT_E005_THE_PROPHET, 1);
      Cthun.ModObjectLimit(Constants.UNIT_U00Z_OBSIDIAN_DESTROYER, 1);

      Cthun.ModObjectLimit(FourCC("R00K"), Faction.UNLIMITED); //Power Infusion


      FactionManager.Register(Cthun);
    }
  }
}