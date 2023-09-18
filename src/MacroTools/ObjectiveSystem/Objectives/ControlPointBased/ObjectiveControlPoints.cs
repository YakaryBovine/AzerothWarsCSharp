using MacroTools.ControlPointSystem;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.QuestSystem;
using System.Collections.Generic;
using System.Linq;

namespace MacroTools.ObjectiveSystem.Objectives.ControlPointBased
{
  /// <summary>
  /// Completes when all specficied control points are controlled by the same team
  /// </summary>
  public sealed class ObjectiveControlPoints : Objective
  {

    private readonly List<ControlPoint> _controlPoints;
    private readonly string _rectName;

    private int _controlPointCount;
    private int ControlPointCount
    {
      get => _controlPointCount;
      set
      {
        _controlPointCount = value;
        Description = $"Your team controls {ControlPointCount} / {_controlPoints.Count} CPs on {_rectName}";
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ObjectiveControlPoints"/> class.
    /// </summary>
    /// <param name="controlPoints">The control points that have to be owned by the same team.</param>
    /// <param name="rectName">The name of the rectangle shown in the description of the quest.</param>
    public ObjectiveControlPoints(List<ControlPoint> controlPoints, string rectName)
    {

      _controlPoints = controlPoints;
      _rectName = rectName;
      ControlPointCount = 0;
      foreach (var cp in controlPoints)
      {
        cp.ChangedOwner += OnTargetChangeOwner;
        cp.Owner.GetPlayerData().PlayerJoinedTeam += OnFactionTeamJoin;
        cp.Owner.GetPlayerData().PlayerLeftTeam += OnFactionTeamLeave;
        if (IsPlayerOnSameTeamAsAnyEligibleFaction(cp.Unit.OwningPlayer()))
          ControlPointCount++;
      }
    }

    internal override void OnAdd(Faction whichFaction)
    {
      foreach (var cp in _controlPoints)
      {
        if (IsPlayerOnSameTeamAsAnyEligibleFaction(cp.Unit.OwningPlayer()))
          ControlPointCount++;
      }

      CheckObjectiveProgress();
    }

    private void OnTargetChangeOwner(object? sender, ControlPointOwnerChangeEventArgs controlPointOwnerChangeEventArgs)
    {
      var cp = controlPointOwnerChangeEventArgs.ControlPoint;
      if (_controlPoints.Select(cp => cp.UnitType).Contains(cp.UnitType))
      {
        if (!IsPlayerOnSameTeamAsAnyEligibleFaction(controlPointOwnerChangeEventArgs.FormerOwner) && IsPlayerOnSameTeamAsAnyEligibleFaction(cp.Unit.OwningPlayer()))
          ControlPointCount++;
        else if (IsPlayerOnSameTeamAsAnyEligibleFaction(controlPointOwnerChangeEventArgs.FormerOwner) && !IsPlayerOnSameTeamAsAnyEligibleFaction(cp.Unit.OwningPlayer()))
          ControlPointCount--;
      }

      CheckObjectiveProgress();
    }

    private void OnFactionTeamJoin(object? sender, PlayerChangeTeamEventArgs playerChangeTeamEventArgs)
    {
      var faction = playerChangeTeamEventArgs.Player.GetFaction();
      if (faction != null && !EligibleFactions.Select(f => f.Name).Contains(faction.Name))
        AddEligibleFaction(faction);

      foreach (var cp in _controlPoints)
      {
        if (cp.Unit.OwningPlayer() == playerChangeTeamEventArgs.Player)
          ControlPointCount++;
      }
      CheckObjectiveProgress();
    }

    private void OnFactionTeamLeave(object? sender, PlayerChangeTeamEventArgs playerChangeTeamEventArgs)
    {
      var faction = playerChangeTeamEventArgs.Player.GetFaction();
      if (faction != null && EligibleFactions.Select(f => f.Name).Contains(faction.Name))
        EligibleFactions.Remove(faction);

      foreach (var cp in _controlPoints)
      {
        if (cp.Unit.OwningPlayer() == playerChangeTeamEventArgs.Player)
          ControlPointCount--;         
      }
      CheckObjectiveProgress();
    }

    private void CheckObjectiveProgress()
    {
      if (ControlPointCount == _controlPoints.Count)
        Progress = QuestProgress.Complete;
      else
        Progress = QuestProgress.Incomplete;
    }
  }
}