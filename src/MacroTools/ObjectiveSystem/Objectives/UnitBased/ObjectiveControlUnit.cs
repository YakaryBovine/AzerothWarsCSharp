using MacroTools.Extensions;
using MacroTools.QuestSystem;
using WCSharp.Events;
using static War3Api.Common;

namespace MacroTools.ObjectiveSystem.Objectives.UnitBased
{
  public sealed class ObjectiveControlUnit : Objective
  {
    private readonly unit _target;

    public ObjectiveControlUnit(unit target)
    {
      Description = "Your team controls " + GetUnitName(target);
      TargetWidget = target;
      _target = target;
      DisplaysPosition = true;

      PlayerUnitEvents.Register(UnitTypeEvent.ChangesOwner, OnUnitChangeOwner);
      Position = new(GetUnitX(_target), GetUnitY(_target));
    }

    private void OnUnitChangeOwner()
    {
      if (IsPlayerOnSameTeamAsAnyEligibleFaction(_target.OwningPlayer()))
        Progress = QuestProgress.Complete;
    }
  }
}