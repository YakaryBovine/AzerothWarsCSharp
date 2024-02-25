using System.Collections.Generic;
using MacroTools;
using MacroTools.ArtifactSystem;
using MacroTools.Extensions;
using MacroTools.LegendSystem;
using WCSharp.Shared.Data;
using static War3Api.Common;

#pragma warning disable CS1591

namespace WarcraftLegacies.Source.Setup.Legends
{
  /// <summary>
  /// Responsible for creating and storing all Lordaeron <see cref="Legend"/>s.
  /// </summary>
  public sealed class LegendLordaeron
  {
    public LegendaryHero Terenas { get; }
    public LegendaryHero Uther { get; }
    public LegendaryHero Arthas { get; }
    public LegendaryHero Mograine { get; }
    public LegendaryHero Garithos { get; }
    public LegendaryHero Goodchild { get; }
    public Capital CapitalPalace { get; }
    public Capital Stratholme { get; }
    public Capital TyrsHand { get; }
    public Capital Monastery { get; }

    /// <summary>
    /// Sets up all Lordaeron <see cref="Legend"/>s.
    /// </summary>
    public LegendLordaeron(PreplacedUnitSystem preplacedUnitSystem, ArtifactSetup artifactSetup)
    {
      Terenas = new LegendaryHero("Terenas Menethil")
      {
        Unit = preplacedUnitSystem.GetUnit(Constants.UNIT_NEMI_KING_TERENAS_MENETHIL_LORDAERON)
      };
      CreateTrigger()
        .RegisterUnitEvent(Terenas.Unit, EVENT_UNIT_DEATH)
        .AddAction(() =>
        {
          if (artifactSetup.CrownOfLordaeron.OwningUnit == Terenas.Unit)
            artifactSetup.CrownOfLordaeron.Item.SetPosition(Regions.King_Arthas_crown.Center);
        });

      Mograine = new LegendaryHero("Alexandros Mograine")
      {
        UnitType = Constants.UNIT_H01J_THE_ASHBRINGER_LORDAERON,
        StartingXp = 5400,
        StartingArtifacts = new List<Artifact>
        {
          new(CreateItem(Constants.ITEM_I012_ASHBRINGER, Regions.ArtifactDummyInstance.Center.X, Regions.ArtifactDummyInstance.Center.Y))
        }
      };

      Garithos = new LegendaryHero("Garithos")
      {
        UnitType = Constants.UNIT_HLGR_GRAND_MARSHAL_SCARLET,
        StartingXp = 5400
      };

      Goodchild = new LegendaryHero("High Commander Goodchilde")
      {
        UnitType = Constants.UNIT_E000_IMPROVED_ANCIENT_PROTECTOR_DRUID_TOWER,
        StartingXp = 2800
      };

      CapitalPalace = new Capital
      {
        Unit = preplacedUnitSystem.GetUnit(Constants.UNIT_H000_CAPITAL_PALACE_LORDAERON),
        Capturable = true,
        Essential = true
      };
      CapitalPalace.AddProtector(preplacedUnitSystem.GetUnit(Constants.UNIT_H006_IMPROVED_GUARD_TOWER_LORDAERON_TOWER,
        new Point(8686, 8862)));
      CapitalPalace.AddProtector(preplacedUnitSystem.GetUnit(Constants.UNIT_H006_IMPROVED_GUARD_TOWER_LORDAERON_TOWER,
        new Point(9476, 8843)));
      CapitalPalace.AddProtector(preplacedUnitSystem.GetUnit(Constants.UNIT_H007_IMPROVED_CANNON_TOWER_LORDAERON_TOWER,
        new Point(8638, 9342)));
      CapitalPalace.AddProtector(preplacedUnitSystem.GetUnit(Constants.UNIT_H007_IMPROVED_CANNON_TOWER_LORDAERON_TOWER,
        new Point(9545, 9372)));

      Stratholme = new Capital
      {
        Unit = preplacedUnitSystem.GetUnit(Constants.UNIT_H01G_STRATHOLME_CASTLE_LORDAERON_OTHER),
        DeathMessage = "The majestic city of Stratholme has been destroyed.",
        Essential = true
      };
      Stratholme.AddProtector(preplacedUnitSystem.GetUnit(Constants.UNIT_H007_IMPROVED_CANNON_TOWER_LORDAERON_TOWER,
        new Point(14587, 14172)));
      Stratholme.AddProtector(preplacedUnitSystem.GetUnit(Constants.UNIT_H006_IMPROVED_GUARD_TOWER_LORDAERON_TOWER,
        new Point(15800, 13242)));

      TyrsHand = new Capital
      {
        Unit = preplacedUnitSystem.GetUnit(Constants.UNIT_H030_TYR_S_HAND_CITADEL_LORDAERON_OTHER),
        DeathMessage = "Tyr's Hand, the bastion of human power in Lordaeron, has fallen.",
        Essential = true
      };
      TyrsHand.AddProtector(preplacedUnitSystem.GetUnit(Constants.UNIT_HCTW_CANNON_TOWER_LORDAERON_TOWER,
        new Point(20652, 8057)));
      TyrsHand.AddProtector(preplacedUnitSystem.GetUnit(Constants.UNIT_H007_IMPROVED_CANNON_TOWER_LORDAERON_TOWER,
        new Point(20024, 8123)));
      TyrsHand.AddProtector(preplacedUnitSystem.GetUnit(Constants.UNIT_H007_IMPROVED_CANNON_TOWER_LORDAERON_TOWER,
        new Point(20042, 7420)));

      Uther = new LegendaryHero("Uther the Lightbringer")
      {
        UnitType = Constants.UNIT_HUTH_LEADER_OF_THE_SILVER_HAND_LORDAERON,
        DeathMessage =
          "Uther the Lightbringer makes his last stand, perishing in the defense of the light. Lordaeron, and humanity itself, has lost one of its finest exemplars in this dark hour.",
        PlayerColor = PLAYER_COLOR_LIGHT_BLUE,
        StartingXp = 2800
      };

      Arthas = new LegendaryHero("Arthas Menethil")
      {
        UnitType = Constants.UNIT_HART_CROWN_PRINCE_OF_LORDAERON_LORDAERON,
        PlayerColor = PLAYER_COLOR_BLUE,
      };

      Monastery = new Capital
      {
        Unit = preplacedUnitSystem.GetUnit(Constants.UNIT_H00T_SCARLET_MONASTERY_SCARLET_LORDAERON),
        Capturable = true
      };
    }


    public void RegisterLegends()
    {
      LegendaryHeroManager.Register(Terenas);
      LegendaryHeroManager.Register(Uther);
      LegendaryHeroManager.Register(Arthas);
      LegendaryHeroManager.Register(Mograine);
      LegendaryHeroManager.Register(Garithos);
      LegendaryHeroManager.Register(Goodchild);
      CapitalManager.Register(CapitalPalace);
      CapitalManager.Register(Stratholme);
      CapitalManager.Register(TyrsHand);
      CapitalManager.Register(Monastery);
    }
  }
}