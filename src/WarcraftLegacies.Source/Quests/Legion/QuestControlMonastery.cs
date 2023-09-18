using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.QuestSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Legion
{
  public sealed class QuestControlMonastery : QuestData
  {
    public QuestControlMonastery(Capital monastery) : base("Corrupting the Monastery",
      "The mind of humans are feeble and easily corruptable, the Scarlet Monastery will be a perfect ground for a secret demon portal",
      @"ReplaceableTextures\CommandButtons\BTNLordaeronMageTower.blp")
    {
      AddObjective(new ObjectiveControlCapital(monastery, false));
      ResearchId = Constants.UPGRADE_R04H_QUEST_COMPLETED_ANY_OF_THE_THREE_INFILTRATION_QUESTS;
    }

    /// <inheritdoc/>
    protected override string RewardFlavour => "The Monastery has been corrupted and plundered. A secret demon gate has now been formed inside.";

    /// <inheritdoc/>
    protected override string RewardDescription => "Unlock the Monastery as a troop production building and able to build 1 more Summoning Circle. Completing any of the three infiltration quest will enable the Alterac Portal to be opened.";

    /// <inheritdoc/>
    protected override void OnComplete(Faction whichFaction)
    {
      whichFaction.ModObjectLimit(FourCC("u006"), 4); //Summoning Circle
    }
  }
}