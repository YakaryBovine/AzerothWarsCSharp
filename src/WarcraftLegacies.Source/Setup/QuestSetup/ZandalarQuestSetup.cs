using MacroTools;
using WarcraftLegacies.Source.Quests.Zandalar;
using WarcraftLegacies.Source.Setup.FactionSetup;

namespace WarcraftLegacies.Source.Setup.QuestSetup
{
  /// <summary>
  /// Responsible for initializing all Zandalar quests
  /// </summary>
  public static class ZandalarQuestSetup
  {
    /// <summary>
    /// Setups up all Zandalar quests
    /// </summary>
    /// <param name="artifactSetup"></param>
    /// <param name="preplacedUnitSystem"></param>
    /// <param name="allLegendSetup"></param>
    public static void Setup(ArtifactSetup artifactSetup, PreplacedUnitSystem preplacedUnitSystem, AllLegendSetup allLegendSetup)
    {
      var zandalar = ZandalarSetup.Zandalar;
      var questZulFarrak = new QuestZulfarrak(Regions.Zulfarrak, allLegendSetup.Neutral.Zulfarrak, allLegendSetup.Troll.Zul);
      zandalar.StartingQuest = questZulFarrak;
      zandalar.AddQuest(questZulFarrak);
      zandalar.AddQuest(new QuestZandalar(Regions.ZandalarUnlock, preplacedUnitSystem));
      zandalar.AddQuest(new QuestGundrak(allLegendSetup));
      zandalar.AddQuest(new QuestJinthaAlor(allLegendSetup));
      zandalar.AddQuest(new QuestZulgurub(allLegendSetup));
      zandalar.AddQuest(new QuestHakkar(artifactSetup.ZinRokh));
      zandalar.AddQuest(new QuestZandalarOutpost());
    }
  }
}
