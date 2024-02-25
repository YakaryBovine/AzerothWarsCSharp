using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.QuestSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Goblin
{
  /// <inheritdoc />
  public sealed class QuestLumberMarket : QuestData
  {
    private const int LumberReward = 30000;
    
    /// <inheritdoc/>
    public override string RewardFlavour => "The World Tree is ours, our lumber supplies will never run out!";

    /// <inheritdoc/>
    protected override string RewardDescription =>
      $"Shredders gain the Cleaving Attack ability and 500 hit points, and you gain {LumberReward} lumber";

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestLumberMarket"/> class.
    /// </summary>
    public QuestLumberMarket(Capital nordrassil) : base("Lumber Market Crash",
      "The World Tree would provide enough lumber to completely crash the lumber market, forcing our Shredders to specialise more on war.",
      @"ReplaceableTextures\CommandButtons\BTNJunkGolem.blp")
    {
      AddObjective(new ObjectiveControlCapital(nordrassil, false));
      ResearchId = Constants.UPGRADE_R07Z_QUEST_COMPLETED_LUMBER_MARKET_CRASH;
    }
    
    /// <inheritdoc/>
    protected override void OnComplete(Faction completingFaction)
    {
      completingFaction.Player?.AdjustPlayerState(PLAYER_STATE_RESOURCE_LUMBER, LumberReward);
    }
  }
}