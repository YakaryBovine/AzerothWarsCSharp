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
    }

    /// <inheritdoc/>
    public override string RewardFlavour => "The Monastery has been corrupted and plundered. A secret demon gate has now been formed inside.";

    /// <inheritdoc/>
    protected override string RewardDescription => $"Learn to train troops from the Monastery, and learn to build 1 more {GetObjectName(Constants.UNIT_U006_SUMMONING_CIRCLE_LEGION_MAGIC)}";

    /// <inheritdoc/>
    protected override void OnComplete(Faction whichFaction)
    {
      whichFaction.ModObjectLimit(FourCC("u006"), 4); //Summoning Circle
    }
  }
}