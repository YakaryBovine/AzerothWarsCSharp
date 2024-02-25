using MacroTools;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using WarcraftLegacies.Source.Factions;
using static War3Api.Common;

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
      SetupPlayer(Player(0), new Frostwolf(_preplacedUnitSystem, _allLegendSetup, _artifactSetup), TeamSetup.Horde);
      SetupPlayer(Player(1), new Stormwind(_preplacedUnitSystem, _allLegendSetup, _artifactSetup), TeamSetup.SouthAlliance);
      SetupPlayer(Player(2), new Quelthalas(_preplacedUnitSystem, _allLegendSetup), TeamSetup.NorthAlliance);
      SetupPlayer(Player(3), new Scourge(_preplacedUnitSystem, _allLegendSetup, _artifactSetup), TeamSetup.Legion);
      SetupPlayer(Player(4), new Ironforge(_preplacedUnitSystem, _allLegendSetup), TeamSetup.SouthAlliance);
      SetupPlayer(Player(5), new Warsong(_preplacedUnitSystem, _allLegendSetup), TeamSetup.Horde);
      SetupPlayer(Player(6), new FelHorde(_preplacedUnitSystem, _allLegendSetup), TeamSetup.Outland);
      Player(7).SetTeam(TeamSetup.NorthAlliance);
      Player(8).SetTeam(TeamSetup.Horde);
      SetupPlayer(Player(9), new Lordaeron(_preplacedUnitSystem, _allLegendSetup, _artifactSetup), TeamSetup.NorthAlliance);
      SetupPlayer(Player(11), new Druids(_preplacedUnitSystem, _allLegendSetup, _artifactSetup), TeamSetup.NightElves);
      SetupPlayer(Player(13), new Draenei(_preplacedUnitSystem, _allLegendSetup), TeamSetup.NightElves);
      SetupPlayer(Player(18), new Sentinels(_preplacedUnitSystem, _allLegendSetup, _artifactSetup), TeamSetup.NightElves);
      Player(15).SetTeam(TeamSetup.Outland);
      SetupPlayer(Player(22), new Kultiras(_preplacedUnitSystem, _allLegendSetup), TeamSetup.SouthAlliance);
      SetupPlayer(Player(23), new Legion(_preplacedUnitSystem, _allLegendSetup), TeamSetup.Legion);
    }

    private static void SetupPlayer(player player, Faction faction, Team northAlliance)
    {
      FactionManager.Register(faction);
      player.SetFaction(faction);
      player.SetTeam(northAlliance);
    }
  }
}