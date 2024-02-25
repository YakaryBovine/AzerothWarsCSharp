using System.Linq;
using MacroTools;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.Powers;
using WarcraftLegacies.Source.Quests.Draenei;
using WarcraftLegacies.Source.Setup;
using static War3Api.Common;

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
      _preplacedUnitSystem = preplacedUnitSystem;
      _allLegendSetup = allLegendSetup;
      StartingGold = 200;
      StartingLumber = 700;
      ControlPointDefenderUnitTypeId = Constants.UNIT_U008_CONTROL_POINT_DEFENDER_DRAENEI;
      IntroText = @"You are playing as the exiled |cff000080Draenei|r.

You begin on Azuremyst Island, amid the wreckage of your flight from the Burning Legion.

Further inland your Night-elf allies will need your help against the Orcish Horde, quickly build your base and gain entry to the Exodar.

The Exodar is a mighty fortress-base with the ability to move around the map, but it will take a long time to repair.";
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
      ModObjectLimit(FourCC("o02P"), UNLIMITED); //Crystal Hall
      ModObjectLimit(FourCC("o050"), UNLIMITED); //Metropolis
      ModObjectLimit(FourCC("o051"), UNLIMITED); //Divine Citadel
      ModObjectLimit(FourCC("o058"), UNLIMITED); //Altar of Light
      ModObjectLimit(FourCC("o052"), UNLIMITED); //Ceremonial Altar
      ModObjectLimit(FourCC("o053"), UNLIMITED); //Smithery
      ModObjectLimit(FourCC("o054"), UNLIMITED); //Astral Sanctum
      ModObjectLimit(FourCC("o055"), UNLIMITED); //Crystal Spire
      ModObjectLimit(FourCC("o056"), 48); //Arcane Well
      ModObjectLimit(FourCC("o057"), UNLIMITED); //Vaults of Relic
      ModObjectLimit(FourCC("u00U"), UNLIMITED); //Crystal Protector
      ModObjectLimit(FourCC("u01Q"), UNLIMITED); //Crystal Protector improved
      ModObjectLimit(FourCC("o059"), UNLIMITED); //Improved Ancient Protector
      ModObjectLimit(FourCC("o05U"), UNLIMITED); //Lightforged Gateway

      ModObjectLimit(FourCC("o05A"), UNLIMITED); //Wisp
      ModObjectLimit(FourCC("o05B"), UNLIMITED); //Defender
      ModObjectLimit(FourCC("h09T"), UNLIMITED); //Rangari
      ModObjectLimit(FourCC("e01K"), 3); //Polybolos
      ModObjectLimit(FourCC("o05D"), UNLIMITED); //Elementalist
      ModObjectLimit(FourCC("o05C"), UNLIMITED); //Luminarch
      ModObjectLimit(FourCC("h09R"), 6); //Vindicator
      ModObjectLimit(FourCC("nmdr"), UNLIMITED); //Elekk
      ModObjectLimit(FourCC("h09U"), 4); //Elekk Knight
      ModObjectLimit(FourCC("u02H"), 6); //Nether Ray

      ModObjectLimit(FourCC("n0BJ"), 6); //Sharpshooter
      ModObjectLimit(FourCC("n0BP"), 4); //Juggernaut
      ModObjectLimit(FourCC("n0BM"), 8); //Nether Ray

      //Ships
      ModObjectLimit(FourCC("etrs"), UNLIMITED); //Night Elf Transport Ship
      ModObjectLimit(FourCC("h0AU"), UNLIMITED); // Scout
      ModObjectLimit(FourCC("h0AV"), UNLIMITED); // Frigate
      ModObjectLimit(FourCC("h0B1"), UNLIMITED); // Fireship
      ModObjectLimit(FourCC("h057"), UNLIMITED); // Galley
      ModObjectLimit(FourCC("h0B4"), UNLIMITED); // Boarding
      ModObjectLimit(FourCC("h0BA"), UNLIMITED); // Juggernaut
      ModObjectLimit(FourCC("h0B8"), 6); // Bombard

      ModObjectLimit(FourCC("H09S"), 1); //Maraad
      ModObjectLimit(FourCC("E01I"), 1); //Velen
      ModObjectLimit(FourCC("E01J"), 1); //Nobundo
      ModObjectLimit(FourCC("H09M"), 1); //Adal

      ModObjectLimit(FourCC("R078"), UNLIMITED); //Elementalist training
      ModObjectLimit(FourCC("R07C"), UNLIMITED); //Luminarch training
    }

    private void RegisterQuests()
    {
      var questRepairHull = new QuestRepairExodarHull(Regions.ExodarBaseUnlock, _allLegendSetup.Draenei.LegendExodar);
      StartingQuest = questRepairHull;
      AddQuest(questRepairHull);
      AddQuest(new QuestRebuildCivilisation(Regions.DesolaceUnlock, _allLegendSetup.Draenei.Velen));
      AddQuest(new QuestShipArgus(
        _preplacedUnitSystem.GetUnit(Constants.UNIT_H03V_ENTRANCE_PORTAL, Regions.OutlandToArgus.Center),
        _preplacedUnitSystem.GetUnit(Constants.UNIT_H03V_ENTRANCE_PORTAL, Regions.TempestKeepSpawn.Center),
        _allLegendSetup.Draenei.Velen
      ));
      var crystalProtectors = CreateGroup()
        .EnumUnitsInRect(Regions.ExodarBaseUnlock.Rect)
        .EmptyToList()
        .Where(x => GetUnitTypeId(x) == Constants.UNIT_U00U_CRYSTAL_PROTECTOR_DRAENEI_TOWER);
      var questRepairGenerator = new QuestRepairGenerator(_allLegendSetup.Draenei.LegendExodarGenerator, questRepairHull, crystalProtectors);
      AddQuest(questRepairGenerator);
      AddQuest(new QuestTriumvirate(_allLegendSetup.Draenei.Velen));
      var questDimensionalShip = new QuestDimensionalShip(Regions.ExodarBaseUnlock, questRepairGenerator, _allLegendSetup.Draenei.LegendExodarGenerator);
      AddQuest(questDimensionalShip);
    }

    private void RegisterPowers()
    {
      var dummyPower = new DummyPower("Crystallization",
        "Arcane Wells placed directly near Divine Citadels will generate mana for them over time. You can then convert that mana into units. The maximum number of Arcane Well around a Divine Citadel is 12 if placed optimally");
      AddPower(dummyPower);
    }
  }
}