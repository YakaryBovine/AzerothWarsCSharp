using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.QuestSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Druids
{
  public sealed class QuestDruidsKillFrostwolf : QuestData
  {
    private const int ElementalGuardianId = Constants.UNIT_E00X_ELEMENTAL_GUARDIAN_DRUIDS_DEMI;

    /// <inheritdoc/>
    public override string RewardFlavour =>
      "The Frostwolves have been driven from Kalimdor. Their departure reveals the existence of a powerful nature spirit that now heeds the call of the Druids.";

    /// <inheritdoc/>
    protected override string RewardDescription => $"The demihero {GetObjectName(ElementalGuardianId)}";

    public QuestDruidsKillFrostwolf(Capital thunderBluff) : base("Natural Contest",
      "The Frostwolf Clan has arrived on the shores of Kalimdor. Though their respect of the wild spirits is to be admired, their presence cannot be tolerated.",
      @"ReplaceableTextures\CommandButtons\BTNHeroTaurenChieftain.blp")
    {
      AddObjective(new ObjectiveControlCapital(thunderBluff, false));
      ResearchId = Constants.UPGRADE_R044_QUEST_COMPLETED_NATURAL_CONTEST_DRUIDS;
    }
    
    /// <inheritdoc/>
    protected override void OnComplete(Faction completingFaction)
    {
      completingFaction.Player?.DisplayUnitTypeAcquired(ElementalGuardianId,
        "You can now train the Elemental Guardian from the Altar of Elders.");
    }
  }
}