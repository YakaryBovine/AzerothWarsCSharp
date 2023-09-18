using MacroTools.ArtifactSystem;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using MacroTools.QuestSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Lordaeron
{
  /// <summary>
  /// Destroying the Lich King while Arthas is under Lordaeron's control and Capital Palace is alive will result in Arthas becoming King of Lordaeron.
  /// </summary>
  public sealed class QuestKingArthas : QuestData
  {
    private readonly unit _terenas;
    private readonly Artifact _crownOfLordaeron;
    private readonly LegendaryHero _arthas;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestKingArthas"/> class.
    /// </summary>
    public QuestKingArthas(unit terenas, Artifact crownOfLordaeron, Capital capitalPalace, LegendaryHero arthas, Capital lichKing) : base("Line of Succession",
      "Arthas Menethil is the one true heir of the Kingdom of Lordaeron. The only thing standing in the way of his coronation is the world-ending threat of the Scourge.",
      @"ReplaceableTextures\CommandButtons\BTNArthas.blp")
    {
      AddObjective(new ObjectiveUnitAlive(capitalPalace.Unit));
      AddObjective(new ObjectiveControlLegend(arthas, true));
      AddObjective(new ObjectiveCapitalDead(lichKing));
      AddObjective(new ObjectiveLegendInRect(arthas, Regions.King_Arthas_crown, "King Terenas"));
      ResearchId = Constants.UPGRADE_R08A_QUEST_COMPLETED_LINE_OF_SUCCESSION;
      _terenas = terenas;
      _crownOfLordaeron = crownOfLordaeron;
      _arthas = arthas;
      Required = true;
    }

    /// <inheritdoc/>
    protected override string RewardFlavour =>
      "With the Lich King eliminated, the Kingdom of Lordaeron is free of its greatest threat. King Terenas Menethil proudly abdicates in favor of his son.";

    /// <inheritdoc/>
    protected override string RewardDescription =>
      "Arthas becomes the King of Lordaeron, gains the Crown of Lordaeron, and he can no longer permanently die";

    /// <inheritdoc/>
    protected override void OnComplete(Faction completingFaction)
    {
      _arthas.UnitType = Constants.UNIT_HARF_HIGH_KING_LORDAERON_HIGH_KING;
      _arthas.ClearUnitDependencies();
      _arthas.Unit?
        .AddItemSafe(_crownOfLordaeron.Item);
      _terenas.SetName("King Emeritus Terenas Menethil");
    }
  }
}