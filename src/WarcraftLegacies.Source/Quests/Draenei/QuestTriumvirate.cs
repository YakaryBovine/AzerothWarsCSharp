using MacroTools.ArtifactSystem;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.ControlPointBased;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.QuestSystem;

namespace WarcraftLegacies.Source.Quests.Draenei
{
  public sealed class QuestTriumvirate : QuestData
  {
    private readonly LegendaryHero _velen;

    public QuestTriumvirate(LegendaryHero velen) : base("Crown of the Triumvirate",
      "Eons ago, the council that led the Eredar was the Triumvirate. If Velen could reconquer Argus, he could reform the Crown of the Triumvirate",
      @"ReplaceableTextures\CommandButtons\BTNNeverMeltingCrown.blp")
    {
      _velen = velen;
      AddObjective(new ObjectiveControlPoint(FourCC("n0BH")));
      AddObjective(new ObjectiveControlPoint(FourCC("n0BL")));
      AddObjective(new ObjectiveControlPoint(FourCC("n09X")));
      AddObjective(new ObjectiveLegendNotPermanentlyDead(velen));
      Global = true;
    }

    /// <inheritdoc />
    public override string RewardFlavour => "Velen has liberated Argus and re-assembled the Crown of Triumvirate";

    /// <inheritdoc />
    protected override string RewardDescription => "You gain the powerful item, the Crown of the Triumvirate";

    /// <inheritdoc />
    protected override void OnComplete(Faction completingFaction)
    {
      var crownOfTheTriumvirate = new Artifact(CreateItem(ITEM_I011_CROWN_OF_THE_TRIUMVIRATE, 0, 0));
      ArtifactManager.Register(crownOfTheTriumvirate);
      _velen.Unit?.AddItemSafe(crownOfTheTriumvirate.Item);
    }
  }
}