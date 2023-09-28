using MacroTools;
using MacroTools.FactionSystem;
using MacroTools.QuestSystem;
using WarcraftLegacies.Source.Quests.Lordaeron;
using WarcraftLegacies.Source.Setup.FactionSetup;

namespace WarcraftLegacies.Source.Setup.QuestSetup
{
  /// <summary>
  /// Setup of all quests for the Lordaeron <see cref="Faction"/>
  /// </summary>
  public static class LordaeronQuestSetup
  {
    public static void Setup(PreplacedUnitSystem preplacedUnitSystem, ArtifactSetup artifactSetup, AllLegendSetup allLegendSetup)
    {
      var questStrahnbrad = new QuestStrahnbrad(Regions.StrahnbradUnlock);
      var questStratholme = new QuestStratholme(Regions.StratholmeUnlock, preplacedUnitSystem, allLegendSetup.Lordaeron.Arthas, allLegendSetup.Lordaeron.Stratholme);
      LordaeronSetup.Lordaeron.AddQuest(new QuestHearthglen(Regions.Hearthglen));
      LordaeronSetup.Lordaeron.AddQuest(questStratholme);
      LordaeronSetup.Lordaeron.StartingQuest = questStratholme;
      LordaeronSetup.Lordaeron.AddQuest(questStrahnbrad);
      LordaeronSetup.Lordaeron.AddQuest(new QuestCapitalCity(Regions.Terenas, allLegendSetup.Lordaeron.Terenas.Unit,
        allLegendSetup.Lordaeron.Uther, allLegendSetup.Lordaeron.Arthas, allLegendSetup.Neutral.Caerdarrow, allLegendSetup.Lordaeron.CapitalPalace,
        new QuestData[]
        {
          questStrahnbrad,
          questStratholme
        }));
      LordaeronSetup.Lordaeron.AddQuest(new QuestTyrHand(allLegendSetup.Lordaeron.CapitalPalace, allLegendSetup.Lordaeron.Stratholme, Regions.TyrUnlock, allLegendSetup.Scourge.TheFrozenThrone));
      LordaeronSetup.Lordaeron.AddQuest(new QuestMograine());
      LordaeronSetup.Lordaeron.AddQuest(new QuestShoresOfNorthrend(allLegendSetup.Lordaeron.Arthas, allLegendSetup.Neutral.Caerdarrow));
      LordaeronSetup.Lordaeron.AddQuest(new QuestThunderEagle(allLegendSetup.Neutral.DraktharonKeep));
      LordaeronSetup.Lordaeron.AddQuest(new QuestKingArthas(allLegendSetup.Lordaeron.Terenas.Unit,
        artifactSetup.CrownOfLordaeron, allLegendSetup.Lordaeron.CapitalPalace, allLegendSetup.Lordaeron.Arthas,
        allLegendSetup.Scourge.TheFrozenThrone));
      LordaeronSetup.Lordaeron.AddQuest(new QuestKingdomOfManLordaeron(artifactSetup.CrownOfLordaeron,
        artifactSetup.CrownOfStormwind, allLegendSetup.Lordaeron.Arthas));
    }
  }
}