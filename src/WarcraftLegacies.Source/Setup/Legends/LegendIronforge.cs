using MacroTools;
using MacroTools.LegendSystem;
using WCSharp.Shared.Data;
using static War3Api.Common;
#pragma warning disable CS1591

namespace WarcraftLegacies.Source.Setup.Legends
{
  public sealed class LegendIronforge
  {
    public LegendaryHero Dagran { get; }
    public LegendaryHero Falstad { get; }
    public LegendaryHero Magni { get; }
    public Capital GreatForge { get; }
    public Capital Thelsamar { get; }
    public Capital MenethilHarbor { get; }

    public LegendIronforge(PreplacedUnitSystem preplacedUnitSystem)
    {
      Dagran = new LegendaryHero("Dagran Thaurissan")
      {
        UnitType = FourCC("H03G"),
        StartingXp = 7000
      };

      Falstad = new LegendaryHero("Falstad Wildhammer")
      {
        UnitType = FourCC("H028"),
        StartingXp = 5400
      };

      Magni = new LegendaryHero("Magni Bronzebeard")
      {
        UnitType = FourCC("H00S"),
        DeathMessage = "King Magni Bronzebeard has died.", //Todo: bad flavour
        StartingXp = 1000
      };

      GreatForge = new Capital()
      {
        Unit = preplacedUnitSystem.GetUnit(FourCC("h001")),
        DeathMessage = "The Great Forge has been extinguished.", //Todo: mediocre flavour
        Essential = true
      };
      GreatForge.AddProtector(preplacedUnitSystem.GetUnit(Constants.UNIT_H07K_IMPROVED_CANNON_TOWER_IRONFORGE_TOWER, new Point(10509, -5976)));
      GreatForge.AddProtector(preplacedUnitSystem.GetUnit(Constants.UNIT_H07K_IMPROVED_CANNON_TOWER_IRONFORGE_TOWER, new Point(10710, -5974)));

      Thelsamar = new Capital
      {
        Unit = preplacedUnitSystem.GetUnit(FourCC("h05H"))
      };

      MenethilHarbor = new Capital
      {
        Unit = preplacedUnitSystem.GetUnit(FourCC("h0AK"))
      };
    }
    
    public void RegisterLegends()
    {
      LegendaryHeroManager.Register(Dagran);
      LegendaryHeroManager.Register(Falstad);
      LegendaryHeroManager.Register(Magni);
      CapitalManager.Register(GreatForge);
      CapitalManager.Register(Thelsamar);
      CapitalManager.Register(MenethilHarbor);
    }
  }
}