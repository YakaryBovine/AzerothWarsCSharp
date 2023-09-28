using WarcraftLegacies.Source.Quests.Goblin;
using WarcraftLegacies.Source.Setup.FactionSetup;

namespace WarcraftLegacies.Source.Setup.QuestSetup
{
  public static class GoblinQuestSetup
  {
    public static void Setup(AllLegendSetup allLegendSetup)
    {
      var goblin = GoblinSetup.Goblin;
      goblin.StartingQuest = goblin.AddQuest(new QuestKezan());
      goblin.AddQuest(new QuestExplosiveEngineering());
      goblin.AddQuest(new QuestRatchet());
      goblin.AddQuest(new QuestWesternExpansion(new [] { allLegendSetup.Sentinels.Auberdine, allLegendSetup.Sentinels.Feathermoon }));
      goblin.AddQuest(new QuestLumberMarket(allLegendSetup.Druids.Nordrassil));
      goblin.AddQuest(new QuestBusinessExpansion());
      goblin.AddQuest(new QuestGoblinEmpire());
    }
  }
}