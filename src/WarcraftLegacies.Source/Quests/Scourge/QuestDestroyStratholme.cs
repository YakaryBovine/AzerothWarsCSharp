using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.ObjectiveSystem.Objectives.QuestBased;
using MacroTools.QuestSystem;
using WarcraftLegacies.Source.Quests.Lordaeron;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Scourge
{
  public sealed class QuestDestroyStratholme : QuestData
  {
    private readonly Faction _lordaeron;
    private readonly LegendaryHero _arthas;
    
    public QuestDestroyStratholme(Faction lordaeron, Capital stratholme, LegendaryHero arthas) : base("The Culling",
      "When the city of Stratholme falls, Prince Arthas' despair will make him more susceptible to the power of the Lich King.",
      @"ReplaceableTextures\CommandButtons\BTNRuneblade.blp")
    {
      _lordaeron = lordaeron;
      _arthas = arthas;
      AddObjective(new ObjectiveCapitalDead(stratholme));
      var lineOfSuccession = lordaeron.GetQuestByType<QuestKingArthas>();
      AddObjective(new ObjectiveFactionQuestNotComplete(lineOfSuccession, lordaeron));
      ResearchId = Constants.UPGRADE_R01K_QUEST_COMPLETED_THE_CULLING;
      
    }

    /// <inheritdoc />
    public override string RewardFlavour =>
      "Having failed to protect his people, Arthas seizes the cursed runeblade Frostmourne as the instrument of his vengeance. The malevolence of the blade overwhelms him. Arthas is now a loyal Death Knight of the Scourge, and will soon become its greatest champion.";

    /// <inheritdoc />
    protected override string RewardDescription =>
      $"Arthas abandons Lordaeron to join the Scourge; learn to train {_arthas.Name} from the {GetObjectName(Constants.UNIT_UAOD_ALTAR_OF_DARKNESS_SCOURGE_ALTAR)}";

    /// <inheritdoc />
    protected override void OnComplete(Faction completingFaction)
    {
      var arthas = _arthas.Unit;
      
      _lordaeron.ModObjectLimit(Constants.UNIT_HART_CROWN_PRINCE_OF_LORDAERON_LORDAERON, -1);

      if (arthas == null || !UnitAlive(arthas))
        return;

      arthas.Kill();
      arthas.Remove();
    }
  }
}