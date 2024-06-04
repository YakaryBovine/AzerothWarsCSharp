using System.Collections.Generic;
using MacroTools;
using MacroTools.DialogueSystem;
using MacroTools.FactionSystem;
using MacroTools.ObjectiveSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using WarcraftLegacies.Shared.Config.Factions;
using WarcraftLegacies.Source.Quests.Warsong;
using WarcraftLegacies.Source.Setup;
using WCSharp.Shared.Data;

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
      TraditionalTeam = TeamSetup.Horde;
      _preplacedUnitSystem = preplacedUnitSystem;
      _allLegendSetup = allLegendSetup;
      UndefeatedResearch = FourCC("R05W");
      StartingGold = 200;
      CinematicMusic = "DarkAgents";
      ControlPointDefenderUnitTypeId = UNIT_N0D6_CONTROL_POINT_DEFENDER_WARSONG;
      IntroText = @"You are playing as the brutal |cffd45e19Warsong clan|r.

You begin in the eaves of Ashenvale, isolated from your ally, the Frostwolf Clan in the South. 

The Warchief expects a large amount of gold from you. Begin by harvesting with your Peons, and expanding into the Barrens and Durotar.

The Night Elves are aware of your presence and are gathering a mighty host against you. Unlock Orgrimmar as soon as possible to defend against the Night Elf assault.";
      GoldMines = new List<unit>
      {
        _preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(-8455, -2777))
      };
      Nicknames = new List<string>
      {
        "ws",
        "war"
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
      foreach (var (objectTypeId, objectLimit) in WarsongObjectLimitData.GetAllObjectLimits())
        ModObjectLimit(FourCC(objectTypeId), objectLimit);
    }

    private void RegisterQuests()
    {
      StartingQuest = AddQuest(new QuestOrgrimmar(Regions.Orgrimmar, _allLegendSetup.Warsong.GromHellscream));
      AddQuest(new QuestCrossroads(Regions.Crossroads));
      AddQuest(new QuestChenStormstout(_preplacedUnitSystem.GetUnit(FourCC("Nsjs"))));
      AddQuest(new QuestFountainOfBlood(_allLegendSetup.Neutral.FountainOfBlood, _allLegendSetup.Warsong.GromHellscream));
      AddQuest(new QuestBloodpact(_allLegendSetup.Warsong.Mannoroth, _allLegendSetup.Warsong.GromHellscream));
      AddQuest(new QuestGarrosh(_allLegendSetup.Druids.TempleOfTheMoon));
      AddQuest(new QuestWarsongKillDruids(_allLegendSetup.Druids.Nordrassil, _allLegendSetup.Warsong.GromHellscream));
      AddQuest(new QuestMoreWyverns(_allLegendSetup.Sentinels.Feathermoon, _allLegendSetup.Sentinels.Auberdine));
      AddQuest(new QuestWarsongHold());
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