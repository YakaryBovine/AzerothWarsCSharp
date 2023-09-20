using MacroTools.ArtifactSystem;
using MacroTools.ControlPointSystem;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.ArtifactBased;
using MacroTools.ObjectiveSystem.Objectives.ControlPointBased;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.QuestSystem;

namespace WarcraftLegacies.Source.Quests.Gilneas
{
  /// <summary>
  /// Go to the Shrine of Goldrinn in Duskwood in order to unlock Goldrinn as a hero
  /// </summary>
  public sealed class QuestGoldrinnHumanPath : QuestData
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="QuestGoldrinnHumanPath"/> class.
    /// </summary>
    public QuestGoldrinnHumanPath(LegendaryHero tess, Artifact scytheOfElune) : base("The Twilight Grove", "To understand the plight of her people, Tess will go to the Shrine of Goldrinn in Duskwood to understand what it means to be a Worgen.", @"ReplaceableTextures\CommandButtons\BTNWorgenHunger.blp")
    {
      AddObjective(new ObjectiveLegendInRect(tess, Regions.WyrmrestTemple, "The Wyrmrest Temple"));
      AddObjective(new ObjectiveArtifactInRect(scytheOfElune, Regions.WyrmrestTemple, "The Wyrmrest Temple"));
      AddObjective(new ObjectiveControlLevel(
        ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N02J_HOWLING_FJORDS), 10));
      ResearchId = Constants.UPGRADE_R07U_QUEST_COMPLETED_SHRINE_OF_THE_WOLF_GOD;
    }

    /// <inheritdoc/>
    protected override string RewardFlavour => "Goldrinn has joined Gilneas and they remain in the Alliance.";

    /// <inheritdoc/>
    protected override string RewardDescription => "Goldrinn will be trainable at the altar.";

    /// <inheritdoc/>
    protected override void OnComplete(Faction whichFaction)
    {
    }
  }
}
