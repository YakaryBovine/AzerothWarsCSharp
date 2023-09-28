using MacroTools.Extensions;
using MacroTools.HintSystem;
using WarcraftLegacies.Source.GameLogic.GameEnd;
using WarcraftLegacies.Source.Setup;
using static War3Api.Common;

namespace WarcraftLegacies.Source.GameLogic
{
  /// <summary>
  /// Players can vote as to whether or not all players can freely ally.
  /// </summary>
  public static class OpenAllianceVote
  {
    private static readonly dialog? VoteDialogue = DialogCreate();
    private static readonly button? WarButton = DialogAddButton(VoteDialogue, "Great War (9v7)", 0);
    private static readonly button? NoButton = DialogAddButton(VoteDialogue, "Closed Alliance", 0);
    private static readonly button? YesButton = DialogAddButton(VoteDialogue, "Open Alliance", 0);
    private static readonly trigger? YesTrigger = CreateTrigger();
    private static readonly trigger? NoTrigger = CreateTrigger();
    private static readonly trigger? WarTrigger = CreateTrigger();
    private static int _voteCount;
    private static int _warCount;

    /// <summary>
    /// Whether or not all players can freely ally.
    /// </summary>
    public static bool AreAlliancesOpen { get; private set; }

    /// <summary>
    /// Sets up <see cref="OpenAllianceVote"/>.
    /// </summary>
    public static void Setup()
    {
      DialogSetMessage(VoteDialogue, "Vote Game Mode");
      var timer = CreateTimer();
      TimerStart(timer, 49, false, StartVote);
      var concludeTimer = CreateTimer();
      TimerStart(concludeTimer, 59, false, ConcludeVote);
    }
    
    private static void ConcludeVote()
    {
      foreach (var player in WCSharp.Shared.Util.EnumeratePlayers()) 
        DialogDisplay(player, VoteDialogue, false);
      DialogClear(VoteDialogue);
      DialogDestroy(VoteDialogue);
      DestroyTrigger(YesTrigger);
      DestroyTrigger(NoTrigger);
      DestroyTrigger(WarTrigger);

      if (_warCount >= 12)
      {
        DisplayTextToPlayer(GetLocalPlayer(), 0, 0, "The Great war begins!");
        Hint.Register(
          new Hint("You can change alliances by using the commands -invite, -uninvite, -join, and -unally."));
        AreAlliancesOpen = true;
        ControlPointVictory.CpsVictory = 1000;
        ControlPointVictory.CpsWarning = 1000;

        Player(3).SetTeam(TeamSetup.Legion);
        Player(23).SetTeam(TeamSetup.Legion);

        Player(6).SetTeam(TeamSetup.Legion);
        Player(15).SetTeam(TeamSetup.Legion);

        Player(0).SetTeam(TeamSetup.Legion);
        Player(5).SetTeam(TeamSetup.Legion);
        Player(8).SetTeam(TeamSetup.Legion);

        Player(9).SetTeam(TeamSetup.Alliance);
        Player(2).SetTeam(TeamSetup.Alliance);
        Player(7).SetTeam(TeamSetup.Alliance);

        Player(4).SetTeam(TeamSetup.Alliance);
        Player(1).SetTeam(TeamSetup.Alliance);
        Player(22).SetTeam(TeamSetup.Alliance);

        Player(11).SetTeam(TeamSetup.Alliance);
        Player(13).SetTeam(TeamSetup.Alliance);
        Player(18).SetTeam(TeamSetup.Alliance);
      }
      
      else if (_voteCount > 0)
      {
        DisplayTextToPlayer(GetLocalPlayer(), 0, 0, "Alliances are open!");
        Hint.Register(
          new Hint("You can change alliances by using the commands -invite, -uninvite, -join, and -unally."));
        AreAlliancesOpen = true;
      }
      else
      {
        DisplayTextToPlayer(GetLocalPlayer(), 0, 0,
          "Open alliances are disabled; only Quests can change your alliance.");
        Hint.Register(new Hint("You can leave your current alliances by typing -unally, but you won't be able to join a new one."));
        AreAlliancesOpen = false;
      }

      DestroyTimer(GetExpiredTimer());
    }

    private static void StartVote()
    {
      foreach (var player in WCSharp.Shared.Util.EnumeratePlayers())
      {
        DialogDisplay(player, VoteDialogue, true);
      }

      var yesTrigger = CreateTrigger();
      var noTrigger = CreateTrigger();
      var warTrigger = CreateTrigger();
      TriggerRegisterDialogButtonEvent(warTrigger, WarButton);
      TriggerRegisterDialogButtonEvent(noTrigger, NoButton);
      TriggerRegisterDialogButtonEvent(yesTrigger, YesButton);
      TriggerAddAction(noTrigger, () => { _voteCount--; });
      TriggerAddAction(yesTrigger, () => { _voteCount++; });
      TriggerAddAction(warTrigger, () => { _warCount++; });
      DestroyTimer(GetExpiredTimer());
    }
  }
}