using System.Collections.Generic;
using MacroTools;
using MacroTools.DialogueSystem;
using MacroTools.FactionSystem;
using MacroTools.ObjectiveSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.ObjectiveSystem.Objectives.QuestBased;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using MacroTools.Powers;
using WarcraftLegacies.Source.FactionMechanics.Scourge;
using WarcraftLegacies.Source.FactionMechanics.Scourge.Blight;
using WarcraftLegacies.Source.FactionMechanics.Scourge.Plague;
using WarcraftLegacies.Source.Quests.Scourge;
using WarcraftLegacies.Source.Setup;
using WCSharp.Shared.Data;
using static War3Api.Common;

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
      _preplacedUnitSystem = preplacedUnitSystem;
      _allLegendSetup = allLegendSetup;
      _artifactSetup = artifactSetup;
      UndefeatedResearch = Constants.UPGRADE_R05K_SCOURGE_EXISTS;
      StartingGold = 200;
      StartingLumber = 700;
      FoodMaximum = 250;
      CinematicMusic = "ArthasTheme";
      ControlPointDefenderUnitTypeId = Constants.UNIT_U028_CONTROL_POINT_DEFENDER_SCOURGE;
      IntroText = @"You are playing as the the horrific Undead Scourge.

You begin in Northrend, a vast and isolated land; perfect to raise an army of undying warriors to destroy the living.

The local Nerubians have declared war on you. Destroy their decrepit holdings and kill their Queen to secure the continent.

Coordinate with the Burning Legion and use the Plague of Undeath to sweep Lordaeron away.

When the Plague hits Lordaeron, you will have a choice to where you want all your military units to be instantly transported.";
      GoldMines = new List<unit>
      {
        _preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(-4939, 18803))
      };
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
      RegisterHelmOfDominationLogic();
      BlightSystem.Setup(this);
      BlightSetup.Setup(_preplacedUnitSystem);
      SacrificeAcolyte.Setup();
      SharedFactionConfigSetup.AddSharedFactionConfig(this);
    }

    private void RegisterObjectLimits()
    {
      //Buildings
      ModObjectLimit(FourCC("unpl"), UNLIMITED); //Necropolis
      ModObjectLimit(FourCC("unp1"), UNLIMITED); //Halls of the Dead
      ModObjectLimit(FourCC("unp2"), UNLIMITED); //Black Citadel
      ModObjectLimit(FourCC("uzig"), UNLIMITED); //Ziggurat
      ModObjectLimit(FourCC("uzg1"), UNLIMITED); //Spirit Tower
      ModObjectLimit(FourCC("uzg2"), UNLIMITED); //Nerubian Tower
      ModObjectLimit(FourCC("uaod"), UNLIMITED); //Altar of Darkness
      ModObjectLimit(FourCC("usep"), UNLIMITED); //Crypt
      ModObjectLimit(FourCC("ugrv"), UNLIMITED); //Graveyard
      ModObjectLimit(FourCC("uslh"), UNLIMITED); //Slaughterhouse
      ModObjectLimit(FourCC("utod"), UNLIMITED); //Temple of the Damned
      ModObjectLimit(FourCC("ubon"), UNLIMITED); //Boneyard
      ModObjectLimit(FourCC("utom"), UNLIMITED); //Tomb of Relics
      ModObjectLimit(FourCC("ushp"), UNLIMITED); //Undead Shipyard
      ModObjectLimit(FourCC("u002"), UNLIMITED); //Improved Spirit Tower
      ModObjectLimit(FourCC("u003"), UNLIMITED); //Improved Nerubian Tower

      //Units
      ModObjectLimit(FourCC("uaco"), UNLIMITED); //Acolyte
      ModObjectLimit(FourCC("ushd"), UNLIMITED); //Shade
      ModObjectLimit(FourCC("ugho"), UNLIMITED); //Ghoul
      ModObjectLimit(FourCC("uabo"), UNLIMITED); //Abomination
      ModObjectLimit(FourCC("umtw"), 8); //Meat Wagon
      ModObjectLimit(FourCC("ucry"), UNLIMITED); //Crypt Fiend
      ModObjectLimit(FourCC("ugar"), 12); //Gargoyle
      ModObjectLimit(FourCC("h02G"), UNLIMITED); //Vrykul Warrior
      ModObjectLimit(FourCC("h061"), 12); //Vrykul Champion
      ModObjectLimit(FourCC("h00P"), 4); //Mammoth rider
      ModObjectLimit(FourCC("uban"), UNLIMITED); //Banshee
      ModObjectLimit(FourCC("unec"), UNLIMITED); //Necromancer
      ModObjectLimit(FourCC("uobs"), 4); //Obsidian Statue
      ModObjectLimit(FourCC("ufro"), 4); //Frost Wyrm
      ModObjectLimit(FourCC("h00H"), 6); //Death Knight
      ModObjectLimit(FourCC("ubsp"), 6); //Destroyer
      ModObjectLimit(FourCC("nfgl"), 2); //Plague Titan

      //Ship
      ModObjectLimit(FourCC("ubot"), UNLIMITED); //Undead Transport Ship
      ModObjectLimit(FourCC("h0AT"), UNLIMITED); //Scout
      ModObjectLimit(FourCC("h0AW"), UNLIMITED); //Frigate
      ModObjectLimit(FourCC("h0AM"), UNLIMITED); //Fireship
      ModObjectLimit(FourCC("h0AZ"), UNLIMITED); //Galley
      ModObjectLimit(FourCC("h0AQ"), UNLIMITED); //Boarding
      ModObjectLimit(FourCC("h0BB"), UNLIMITED); //Juggernaut
      ModObjectLimit(FourCC("h0B9"), 6); //Bombard

      //Demi-Heroes
      ModObjectLimit(FourCC("ubdd"), 1); //Sapphiron
      ModObjectLimit(FourCC("uswb"), 1); //Banshee
      ModObjectLimit(FourCC("Uanb"), 1); //Anub'arak
      ModObjectLimit(FourCC("U001"), 1); //Kel'thuzad
      ModObjectLimit(FourCC("U00M"), 1); //Kel'thuzad (Ghost)
      ModObjectLimit(FourCC("U00A"), 1); //Rivendare
      ModObjectLimit(FourCC("Uktl"), 1); //Kel'thuzad (Lich)
      ModObjectLimit(Constants.UNIT_UEAR_CHAMPION_OF_THE_SCOURGE_SCOURGE, 1);

      //Upgrades
      ModObjectLimit(FourCC("Ruba"), UNLIMITED); //Banshee Adept Training
      ModObjectLimit(FourCC("Rubu"), UNLIMITED); //Burrow
      ModObjectLimit(FourCC("Ruex"), UNLIMITED); //Exhume Corpses
      ModObjectLimit(FourCC("Rufb"), UNLIMITED); //Freezing Breath
      ModObjectLimit(FourCC("Rugf"), UNLIMITED); //Ghoul Frenzy
      ModObjectLimit(FourCC("Rune"), UNLIMITED); //Necromancer Adept Training
      ModObjectLimit(FourCC("Ruwb"), UNLIMITED); //Web
      ModObjectLimit(FourCC("R00Q"), UNLIMITED); //Chilling Aura
      ModObjectLimit(Constants.UPGRADE_R01X_EPIDEMIC_SCOURGE, UNLIMITED);
      ModObjectLimit(Constants.UPGRADE_R01D_HOWL_OF_TERROR_SCOURGE, UNLIMITED);
      ModObjectLimit(Constants.UPGRADE_R06N_IMPROVED_ORB_OF_ANNIHILATION_SCOURGE, UNLIMITED);
      ModObjectLimit(Constants.UPGRADE_RUSL_SKELETAL_LONGEVITY, UNLIMITED);
      ModObjectLimit(Constants.UPGRADE_RUSM_SKELETAL_MASTERY, UNLIMITED);

      ModObjectLimit(Constants.UPGRADE_R07X_MAKE_ARTHAS_THE_LICH_KING_SCOURGE, UNLIMITED);

      //Abilities
      ModAbilityAvailability(Constants.ABILITY_A0WG_SPELL_SHIELD_SPELL_BOOK_ORANGE_ANTONIDAS_RED_LICH_KING, -1);
      ModAbilityAvailability(Constants.ABILITY_A0K2_RAISE_DEAD_AUTO_CAST_RED_TEMPLE_OF_THE_DAMNED_OFF, -1);
      ModAbilityAvailability(Constants.ABILITY_A09N_PERMANENT_IMMOLATION_SCOURGE_ICECROWN_OBELISK, -1);
    }

    private void RegisterQuests()
    {
      QuestSpiderWar questSpiderWar = new(Regions.Ice_Crown,
        _preplacedUnitSystem.GetUnit(Constants.UNIT_N074_QUEEN_NEZAR_AZRET));
      QuestEnKilahUnlock questEnKilahUnlock = new(Regions.EnKilahUnlock);
      QuestDrakUnlock questDrakUnlock = new(Regions.DrakUnlock, _allLegendSetup.Scourge.Kelthuzad);

      QuestSapphiron questSapphiron = new(_preplacedUnitSystem.GetUnit(Constants.UNIT_UBDR_SAPPHIRON_CREEP),
        _allLegendSetup.Scourge.Kelthuzad);

      QuestLichKingArthas questLichKingArthas =
        new(_preplacedUnitSystem.GetUnit(Constants.UNIT_H00O_UTGARDE_KEEP_SCOURGE_OTHER),
          _artifactSetup.HelmOfDomination,
          _allLegendSetup.Scourge.Arthas, _allLegendSetup.Scourge.TheFrozenThrone);
      QuestEnemyEncroachment questEnemyEncroachment = new();
      
      var questKelthuzadLich = AddQuest(new QuestKelthuzadLich(_allLegendSetup.Quelthalas.Sunwell, _allLegendSetup.Scourge.Kelthuzad));
      AddQuest(new QuestKelthuzadDies(questKelthuzadLich, _allLegendSetup.Scourge.Kelthuzad));
      
      AddQuest(questSpiderWar);
      StartingQuest = questSpiderWar;
      AddQuest(questDrakUnlock);
      AddQuest(questEnKilahUnlock);
      AddQuest(questSapphiron);
      //Misc
      AddQuest(questLichKingArthas);
      AddQuest(questEnemyEncroachment);
    }

    private void RegisterPowers()
    {
      //Powers
      var visionPower = new RegionVisionPower("All-Seeing",
        "Grants permanent vision over Northrend.", new[]
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
      AddPower(visionPower);
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
            new ObjectiveStartSpell(Constants.ABILITY_A00J_SUMMON_THE_BURNING_LEGION_ALL_FACTIONS, false,
              _allLegendSetup.Scourge.Kelthuzad)
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
            new ObjectiveQuestComplete(GetQuestByType<QuestKelthuzadDies>()),
            new ObjectiveQuestNotComplete(GetQuestByType<QuestKelthuzadLich>())
          }));
    }

    private void RegisterHelmOfDominationLogic()
    {
      HelmOfDominationDropsWhenScourgeLeaves.Setup(this, _artifactSetup.HelmOfDomination, _allLegendSetup.Scourge.TheFrozenThrone);
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
            new ObjectiveQuestComplete(GetQuestByType<QuestKelthuzadDies>()),
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
              soundFile: @"Sound\Dialogue\UndeadCampaign\Undead05A\U05AArthas22.flac",
              caption:
              "Citizens of Silvermoon! I have given you ample opportunities to surrender, but you have stubbornly refused! Know that today, your entire race and your ancient heritage will end! Death itself has come to claim the high home of the elves!",
              speaker: "Arthas Menethil"),
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
    
    private void RegisterLordaeronRelatedQuests(Lordaeron lordaeron)
    {
      AddQuest(new QuestDestroyStratholme(lordaeron, _allLegendSetup.Lordaeron.Stratholme, _allLegendSetup.Lordaeron.Arthas));
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
      plagueParameters.PlagueCauldronSummonParameters = new List<PlagueCauldronSummonParameter>
      {
        new(2, Constants.UNIT_UNEC_NECROMANCER_SCOURGE),
        new(2, Constants.UNIT_UACO_ACOLYTE_SCOURGE_WORKER),
        new(5, Constants.UNIT_UGHO_GHOUL_SCOURGE),
        new(2, Constants.UNIT_UCRY_CRYPT_FIEND_SCOURGE),
        new(1, Constants.UNIT_UABO_ABOMINATION_SCOURGE),
      };
      plagueParameters.PlagueCauldronUnitTypeId = Constants.UNIT_H02W_PLAGUE_CAULDRON_SCOURGE_OTHER;
      plagueParameters.Duration = 360;
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