using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.QuestSystem;

namespace WarcraftLegacies.Source.Quests.Warsong
{
  public sealed class QuestGarrosh : QuestData
  {
    /// <inheritdoc/>
    public override string RewardFlavour =>
      "With Kalimdor now under the Horde's control, the Warsong will be able to expand towards new conquests";

    /// <inheritdoc/>
    protected override string RewardDescription => $"The Warsong Expedition and Garrosh will become available";

    public QuestGarrosh(Capital templeOfTheMoon) : base("Thirst for Conquest",
      "The Night Elven Druids stand in the way of the Warsong's expansion, they will need to be eliminated for the Horde to grow",
      @"ReplaceableTextures\CommandButtons\BTNWC1UnholyArmorRemasteredAlt.blp")
    {
      AddObjective(new ObjectiveCapitalDead(templeOfTheMoon));
      ResearchId = Constants.UPGRADE_R062_QUEST_COMPLETED_THIRST_FOR_CONQUEST;
    }

  }
}