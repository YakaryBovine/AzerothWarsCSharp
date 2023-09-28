using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.QuestSystem;

namespace WarcraftLegacies.Source.Quests.Legion
{
  public sealed class QuestUnlockAstral : QuestData
  {

    public QuestUnlockAstral(Capital caerDarrow) : base("Secrets of the Astral Realm",
      "Within the island stronghold of Caer'Darrow, a profound and potent dark energy pulsates, holding the potential to restore our mastery and grant us the power to traverse realms freely once more.",
      @"ReplaceableTextures\CommandButtons\BTNSpell_Nature_AstralRecal.blp")

    {
      AddObjective(new ObjectiveControlCapital(caerDarrow, false));
      ResearchId = Constants.UPGRADE_R09S_QUEST_COMPLETED_SECRETS_OF_THE_ASTRAL_REALM;

    }

    /// <inheritdoc/>
    protected override string RewardFlavour => "With a firm grip on the stronghold and its enigmatic secrets, we embark upon the path to dominate this feeble realm.";

    /// <inheritdoc/>
    protected override string RewardDescription => "You can now research Astral Walk from Dominant Spires; for Dreadlords and Nathrezim infiltrators.";

    /// <inheritdoc/>
    protected override void OnComplete(Faction whichFaction)
    {


    }
  }
}