using MacroTools;
using WarcraftLegacies.Source.Quests.Frostwolf;
using WarcraftLegacies.Source.Setup.FactionSetup;

namespace WarcraftLegacies.Source.Setup.QuestSetup
{
  public static class FrostwolfQuestSetup
  {
    public static void Setup(PreplacedUnitSystem preplacedUnitSystem, ArtifactSetup artifactSetup, AllLegendSetup allLegendSetup)
    {
      var frostwolf = FrostwolfSetup.Frostwolf;
      if (frostwolf == null) 
        return;
      frostwolf.StartingQuest = frostwolf.AddQuest(new QuestThunderBluff(preplacedUnitSystem, Regions.ThunderBluff));
      frostwolf.AddQuest(new QuestStonemaul(preplacedUnitSystem, Regions.StonemaulKeep));
      frostwolf.AddQuest(new QuestDarkspear());
      frostwolf.AddQuest(new QuestRagetotem(allLegendSetup.Frostwolf.Cairne));
      frostwolf.AddQuest(new QuestHighmountain(allLegendSetup.Frostwolf.Cairne, Regions.Highmountain_Unlock));
      frostwolf.AddQuest(new QuestMammoth(allLegendSetup.Frostwolf.Rexxar));
      frostwolf.AddQuest(new QuestDrektharsSpellbook(allLegendSetup.Druids.Nordrassil, allLegendSetup.Frostwolf.Thrall));
      frostwolf.AddQuest(new QuestFreeNerzhul(allLegendSetup.Scourge.TheFrozenThrone, allLegendSetup.Frostwolf.Thrall));
      frostwolf.AddQuest(new QuestWorldShaman(allLegendSetup.Frostwolf.Thrall));
      frostwolf.AddQuest(new QuestScepterOfTheQueenWarsong(Regions.TheAthenaeum, artifactSetup.ScepterOfTheQueen, allLegendSetup.Sentinels.Auberdine));
    }
  }
}