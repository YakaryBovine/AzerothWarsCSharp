using System.Collections.Generic;
using MacroTools;
using MacroTools.DialogueSystem;
using MacroTools.FactionSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.ObjectiveSystem.Objectives.QuestBased;
using MacroTools.ResearchSystems;
using WarcraftLegacies.Source.FactionMechanics.Frostwolf;
using WarcraftLegacies.Source.Quests.Frostwolf;
using WarcraftLegacies.Source.Quests.Warsong;
using WarcraftLegacies.Source.Researches;
using WarcraftLegacies.Source.Setup;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Factions
{
  public sealed class Frostwolf : Faction
  {
    private readonly AllLegendSetup _allLegendSetup;
    private readonly ArtifactSetup _artifactSetup;
    private readonly PreplacedUnitSystem _preplacedUnitSystem;

    /// <inheritdoc />
    public Frostwolf(PreplacedUnitSystem preplacedUnitSystem, AllLegendSetup allLegendSetup,
      ArtifactSetup artifactSetup) : base("Frostwolf", PLAYER_COLOR_RED, "|c00ff0303",
      @"ReplaceableTextures\CommandButtons\BTNThrall.blp")
    {
      _preplacedUnitSystem = preplacedUnitSystem;
      _allLegendSetup = allLegendSetup;
      _artifactSetup = artifactSetup;
      UndefeatedResearch = Constants.UPGRADE_R05V_FROSTWOLF_EXISTS;
      StartingGold = 200;
      StartingLumber = 700;
      CinematicMusic = "SadMystery";
      ControlPointDefenderUnitTypeId = Constants.UNIT_N0B6_CONTROL_POINT_DEFENDER_FROSTWOLF;
      IntroText = @"You are playing as the honorable |cffff0000Frostwolf Clan|r.

You begin in the Salt Flats, separated from your ally, the Warsong Clan in the North.

Salvage the wrecked ships, establish a base and gather your troops to move inland and assist your ally against the Night Elf threat.";
      GoldMines = new List<unit>
      {
        _preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(-8793, -11350)),
        _preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(-15828, -3120))
      };
      RegisterFactionDependentInitializer<Warsong>(RegisterWarsongDialogue);
      RegisterFactionDependentInitializer<Warsong>(RegisterWarsongRelatedResearches);
    }

    /// <inheritdoc />
    public override void OnRegistered()
    {
      RegisterObjectLimits();
      RegisterQuests();
      RegisterDialogue();
      PeonsStartHarvestingShips.Setup(_preplacedUnitSystem);
      SharedFactionConfigSetup.AddSharedFactionConfig(this);
    }

    private void RegisterObjectLimits()
    {
      ModObjectLimit(Constants.UNIT_OGRE_GREAT_HALL_FROSTWOLF_T1, UNLIMITED);
      ModObjectLimit(Constants.UNIT_OSTR_STRONGHOLD_FROSTWOLF_T2, UNLIMITED);
      ModObjectLimit(Constants.UNIT_OFRT_FORTRESS_FROSTWOLF_T3, UNLIMITED);
      ModObjectLimit(Constants.UNIT_OALT_ALTAR_OF_STORMS_FROSTWOLF_ALTAR, UNLIMITED);
      ModObjectLimit(Constants.UNIT_OBAR_WAR_CAMP_FROSTWOLF_BARRACKS, UNLIMITED);
      ModObjectLimit(Constants.UNIT_OFOR_WAR_MILL_FROSTWOLF_RESEARCH, UNLIMITED);
      ModObjectLimit(Constants.UNIT_OTTO_TAUREN_TOTEM_FROSTWOLF_SIEGE, UNLIMITED);
      ModObjectLimit(Constants.UNIT_OSLD_SPIRIT_LODGE_FROSTWOLF_MAGIC, UNLIMITED);
      ModObjectLimit(Constants.UNIT_OTRB_BURROW_FROSTWOLF_FARM, UNLIMITED);
      ModObjectLimit(Constants.UNIT_OWTW_WATCH_TOWER_FROSTWOLF_TOWER, UNLIMITED);
      ModObjectLimit(Constants.UNIT_O002_IMPROVED_WATCH_TOWER_FROSTWOLF_TOWER_2, UNLIMITED);
      ModObjectLimit(Constants.UNIT_OVLN_VOODOO_LOUNGE_FROSTWOLF_SHOP, UNLIMITED);
      ModObjectLimit(Constants.UNIT_OSHY_HORDE_PIER_FROSTWOLF_SHIPYARD, UNLIMITED);
      ModObjectLimit(Constants.UNIT_OOSC_PACK_KODO_FROSTWOLF, UNLIMITED);
      ModObjectLimit(Constants.UNIT_OBEA_BEASTIARY_FROSTWOLF_SPECIALIST, UNLIMITED);

      ModObjectLimit(Constants.UNIT_OPEO_PEON_FROSTWOLF_WARSONG_WORKER, UNLIMITED);
      ModObjectLimit(Constants.UNIT_OGRU_GRUNT_FROSTWOLF, UNLIMITED);
      ModObjectLimit(Constants.UNIT_OTAU_TAUREN_FROSTWOLF, UNLIMITED);
      ModObjectLimit(Constants.UNIT_OHUN_HEADHUNTER_FROSTWOLF, UNLIMITED);
      ModObjectLimit(Constants.UNIT_OCAT_CATAPULT_FROSTWOLF, 6);
      ModObjectLimit(Constants.UNIT_OTBR_BATRIDER_FROSTWOLF, 12);
      ModObjectLimit(Constants.UNIT_ODOC_WITCH_DOCTOR_FROSTWOLF, UNLIMITED);
      ModObjectLimit(Constants.UNIT_OSHM_SHAMAN_FROSTWOLF, UNLIMITED);
      ModObjectLimit(Constants.UNIT_OSPW_SPIRIT_WALKER_FROSTWOLF, UNLIMITED);
      ModObjectLimit(Constants.UNIT_O00A_FAR_SEER_FROSTWOLF_ELITE, 6);

      ModObjectLimit(Constants.UNIT_O06T_TAUREN_GLADIATOR_FROSTWOLF, 6);
      ModObjectLimit(Constants.UNIT_H0CN_PACKLEADER_FROSTWOLF, 4);
      ModObjectLimit(Constants.UNIT_H0CO_MAMMOTH_WRANGLER_FROSTWOLF, 2);
      ModObjectLimit(Constants.UNIT_N049_WANDERER_FROSTWOLF, 4);

      //Ship
      ModObjectLimit(FourCC("obot"), UNLIMITED); //Transport Ship
      ModObjectLimit(FourCC("h0AS"), UNLIMITED); //Scout
      ModObjectLimit(FourCC("h0AP"), UNLIMITED); //Frigate
      ModObjectLimit(FourCC("h0B2"), UNLIMITED); //Fireship
      ModObjectLimit(FourCC("h0AY"), UNLIMITED); //Galley
      ModObjectLimit(FourCC("h0B5"), UNLIMITED); //Boarding
      ModObjectLimit(FourCC("h0BC"), UNLIMITED); //Juggernaut
      ModObjectLimit(FourCC("h0AO"), 6); //Bombard

      ModObjectLimit(FourCC("h00C"), 1); //Drek)thar
      ModObjectLimit(FourCC("Othr"), 1); //Thrall
      ModObjectLimit(FourCC("Ocbh"), 1); //Cairne
      ModObjectLimit(FourCC("Orkn"), 1); //Voljin
      ModObjectLimit(FourCC("Orex"), 1); //Rexxar

      ModObjectLimit(FourCC("Rows"), UNLIMITED); //Improved Pulverize
      ModObjectLimit(FourCC("Rost"), UNLIMITED); //Shaman Adept Training
      ModObjectLimit(FourCC("Rowd"), UNLIMITED); //Witch Doctor Adept Training
      ModObjectLimit(FourCC("Rowt"), UNLIMITED); //Spirit Walker Adept Training
      ModObjectLimit(FourCC("Rolf"), UNLIMITED); //Liquid Fire
      ModObjectLimit(FourCC("Rosp"), UNLIMITED); //Spiked Barricades
      ModObjectLimit(FourCC("Rorb"), UNLIMITED); //reinforced Defenses
      ModObjectLimit(FourCC("R00H"), UNLIMITED); //Animal Companion
      ModObjectLimit(FourCC("R00R"), UNLIMITED); //Improved Chain Lightning
      ModObjectLimit(FourCC("R00W"), UNLIMITED); //Toughened Hides
      ModObjectLimit(FourCC("R01Z"), UNLIMITED); //Battle Stations
      SetObjectLevel(FourCC("R01Z"), 1); //Battle Stations

      ModAbilityAvailability(Constants.ABILITY_A0PF_FEL_ENERGY_TEAL_FORTRESSES, -1);
      ModAbilityAvailability(Constants.ABILITY_ANTR_TROLL_REGENERATION_PINK_WITCH_DOCTOR_TROLL_HEADHUNTER_TROLL_BATRIDER_DARKSPEAR_WARLORD_TROLL_BERSERKER_ICON, -1);
      ModAbilityAvailability(Constants.ABILITY_A0M4_BATTLE_STATIONS_PINK_GREY_ORC_BURROW_BLOODPACT, -1);
      ModAbilityAvailability(Constants.ABILITY_ABTL_BATTLE_STATIONS_FROSTWOLF_WARSONG_BURROW, 1);
      ModAbilityAvailability(Constants.ABILITY_A0GM_FOR_THE_HORDE_PINK_GREY_MAIN_BUILDINGS, 1);
      
      ModObjectLimit(Constants.UPGRADE_R09N_FLIGHT_PATH_WARSONG, 1);
    }
    
    private void RegisterQuests()
    {
      StartingQuest = AddQuest(new QuestThunderBluff(_preplacedUnitSystem, Regions.ThunderBluff));
      AddQuest(new QuestStonemaul(_preplacedUnitSystem, Regions.StonemaulKeep));
      AddQuest(new QuestDarkspear());
      AddQuest(new QuestRagetotem(_allLegendSetup.Frostwolf.Cairne));
      AddQuest(new QuestHighmountain(_allLegendSetup.Frostwolf.Cairne, Regions.Highmountain_Unlock));
      AddQuest(new QuestMammoth(_allLegendSetup.Frostwolf.Rexxar));
      AddQuest(new QuestDrektharsSpellbook(_allLegendSetup.Druids.Nordrassil, _allLegendSetup.Frostwolf.Thrall));
      AddQuest(new QuestFreeNerzhul(_allLegendSetup.Scourge.TheFrozenThrone, _allLegendSetup.Frostwolf.Thrall));
      AddQuest(new QuestWorldShaman(_allLegendSetup.Frostwolf.Thrall));
      AddQuest(new QuestScepterOfTheQueenWarsong(Regions.TheAthenaeum, _artifactSetup.ScepterOfTheQueen, _allLegendSetup.Sentinels.Auberdine));
    }

    private void RegisterDialogue()
    {
      TriggeredDialogueManager.Add(
        new TriggeredDialogue(new DialogueSequence(new Dialogue(
              @"Sound\Dialogue\OrcExpCamp\OrcQuest00x\D00Rexxar01",
              "I have wandered alone for many years, little Misha. Yet sometimes, even I grow weary of this endless solitude.",
              "Rexxar"),
            new Dialogue(
              @"Sound\Dialogue\OrcExpCamp\OrcQuest00x\D00Rexxar02",
              "I have watched the other races. I have seen their squabbling, their ruthlessness. Their wars do nothing but scar the land and drive the wild things to extinction.",
              "Rexxar"),
            new Dialogue(
              @"Sound\Dialogue\OrcExpCamp\OrcQuest00x\D00Rexxar03",
              "No, they cannot be trusted. Only beasts are above deceit.",
              "Rexxar"))
          , new[]
          {
            this
          }, new[]
          {
            new ObjectiveControlLegend(_allLegendSetup.Frostwolf.Rexxar, false)
            {
              EligibleFactions = new List<Faction> { this }
            }
          }));

      TriggeredDialogueManager.Add(
        new TriggeredDialogue(new DialogueSequence(new Dialogue(
              @"Sound\Dialogue\OrcExpCamp\OrcQuest00x\D00Thrall25",
              "Who are you, warrior?",
              "Thrall"),
            new Dialogue(
              @"Sound\Dialogue\OrcExpCamp\OrcQuest00x\D00Rexxar26",
              "I am Rexxar, last son of the Mok'Nathal.",
              "Rexxar"))
          , new[]
          {
            this
          }, new[]
          {
            new ObjectiveLegendMeetsLegend(_allLegendSetup.Frostwolf.Thrall, _allLegendSetup.Frostwolf.Rexxar)
          }));

      TriggeredDialogueManager.Add(
        new TriggeredDialogue(new DialogueSequence(new Dialogue(
              @"Sound\Dialogue\OrcCampaign\Orc01\O01Grunt01",
              "Warchief, our ship sustained heavy damage when we passed through the raging maelstrom. It's unsalvageable.",
              "Grunt"),
            new Dialogue(
              @"Sound\Dialogue\OrcCampaign\Orc01\O01Thrall02",
              "I knew it. Can we confirm our location? Is this Kalimdor?",
              "Thrall"),
            new Dialogue(
              @"Sound\Dialogue\OrcCampaign\Orc01\O01Grunt03",
              "We traveled due west, as you instructed. This should be it.",
              "Grunt"))
          , new[]
          {
            this
          }, new[]
          {
            new ObjectiveControlLegend(_allLegendSetup.Frostwolf.Thrall, false)
            {
              EligibleFactions = new List<Faction> { this }
            }
          }));

      TriggeredDialogueManager.Add(
        new TriggeredDialogue(new DialogueSequence(new Dialogue(
              @"Sound\Dialogue\OrcCampaign\Orc01\O01Cairne23",
              "I am Cairne, chief of the Bloodhoof tauren. You greenskins fight with both savagery and valor. I am intrigued.",
              "Cairne Bloodhoof"),
            new Dialogue(
              @"Sound\Dialogue\OrcCampaign\Orc01\O01Thrall24",
              "I am Thrall, and these are my brethren, the orcs. We've come seeking the destiny promised to us.",
              "Thrall"))
          , new[]
          {
            this
          }, new[]
          {
            new ObjectiveLegendMeetsLegend(_allLegendSetup.Frostwolf.Cairne, _allLegendSetup.Frostwolf.Thrall)
          }));
    }
    
    private void RegisterWarsongDialogue(Warsong warsong)
    {
      TriggeredDialogueManager.Add(
        new TriggeredDialogue(new Dialogue(
          @"Sound\Dialogue\OrcCampaign\Orc08\O08Thrall07",
          "Hellscream is like a brother to me, Cairne. But he and his clan have fallen under the demon's influence. If I can't save him, then my people might be damned for all time.",
          "Thrall"), new[]
        {
          this
        }, new[]
        {
          new ObjectiveQuestComplete(warsong.GetQuestByType<QuestFountainOfBlood>())
        }));
    }
    
    private void RegisterWarsongRelatedResearches(Warsong warsong)
    {
      ResearchManager.Register(new FlightPath(warsong, this, Constants.UPGRADE_R09N_FLIGHT_PATH_WARSONG, 70, 75,
        _preplacedUnitSystem));
    }
  }
}