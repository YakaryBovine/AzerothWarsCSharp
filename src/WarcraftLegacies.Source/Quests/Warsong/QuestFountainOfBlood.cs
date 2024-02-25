using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.QuestSystem;

namespace WarcraftLegacies.Source.Quests.Warsong
{
  public sealed class QuestFountainOfBlood : QuestData
  {
    public QuestFountainOfBlood(Capital fountainOfBlood, LegendaryHero grom) : base("The Blood of Mannoroth",
      "Long ago, the orcs drank the blood of Mannoroth and were infused with demonic fury. A mere taste of his blood would reignite those powers.",
      @"ReplaceableTextures\CommandButtons\BTNFountainOfLifeBlood.blp")
    {
      AddObjective(new ObjectiveLegendReachRect(grom, Regions.FountainUnlock,
        "The Fountain of Blood"));
      AddObjective(new ObjectiveControlCapital(fountainOfBlood, false));
      ResearchId = Constants.UPGRADE_R00X_QUEST_COMPLETED_THE_BLOOD_OF_MANNOROTH_WARSONG;
      
    }
    
    /// <inheritdoc/>
    public override string RewardFlavour =>
      "The Fountain of Blood is under Warsong control. As the orcs drink from it, they feel a a familiar fury awake within them.";

    /// <inheritdoc/>
    protected override string RewardDescription =>
      "Allows Orcish units to increase their attack rate and movement speed temporarily. Blood Brothers is now available to Grunts";
  }
}