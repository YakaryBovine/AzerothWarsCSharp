using MacroTools.Extensions;
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
    }
    
    /// <inheritdoc/>
    public override string RewardFlavour => "The Spire has been pillaged. A secret demon gate has now been formed inside.";

    /// <inheritdoc/>
    protected override string RewardDescription => "Learn to train troops from the Spire Keep and gain 500 gold";

    /// <inheritdoc/>
    protected override void OnComplete(Faction whichFaction) => whichFaction.Player?.AddGold(500);
  }
}