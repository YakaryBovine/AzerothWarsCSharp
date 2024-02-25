using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.QuestSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Sentinels
{
  /// <summary>
  /// Destroy Orgrimmar to unlock the Watcher Bastion as a building.
  /// </summary>
  public sealed class QuestSentinelsKillWarsong : QuestData
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="QuestSentinelsKillWarsong"/> class.
    /// </summary>
    public QuestSentinelsKillWarsong(Capital orgrimmar) : base("Green-skinned Brutes",
      "The Warsong Clan has arrived near Ashenvale and begun threatening the wilds. These invaders must be repelled.",
      @"ReplaceableTextures\CommandButtons\BTNRaider.blp")
    {
      AddObjective(new ObjectiveCapitalDead(orgrimmar));
      ResearchId = Constants.UPGRADE_R007_QUEST_COMPLETED_GREEN_SKINNED_BRUTES_SENTINELS;
      
    }

    /// <inheritdoc/>
    public override string RewardFlavour =>
      "The Warsong presence on Kalimdor has been eliminated. The land has been protected from their misbegotten race.";

    /// <inheritdoc/>
    protected override string RewardDescription => $"Allows {GetObjectName(Constants.UNIT_N034_GUILD_RANGER_SENTINELS)}s to be trained from {GetObjectName(Constants.UNIT_E00T_WATCHER_S_BASTION_SENTINEL_SIEGE)}s";

    /// <inheritdoc/>
    protected override void OnComplete(Faction completingFaction)
    {
      SetPlayerTechResearched(completingFaction.Player, ResearchId, 1);
    }

    /// <inheritdoc/>
    protected override void OnAdd(Faction whichFaction)
    {
      whichFaction.ModObjectLimit(ResearchId, Faction.UNLIMITED);
    }
  }
}