using MacroTools.ArtifactSystem;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using MacroTools.QuestSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Scourge
{
  /// <summary>
  /// Arthas ascends to become the Lich King.
  /// </summary>
  public sealed class QuestLichKingArthas : QuestData
  {
    private readonly unit _utgardeKeep;
    private readonly Artifact _helmOfDomination;
    private readonly LegendaryHero _arthas;
    private readonly Capital _lichKing;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestLichKingArthas"/> class.
    /// </summary>
    public QuestLichKingArthas(unit utgardeKeep, Artifact helmOfDomination, LegendaryHero arthas, Capital lichKing) : base("The Ascension",
      "From within the depths of the Frozen Throne, the Lich King Ner'zhul cries out for his champion. Release the Helm of Domination from its confines and merge its power with that of the Scourge's greatest Death Knight.",
      @"ReplaceableTextures\CommandButtons\BTNRevenant.blp")
    {
      _utgardeKeep = utgardeKeep;
      _helmOfDomination = helmOfDomination;
      _arthas = arthas;
      _lichKing = lichKing;
      AddObjective(new ObjectiveControlLegend(arthas, false));
      AddObjective(new ObjectiveLegendLevel(arthas, 15));
      AddObjective(new ObjectiveResearch(Constants.UPGRADE_R07X_MAKE_ARTHAS_THE_LICH_KING_SCOURGE, FourCC("u000")));
      AddObjective(new ObjectiveLegendInRect(arthas, Regions.LichKing, "Icecrown Citadel"));
      Global = true;
      
    }

    /// <inheritdoc />
    public override string RewardFlavour =>
      "Arthas has ascended the Frozen Throne itself and shattered Ner'zhul's frozen prison. Ner'zhul and Arthas are now joined, body and soul, into one being: the god-like Lich King.";

    /// <inheritdoc />
    protected override string RewardDescription =>
      "Arthas becomes the Lich King, but the Frozen Throne loses its abilities";

    /// <inheritdoc />
    protected override void OnComplete(Faction completingFaction)
    {
      PlayThematicMusic(@"Sound\Music\mp3Music\LichKingTheme.mp3");
      
      _lichKing.DeathMessage =
        "Icecrown Citadel been razed. Unfortunately, the Lich King has already vacated his unholy throne.";
      _lichKing.Hivemind = false;
      _lichKing.Unit?
        .RemoveAbility(Constants.ABILITY_A0W8_RECALL_FROZEN_THRONE)
        .RemoveAbility(Constants.ABILITY_A0L3_ANIMATE_DEAD_RED_THE_FROZEN_THRONE)
        .RemoveAbility(Constants.ABILITY_A001_FROST_NOVA_RED_THE_FROZEN_THRONE)
        .SetMaximumMana(0)
        .SetName("Icecrown Citadel");
      
      _arthas.UnitType = Constants.UNIT_N023_LORD_OF_THE_SCOURGE_SCOURGE;
      _arthas.PermaDies = true;
      _arthas.Hivemind = true;
      _arthas.DeathMessage =
        "The great Lich King has been destroyed. With no central mind to command them, the forces of the Undead have gone rogue.";

      _arthas.Unit?
        .SetLifePercent(100)
        .SetManaPercent(100)
        .AddItemSafe(_helmOfDomination.Item);
      
      _utgardeKeep.Rescue(completingFaction.Player);
    }
  }
}