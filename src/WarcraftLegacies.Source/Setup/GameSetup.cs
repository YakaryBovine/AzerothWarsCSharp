using MacroTools;
using MacroTools.CommandSystem;
using MacroTools.ControlPointSystem;
using MacroTools.GameModes;
using MacroTools.Mechanics;
using MacroTools.PassiveAbilitySystem;
using MacroTools.Save;
using MacroTools.UserInterface;
using WarcraftLegacies.Source.ArtifactBehaviour;
using WarcraftLegacies.Source.GameLogic;
using WarcraftLegacies.Source.GameLogic.GameEnd;
using WarcraftLegacies.Source.GameModes;
using WarcraftLegacies.Source.Mechanics.Druids;
using WarcraftLegacies.Source.Mechanics.Frostwolf;
using WarcraftLegacies.Source.Mechanics.Scourge;
using WarcraftLegacies.Source.Mechanics.Scourge.Blight;
using WarcraftLegacies.Source.Setup.FactionSetup;
using WarcraftLegacies.Source.UnitTypes;
using static War3Api.Common;

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
      TeamSetup.Setup();
      AllFactionSetup.Setup(preplacedUnitSystem, artifactSetup, allLegendSetup);
      SharedFactionConfigSetup.Setup();
      PlayerSetup.Setup();
      new FactionChoiceDialogPresenter(GoblinSetup.Goblin, ZandalarSetup.Zandalar).Run(Player(8));
      new FactionChoiceDialogPresenter(IllidariSetup.Illidari, SunfurySetup.Sunfury).Run(Player(15));
      new FactionChoiceDialogPresenter(DalaranSetup.Dalaran, GilneasSetup.Gilneas).Run(Player(7));
      NeutralHostileSetup.Setup();
      AllQuestSetup.Setup(preplacedUnitSystem, artifactSetup, allLegendSetup);
      ObserverSetup.Setup(new[] { Player(21) });
      SpellsSetup.Setup();
      var commandManager = new CommandManager();
      CommandSetup.Setup(commandManager);
      ControlPointVictory.Setup();
      FactionMultiboard.Setup();
      BookSetup.Setup();
      HintConfig.Setup();
      WaygateManager.Setup(Constants.UNIT_N0AO_WAY_GATE_DALARAN_OTHER);
      BlightSystem.Setup(ScourgeSetup.Scourge);
      BlightSetup.Setup(preplacedUnitSystem);
      QuestMenuSetup.Setup();
      GameTime.Start();
      CheatSetup.Setup(commandManager);
      DialogueSetup.Setup(preplacedUnitSystem, allLegendSetup);
      MapFlagSetup.Setup();
      InfoQuests.Setup();
      DestructibleSetup.Setup(preplacedUnitSystem);
      ResearchSetup.Setup(preplacedUnitSystem);
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
      CleanPersons.Setup();
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
      SacrificeAcolyte.Setup();
      RuntimeIntegrityChecker.Setup(true);
      PeonsStartHarvestingShips.Setup(preplacedUnitSystem);
      DarkPortalControlNexusSetup.Setup(preplacedUnitSystem);
      BlackPortalControlNexusSetup.Setup(preplacedUnitSystem);
      CenariusGhost.Setup(allLegendSetup.Druids);
      HelmOfDominationDropsWhenScourgeLeaves.Setup(artifactSetup.HelmOfDomination, allLegendSetup.Scourge.TheFrozenThrone);
      TagSummonedUnits.Setup();
    }

    private static void SetupControlPointManager()
    {
      ControlPointManager.Instance = new ControlPointManager
      {
        StartingMaxHitPoints = 1900,
        HostileStartingCurrentHitPoints = 1000,
        RegenerationAbility = Constants.ABILITY_A0UT_CP_LIFE_REGEN,
        PiercingResistanceAbility = Constants.ABILITY_A13X_MAGIC_RESISTANCE_CONTROL_POINT_TOWER,
        IncreaseControlLevelAbilityTypeId = Constants.ABILITY_A0A8_FORTIFY_CONTROL_POINTS_SHARED,
        ControlLevelSettings = new ControlLevelSettings
        {
          DefaultDefenderUnitTypeId = Constants.UNIT_H03W_CONTROL_POINT_DEFENDER_LORDAERON,
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