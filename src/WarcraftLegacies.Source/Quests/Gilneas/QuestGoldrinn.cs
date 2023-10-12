using MacroTools.ArtifactSystem;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.ArtifactBased;
using MacroTools.QuestSystem;
using WarcraftLegacies.Source.Setup.FactionSetup;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Gilneas
{
  public sealed class QuestGoldrinn : QuestData
  {
    private readonly LegendaryHero _goldrinn;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestGoldrinn"/> class.
    /// </summary>
    public QuestGoldrinn(Artifact scytheOfElune, LegendaryHero goldrinn) : base("Shrine of the Wolf God",
      "The Worgen curse originated from Goldrinn, the embodiment of ferocity, savagery, and unyielding will. Through the power of the Scythe of Elune, his fallen spirit might be called upon to aid his unwillingly conceived progeny.",
      @"ReplaceableTextures\CommandButtons\BTNWorgenHunger.blp")
    {
      AddObjective(new ObjectiveAcquireArtifact(scytheOfElune));
      AddObjective(new ObjectiveArtifactInRect(scytheOfElune, Regions.MountHyjal, "Mount Hyjal"));
      ResearchId = Constants.UPGRADE_R07U_QUEST_COMPLETED_SHRINE_OF_THE_WOLF_GOD;
      _goldrinn = goldrinn;
    }

    /// <inheritdoc/>
    protected override string RewardFlavour =>
      "The Scythe of Elune calls to Goldrinn's spirit. Revolted at the horrors that his fang had wrought on the Gilnean people but impressed with their ferocity, he returns to the mortal world, ready to rend and tear for his new people.";

    /// <inheritdoc/>
    protected override string RewardDescription =>
      $"Learn to train {_goldrinn.Name} from the {GetObjectName(Constants.UNIT_H02X_ALTAR_OF_KINGS_GILNEAS_ALTAR)}. If you're allied to the Druids, {_goldrinn.Name}'s starting experience is halved";
    
    /// <inheritdoc/>
    protected override void OnComplete(Faction completingFaction)
    {
      if (ShouldApplyExperiencePenalty(completingFaction)) 
        _goldrinn.StartingXp /= 2;
    }

    private static bool ShouldApplyExperiencePenalty(Faction completingFaction)
    {
      var druidsPlayer = DruidsSetup.Druids?.Player;
      return druidsPlayer != null && completingFaction.Player != null &&
             druidsPlayer.GetTeam()?.Contains(completingFaction.Player) != false;
    }
  }
}
