using System.Collections.Generic;
using MacroTools;
using MacroTools.DialogueSystem;
using MacroTools.FactionSystem;
using MacroTools.ObjectiveSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using WarcraftLegacies.Source.Quests.Warsong;
using WarcraftLegacies.Source.Setup;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Factions
{
  public sealed class Warsong : Faction
  {
    private readonly PreplacedUnitSystem _preplacedUnitSystem;
    private readonly AllLegendSetup _allLegendSetup;

    /// <inheritdoc />
    public Warsong(PreplacedUnitSystem preplacedUnitSystem, AllLegendSetup allLegendSetup) : base("Warsong", PLAYER_COLOR_ORANGE, "|c00ff8000",
      @"ReplaceableTextures\CommandButtons\BTNHellScream.blp")
    {
      _preplacedUnitSystem = preplacedUnitSystem;
      _allLegendSetup = allLegendSetup;
      UndefeatedResearch = FourCC("R05W");
      StartingGold = 200;
      StartingLumber = 700;
      CinematicMusic = "DarkAgents";
      ControlPointDefenderUnitTypeId = Constants.UNIT_N0D6_CONTROL_POINT_DEFENDER_WARSONG;
      IntroText = @"You are playing as the brutal |cffd45e19Warsong clan|r.

You begin in the eaves of Ashenvale, isolated from your ally, the Frostwolf Clan in the South. 

The Warchief expects a large amount of lumber from you. Begin by harvesting with your Peons, and expanding into the Barrens and Durotar.

The Night Elves are aware of your presence and are gathering a mighty host against you. Unlock Orgrimmar as soon as possible to defend against the Night Elf assault.";
      GoldMines = new List<unit>
      {
        _preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(-8455, -2777))
      };
    }
        
    /// <inheritdoc />
    public override void OnRegistered()
    {
      RegisterObjectLimits();
      RegisterQuests();
      RegisterDialogue();
      SharedFactionConfigSetup.AddSharedFactionConfig(this);
    }

    private void RegisterObjectLimits()
    {
      ModObjectLimit(FourCC("o00C"), UNLIMITED); //Great Hall
      ModObjectLimit(FourCC("o02R"), UNLIMITED); //Stronghold
      ModObjectLimit(FourCC("o02S"), UNLIMITED); //Fortress
      ModObjectLimit(FourCC("o020"), UNLIMITED); //Altar of Storms
      ModObjectLimit(FourCC("o01S"), UNLIMITED); //Barracks
      ModObjectLimit(FourCC("o009"), UNLIMITED); //War Mill
      ModObjectLimit(FourCC("o006"), UNLIMITED); //Ogre Barrack
      ModObjectLimit(FourCC("o05G"), UNLIMITED); //Siege Workshop
      ModObjectLimit(FourCC("o02Q"), UNLIMITED); //Bestiary
      ModObjectLimit(FourCC("o028"), UNLIMITED); //Orc Burrow
      ModObjectLimit(FourCC("n03E"), UNLIMITED); //Watch Tower
      ModObjectLimit(FourCC("o01H"), UNLIMITED); //Troll Shrine
      ModObjectLimit(FourCC("n0AL"), UNLIMITED); //Improved Watch Tower
      ModObjectLimit(FourCC("o02T"), UNLIMITED); //Shipyard
      ModObjectLimit(FourCC("o01T"), UNLIMITED); //Goblin Hardware Shop

      ModObjectLimit(FourCC("o04L"), UNLIMITED); //Peon
      ModObjectLimit(FourCC("o02M"), UNLIMITED); //Grunt
      ModObjectLimit(FourCC("orai"), UNLIMITED); //Raider
      ModObjectLimit(FourCC("n08E"), UNLIMITED); //Hexbinder
      ModObjectLimit(FourCC("otbk"), UNLIMITED); //Troll Berseker
      ModObjectLimit(FourCC("nogn"), UNLIMITED); //Stonemaul Ogre Magi
      ModObjectLimit(FourCC("o00I"), 6); //Horde War Machine
      ModObjectLimit(FourCC("e01M"), 4); //Azerite Siege Engine
      ModObjectLimit(FourCC("okod"), 4); //Kodo Beast
      ModObjectLimit(FourCC("o00G"), 6); //Blademaster
      ModObjectLimit(FourCC("n03F"), 6); //Korkron elite
      ModObjectLimit(FourCC("owyv"), 8); //Wind Rider

      //Ship
      ModObjectLimit(FourCC("obot"), UNLIMITED); //Transport Ship
      ModObjectLimit(FourCC("h0AS"), UNLIMITED); //Scout
      ModObjectLimit(FourCC("h0AP"), UNLIMITED); //Frigate
      ModObjectLimit(FourCC("h0B2"), UNLIMITED); //Fireship
      ModObjectLimit(FourCC("h0AY"), UNLIMITED); //Galley
      ModObjectLimit(FourCC("h0B5"), UNLIMITED); //Boarding
      ModObjectLimit(FourCC("h0BC"), UNLIMITED); //Juggernaut
      ModObjectLimit(FourCC("h0AO"), 6); //Bombard

      ModObjectLimit(FourCC("Ogrh"), 1); //Grom
      ModObjectLimit(FourCC("Obla"), 1); //Varok
      ModObjectLimit(FourCC("O06L"), 1); //Garrosh
      ModObjectLimit(Constants.UNIT_NSJS_BREWMASTER_WARSONG, 1);
      ModObjectLimit(FourCC("n0CN"), 1); //Gibbs
      ModObjectLimit(Constants.UNIT_OPGH_CORRUPTOR_OF_THE_WARSONG_CLAN_WARSONG_BLOODPACT, 1); //Blood Pact Grom   Fixes Perma Death Grom Blood Pact bug

      ModObjectLimit(FourCC("Robs"), UNLIMITED); //Berserker Strength
      ModObjectLimit(FourCC("Rotr"), UNLIMITED); //Troll Regeneration
      ModObjectLimit(FourCC("R01J"), UNLIMITED); //Ensnare
      ModObjectLimit(FourCC("R02I"), UNLIMITED); //Ogre Magi Adept Training
      ModObjectLimit(FourCC("R03Q"), UNLIMITED); //Warlock Adept Training
      ModObjectLimit(FourCC("Rorb"), UNLIMITED); //Reinforced Defenses
      ModObjectLimit(FourCC("Rosp"), UNLIMITED); //Spiked Barricades
      ModObjectLimit(FourCC("R016"), UNLIMITED); //Warlords
      ModObjectLimit(FourCC("R019"), UNLIMITED); //Improved Shockwave
      ModObjectLimit(Constants.UPGRADE_R01Z_BATTLE_STATIONS_ORC, UNLIMITED);
      SetObjectLevel(Constants.UPGRADE_R01Z_BATTLE_STATIONS_ORC, 1);
      ModObjectLimit(Constants.UPGRADE_R00D_MASS_BLOODLUST_FROSTWOLF, UNLIMITED);
      ModObjectLimit(Constants.UPGRADE_ROVS_ENVENOMED_SPEARS_WARSONG, UNLIMITED);
      ModObjectLimit(Constants.UPGRADE_R017_IMPROVED_IGNORE_PAIN_WARSONG, UNLIMITED);


      ModObjectLimit(Constants.UPGRADE_R09N_FLIGHT_PATH_WARSONG, 1);

      ModAbilityAvailability(Constants.ABILITY_A0PF_FEL_ENERGY_TEAL_FORTRESSES, -1);
      ModAbilityAvailability(Constants.ABILITY_ANTR_TROLL_REGENERATION_PINK_WITCH_DOCTOR_TROLL_HEADHUNTER_TROLL_BATRIDER_DARKSPEAR_WARLORD_TROLL_BERSERKER_ICON, -1);
      ModAbilityAvailability(Constants.ABILITY_ABTL_BATTLE_STATIONS_FROSTWOLF_WARSONG_BURROW, 1);
      ModAbilityAvailability(Constants.ABILITY_A0M4_BATTLE_STATIONS_PINK_GREY_ORC_BURROW_BLOODPACT, -1);
      ModAbilityAvailability(Constants.ABILITY_A0GM_FOR_THE_HORDE_PINK_GREY_MAIN_BUILDINGS, 1);

      ModObjectLimit(Constants.UPGRADE_R09O_DRINK_THE_BLOOD_OF_MANNOROTH, 1);
      ModObjectLimit(Constants.UPGRADE_R09P_REVERT_BLOODPACT, 1);
    }

    private void RegisterQuests()
    {
      StartingQuest = AddQuest(new QuestOrgrimmar(Regions.Orgrimmar));
      AddQuest(new QuestCrossroads(Regions.Crossroads, _preplacedUnitSystem));
      AddQuest(new QuestLumberCamp(Regions.LumberCampUnlock, _allLegendSetup.Warsong.GromHellscream));
      AddQuest(new QuestChenStormstout(_preplacedUnitSystem.GetUnit(FourCC("Nsjs"))));
      AddQuest(new QuestFountainOfBlood(_allLegendSetup.Neutral.FountainOfBlood, _allLegendSetup.Warsong.GromHellscream));
      AddQuest(new QuestBloodpact(_allLegendSetup.Warsong.Mannoroth, _allLegendSetup.Warsong.GromHellscream));
      AddQuest(new QuestGarrosh(_allLegendSetup.Druids.TempleOfTheMoon));
      AddQuest(new QuestWarsongKillDruids(_allLegendSetup.Druids.Nordrassil, _allLegendSetup.Warsong.GromHellscream));
      AddQuest(new QuestMoreWyverns(_allLegendSetup.Sentinels.Feathermoon, _allLegendSetup.Sentinels.Auberdine));
      AddQuest(new QuestWarsongHold());
      AddQuest(new QuestWarsongOutpost());
    }

    private void RegisterDialogue()
    {
      TriggeredDialogueManager.Add(new TriggeredDialogue(
        new Dialogue(@"Sound\Dialogue\OrcCampaign\Orc05\O05Grom26.flac",
          "Yes! I feel the power once again! Come, my warriors; drink from the dark waters, and you will be reborn!",
          "Grom Hellscream"), new[]
        {
          this
        }, new List<Objective>
        {
          new ObjectiveControlLegend(_allLegendSetup.Warsong.GromHellscream, false)
          {
            EligibleFactions = new List<Faction>
            {
              this
            }
          },
          new ObjectiveControlCapital(_allLegendSetup.Neutral.FountainOfBlood, false)
          {
            EligibleFactions = new List<Faction>
            {
              this
            }
          }
        }
      ));
      
      TriggeredDialogueManager.Add(
        new TriggeredDialogue(new Dialogue(
            @"Sound\Dialogue\OrcExpCamp\RandomOrcQuest02x\DR02Chen01",
            "Ah, greetings, my friend. I am Chen Stormstout, humble brewmaster of Pandaria. I have traveled the wide world searching for rare, exotic ingredients to use in my special brew! After all, good ale can solve all the problems of this world, don't you agree?",
            "Chen Stormstout")
          , new[]
          {
            this
          }, new[]
          {
            new ObjectiveControlLegend(_allLegendSetup.Warsong.ChenStormstout, false)
            {
              EligibleFactions = new List<Faction>{ this }
            }
          }));
      
      TriggeredDialogueManager.Add(
        new TriggeredDialogue(new DialogueSequence(new Dialogue(
              @"Sound\Dialogue\OrcCampaign\Orc04\O04Grom01",
              "Damn Thrall for sending us away! He chooses to use his greatest warriors for manual labor? He'll be lost without me.",
              "Grom Hellscream"))
          , new[]
          {
            this
          }, new[]
          {
            new ObjectiveControlLegend(_allLegendSetup.Warsong.GromHellscream, false)
            {
              EligibleFactions = new List<Faction>{this}
            }
          }));
    }
  }
}