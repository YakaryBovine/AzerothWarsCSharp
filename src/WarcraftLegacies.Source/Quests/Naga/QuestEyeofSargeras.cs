using MacroTools.ArtifactSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.QuestSystem;
using WarcraftLegacies.Source.Setup;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Naga
{
  /// <summary>
  /// Acquire the <see cref="ArtifactSetup.EyeOfSargeras"/> to unlock Coilfang Summoners
  /// </summary>
  public sealed class QuestEyeofSargeras : QuestData
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="QuestEyeofSargeras"/> class.
    /// </summary>
    public QuestEyeofSargeras(Artifact eyeOfSargeras, LegendaryHero illidan) : base("The Eye of Sargeras",
      "The Eye of Sargeras is an extremely powerful artifact, it could be the key to satiate Illidan's thirst for power.",
      @"ReplaceableTextures\CommandButtons\BTNKazzakIon.blp")
    {
      AddObjective(new ObjectiveLegendHasArtifact(illidan, eyeOfSargeras));
      ResearchId = Constants.UPGRADE_R094_QUEST_COMPLETED_THE_EYE_OF_SARGERAS;
    }

    /// <inheritdoc/>
    public override string RewardFlavour =>
      "The Eye of Sargeras' power needs to be channeled by powerful arcanists, the Naga Sea Witch will be the perfect vessels.";

    /// <inheritdoc/>
    protected override string RewardDescription =>
      $"Learn to train {GetObjectName(Constants.UNIT_HVSH_SEA_WITCH_ILLIDARI)}es from {GetObjectName(Constants.UNIT_N055_BETRAYER_S_CITADEL_ILLIDARI_T3)}s";
  }
}