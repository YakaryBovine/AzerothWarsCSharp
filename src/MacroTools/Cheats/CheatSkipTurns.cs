using MacroTools.CommandSystem;
using static War3Api.Common;

namespace MacroTools.Cheats
{
  public sealed class CheatSkipTurns : Command
  {
    /// <inheritdoc />
    public override string CommandText => "skipturns";

    /// <inheritdoc />
    public override bool Exact => false;

    /// <inheritdoc />
    public override int MinimumParameterCount => 0;

    /// <inheritdoc />
    public override CommandType Type => CommandType.Cheat;

    /// <inheritdoc />
    public override string Description => "Skips the game forward a number of turns.";

    /// <inheritdoc />
    public override string Execute(player cheater, params string[] parameters)
    {
      if (!int.TryParse(parameters[0], out var turnSkip))
        return "You must specify a whole number as the first parameter.";

      GameTime.SkipTurns(turnSkip);
      return $"Skipping forward {turnSkip} turns.";
    }
  }
}