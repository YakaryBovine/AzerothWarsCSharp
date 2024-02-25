using MacroTools.CommandSystem;
using MacroTools.Extensions;
using static War3Api.Common;

namespace MacroTools.Commands
{
  public sealed class Settings : Command
  {
    /// <inheritdoc />
    public override string CommandText => "settings";
    
    /// <inheritdoc />
    public override bool Exact => false;
  
    /// <inheritdoc />
    public override int MinimumParameterCount => 0;

    /// <inheritdoc />
    public override CommandType Type => CommandType.Normal;

    /// <inheritdoc />
    public override string Description => "Shows your current settings.";

    /// <inheritdoc />
    public override string Execute(player commandUser, params string[] parameters)
    {
      var playerSettings = commandUser.GetPlayerSettings();
      return @"Current settings:
Camera distance: " + playerSettings.CamDistance + @"

Show quest text: " + playerSettings.ShowQuestText + @"

Play dialogue: " + playerSettings.PlayDialogue + @"

Show captions: " + playerSettings.ShowCaptions;
    }
  }
}