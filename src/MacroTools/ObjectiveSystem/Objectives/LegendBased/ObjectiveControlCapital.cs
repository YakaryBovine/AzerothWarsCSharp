using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.QuestSystem;
using static War3Api.Common;

namespace MacroTools.ObjectiveSystem.Objectives.LegendBased
{
  /// <summary>
  /// Completed when your team controls a particular <see cref="Capital"/>.
  /// </summary>
  public sealed class ObjectiveControlCapital : Objective
  {
    private readonly bool _canFail;
    private readonly Legend _target;

    /// <summary>
    /// Initializes a new instance of the <see cref="ObjectiveControlCapital"/> class.
    /// </summary>
    public ObjectiveControlCapital(Capital target, bool canFail)
    {
      _target = target;
      Description = $"Your team controls {target.Name}";
      _canFail = canFail;
      if (target.Unit != null)
      {
        TargetWidget = target.Unit;
      }

      DisplaysPosition = true;
      target.ChangedOwner += (_, _) => { RecalculateProgress(); };
      target.UnitChanged += (_, _) => { RecalculateProgress(); };

      CreateTrigger()
        .RegisterUnitEvent(target.Unit, EVENT_UNIT_DEATH)
        .AddAction(() => { if (_canFail) { Progress = QuestProgress.Failed; } });
      
      Position = new(GetUnitX(_target.Unit), GetUnitY(_target.Unit));
    }

    internal override void OnAdd(Faction whichFaction)
    {
      if (_target.Unit != null && IsPlayerOnSameTeamAsAnyEligibleFaction(_target.Unit.OwningPlayer()))
      {
        Progress = QuestProgress.Complete;
      }
    }

    private void RecalculateProgress()
    {
      if (_target.Unit != null && IsPlayerOnSameTeamAsAnyEligibleFaction(_target.Unit.OwningPlayer()))
        Progress = QuestProgress.Complete;
      else
        Progress = _canFail ? QuestProgress.Failed : QuestProgress.Incomplete;
    }
  }
}