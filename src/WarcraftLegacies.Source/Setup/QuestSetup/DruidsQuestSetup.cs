using MacroTools.QuestSystem;
using WarcraftLegacies.Source.Quests.Druids;

namespace WarcraftLegacies.Source.Setup.QuestSetup
{
  /// <summary>
  /// Responsible for setting up all Druid <see cref="QuestData"/>s.
  /// </summary>
  public static class DruidsQuestSetup
  {
    /// <summary>
    /// Sets up <see cref="DruidsQuestSetup"/>.
    /// </summary>
    public static void Setup(ArtifactSetup artifactSetup, AllLegendSetup allLegendSetup)
    {
      var druids = FactionSetup.DruidsSetup.Druids;
      var newQuest = druids.AddQuest(new QuestMalfurionAwakens(Regions.MoongladeVillage, Regions.TeldrassilUnlock, Regions.CenarionHoldUnlock,
        allLegendSetup.Druids.Nordrassil.Unit, artifactSetup.HornOfCenarius,
        allLegendSetup.Druids.Malfurion));
      druids.StartingQuest = newQuest;
      druids.AddQuest(new QuestShrineBase(Regions.ShrineBaseUnlock));
      druids.AddQuest(new QuestRiseBase(Regions.RiseBaseUnlock));
      druids.AddQuest(new QuestAshenvale(Regions.AshenvaleUnlock));
      druids.AddQuest(new QuestDruidsKillFrostwolf(allLegendSetup.Frostwolf.ThunderBluff));
      druids.AddQuest(new QuestDruidsKillWarsong());
      druids.AddQuest(new QuestAndrassil(allLegendSetup.Scourge.TheFrozenThrone, allLegendSetup.Druids.Vordrassil));
      druids.AddQuest(new QuestShaladrassil(allLegendSetup.Neutral.Shaladrassil));
      druids.AddQuest(new QuestTortolla(allLegendSetup.Druids.Tortolla));
    }
  }
}