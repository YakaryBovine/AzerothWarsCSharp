using MacroTools.LegendSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Setup.Legends
{
  public sealed class LegendTroll
  {
    public LegendaryHero Zul { get; }
    public LegendaryHero Rastakhan { get; }
    public LegendaryHero Hakkar { get; }
    public LegendaryHero Gahzrilla { get; }

    public LegendTroll()
    {
      Zul = new LegendaryHero("Zul")
      {
        UnitType = FourCC("O01J")
      };

      Gahzrilla = new LegendaryHero("Gahz'rilla")
      {
        UnitType = FourCC("H06Q"),
        StartingXp = 1400
      };

      Hakkar = new LegendaryHero("Hakkar the Soulflayer")
      {
        UnitType = FourCC("U023"),
        StartingXp = 10800
      };

      Rastakhan = new LegendaryHero("Rastakhan")
      {
        UnitType = FourCC("O026"),
        StartingXp = 2800
      };
    }
    public void RegisterLegends()
    {
      LegendaryHeroManager.Register(Zul);
      LegendaryHeroManager.Register(Hakkar);
      LegendaryHeroManager.Register(Rastakhan);
      LegendaryHeroManager.Register(Gahzrilla);
    }
  }
}