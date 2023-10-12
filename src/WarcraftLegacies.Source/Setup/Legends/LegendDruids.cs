using MacroTools;
using MacroTools.LegendSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Setup.Legends
{
  public sealed class LegendDruids
  {
    public LegendaryHero Cenarius { get; }
    public LegendaryHero Malfurion { get; }
    public LegendaryHero Fandral { get; }
    public LegendaryHero Ursoc { get; }
    public LegendaryHero Tortolla { get; }
    public Capital Nordrassil { get; }
    public Capital Vordrassil { get; }
    public Capital TempleOfTheMoon { get; }
    public Capital CenarionHold { get; }
    
    public static int UnittypeCenariusGhost => Constants.UNIT_E00H_DEMIGOD_OF_THE_NIGHT_ELVES_DRUIDS_GHOST;

    public LegendDruids(PreplacedUnitSystem preplacedUnitSystem)
    {
      Cenarius = new LegendaryHero("Cenarius")
      {
        UnitType = FourCC("Ecen"),
        PermaDies = true,
        DeathMessage =
          "The Lord of the Forest, Cenarius, has fallen. The druids of the Kaldorei have lost their greatest mentor.",
        DeathSfx = @"Objects\Spawnmodels\NightElf\EntBirthTarget\EntBirthTarget.mdl",
        PlayerColor = PLAYER_COLOR_CYAN,
      };

      Malfurion = new LegendaryHero("Malfurion")
      {
        UnitType = FourCC("Efur"),
        StartingArtifactItemTypeIds = new()
        {
          Constants.ITEM_I00C_G_HANIR_THE_MOTHER_TREE
        }
      };

      Fandral = new LegendaryHero("Fandral")
      {
        UnitType = FourCC("E00K")
      };

      Ursoc = new LegendaryHero("Ursoc")
      {
        UnitType = FourCC("E00A"),
        StartingXp = 7000
      };

      Nordrassil = new Capital
      {
        Unit = preplacedUnitSystem.GetUnit(FourCC("n002")),
        Capturable = true,
        Essential = true
      };
      
      Vordrassil = new Capital
      {
        Capturable = true,
        Essential = true
      };
      
      CenarionHold = new Capital
      {
        Capturable = true,
        Essential = true,
        Unit = preplacedUnitSystem.GetUnit(Constants.UNIT_N06D_CENARION_HOLD_SENTINEL_OTHER)
      };

      TempleOfTheMoon = new Capital
      {
        Unit = preplacedUnitSystem.GetUnit(FourCC("o029")),
      };

      Tortolla = new LegendaryHero("Tortolla")
      {
        UnitType = FourCC("H04U"),
        StartingXp = 4000
      };
    }

    public void RegisterLegends()
    {
      LegendaryHeroManager.Register(Cenarius);
      LegendaryHeroManager.Register(Malfurion);
      LegendaryHeroManager.Register(Fandral);
      LegendaryHeroManager.Register(Ursoc);
      LegendaryHeroManager.Register(Tortolla);
      CapitalManager.Register(Nordrassil);
      CapitalManager.Register(Vordrassil);
      CapitalManager.Register(TempleOfTheMoon);
      CapitalManager.Register(CenarionHold);
    }
  }
}