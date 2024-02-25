using MacroTools.Extensions;
using MacroTools.FactionSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Commands
{
  /// <summary>
  ///   Joins the specified <see cref="Team" />. Only works if an invite has first been extended.
  /// </summary>
  public static class JoinCommand
  {
    private const string Command = "-join ";

    private static void Actions()
    {
      var enteredString = GetEventPlayerChatString();
      var triggerPlayer = GetTriggerPlayer();

      if (SubString(enteredString, 0, StringLength(Command)) != Command) return;
      var content = SubString(enteredString, StringLength(Command), StringLength(enteredString));
      content = StringCase(content, false);

      if (FactionManager.TryGetTeamByName(content, out var targetTeam))
      {
        if (targetTeam.IsPlayerInvited(triggerPlayer))
        {
          triggerPlayer.SetTeam(targetTeam);
          DisplayTextToPlayer(triggerPlayer, 0, 0, $"You have joined {targetTeam.Name}.");
          targetTeam.DisplayText($"{triggerPlayer?.GetFaction()?.ColoredName} has joined the {targetTeam.Name}.");
        }
        else
          DisplayTextToPlayer(triggerPlayer, 0, 0, $"You have not been invited to join {targetTeam.Name}.");
      }
      else
        DisplayTextToPlayer(triggerPlayer, 0, 0, $"There is no Team with the name {content}.");
    }

    public static void Setup()
    {
      var trig = CreateTrigger();
      foreach (var player in WCSharp.Shared.Util.EnumeratePlayers()) 
        TriggerRegisterPlayerChatEvent(trig, player, Command, false);
      TriggerAddAction(trig, Actions);
    }
  }
}