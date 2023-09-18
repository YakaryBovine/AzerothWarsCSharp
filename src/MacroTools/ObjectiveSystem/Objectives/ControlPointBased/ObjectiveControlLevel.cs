using MacroTools.ControlPointSystem;
using MacroTools.Extensions;
using MacroTools.QuestSystem;
using static War3Api.Common;

namespace MacroTools.ObjectiveSystem.Objectives.ControlPointBased
{
  /// <summary>
  /// Completed when a <see cref="ControlPoint"/> has a certain <see cref="ControlPoint.ControlLevel"/>.
  /// </summary>
  public sealed class ObjectiveControlLevel : Objective
  {
    private readonly ControlPoint _target;
    private readonly int _requiredLevel;

    /// <summary>
    /// Initializes a new instance of the <see cref="ObjectiveControlLevel"/> class.
    /// </summary>
    /// <param name="target">The <see cref="ControlPoint"/> that must reach the specified level.</param>
    /// <param name="requiredLevel">The level the <see cref="ControlPoint"/> must reach.</param>
    public ObjectiveControlLevel(ControlPoint target, int requiredLevel)
    {
      _target = target;
      _requiredLevel = requiredLevel;
      target.ChangedOwner += (_, _) => Refresh();
      target.Owner.GetPlayerData().PlayerJoinedTeam += (_, _) => Refresh();
      _target.ControlLevelChanged += (_, _) => Refresh();
      TargetWidget = target.Unit;
      DisplaysPosition = true;
      Position = new(GetUnitX(_target.Unit), GetUnitY(_target.Unit));
    }

    internal override void OnAdd(FactionSystem.Faction whichFaction)
    {
      RefreshDescription();
      RefreshProgress();
    }

    private void Refresh()
    {
      RefreshDescription();
      RefreshProgress();
    }

    private void RefreshDescription()
    {
      Description = IsPlayerOnSameTeamAsAnyEligibleFaction(_target.Owner)
        ? $"{_target.Name} is Control Level {_requiredLevel} or higher ({(int)_target.ControlLevel}/{_requiredLevel})"
        : $"{_target.Name} is Control Level {_requiredLevel} or higher";
    }

    private void RefreshProgress()
    {
      var isOnSameTeam = IsPlayerOnSameTeamAsAnyEligibleFaction(_target.Owner);

      Progress = _target.ControlLevel >= _requiredLevel && isOnSameTeam
        ? QuestProgress.Complete
        : QuestProgress.Incomplete;
    }
  }
}