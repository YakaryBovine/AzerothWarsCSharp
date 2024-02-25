using MacroTools.ObjectiveSystem.Objectives.TimeBased;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using MacroTools.QuestSystem;
using static War3Api.Common;

namespace TestMap.Source.Quests
{
  public sealed class SharedQuest : QuestData
  {
    public SharedQuest() : base("Tomb of Sargeras",
      "When the Guardian Aegwynn defeated the fallen Titan Sargeras, she sealed his corpse within the temple that would come to be known as the Tomb of Sargeras. It lies still there, tempting those with the ambition to seize the power that remains within.",
      @"ReplaceableTextures\CommandButtons\BTNUnholyFrenzy.blp")
    {
      AddObjective(new ObjectiveTime(35));
      AddObjective(new ObjectiveKillXUnit(FourCC("hfoo"), 4));
    }
    
    protected override string RewardDescription => "The Tomb of Sargeras has been opened.";

    public override string RewardFlavour => "The Tomb of Sargeras has been opened.";
  }
}