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
    private readonly bool _failOnControlLoss;
    private readonly Legend _target;

    /// <summary>
    /// Initializes a new instance of the <see cref="ObjectiveControlCapital"/> class.
    /// </summary>
    /// <param name="target">The <see cref="Capital"/> that needs to be controlled to complete the objective.</param>
    /// <param name="failOnControlLoss">If true, the objective will fail when control of the capital is lost for the tirst time.</param>
    public ObjectiveControlCapital(Capital target, bool failOnControlLoss)
    {
      _target = target;
      Description = $"You control {target.Name}";
      _failOnControlLoss = failOnControlLoss;
      if (target.Unit != null)
      {
        TargetWidget = target.Unit;
      }

      DisplaysPosition = true;
      target.ChangedOwner += (_, _) => { RecalculateProgress(); };
      target.UnitChanged += (_, _) => { RecalculateProgress(); };

      CreateTrigger()
        .RegisterUnitEvent(target.Unit, EVENT_UNIT_DEATH)
        .AddAction(() => { Progress = QuestProgress.Failed; });

      Position = new(GetUnitX(_target.Unit), GetUnitY(_target.Unit));
    }

    public override void OnAdd(Faction whichFaction)
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
        Progress = _failOnControlLoss ? QuestProgress.Failed : QuestProgress.Incomplete;
    }
  }
}