using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.ObjectiveSystem.Objectives.TimeBased;
using MacroTools.QuestSystem;
using WarcraftLegacies.Source.Setup;
using WarcraftLegacies.Source.Setup.Legends;

namespace WarcraftLegacies.Source.Quests.Zandalar
{

  /// <summary>
  /// Capture <see cref="LegendNeutral.Gundrak"/> to unlock a new unit
  /// </summary>
  public sealed class QuestGundrak : QuestData
  {
    private const int GundrakResearch = UPGRADE_R02Q_QUEST_COMPLETED_THE_DRAKKARI_FORTRESS_WARSONG;
    private const int WarlordId = UNIT_NFTK_WARLORD_ZANDALAR;
    private const int TrollShrineId = UNIT_O04X_LOA_SHRINE_ZANDALARI_SIEGE;
    private readonly int _goldReward = 50;
    /// <summary>
    /// Initializes a new instance of the <see cref="QuestGundrak"/> class
    /// </summary>
    public QuestGundrak(AllLegendSetup legendSetup) : base("The Drakkari Fortress",
      "The Drakkari troll of Gundrak believe their fortress to be impregnable. Capture it to gain their loyalty.",
      @"ReplaceableTextures\CommandButtons\BTNTerrorTroll.blp")
    {
      AddObjective(new ObjectiveControlCapital(legendSetup.Neutral.Gundrak, false));
      AddObjective(new ObjectiveTime(900));
    }

    /// <inheritdoc/>
    public override string RewardFlavour =>
      "Gundrak has fallen. The Drakkari trolls lend their might to the Zandalari.";

    /// <inheritdoc/>
    protected override string RewardDescription =>
      $"{_goldReward} gold and the ability to train {GetObjectName(WarlordId)}s from the {GetObjectName(TrollShrineId)}.";

    /// <inheritdoc/>
    protected override void OnComplete(Faction completingFaction)
    {
      if (completingFaction.Player != null)
      {
        SetPlayerTechResearched(completingFaction.Player, GundrakResearch, 1);
        completingFaction.Player.AdjustPlayerState(PLAYER_STATE_RESOURCE_GOLD, _goldReward);
      }
    }

    /// <inheritdoc/>
    protected override void OnAdd(Faction whichFaction)
    {
      whichFaction.ModObjectLimit(GundrakResearch, Faction.UNLIMITED);
    }
  }
}