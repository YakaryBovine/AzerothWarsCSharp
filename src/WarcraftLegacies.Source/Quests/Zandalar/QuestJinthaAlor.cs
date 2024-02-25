using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.ObjectiveSystem.Objectives.TimeBased;
using MacroTools.QuestSystem;
using WarcraftLegacies.Source.Setup;
using WarcraftLegacies.Source.Setup.Legends;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Zandalar
{
  /// <summary>
  /// Capture <see cref="LegendNeutral.Jinthaalor"/> to unlock a new unit
  /// </summary>
  public sealed class QuestJinthaAlor : QuestData
  {
    private const int _jinthaalorResearch = Constants.UPGRADE_R02N_QUEST_COMPLETED_THE_ANCIENT_EGG_WARSONG;
    private const int _bearRiderId = Constants.UNIT_O02K_BEAR_RIDER_ZANDALAR;
    private const int _trollShrineId = Constants.UNIT_O04X_LOA_SHRINE_ZANDALARI_SIEGE;
    private readonly int goldReward = 50;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestJinthaAlor"/> class
    /// </summary>
    public QuestJinthaAlor(AllLegendSetup legendSetup) : base("The Ancient Egg",
      "The Vilebranch trolls of Jintha'Alor are controlled by their fear of the Soulflayer's egg, hidden within their shrine. Smash it to gain their loyalty.",
      @"ReplaceableTextures\CommandButtons\BTNForestTrollShadowPriest.blp")
    {
      AddObjective(new ObjectiveControlCapital(legendSetup.Neutral.Jinthaalor, false));
      AddObjective(new ObjectiveTime(900));
    }

    /// <inheritdoc/>
    public override string RewardFlavour =>
      "Jintha'Alor has fallen. The Vilebranch trolls lend their might to the Zandalari";

    /// <inheritdoc/>>
    protected override string RewardDescription =>
      $"Control of Jintha'Alor, {goldReward} gold tribute and the ability to train {GetObjectName(_bearRiderId)}s from the {GetObjectName(_trollShrineId)}";
    /// <inheritdoc/>
    protected override void OnComplete(Faction completingFaction)
    {
      if (completingFaction.Player != null)
      {
        SetPlayerTechResearched(completingFaction.Player, _jinthaalorResearch, 1);
        completingFaction.Player.AdjustPlayerState(PLAYER_STATE_RESOURCE_GOLD, goldReward);
      }
    }
    /// <inheritdoc/>
    protected override void OnAdd(Faction whichFaction)
    {
      whichFaction.ModObjectLimit(_jinthaalorResearch, Faction.UNLIMITED);
    }
  }
}