using MacroTools;
using MacroTools.LegendSystem;
using WCSharp.Shared.Data;

namespace WarcraftLegacies.Source.Setup.Legends
{
  public sealed class LegendStormwind
  {
    public LegendaryHero Varian { get; }
    public LegendaryHero Khadgar { get; }
    public LegendaryHero Galen { get; }
    public LegendaryHero Bolvar { get; }
    public Capital StormwindKeep { get; }
    public Capital Darkshire { get; }
    public Capital ConstructionSiteMartial { get; }
    public Capital ConstructionSiteMagic { get; }

    public LegendStormwind(PreplacedUnitSystem preplacedUnitSystem)
    {
      Varian = new LegendaryHero("Varian Wrynn")
      {
        UnitType = Constants.UNIT_H00R_KING_OF_STORMWIND_DARK_GREEN,
        DeathMessage = "The King of Stormwind dies a warrior’s death, defending hearth and family. The Wrynn Dynasty crumbles with his passing.",
        StartingXp = 1800,
        StartingArtifactItemTypeIds = new()
        {
          Constants.ITEM_I00D_SHALAMAYNE
        }
      };

      Galen = new LegendaryHero("Galen Trollbane")
      {
        UnitType = Constants.UNIT_H00Z_CROWN_PRINCE_OF_STROMGARDE_STORMWIND,
        StartingXp = 5400,
        StartingArtifactItemTypeIds = new() { Constants.ITEM_I01O_TROL_KALAR }
      };

      Bolvar = new LegendaryHero("Bolvar Fordragon")
      {
        UnitType = Constants.UNIT_H017_HIGHLORD_OF_THE_ALLIANCE_DARK_GREEN
      };

      Khadgar = new LegendaryHero("Khadgar")
      {
        UnitType = Constants.UNIT_H05Y_LORD_WIZARD_STORMWIND,
        StartingXp = 7000
      };

      StormwindKeep = new Capital
      {
        Unit = preplacedUnitSystem.GetUnit(Constants.UNIT_H00X_STORMWIND_KEEP_STORMWIND_OTHER),
        DeathMessage = "Stormwind Keep, the capitol of the nation of Stormwind, has been destroyed!",
        Essential = true
      };
      StormwindKeep.AddProtector(preplacedUnitSystem.GetUnit(Constants.UNIT_H070_IMPROVED_GUARD_TOWER_STORMWIND_TOWER, new Point(9530, -10941)));
      StormwindKeep.AddProtector(preplacedUnitSystem.GetUnit(Constants.UNIT_H070_IMPROVED_GUARD_TOWER_STORMWIND_TOWER, new Point(10177, -10952)));

      Darkshire = new Capital
      {
        Unit = preplacedUnitSystem.GetUnit(Constants.UNIT_H03Y_DARKSHIRE_STORMWIND_OTHER)
      };

      ConstructionSiteMagic = new Capital
      {
        Unit = preplacedUnitSystem.GetUnit(Constants.UNIT_H053_CONSTRUCTION_SITE_STORMWIND_WIZARD_S_SANCTUM)
      };
      ConstructionSiteMagic.AddProtector(StormwindKeep.Unit);

      ConstructionSiteMartial = new Capital
      {
        Unit = preplacedUnitSystem.GetUnit(Constants.UNIT_H055_CONSTRUCTION_SITE_STORMWIND_CHAMPION_S_HALL)
      };
      ConstructionSiteMartial.AddProtector(StormwindKeep.Unit);
    }
    
    public void RegisterLegends()
    {
      LegendaryHeroManager.Register(Varian);
      LegendaryHeroManager.Register(Khadgar);
      LegendaryHeroManager.Register(Galen);
      LegendaryHeroManager.Register(Bolvar);
      CapitalManager.Register(StormwindKeep);
      CapitalManager.Register(Darkshire);
      CapitalManager.Register(ConstructionSiteMartial);
      CapitalManager.Register(ConstructionSiteMagic);
    }
  }
}