using MacroTools.ArtifactSystem;
using MacroTools.ControlPointSystem;
using MacroTools.FactionSystem;
using MacroTools.ObjectiveSystem.Objectives.ArtifactBased;
using MacroTools.ObjectiveSystem.Objectives.ControlPointBased;
using MacroTools.QuestSystem;

namespace WarcraftLegacies.Source.Quests.Zandalar
{
  /// <summary>
  /// Zandalar can acquire Hakkar as a hero.
  /// </summary>
  public sealed class QuestHakkar : QuestData
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="QuestHakkar"/> class.
    /// </summary>
    public QuestHakkar(Artifact zinrokh) : base("The Binding of the Soulflayer",
      "Hakkar is the most dangerous and powerful of the Troll gods. Only by fusing the Demon Soul would the Zandalari be able to control Hakkar and bind him to their will.",
      @"ReplaceableTextures\CommandButtons\BTNWindSerpent2.blp")
    {
      AddObjective(new ObjectiveAcquireArtifact(zinrokh));
      AddObjective(new ObjectiveArtifactInRect(zinrokh, Regions.DrownedTemple,
        "The Drowned Temple"));
      AddObjective(new ObjectiveControlPoint(ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N00U_SWAMP_OF_SORROWS)));
      Global = true;
      ResearchId = Constants.UPGRADE_R06W_QUEST_COMPLETED_THE_BINDING_OF_THE_SOULFLAYER;
    }

    /// <inheritdoc/>
    public override string RewardFlavour => "Hakkar has emerged from the Drowned Temple";

    /// <inheritdoc/>
    protected override string RewardDescription => "Gain the demigod hero Hakkar";

    /// <inheritdoc/>
    protected override void OnComplete(Faction completingFaction)
    {
    }
  }
}