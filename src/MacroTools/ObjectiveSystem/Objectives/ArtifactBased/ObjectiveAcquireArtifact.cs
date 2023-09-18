using MacroTools.ArtifactSystem;
using MacroTools.FactionSystem;
using MacroTools.QuestSystem;
using static War3Api.Common;

namespace MacroTools.ObjectiveSystem.Objectives.ArtifactBased
{
  /// <summary>
  ///   Completes when the quest holder picks up a particular <see cref="Artifact" />.
  /// </summary>
  public sealed class ObjectiveAcquireArtifact : Objective
  {
    private readonly Artifact _target;

    /// <summary>
    /// Initializes a new instance of the <see cref="ObjectiveAcquireArtifact"/> class.
    /// </summary>
    /// <param name="target">The objective is completed when this artifact is acquired.</param>
    public ObjectiveAcquireArtifact(Artifact target)
    {
      Description = $"Acquire {GetItemName(target.Item)}";
      _target = target;
      target.PickedUp += (_, _) =>
        Progress = EligibleFactions.Contains(_target.OwningPlayer) ? QuestProgress.Complete : QuestProgress.Incomplete;
      target.Dropped += (_, _) => Progress = QuestProgress.Incomplete;
      target.Disposed += (_, _) => Progress = QuestProgress.Failed;
    }

    internal override void OnAdd(Faction whichFaction)
    {
      if (EligibleFactions.Contains(_target.OwningPlayer))
        Progress = QuestProgress.Complete;
    }
  }
}