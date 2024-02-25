using System.Collections.Generic;
using MacroTools;
using MacroTools.DialogueSystem;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.ObjectiveSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.ObjectiveSystem.Objectives.QuestBased;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using MacroTools.QuestSystem;
using MacroTools.ResearchSystems;
using WarcraftLegacies.Source.Quests.Lordaeron;
using WarcraftLegacies.Source.Quests.Scourge;
using WarcraftLegacies.Source.Researches;
using WarcraftLegacies.Source.Setup;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Factions
{
  public sealed class Lordaeron : Faction
  {
    private readonly PreplacedUnitSystem _preplacedUnitSystem;
    private readonly AllLegendSetup _allLegendSetup;
    private readonly ArtifactSetup _artifactSetup;

    /// <inheritdoc />
    public Lordaeron(PreplacedUnitSystem preplacedUnitSystem, AllLegendSetup allLegendSetup, ArtifactSetup artifactSetup) : base("Lordaeron",
      PLAYER_COLOR_LIGHT_BLUE, "|cff8080ff", @"ReplaceableTextures\CommandButtons\BTNArthas.blp")
    {
      _preplacedUnitSystem = preplacedUnitSystem;
      _allLegendSetup = allLegendSetup;
      _artifactSetup = artifactSetup;
      StartingGold = 200;
      StartingLumber = 700;
      UndefeatedResearch = Constants.UPGRADE_R05M_LORDAERON_EXISTS;
      CinematicMusic = "Comradeship";
      ControlPointDefenderUnitTypeId = Constants.UNIT_H03W_CONTROL_POINT_DEFENDER_LORDAERON;
      IntroText = @"You are playing as the great |cff4242ebKingdom of Lordaeron|r.

You begin in Andorhal, isolated from your forces in the rest of the Kingdom, and the Plague of Undeath is coming.

Secure your major settlements by clearing out clusters of enemies and fortify your Kingdom as much as possible.

If you survive the Plague, sail to the frozen wasteland of Northrend and take the fight to the Lich King.";
      GoldMines = new List<unit>
      {
        _preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(13617, 8741)),
        _preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(7386, 6999)),
        _preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(7716, 11657)),
        _preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(17198, 8222))
      };
      RegisterFactionDependentInitializer<Legion>(RegisterLegionDialogue);
      RegisterFactionDependentInitializer<Scourge>(RegisterScourgeDialogue);
      RegisterFactionDependentInitializer<Dalaran>(RegisterDalaranDialogue);
      RegisterFactionDependentInitializer<Scourge, Legion>(RegisterScourgeLegionDialogue);
    }

    /// <inheritdoc />
    public override void OnRegistered()
    {
      RegisterObjectLimits();
      RegisterQuests();
      RegisterDialogue();
      RegisterCrownOfLordaeronDrop();
      RegisterResearches();
      SharedFactionConfigSetup.AddSharedFactionConfig(this);
    }
    
    private void RegisterObjectLimits()
    {
      //Structures
      ModObjectLimit(FourCC("htow"), UNLIMITED); //Town Hall
      ModObjectLimit(FourCC("hkee"), UNLIMITED); //Keep
      ModObjectLimit(FourCC("hcas"), UNLIMITED); //Castle
      ModObjectLimit(FourCC("hhou"), UNLIMITED); //Farm
      ModObjectLimit(FourCC("halt"), UNLIMITED); //Altar of Kings
      ModObjectLimit(FourCC("hbar"), UNLIMITED); //Barracks
      ModObjectLimit(FourCC("hbla"), UNLIMITED); //Blacksmith
      ModObjectLimit(FourCC("h035"), UNLIMITED); //Chapel
      ModObjectLimit(FourCC("hwtw"), UNLIMITED); //Scout Tower
      ModObjectLimit(FourCC("hgtw"), UNLIMITED); //Guard Tower
      ModObjectLimit(FourCC("h006"), UNLIMITED); //Guard Tower (Improved)
      ModObjectLimit(FourCC("hctw"), UNLIMITED); //Cannon Tower
      ModObjectLimit(FourCC("h007"), UNLIMITED); //Cannon Tower (Improved)
      ModObjectLimit(FourCC("hshy"), UNLIMITED); //Alliance Shipyard
      ModObjectLimit(FourCC("nmrk"), UNLIMITED); //Marketplace
      ModObjectLimit(FourCC("h06C"), UNLIMITED); //Aviary
      ModObjectLimit(FourCC("h094"), UNLIMITED); //Siege Camp

      //Units
      ModObjectLimit(FourCC("hpea"), UNLIMITED); //Peasant
      ModObjectLimit(FourCC("hfoo"), UNLIMITED); //Footman
      ModObjectLimit(FourCC("hkni"), UNLIMITED); //Knight
      ModObjectLimit(FourCC("nchp"), UNLIMITED); //Cleric
      ModObjectLimit(FourCC("h00F"), 6); //Paladin
      ModObjectLimit(FourCC("nwe2"), 6); //Thunder Eagle
      ModObjectLimit(FourCC("h01C"), UNLIMITED); //Longbowman
      ModObjectLimit(FourCC("n03K"), UNLIMITED); //Chaplain
      ModObjectLimit(FourCC("hcth"), 12); //Silver Hand Squire
      ModObjectLimit(FourCC("h02Q"), 6); //Pegasus Knight
      ModObjectLimit(FourCC("e017"), 8); //Scorpion
      ModObjectLimit(FourCC("o02F"), 6); //Mangonel
      ModObjectLimit(FourCC("h09Y"), 2); //Throne Guard

      //Ships
      ModObjectLimit(FourCC("hbot"), UNLIMITED); //Alliance Transport Ship
      ModObjectLimit(FourCC("h0AR"), UNLIMITED); //Alliance Scout
      ModObjectLimit(FourCC("h0AX"), UNLIMITED); //Alliance Frigate
      ModObjectLimit(FourCC("h0B3"), UNLIMITED); //Alliance Fireship
      ModObjectLimit(FourCC("h0B0"), UNLIMITED); //Alliance Galley
      ModObjectLimit(FourCC("h0B6"), UNLIMITED); //Alliance Boarding
      ModObjectLimit(FourCC("h0AN"), UNLIMITED); //Alliance Juggernaut
      ModObjectLimit(FourCC("h0B7"), 6); //Alliance Bombard

      //Demis
      ModObjectLimit(Constants.UNIT_H012_CAPTAIN_FALRIC_LORDAERON_DEMI, 1);

      //Heroes
      ModObjectLimit(Constants.UNIT_HART_CROWN_PRINCE_OF_LORDAERON_LORDAERON, 1);
      ModObjectLimit(Constants.UNIT_HUTH_LEADER_OF_THE_SILVER_HAND_LORDAERON, 1);
      ModObjectLimit(Constants.UNIT_H01J_THE_ASHBRINGER_LORDAERON, 1);
      ModObjectLimit(FourCC("Hlgr"), 1); //Garithos
      ModObjectLimit(Constants.UNIT_HARF_HIGH_KING_LORDAERON_HIGH_KING, 1);

      //Upgrades
      ModObjectLimit(Constants.UPGRADE_R02E_LIGHT_S_PRAISE_MASTER_TRAINING_ARATHOR_LORDAERON, UNLIMITED);
      ModObjectLimit(Constants.UPGRADE_R00I_MAGE_MASTER_TRAINING_LORDAERON, UNLIMITED);
      ModObjectLimit(Constants.UPGRADE_RHAN_ANIMAL_WAR_TRAINING_DARK_GREEN_PURPLE_RESEARCH, UNLIMITED);
      ModObjectLimit(Constants.UPGRADE_RHLH_IMPROVED_LUMBER_HARVESTING_ADVANCED_LUMBER_HARVESTING_YELLOW_PURPLE_ORANGE_GREEN_DARK_GREEN_RESEARCH, UNLIMITED);
      ModObjectLimit(Constants.UPGRADE_RHAC_IMPROVED_MASONRY_ADVANCED_MASONRY_IMBUED_MASONRY_YELLOW_PURPLE_ORANGE_GREEN_DARK_GREEN_RESEARCH, UNLIMITED);
      ModObjectLimit(Constants.UPGRADE_R04D_SEAL_OF_RIGHTEOUSNESS_LORDAERON, UNLIMITED);
      ModObjectLimit(Constants.UPGRADE_R01P_ENSNARE_LORDAERON, UNLIMITED);
      ModObjectLimit(Constants.UPGRADE_R04A_RAPID_FIRE_LORDAERON, UNLIMITED);
      ModObjectLimit(Constants.UPGRADE_R00B_VETERAN_FOOTMEN_LORDAERON, UNLIMITED);

      //Todo: these probably should be in some kind of ability library, not here
      ModAbilityAvailability(Constants.ABILITY_A0N2_GRASPING_VINES_TREANTS, -1);
      ModAbilityAvailability(Constants.ABILITY_A0GC_REPLENISH_MANA_ORANGE_KEEPS_CAPITALS, -1);
      ModAbilityAvailability(Constants.ABILITY_A0K5_DWARVEN_MASONRY_CASTLES_YELLOW, -1);
      ModAbilityAvailability(Constants.ABILITY_A0GA_SUMMON_GARRISON_LORDAERON, 1);
      ModAbilityAvailability(Constants.ABILITY_A0GD_SUMMON_GARRISON_STORMWIND, -1);

      ModObjectLimit(Constants.UPGRADE_R0XZ_THE_SCARLET_CRUSADE_LORDAERON_SCARLET, UNLIMITED);
    }

    private void RegisterQuests()
    {
      var questStrahnbrad = new QuestStrahnbrad(Regions.StrahnbradUnlock);
      var questStratholme = new QuestStratholme(Regions.StratholmeUnlock, _preplacedUnitSystem, _allLegendSetup.Lordaeron.Arthas, _allLegendSetup.Lordaeron.Uther, _allLegendSetup.Lordaeron.Stratholme);
      var questTyrHand = new QuestTyrHand(_allLegendSetup.Lordaeron.Stratholme, Regions.TyrUnlock);
      AddQuest(new QuestHearthglen(Regions.Hearthglen));
      AddQuest(questStratholme);
      StartingQuest = questStratholme;
      AddQuest(questStrahnbrad);
      AddQuest(new QuestCapitalCity(_preplacedUnitSystem, Regions.Terenas, _allLegendSetup.Lordaeron.Terenas.Unit,
        _allLegendSetup.Lordaeron.Uther, _allLegendSetup.Lordaeron.Arthas, _allLegendSetup.Neutral.Caerdarrow, _allLegendSetup.Lordaeron.CapitalPalace,
        new QuestData[]
        {
          questStrahnbrad,
          questStratholme
        }));
      AddQuest(questTyrHand);
      AddQuest(new QuestMograine());
      AddQuest(new QuestScarletCrusade(Regions.Scarlet_Spawn, _allLegendSetup.Lordaeron.TyrsHand, _allLegendSetup.Scarlet.Saiden, questTyrHand, _allLegendSetup, _artifactSetup));
      AddQuest(new QuestShoresOfNorthrend(_allLegendSetup.Lordaeron.Arthas, _allLegendSetup.Neutral.Caerdarrow));
      AddQuest(new QuestThunderEagle(_allLegendSetup.Neutral.DraktharonKeep));
      AddQuest(new QuestChampionoftheLight(_allLegendSetup.Lordaeron.Uther));
      AddQuest(new QuestKingArthas(_allLegendSetup.Lordaeron.Terenas.Unit,
        _artifactSetup.CrownOfLordaeron, _allLegendSetup.Lordaeron.CapitalPalace, _allLegendSetup.Lordaeron.Arthas));
      AddQuest(new QuestKingdomOfManLordaeron(_artifactSetup.CrownOfLordaeron,
        _artifactSetup.CrownOfStormwind, _allLegendSetup.Lordaeron.Arthas));
    }

    private void RegisterResearches()
    {
      ResearchManager.Register(new VeteranFootmen(Constants.UPGRADE_R00B_VETERAN_FOOTMEN_LORDAERON, 220, 120));
    }
    
    private void RegisterDialogue()
    {
      TriggeredDialogueManager.Add(
        new TriggeredDialogue(
          new DialogueSequence(
            new Dialogue(
              @"Sound\Dialogue\HumanCampaign\Human01\H01Uther01",
              "Welcome, Prince Arthas. The men and I are honored by your presence.",
              "Uther the Lightbringer"),
            new Dialogue(
              @"Sound\Dialogue\HumanCampaign\Human01\H01Arthas02",
              "Can the formalities, Uther. I'm not king yet. It's good to see you.",
              "Arthas Menethil"),
            new Dialogue(
              @"Sound\Dialogue\HumanCampaign\Human01\H01Uther03",
              "You too, lad. I'm pleased that King Terenas sent you to help me.",
              "Uther the Lightbringer"),
            new Dialogue(
              @"Sound\Dialogue\HumanCampaign\Human01\H01Arthas04",
              "Father still hopes your patience and experience might rub off on me.",
              "Arthas Menethil"),
            new Dialogue(
              @"Sound\Dialogue\HumanCampaign\Human01\H01Uther05",
              "It is a father's right to dream, isn't it?",
              "Uther the Lightbringer")
          ),
          new[]
          {
            this
          }, new Objective[]
          {
            new ObjectiveLegendMeetsLegend(_allLegendSetup.Lordaeron.Arthas, _allLegendSetup.Lordaeron.Uther)
          }));
      
      TriggeredDialogueManager.Add(
        new TriggeredDialogue(
          new DialogueSequence(
            new Dialogue(
              @"Sound\Dialogue\HumanCampaign\Human02\H02Blademaster11",
              "Paladin fool! The warlocks of the Blackrock clan have spoken! Soon, demons will rain from the sky, and this wretched world will burn!",
              "Jubei'thos"),
            new Dialogue(
              @"Sound\Dialogue\HumanCampaign\Human02\H02Uther12",
              "Yes, I've heard this rhetoric before. You orcs will never learn!",
              "Uther the Lightbringer")
          ),
          new[]
          {
            this
          }, new Objective[]
          {
            new ObjectiveLegendInRect(_allLegendSetup.Lordaeron.Uther, Regions.AlteracAmbient, "Alterac"),
            new ObjectiveUnitAlive(_preplacedUnitSystem.GetUnit(Constants.UNIT_O00B_JUBEI_THOS_LEGION_DEMI,
              new Point(11066, 6291)))
          }));
      
      TriggeredDialogueManager.Add(
        new TriggeredDialogue(new DialogueSequence(
          new Dialogue(
            @"Sound\Dialogue\HumanCampaign\Human07\H07Captain01",
            "This is a Light-forsaken land, isn't it? You can barely even see the sun! This howling wind cuts to the bone and you're not even shaking. Mi'lord, are you alright?",
            "Captain Falric"),
          new Dialogue(
            @"Sound\Dialogue\HumanCampaign\Human07\H07Arthas02",
            "Captain, are all my forces accounted for?",
            "Arthas Menethil"),
          new Dialogue(
            @"Sound\Dialogue\HumanCampaign\Human07\H07Captain03",
            "Nearly. There are only a few ships that--",
            "Captain Falric"),
          new Dialogue(
            @"Sound\Dialogue\HumanCampaign\Human07\H07Arthas04",
            "Very well. Our first priority is to set up a base camp with proper defenses. There's no telling what's waiting for us out there in the shadows.",
            "Arthas Menethil")
        ), new[]
        {
          this
        }, new Objective[]
        {
          new ObjectiveLegendReachRect(_allLegendSetup.Lordaeron.Arthas, Regions.Central_Northrend, "central Northrend")
        }));
      
      TriggeredDialogueManager.Add(
        new TriggeredDialogue(
          new DialogueSequence(
            new Dialogue(
              @"Sound\Dialogue\HumanCampaign\Human03\H03Arthas12",
              "This must be the shrine that the old man spoke of. Any man who drinks from these Light-blessed waters will be healed.",
              "Arthas Menethil")
          ),
          new[]
          {
            this
          }, new Objective[]
          {
            new ObjectiveLegendInRect(_allLegendSetup.Lordaeron.Arthas, Regions.FountainOfHealthAlterac,
              "Fountain of Health in Alterac")
          }));
    }

    private void RegisterLegionDialogue(Legion legion)
    {
      TriggeredDialogueManager.Add(
        new TriggeredDialogue(
          new DialogueSequence(
            new Dialogue(
              @"Sound\Dialogue\HumanCampaign\Human09\H09MalGanis23",
              "So, you've taken up Frostmourne at the expense of your comrades' lives, just as the Dark Lord said you would. You're stronger than I thought.",
              "Mal'ganis"),
            new Dialogue(
              @"Sound\Dialogue\HumanCampaign\Human09\H09Arthas24",
              "You waste your breath, Mal'Ganis. I heed only the voice of Frostmourne now.",
              "Arthas Menethil"),
            new Dialogue(
              @"Sound\Dialogue\HumanCampaign\Human09\H09MalGanis25",
              "You hear the voice of the Dark Lord. He whispers to you through the blade you wield. What does he say, young human? What does the Dark Lord of the Dead tell you now?",
              "Mal'ganis"),
            new Dialogue(
              @"Sound\Dialogue\HumanCampaign\Human09\H09Arthas26",
              "He tells me that the time for my vengeance has come.",
              "Arthas Menethil")
          ),
          new Faction[]
          {
            this,
            legion
          }, new Objective[]
          {
            new ObjectiveLegendMeetsLegend(_allLegendSetup.Scourge.Arthas, _allLegendSetup.Legion.Malganis)
          }));

      TriggeredDialogueManager.Add(
        new TriggeredDialogue(
          new Dialogue(
            @"Sound\Dialogue\HumanCampaign\Human07\H07Arthas26",
            "That has to be where Mal'Ganis is hiding! I want that base leveled!",
            "Arthas Menethil"),
          new Faction[]
          {
            this,
            legion
          }, new Objective[]
          {
            new ObjectiveLegendInRect(_allLegendSetup.Lordaeron.Arthas, Regions.DrakUnlock, ""),
          }));

      TriggeredDialogueManager.Add(
        new TriggeredDialogue(
          new Dialogue(
            @"Sound\Dialogue\HumanCampaign\Human09\H09MalGanis03",
            "The Dark Lord said you would come. This is where your journey ends, boy. Trapped and freezing at the roof of the world, with only death to sing the tale of your doom.",
            "Mal'ganis"),
          new Faction[]
          {
            this,
            legion
          }, new Objective[]
          {
            new ObjectiveLegendMeetsLegend(_allLegendSetup.Legion.Malganis, _allLegendSetup.Lordaeron.Arthas),
            new ObjectiveLegendInRect(_allLegendSetup.Lordaeron.Arthas, Regions.Central_Northrend,
              ""), //Todo: make this work in any region of Northrend
            new ObjectiveLegendInRect(_allLegendSetup.Legion.Malganis, Regions.Central_Northrend, "")
          }));
    }

    private void RegisterScourgeDialogue(Scourge scourge)
    {
      TriggeredDialogueManager.Add(new TriggeredDialogue(
        new DialogueSequence(
          new Dialogue(@"Sound\Dialogue\UndeadCampaign\Undead02\U02Uther21.flac",
            "Your father ruled this land for seventy years, and you've ground it to dust in a matter of days.",
            "Uther the Lightbringer")
        )
        , new Faction[]
        {
          scourge,
          this
        }, new List<Objective>
        {
          new ObjectiveLegendMeetsLegend(_allLegendSetup.Lordaeron.Uther, _allLegendSetup.Scourge.Arthas)
        }
      ));

      TriggeredDialogueManager.Add(new TriggeredDialogue(
        new DialogueSequence(
          new Dialogue(@"Sound\Dialogue\UndeadCampaign\Undead02\U02PaladinD10.flac",
            "Vile betrayer! You are not fit enough to even carry your father's name! Why Uther ever vouched for you is beyond me. You've stripped him of his honor by casting yours to the winds! You deserve a gruesome death, boy!",
            "Alexandros Mograine")
        )
        , new Faction[]
        {
          scourge,
          this
        }, new List<Objective>
        {
          new ObjectiveLegendMeetsLegend(_allLegendSetup.Lordaeron.Mograine, _allLegendSetup.Scourge.Arthas)
        }
      ));
    }

    private void RegisterDalaranDialogue(Dalaran dalaran)
    {
      TriggeredDialogueManager.Add(
        new TriggeredDialogue(
          new DialogueSequence(
            new Dialogue(
              @"Sound\Dialogue\HumanCampaign\Human03\H03Arthas06",
              "Looks like you haven't lost your touch. It's good to see you again, Jaina.",
              "Arthas Menethil"),
            new Dialogue(
              @"Sound\Dialogue\HumanCampaign\Human03\H03Jaina07",
              "You too, Arthas. It's been awhile since a prince escorted me anywhere.",
              "Jaina Proudmoore"),
            new Dialogue(
              @"Sound\Dialogue\HumanCampaign\Human03\H03Arthas08",
              "Yes, it has. Well, I guess we should get underway.",
              "Arthas Menethil")
          ),
          new[]
          {
            this
          }, new Objective[]
          {
            new ObjectiveLegendMeetsLegend(_allLegendSetup.Lordaeron.Arthas, _allLegendSetup.Dalaran.Jaina)
          }));
    }
    
    private void RegisterScourgeLegionDialogue(Scourge scourge, Legion legion)
    {
      TriggeredDialogueManager.Add(
        new TriggeredDialogue(
          new DialogueSequence(
            new Dialogue(
              @"Sound\Dialogue\HumanCampaign\Human05\H05Arthas10",
              "Oh no...",
              "Arthas Menethil"),
            new Dialogue(
              @"Sound\Dialogue\HumanCampaign\Human05\H05Arthas11",
              "The plague was never meant to simply kill my people. It was meant to turn them... into the undead! Defend yourselves!",
              "Arthas Menethil")
          ),
          new[]
          {
            this
          }, new Objective[]
          {
            new ObjectiveQuestComplete(scourge.GetQuestByType<QuestPlague>()),
            new ObjectiveControlLegend(_allLegendSetup.Lordaeron.Arthas, false)
            {
              EligibleFactions = new List<Faction> { this }
            }
          }));
    }
    
    private void RegisterCrownOfLordaeronDrop()
    {
      CreateTrigger()
        .RegisterUnitEvent(_allLegendSetup.Lordaeron.CapitalPalace.Unit, EVENT_UNIT_CHANGE_OWNER)
        .AddAction(() =>
        {
          var lordaeronPlayer = Player;
          if (lordaeronPlayer?.GetTeam()?.Contains(GetOwningPlayer(GetTriggerUnit())) == true){
            return;
          }
          _allLegendSetup.Lordaeron.Terenas.Unit?.Kill();
          SetDoodadAnimation(Regions.King_Arthas_crown.Center.X, Regions.King_Arthas_crown.Center.Y, 200,
            FourCC("Ysaw"), false, "hide", false);
          SetDoodadAnimation(Regions.King_Arthas_crown.Center.X, Regions.King_Arthas_crown.Center.Y, 200,
            FourCC("D044"), false, "hide", false);
          SetDoodadAnimation(Regions.King_Arthas_crown.Center.X, Regions.King_Arthas_crown.Center.Y, 200,
            FourCC("YObb"), false, "hide", false);
          SetDoodadAnimationRect(Regions.Terenas.Rect, FourCC("YScr"), "show", false);
          DestroyTrigger(GetTriggeringTrigger());
        });
    }
  }
}