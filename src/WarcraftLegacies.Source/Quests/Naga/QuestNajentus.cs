using System.Collections.Generic;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.QuestSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Naga
{
  
  /// <summary>
  /// Capture control points in Nazjatar to unlock a hero
  /// </summary>
  public sealed class QuestNajentus : QuestData
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="QuestNajentus"/> class.
    /// </summary>
    public QuestNajentus(IEnumerable<Capital> capitalTargets) : base("Lord of the Depths",
      "the Naga Lord Naj'entus will only join us if enough blood and destruction was shed in his name",
      @"ReplaceableTextures\CommandButtons\BTNLordNaj.blp")
    {
      foreach (var capital in capitalTargets) 
        AddObjective(new ObjectiveCapitalDead(capital));
      
      ResearchId = Constants.UPGRADE_R08W_QUEST_COMPLETED_LORD_OF_THE_DEPTHS;
      
    }

    /// <inheritdoc/>
    public override string RewardFlavour =>
      "Now that the South Alliance lies in ruin, Lady Vashj's champion Lord Naj'entus has joined Illidan's forces.";

    /// <inheritdoc/>
    protected override string RewardDescription => $" Naj'entus can be trained from the {GetObjectName(Constants.UNIT_NNAD_ALTAR_OF_THE_BETRAYER_ILLIDARI_ALTAR)}";

    /// <inheritdoc/>
    protected override void OnComplete(Faction completingFaction)
    {
      SetPlayerTechResearched(completingFaction.Player, ResearchId, 1);
    }

    /// <inheritdoc/>
    protected override void OnAdd(Faction whichFaction)
    {
      whichFaction.ModObjectLimit(ResearchId, Faction.UNLIMITED);
    }
  }
}