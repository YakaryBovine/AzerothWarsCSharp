using System;
using MacroTools.ControlPointSystem;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.QuestSystem;
using static War3Api.Common;

namespace MacroTools.ObjectiveSystem.Objectives.ControlPointBased
{
  public sealed class ObjectiveControlPoint : Objective
  {
    private readonly ControlPoint _target;

    public ObjectiveControlPoint(ControlPoint target)
    {
      _target = target;
      Description = "Your team controls " + target.Name;
      TargetWidget = target.Unit;
      target.ChangedOwner += OnTargetChangeOwner;
      target.Owner.GetPlayerData().PlayerJoinedTeam += OnFactionTeamJoin;
      DisplaysPosition = true;
      Position = new(GetUnitX(_target.Unit), GetUnitY(_target.Unit));
    }

    internal override void OnAdd(Faction whichFaction)
    {
      Progress = IsPlayerOnSameTeamAsAnyEligibleFaction(_target.Unit.OwningPlayer())
        ? QuestProgress.Complete
        : QuestProgress.Incomplete;
    }

    private void OnTargetChangeOwner(object? sender, ControlPointOwnerChangeEventArgs controlPointOwnerChangeEventArgs)
    {
      Progress = IsPlayerOnSameTeamAsAnyEligibleFaction(_target.Unit.OwningPlayer())
        ? QuestProgress.Complete
        : QuestProgress.Incomplete;
    }

    private void OnFactionTeamJoin(object? sender, PlayerChangeTeamEventArgs playerChangeTeamEventArgs)
    {
      Progress = IsPlayerOnSameTeamAsAnyEligibleFaction(_target.Unit.OwningPlayer())
        ? QuestProgress.Complete
        : QuestProgress.Incomplete;
    }
  }
}