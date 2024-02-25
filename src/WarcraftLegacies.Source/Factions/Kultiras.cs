using System.Collections.Generic;
using MacroTools;
using MacroTools.DialogueSystem;
using MacroTools.FactionSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using WarcraftLegacies.Source.Quests.KulTiras;
using WarcraftLegacies.Source.Setup;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Factions
{
  public sealed class Kultiras : Faction
  {
    private readonly AllLegendSetup _allLegendSetup;
    private readonly unit _proudmooreCapitalShip;

    /// <inheritdoc />
    public Kultiras(PreplacedUnitSystem preplacedUnitSystem, AllLegendSetup allLegendSetup) : base("Kul'tiras",
      PLAYER_COLOR_EMERALD, "|cff00781e", @"ReplaceableTextures\CommandButtons\BTNProudmoore.blp")
    {
      _allLegendSetup = allLegendSetup;
      _proudmooreCapitalShip = preplacedUnitSystem.GetUnit(Constants.UNIT_H05V_PROUDMOORE_FLAGSHIP_KUL_TIRAS);
      StartingGold = 200;
      StartingLumber = 700;
      ControlPointDefenderUnitTypeId = Constants.UNIT_H09W_CONTROL_POINT_DEFENDER_KUL_TIRAS;
      IntroText = @"You are playing as the maritime |cff008000Kingdom of Kul'tiras|r.

You begin on Balor island, separated from your main forces in Kul Tiras. Unite your forces by eliminating your enemies in Tiragarde, Drustvar and Stormsong Valley.

Stormwind is preparing for an invasion through the Dark Portal in the South. Muster the Admiralty and help them, or you may lose your strongest ally.";
      GoldMines = new List<unit>
      {
        preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(4585, -13038))
      };
      
      RegisterFactionDependentInitializer<Frostwolf>(RegisterDialogue);
    }

    /// <inheritdoc />
    public override void OnRegistered()
    {
      RegisterObjectLimits();
      RegisterQuests();
      SharedFactionConfigSetup.AddSharedFactionConfig(this);
    }

    private void RegisterObjectLimits()
    {
      SharedFactionConfigSetup.AddSharedFactionConfig(this);

      ModObjectLimit(FourCC("h062"), UNLIMITED); //Town Hall
      ModObjectLimit(FourCC("h064"), UNLIMITED); //Keep
      ModObjectLimit(FourCC("h06I"), UNLIMITED); //Castle
      ModObjectLimit(FourCC("h07N"), UNLIMITED); //Farm
      ModObjectLimit(FourCC("h07M"), UNLIMITED); //Altar
      ModObjectLimit(FourCC("h07R"), UNLIMITED); //Scout Tower
      ModObjectLimit(FourCC("h07S"), UNLIMITED); //Guard Tower
      ModObjectLimit(FourCC("h07T"), UNLIMITED); //Improved Guard Tower
      ModObjectLimit(FourCC("h07U"), UNLIMITED); //Cannon Tower
      ModObjectLimit(FourCC("h07V"), UNLIMITED); //Improved Cannon Tower
      ModObjectLimit(FourCC("h07O"), UNLIMITED); //Blacksmith
      ModObjectLimit(FourCC("h07Q"), UNLIMITED); //Arcane Sanctum
      ModObjectLimit(FourCC("n07H"), UNLIMITED); //Marketplace
      ModObjectLimit(FourCC("h07W"), UNLIMITED); //Shipyard
      ModObjectLimit(FourCC("h06R"), UNLIMITED); //Garrison
      ModObjectLimit(FourCC("h07P"), UNLIMITED); //Workshop
      ModObjectLimit(FourCC("h093"), UNLIMITED); //Workshop

      //Units
      ModObjectLimit(FourCC("h01E"), UNLIMITED); //Deckhand
      ModObjectLimit(FourCC("e007"), UNLIMITED); //Thornspeaker
      ModObjectLimit(FourCC("n09A"), 12); //Ember Cleric
      ModObjectLimit(FourCC("n09B"), 8); //Witch Hunter
      ModObjectLimit(FourCC("h092"), 4); //Order Inquisitor
      ModObjectLimit(FourCC("h05K"), UNLIMITED); //Tidesage
      ModObjectLimit(FourCC("h041"), UNLIMITED); //Marine
      ModObjectLimit(FourCC("e00B"), UNLIMITED); //Thornspeaker Bear
      ModObjectLimit(FourCC("n009"), 12); //Revenant of the Tides
      ModObjectLimit(FourCC("n07G"), 6); //muskateer
      ModObjectLimit(FourCC("n029"), 12); //Sea Giant
      ModObjectLimit(FourCC("h06J"), UNLIMITED); //Sniper
      ModObjectLimit(FourCC("o01A"), 6); //Cannon Team
      ModObjectLimit(FourCC("h04O"), 12); //Bomber
      ModObjectLimit(FourCC("h04W"), 3); //Siege Tank
      ModObjectLimit(FourCC("h0A0"), 8); //Fusillier

      //Ships
      ModObjectLimit(FourCC("hbot"), UNLIMITED); //Alliance Transport Ship
      ModObjectLimit(FourCC("h0AR"), UNLIMITED); //Alliance Scout
      ModObjectLimit(FourCC("h0AX"), UNLIMITED); //Alliance Frigate
      ModObjectLimit(FourCC("h0B3"), UNLIMITED); //Alliance Fireship
      ModObjectLimit(FourCC("h0B0"), UNLIMITED); //Alliance Galley
      ModObjectLimit(FourCC("h0B6"), UNLIMITED); //Alliance Boarding
      ModObjectLimit(FourCC("h0AN"), UNLIMITED); //Alliance Juggernaut
      ModObjectLimit(FourCC("h0B7"), 6); //Alliance Bombard

      //Upgrades
      ModObjectLimit(FourCC("R001"), UNLIMITED); //Rising Tides
      ModObjectLimit(FourCC("R000"), UNLIMITED); //Tidesage Adept Training
      ModObjectLimit(FourCC("R01O"), UNLIMITED); //Crushing Wave
      ModObjectLimit(FourCC("R01T"), UNLIMITED); //Cluster Rockets
      ModObjectLimit(FourCC("R01U"), UNLIMITED); //Improved Barrage
      ModObjectLimit(FourCC("R05G"), UNLIMITED); //Thornspeaker Training
      ModObjectLimit(FourCC("Rhlh"), UNLIMITED); //Improved Lumber Harvesting
      ModObjectLimit(FourCC("Rhac"), UNLIMITED); //Improved Masonry
      ModObjectLimit(FourCC("R08B"), UNLIMITED); //Long Rifles
      ModObjectLimit(FourCC("R05J"), UNLIMITED); //Expedition

      //Heroes
      ModObjectLimit(Constants.UNIT_HAPM_LORD_ADMIRAL_OF_KUL_TIRAS_KUL_TIRAS, 1);
      ModObjectLimit(Constants.UNIT_H05L_LADY_OF_HOUSE_PROUDMOORE_KUL_TIRAS, 1);
      ModObjectLimit(Constants.UNIT_U026_MATRIARCH_OF_HOUSE_WAYCREST_KULTIRAS, 1);
    }

    private void RegisterQuests()
    {
      StartingQuest = AddQuest(new QuestBoralus(Regions.Kultiras));
      AddQuest(new QuestUnlockShip(Regions.ShipAmbient, _proudmooreCapitalShip, _allLegendSetup.Kultiras.LegendBoralus,
        _allLegendSetup.Kultiras.LegendAdmiral));
      AddQuest(new QuestOldHatreds(_allLegendSetup.Kultiras.LegendAdmiral));
      AddQuest(new QuestWestfallOutpost(Regions.StranglethornBaseBuild));
      AddQuest(new QuestHighBank(Regions.HighbankUnlock, _allLegendSetup.Kultiras.LegendKatherine));
      AddQuest(new QuestBeyondPortal(_allLegendSetup.FelHorde.HellfireCitadel,
        _allLegendSetup.FelHorde.KilsorrowFortress));
    }

    private void RegisterDialogue(Frostwolf frostwolf)
    {
      TriggeredDialogueManager.Add(
        new TriggeredDialogue(new DialogueSequence(new Dialogue(
              @"Sound\Dialogue\OrcExpCamp\OrcQuest20x\D20Proudmoore05",
              "I must admit, you orcs are more tenacious than I remembered. I thought you savages would have turned on each other by now.",
              "Daelin Proudmoore"),
            new Dialogue(
              @"Sound\Dialogue\OrcExpCamp\OrcQuest20x\D20Thrall06",
              "This is not the Horde you remember, old man. We have no interest in conquest or murder. We have paid for our sins of our forebears in blood.",
              "Thrall"),
            new Dialogue(
              @"Sound\Dialogue\OrcExpCamp\OrcQuest20x\D20Proudmoore07",
              "Can your blood atone for genocide, orc? Your Horde killed countless innocents with its rampage across Stormwind and Lordaeron. Do you really think you can just sweep all that away and cast aside your guilt so easily? No, your kind will never change, and I will never stop fighting you.",
              "Daelin Proudmoore"))
          , new Faction[]
          {
            this,
            frostwolf
          }, new[]
          {
            new ObjectiveLegendMeetsLegend(_allLegendSetup.Kultiras.LegendAdmiral, _allLegendSetup.Frostwolf.Thrall)
          }));
    }
  }
}