using System.Collections.Generic;
using MacroTools;
using MacroTools.DialogueSystem;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using WarcraftLegacies.Source.FactionMechanics.Druids;
using WarcraftLegacies.Source.Powers;
using WarcraftLegacies.Source.Quests.Druids;
using WarcraftLegacies.Source.Setup;
using WCSharp.Shared.Data;
using static War3Api.Common;

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
      _preplacedUnitSystem = preplacedUnitSystem;
      _allLegendSetup = allLegendSetup;
      _artifactSetup = artifactSetup;
      UndefeatedResearch = FourCC("R06E");
      StartingGold = 200;
      StartingLumber = 700;
      CinematicMusic = "DarkAgents";
      ControlPointDefenderUnitTypeId = Constants.UNIT_E01Y_CONTROL_POINT_DEFENDER_DRUIDS;
      IntroText = @"You are playing as the ancient Druids of the Cenarion Circle.

You begin isolated in the deepest parts of Mount Hyjal near the World Tree.

The Horde is gathering to burn Ashenvale forest and the World Tree. Cenarius has emerged from his seclusion to stop them. 
Use him to awaken Malfurion from his slumber as soon as possible. 

Once awakened, use his Force of Nature to clear a path through the trees in Ashenvale.

Gather your forces and strike before the Horde can organize their efforts.";
      GoldMines = new List<unit>
      {
        _preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(-9200, 10742))
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
      ModObjectLimit(FourCC("etol"), UNLIMITED); //Tree of Life
      ModObjectLimit(FourCC("etoa"), UNLIMITED); //Tree of Ages
      ModObjectLimit(FourCC("etoe"), UNLIMITED); //Tree of Eternity
      ModObjectLimit(FourCC("emow"), UNLIMITED); //Moon Well
      ModObjectLimit(FourCC("eate"), UNLIMITED); //Altar of Elders
      ModObjectLimit(FourCC("eaoe"), UNLIMITED); //Ancient of Lore
      ModObjectLimit(FourCC("eaow"), UNLIMITED); //Ancient of Wind
      ModObjectLimit(FourCC("eaom"), UNLIMITED); //Ancient of war
      ModObjectLimit(FourCC("etrp"), UNLIMITED); //Ancient Protector
      ModObjectLimit(FourCC("e010"), UNLIMITED); //Hunter)s Hall
      ModObjectLimit(FourCC("e019"), UNLIMITED); //Ancient of Wonders
      ModObjectLimit(FourCC("eshy"), UNLIMITED); //Night Elf Shipyard
      ModObjectLimit(FourCC("e000"), UNLIMITED); //Improved Ancient Protector

      ModObjectLimit(FourCC("ewsp"), UNLIMITED); //Wisp
      ModObjectLimit(FourCC("edry"), UNLIMITED); //Dryad
      ModObjectLimit(FourCC("edot"), UNLIMITED); //Druid of the Talon
      ModObjectLimit(FourCC("emtg"), 12); //Mountain Giant
      ModObjectLimit(FourCC("efdr"), 6); //Faerie Dragon
      ModObjectLimit(FourCC("edoc"), UNLIMITED); //Druid of the Claw
      ModObjectLimit(FourCC("edcm"), UNLIMITED); //Druid of the Claw bear form
      ModObjectLimit(FourCC("e00N"), 6); //Keeper of the Grove
      ModObjectLimit(FourCC("n05H"), UNLIMITED); //Furbolg
      ModObjectLimit(FourCC("n065"), 6); //Green Dragon
      ModObjectLimit(Constants.UNIT_E012_SIEGE_ANCIENT_DRUIDS_ELITE, 6);

      //Ships
      ModObjectLimit(FourCC("etrs"), UNLIMITED); //Night Elf Transport Ship
      ModObjectLimit(FourCC("h0AU"), UNLIMITED); // Scout
      ModObjectLimit(FourCC("h0AV"), UNLIMITED); // Frigate
      ModObjectLimit(FourCC("h0B1"), UNLIMITED); // Fireship
      ModObjectLimit(FourCC("h057"), UNLIMITED); // Galley
      ModObjectLimit(FourCC("h0B4"), UNLIMITED); // Boarding
      ModObjectLimit(FourCC("h0BA"), UNLIMITED); // Juggernaut
      ModObjectLimit(FourCC("h0B8"), 6); // Bombard

      ModObjectLimit(FourCC("Ecen"), 1); //Cenarius
      ModObjectLimit(FourCC("E00H"), 1); //Cenarius
      ModObjectLimit(FourCC("E00K"), 1); //Tortolla
      ModObjectLimit(FourCC("Efur"), 1); //Furion
      ModObjectLimit(Constants.UNIT_E00A_ANCIENT_GUARDIAN_DRUIDS, 1);
      ModObjectLimit(Constants.UNIT_E00X_ELEMENTAL_GUARDIAN_DRUIDS_DEMI, 1);
      ModObjectLimit(Constants.UNIT_H04U_DEMIGOD_DRUIDS, 1);

      ModObjectLimit(FourCC("Redt"), UNLIMITED); //Druid of the Talon Adept Training
      ModObjectLimit(FourCC("Renb"), UNLIMITED); //Nature)s Blessing
      ModObjectLimit(FourCC("Rers"), UNLIMITED); //Resistant Skin
      ModObjectLimit(FourCC("Reuv"), UNLIMITED); //Ultravision
      ModObjectLimit(FourCC("Rews"), UNLIMITED); //Well Spring
      ModObjectLimit(FourCC("Redc"), UNLIMITED); //Druid of the Claw Adept Training
      ModObjectLimit(FourCC("R04E"), UNLIMITED); //Ysera)s Gift
      ModObjectLimit(FourCC("R02G"), UNLIMITED); //Emerald Flames
      ModObjectLimit(FourCC("R05X"), UNLIMITED); //Blessing of Ursoc
      ModObjectLimit(FourCC("R002"), UNLIMITED); //Blackwald Enhancement
      ModObjectLimit(FourCC("R00A"), UNLIMITED); //Improved Thorns
      ModObjectLimit(FourCC("R02T"), UNLIMITED); //Improved Moonwells
      ModObjectLimit(FourCC("R033"), UNLIMITED); //Limber Timber
      ModObjectLimit(FourCC("R046"), UNLIMITED); //Grasping Vines
      ModObjectLimit(FourCC("R047"), UNLIMITED); //Crippling Poison
      ModObjectLimit(FourCC("R048"), UNLIMITED); //Deadly Poison
      ModObjectLimit(FourCC("R008"), UNLIMITED); //Improved Natures FuryR015
      ModObjectLimit(FourCC("R015"), Constants.UPGRADE_R015_IMPROVED_MANA_FLARE_DRUIDS);
      ModObjectLimit(Constants.UPGRADE_R09V_STORM_CROW_FORM_DRUIDS, UNLIMITED);

      SetObjectLevel(Constants.UPGRADE_REWS_WELL_SPRING, 1);
    }

    private void RegisterQuests()
    {
      var newQuest = AddQuest(new QuestMalfurionAwakens(Regions.MoongladeVillage, Regions.TeldrassilUnlock, Regions.CenarionHoldUnlock,
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
          new ObjectiveUnitAlive(_preplacedUnitSystem.GetUnit(Constants.UNIT_NSTH_SATYR_HELLCALLER, Regions.SatyrCamp.Center)),
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
      AddPower(new Immortality(25, 45, worldTrees)
      {
        Name = "Immortality",
        Effect = @"Abilities\Spells\Human\Heal\HealTarget.mdl",
        ResearchId = Constants.UPGRADE_YB01_IMMORTALITY_POWER_IS_ACTIVE
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