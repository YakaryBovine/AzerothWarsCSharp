using MacroTools;
using System.Linq;
using MacroTools.Extensions;
using static War3Api.Common;
using WarcraftLegacies.Source.Quests.Draenei;
using WarcraftLegacies.Source.Setup.FactionSetup;

namespace WarcraftLegacies.Source.Setup.QuestSetup
{
  public static class DraeneiQuestSetup
  {
    public static void Setup(PreplacedUnitSystem preplacedUnitSystem, AllLegendSetup allLegendSetup)
    {
      var draenei = DraeneiSetup.Draenei;
      if (draenei == null) 
        return;
      var questRepairHull = new QuestRepairExodarHull(Regions.ExodarBaseUnlock, allLegendSetup.Draenei.LegendExodar);
      draenei.StartingQuest = questRepairHull;
      draenei.AddQuest(questRepairHull);
      draenei.AddQuest(new QuestRebuildCivilisation(Regions.DesolaceUnlock, allLegendSetup.Draenei.Velen));
      draenei.AddQuest(new QuestShipArgus(
        preplacedUnitSystem.GetUnit(Constants.UNIT_H03V_ENTRANCE_PORTAL, Regions.OutlandToArgus.Center),
        preplacedUnitSystem.GetUnit(Constants.UNIT_H03V_ENTRANCE_PORTAL, Regions.TempestKeepSpawn.Center),
        allLegendSetup.Draenei.Velen
      ));
      var crystalProtectors = CreateGroup()
        .EnumUnitsInRect(Regions.ExodarBaseUnlock.Rect)
        .EmptyToList()
        .Where(x => GetUnitTypeId(x) == Constants.UNIT_U00U_CRYSTAL_PROTECTOR_DRAENEI_TOWER);
      var questRepairGenerator = new QuestRepairGenerator(allLegendSetup.Draenei.LegendExodarGenerator, questRepairHull, crystalProtectors);
      draenei.AddQuest(questRepairGenerator);
      draenei.AddQuest(new QuestTriumvirate(allLegendSetup.Draenei.Velen));
      var questDimensionalShip = new QuestDimensionalShip(Regions.ExodarBaseUnlock, questRepairGenerator, allLegendSetup.Draenei.LegendExodarGenerator);
      draenei.AddQuest(questDimensionalShip);
    }
  }
}