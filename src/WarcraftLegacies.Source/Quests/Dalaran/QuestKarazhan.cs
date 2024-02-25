using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.QuestSystem;

namespace WarcraftLegacies.Source.Quests.Dalaran
{
  public sealed class QuestKarazhan : QuestData
  {
    public QuestKarazhan(Capital karazhan) : base("Secrets of Karazhan",
      "The spire of Medivh stands mysteriously idle. Dalaran could make use of its grand magicks.",
      @"ReplaceableTextures\CommandButtons\BTNTomeBrown.blp")
    {
      AddObjective(new ObjectiveControlCapital(karazhan, false));
    }
    
    /// <inheritdoc/>
    public override string RewardFlavour => "Karazhan has been captured. Dalaran's archivists scour its halls for arcane resources.";

    /// <inheritdoc/>
    protected override string RewardDescription => "Learn to research three powerful upgrades at Karazhan.";

    /// <inheritdoc/>
    protected override void OnAdd(Faction whichFaction)
    {
      whichFaction.ModObjectLimit(Constants.UPGRADE_R020_RAIN_AN_AMALGAM_DALARAN_KARAZHAN, Faction.UNLIMITED);
      whichFaction.ModObjectLimit(Constants.UPGRADE_R03M_METHODS_OF_NEGATION_DALARAN_KARAZHAN, Faction.UNLIMITED);
      whichFaction.ModObjectLimit(Constants.UPGRADE_R01B_A_TREATISE_ON_BARRIERS_DALARAN_KARAZHAN, Faction.UNLIMITED);
    }
  }
}