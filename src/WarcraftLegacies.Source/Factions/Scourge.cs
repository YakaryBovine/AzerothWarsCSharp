using System.Collections.Generic;
using MacroTools;
using MacroTools.DialogueSystem;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.ObjectiveSystem.Objectives.QuestBased;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using MacroTools.Powers;
using WarcraftLegacies.Shared.FactionObjectLimits;
using WarcraftLegacies.Source.FactionMechanics.Scourge;
using WarcraftLegacies.Source.FactionMechanics.Scourge.Blight;
using WarcraftLegacies.Source.Powers;
using WarcraftLegacies.Source.Quests.Scourge;
using WarcraftLegacies.Source.Setup;
using WCSharp.Shared.Data;

namespace WarcraftLegacies.Source.Factions
{
  public sealed class Scourge : Faction
  {
    private readonly PreplacedUnitSystem _preplacedUnitSystem;
    private readonly AllLegendSetup _allLegendSetup;
    private readonly ArtifactSetup _artifactSetup;

    /// <inheritdoc />
    
    public Scourge(PreplacedUnitSystem preplacedUnitSystem, AllLegendSetup allLegendSetup, ArtifactSetup artifactSetup)
      : base("Scourge", PLAYER_COLOR_PURPLE, "|c00540081",
        @"ReplaceableTextures\CommandButtons\BTNRevenant.blp")
    {
      TraditionalTeam = TeamSetup.Legion;
      _preplacedUnitSystem = preplacedUnitSystem;
      _allLegendSetup = allLegendSetup;
      _artifactSetup = artifactSetup;
      UndefeatedResearch = UPGRADE_R05K_SCOURGE_EXISTS;
      StartingGold = 200;
      FoodMaximum = 250;
      CinematicMusic = "ArthasTheme";
      ControlPointDefenderUnitTypeId = UNIT_U028_CONTROL_POINT_DEFENDER_SCOURGE;
      IntroText = @"You are playing as the the horrific Undead Scourge.

You begin in Northrend, a vast and isolated land; perfect to raise an army of undying warriors to destroy the living.

The local Nerubians have declared war on you. Destroy their decrepit holdings and kill their Queen to secure the continent.

Coordinate with the Burning Legion and use the Plague of Undeath to sweep Lordaeron away.

When the Plague hits Lordaeron, you will have a choice to where you want all your military units to be instantly transported.";
      GoldMines = new List<unit>
      {
        _preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(-4939, 18803))
      };
      Nicknames = new List<string>
      {
        "ud",
        "undead"
      };
      RegisterFactionDependentInitializer<Quelthalas>(RegisterQuelthalasRelatedQuests);
      RegisterFactionDependentInitializer<Lordaeron>(RegisterLordaeronRelatedQuests);
      RegisterFactionDependentInitializer<Lordaeron, Legion>(RegisterLordaeronLegionRelatedQuests);
      RegisterFactionDependentInitializer<Dalaran>(RegisterDalaranDialogue);
      RegisterFactionDependentInitializer<Quelthalas>(RegisterQuelthalasDialogue);
      RegisterFactionDependentInitializer<Lordaeron>(RegisterLordaeronDialogue);
      RegisterFactionDependentInitializer<Legion>(RegisterLegionDialogue);
    }

    /// <inheritdoc />
    public override void OnRegistered()
    {
      RegisterObjectLimits();
      RegisterQuests();
      RegisterPowers();
      RegisterDialogue();
      BlightSystem.Setup(this);
      BlightSetup.Setup(_preplacedUnitSystem);
      SacrificeAcolyte.Setup();
      SharedFactionConfigSetup.AddSharedFactionConfig(this);
      TheFrozenThrone.Setup(this, _allLegendSetup.Scourge.TheFrozenThrone);
    }

    private void RegisterObjectLimits()
    {
      foreach (var (objectTypeId, objectLimit) in ScourgeObjectLimitData.GetAllObjectLimits())
        ModObjectLimit(FourCC(objectTypeId), objectLimit.Limit);

      //Abilities
      ModAbilityAvailability(ABILITY_A0WG_SPELL_SHIELD_SPELL_BOOK_ORANGE_ANTONIDAS_RED_LICH_KING, -1);
      ModAbilityAvailability(ABILITY_A0K2_RAISE_DEAD_AUTO_CAST_RED_TEMPLE_OF_THE_DAMNED_OFF, -1);
      ModAbilityAvailability(ABILITY_A09N_PERMANENT_IMMOLATION_SCOURGE_ICECROWN_OBELISK, -1);
    }

    private void RegisterQuests()
    {
      QuestSpiderWar questSpiderWar = new(Regions.Ice_Crown);
      QuestEnKilahUnlock questEnKilahUnlock = new(Regions.EnKilahUnlock);
      QuestDrakUnlock questDrakUnlock = new(Regions.DrakUnlock, _allLegendSetup.Scourge.Kelthuzad);

      QuestSapphiron questSapphiron = new(_preplacedUnitSystem.GetUnit(UNIT_UBDR_SAPPHIRON_CREEP),
        _allLegendSetup.Scourge.Kelthuzad);

      QuestLichKingArthas questLichKingArthas =
        new(_preplacedUnitSystem.GetUnit(UNIT_H00O_UTGARDE_KEEP_SCOURGE_OTHER),
          _artifactSetup.HelmOfDomination,
          _allLegendSetup.Scourge.Arthas,
          _allLegendSetup.Scourge.TheFrozenThrone);
      QuestSlumberingKing questSlumberingKing = new();
      
      AddQuest(questSpiderWar);
      StartingQuest = questSpiderWar;
      AddQuest(questDrakUnlock);
      AddQuest(questEnKilahUnlock);
      AddQuest(questSapphiron);
      //Misc
      AddQuest(questLichKingArthas);
      AddQuest(questSlumberingKing);
    }

    private void RegisterPowers()
    {
      var allSeeing = new RegionVisionPower("All-Seeing",
        "Grants permanent vision over Northrend.",
        "Charm", new[]
        {
          Regions.Storm_Peaks,
          Regions.Central_Northrend,
          Regions.The_Basin,
          Regions.Ice_Crown,
          Regions.Fjord,
          Regions.Eastern_Northrend,
          Regions.Far_Eastern_Northrend,
          Regions.Coldarra,
          Regions.Borean_Tundra,
          Regions.IcecrownShipyard
        });
      AddPower(allSeeing);

      var domination = new Domination
      {
        ResearchId = UPGRADE_R008_DOMINATION_POWER,
        MindlessUndeadUnitTypes = new List<int>
        {
          UNIT_UGHO_GHOUL_SCOURGE,
          UNIT_U012_HALF_GHOUL_SCOURGE,
          UNIT_UABO_ABOMINATION_SCOURGE,
          UNIT_UFRO_FROST_WYRM_SCOURGE,
          UNIT_UCRM_BURROWED_CRYPT_FIEND_RED,
          UNIT_UCRY_CRYPT_FIEND_SCOURGE
        },
        DependentArtifact = _artifactSetup.HelmOfDomination,
        IconName = "Revenant",
        ValidHolders = new List<Legend>
        {
          _allLegendSetup.Scourge.TheFrozenThrone,
          _allLegendSetup.Scourge.Arthas
        }
      };
      
      AddPower(domination);
    }

    private void RegisterDialogue()
    {
      TriggeredDialogueManager.Add(
        new TriggeredDialogue(new Dialogue(
            soundFile: @"Sound\Dialogue\UndeadCampaign\Undead08\U08Kelthuzad18.flac",
            caption: "Come forth, Lord Archimonde! Enter this world, and let us bask in your power!",
            speaker: "Kel'thuzad"),
          null,
          new[]
          {
            new ObjectiveStartSpell(ABILITY_A00J_SUMMON_THE_BURNING_LEGION_ALL_FACTIONS, false,
              _allLegendSetup.Scourge.Kelthuzad)
          }));
    }
    
    private void RegisterDalaranDialogue(Dalaran dalaran)
    {
      TriggeredDialogueManager.Add(new TriggeredDialogue(
        new DialogueSequence(
          new Dialogue(@"Sound\Dialogue\UndeadCampaign\Undead07\U07Arthas01.flac",
            "Wizards of the Kirin Tor! I am Arthas, first of the Lich King's death knights! I demand that you open your gates and surrender to the might of the Scourge!",
            "Arthas Menethil"),
          new Dialogue(@"Sound\Dialogue\UndeadCampaign\Undead07\U07Antonidas02.flac",
            "Greetings, Prince Arthas. How fares your noble father?",
            "Antonidas"),
          new Dialogue(@"Sound\Dialogue\UndeadCampaign\Undead07\U07Arthas03.flac",
            "Lord Antonidas. There's no need to be snide.",
            "Arthas Menethil"),
          new Dialogue(@"Sound\Dialogue\UndeadCampaign\Undead07\U07Antonidas04.flac",
            "We've prepared for your coming, Arthas. My brethren and I have erected auras that will destroy any undead that pass through them.",
            "Antonidas"),
          new Dialogue(@"Sound\Dialogue\UndeadCampaign\Undead07\U07Arthas05.flac",
            "Your petty magics will not stop me, Antonidas.",
            "Arthas Menethil"),
          new Dialogue(@"Sound\Dialogue\UndeadCampaign\Undead07\U07Antonidas06.flac",
            "Pull your troops back, or we will be forced to unleash our full powers against you! Make your choice, death knight.",
            "Antonidas")
        ), new Faction[]
        {
          this,
          dalaran
        }, new List<Objective>
        {
          new ObjectiveLegendInRect(_allLegendSetup.Dalaran.Antonidas, Regions.Dalaran, "Dalaran"),
          new ObjectiveLegendInRect(_allLegendSetup.Scourge.Arthas, Regions.Dalaran, "Dalaran")
        }
      ));
    }

    private void RegisterLordaeronDialogue(Lordaeron lordaeron)
    {
      TriggeredDialogueManager.Add(
        new TriggeredDialogue(new Dialogue(
          soundFile: @"Sound\Dialogue\HumanCampaign\Human04\H04Kelthuzad28.flac",
          caption:
          "Naive fool. My death will make little difference in the long run. For now, the scourging of this land... begins.",
          speaker: "Kel'thuzad"), new Faction[]
        {
          this,
          lordaeron,
        }, new[]
        {
          new ObjectiveLegendDead(_allLegendSetup.Scourge.Kelthuzad)
        }));
    }

    private void RegisterQuelthalasDialogue(Quelthalas quelthalas)
    {
      TriggeredDialogueManager.Add(
        new TriggeredDialogue(
          new DialogueSequence(
            new Dialogue(
              soundFile: @"Sound\Dialogue\UndeadCampaign\Undead03A\U03AArthas01.flac",
              caption: "Ah, wondrous, eternal Quel'Thalas. I haven't been here since I was a boy.",
              speaker: "Arthas Menethil"),
            new Dialogue(
              soundFile: @"Sound\Dialogue\UndeadCampaign\Undead03A\U03AKelThuzad02.flac",
              caption: "Be wary. The elves likely wait in ambush.",
              speaker: "Kel'thuzad"),
            new Dialogue(
              soundFile: @"Sound\Dialogue\UndeadCampaign\Undead03A\U03AArthas03.flac",
              caption:
              "The frail elves do not concern me, necromancer. Our forces are strengthened with every foe we slay.",
              speaker: "Arthas Menethil"),
            new Dialogue(
              soundFile: @"Sound\Dialogue\UndeadCampaign\Undead03A\U03AKelthuzad04.flac",
              caption: "Don't be too overconfident, death knight. The elves must not be taken lightly.",
              speaker: "Kel'thuzad")),
          new Faction[]
          {
            this,
            quelthalas
          },
          new Objective[]
          {
            new ObjectiveLegendInRect(_allLegendSetup.Scourge.Arthas, Regions.QuelthalasAmbient, "Quel'thalas"),
            new ObjectiveLegendInRect(_allLegendSetup.Scourge.Kelthuzad, Regions.QuelthalasAmbient, "Quel'thalas"),
            new ObjectiveQuestComplete(GetQuestByType<QuestKelthuzadDies>())
            {
              EligibleFactions = new List<Faction> { this }
            },
            new ObjectiveQuestNotComplete(GetQuestByType<QuestKelthuzadLich>())
          }));

      TriggeredDialogueManager.Add(
        new TriggeredDialogue(
          new DialogueSequence(
            new Dialogue(
              soundFile: @"Sound\Dialogue\UndeadCampaign\Undead03A\U03ASylvanas12.flac",
              caption:
              "You are not welcome here. I am Sylvanas Windrunner, Ranger-General of Silvermoon. I advise you to turn back now.",
              speaker: "Sylvanas Windrunner"),
            new Dialogue(
              soundFile: @"Sound\Dialogue\UndeadCampaign\Undead03A\U03AArthas13.flac",
              caption: "It is you who should turn back, Sylvanas. Death itself has come for your land.",
              speaker: "Arthas Menethil")),
          new Faction[]
          {
            this,
            quelthalas
          },
          new[]
          {
            new ObjectiveLegendMeetsLegend(_allLegendSetup.Scourge.Arthas, _allLegendSetup.Quelthalas.Sylvanas)
          }));

      TriggeredDialogueManager.Add(
        new TriggeredDialogue(
          new DialogueSequence(
            new Dialogue(
              soundFile: @"Sound\Dialogue\UndeadCampaign\Undead05A\U05AArthas30.flac",
              caption: "Now, arise, Kel'Thuzad, and serve the Lich King once again!",
              speaker: "Arthas Menethil"),
            new Dialogue(
              soundFile: @"Sound\Dialogue\UndeadCampaign\Undead05A\U05AKelThuzad31.flac",
              caption: "I am reborn, as promised! The Lich King has granted me eternal life!",
              speaker: "Kel'thuzad")),
          new Faction[]
          {
            this,
            quelthalas
          },
          new[]
          {
            new ObjectiveQuestComplete(GetQuestByType<QuestKelthuzadLich>())
            {
              EligibleFactions = new List<Faction> { this }
            }
          }));
      
      TriggeredDialogueManager.Add(
        new TriggeredDialogue(
          new DialogueSequence(
            new Dialogue(
              soundFile: @"Sound\Dialogue\UndeadCampaign\Undead02\U02KelThuzad27.flac",
              caption: "Told you my death would mean little.",
              speaker: "Kel'thuzad"),
            new Dialogue(
              soundFile: @"Sound\Dialogue\UndeadCampaign\Undead02\U02Arthas28.flac",
              caption: "What the... Am I hearing ghosts now?",
              speaker: "Arthas Menethil"),
            new Dialogue(
              soundFile: @"Sound\Dialogue\UndeadCampaign\Undead02\U02KelThuzad29.flac",
              caption: "It is I, Kel'Thuzad. I was right about you, Prince Arthas.",
              speaker: "Kel'thuzad")),
          new[] { this },
          new Objective[]
          {
            new ObjectiveLegendMeetsLegend(_allLegendSetup.Scourge.Arthas, _allLegendSetup.Scourge.Kelthuzad),
            new ObjectiveQuestComplete(GetQuestByType<QuestKelthuzadDies>())
            {
              EligibleFactions = new List<Faction> { this }
            },
            new ObjectiveQuestNotComplete(GetQuestByType<QuestKelthuzadLich>())
            {
              EligibleFactions = new List<Faction> { this }
            }
          }));
    }

    private void RegisterLegionDialogue(Legion legion)
    {
      TriggeredDialogueManager.Add(
        new TriggeredDialogue(
          new DialogueSequence(
            new Dialogue(
              soundFile: @"Sound\Dialogue\UndeadCampaign\Undead05A\U05AArthas02.flac",
              caption: "I was wondering when you'd show up.",
              speaker: "Arthas Menethil"),
            new Dialogue(
              soundFile: @"Sound\Dialogue\UndeadCampaign\Undead05A\U05ATichondrius03.flac",
              caption: "I am here to ensure that you do your job, little human. Not do it for you.",
              speaker: "Tichondrius")),
          new Faction[]
          {
            this,
            legion
          },
          new[]
          {
            new ObjectiveLegendMeetsLegend(_allLegendSetup.Scourge.Arthas, _allLegendSetup.Legion.Tichondrius)
          }));
    }
    
    private void RegisterQuelthalasRelatedQuests(Quelthalas quelthalas)
    {
      var questKelthuzadLich = AddQuest(new QuestKelthuzadLich(_allLegendSetup.Quelthalas.Sunwell,
        _allLegendSetup.Scourge.Kelthuzad, quelthalas));
      AddQuest(new QuestKelthuzadDies(questKelthuzadLich, _allLegendSetup.Scourge.Kelthuzad));
    }
    
    private void RegisterLordaeronRelatedQuests(Faction lordaeron)
    {
      var stratholme = _allLegendSetup.Lordaeron.Stratholme;
      var arthas = _allLegendSetup.Lordaeron.Arthas;
      
      QuestDestroyStratholme questDestroyStratholme = new(lordaeron, stratholme, arthas);
      AddQuest(questDestroyStratholme);
      
      AddQuest(new QuestCultoftheDamned(lordaeron, _allLegendSetup.Scourge.Rivendare));
    }

    private void RegisterLordaeronLegionRelatedQuests(Lordaeron lordaeron, Legion legion)
    {
      var plagueParameters = new PlagueParameters();
      plagueParameters.PlagueRects = new List<Rectangle>
      {
        Regions.Plague_1,
        Regions.Plague_2,
        Regions.Plague_3,
        Regions.Plague_4,
        Regions.Plague_5,
        Regions.Plague_6,
        Regions.Plague_7
      };
      plagueParameters.PlagueArmySummonParameters = new List<PlagueArmySummonParameter>
      {
        new(1, UNIT_UACO_ACOLYTE_SCOURGE_WORKER),
        new(2, UNIT_UGHO_GHOUL_SCOURGE),
        new(2, UNIT_UCRY_CRYPT_FIEND_SCOURGE),
        new(2, UNIT_UABO_ABOMINATION_SCOURGE),
      };
      plagueParameters.AttackTargets = new List<Point>
      {
        new Point(9041, 8036),
        new Point(13825, 12471),
        new Point(9418, 5396)
      };
      
      AddQuest(new QuestPlague(plagueParameters, lordaeron, legion, Regions.DeathknellUnlock,
        Regions.StratholmeScourgeBase, Regions.CaerDarrow));
    }
  }
}