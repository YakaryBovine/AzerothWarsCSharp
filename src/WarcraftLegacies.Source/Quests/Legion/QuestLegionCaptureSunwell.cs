using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.QuestSystem;

namespace WarcraftLegacies.Source.Quests.Legion
{
  public sealed class QuestLegionCaptureSunwell : QuestData
  {
    public QuestLegionCaptureSunwell(Capital sunwell) : base("Fall of Silvermoon",
      "The Sunwell is the source of the High Elves' immortality and magical prowess, created from a stolen vial from the Well of Eternity. The immense power within its waters would be an immense boon to the Legion.",
      @"ReplaceableTextures\CommandButtons\BTNOrbOfCorruption.blp")
    {
      AddObjective(new ObjectiveControlCapital(sunwell, false));
      ResearchId = Constants.UPGRADE_R054_QUEST_COMPLETED_FALL_OF_SILVERMOON;
    }

    /// <inheritdoc/>
    public override string RewardFlavour =>
      "The Dreadlords drink freely of the Sunwell. The energies that once coursed through the waters of the well now course through the veins of the Nazrethim, infusing them with power enough to tear holes in dimensions.";

    /// <inheritdoc/>
    protected override string RewardDescription =>
      "Improves Dreadlords and Nathrezim by increasing their attack damage by 20, movement speed by 20, hit points by 200, and granting them the ability to cast Astral Walk";
  }
}