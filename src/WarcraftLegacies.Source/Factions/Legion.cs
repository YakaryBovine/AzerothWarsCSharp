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
using WarcraftLegacies.Source.Quests.Legion;
using WarcraftLegacies.Source.Researches;
using WarcraftLegacies.Source.Setup;
using WCSharp.Shared.Data;
using static War3Api.Common;

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
      _preplacedUnitSystem = preplacedUnitSystem;
      _allLegendSetup = allLegendSetup;
      UndefeatedResearch = Constants.UPGRADE_R04T_LEGION_EXISTS;
      StartingGold = 200;
      StartingLumber = 700;
      FoodMaximum = 250;
      CinematicMusic = "DarkAgents";
      ControlPointDefenderUnitTypeId = Constants.UNIT_U01U_CONTROL_POINT_DEFENDER_LEGION;
      StartingCameraPosition = Regions.LegionStartPos.Center;
      IntroText = @"You are playing as the mighty |cffa2722dBurning Legion|r.

You begin isolated on Argus. Once the Planet is under control, you will unlock 2 teleporters to Northrend and Alterac.

On Azeroth, the Scourge will need your assistance to destroy the Kingdoms of Lordaeron, Dalaran and Quel'thalas.

Your primary objective is to summon the great host of the Burning Legion. Invade the city of Dalaran, where the book of Medivh is kept, and use it to open the Demon-gate to Argus.";
      GoldMines = new List<unit>
      {
        _preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(19331f, -30663))
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
      //Structures
      ModObjectLimit(FourCC("u00H"), UNLIMITED); //Legion Defensive Pylon
      ModObjectLimit(FourCC("u00I"), UNLIMITED); //Improved Defensive Pylon
      ModObjectLimit(FourCC("u00F"), UNLIMITED); //Dormant Spire
      ModObjectLimit(FourCC("u00C"), UNLIMITED); //Legion Bastion
      ModObjectLimit(FourCC("u00N"), UNLIMITED); //Burning Citadel
      ModObjectLimit(FourCC("n040"), UNLIMITED); //Armory
      ModObjectLimit(FourCC("u009"), UNLIMITED); //Undead Shipyard
      ModObjectLimit(FourCC("u00E"), UNLIMITED); //Generator
      ModObjectLimit(FourCC("u01N"), UNLIMITED); //Burning Altar
      ModObjectLimit(Constants.UNIT_U015_UNHOLY_RELIQUARY_LEGION_SHOP, UNLIMITED);
      ModObjectLimit(FourCC("ndmg"), 6); //Demon Gate
      ModObjectLimit(Constants.UNIT_N04N_INFERNAL_SIEGEWORKS_LEGION_SPECIALIST, UNLIMITED);
      ModObjectLimit(Constants.UNIT_U006_SUMMONING_CIRCLE_LEGION_MAGIC, 3);
      ModObjectLimit(Constants.UNIT_N04Q_NETHER_PIT_LEGION_BARRACKS, 3);
      ModObjectLimit(Constants.UNIT_U00F_DORMANT_SPIRE_LEGION_T1, UNLIMITED);
      ModObjectLimit(Constants.UNIT_U00C_LEGION_BASTION_LEGION_T2, UNLIMITED);
      ModObjectLimit(Constants.UNIT_U00N_BURNING_CITADEL_LEGION_T3, UNLIMITED);

      //Units
      ModObjectLimit(FourCC("u00D"), UNLIMITED); //Legion Herald
      ModObjectLimit(FourCC("u007"), 6); //Dreadlord
      ModObjectLimit(FourCC("n04P"), UNLIMITED); //Warlock
      ModObjectLimit(FourCC("ninc"), UNLIMITED); //Burning archer
      ModObjectLimit(FourCC("n04K"), UNLIMITED); //Succubus
      ModObjectLimit(FourCC("n04J"), UNLIMITED); //Felstalker
      ModObjectLimit(FourCC("n0DO"), 12); //Doom Guard
      ModObjectLimit(FourCC("n04O"), 6); //Doom lord
      ModObjectLimit(FourCC("n04L"), 6); //Infernal Juggernaut
      ModObjectLimit(FourCC("o04P"), 6); //Nathrezim
      ModObjectLimit(Constants.UNIT_NINF_INFERNAL_LEGION, 6);
      ModObjectLimit(FourCC("n04H"), UNLIMITED); //Fel Guard
      ModObjectLimit(FourCC("n04U"), 4); //Dragon
      ModObjectLimit(FourCC("n03L"), 4); //Barge

      //Ship
      ModObjectLimit(FourCC("ubot"), UNLIMITED); //Undead Transport Ship
      ModObjectLimit(FourCC("h0AT"), UNLIMITED); //Scout
      ModObjectLimit(FourCC("h0AW"), UNLIMITED); //Frigate
      ModObjectLimit(FourCC("h0AM"), UNLIMITED); //Fireship
      ModObjectLimit(FourCC("h0AZ"), UNLIMITED); //Galley
      ModObjectLimit(FourCC("h0AQ"), UNLIMITED); //Boarding
      ModObjectLimit(FourCC("h0BB"), UNLIMITED); //Juggernaut
      ModObjectLimit(FourCC("h0B9"), 6); //Bombard

      ModObjectLimit(FourCC("n05R"), 1); //Felguard
      ModObjectLimit(FourCC("n06H"), 1); //Pit Fiend
      ModObjectLimit(FourCC("n07B"), 1); //Queen
      ModObjectLimit(FourCC("n07D"), 1); //Maiden
      ModObjectLimit(FourCC("n07o"), 1); //Terror
      ModObjectLimit(FourCC("n07N"), 1); //Lord

      //Researches
      ModObjectLimit(FourCC("R02C"), UNLIMITED); //Acute Sensors
      ModObjectLimit(FourCC("R028"), UNLIMITED); //Shadow Priest Adept Training
      ModObjectLimit(FourCC("R042"), UNLIMITED); //Nathrezim Adept Training
      ModObjectLimit(FourCC("R027"), UNLIMITED); //Warlock Adept Training
      ModObjectLimit(FourCC("R04G"), UNLIMITED); //Improved Carrion Swarm
      ModObjectLimit(FourCC("R03Z"), UNLIMITED); //War Plating
      ModObjectLimit(Constants.UPGRADE_R096_REMATERIALIZATION_LEGION, 1);
      ModObjectLimit(Constants.UPGRADE_R04R_FORTIFIED_HULLS_UNIVERSAL_UPGRADE, 1 );
      ModObjectLimit(Constants.UPGRADE_R03L_IMPROVED_SHADOW_INFUSION_FEL_HORDE, 1);
      
      //Heroes
      ModObjectLimit(FourCC("U00L"), 1); //Anetheron
      ModObjectLimit(Constants.UNIT_UMAL_THE_CUNNING_LEGION, 1);
      ModObjectLimit(Constants.UNIT_UTIC_THE_DARKENER_LEGION, 1);
    }

    private void RegisterQuests()
    {
      var newQuest = AddQuest(new QuestArgusControl(_preplacedUnitSystem));
      StartingQuest = newQuest;
      AddQuest(new QuestControlMonastery(_allLegendSetup.Lordaeron.Monastery));
      AddQuest(new QuestControlSpire(_allLegendSetup.Quelthalas.Spire));
      AddQuest(new QuestControlShadowfang(_allLegendSetup.Dalaran.Shadowfang));
      AddQuest(new QuestLegionCaptureSunwell(_allLegendSetup.Quelthalas.Sunwell));
      AddQuest(new QuestLegionKillLordaeron(_allLegendSetup.Lordaeron.CapitalPalace, _allLegendSetup.Lordaeron.Stratholme, _allLegendSetup.Legion.Tichondrius));
      AddQuest(new QuestSummonLegion(Regions.TwistingNether,_preplacedUnitSystem.GetUnit(Constants.UNIT_N03C_DEMON_PORTAL_NETHER)));
    }
    
    private void RegisterResearches()
    {
      ResearchManager.Register(new PowerResearch(Constants.UPGRADE_R096_REMATERIALIZATION_LEGION, 150, 250,
        new Rematerialization(0.15f, new Point(20454.9f, -28873.6f), "Argus", Regions.MonolithNoBuild)
        {
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
        }
      ));
    }
  }
}