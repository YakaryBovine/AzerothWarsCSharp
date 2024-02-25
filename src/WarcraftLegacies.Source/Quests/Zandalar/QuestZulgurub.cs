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
  /// Capture <see cref="LegendNeutral.Zulgurub"/> to unlock a new unit
  /// </summary>
  public sealed class QuestZulgurub : QuestData
  {
    private const int _zulgurubResearch = Constants.UPGRADE_R02M_QUEST_COMPLETED_THE_HEART_OF_HAKKAR_WARSONG;
    private const int _trollShrineId = Constants.UNIT_O04X_LOA_SHRINE_ZANDALARI_SIEGE;
    private const int _ravagerId = Constants.UNIT_O021_RAVAGER_ZANDALAR;
    private readonly int goldReward = 50;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestZulgurub"/> class
    /// </summary>
    public QuestZulgurub(AllLegendSetup allLegendSetup) : base("Heart of Hakkar",
      "The Gurubashi trolls of Zul'Gurub follow the sacred Heart of Hakkar, hidden within their shrine. Capture it to gain their loyalty.",
      @"ReplaceableTextures\CommandButtons\BTNTrollRavager.blp")
    {
      AddObjective(new ObjectiveControlCapital(allLegendSetup.Neutral.Zulgurub, false));
      AddObjective(new ObjectiveTime(900));
    }

    /// <inheritdoc/>
    public override string RewardFlavour =>
      "Zul'gurub has fallen. The Gurubashi trolls lend their might to the Zandalari.";

    /// <inheritdoc/>
    protected override string RewardDescription =>
      $"{goldReward} gold and the ability to train {GetObjectName(_ravagerId)}s from the {GetObjectName(_trollShrineId)}";

    /// <inheritdoc/>>
    protected override void OnComplete(Faction completingFaction)
    {
      if(completingFaction.Player != null)
      {
        SetPlayerTechResearched(completingFaction.Player, _zulgurubResearch, 1);
        completingFaction.Player.AdjustPlayerState(PLAYER_STATE_RESOURCE_GOLD, goldReward);
      }
    }

    /// <inheritdoc/>
    protected override void OnAdd(Faction whichFaction)
    {
      whichFaction.ModObjectLimit(_zulgurubResearch, Faction.UNLIMITED);
    }
  }
}