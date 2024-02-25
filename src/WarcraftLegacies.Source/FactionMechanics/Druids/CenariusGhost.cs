using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using WarcraftLegacies.Source.Setup.Legends;
using WCSharp.Shared.Data;

namespace WarcraftLegacies.Source.FactionMechanics.Druids
{
  public static class CenariusGhost
  {
    private static void Dies(Legend legend, Faction druids)
    {
      if (legend is not LegendaryHero cenarius) 
        return;
      cenarius.UnitType = LegendDruids.UnittypeCenariusGhost;
      cenarius.PermaDies = false;
      cenarius.ClearUnitDependencies();
      cenarius.StartingXp = 5400;
      cenarius.ForceCreate(druids.Player, new Point(Regions.Cenarius.Center.X, Regions.Cenarius.Center.Y),
        270);
    }

    public static void Setup(LegendaryHero cenarius, Faction druids)
    {
      cenarius.PermanentlyDied += (sender, hero) => Dies(hero, druids);
      cenarius.DeathMessage =
        "Cenarius, Demigod of the Night Elves, has fallen. His spirit lives on, a mere echo of his former self.";
    }
  }
}