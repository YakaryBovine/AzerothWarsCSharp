using static War3Api.Common;

namespace MacroTools.CommandSystem
{
  /// <summary>
  /// A function that a player can execute via the chat, which is only enabled in testing scenarios.
  /// </summary>
  public abstract class Command
  {
    /// <summary>
    /// The command that a player can execute to run the <see cref="CommandSystem.Command"/>.
    /// </summary>
    public abstract string CommandText { get; }
    
    /// <summary>
    /// Set if the command needs to be an exact match or not, commands with 0 params should be exact<see cref="CommandSystem.Command"/>.
    /// </summary>
    public abstract bool Exact { get; }

    /// <summary>
    /// The minimum number of parameters that must be supplied to this <see cref="CommandSystem.Command"/> command.
    /// </summary>
    public abstract int MinimumParameterCount { get; }

    /// <summary>
    /// Determines the purpose of the command and when it can be used.
    /// </summary>
    public abstract CommandType Type { get; }
    
    /// <summary>
    /// Describes to the player what the commmand will do.
    /// </summary>
    public abstract string Description { get; }

    /// <summary>
    /// What happens when the <see cref="CommandSystem.Command"/> gets executed by a player.
    /// </summary>
    /// <param name="commandUser">The player typing the <see cref="CommandSystem.Command"/> command in.</param>
    /// <param name="parameters">The parameters specified by the player entering the sheet, seperated by spaces.</param>
    /// <returns>A message describing what the <see cref="CommandSystem.Command"/> is doing to the executing player.</returns>
    public abstract string Execute(player commandUser, params string[] parameters);

    /// <summary>
    /// Fired when the <see cref="Command"/> is registered.
    /// </summary>
    public virtual void OnRegister()
    {
    }
  }
}