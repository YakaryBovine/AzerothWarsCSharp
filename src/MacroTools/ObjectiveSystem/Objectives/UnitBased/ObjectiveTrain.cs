using MacroTools.QuestSystem;
using WCSharp.Events;
using static War3Api.Common;

namespace MacroTools.ObjectiveSystem.Objectives.UnitBased
{
  /// <summary>
  ///   Completes when the holder has trained a specific number of a specific unit type.
  /// </summary>
  public sealed class ObjectiveTrain : Objective
  {
    private readonly int _objectId;
    private readonly int _targetTrainCount;
    private readonly int _trainFromId;
    private int _currentTrainCount;

    /// <summary>
    ///   Completes when the holder has trained a specific number of a specific unit type.
    /// </summary>
    /// <param name="objectId">The unit type that has to be trained.</param>
    /// <param name="trainFromId">The unit type of a building from which the unit can be trained.</param>
    /// <param name="targetTrainCount">How many of the unit type need to be trained.</param>
    public ObjectiveTrain(int objectId, int trainFromId, int targetTrainCount)
    {
      _objectId = objectId;
      _trainFromId = trainFromId;
      _targetTrainCount = targetTrainCount;
      CurrentTrainCount = 0;
      PlayerUnitEvents.Register(UnitTypeEvent.FinishesTraining, OnTrain);
    }

    private int CurrentTrainCount
    {
      set
      {
        _currentTrainCount = value;
        Description =
          $"Train {GetObjectName(_objectId)}s from the {GetObjectName(_trainFromId)} ({_currentTrainCount}/{_targetTrainCount})";
      }
    }

    private void OnTrain()
    {
      if (GetUnitTypeId(GetTrainedUnit()) != _objectId) return;

      if (!ProgressLocked && EligibleFactions.Contains(GetOwningPlayer(GetTrainedUnit())))
      {
        CurrentTrainCount = _currentTrainCount + 1;
        if (_currentTrainCount == _targetTrainCount)
          Progress = QuestProgress.Complete;
      }
    }
  }
}