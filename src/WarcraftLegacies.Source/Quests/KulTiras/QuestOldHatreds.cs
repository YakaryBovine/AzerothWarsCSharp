using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.QuestSystem;

namespace WarcraftLegacies.Source.Quests.KulTiras
{
  /// <inheritdoc/>
  public sealed class QuestOldHatreds : QuestData
  {
    private readonly LegendaryHero _proudmoore;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestOldHatreds"/> class.
    /// </summary>
    public QuestOldHatreds(LegendaryHero proudmoore) : base("Old Hatreds",
      "Daelin Proudmoore led his people against the savage Orcs during the Second War. Now his old enemies ride forth once more, and he won't be satisfied until he brings the battle to their doorstep.",
      @"ReplaceableTextures\CommandButtons\BTNProudmoore.blp")
    {
      _proudmoore = proudmoore;
      AddObjective(new ObjectiveLegendInRect(proudmoore, Regions.HellfireUnlock,
        "Hellfire, the belly of the beast"));
    }

    /// <inheritdoc/>
    public override string RewardFlavour =>
      "Daelin stands before the Hellfire Citadel, towering over the landscape like a twisted monument to the Orc's brutality. He vows that, this time, he won't merely drive the Orcs back - he'll lead his men to conquer these brutal lands and slaughter them all.";

    /// <inheritdoc/>
    protected override string RewardDescription => "Daelin Proudmoore gains 4000 experience";

    /// <inheritdoc/>
    protected override void OnComplete(Faction completingFaction) =>
      _proudmoore.Unit?.AddExperience(4000);
  }
}