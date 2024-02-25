using MacroTools.ArtifactSystem;
using MacroTools.ControlPointSystem;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.ObjectiveSystem.Objectives.ControlPointBased;
using MacroTools.QuestSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Ironforge
{
  /// <summary>
  /// Find a random <see cref="Artifact"/> in Uldum.
  /// </summary>
  public sealed class QuestExpedition : QuestData
  {
    private readonly int _rewardArtifactItemTypeId;

    /// <inheritdoc />
    public override string RewardFlavour => 
      $"After months of digging, excavating, and tomb raiding, the Explorer's Guild has finally unearthed an artifact from Ul'dum: {GetObjectName(_rewardArtifactItemTypeId)}.";

    /// <inheritdoc />
    protected override string RewardDescription => $"The Artifact {GetObjectName(_rewardArtifactItemTypeId)} appears at Uldum";
    
    /// <summary>
    /// Initializes a new instance of the <see cref="QuestExpedition"/> class.
    /// </summary>
    /// <param name="rewardArtifactItemTypeId">The reward for completing the quest.</param>
    public QuestExpedition(int rewardArtifactItemTypeId) : base("Secrets of Uldum", 
      "Uldum was once a vast jungle, until the Forge of Origination stationed there wiped the slate clean. Now, buried under the sands lies a veritable trove of ancient relics.",
      @"ReplaceableTextures\CommandButtons\BTNGatherGold.blp")
    {
      _rewardArtifactItemTypeId = rewardArtifactItemTypeId;
      AddObjective(new ObjectiveControlLevel(ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N0BD_ULDUM), 5));
    }

    /// <inheritdoc />
    protected override void OnComplete(Faction whichFaction)
    {
      var uldumPosition = ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N0BD_ULDUM).Unit.GetPosition();
      var rewardArtifact = new Artifact(CreateItem(_rewardArtifactItemTypeId, uldumPosition.X, uldumPosition.Y));
      rewardArtifact.Titanforge();
      ArtifactManager.Register(rewardArtifact);
    }
  }
}