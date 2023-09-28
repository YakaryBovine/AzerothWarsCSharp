using MacroTools.Cheats;
using MacroTools.CommandSystem;
using MacroTools.QuestSystem;
using WarcraftLegacies.Source.Cheats;

namespace WarcraftLegacies.Source.Setup
{
  public static class CheatSetup
  {
    public static void Setup(CommandManager commandManager)
    {
      commandManager.Register(new CheatAddSpell());
      commandManager.Register(new CheatResearchLevel());
      commandManager.Register(new CheatBuild());
      commandManager.Register(new CheatQuestProgress("complete", QuestProgress.Complete));
      commandManager.Register(new CheatQuestProgress("fail", QuestProgress.Failed));
      commandManager.Register(new CheatQuestProgress("uncomplete", QuestProgress.Incomplete));
      commandManager.Register(new CheatQuestProgress("undiscover", QuestProgress.Undiscovered));
      commandManager.Register(new CheatControl());
      commandManager.Register(new CheatDestroy());
      commandManager.Register(new CheatFaction());
      commandManager.Register(new CheatFood());
      commandManager.Register(new CheatGod());
      commandManager.Register(new CheatGold());
      commandManager.Register(new CheatHp());
      commandManager.Register(new CheatKick());
      commandManager.Register(new CheatLevel());
      commandManager.Register(new CheatLumber());
      commandManager.Register(new CheatMana());
      commandManager.Register(new CheatMp());
      commandManager.Register(new CheatNocd());
      commandManager.Register(new CheatOwner());
      commandManager.Register(new CheatRemove());
      commandManager.Register(new CheatResearchLevel());
      commandManager.Register(new CheatSetResearchLevel());
      commandManager.Register(new CheatShowQuestNames());
      commandManager.Register(new CheatSpawn());
      commandManager.Register(new CheatTeam());   
      commandManager.Register(new CheatTele());
      commandManager.Register(new CheatTime());
      commandManager.Register(new CheatUncontrol());
      commandManager.Register(new CheatVision());
      commandManager.Register(new CheatShore());
      commandManager.Register(new CheatPosition());
      commandManager.Register(new CheatGetUnitAbilities());
      commandManager.Register(new CheatRemoveAllAbilities());
      TestMode.Setup(commandManager);
      var cheatSkipCinematic = new CheatSkipCinematic();
      CheatSkipCinematic.Init();
    }
  }
}