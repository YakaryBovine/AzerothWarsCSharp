using MacroTools.CommandSystem;
using MacroTools.ControlPointSystem;
using MacroTools.Mechanics;
using MacroTools.PassiveAbilitySystem;
using MacroTools.ResearchSystems;
using MacroTools.Save;
using MacroTools.UserInterface;
using static War3Api.Common;

namespace TestMap.Source.Setup
{
  public static class GameSetup
  {
    public static void Setup()
    {
      SetPlayerState(Player(0), PLAYER_STATE_RESOURCE_FOOD_CAP, 100);
      SetPlayerState(Player(0), PLAYER_STATE_RESOURCE_GOLD, 10000);
      SetPlayerState(Player(0), PLAYER_STATE_RESOURCE_LUMBER, 10000);
      SaveManager.Initialize();
      ControlPointManager.Instance = new ControlPointManager();
      LegendSetup.Setup();
      TeamSetup.Setup();
      AllFactionSetup.Setup();
      PlayerSetup.Setup();
      ArtifactSetup.Setup();
      ControlPointSetup.Setup();
      AllQuestSetup.Setup();
      SpellSetup.Setup();
      CheatSetup.Setup(new CommandManager());
      FactionMultiboard.Setup();
      BookSetup.Setup();
      TestSetup.Setup();
      WaygateManager.Setup(FourCC("nwgt"));
      HintSetup.Setup();
      AugmentSetup.Setup();
      PassiveAbilityManager.InitializePreplacedUnits();
      ResearchManager.RegisterIncompatibleSet(
        new BasicResearch(FourCC("Rhan"), 30, 30),
        new BasicResearch(FourCC("Rhri"), 30, 30),
        new BasicResearch(FourCC("Rhde"), 30, 30)
      );
    }
  }
}