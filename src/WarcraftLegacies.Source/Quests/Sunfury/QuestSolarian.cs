using MacroTools.ArtifactSystem;
using MacroTools.ObjectiveSystem.Objectives.ArtifactBased;
using MacroTools.QuestSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Sunfury
{
  /// <summary>
  /// The druids gets Solarian
  /// </summary>
  public sealed class QuestSolarian : QuestData
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="QuestSolarian"/> class.
    /// </summary>
    /// <param name="essence">Required to complete the quest.</param>
    public QuestSolarian(Artifact essence) : base("The High Astromancer",
      "High Astromancer Solarion has long had a fascination with the void, much to the chagrin of her colleagues. With the right research materials in hand, she could become a force to be reckoned with.",
      @"ReplaceableTextures\CommandButtons\BTNSpell_Shadow_SummonVoidWalker.blp")
    {
      AddObjective(new ObjectiveAcquireArtifact(essence));
      
      ResearchId = Constants.UPGRADE_R09K_QUEST_COMPLETED_THE_HIGH_ASTROMANCER;
    }

    /// <inheritdoc />
    public override string RewardFlavour =>
      "Extensive study of Murmur's essence has granted Solarion the power to channel void energies.";

    /// <inheritdoc />
    protected override string RewardDescription =>
      $"Learn to train High Astromancer Solarion from the {GetObjectName(Constants.UNIT_H0C6_ALTAR_OF_BLOOD_SUNFURY_ALTAR)}";
  }
}