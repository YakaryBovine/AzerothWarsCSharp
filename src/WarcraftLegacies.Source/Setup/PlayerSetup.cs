using MacroTools.Extensions;
using WarcraftLegacies.Source.Setup.FactionSetup;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Setup
{
  public static class PlayerSetup
  {
    public static void Setup()
    {
      Player(0).SetFaction(FrostwolfSetup.Frostwolf);
      Player(0).SetTeam(TeamSetup.Horde);

      Player(1).SetFaction(LordaeronSetup.Lordaeron);
      Player(1).SetTeam(TeamSetup.NorthAlliance);

      Player(2).SetFaction(QuelthalasSetup.Quelthalas);
      Player(2).SetTeam(TeamSetup.NorthAlliance);
      
      Player(3).SetFaction(ScourgeSetup.Scourge);
      Player(3).SetTeam(TeamSetup.Legion);

      Player(4).SetFaction(IronforgeSetup.Ironforge);
      Player(4).SetTeam(TeamSetup.SouthAlliance);

      Player(5).SetFaction(WarsongSetup.WarsongClan);
      Player(5).SetTeam(TeamSetup.Horde);

      Player(6).SetFaction(FelHordeSetup.FelHorde);
      Player(6).SetTeam(TeamSetup.Outland);

      Player(7).SetTeam(TeamSetup.NorthAlliance);
      
      Player(8).SetTeam(TeamSetup.Horde);
      
      Player(10).SetFaction(StormwindSetup.Stormwind);
      Player(10).SetTeam(TeamSetup.SouthAlliance);

      Player(11).SetFaction(DruidsSetup.Druids);
      Player(11).SetTeam(TeamSetup.NightElves);

      Player(13).SetFaction(DraeneiSetup.Draenei);
      Player(13).SetTeam(TeamSetup.NightElves);

      Player(18).SetFaction(SentinelsSetup.Sentinels);
      Player(18).SetTeam(TeamSetup.NightElves);
      
      Player(15).SetTeam(TeamSetup.Outland);

      Player(22).SetFaction(KultirasSetup.Kultiras);
      Player(22).SetTeam(TeamSetup.SouthAlliance);

      Player(23).SetFaction(LegionSetup.Legion);
      Player(23).SetTeam(TeamSetup.Legion);
    }
  }
}