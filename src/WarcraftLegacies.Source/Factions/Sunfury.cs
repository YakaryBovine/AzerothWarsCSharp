using System.Collections.Generic;
using MacroTools;
using MacroTools.Extensions;
using MacroTools.FactionChoices;
using MacroTools.FactionSystem;
using WarcraftLegacies.Source.Quests.Quelthalas;
using WarcraftLegacies.Source.Quests.Sunfury;
using WarcraftLegacies.Source.Setup;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Factions
{
  public sealed class Sunfury : Faction
  {
    private readonly PreplacedUnitSystem _preplacedUnitSystem;
    private readonly AllLegendSetup _allLegendSetup;
    private readonly ArtifactSetup _artifactSetup;

    /// <inheritdoc />
    public Sunfury(PreplacedUnitSystem preplacedUnitSystem, AllLegendSetup allLegendSetup, ArtifactSetup artifactSetup)
      : base("Sunfury", PLAYER_COLOR_MAROON, "|cffff0000", @"ReplaceableTextures\CommandButtons\BTNBloodMage2.blp")
    {
      _preplacedUnitSystem = preplacedUnitSystem;
      _allLegendSetup = allLegendSetup;
      _artifactSetup = artifactSetup;
      StartingGold = 200;
      StartingLumber = 700;
      CinematicMusic = "BloodElfTheme";
      FoodMaximum = 250;
      StartingCameraPosition = Regions.SunfuryStartingPosition.Center;
      StartingUnits = Regions.SunfuryStartingPosition.PrepareUnitsForRescue(RescuePreparationMode.Invulnerable);
      ControlPointDefenderUnitTypeId = Constants.UNIT_N0BC_CONTROL_POINT_DEFENDER_QUELTHALAS;
      LearningDifficulty = FactionLearningDifficulty.Advanced;
      IntroText = @"You are playing as the power-hungry |cffff0000Sunfury|r.

You begin in Netherstorm, your first mission is to build three biodomes in the green areas protected by a bubble.
Unite with your fel ally to push through the Dark Portal and destroy Stormwind. 

Your main goal is to summon Kil'jaeden and destroy your enemies.";
      GoldMines = new List<unit>
      {
        _preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(3295, -22670)),
        _preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(2529, -19141))
      };
    }
        
    /// <inheritdoc />
    public override void OnRegistered()
    {
      RegisterObjectLimits();
      RegisterQuests();
      SharedFactionConfigSetup.AddSharedFactionConfig(this);
    }
    
    /// <inheritdoc />
    public override void OnNotPicked()
    {
      Regions.Area52Unlock.CleanupNeutralPassiveUnits();
      Regions.Netherstorm.CleanupNeutralPassiveUnits();
      Regions.UpperNetherstorm.CleanupNeutralPassiveUnits();
      Regions.TempestKeep.CleanupNeutralPassiveUnits();
      _preplacedUnitSystem.GetUnit(Constants.UNIT_N0DZ_THE_WELL_OF_ETERNITY_SUNFURY_OTHER).Remove();
      base.OnNotPicked();
    }

    private void RegisterObjectLimits()
    {
      //Structures
      ModObjectLimit(FourCC("h02P"), UNLIMITED); //t1
      ModObjectLimit(FourCC("h0C4"), UNLIMITED); //t2
      ModObjectLimit(FourCC("h0C5"), UNLIMITED); //t3
      ModObjectLimit(FourCC("h0C7"), 3); //house
      ModObjectLimit(FourCC("h0C8"), UNLIMITED); //forge
      ModObjectLimit(FourCC("h0C9"), UNLIMITED); //barrack
      ModObjectLimit(FourCC("h0CB"), UNLIMITED); //magic
      ModObjectLimit(FourCC("h0CA"), UNLIMITED); //VOid Well
      ModObjectLimit(FourCC("h0CI"), UNLIMITED); //Tempest-Forge
      ModObjectLimit(FourCC("h0C6"), UNLIMITED); //Altar
      ModObjectLimit(FourCC("h0CC"), UNLIMITED); //Vault
      ModObjectLimit(FourCC("h0CD"), UNLIMITED); //Scout tower
      ModObjectLimit(FourCC("n0E0"), UNLIMITED); //Skyfury tower
      ModObjectLimit(FourCC("n0E1"), UNLIMITED); //improved skyfury tower
      ModObjectLimit(FourCC("N0DZ"), 1); //Fountain
      ModObjectLimit(FourCC("tp04"), UNLIMITED); //Alliance Shipyard

      //Units
      ModObjectLimit(FourCC("n0E2"), UNLIMITED); //worker
      ModObjectLimit(FourCC("n09S"), UNLIMITED); //Elven Warrior
      ModObjectLimit(FourCC("h0CF"), UNLIMITED); //Elven Ranger
      ModObjectLimit(FourCC("u02W"), 2); //Energy Wagon
      ModObjectLimit(FourCC("h0CH"), UNLIMITED); //Astromancer
      ModObjectLimit(FourCC("h0CG"), UNLIMITED); //Flamekeeper
      ModObjectLimit(Constants.UNIT_H0CE_BLOOD_KNIGHT_SQUIRE_SUNFURY, 12);
      ModObjectLimit(FourCC("n0E3"), 6); //Warlock
      ModObjectLimit(FourCC("n0E4"), 6); //Elven Ballista
      ModObjectLimit(FourCC("n0E8"), 3); //Skyship
      ModObjectLimit(FourCC("n0E7"), 6); //Bloodwarder
      ModObjectLimit(FourCC("e01B"), 6); //Arcane Annihilator
      ModObjectLimit(FourCC("n006"), 2); //Ancient of the Arcane

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
      ModObjectLimit(FourCC("H098"), 1); //Pathaleon
      ModObjectLimit(FourCC("U02V"), 1); //Solarian
      ModObjectLimit(FourCC("Hkal"), 1); //Kael
      ModObjectLimit(FourCC("U004"), 1); //Kil
      ModObjectLimit(Constants.UNIT_N0E5_VOID_REAVER_SUNFURY_DEMI, 1);

      //Upgrades
      ModObjectLimit(Constants.UPGRADE_R09H_ASTROMANCER_MASTER_TRAINING_SUNFURY, UNLIMITED);
      ModObjectLimit(Constants.UPGRADE_R09G_FLAMEKEEPER_MASTER_TRAINING_SUNFURY, UNLIMITED);
      ModObjectLimit(Constants.UPGRADE_R09U_SEAL_OF_BLOOD_SUNFURY, UNLIMITED);
    }

    private void RegisterQuests()
    {
      var newQuest = AddQuest(new QuestTempestKeep(Regions.TempestKeep, Regions.Biodome1, Regions.Biodome2, Regions.Biodome3));
      StartingQuest = newQuest;

      AddQuest(new QuestArea52(Regions.Area52Unlock));
      AddQuest(new QuestUpperNetherstorm(Regions.UpperNetherstorm));
      AddQuest(new QuestSolarian(_artifactSetup.EssenceofMurmur));
      AddQuest(new QuestSummonKil(_allLegendSetup.Stormwind.StormwindKeep, _allLegendSetup.Neutral.Karazhan, _allLegendSetup.Quelthalas.Kael));
      AddQuest(new QuestForgottenKnowledge());
      AddQuest(new QuestWellOfEternity(_preplacedUnitSystem, _allLegendSetup.Quelthalas.Kiljaeden));
    }
  }
}