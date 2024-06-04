using System.Collections.Generic;
using MacroTools;
using MacroTools.DialogueSystem;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using WarcraftLegacies.Shared.Config.Factions;
using WarcraftLegacies.Source.FactionMechanics.Druids;
using WarcraftLegacies.Source.Powers;
using WarcraftLegacies.Source.Quests.Druids;
using WarcraftLegacies.Source.Setup;
using WCSharp.Shared.Data;

namespace WarcraftLegacies.Source.Factions
{
  public sealed class Druids : Faction
  {
    private readonly PreplacedUnitSystem _preplacedUnitSystem;
    private readonly AllLegendSetup _allLegendSetup;
    private readonly ArtifactSetup _artifactSetup;

    /// <inheritdoc />
    
    public Druids(PreplacedUnitSystem preplacedUnitSystem, AllLegendSetup allLegendSetup, ArtifactSetup artifactSetup) :
      base("Druids", PLAYER_COLOR_BROWN, "|c004e2a04", @"ReplaceableTextures\CommandButtons\BTNFurion.blp")
    {
      TraditionalTeam = TeamSetup.NightElves;
      _preplacedUnitSystem = preplacedUnitSystem;
      _allLegendSetup = allLegendSetup;
      _artifactSetup = artifactSetup;
      UndefeatedResearch = FourCC("R06E");
      StartingGold = 200;
      CinematicMusic = "DarkAgents";
      ControlPointDefenderUnitTypeId = UNIT_E01Y_CONTROL_POINT_DEFENDER_DRUIDS;
      IntroText = @"You are playing as the ancient Druids of the Cenarion Circle.

You begin isolated in the deepest parts of Mount Hyjal near the World Tree.

The Horde is gathering to burn Ashenvale forest and the World Tree. Cenarius has emerged from his seclusion to stop them. 
Use him to awaken Malfurion from his slumber as soon as possible.

Gather your forces and strike before the Horde can organize their efforts.";
      GoldMines = new List<unit>
      {
        _preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(-9200, 10742))
      };
      Nicknames = new List<string>
      {
        "druid"
      };
      RegisterFactionDependentInitializer<Sentinels>(RegisterSentinelsDialogue);
      RegisterFactionDependentInitializer<Scourge>(RegisterScourgeQuests);
      RegisterFactionDependentInitializer<Sentinels, Frostwolf, Warsong>(RegisterSentinelsFrostwolfWarsongDialogue);
    }

    /// <inheritdoc />
    public override void OnRegistered()
    {
      RegisterObjectLimits();
      RegisterQuests();
      RegisterDialogue();
      RegisterPowers();
      CenariusGhost.Setup(_allLegendSetup.Druids.Cenarius, this);
      SharedFactionConfigSetup.AddSharedFactionConfig(this);
    }

    private void RegisterObjectLimits()
    {
      foreach (var (objectTypeId, objectLimit) in DruidsObjectLimitData.GetAllObjectLimits())
        ModObjectLimit(FourCC(objectTypeId), objectLimit);
    }

    private void RegisterQuests()
    {
      var newQuest = AddQuest(new QuestMalfurionAwakens(Regions.MoongladeVillage, Regions.TeldrassilUnlock,
        _allLegendSetup.Druids.Nordrassil.Unit, _artifactSetup.HornOfCenarius,
        _allLegendSetup.Druids.Malfurion));
      StartingQuest = newQuest;
      AddQuest(new QuestShrineBase(Regions.ShrineBaseUnlock));
      AddQuest(new QuestRiseBase(Regions.RiseBaseUnlock));
      AddQuest(new QuestAshenvale(Regions.AshenvaleUnlock));
      AddQuest(new QuestDruidsKillFrostwolf(_allLegendSetup.Frostwolf.ThunderBluff));
      AddQuest(new QuestDruidsKillWarsong());
      AddQuest(new QuestShaladrassil(_allLegendSetup.Neutral.Shaladrassil));
      AddQuest(new QuestTortolla(_allLegendSetup.Druids.Tortolla));
    }

    private void RegisterDialogue()
    {
      TriggeredDialogueManager.Add(new TriggeredDialogue(new DialogueSequence(
          new Dialogue(
            @"Sound\Dialogue\NightElfCampaign\NightElf04\N04Satyr29",
            "Come no further, weakling!  Lord Tichondrius commanded us to kill anyone attempting to enter this place, and we shall.",
            "Satyr"),
          new Dialogue(
            @"Sound\Dialogue\NightElfCampaign\NightElf04\N04Furion30",
            "Patches wretches! It pains me that you once called yourselves Night Elves.",
            "Malfurion Stormrage")
        ), 
        new[] { this },
        new Objective[]
        {
          new ObjectiveUnitAlive(_preplacedUnitSystem.GetUnit(UNIT_NSTH_SATYR_HELLCALLER, Regions.SatyrCamp.Center)),
          new ObjectiveLegendInRect(_allLegendSetup.Druids.Malfurion, Regions.SatyrCamp, "Satyr camp")
        }));
      
      TriggeredDialogueManager.Add(new TriggeredDialogue(
        new Dialogue(
          @"Sound\Dialogue\NightElfCampaign\NightElf03\N03Furion22",
          "The horn has sounded, and I have come as promised! I smell the stench of decay and corruption in our land. That angers me greatly.",
          "Malfurion Stormrage"),
        new[] { this },
        new Objective[]
        {
          new ObjectiveControlLegend(_allLegendSetup.Druids.Malfurion, false)
          {
            EligibleFactions = new List<Faction>
            {
              this
            }
          }
        }));
    }
    
    private void RegisterPowers()
    {
      var worldTrees = new List<Capital>
      {
        _allLegendSetup.Druids.Nordrassil,
        _allLegendSetup.Neutral.Shaladrassil,
        _allLegendSetup.Druids.Vordrassil
      };
      AddPower(new Immortality(20, 40, worldTrees)
      {
        IconName = "ArcaneRessurection",
        Name = "Immortality",
        Effect = @"Abilities\Spells\Human\Heal\HealTarget.mdl",
        ResearchId = UPGRADE_YB01_IMMORTALITY_POWER_IS_ACTIVE
      });
    }
    
    private void RegisterSentinelsDialogue(Sentinels sentinels)
    {
      TriggeredDialogueManager.Add(new TriggeredDialogue(new DialogueSequence(
          new Dialogue(
            @"Sound\Dialogue\NightElfCampaign\NightElf04\N04Furion01",
            "It has been a thousand years since I last looked up you, Tyrande. I thought of you every moment I roamed through the Emerald Dream.",
            "Malfurion Stormrage"), 
          new Dialogue(
            @"Sound\Dialogue\NightElfCampaign\NightElf04\N04Tyrande02",
            "My heart rejoices to see you again, Furion. But I would not have awakened you unless the need was urgent.",
            "Tyrande Whisperwind"), 
          new Dialogue(
            @"Sound\Dialogue\NightElfCampaign\NightElf04\N04Furion03",
            "In the Dream, I felt our land being corrupted, just as if it were my own body. You were right to awaken me.",
            "Malfurion Stormrage")
        ), 
        new Faction[] { this, sentinels },
        new[] { new ObjectiveLegendMeetsLegend(_allLegendSetup.Druids.Malfurion, _allLegendSetup.Sentinels.Tyrande) }));
    }
    
    private void RegisterSentinelsFrostwolfWarsongDialogue(Sentinels sentinels, Frostwolf frostwolf, Warsong warsong)
    {
      TriggeredDialogueManager.Add(
        new TriggeredDialogue(new Dialogue(
          @"Sound\Dialogue\OrcCampaign\Orc05\O05Cenarius01",
          "Who dares defile this ancient land? Who dares the wrath of Cenarius and the Night Elves?",
          "Cenarius"), new Faction[]
        {
          sentinels,
          this,
          frostwolf,
          warsong
        }, new[]
        {
          new ObjectiveControlLegend(_allLegendSetup.Druids.Cenarius, false)
          {
            EligibleFactions = new List<Faction>
            {
              this
            }
          }
        }));
    }
    
    private void RegisterScourgeQuests(Scourge scourge)
    {
      AddQuest(new QuestAndrassil(_allLegendSetup.Druids.Vordrassil, _allLegendSetup.Druids.Ursoc, scourge));
    }
  }
}