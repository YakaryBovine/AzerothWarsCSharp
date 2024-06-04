using System.Collections.Generic;
using System.Linq;
using MacroTools;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.Powers;
using WarcraftLegacies.Shared.Config.Factions;
using WarcraftLegacies.Source.Quests.Draenei;
using WarcraftLegacies.Source.Setup;

namespace WarcraftLegacies.Source.Factions
{
  public sealed class Draenei : Faction
  {
    private readonly PreplacedUnitSystem _preplacedUnitSystem;
    private readonly AllLegendSetup _allLegendSetup;

    /// <inheritdoc />
    
    public Draenei(PreplacedUnitSystem preplacedUnitSystem, AllLegendSetup allLegendSetup) : base("The Exodar",
      PLAYER_COLOR_NAVY, "|cff000080", @"ReplaceableTextures\CommandButtons\BTNBOSSVelen.blp")
    {
      TraditionalTeam = TeamSetup.NightElves;
      _preplacedUnitSystem = preplacedUnitSystem;
      _allLegendSetup = allLegendSetup;
      StartingGold = 200;
      ControlPointDefenderUnitTypeId = UNIT_U008_CONTROL_POINT_DEFENDER_DRAENEI;
      IntroText = @"You are playing as the exiled |cff000080Draenei|r.

You begin on Azuremyst Island, amid the wreckage of your flight from the Burning Legion.

Further inland your Night-elf allies will need your help against the Orcish Horde, quickly build your base and gain entry to the Exodar.

The Exodar is a mighty fortress-base with the ability to move around the map, but it will take a long time to repair.";
      Nicknames = new List<string>
      {
        "draenei",
        "dranei",
        "exo",
        "exodar",
        "theexodar",
        "goats"
      };
    }
    
    /// <inheritdoc />
    public override void OnRegistered()
    {
      RegisterObjectLimits();
      RegisterQuests();
      RegisterPowers();
      SharedFactionConfigSetup.AddSharedFactionConfig(this);
    }

    private void RegisterObjectLimits()
    {
      foreach (var (objectTypeId, objectLimit) in DraeneiObjectLimitData.GetAllObjectLimits())
        ModObjectLimit(FourCC(objectTypeId), objectLimit);
    }

    private void RegisterQuests()
    {
      var questRepairHull = new QuestRepairExodarHull(Regions.ExodarBaseUnlock, _allLegendSetup.Draenei.LegendExodar);
      StartingQuest = questRepairHull;
      AddQuest(questRepairHull);
      AddQuest(new QuestShipArgus(
        _preplacedUnitSystem.GetUnit(UNIT_H03V_ENTRANCE_PORTAL, Regions.OutlandToArgus.Center),
        _preplacedUnitSystem.GetUnit(UNIT_H03V_ENTRANCE_PORTAL, Regions.TempestKeepSpawn.Center),
        _allLegendSetup.Draenei.Velen
      ));
      var crystalProtectors = CreateGroup()
        .EnumUnitsInRect(Regions.ExodarBaseUnlock.Rect)
        .EmptyToList()
        .Where(x => GetUnitTypeId(x) == UNIT_U00U_CRYSTAL_PROTECTOR_DRAENEI_TOWER);
      var questRepairGenerator = new QuestRepairGenerator(_allLegendSetup.Draenei.LegendExodarGenerator, questRepairHull, crystalProtectors);
      AddQuest(questRepairGenerator);
      AddQuest(new QuestTriumvirate(_allLegendSetup.Draenei.Velen));
      var questDimensionalShip = new QuestDimensionalShip(Regions.ExodarBaseUnlock, questRepairGenerator, _allLegendSetup.Draenei.LegendExodarGenerator);
      AddQuest(questDimensionalShip);
    }

    private void RegisterPowers()
    {
      var dummyPower = new DummyPower("Crystallization",
        "Arcane Wells placed directly near Divine Citadels will generate mana for them over time. You can then convert that mana into units. The maximum number of Arcane Well around a Divine Citadel is 12 if placed optimally",
        "ManaGem.blp");
      AddPower(dummyPower);
    }
  }
}