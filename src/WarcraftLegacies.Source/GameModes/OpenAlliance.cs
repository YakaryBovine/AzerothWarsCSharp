using MacroTools.GameModes;
using MacroTools.HintSystem;
using WarcraftLegacies.Source.Commands;
using WarcraftLegacies.Source.GameLogic.GameEnd;
using static War3Api.Common;

namespace WarcraftLegacies.Source.GameModes
{
  public sealed class OpenAlliance : IGameMode
  {
    /// <inheritdoc />
    public string Name => "Open Alliance";

    /// <inheritdoc />
    public void OnChoose()
    {
      foreach (var player in WCSharp.Shared.Util.EnumeratePlayers())
        DisplayTextToPlayer(player, 0, 0, "Alliances are open!");
      
      SetupAllianceCommands();
      SetupControlPointVictory();
    }
    
    private static void SetupAllianceCommands()
    {
      Hint.Register(new Hint("You can change alliances by using the commands -invite, -uninvite, -join, and -unally."));
      InviteCommand.Setup();
      JoinCommand.Setup();
      UnallyCommand.Setup();
      UninviteCommand.Setup();
    }

    private static void SetupControlPointVictory()
    {
      ControlPointVictory.Setup();
      Hint.Register(new Hint($"Win the game by capturing {ControlPointVictory.CpsVictory} Control Points."));
    }
  }
}