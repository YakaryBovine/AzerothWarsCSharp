using System.Collections.Generic;
using MacroTools;
using MacroTools.DialogueSystem;
using MacroTools.FactionSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.ResearchSystems;
using WarcraftLegacies.Shared.FactionObjectLimits;
using WarcraftLegacies.Source.Quests.Quelthalas;
using WarcraftLegacies.Source.Researches;
using WarcraftLegacies.Source.Setup;
using WCSharp.Shared.Data;
using WarcraftLegacies.Source.Powers;
using MacroTools.LegendSystem;

namespace WarcraftLegacies.Source.Factions
{
  public sealed class Quelthalas : Faction
  {
    private readonly PreplacedUnitSystem _preplacedUnitSystem;
    private readonly AllLegendSetup _allLegendSetup;

    /// <inheritdoc />
    public Quelthalas(PreplacedUnitSystem preplacedUnitSystem, AllLegendSetup allLegendSetup) : base("Quel'thalas", PLAYER_COLOR_CYAN, "|C0000FFFF",
      @"ReplaceableTextures\CommandButtons\BTNSylvanusWindrunner.blp")
    {
      TraditionalTeam = TeamSetup.NorthAlliance;
      _preplacedUnitSystem = preplacedUnitSystem;
      _allLegendSetup = allLegendSetup;
      UndefeatedResearch = UPGRADE_R05U_QUEL_THALAS_EXISTS;
      StartingGold = 200;
      CinematicMusic = "BloodElfTheme";
      ControlPointDefenderUnitTypeId = UNIT_N0BC_CONTROL_POINT_DEFENDER_QUELTHALAS;
      IntroText = @"You are playing as the proud |cff32e1e1Kingdom of Quel'thalas|r.

You begin in Tranquilien, separated from Silvermoon.
The Trolls of Zul'Aman have laid siege to the city, and are preparing attacks on your base.

Train soldiers to repel the attacks, then gather enough strength to besiege Zul'Aman and take the head of Zul'jin.

The Plague of Undeath is coming and Lordaeron will need your help with the Scourge soon. Be ready to join them as once you have secured Silvermoon and dealt with the Amani invasion.";
      GoldMines = new List<unit>
      {
        _preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(17716, 13000))
      };
      Nicknames = new List<string>
      {
        "qt",
        "quel",
        "quelthalas"
      };

      RegisterFactionDependentInitializer<Scourge>(RegisterScourgeDialogue);
    }

    /// <inheritdoc />
    public override void OnRegistered()
    {
      RegisterObjectLimits();
      RegisterQuests();
      RegisterResearches();
      SharedFactionConfigSetup.AddSharedFactionConfig(this);
      RegisterPowers();
    }
    private void RegisterPowers()
    {
      var fontsOfPower = new List<Capital>
      {
        _allLegendSetup.Quelthalas.Sunwell,
        _allLegendSetup.FelHorde.BlackTemple,
        _allLegendSetup.Druids.Nordrassil,
      };

      AddPower(new FontOfPower(fontsOfPower)
      {
        IconName = "PriestAdept",
        Name = "Font of Power",
        ResearchId = UPGRADE_ZBFO_FONT_OF_POWER_IS_ACTIVE_POWER
      });
    }
    
    private void RegisterObjectLimits()
    {
      foreach (var (objectTypeId, objectLimit) in QuelthalasObjectLimitData.GetAllObjectLimits())
        ModObjectLimit(FourCC(objectTypeId), objectLimit.Limit);

      ModAbilityAvailability(ABILITY_A0K5_DWARVEN_MASONRY_CASTLES_YELLOW, -1);
      ModAbilityAvailability(ABILITY_A0OC_EXTRACT_VIAL_ALL, -1);
    }

    private void RegisterQuests()
    {
      var newQuest = AddQuest(new QuestSilvermoon(Regions.SunwellAmbient,
        _preplacedUnitSystem.GetUnit(UNIT_H00D_ELVEN_RUNESTONE_QUEL_THALAS_OTHER, new Point(20477, 17447)), _allLegendSetup.Quelthalas.Silvermoon, _allLegendSetup.Quelthalas.Sunwell));
      StartingQuest = newQuest;
      AddQuest(new QuestUnlockSpire(Regions.WindrunnerSpireUnlock, _allLegendSetup.Quelthalas.Sylvanas));
      AddQuest(new QuestTheBloodElves(_allLegendSetup.Neutral.DraktharonKeep));
      AddQuest(new QuestQueldanil(Regions.QuelDanil_Lodge));
      AddQuest(new QuestQueensArchive(_allLegendSetup.Quelthalas.Rommath));
      AddQuest(new QuestForgottenKnowledge());
    }
    
    private void RegisterResearches()
    {
      ResearchManager.Register(new SunfuryWarrior(UPGRADE_R004_SUNFURY_TRAINING_QUEL_THALAS, 300));
    }
    
    private void RegisterScourgeDialogue(Scourge scourge)
    {
      TriggeredDialogueManager.Add(
        new TriggeredDialogue(new DialogueSequence(new Dialogue(
            @"Sound\Dialogue\UndeadExpCamp\Undead08x\L08Arthas30",
            "Are you still upset that I stole Jaina from you, Kael?",
            "Illidan Stormrage"), new Dialogue(
            @"Sound\Dialogue\UndeadExpCamp\Undead08x\L08Kael31",
            "You've taken everything I ever cared for, Arthas. Vengeance is all I have left.",
            "Kael'thas Sunstrider"))
          , new Faction[]
          {
            this,
            scourge
          }, new[]
          {
            new ObjectiveLegendMeetsLegend(_allLegendSetup.Scourge.Arthas, _allLegendSetup.Quelthalas.Kael)
          }));
    }
  }
}