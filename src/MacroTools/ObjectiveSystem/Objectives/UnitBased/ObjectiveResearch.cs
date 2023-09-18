using System;
using MacroTools.QuestSystem;
using WCSharp.Events;
using static War3Api.Common;

namespace MacroTools.ObjectiveSystem.Objectives.UnitBased
{
  /// <summary>An objective that involves researching a particular research.</summary>
  public sealed class ObjectiveResearch : Objective
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="ObjectiveResearch"/> class.
    /// </summary>
    /// <param name="researchId">The research one has to complete to complete the objective.</param>
    /// <param name="structureId">A strucutre that can research the research.</param>
    /// <param name="structureIsProperNoun">If true, the structure is a proper noun, and the objective's description
    /// terminology will treat it as one.</param>
    public ObjectiveResearch(int researchId, int structureId, bool structureIsProperNoun = false)
    {
      Description = structureIsProperNoun
        ? $"Research {GetObjectName(researchId)} from {GetObjectName(structureId)}"
        : $"Research {GetObjectName(researchId)} from the {GetObjectName(structureId)}";
      PlayerUnitEvents.Register(ResearchEvent.IsFinished, OnAnyResearch, researchId);
    }

    private void OnAnyResearch()
    {
      try
      {
        if (EligibleFactions.Contains(GetOwningPlayer(GetTriggerUnit())))
          Progress = QuestProgress.Complete;
      }
      catch (Exception ex)
      {
        Logger.LogError(ex.ToString());
      }
    }
  }
}