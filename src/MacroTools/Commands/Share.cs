using MacroTools.CommandSystem;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using static War3Api.Common;

namespace MacroTools.Commands
{
  /// <summary>Share control of your units with another player.</summary>
  public sealed class Share : Command
  {
    /// <inheritdoc />
    public override string CommandText => "share";

    /// <inheritdoc />
    public override bool Exact => false;

    /// <inheritdoc />
    public override int MinimumParameterCount => 1;

    /// <inheritdoc />
    public override CommandType Type => CommandType.Normal;

    /// <inheritdoc />
    public override string Description => "Share control of your units with another faction.";

    /// <inheritdoc />
    public override string Execute(player cheater, params string[] parameters)
    {
      var targetFaction = FactionManager.GetFromName(parameters[0]);
      if (targetFaction == null) 
        return $"There is no faction named {parameters[0]}.";

      if (targetFaction.Player == null)
        return $"There is nobody playing the {targetFaction.Name} faction.";

      var cheaterTeam = cheater.GetTeam();
      if (cheaterTeam != targetFaction.Player.GetTeam())
        return $"{targetFaction.Name} isn't on your team, so you can't share control with them.";
      
      SetPlayerAlliance(cheater, targetFaction.Player, ALLIANCE_SHARED_CONTROL, true);
      
      return $"Shared control with {targetFaction.Name}.";
    }
  }
}