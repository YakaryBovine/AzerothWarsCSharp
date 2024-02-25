using MacroTools.GameModes;
using MacroTools.HintSystem;
using WarcraftLegacies.Source.Commands;
using WarcraftLegacies.Source.GameLogic.GameEnd;
using static War3Api.Common;

namespace WarcraftLegacies.Source.GameModes
{
  public sealed class ClosedAlliance : IGameMode
  {
    /// <inheritdoc />
    public string Name => "Closed Alliance";
    
    /// <inheritdoc />
    public void OnChoose()
    {
      foreach (var player in WCSharp.Shared.Util.EnumeratePlayers())
        DisplayTextToPlayer(player, 0, 0, "Open alliances are disabled; only Quests can change your alliance.");
      
      Hint.Register(new Hint("You can leave your current alliances by typing -unally, but you won't be able to join a new one."));
      SetupControlPointVictory();
      UnallyCommand.Setup();
    }

    private static void SetupControlPointVictory()
    {
      ControlPointVictory.Setup();
      Hint.Register(new Hint($"Win the game by capturing {ControlPointVictory.CpsVictory} Control Points."));
    }
  }
}