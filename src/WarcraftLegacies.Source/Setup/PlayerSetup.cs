using System;
using MacroTools;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using WarcraftLegacies.Source.Factions;

namespace WarcraftLegacies.Source.Setup
{
  public sealed class PlayerSetup
  {
    private readonly PreplacedUnitSystem _preplacedUnitSystem;
    private readonly AllLegendSetup _allLegendSetup;
    private readonly ArtifactSetup _artifactSetup;

    public PlayerSetup(PreplacedUnitSystem preplacedUnitSystem, AllLegendSetup allLegendSetup, ArtifactSetup artifactSetup)
    {
      _preplacedUnitSystem = preplacedUnitSystem;
      _allLegendSetup = allLegendSetup;
      _artifactSetup = artifactSetup;
    }
    
    public void Setup()
    {
      SetupPlayer(Player(0), new Frostwolf(_preplacedUnitSystem, _allLegendSetup, _artifactSetup));
      SetupPlayer(Player(1), new Stormwind(_preplacedUnitSystem, _allLegendSetup, _artifactSetup));
      SetupPlayer(Player(2), new Quelthalas(_preplacedUnitSystem, _allLegendSetup));
      SetupPlayer(Player(3), new Scourge(_preplacedUnitSystem, _allLegendSetup, _artifactSetup));
      SetupPlayer(Player(4), new Ironforge(_preplacedUnitSystem, _allLegendSetup, _artifactSetup));
      SetupPlayer(Player(6), new FelHorde(_preplacedUnitSystem, _allLegendSetup, _artifactSetup));
      Player(7).SetTeam(TeamSetup.NorthAlliance);
      Player(8).SetTeam(TeamSetup.OldGods);
      SetupPlayer(Player(9), new Lordaeron(_preplacedUnitSystem, _allLegendSetup, _artifactSetup));
      SetupPlayer(Player(11), new Druids(_preplacedUnitSystem, _allLegendSetup, _artifactSetup));
      SetupPlayer(Player(12), new BlackEmpire(_preplacedUnitSystem, _allLegendSetup));
      SetupPlayer(Player(16), new Ahnqiraj(_preplacedUnitSystem, _allLegendSetup));
      Player(18).SetTeam(TeamSetup.Kalimdor);
      Player(15).SetTeam(TeamSetup.Outland);
      SetupPlayer(Player(22), new Kultiras(_preplacedUnitSystem, _allLegendSetup, _artifactSetup));
      SetupPlayer(Player(23), new Legion(_preplacedUnitSystem, _allLegendSetup));
    }
    

    private static void SetupPlayer(player player, Faction faction)
    {
      var traditionalTeam = faction.TraditionalTeam;
      if (traditionalTeam != null)
        player.SetTeam(traditionalTeam);
      else
        throw new InvalidOperationException($"{GetPlayerName(player)}'s {nameof(Faction)} doesn't have a {nameof(Faction.TraditionalTeam)}.");
      player.SetFaction(faction);
      FactionManager.Register(faction);
    }
  }
}