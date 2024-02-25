using System.Collections.Generic;
using MacroTools;
using MacroTools.ArtifactSystem;
using MacroTools.Extensions;
using MacroTools.LegendSystem;
using WCSharp.Shared.Data;
using static War3Api.Common;
using static War3Api.Blizzard;

namespace WarcraftLegacies.Source.Setup.Legends
{
  /// <summary>
  /// Responsible for setting up and managing all Scourge <see cref="Legend"/>s.
  /// </summary>
  public sealed class LegendScourge
  {
    public LegendaryHero Kelthuzad { get; }
    public LegendaryHero Anubarak { get; }
    public LegendaryHero Rivendare { get; }
    public LegendaryHero Arthas { get; }
    public Capital TheFrozenThrone { get; }
    public Capital Utgarde { get; }

    /// <summary>
    /// Sets up <see cref="LegendScourge"/>.
    /// </summary>
    /// <param name="preplacedUnitSystem">A system for finding preplaced units on the map.</param>
    public LegendScourge(PreplacedUnitSystem preplacedUnitSystem)
    {
      Kelthuzad = new LegendaryHero("Kel'thuzad")
      {
        UnitType = FourCC("U001"),
        PermaDies = true,
        DeathSfx = @"Abilities\Spells\Undead\DeathCoil\DeathCoilSpecialArt.mdl",
        StartingXp = 2800
      };

      Anubarak = new LegendaryHero("Anub'arak")
      {
        UnitType = FourCC("Uanb")
      };

      Rivendare = new LegendaryHero("Baron Rivendare")
      {
        UnitType = FourCC("U00A"),
        StartingXp = 2800
      };

      Arthas = new LegendaryHero("Arthas Menethil")
      {
        UnitType = Constants.UNIT_UEAR_CHAMPION_OF_THE_SCOURGE_SCOURGE,
        StartingXp = 7000,
        StartingArtifacts = new List<Artifact>
        {
          new(CreateItem(Constants.ITEM_ZB07_FROSTMOURNE, Regions.ArtifactDummyInstance.Center.X, Regions.ArtifactDummyInstance.Center.Y))
        }
      };

      Utgarde = new Capital
      {
        Unit = preplacedUnitSystem.GetUnit(FourCC("h00O")),
        Capturable = true
      };

      TheFrozenThrone = new Capital
      {
        Unit = preplacedUnitSystem.GetUnit(FourCC("u000")),
        Hivemind = true,
        DeathMessage =
          "The great Lich King has been destroyed. With no central mind to command them, the forces of the Undead have gone rogue.",
        Essential = true
      };
    }
    
    public void RegisterLegends(PreplacedUnitSystem preplacedUnitSystem)
    {
      LegendaryHeroManager.Register(Kelthuzad);
      LegendaryHeroManager.Register(Anubarak);
      LegendaryHeroManager.Register(Rivendare);
      LegendaryHeroManager.Register(Arthas);
      CapitalManager.Register(TheFrozenThrone);
      TheFrozenThrone.AddProtector(preplacedUnitSystem.GetUnit(Constants.UNIT_N094_ICECROWN_OBELISK_RED, new Point(-3655, 20220)));
      TheFrozenThrone.AddProtector(preplacedUnitSystem.GetUnit(Constants.UNIT_N094_ICECROWN_OBELISK_RED, new Point(-3015, 20762)));
      TheFrozenThrone.AddProtector(preplacedUnitSystem.GetUnit(Constants.UNIT_N094_ICECROWN_OBELISK_RED, new Point(-3643, 22588)));
      TheFrozenThrone.AddProtector(preplacedUnitSystem.GetUnit(Constants.UNIT_N094_ICECROWN_OBELISK_RED, new Point(-3638, 23374)));
      CreateTrigger()
        .RegisterUnitEvent(TheFrozenThrone.Unit, EVENT_UNIT_CHANGE_OWNER)
        .AddAction(() =>
      {
        if (TheFrozenThrone.Unit.OwningPlayer() != Player(bj_PLAYER_NEUTRAL_VICTIM))
          TheFrozenThrone.Unit.SetOwner(Player(bj_PLAYER_NEUTRAL_VICTIM));
      });
      CapitalManager.Register(Utgarde);
    }
  }
}