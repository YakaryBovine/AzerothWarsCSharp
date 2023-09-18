using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.QuestSystem;

namespace WarcraftLegacies.Source.Quests.Legion
{
  public sealed class QuestControlSpire : QuestData
  {
    public QuestControlSpire(Capital spire) : base("Windrunner Spire",
      "The seat of the Windrunners, pillaging it would yield a great bounty and be the perfect grounds for a demon gate.",
      @"ReplaceableTextures\CommandButtons\BTNElvenScoutTower.blp")
    {
      AddObjective(new ObjectiveControlCapital(spire, false));
      ResearchId = Constants.UPGRADE_R04H_QUEST_COMPLETED_ANY_OF_THE_THREE_INFILTRATION_QUESTS;
    }
    
    /// <inheritdoc/>
    protected override string RewardFlavour => "The Spire has been pillaged. A secret demon gate has now been formed inside.";

    /// <inheritdoc/>
    protected override string RewardDescription => "Unlock the Spire as a troop production building and 500 gold. Completing any of the three infiltration quest will enable the Alterac Portal to be opened.";

    /// <inheritdoc/>
    protected override void OnComplete(Faction whichFaction)
    {
      whichFaction.Gold += 500;
    }
  }
}