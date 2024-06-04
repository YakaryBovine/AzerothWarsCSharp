using System.Collections.Generic;
using MacroTools;
using MacroTools.Extensions;
using MacroTools.FactionChoices;
using MacroTools.FactionSystem;
using WarcraftLegacies.Shared.Config.Factions;
using WarcraftLegacies.Source.Quests.Quelthalas;
using WarcraftLegacies.Source.Quests.Sunfury;
using WarcraftLegacies.Source.Setup;
using WCSharp.Shared.Data;

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
      TraditionalTeam = TeamSetup.Outland;
      _preplacedUnitSystem = preplacedUnitSystem;
      _allLegendSetup = allLegendSetup;
      _artifactSetup = artifactSetup;
      StartingGold = 200;
      CinematicMusic = "BloodElfTheme";
      FoodMaximum = 250;
      StartingCameraPosition = Regions.SunfuryStartingPosition.Center;
      StartingUnits = Regions.SunfuryStartingPosition.PrepareUnitsForRescue(RescuePreparationMode.Invulnerable);
      ControlPointDefenderUnitTypeId = UNIT_N0BC_CONTROL_POINT_DEFENDER_QUELTHALAS;
      LearningDifficulty = FactionLearningDifficulty.Advanced;
      IntroText = @"You are playing as the power-hungry |cffff0000Sunfury|r.

You begin in Netherstorm, your first mission is to build three biodomes in the green areas protected by a bubble.
Unite with your fel ally to push through the Dark Portal and destroy Stormwind. 

Your main goal is to summon Kil'jaeden and destroy your enemies.";
      GoldMines = new List<unit>
      {
        _preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(3295, -22670)),
        _preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(2529, -19141)),
        _preplacedUnitSystem.GetUnit(FourCC("ngol"), Regions.Area52Unlock.Center)
      };
      Nicknames = new List<string>
      {
        "sf",
        "sun"
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
      _preplacedUnitSystem.GetUnit(UNIT_N0DZ_THE_WELL_OF_ETERNITY_SUNFURY_OTHER).Remove();
      base.OnNotPicked();
    }

    private void RegisterObjectLimits()
    {
      foreach (var (objectTypeId, objectLimit) in SunfuryObjectLimitData.GetAllObjectLimits())
        ModObjectLimit(FourCC(objectTypeId), objectLimit);
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