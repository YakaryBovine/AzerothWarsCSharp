using System.Collections.Generic;
using MacroTools;
using MacroTools.DialogueSystem;
using MacroTools.Extensions;
using MacroTools.FactionChoices;
using MacroTools.FactionSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.Powers;
using WarcraftLegacies.Shared.Config.Factions;
using WarcraftLegacies.Source.Quests.Goblin;
using WarcraftLegacies.Source.Setup;
using WCSharp.Shared.Data;

namespace WarcraftLegacies.Source.Factions
{
  public sealed class Bilgewater : Faction
  {
    private readonly AllLegendSetup _allLegendSetup;

    /// <inheritdoc />
    public Bilgewater(PreplacedUnitSystem preplacedUnitSystem, AllLegendSetup allLegendSetup) : base("Bilgewater", PLAYER_COLOR_LIGHT_GRAY, "|cff808080",
      @"ReplaceableTextures\CommandButtons\BTNHeroTinker.blp")
    {
      TraditionalTeam = TeamSetup.Horde;
      _allLegendSetup = allLegendSetup;
      StartingGold = 200;
      ControlPointDefenderUnitTypeId = UNIT_O01C_CONTROL_POINT_DEFENDER_GOBLIN;
      LearningDifficulty = FactionLearningDifficulty.Advanced;
      IntroText = @"You are playing as the industrious |cff808080Bilgewater Cartel|r.

You begin in Tanaris with a very small business venture. Expand onto Kalimdor to grow your trade empire.

Your advanced units require Oil to function. Use oil ships to find oil deposits in the Great Sea and build platforms on them.

The Trading Center in Kezan will unlock the ability to train Traders. Be sure to protect the Trading Center once you unlock it, as it will form the backbone of your Goblin Empire.";
      GoldMines = new List<unit>
      {
        preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(-8615, -12869)), //Starting
        preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(-730, -6777))    //Kezan
      };
      Nicknames = new List<string>
      {
        "bw",
        "bilge",
        "gob",
        "goblin",
        "goblins"
      };
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

    /// <inheritdoc />
    public override void OnNotPicked()
    {
      Regions.KezanUnlock.CleanupNeutralPassiveUnits();
      base.OnNotPicked();
    }
    
    private void RegisterObjectLimits()
    {
      foreach (var (objectTypeId, objectLimit) in BilgewaterObjectLimitData.GetAllObjectLimits())
        ModObjectLimit(FourCC(objectTypeId), objectLimit);
    }

    private void RegisterQuests()
    {
      StartingQuest = AddQuest(new QuestKezan());
      AddQuest(new QuestExplosiveEngineering());
      AddQuest(new QuestRatchet());
      AddQuest(new QuestWesternExpansion(new [] { _allLegendSetup.Sentinels.Auberdine, _allLegendSetup.Sentinels.Feathermoon }));
      AddQuest(new QuestLumberMarket(_allLegendSetup.Druids.Nordrassil));
      AddQuest(new QuestBusinessExpansion());
      AddQuest(new QuestGoblinEmpire());
    }

    private void RegisterDialogue()
    {
      TriggeredDialogueManager.Add(
        new TriggeredDialogue(new Dialogue(
            @"Sound\Dialogue\OrcExpCamp\OrcQuest03x\D03Gazlowe01",
            "Ah, new guy, huh? I'm Gazlowe, chief engineer around these parts. But enough about me. We got work to do, buddy!",
            "Gazlowe")
          , new[]
          {
            this
          }, new[]
          {
            new ObjectiveControlLegend(_allLegendSetup.Goblin.Gazlowe, false)
            {
              EligibleFactions = new List<Faction>{ this }
            }
          }));
    }
    
    private void RegisterPowers()
    {
      var oilPower = new OilPower
      {
        Name = "Oil Tycoon",
        IconName = "OilStation",
        StartingRandomOilPoolCount = 3,
        MaximumOilPoolCount = 15,
        OilPoolMinimumValue = 1500,
        OilPoolMaximumValue = 9000,
        OilPoolBorderDistance = 600,
        ForcedStartingOilPoolSpawnLocations = new [] { new Point(-4825f, -282f) }
      };
      AddPower(oilPower);
    }
  }
}