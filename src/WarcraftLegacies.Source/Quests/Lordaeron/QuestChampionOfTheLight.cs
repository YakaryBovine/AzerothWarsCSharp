using MacroTools.QuestSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.LegendSystem;

namespace WarcraftLegacies.Source.Quests.Lordaeron
{
  /// <summary>
  /// Level up Uther to buff paladins and casters
  /// </summary>
  public sealed class QuestChampionoftheLight : QuestData
  {

    /// <inheritdoc />
    public QuestChampionoftheLight(LegendaryHero uther) : base("Champion of the Light",
      "Uther Lightbringer is a paragon of Light and a champion of Lordaeron. His example inspires many man to rise up.",
      @"ReplaceableTextures\CommandButtons\BTNHolyNova.blp")
    {
      AddObjective(new ObjectiveLegendLevel(uther, 12));
      ResearchId = Constants.UPGRADE_R01Q_QUEST_COMPLETED_CHAMPION_OF_THE_LIGHT;
    }

    /// <inheritdoc />
    public override string RewardFlavour =>
      "Uther has achieved the status of living legend, inspiring the men and women of Lordaeron to strive for greatness.";

    /// <inheritdoc />
    protected override string RewardDescription =>
      $"Your casters and Paladins gain 200 hit points and 5 damage, and Paladins gain the Reincarnation ability";

  }
}