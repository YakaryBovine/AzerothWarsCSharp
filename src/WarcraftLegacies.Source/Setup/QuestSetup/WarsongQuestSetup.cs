using MacroTools;
using WarcraftLegacies.Source.Quests.Warsong;
using WarcraftLegacies.Source.Setup.FactionSetup;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Setup.QuestSetup
{
  public static class WarsongQuestSetup
  {
    public static void Setup(PreplacedUnitSystem preplacedUnitSystem, AllLegendSetup allLegendSetup)
    {
      var warsong = WarsongSetup.WarsongClan;
      warsong.StartingQuest = warsong.AddQuest(new QuestOrgrimmar(Regions.Orgrimmar));
      warsong.AddQuest(new QuestCrossroads(Regions.Crossroads, preplacedUnitSystem));
      warsong.AddQuest(new QuestLumberCamp(Regions.LumberCampUnlock, allLegendSetup.Warsong.GromHellscream));
      warsong.AddQuest(new QuestChenStormstout(preplacedUnitSystem.GetUnit(FourCC("Nsjs"))));
      warsong.AddQuest(new QuestFountainOfBlood(allLegendSetup.Neutral.FountainOfBlood, allLegendSetup.Warsong.GromHellscream));
      warsong.AddQuest(new QuestBloodpact(allLegendSetup.Warsong.Mannoroth, allLegendSetup.Warsong.GromHellscream));
      warsong.AddQuest(new QuestGarrosh(allLegendSetup.Druids.TempleOfTheMoon));
      warsong.AddQuest(new QuestWarsongKillDruids(allLegendSetup.Druids.Nordrassil, allLegendSetup.Warsong.GromHellscream));
      warsong.AddQuest(new QuestMoreWyverns(allLegendSetup.Sentinels.Feathermoon, allLegendSetup.Sentinels.Auberdine));
      warsong.AddQuest(new QuestWarsongHold());
      warsong.AddQuest(new QuestWarsongOutpost());
    }
  }
}