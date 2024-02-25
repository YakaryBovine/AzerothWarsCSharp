using MacroTools.ControlPointSystem;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.ControlPointBased;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.QuestSystem;

namespace WarcraftLegacies.Source.Quests.Dalaran
{
  public sealed class QuestCrystalGolem : QuestData
  {
    private const int RewardResearchTypeId = Constants.UPGRADE_R045_HARD_CRYSTAL_CONSTRUCTS_DALARAN_QUEST;
    
    public QuestCrystalGolem(Capital draktharonKeep) : base("Crystalsong Forest",
      "The living crystal of the Crystalsong Forest suffers from its proximity to the Legion. Freed from that corruption, it could be used to empower Dalaran's constructs."
      , @"ReplaceableTextures\CommandButtons\BTNRockGolem.blp")
    {
      AddObjective(new ObjectiveControlPoint(ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N02R_CRYSTALSONG_FOREST)));
      AddObjective(new ObjectiveControlCapital(draktharonKeep, false));
    }

    /// <inheritdoc/>
    public override string RewardFlavour => "Dalaran's Earth Golems have been infused with living crystal.";

    /// <inheritdoc/>
    protected override string RewardDescription => "Transform your Earth Golems into Crystal Golems";

    /// <inheritdoc/>
    protected override void OnComplete(Faction completingFaction)
    {
      completingFaction.SetObjectLevel(RewardResearchTypeId, 1);
      completingFaction.Player?.DisplayResearchAcquired(RewardResearchTypeId, 1);
      completingFaction.ModObjectLimit(Constants.UNIT_N096_EARTH_GOLEM_DALARAN, -6);
      completingFaction.ModObjectLimit(Constants.UNIT_N0AD_CRYSTAL_GOLEM_DALARAN_HARD_CRYSTAL_CONSTRUCTS, 6);
    }
    
    /// <inheritdoc/>
    protected override void OnAdd(Faction whichFaction) => 
      whichFaction.ModObjectLimit(RewardResearchTypeId, Faction.UNLIMITED);
  }
}