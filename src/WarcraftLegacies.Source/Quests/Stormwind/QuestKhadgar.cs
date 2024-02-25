using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.QuestSystem;

namespace WarcraftLegacies.Source.Quests.Stormwind
{
  /// <summary>
  /// When Black Temple is destroyed, Stormwind can train Khadgar.
  /// </summary>
  public sealed class QuestKhadgar : QuestData
  {

    public QuestKhadgar(Capital blackTemple) : base("Keeper of the Eternal Watch",
      "At the end of the Second War, Khadgar remained in Draenor to seal the Dark Portal, effectively ending the conflict. He has been stranded deep in Outland ever since.",
      @"ReplaceableTextures\CommandButtons\BTNMageWC2.blp")
    {
      AddObjective(new ObjectiveCapitalDead(blackTemple));
      ResearchId = Constants.UPGRADE_R016_QUEST_COMPLETED_KEEPER_OF_THE_ETERNAL_WATCH_STORMWIND;
    }
    
    /// <inheritdoc/>
    public override string RewardFlavour =>
      "Khadgar has been freed from his confines under the Black Temple, and he is now free to serve the Kingdom of Stormwind.";

    /// <inheritdoc/>
    protected override string RewardDescription => "You can summon Khadgar from the Altar of Kings";

  }
}