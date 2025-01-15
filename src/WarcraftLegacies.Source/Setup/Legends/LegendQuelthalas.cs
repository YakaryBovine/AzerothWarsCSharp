using MacroTools;
using MacroTools.LegendSystem;
using WCSharp.Shared.Data;

#pragma warning disable CS1591

namespace WarcraftLegacies.Source.Setup.Legends
{
  public sealed class LegendQuelthalas
  {
    public LegendaryHero Anasterian { get; }
    public LegendaryHero Rommath { get; }
    public LegendaryHero Solarian { get; }
    public LegendaryHero Sylvanas { get; }
    public LegendaryHero Kael { get; }
    public LegendaryHero Lorthemar { get; }
    public LegendaryHero Kiljaeden { get; }
    public LegendaryHero Pathaleon { get; }
    public Capital Silvermoon { get; }
    public Capital Sunwell { get; }
    public Capital Spire { get; }

    public LegendQuelthalas(PreplacedUnitSystem preplacedUnitSystem)
    {
      Silvermoon = new Capital
      {
        Unit = preplacedUnitSystem.GetUnit(FourCC("h003")),
        DeathMessage = "The grand city of the high elves, Silvermoon, has been crushed by her enemies.",
        Essential = true
      };
      Silvermoon.AddProtector(preplacedUnitSystem.GetUnit(UNIT_H00D_ELVEN_RUNESTONE_QUEL_THALAS_OTHER, new Point(20479, 17477)));
      Silvermoon.AddProtector(preplacedUnitSystem.GetUnit(UNIT_H00D_ELVEN_RUNESTONE_QUEL_THALAS_OTHER, new Point(17415, 13196)));

      Sunwell = new Capital
      {
        Unit = preplacedUnitSystem.GetUnit(UNIT_N001_THE_SUNWELL_QUEL_THALAS_OTHER),
        Capturable = true,
        Essential = true,
        DeathMessage = "The Sunwell, once a source of great magical energy, is no more. Its corruption has ended, and the land is free from its dark influence."
      };
      Sunwell.AddProtector(Silvermoon.Unit);

      Anasterian = new LegendaryHero("Anasterian Sunstrider")
      {
        UnitType = UNIT_H00Q_KING_OF_QUEL_THALAS_QUEL_THALAS,
        PlayerColor = PLAYER_COLOR_MAROON,
        StartingXp = 1000,
        StartingArtifacts = new()
        {
          new(CreateItem(ITEM_I00J_FELO_MELORN, Regions.ArtifactDummyInstance.Center.X, Regions.ArtifactDummyInstance.Center.Y))
        }
      };

      Rommath = new LegendaryHero("Grand Magister Rommath")
      {
        UnitType = UNIT_H04F_ARCHMAGE_GREEN,
        StartingXp = 4000
      };

      Solarian = new LegendaryHero("Solarian")
      {
        UnitType = UNIT_U02V_HIGH_ASTROMANCER_SUNFURY,
        StartingXp = 2800
      };

      Pathaleon = new LegendaryHero("Pathaleon")
      {
        UnitType = UNIT_H098_SUNFURY_MASTERMIND_HIGH_ELVES,
        StartingXp = 1800
      };

      Sylvanas = new LegendaryHero("Sylvanas Windrunner")
      {
        UnitType = UNIT_HVWD_RANGER_GENERAL_OF_SILVERMOON_QUEL_THALAS,
        PlayerColor = PLAYER_COLOR_GREEN
      };

      Kael = new LegendaryHero("Kael'thas Sunstrider")
      {
        PlayerColor = PLAYER_COLOR_RED,
        UnitType = UNIT_HKAL_PRINCE_OF_QUEL_THALAS_QUEL_THALAS
      };

      Lorthemar = new LegendaryHero("Lor'themar Theron")
      {
        UnitType = UNIT_H02E_REGENT_OF_QUEL_THALAS_QUEL_THALAS_VASSAL,
        StartingXp = 2800
      };

      Kiljaeden = new LegendaryHero("Kil'jaeden")
      {
        UnitType = UNIT_U004_THE_DECEIVER_LEGION,
        PermaDies = true,
        StartingXp = 10800,
        DeathMessage =
          "Kil'jaeden the Deceiver has been annihilated, but it is too late for the Sunfury, who will continue to live and die with demonic taint coursing through their veins."
      };

      Spire = new Capital
      {
        Unit = preplacedUnitSystem.GetUnit(UNIT_H0C2_WINDRUNNER_SPIRE_QUELTHALAS),
        Capturable = true
      };
    }
    
    public void RegisterLegends()
    {
      LegendaryHeroManager.Register(Anasterian);
      LegendaryHeroManager.Register(Rommath);
      LegendaryHeroManager.Register(Solarian);
      LegendaryHeroManager.Register(Sylvanas);
      LegendaryHeroManager.Register(Kael);
      LegendaryHeroManager.Register(Lorthemar);
      LegendaryHeroManager.Register(Kiljaeden);
      LegendaryHeroManager.Register(Pathaleon);
      CapitalManager.Register(Silvermoon);
      CapitalManager.Register(Sunwell);
      CapitalManager.Register(Spire);
    }
  }
}