using MacroTools.ArtifactSystem;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.QuestSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Frostwolf
{
  public sealed class QuestDrektharsSpellbook : QuestData
  {
    private readonly LegendaryHero _thrall;

    public QuestDrektharsSpellbook(Capital nodrassil, LegendaryHero thrall) : base("Drekthar's Spellbook",
      "The savage Night Elves threaten the safety of the entire Horde. Capture their World Tree and bring Thrall to its roots.",
      @"ReplaceableTextures\CommandButtons\BTNSorceressMaster.blp")
    {
      _thrall = thrall;
      AddObjective(new ObjectiveControlCapital(nodrassil, false));
      AddObjective(new ObjectiveLegendInRect(thrall, Regions.Drekthars_Spellbook,
        "Nordrassil"));
    }

    /// <inheritdoc />
    public override string RewardFlavour =>
      "The World Tree, Nordrassil, has been captured by the forces of the Horde. Drek'thar has gifted Warchief Thrall his magical spellbook for this achievement.";

    /// <inheritdoc />
    protected override string RewardDescription => "Drek'thar's Spellbook";

    /// <inheritdoc />
    protected override void OnComplete(Faction completingFaction)
    {
      var drektharsSpellBook = new Artifact(CreateItem(Constants.ITEM_DTSB_DREK_THAR_S_SPELLBOOK, 0, 0));
      ArtifactManager.Register(drektharsSpellBook);
      _thrall.Unit?.AddItemSafe(drektharsSpellBook.Item);
    }
  }
}