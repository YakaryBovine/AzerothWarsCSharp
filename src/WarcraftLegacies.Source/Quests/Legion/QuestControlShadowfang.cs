using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.QuestSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Legion
{
  public sealed class QuestControlShadowfang : QuestData
  {
    public QuestControlShadowfang(Capital shadowfang) : base("The Dark Manor",
      "The Legion will need a hidden stronghold to house a demon gate, the Shadowfang Keep is perfectly out of the way for the role.",
      @"ReplaceableTextures\CommandButtons\BTNKeep.blp")
    {
      AddObjective(new ObjectiveControlCapital(shadowfang, false));
    }
    
    /// <inheritdoc/>
    public override string RewardFlavour => "Shadowfang Keep is now under the Legion control. A secret demon gate has now been formed inside.";

    /// <inheritdoc/>
    protected override string RewardDescription => $"Learn to train troops from Shadowfang Keep, and learn to build 1 more {GetObjectName(Constants.UNIT_N04Q_NETHER_PIT_LEGION_BARRACKS)}";

    /// <inheritdoc/>
    protected override void OnComplete(Faction whichFaction)
    {
      whichFaction.ModObjectLimit(FourCC("n04Q"), 4); //Nether Pit
    }
  }
}