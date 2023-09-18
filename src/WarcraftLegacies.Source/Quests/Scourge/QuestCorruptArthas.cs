using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.FactionBased;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.ObjectiveSystem.Objectives.MetaBased;
using MacroTools.ObjectiveSystem.Objectives.QuestBased;
using MacroTools.QuestSystem;
using WarcraftLegacies.Source.Setup.FactionSetup;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Scourge
{
  /// <summary>
  /// The Scourge can corrupt Arthas, turning him into a Death Knight in their service.
  /// </summary>
  public sealed class QuestCorruptArthas : QuestData
  {
    private readonly LegendaryHero _arthas;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestCorruptArthas"/> class.
    /// </summary>
    public QuestCorruptArthas(QuestData questDestroyStratholme, LegendaryHero arthas) : base("The Dark Champion",
      "Driven by vengeance, Prince Arthas will abandon his people and join the Scourge as their champion.",
      @"ReplaceableTextures\CommandButtons\BTNHeroDeathKnight.blp")
    {
      _arthas = arthas;
      AddObjective(new ObjectiveCompleteQuest(questDestroyStratholme));
      AddObjective(new ObjectiveEitherOf(new ObjectiveLegendDead(arthas),
        new ObjectiveFactionDefeated(LordaeronSetup.Lordaeron)));
      AddObjective(new ObjectiveSelfExists());
      Required = true;
      ResearchId = Constants.UPGRADE_R01K_QUEST_COMPLETED_THE_CULLING_LORDAERON;
    }

    /// <inheritdoc />
    protected override string RewardFlavour =>
      "Having failed to protect his people, Arthas seizes the cursed runeblade Frostmourne as the instrument of his vengeance. The malevolence of the blade overwhelms him. Arthas is now a loyal Death Knight of the Scourge, and will soon become its greatest champion.";

    /// <inheritdoc />
    protected override string RewardDescription => $"Learn to train {_arthas.Name} from the {GetObjectName(Constants.UNIT_UAOD_ALTAR_OF_DARKNESS)}";

    /// <inheritdoc />
    protected override void OnComplete(Faction completingFaction)
    {
      var arthas = _arthas.Unit;
      if (arthas != null && UnitAlive(arthas))
      {
        arthas
          .Kill()
          .Remove();
        completingFaction.RemovePowerByName("Eye of the Lich King");
      }
    }
  }
}