using System.Collections.Generic;
using MacroTools;
using WarcraftLegacies.Source.Mechanics.Scourge.Plague;
using WarcraftLegacies.Source.Quests.Scourge;
using WarcraftLegacies.Source.Setup.FactionSetup;
using WCSharp.Shared.Data;

namespace WarcraftLegacies.Source.Setup.QuestSetup
{
  public static class ScourgeQuestSetup
  {
    public static void Setup(PreplacedUnitSystem preplacedUnitSystem, ArtifactSetup artifactSetup, AllLegendSetup allLegendSetup)
    {
      QuestSpiderWar questSpiderWar = new(Regions.Ice_Crown,
        preplacedUnitSystem.GetUnit(Constants.UNIT_N074_QUEEN_NEZAR_AZRET));
      QuestKelthuzadLich questKelthuzadLich = new(allLegendSetup.Quelthalas.Sunwell, allLegendSetup.Scourge.Kelthuzad);
      QuestKelthuzadDies questKelthuzadDies = new(questKelthuzadLich, allLegendSetup.Scourge.Kelthuzad);
      QuestDrakUnlock questDrakUnlock = new(Regions.DrakUnlock, allLegendSetup.Neutral.DraktharonKeep);

      var plagueParameters = new PlagueParameters();
      plagueParameters.PlagueRects = new List<Rectangle>
      {
        Regions.Plague_1,
        Regions.Plague_2,
        Regions.Plague_3,
        Regions.Plague_4,
        Regions.Plague_5,
        Regions.Plague_6,
        Regions.Plague_7
      };
      plagueParameters.PlagueCauldronSummonParameters = new List<PlagueCauldronSummonParameter>
      {
        new(2, Constants.UNIT_UNEC_NECROMANCER_SCOURGE),
        new(2, Constants.UNIT_UACO_ACOLYTE_SCOURGE_WORKER),
        new(3, Constants.UNIT_UGHO_GHOUL_SCOURGE),
        new(2, Constants.UNIT_UCRY_CRYPT_FIEND_SCOURGE),
        new(1, Constants.UNIT_UABO_ABOMINATION_SCOURGE),
      };
      plagueParameters.PlagueCauldronUnitTypeId = Constants.UNIT_H02W_PLAGUE_CAULDRON_SCOURGE_OTHER;
      plagueParameters.Duration = 360;
      plagueParameters.AttackTargets = new List<Point>
      {
        new Point(9041, 8036),
        new Point(13825, 12471),
        new Point(9418, 5396)
      };

      QuestPlague questPlague = new(plagueParameters, preplacedUnitSystem, LordaeronSetup.Lordaeron, LegionSetup.Legion);

      ScourgeSetup.Scourge.AddQuest(new QuestScouringofLordaeron(
        new[]
        {
          allLegendSetup.Lordaeron.CapitalPalace}, allLegendSetup.Lordaeron.Uther));

      QuestSapphiron questSapphiron = new(preplacedUnitSystem.GetUnit(Constants.UNIT_UBDR_SAPPHIRON_CREEP), allLegendSetup.Scourge.Kelthuzad);
      QuestDestroyStratholme questDestroyStratholme = new(allLegendSetup.Lordaeron.Stratholme, allLegendSetup.Lordaeron.Arthas);
      QuestCorruptArthas questCorruptArthas = new(questDestroyStratholme, allLegendSetup.Lordaeron.Arthas);
      QuestLichKingArthas questLichKingArthas =
        new(preplacedUnitSystem.GetUnit(Constants.UNIT_H00O_UTGARDE_KEEP_SCOURGE_OTHER), artifactSetup.HelmOfDomination,
          allLegendSetup.Scourge.Arthas, allLegendSetup.Scourge.TheFrozenThrone);

      //Setup
      if (ScourgeSetup.Scourge != null)
      {
        ScourgeSetup.Scourge.AddQuest(questSpiderWar);
        ScourgeSetup.Scourge.StartingQuest = questSpiderWar;
        ScourgeSetup.Scourge.AddQuest(questDrakUnlock);
        ScourgeSetup.Scourge.AddQuest(questPlague);
        ScourgeSetup.Scourge.AddQuest(questSapphiron);
        //Early duel
        ScourgeSetup.Scourge.AddQuest(questDestroyStratholme);
        ScourgeSetup.Scourge.AddQuest(questCorruptArthas);
        ScourgeSetup.Scourge.AddQuest(questKelthuzadLich);
        ScourgeSetup.Scourge.AddQuest(questKelthuzadDies);
        //Misc
        ScourgeSetup.Scourge.AddQuest(questLichKingArthas);
      }
    }
  }
}