using System.Collections.Generic;
using MacroTools.DialogueSystem;
using MacroTools.Extensions;
using MacroTools.FactionChoices;
using MacroTools.FactionSystem;
using MacroTools.ObjectiveSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.ObjectiveSystem.Objectives.MetaBased;
using MacroTools.ObjectiveSystem.Objectives.QuestBased;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using WarcraftLegacies.Source.Quests.Naga;
using WarcraftLegacies.Source.Setup;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Factions
{
  public sealed class Illidari : Faction
  {
    private readonly AllLegendSetup _allLegendSetup;
    private readonly ArtifactSetup _artifactSetup;

    /// <inheritdoc />
    public Illidari(AllLegendSetup allLegendSetup, ArtifactSetup artifactSetup) : base("Illidan", PLAYER_COLOR_VIOLET,
      "|cffff00ff", @"ReplaceableTextures\CommandButtons\BTNEvilIllidan.blp")
    {
      _allLegendSetup = allLegendSetup;
      _artifactSetup = artifactSetup;
      UndefeatedResearch = Constants.UPGRADE_R02L_ILLIDAN_EXISTS;
      StartingGold = 200;
      StartingLumber = 700;
      FoodMaximum = 250;
      StartingCameraPosition = Regions.IllidanStartingPosition.Center;
      StartingUnits = Regions.IllidanStartingPosition.PrepareUnitsForRescue(RescuePreparationMode.Invulnerable);
      ControlPointDefenderUnitTypeId = Constants.UNIT_N0BB_CONTROL_POINT_DEFENDER_ILLIDARI_TOWER;
      LearningDifficulty = FactionLearningDifficulty.Basic;
      IntroText = @"You are playing as the Betrayer, Illidan|r|r.

You begin on the Broken Isles, ready to plunder the tombs for artifacts to empower Illidan.

Unfortunately, you cannot progress further in the Islands. Use Illidan's mastery of portals to travel to Outland and join forces with your ally.

Support your ally in Outland by unlocking bases and coordinating with his push out of the Dark Portal.";
      RegisterFactionDependentInitializer<Sentinels>(RegisterSentinelsDialogue);
      RegisterFactionDependentInitializer<Druids>(RegisterDruidsDialogue);
      RegisterFactionDependentInitializer<Scourge>(RegisterScourgeDialogue);
      RegisterFactionDependentInitializer<Sentinels, Druids>(RegisterSentinelsDruidsDialogue);
    }

    /// <inheritdoc />
    public override void OnRegistered()
    {
      RegisterObjectLimits();
      RegisterQuests();
      RegisterDialogue();
      SharedFactionConfigSetup.AddSharedFactionConfig(this);
    }

    /// <inheritdoc />
    public override void OnNotPicked()
    {
      Regions.IllidanStartingPosition.CleanupNeutralPassiveUnits();
      Regions.IllidanBlackTempleUnlock.CleanupNeutralPassiveUnits();
      base.OnNotPicked();
    }

    private void RegisterObjectLimits()
    {
      ModObjectLimit(FourCC("nntt"), UNLIMITED); //Pillar of Waves
      ModObjectLimit(FourCC("n04T"), UNLIMITED); //Monument of Currents
      ModObjectLimit(FourCC("n055"), UNLIMITED); //Temple of Tides
      ModObjectLimit(FourCC("nnad"), UNLIMITED); //Altar of the Depths
      ModObjectLimit(FourCC("nnsg"), UNLIMITED); //Spawning Grounds
      ModObjectLimit(FourCC("h06S"), UNLIMITED); //Coral Forge
      ModObjectLimit(FourCC("n0A3"), UNLIMITED); //Royal Pyramid
      ModObjectLimit(FourCC("nnsa"), UNLIMITED); //Temple of Azshara
      ModObjectLimit(FourCC("nnfm"), UNLIMITED); //Coral Beds
      ModObjectLimit(FourCC("nntg"), UNLIMITED); //Tidal Guardian
      ModObjectLimit(FourCC("n005"), UNLIMITED); //Improved Tidal Guardian
      ModObjectLimit(FourCC("nmrb"), UNLIMITED); //Deep Sea Vault
      ModObjectLimit(FourCC("n08W"), UNLIMITED); //Deep Sea Vault
      ModObjectLimit(FourCC("e020"), UNLIMITED); //Shipyard

      ModObjectLimit(FourCC("nmpe"), UNLIMITED); //Murgul Slave
      ModObjectLimit(FourCC("nmyr"), UNLIMITED); //Myrmidon
      ModObjectLimit(FourCC("nsnp"), UNLIMITED); //Snap Dragon
      ModObjectLimit(FourCC("nnsw"), UNLIMITED); //Siren
      ModObjectLimit(FourCC("nmsc"), UNLIMITED); //Shadowcaster
      ModObjectLimit(FourCC("nnsu"), 6); //Summoner
      ModObjectLimit(FourCC("nnrg"), 6); //Royal Guard
      ModObjectLimit(FourCC("nhyc"), 8); //Dragon Turtle
      ModObjectLimit(FourCC("nwgs"), 8); //Couatl
      ModObjectLimit(FourCC("e00Y"), 4); //Scylla
      ModObjectLimit(FourCC("h0AC"), 6); //Sea Witch
      ModObjectLimit(FourCC("ndrn"), UNLIMITED); //AshtongueMelee
      ModObjectLimit(FourCC("ndrs"), 6); //Ashtonguecaster

      //Ships
      ModObjectLimit(FourCC("etrs"), UNLIMITED); //Night Elf Transport Ship
      ModObjectLimit(FourCC("h0AU"), UNLIMITED); // Scout
      ModObjectLimit(FourCC("h0AV"), UNLIMITED); // Frigate
      ModObjectLimit(FourCC("h0B1"), UNLIMITED); // Fireship
      ModObjectLimit(FourCC("h057"), UNLIMITED); // Galley
      ModObjectLimit(FourCC("h0B4"), UNLIMITED); // Boarding
      ModObjectLimit(FourCC("h0BA"), UNLIMITED); // Juggernaut
      ModObjectLimit(FourCC("h0B8"), 6); // Bombard

      ModObjectLimit(FourCC("Hvsh"), 1); //Vashj
      ModObjectLimit(FourCC("U00S"), 1); //Najentus
      ModObjectLimit(FourCC("Naka"), 1); //Akama
      ModObjectLimit(FourCC("Eevi"), 1); //Illidan

      ModObjectLimit(Constants.UPGRADE_RNSW_NAGA_SIREN_ADEPT_TRAINING_NAGA_SIREN_MASTER_TRAINING, UNLIMITED);
      ModObjectLimit(Constants.UPGRADE_R02V_SHADOWCASTER_MASTER_TRAINING, UNLIMITED);
    }

    private void RegisterQuests()
    {
      StartingQuest = AddQuest(new QuestLostOnes(Regions.AkamaUnlock));
      AddQuest(new QuestBlackTemple(Regions.IllidanBlackTempleUnlock, _allLegendSetup.Naga.Illidan));
      AddQuest(new QuestEyeofSargeras(_artifactSetup.EyeOfSargeras, _allLegendSetup.Naga.Illidan));
      AddQuest(new QuestFlameAndSorrow(_artifactSetup.SkullOfGuldan, _allLegendSetup.Naga.Illidan));
      AddQuest(new QuestZangarmarsh(Regions.TelredorUnlock, _allLegendSetup.Naga.Vashj));
      AddQuest(new QuestStranglethornOutpost(Regions.IllidariUnlockSA, _allLegendSetup.Naga.Vashj));
      AddQuest(new QuestNajentus(new[]
      {
        _allLegendSetup.Stormwind.StormwindKeep,
        _allLegendSetup.Ironforge.GreatForge
      }));
      AddQuest(new QuestRegroupCastaway());
      AddQuest(new QuestBlackrookHold(_allLegendSetup.Sentinels.BlackrookHold));
    }

    private void RegisterDialogue()
    {
      TriggeredDialogueManager.Add(new TriggeredDialogue(
        new Dialogue(@"Sound\Dialogue\HumanExpCamp\Human07x\A07Illidan24",
          "Hear me now, you trembling mortals! I am your lord and master! Illidan reigns supreme!",
          "Illidan Stormrage")
        , new[]
        {
          this
        }, new List<Objective>
        {
          new ObjectiveQuestComplete(GetQuestByType<QuestBlackTemple>())
        }
      ));

      TriggeredDialogueManager.Add(new TriggeredDialogue(
        new Dialogue(@"Sound\Dialogue\NightElfExpCamp\NightElf02x\S02Illidan45.flac",
          "At last! The Tomb of Sargeras is found!",
          "Illidan Stormrage")
        , new[]
        {
          this
        }, new List<Objective>
        {
          new ObjectiveLegendReachRect(_allLegendSetup.Naga.Illidan, Regions.Sargeras_Entrance,
            "the Tomb of Sargeras' entrance")
        }
      ));
    }

    private void RegisterSentinelsDialogue(Sentinels sentinels)
    {
      TriggeredDialogueManager.Add(
        new TriggeredDialogue(new DialogueSequence(new Dialogue(
              @"Sound\Dialogue\NightElfExpCamp\NightElf03x\S03LadyVashj31",
              "You've come far enough, little warden. Your vaunted night elf justice has no jurisdiction here.",
              "Lady Vashj"),
            new Dialogue(
              @"Sound\Dialogue\NightElfExpCamp\NightElf03x\S03Maiev32",
              "What would you know of us or our justice, naga witch?",
              "Maiev Shadowsong")),
          new Faction[]
          {
            this,
            sentinels
          }, new[]
          {
            new ObjectiveLegendMeetsLegend(_allLegendSetup.Naga.Vashj, _allLegendSetup.Sentinels.Maiev)
          }));

      TriggeredDialogueManager.Add(
        new TriggeredDialogue(new DialogueSequence(new Dialogue(
              @"Sound\Dialogue\NightElfExpCamp\NightElf03x\S03Illidan45",
              "So, Warden Shadowsong, you've made it at last. I knew you would.",
              "Illidan Stormrage"),
            new Dialogue(
              @"Sound\Dialogue\NightElfExpCamp\NightElf03x\S03Maiev46",
              "You have much to pay for, Illidan. I'm taking you back to your cell.",
              "Maiev Shadowsong")),
          new Faction[]
          {
            this,
            sentinels
          }, new[]
          {
            new ObjectiveLegendMeetsLegend(_allLegendSetup.Naga.Illidan, _allLegendSetup.Sentinels.Maiev)
          }));

      TriggeredDialogueManager.Add(
        new TriggeredDialogue(new DialogueSequence(new Dialogue(
            @"Sound\Dialogue\NightElfExpCamp\NightElf05x\S05Illidan31",
            "Tyrande! What are you doing here? This battle does not concern you.",
            "Illidan Stormrage"), new Dialogue(
            @"Sound\Dialogue\NightElfExpCamp\NightElf05x\S05Tyrande32",
            "I was wrong to set you free, Illidan. I can see that now. You've become a monster.",
            "Tyrande Whisperwind"), new Dialogue(
            @"Sound\Dialogue\NightElfExpCamp\NightElf05x\S05Illidan33",
            "Monster? Is that what you think of me? I have always...cared about you, Tyrande. I sought only to prove my worthiness--my power!",
            "Illidan Stormrage"), new Dialogue(
            @"Sound\Dialogue\NightElfExpCamp\NightElf05x\S05Tyrande34",
            "Raw power is no substitute for true strength, Illidan. That is why I chose your brother over you.",
            "Tyrande Whisperwind"), new Dialogue(
            @"Sound\Dialogue\NightElfExpCamp\NightElf05x\S05Illidan35",
            "Still you refuse to see me for what I truly am. You believe me to be a villain--your enemy. But soon now, you will see our enemies are the same!",
            "Illidan Stormrage"))
          , new Faction[]
          {
            this,
            sentinels
          }, new[]
          {
            new ObjectiveLegendMeetsLegend(_allLegendSetup.Naga.Illidan, _allLegendSetup.Sentinels.Tyrande)
          }));
    }

    private void RegisterDruidsDialogue(Druids druids)
    {
      TriggeredDialogueManager.Add(
        new TriggeredDialogue(
          new DialogueSequence(new Dialogue(
              @"Sound\Dialogue\NightElfExpCamp\NightElf05x\S05Illidan38",
              "Brother? What are you doing here?",
              "Illidan Stormrage"),
            new Dialogue(
              @"Sound\Dialogue\NightElfExpCamp\NightElf05x\S05Furion39",
              "I've come to stop you, Illidan. Instead of banishing you, I should have returned you to your cage when I had the chance! I was weak then--but no longer.",
              "Malfurion Stormrage")),
          new Faction[]
          {
            this,
            druids
          }, new[]
          {
            new ObjectiveLegendMeetsLegend(_allLegendSetup.Naga.Illidan, _allLegendSetup.Druids.Malfurion)
          }));
    }

    private void RegisterScourgeDialogue(Scourge scourge)
    {
      TriggeredDialogueManager.Add(new TriggeredDialogue(
        new Dialogue(@"Sound\Dialogue\UndeadExpCamp\Undead08x\L08Illidan33.flac",
          "You're out of your league, old king. You should have stayed hidden underground.",
          "Illidan Stormrage")
        , new Faction[]
        {
          this,
          scourge
        }, new List<Objective>
        {
          new ObjectiveLegendMeetsLegend(_allLegendSetup.Naga.Illidan, _allLegendSetup.Scourge.Anubarak)
        }
      ));

      TriggeredDialogueManager.Add(
        new TriggeredDialogue(new DialogueSequence(new Dialogue(
              @"Sound\Dialogue\UndeadExpCamp\Undead08x\L08Illidan27",
              "Hello, Arthas.",
              "Illidan Stormrage"),
            new Dialogue(
              @"Sound\Dialogue\UndeadExpCamp\Undead08x\L08Arthas28",
              "You look different, Illidan. I guess the Skull of Gul'dan didn't agree with you.",
              "Arthas Menethil")),
          new Faction[]
          {
            this,
            scourge
          }, new[]
          {
            new ObjectiveLegendMeetsLegend(_allLegendSetup.Naga.Illidan, _allLegendSetup.Scourge.Arthas)
          }));
    }

    private void RegisterSentinelsDruidsDialogue(Sentinels sentinels, Druids druids)
    {
      TriggeredDialogueManager.Add(
        new TriggeredDialogue(new DialogueSequence(new Dialogue(
              @"Sound\Dialogue\NightElfExpCamp\NightElf05x\S05Tyrande06",
              "What are these vile serpents?",
              "Tyrande Whisperwind"),
            new Dialogue(
              @"Sound\Dialogue\NightElfExpCamp\NightElf05x\S05Furion07",
              "I don't know, but these creatures feel familiar somehow.",
              "Malfurion Stormrage"),
            new Dialogue(
              @"Sound\Dialogue\NightElfExpCamp\NightElf05x\S05Naga08",
              "Wretched Night Elves. We are the Naga! We are the future!",
              "Myrmidon")),
          new Faction[]
          {
            this,
            druids
          }, new[]
          {
            new ObjectiveEitherOf(
              new ObjectiveDamagePlayer(sentinels.Player)
              {
                EligibleFactions = new List<Faction>
                {
                  this
                }
              }, new ObjectiveDamagePlayer(druids.Player)
              {
                EligibleFactions = new List<Faction>
                {
                  this
                }
              })
          }));
    }
  }
}