using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.QuestSystem;
using static War3Api.Common;

namespace MacroTools.ObjectiveSystem.Objectives.LegendBased
{
  /// <summary>
  /// Retain control of a particular <see cref="Legend"/>.
  /// </summary>
  public sealed class ObjectiveControlLegend : Objective
  {
    private readonly bool _canFail;
    private readonly Legend _target;

    /// <summary>
    /// Initializes a new instance of the <see cref="ObjectiveControlLegend"/> class.
    /// </summary>
    public ObjectiveControlLegend(LegendaryHero target, bool canFail)
    {
      _target = target;
      Description = $"You control {target.Name}";
      _canFail = canFail;
      if (target.Unit != null) TargetWidget = target.Unit;

      DisplaysPosition = GetOwningPlayer(target.Unit) == Player(PLAYER_NEUTRAL_AGGRESSIVE);
      target.ChangedOwner += OnTargetChangeOwner;
      target.PermanentlyDied += OnTargetPermaDeath;
      Position = new(GetUnitX(_target.Unit), GetUnitY(_target.Unit));
    }

    internal override void OnAdd(Faction whichFaction)
    {
      if (_target.Unit != null && IsPlayerOnSameTeamAsAnyEligibleFaction(_target.Unit.OwningPlayer()))
        Progress = QuestProgress.Complete;
    }

    private void OnTargetChangeOwner(object? sender, LegendChangeOwnerEventArgs legendChangeOwnerEventArgs)
    {
      if (_target.Unit != null && IsPlayerOnSameTeamAsAnyEligibleFaction(_target.Unit.OwningPlayer()))
        Progress = QuestProgress.Complete;
      else
        Progress = _canFail ? QuestProgress.Failed : QuestProgress.Incomplete;
    }

    private void OnTargetPermaDeath(object? sender, Legend legend)
    {
      if (_canFail) 
        Progress = QuestProgress.Failed;
    }
  }
}