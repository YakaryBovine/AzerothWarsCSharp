using MacroTools.ArtifactSystem;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.QuestSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Dalaran
{
  public sealed class QuestJainaSoulGem : QuestData
  {
    private readonly LegendaryHero _jaina;

    public QuestJainaSoulGem(LegendaryHero jaina, Capital caerDarrow) : base("The Soul Gem",
      "Scholomance is home to a wide variety of profane artifacts. Bring Jaina there to see what might be discovered.",
      @"ReplaceableTextures\CommandButtons\BTNSoulGem.blp")
    {
      _jaina = jaina;
      AddObjective(new ObjectiveLegendInRect(jaina, Regions.CaerDarrow, "Caer Darrow"));
      AddObjective(new ObjectiveControlCapital(caerDarrow, false));
    }
    
    /// <inheritdoc />
    public override string RewardFlavour =>
      "Jaina Proudmoore has discovered the Soul Gem within the ruined vaults at Scholomance.";

    /// <inheritdoc />
    protected override string RewardDescription => "The Soul Gem";

    /// <inheritdoc />
    protected override void OnComplete(Faction completingFaction)
    {
      var soulGem = new Artifact(CreateItem(Constants.ITEM_GSOU_SOUL_GEM, 0, 0));
      ArtifactManager.Register(soulGem);
      _jaina.Unit?.AddItemSafe(soulGem.Item);
    }
  }
}