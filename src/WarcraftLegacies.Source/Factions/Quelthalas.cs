using System.Collections.Generic;
using MacroTools;
using MacroTools.DialogueSystem;
using MacroTools.FactionSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.ResearchSystems;
using WarcraftLegacies.Source.Quests.Quelthalas;
using WarcraftLegacies.Source.Researches;
using WarcraftLegacies.Source.Setup;
using WCSharp.Shared.Data;
using static War3Api.Common;

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
      _preplacedUnitSystem = preplacedUnitSystem;
      _allLegendSetup = allLegendSetup;
      UndefeatedResearch = Constants.UPGRADE_R05U_QUEL_THALAS_EXISTS;
      StartingGold = 200;
      StartingLumber = 700;
      CinematicMusic = "BloodElfTheme";
      ControlPointDefenderUnitTypeId = Constants.UNIT_N0BC_CONTROL_POINT_DEFENDER_QUELTHALAS;
      IntroText = @"You are playing as the proud |cff32e1e1Kingdom of Quel'thalas|r.

You begin in Tranquilien, separated from Silvermoon.
The Trolls of Zul'Aman have laid siege to the city, and are preparing attacks on your base.

Train soldiers to repel the attacks, then gather enough strength to besiege Zul'Aman and take the head of Zul'jin.

The Plague of Undeath is coming and Lordaeron will need your help with the Scourge soon. Be ready to join them as once you have secured Silvermoon and dealt with the Amani invasion.";
      GoldMines = new List<unit>
      {
        _preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(17716, 13000))
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
    }

    private void RegisterObjectLimits()
    {
      //Structures
      ModObjectLimit(FourCC("h033"), UNLIMITED); //Steading
      ModObjectLimit(FourCC("h03S"), UNLIMITED); //Mansion
      ModObjectLimit(FourCC("h03T"), UNLIMITED); //Palace
      ModObjectLimit(FourCC("h01H"), UNLIMITED); //Altar of Prowess
      ModObjectLimit(FourCC("h02Y"), UNLIMITED); //Artisan)s Hall
      ModObjectLimit(FourCC("h034"), UNLIMITED); //Arcane Sanctum (Quel)thalas)
      ModObjectLimit(FourCC("h073"), UNLIMITED); //Scout Tower
      ModObjectLimit(FourCC("h074"), UNLIMITED); //Arcane Tower
      ModObjectLimit(FourCC("h075"), UNLIMITED); //Arcane Tower (Improved)
      ModObjectLimit(FourCC("negt"), UNLIMITED); //High Elven Guard Tower
      ModObjectLimit(FourCC("n003"), UNLIMITED); //High Elven Guard Tower (Improved)
      ModObjectLimit(FourCC("h04V"), UNLIMITED); //Arcane Vault (Elven)
      ModObjectLimit(FourCC("nheb"), UNLIMITED); //Cantonment
      ModObjectLimit(FourCC("n0A2"), UNLIMITED); //Consortium
      ModObjectLimit(FourCC("h03J"), UNLIMITED); //Academy
      ModObjectLimit(FourCC("h077"), UNLIMITED); //Alliance Shipyard
      ModObjectLimit(FourCC("nefm"), UNLIMITED); //Residence

      //Units
      ModObjectLimit(FourCC("nbee"), UNLIMITED); //Elven Worker
      ModObjectLimit(FourCC("hhes"), UNLIMITED); //Elven Warrior
      ModObjectLimit(FourCC("hmpr"), UNLIMITED); //Priest
      ModObjectLimit(FourCC("hsor"), UNLIMITED); //Sorceress
      ModObjectLimit(FourCC("hdhw"), 6); //Dragonhawk Rider
      ModObjectLimit(FourCC("nhea"), UNLIMITED); //Archer
      ModObjectLimit(FourCC("e008"), 6); //Elven Ballista
      ModObjectLimit(FourCC("n00A"), 6); //Farstrider
      ModObjectLimit(FourCC("e01B"), 6); //Arcane Annihilator
      ModObjectLimit(FourCC("n02F"), 6); //Warlock
      ModObjectLimit(FourCC("n063"), 12); //Magus
      ModObjectLimit(FourCC("hspt"), UNLIMITED); //Spell Breaker
      ModObjectLimit(FourCC("u00J"), 2); //Arcane Wagon
      ModObjectLimit(Constants.UNIT_N048_BLOOD_MAGE_QUEL_THALAS, 6);

      //Ships
      ModObjectLimit(FourCC("hbot"), UNLIMITED); //Alliance Transport Ship
      ModObjectLimit(FourCC("h0AR"), UNLIMITED); //Alliance Scout
      ModObjectLimit(FourCC("h0AX"), UNLIMITED); //Alliance Frigate
      ModObjectLimit(FourCC("h0B3"), UNLIMITED); //Alliance Fireship
      ModObjectLimit(FourCC("h0B0"), UNLIMITED); //Alliance Galley
      ModObjectLimit(FourCC("h0B6"), UNLIMITED); //Alliance Boarding
      ModObjectLimit(FourCC("h0AN"), UNLIMITED); //Alliance Juggernaut
      ModObjectLimit(FourCC("h0B7"), 6); //Alliance Bombard

      //Demi-heroes
      ModObjectLimit(FourCC("n075"), 1); //Vareesa
      ModObjectLimit(FourCC("Hvwd"), 1); //Sylvanas
      ModObjectLimit(FourCC("H00Q"), 1); //Anasterian
      ModObjectLimit(FourCC("H04F"), 1); //Rommath
      ModObjectLimit(FourCC("H02E"), 1); //Lorthemar

      //Upgrades
      ModObjectLimit(FourCC("R01S"), UNLIMITED); //Aimed Shot
      ModObjectLimit(FourCC("R00G"), UNLIMITED); //Feint
      ModObjectLimit(FourCC("R01R"), UNLIMITED); //Improved Bows
      ModObjectLimit(FourCC("R029"), UNLIMITED); //Magus Adept Training
      ModObjectLimit(FourCC("Rhcd"), UNLIMITED); //Cloud
      ModObjectLimit(FourCC("Rhss"), UNLIMITED); //Control Magic
      ModObjectLimit(FourCC("Rhlh"), UNLIMITED); //Improved Lumber Harvesting
      ModObjectLimit(FourCC("Rhac"), UNLIMITED); //Improved Masonry
      ModObjectLimit(FourCC("Rhse"), UNLIMITED); //Magic Sentry
      ModObjectLimit(FourCC("Rhpt"), UNLIMITED); //Priest Adept Training
      ModObjectLimit(FourCC("Rhst"), UNLIMITED); //Sorceress Adept Training
      ModObjectLimit(FourCC("R004"), UNLIMITED); //Sunfury Warrior Training
      ModObjectLimit(FourCC("R02Y"), UNLIMITED); //Improved Glaives

      ModAbilityAvailability(Constants.ABILITY_A0K5_DWARVEN_MASONRY_CASTLES_YELLOW, -1);
      ModAbilityAvailability(Constants.ABILITY_A0OC_EXTRACT_VIAL_ALL, -1);
    }

    private void RegisterQuests()
    {
      var newQuest = AddQuest(new QuestSilvermoon(Regions.SunwellAmbient,
        _preplacedUnitSystem.GetUnit(Constants.UNIT_H00D_ELVEN_RUNESTONE_QUEL_THALAS_OTHER, new Point(20477, 17447)),
        _preplacedUnitSystem, _allLegendSetup.Quelthalas.Silvermoon, _allLegendSetup.Quelthalas.Sunwell));
      StartingQuest = newQuest;
      AddQuest(new QuestUnlockSpire(Regions.WindrunnerSpireUnlock, _allLegendSetup.Quelthalas.Sylvanas));
      AddQuest(new QuestTheBloodElves(_allLegendSetup.Neutral.DraktharonKeep));
      AddQuest(new QuestQueldanil(Regions.QuelDanil_Lodge, Regions.BloodElfSecondChanceSpawn, 
        _allLegendSetup.Quelthalas.Sunwell, _allLegendSetup.Quelthalas.Rommath));
      AddQuest(new QuestQueensArchive(_allLegendSetup.Quelthalas.Rommath));
      AddQuest(new QuestForgottenKnowledge());
    }
    
    private void RegisterResearches()
    {
      ResearchManager.Register(new SunfuryWarrior(Constants.UPGRADE_R004_SUNFURY_TRAINING_QUEL_THALAS, 300, 300));
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