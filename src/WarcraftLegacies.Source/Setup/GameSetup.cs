using MacroTools;
using MacroTools.CommandSystem;
using MacroTools.ControlPointSystem;
using MacroTools.GameModes;
using MacroTools.PassiveAbilitySystem;
using MacroTools.Save;
using MacroTools.Sound;
using MacroTools.UserInterface;
using WarcraftLegacies.Source.ArtifactBehaviour;
using WarcraftLegacies.Source.GameLogic;
using WarcraftLegacies.Source.GameLogic.GameEnd;
using WarcraftLegacies.Source.GameModes;
using WarcraftLegacies.Source.UnitTypes;
using MacroTools.FactionChoices;

namespace WarcraftLegacies.Source.Setup
{
  /// <summary>
  /// Responsible for setting up the entire game.
  /// </summary>
  public static class GameSetup
  {
    /// <summary>
    /// Initialize the entire game.
    /// </summary>
    public static void Setup()
    {
      SaveManager.Initialize();
      DisplayIntroText.Setup(25);
      CinematicMode.Setup(59);
      SetupControlPointManager();
      var preplacedUnitSystem = new PreplacedUnitSystem();
      SoundLibrary.Setup();
      var artifactSetup = new ArtifactSetup(preplacedUnitSystem);
      var allLegendSetup = new AllLegendSetup(preplacedUnitSystem, artifactSetup);
      allLegendSetup.RegisterLegends(preplacedUnitSystem);
      ShoreSetup.Setup();
      ControlPointSetup.Setup();
      InstanceSetup.Setup(preplacedUnitSystem);
      NeutralHostileSetup.Setup();
      TeamSetup.Setup();
      new PlayerSetup(preplacedUnitSystem, allLegendSetup, artifactSetup).Setup();
      var sharedGoldMineManager = new SharedGoldMineManager();
      FactionChoiceDialogSetup.Setup(preplacedUnitSystem, artifactSetup, allLegendSetup, sharedGoldMineManager);
      SharedQuestSetup.Setup(preplacedUnitSystem, artifactSetup, allLegendSetup);
      SpellsSetup.Setup();
      var commandManager = new CommandManager();
      CommandSetup.Setup(commandManager);
      FactionMultiboard.Setup();
      BookSetup.Setup();
      HintConfig.Setup();
      QuestMenuSetup.Setup();
      GameTime.Start();
      CheatSetup.Setup(commandManager, artifactSetup);
      MapFlagSetup.Setup();
      InfoQuests.Setup();
      DestructibleSetup.Setup(preplacedUnitSystem);
      PatronSystem.Setup(preplacedUnitSystem);
      var gameModeManager =new GameModeManager(new IGameMode[]
      {
        new ClosedAlliance(),
        new OpenAlliance(),
        new GreatWar()
      })
      {
        TimeToDisplay = 49,
        VoteLength = 10
      };
      gameModeManager.Setup();
      RockSetup.Setup();
      TurnResearchSetup.Setup();
      UnitTypeConfig.Setup();
      ShipyardBanZonesSetup.Setup();
      BlockerSetup.Setup();
      NeutralVictimAndPassiveSetup.Setup();
      GateSetup.Setup();
      StartingResources.Setup();
      StartingQuestPopup.Setup(63);
      RefundZeroLimitUnits.Setup();
      HeroGlowFix.Setup();
      CleanupUnoccupiedPlayerSlots.Setup();
      PlayerLeaves.Setup();
      FloatingTextSetup.Setup(60, 10);
      AmbianceSetup.Setup();
      PassiveAbilityManager.InitializePreplacedUnits();
      IncompatibleResearchSetup.Setup();
      DemonGateSetup.Setup();
      SummonRallyPoints.Setup();
      RemoveUnusedAreas.Run();
      EyeOfSargerasCooldowns.Setup();
      CapturableUnitSetup.Setup(preplacedUnitSystem);
      EyeOfSargerasPickup.Setup();
      RuntimeIntegrityChecker.Setup();
      DarkPortalControlNexusSetup.Setup(preplacedUnitSystem);
      TagSummonedUnits.Setup();
    }

    private static void SetupControlPointManager()
    {
      ControlPointManager.Instance = new ControlPointManager
      {
        StartingMaxHitPoints = 1900,
        HostileStartingCurrentHitPoints = 1000,
        RegenerationAbility = ABILITY_A0UT_CP_LIFE_REGEN,
        PiercingResistanceAbility = ABILITY_A13X_MAGIC_RESISTANCE_CONTROL_POINT_TOWER,
        IncreaseControlLevelAbilityTypeId = ABILITY_A0A8_FORTIFY_CONTROL_POINTS_SHARED,
        ControlLevelSettings = new ControlLevelSettings
        {
          DefaultDefenderUnitTypeId = UNIT_H03W_CONTROL_POINT_DEFENDER_LORDAERON,
          DamageBase = 8,
          DamagePerControlLevel = 1,
          ArmorPerControlLevel = 1,
          HitPointsPerControlLevel = 70,
          ControlLevelMaximum = 30
        }
      };
    }
  }
}