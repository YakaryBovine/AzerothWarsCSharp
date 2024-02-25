using MacroTools.ObjectiveSystem.Objectives.QuestBased;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using MacroTools.QuestSystem;
using static War3Api.Common;

namespace TestMap.Source.Quests
{
  public sealed class ExampleQuestB : QuestData
  {
    public ExampleQuestB(QuestData otherQuest) : base("Better Quest", "Don't complete that other quest, he sucks.", "ReplaceableTextures\\CommandButtons\\BTNPaladin.blp")
    {
      AddObjective(new ObjectiveQuestNotComplete(otherQuest));
      AddObjective(new ObjectiveBuild(FourCC("hbar"), 1));
    }

    protected override string RewardDescription => "No idea.";
    public override string RewardFlavour => "Thanks for completing me instead of that other quest!";
  }
}