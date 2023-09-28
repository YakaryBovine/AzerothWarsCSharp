using MacroTools.FactionSystem;
using MacroTools.QuestSystem;
using System.Collections.Generic;
using static War3Api.Common;

namespace MacroTools.ObjectiveSystem.Objectives.TimeBased
{
  /// <summary>
  ///   Starts completed, then fails when the specified amount of time has elapsed.
  /// </summary>
  public sealed class ObjectiveExpire : Objective
  {
    private readonly timer _expirationTimer;
    private readonly timer _warningTimer;
    private readonly string _questName;
    private readonly List<Faction> _assignedFactions = new();
    
    /// <param name="duration">The time after which the objective will fail</param>
    /// <param name="questName">The name of the quest this objective belongs to.</param>
    public ObjectiveExpire(int duration, string questName)
    {
      var turn = GameTime.ConvertGameTimeToTurn(duration);
      Description = $"Complete this quest before turn {turn} has passed";
      _expirationTimer = CreateTimer();
      _warningTimer = CreateTimer();
      TimerStart(_expirationTimer, duration, false, OnExpire);
      TimerStart(_warningTimer, duration - 120, false, OnWarning);
      _questName = questName;
    }

    internal override void OnAdd(Faction whichFaction)
    {
      _assignedFactions.Add(whichFaction);
      Progress = QuestProgress.Complete;
    }

    private void OnExpire()
    {
      DestroyTimer(_expirationTimer);
      Progress = QuestProgress.Failed;
    }

    private void OnWarning()
    {
      DestroyTimer(_warningTimer);
      foreach (var assignedFaction in _assignedFactions)
      {
        if (Progress != QuestProgress.Complete)
          DisplayTextToPlayer(assignedFaction.Player, 0, 0,
            $"\n|c00FF7F00WARNING|r - Quest {_questName} will expire in 2 minutes.");
      }
    }
  }
}