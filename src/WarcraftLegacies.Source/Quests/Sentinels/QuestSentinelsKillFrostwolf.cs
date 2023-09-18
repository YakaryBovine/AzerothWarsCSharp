using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.QuestSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Sentinels
{
  /// <summary>
  /// Destroy Thunderbluff to unlock a hero and a demi hero.
  /// </summary>
  public sealed class QuestSentinelsKillFrostwolf : QuestData
  {

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestSentinelsKillFrostwolf"/> class.
    /// </summary>
    public QuestSentinelsKillFrostwolf(Capital thunderBluff) : base("Drive Them Back",
      "The Frostwolf Clan is beginning to seize a firm foothold within the Barrens and on the plains of Mulgore. They must be driven back.",
      @"ReplaceableTextures\CommandButtons\BTNThrall.blp")
    {
      AddObjective(new ObjectiveCapitalDead(thunderBluff));
      ResearchId = Constants.UPGRADE_R052_QUEST_COMPLETED_DRIVE_THEM_BACK_SENTINELS;
      Required = true;
    }
    /// <inheritdoc/>
    protected override string RewardFlavour =>
      "The Frostwolves have been ousted from Kalimdor, along with their Tauren allies. They will not be missed.";

    /// <inheritdoc/>
    protected override string RewardDescription => "Moon Riders are trainable at the Roost";

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