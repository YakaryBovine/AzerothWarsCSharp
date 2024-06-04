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
using WarcraftLegacies.Shared.Config.Factions;
using WarcraftLegacies.Source.Quests.Naga;
using WarcraftLegacies.Source.Setup;

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
      TraditionalTeam = TeamSetup.Outland;
      _allLegendSetup = allLegendSetup;
      _artifactSetup = artifactSetup;
      UndefeatedResearch = UPGRADE_R02L_ILLIDAN_EXISTS;
      StartingGold = 200;
      FoodMaximum = 250;
      StartingCameraPosition = Regions.IllidanStartingPosition.Center;
      StartingUnits = Regions.IllidanStartingPosition.PrepareUnitsForRescue(RescuePreparationMode.Invulnerable);
      ControlPointDefenderUnitTypeId = UNIT_N0BB_CONTROL_POINT_DEFENDER_ILLIDARI_TOWER;
      LearningDifficulty = FactionLearningDifficulty.Basic;
      IntroText = @"You are playing as the Betrayer, Illidan|r|r.

You begin on the Broken Isles, ready to plunder the tombs for artifacts to empower Illidan.

Unfortunately, you cannot progress further in the Islands. Use Illidan's mastery of portals to travel to Outland and join forces with your ally.

Support your ally in Outland by unlocking bases and coordinating with his push out of the Dark Portal.";
      Nicknames = new List<string>
      {
        "illi",
        "illidan",
        "ill",
        "naga",
        "nagas",
        "fish"
      };
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
      Regions.TelredorUnlock.CleanupNeutralPassiveUnits(NeutralPassiveCleanupType.TurnUnitsHostile);
      Regions.IllidariUnlockSA.CleanupNeutralPassiveUnits(NeutralPassiveCleanupType.TurnUnitsHostile);
      Regions.AkamaUnlock.CleanupNeutralPassiveUnits(NeutralPassiveCleanupType.TurnUnitsHostile);
      base.OnNotPicked();
    }

    private void RegisterObjectLimits()
    {
      foreach (var (objectTypeId, objectLimit) in IllidariObjectLimitData.GetAllObjectLimits())
        ModObjectLimit(FourCC(objectTypeId), objectLimit);
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
          {
            EligibleFactions = new List<Faction>
            {
              this
            }
          }
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

      TriggeredDialogueManager.Add(new TriggeredDialogue(
        new DialogueSequence(
          new Dialogue(@"Sound\Dialogue\NightElfCampaign\NightElf06\N06Illidan16.flac",
            "Yes... the power should be mine!",
            "Illidan Stormrage"),
          new Dialogue(@"Sound\Dialogue\NightElfCampaign\NightElf06\N06Illidan17.flac",
            "Now I am complete!",
            "Illidan Stormrage"))
        , new[]
        {
          this
        }, new List<Objective>
        {
          new ObjectiveQuestComplete(GetQuestByType<QuestFlameAndSorrow>())
          {
            EligibleFactions = new List<Faction> { this }
          }
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