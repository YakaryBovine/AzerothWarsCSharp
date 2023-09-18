using MacroTools;
using WarcraftLegacies.Source.Quests.Quelthalas;
using WarcraftLegacies.Source.Quests.Sunfury;
using WarcraftLegacies.Source.Setup.FactionSetup;

namespace WarcraftLegacies.Source.Setup.QuestSetup
{
  public static class SunfuryQuestSetup
  {
    public static void Setup(ArtifactSetup artifactSetup, PreplacedUnitSystem preplacedUnitSystem, AllLegendSetup allLegendSetup)
    {
      var sunfury = SunfurySetup.Sunfury;
      var newQuest = sunfury.AddQuest(new QuestTempestKeep(Regions.TempestKeep, Regions.Biodome1, Regions.Biodome2, Regions.Biodome3));
      sunfury.StartingQuest = newQuest;

      sunfury.AddQuest(new QuestArea52(Regions.Area52Unlock));
      sunfury.AddQuest(new QuestUpperNetherstorm(Regions.UpperNetherstorm));
      sunfury.AddQuest(new QuestSolarian(artifactSetup.EssenceofMurmur));
      sunfury.AddQuest(new QuestSummonKil(allLegendSetup.Stormwind.StormwindKeep, allLegendSetup.Neutral.Karazhan, allLegendSetup.Quelthalas.Kael));
      sunfury.AddQuest(new QuestForgottenKnowledge());
      sunfury.AddQuest(new QuestWellOfEternity(preplacedUnitSystem, allLegendSetup.Quelthalas.Kiljaeden));
    }
  }
}