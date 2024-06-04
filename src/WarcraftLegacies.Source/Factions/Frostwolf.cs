using System.Collections.Generic;
using MacroTools;
using MacroTools.DialogueSystem;
using MacroTools.FactionSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.ObjectiveSystem.Objectives.QuestBased;
using MacroTools.ResearchSystems;
using WarcraftLegacies.Shared.Config.Factions;
using WarcraftLegacies.Source.Quests.Frostwolf;
using WarcraftLegacies.Source.Quests.Warsong;
using WarcraftLegacies.Source.Researches;
using WarcraftLegacies.Source.Setup;
using WCSharp.Shared.Data;

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
      TraditionalTeam = TeamSetup.Horde;
      _preplacedUnitSystem = preplacedUnitSystem;
      _allLegendSetup = allLegendSetup;
      _artifactSetup = artifactSetup;
      UndefeatedResearch = UPGRADE_R05V_FROSTWOLF_EXISTS;
      StartingGold = 200;
      CinematicMusic = "SadMystery";
      ControlPointDefenderUnitTypeId = UNIT_N0B6_CONTROL_POINT_DEFENDER_FROSTWOLF;
      IntroText = @"You are playing as the honorable |cffff0000Frostwolf Clan|r.

You begin in the Salt Flats, separated from your ally, the Warsong Clan in the North.

Salvage the wrecked ships, establish a base and gather your troops to move inland and assist your ally against the Night Elf threat.";
      GoldMines = new List<unit>
      {
        _preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(-8123, -2514)),
        _preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(-15828, -3120))
      };
      Nicknames = new List<string>
      {
        "fw",
        "frost",
        "frostwolves",
        "frostwolve"
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
      SharedFactionConfigSetup.AddSharedFactionConfig(this);
    }

    private void RegisterObjectLimits()
    {
      foreach (var (objectTypeId, objectLimit) in FrostwolfObjectLimitData.GetAllObjectLimits())
        ModObjectLimit(FourCC(objectTypeId), objectLimit);

      ModAbilityAvailability(ABILITY_A0PF_FEL_ENERGY_TEAL_FORTRESSES, -1);
      ModAbilityAvailability(ABILITY_ANTR_TROLL_REGENERATION_PINK_WITCH_DOCTOR_TROLL_HEADHUNTER_TROLL_BATRIDER_DARKSPEAR_WARLORD_TROLL_BERSERKER_ICON, -1);
      ModAbilityAvailability(ABILITY_A0M4_BATTLE_STATIONS_PINK_GREY_ORC_BURROW_BLOODPACT, -1);
      ModAbilityAvailability(ABILITY_ABTL_BATTLE_STATIONS_FROSTWOLF_WARSONG_BURROW, 1);
      ModAbilityAvailability(ABILITY_A0GM_FOR_THE_HORDE_PINK_GREY_MAIN_BUILDINGS, 1);
    }
    
    private void RegisterQuests()
    {
      StartingQuest = AddQuest(new QuestThunderBluff(Regions.ThunderBluff));
      AddQuest(new QuestStonemaul(Regions.StonemaulKeep));
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
          {
            EligibleFactions = new List<Faction> { warsong }
          }
        }));
    }
    
    private void RegisterWarsongRelatedResearches(Warsong warsong)
    {
      ResearchManager.Register(new FlightPath(warsong, this, UPGRADE_R09N_FLIGHT_PATH_WARSONG, 70,
        _preplacedUnitSystem));
    }
  }
}