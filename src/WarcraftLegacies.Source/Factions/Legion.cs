using System.Collections.Generic;
using MacroTools;
using MacroTools.DialogueSystem;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.ObjectiveSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.ObjectiveSystem.Objectives.QuestBased;
using MacroTools.Powers;
using MacroTools.ResearchSystems;
using WarcraftLegacies.Shared.Config.Factions;
using WarcraftLegacies.Source.Quests.Legion;
using WarcraftLegacies.Source.Researches;
using WarcraftLegacies.Source.Setup;
using WCSharp.Shared.Data;

namespace WarcraftLegacies.Source.Factions
{
  public sealed class Legion : Faction
  {
    private readonly PreplacedUnitSystem _preplacedUnitSystem;
    private readonly AllLegendSetup _allLegendSetup;

    /// <inheritdoc />
    
    public Legion(PreplacedUnitSystem preplacedUnitSystem, AllLegendSetup allLegendSetup) : base("Legion",
      PLAYER_COLOR_PEANUT, "|CFFBF8F4F", @"ReplaceableTextures\CommandButtons\BTNKiljaedin.blp")
    {
      TraditionalTeam = TeamSetup.Legion;
      _preplacedUnitSystem = preplacedUnitSystem;
      _allLegendSetup = allLegendSetup;
      UndefeatedResearch = UPGRADE_R04T_LEGION_EXISTS;
      StartingGold = 200;
      FoodMaximum = 250;
      CinematicMusic = "DarkAgents";
      ControlPointDefenderUnitTypeId = UNIT_U01U_CONTROL_POINT_DEFENDER_LEGION;
      StartingCameraPosition = Regions.LegionStartPos.Center;
      IntroText = @"You are playing as the mighty |cffa2722dBurning Legion|r.

You begin isolated on Argus. Once the Planet is under control, you will unlock 2 teleporters to Northrend and Alterac.

On Azeroth, the Scourge will need your assistance to destroy the Kingdoms of Lordaeron, Dalaran and Quel'thalas.

Your primary objective is to summon the great host of the Burning Legion. Invade the city of Dalaran, where the book of Medivh is kept, and use it to open the Demon-gate to Argus.";
      GoldMines = new List<unit>
      {
        _preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(19331f, -30663))
      };
      Nicknames = new List<string>
      {
        "leg",
        "demon",
        "demons",
        "argus"
      };
      RegisterFactionDependentInitializer<Dalaran>(RegisterDalaranDialogue);
      RegisterFactionDependentInitializer<Illidari>(RegisterIllidariDialogue);
      RegisterFactionDependentInitializer<Frostwolf>(RegisterFrostwolfDialogue);
      RegisterFactionDependentInitializer<Scourge>(RegisterScourgeDialogue);
      RegisterFactionDependentInitializer<Scourge>(RegisterScourgeQuests);
      RegisterFactionDependentInitializer<Druids>(RegisterDruidsRelatedQuestsAndDialogue);
    }

    /// <inheritdoc />
    public override void OnRegistered()
    {
      RegisterObjectLimits();
      RegisterQuests();
      RegisterResearches();
      RegisterDialogue();
      SharedFactionConfigSetup.AddSharedFactionConfig(this);
    }

    private void RegisterObjectLimits()
    {
      foreach (var (objectTypeId, objectLimit) in LegionObjectLimitData.GetAllObjectLimits())
        ModObjectLimit(FourCC(objectTypeId), objectLimit);
    }

    private void RegisterQuests()
    {
      var newQuest = AddQuest(new QuestArgusControl(_preplacedUnitSystem));
      StartingQuest = newQuest;
      AddQuest(new QuestControlMonastery(_allLegendSetup.Lordaeron.Monastery));
      AddQuest(new QuestControlSpire(_allLegendSetup.Quelthalas.Spire));
      AddQuest(new QuestControlShadowfang(_allLegendSetup.Dalaran.Shadowfang));
      AddQuest(new QuestLegionCaptureSunwell(_allLegendSetup.Quelthalas.Sunwell));
      AddQuest(new QuestLegionKillLordaeron(_allLegendSetup.Lordaeron.CapitalPalace,
        _allLegendSetup.Lordaeron.Stratholme, _allLegendSetup.Legion.Tichondrius));
      AddQuest(new QuestSummonLegion(Regions.TwistingNether,
        _preplacedUnitSystem.GetUnit(UNIT_N03C_DEMON_PORTAL_NETHER)));
    }

    private void RegisterResearches()
    {
      ResearchManager.Register(new PowerResearch(UPGRADE_R096_REMATERIALIZATION_LEGION, 150,
        new Rematerialization(0.2f, new Point(20454.9f, -28873.6f), "Argus", Regions.MonolithNoBuild)
        {
          IconName = "achievement_raid_argusraid",
          Name = "Rematerialization",
          EligibilityCondition = dyingUnit => dyingUnit.OwningPlayer().GetObjectLimit(dyingUnit.GetTypeId()) != 0
        }));
    }

    private void RegisterDialogue()
    {
      TriggeredDialogueManager.Add(new TriggeredDialogue(new Dialogue(
        @"Sound\Dialogue\UndeadCampaign\Undead08\U08Archimonde19.flac",
        "Tremble, mortals, and despair! Doom has come to this world!", "Archimonde"), new Faction[]
      {
        this
      }, new Objective[]
      {
        new ObjectiveQuestComplete(GetQuestByType<QuestSummonLegion>())
        {
          EligibleFactions = new List<Faction>
          {
            this
          }
        }
      }));
    }

    private void RegisterDalaranDialogue(Dalaran dalaran)
    {
      TriggeredDialogueManager.Add(new TriggeredDialogue(
        new DialogueSequence(
          new Dialogue(@"Sound\Dialogue\NightElfCampaign\NightElf07\N07Archimonde21.flac",
            "You are very brave to stand against me, little human. If only your countrymen had been as bold, I would have had more fun scouring your wretched nations from the world!",
            "Archimonde"),
          new Dialogue(@"Sound\Dialogue\NightElfCampaign\NightElf07\N07Jaina22.flac",
            "Is talking all you demons do?",
            "Jaina Proudmoore")
        )
        , new Faction[]
        {
          this,
          dalaran
        }, new List<Objective>
        {
          new ObjectiveLegendMeetsLegend(_allLegendSetup.Legion.Archimonde, _allLegendSetup.Dalaran.Jaina)
        }
      ));
    }

    private void RegisterIllidariDialogue(Illidari illidari)
    {
      TriggeredDialogueManager.Add(new TriggeredDialogue(
        new DialogueSequence(
          new Dialogue(@"Sound\Dialogue\NightElfCampaign\NightElf06\N06Tichondrius21.flac",
            "What? Who are... you?",
            "Tichondrius"),
          new Dialogue(@"Sound\Dialogue\NightElfCampaign\NightElf06\N06Illidan22.flac",
            "Let's see how confident you are against one of your own kind, dreadlord!",
            "Illidan Stormrage"),
          new Dialogue(@"Sound\Dialogue\NightElfCampaign\NightElf06\N06Tichondrius20.flac",
            "I'm through toying with you, night elf! Begone from my sight!",
            "Tichondrius")
        )
        , new Faction[]
        {
          this,
          illidari
        }, new List<Objective>
        {
          new ObjectiveLegendMeetsLegend(_allLegendSetup.Legion.Tichondrius, _allLegendSetup.Naga.Illidan)
        }
      ));
    }

    private void RegisterFrostwolfDialogue(Frostwolf frostwolf)
    {
      TriggeredDialogueManager.Add(new TriggeredDialogue(
        new DialogueSequence(
          new Dialogue(@"Sound\Dialogue\NightElfCampaign\NightElf07\N07Archimonde14.flac",
            "You orcs are weak, and hardly worth the effort. I wonder why Mannoroth even bothered with you.",
            "Archimonde"),
          new Dialogue(@"Sound\Dialogue\NightElfCampaign\NightElf07\N07Thrall15.flac",
            "Our spirit is stronger than you know, demon! If we are to fall, then so be it! At least now... we are free!",
            "Thrall")
        )
        , new Faction[]
        {
          this,
          frostwolf
        }, new List<Objective>
        {
          new ObjectiveLegendMeetsLegend(_allLegendSetup.Legion.Archimonde, _allLegendSetup.Frostwolf.Thrall)
        }
      ));
    }

    private void RegisterScourgeDialogue(Scourge scourge)
    {
      TriggeredDialogueManager.Add(new TriggeredDialogue(
        new DialogueSequence(
          new Dialogue(@"Sound\Dialogue\UndeadCampaign\Undead06\U06Archimonde22.flac",
            "You called my name, puny lich, and I have come. You are Kel'Thuzad, are you not?",
            "Archimonde"),
          new Dialogue(@"Sound\Dialogue\UndeadCampaign\Undead06\U06Kelthuzad23.flac",
            "Yes, great one. I am the summoner.",
            "Kel'thuzad")
        ), new Faction[]
        {
          this,
          scourge
        }, new List<Objective>
        {
          new ObjectiveLegendMeetsLegend(_allLegendSetup.Legion.Archimonde, _allLegendSetup.Scourge.Kelthuzad)
        }
      ));
    }

    private void RegisterScourgeQuests(Scourge scourge)
    {
      AddQuest(new QuestCunningPlan(Regions.AlteracAmbient, scourge));
    }

    private void RegisterDruidsRelatedQuestsAndDialogue(Druids druids)
    {
      var questConsumeTree = AddQuest(new QuestConsumeTree(_allLegendSetup.Legion.Archimonde, druids));

      TriggeredDialogueManager.Add(new TriggeredDialogue(
        new Dialogue(@"Sound\Dialogue\NightElfCampaign\NightElf07\N07Archimonde28.flac",
          "At last, the way to the World Tree is clear! Witness the end, you mortals! The final hour has come.",
          "Archimonde")
        , null, new List<Objective>
        {
          new ObjectiveQuestComplete(questConsumeTree)
          {
            EligibleFactions = new List<Faction>
            {
              this
            }
          }
        }
      ));
    }
  }
}