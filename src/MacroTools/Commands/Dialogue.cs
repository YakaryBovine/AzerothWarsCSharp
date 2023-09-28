using MacroTools.CommandSystem;
using MacroTools.Extensions;
using static War3Api.Common;

namespace MacroTools.Commands
{
  /// <summary>
  /// A <see cref="CommandSystem.Command"/> That turns dialogue sound on or off.
  /// </summary>
  public sealed class Dialogue : Command
  {
    /// <inheritdoc />
    public override string CommandText => "dialogue";
    
    /// <inheritdoc />
    public override bool Exact => false;
  
    /// <inheritdoc />
    public override int MinimumParameterCount => 1;

    /// <inheritdoc />
    public override CommandType Type => CommandType.Normal;

    /// <inheritdoc />
    public override string Description => "Turns dialogue sound on or off.";

    /// <inheritdoc />
    public override string Execute(player commandUser, params string[] parameters)
    {
      var dialogue = parameters[0];
      if (!bool.TryParse(dialogue, out var dialogueBool))
        return "You must specify either true or false as the first parameter.";
      
      PlayerData.ByHandle(commandUser).UpdatePlayerSetting("PlayDialogue", dialogueBool);
      return $"Setting play dialogue option to {dialogueBool}.";
    }
  }
}