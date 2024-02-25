using System.Collections.Generic;
using MacroTools;
using MacroTools.DialogueSystem;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.Powers;
using WarcraftLegacies.Source.Powers;
using WarcraftLegacies.Source.Quests.Sentinels;
using WarcraftLegacies.Source.Setup;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Factions
{
  public sealed class Sentinels : Faction
  {
    private readonly AllLegendSetup _allLegendSetup;
    private readonly ArtifactSetup _artifactSetup;

    /// <inheritdoc />
    public Sentinels(PreplacedUnitSystem preplacedUnitSystem, AllLegendSetup allLegendSetup, ArtifactSetup artifactSetup) : base("Sentinels", PLAYER_COLOR_MINT, "|CFFBFFF80",
      @"ReplaceableTextures\CommandButtons\BTNPriestessOfTheMoon.blp")
    {
      _allLegendSetup = allLegendSetup;
      _artifactSetup = artifactSetup;
      UndefeatedResearch = FourCC("R05Y");
      StartingGold = 200;
      StartingLumber = 700;
      CinematicMusic = "Comradeship";
      ControlPointDefenderUnitTypeId = Constants.UNIT_H03F_CONTROL_POINT_DEFENDER_SENTINELS;
      IntroText = @"You are playing as the ever-watchful 

The Druids are slowly waking from their slumber, and it falls to you to drive back the Orcish invaders from Kalimdor until then.

Your first mission is to race down the coast to Feathermoon Stronghold, a powerful Sentinel stronghold on the southern half of the continent. 

Once you have secured your holdings, gather your army and destroy the Orcish Horde. Be careful, they will outnumber you if given time to unite the clans.";
      GoldMines = new List<unit>
      {
        preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(-22721, -13570))
      };
      RegisterFactionDependentInitializer<Druids>(RegisterDruidsDialogue);
      RegisterFactionDependentInitializer<Illidari>(RegisterIlliariDialogue);
      RegisterFactionDependentInitializer<Legion>(RegisterLegionDialogue);
    }

    /// <inheritdoc />
    public override void OnRegistered()
    {
      RegisterObjectLimits();
      RegisterQuests();
      RegisterDialogue();
      RegisterPowers();
      SharedFactionConfigSetup.AddSharedFactionConfig(this);
    }

    private void RegisterObjectLimits()
    {
      ModObjectLimit(FourCC("e00V"), UNLIMITED); //Temple of Elune
      ModObjectLimit(FourCC("e00R"), UNLIMITED); //Altar of Watchers
      ModObjectLimit(FourCC("e00L"), UNLIMITED); //War Academy
      ModObjectLimit(FourCC("edob"), UNLIMITED); //Hunter)s Hall
      ModObjectLimit(FourCC("eden"), UNLIMITED); //Ancient of Wonders
      ModObjectLimit(FourCC("e011"), UNLIMITED); //Night Elf Shipyard
      ModObjectLimit(FourCC("h03N"), UNLIMITED); //Enchanged Runestone
      ModObjectLimit(FourCC("h03M"), UNLIMITED); //Runestone
      ModObjectLimit(FourCC("n06O"), UNLIMITED); //Sentinel Embassy
      ModObjectLimit(FourCC("n06P"), UNLIMITED); //Sentinel Enclave
      ModObjectLimit(FourCC("n06J"), UNLIMITED); //Sentinel Outpost
      ModObjectLimit(FourCC("n06M"), UNLIMITED); //Residence
      ModObjectLimit(FourCC("edos"), UNLIMITED); //Roost
      ModObjectLimit(FourCC("e00T"), UNLIMITED); //Bastion

      ModObjectLimit(FourCC("ewsp"), UNLIMITED); //Wisp
      ModObjectLimit(FourCC("e006"), UNLIMITED); //Priestess
      ModObjectLimit(FourCC("n06C"), UNLIMITED); //Trapper
      ModObjectLimit(FourCC("h04L"), 6); //Priestess of the Moon
      ModObjectLimit(FourCC("earc"), UNLIMITED); //Archer
      ModObjectLimit(FourCC("esen"), UNLIMITED); //Huntress
      ModObjectLimit(FourCC("h08V"), UNLIMITED); //Nightsaber Knight
      ModObjectLimit(FourCC("ebal"), 8); //Glaive Thrower
      ModObjectLimit(FourCC("ehpr"), 6); //Hippogryph Rider
      ModObjectLimit(FourCC("n034"), 12); //Guild Ranger
      ModObjectLimit(FourCC("nwat"), UNLIMITED); //Nightblade
      ModObjectLimit(FourCC("nnmg"), 12); //Redeemed Highborne
      ModObjectLimit(FourCC("e022"), 2); //Moon Rider
      ModObjectLimit(Constants.UNIT_ECHM_CHIMAERA_SENTINELS, 6);
      ModObjectLimit(Constants.UNIT_H045_WARDEN_SENTINELS, 8);

      //Ships
      ModObjectLimit(FourCC("etrs"), UNLIMITED); //Night Elf Transport Ship
      ModObjectLimit(FourCC("h0AU"), UNLIMITED); // Scout
      ModObjectLimit(FourCC("h0AV"), UNLIMITED); // Frigate
      ModObjectLimit(FourCC("h0B1"), UNLIMITED); // Fireship
      ModObjectLimit(FourCC("h057"), UNLIMITED); // Galley
      ModObjectLimit(FourCC("h0B4"), UNLIMITED); // Boarding
      ModObjectLimit(FourCC("h0BA"), UNLIMITED); // Juggernaut
      ModObjectLimit(FourCC("h0B8"), 6); // Bombard

      ModObjectLimit(FourCC("E025"), 1); //Naisha
      ModObjectLimit(FourCC("Etyr"), 1); //Tyrande
      ModObjectLimit(FourCC("E002"), 1); //Shandris
      ModObjectLimit(FourCC("Ewrd"), 1); //Maiev

      ModObjectLimit(FourCC("R00S"), UNLIMITED); //Priestess Adept Training
      ModObjectLimit(FourCC("R064"), UNLIMITED); //Sentinel Fortifications
      ModObjectLimit(FourCC("R01W"), UNLIMITED); //Trapper Adept Training
      ModObjectLimit(FourCC("Reib"), UNLIMITED); //Improved Bows
      ModObjectLimit(FourCC("Reuv"), UNLIMITED); //Ultravision
      ModObjectLimit(FourCC("Remg"), UNLIMITED); //Upgraded Moon Glaive
      ModObjectLimit(FourCC("Roen"), UNLIMITED); //Ensnare
      ModObjectLimit(Constants.UPGRADE_R04E_YSERA_S_GIFT_DRUIDS, UNLIMITED);
      ModObjectLimit(Constants.UPGRADE_R03J_WIND_WALK_SENTINELS, UNLIMITED);
      ModObjectLimit(Constants.UPGRADE_R018_IMPROVED_LIGHTNING_BARRAGE_SENTINELS, UNLIMITED);
    }

    private void RegisterQuests()
    {
      StartingQuest = AddQuest(new QuestFeathermoon(Regions.FeathermoonUnlock));
      AddQuest(new QuestAstranaar(new List<Rectangle> { Regions.AstranaarUnlock, Regions.AuberdineUnlock }));
      AddQuest(new QuestSentinelsKillWarsong(_allLegendSetup.Warsong.Orgrimmar));
      AddQuest(new QuestSentinelsKillFrostwolf(_allLegendSetup.Frostwolf.ThunderBluff));
      AddQuest(new QuestScepterOfTheQueenSentinels(Regions.TheAthenaeum, _artifactSetup.ScepterOfTheQueen, _allLegendSetup.Warsong.StonemaulKeep));
      AddQuest(new QuestVaultoftheWardens(_allLegendSetup.Sentinels.Maiev, _allLegendSetup.Sentinels.VaultOfTheWardens));
      AddQuest(new QuestMaievOutland(Regions.MaievStartUnlock, _allLegendSetup.Sentinels.Maiev, _allLegendSetup.Sentinels.VaultOfTheWardens));
    }

    private void RegisterDialogue()
    {
      TriggeredDialogueManager.Add(new TriggeredDialogue(
        new DialogueSequence(
          new Dialogue(@"Sound\Dialogue\NightElfExpCamp\NightElf02x\S02Maiev02.flac",
            "I suspected as much. These islands must have been formed only recently.",
            "Maiev Shadowsong"),
          new Dialogue(@"Sound\Dialogue\NightElfExpCamp\NightElf02x\S02Naisha03.flac",
            "What makes you say that?",
            "Naisha"),
          new Dialogue(@"Sound\Dialogue\NightElfExpCamp\NightElf02x\S02Maiev04.flac",
            "The ruins all around us, Naisha... I recognize them.",
            "Maiev Shadowsong"),
          new Dialogue(@"Sound\Dialogue\NightElfExpCamp\NightElf02x\S02Maiev05.flac",
            "This was once the great city of Suramar, built before our civilization was blasted beneath the sea ten thousand years ago.",
            "Maiev Shadowsong")
        )
        , new[]
        {
          this
        }, new List<Objective>
        {
          new ObjectiveLegendReachRect(_allLegendSetup.Sentinels.Maiev, Regions.BrokenIslesA, "the Broken Isles")
        }
      ));
      
      TriggeredDialogueManager.Add(
        new TriggeredDialogue(new Dialogue(
          @"Sound\Dialogue\NightElfExpCamp\NightElf05x\S05Maiev24",
          "Priestess Tyrande. I'm surprised you came in person. Are you here to absolve your guilty conscience?",
          "Maiev Shadowsong"), new[]
        {
          this
        }, new[]
        {
          new ObjectiveLegendMeetsLegend(_allLegendSetup.Sentinels.Maiev, _allLegendSetup.Sentinels.Tyrande)
        }));
    }
    
    private void RegisterPowers()
    {
      AddPower(new DummyPower("Unspoiled Wilderness",
        "Your Control Points increase your units' movement speed by 24% in a large radius."));
      
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
    
    private void RegisterDruidsDialogue(Druids druids)
    {
      TriggeredDialogueManager.Add(
        new TriggeredDialogue(new Dialogue(
          @"Sound\Dialogue\NightElfExpCamp\NightElf05x\S05Maiev22",
          "Elune be praised! I knew you would come, Shan'do Stormrage.",
          "Illidan Stormrage"), new Faction[]
        {
          this,
          druids
        }, new[]
        {
          new ObjectiveLegendMeetsLegend(_allLegendSetup.Sentinels.Maiev, _allLegendSetup.Druids.Malfurion)
        }));
    }

    private void RegisterIlliariDialogue(Illidari illidari)
    {
      TriggeredDialogueManager.Add(
        new TriggeredDialogue(new Dialogue(
          @"Sound\Dialogue\NightElfExpCamp\NightElf05x\S05Maiev37",
          "I am the hand of justice, Illidan. Long ago, I swore an oath to keep you chained, and by all the gods, I shall.",
          "Maiev Shadowsong"), new Faction[]
        {
          this,
          illidari
        }, new[]
        {
          new ObjectiveLegendMeetsLegend(_allLegendSetup.Sentinels.Maiev, _allLegendSetup.Naga.Illidan)
        }));
    }

    private void RegisterLegionDialogue(Legion legion)
    {
      TriggeredDialogueManager.Add(new TriggeredDialogue(
        new DialogueSequence(
          new Dialogue(@"Sound\Dialogue\NightElfCampaign\NightElf02\N02Tyrande03.flac",
            "Archimonde... After ten thousand years, how is it possible?",
            "Tyrande Whisperwind"),
          new Dialogue(@"Sound\Dialogue\NightElfCampaign\NightElf02\N02Archimonde04.flac",
            "The Legion has returned to consume this world, woman. And this time, your troublesome race will not stop us.",
            "Archimonde")
        )
        , new Faction[]
        {
          legion,
          this
        }, new List<Objective>
        {
          new ObjectiveLegendMeetsLegend(_allLegendSetup.Sentinels.Tyrande, _allLegendSetup.Legion.Archimonde)
        }
      ));
    }
  }
}