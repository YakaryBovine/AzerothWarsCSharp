using System;
using System.Collections.Generic;
using System.Linq;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.ObjectiveSystem;
using MacroTools.QuestSystem;
using static War3Api.Common;

namespace MacroTools.DialogueSystem
{
  /// <summary>
  /// Plays a particular <see cref="IHasPlayableDialogue"/> when conditions are met.
  /// </summary>
  public sealed class TriggeredDialogue
  {
    /// <summary>
    /// Fired when the <see cref="Dialogue"/> plays.
    /// </summary>
    public event EventHandler<TriggeredDialogue>? Completed;
    
    internal List<Objective> Objectives { get; }
    
    private readonly IEnumerable<Faction>? _audience;
    private readonly IHasPlayableDialogue _playableDialogue;
    private bool _inactive;

    private QuestProgress Progress
    {
      set
      {
        switch (value)
        {
          case QuestProgress.Complete:
            Complete();
            break;
          case QuestProgress.Failed:
            Fail();
            break;
          default:
            throw new ArgumentOutOfRangeException(nameof(value), value, null);
        }
      }
    }

    private void Complete()
    {
      //Play audio to everyone if audience was not set.
      var audience = _audience != null
        ? _audience.Select(x => x.Player)
        : WCSharp.Shared.Util.EnumeratePlayers(PLAYER_SLOT_STATE_PLAYING, MAP_CONTROL_USER);
      
      foreach (var player in audience) 
        player?.QueueDialogue(_playableDialogue);

      _inactive = true;
      Completed?.Invoke(this, this);
    }
    
    private void Fail()
    {
      _inactive = true;
      Completed?.Invoke(this, this);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TriggeredDialogue"/> class.
    /// </summary>
    /// <param name="playableDialogue">The dialogue that will be played when the conditions are met.</param>
    /// <param name="objectives">When these are completed, the dialogue plays.</param>
    /// <param name="audience">A list of factions that can hear the dialogue being played.</param>
    public TriggeredDialogue(IHasPlayableDialogue playableDialogue, IEnumerable<Faction>? audience, 
      IEnumerable<Objective> objectives)
    {
      _playableDialogue = playableDialogue;
      _audience = audience;
      Objectives = objectives.ToList();
    }
    
    internal void OnObjectiveCompleted(object? sender, Objective completedObjective)
    {
      if (_inactive)
        return;

      var allComplete = true;
      var anyFailed = false;

      foreach (var objective in Objectives)
      {
        if (objective.Progress == QuestProgress.Complete) 
          continue;
        allComplete = false;
        if (objective.Progress == QuestProgress.Failed)
          anyFailed = true;
      }

      if (allComplete)
        Progress = QuestProgress.Complete;
      else if (anyFailed)
        Progress = QuestProgress.Failed;
    }
  }
}