using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.Sound;
using static War3Api.Common;

namespace MacroTools.QuestSystem
{
  public static class FactionQuestExtensions
  {
    /// <summary>
    ///   Displays a warning message to everyone except the player that completed the <see cref="QuestData" />.
    /// </summary>
    internal static void DisplayCompletedGlobal(this player whichPlayer, QuestData questData)
    {
      var soundCompleted = SoundLibrary.Completed;
      var soundFailed = SoundLibrary.Failed;
      StartSound(GetLocalPlayer().GetTeam()?.Contains(whichPlayer) == true
        ? soundCompleted
        : soundFailed);
      
      foreach (var enumPlayer in WCSharp.Shared.Util.EnumeratePlayers())
        if (enumPlayer != whichPlayer)
          if (PlayerData.ByHandle(whichPlayer).PlayerSettings.ShowQuestText)
            DisplayTextToPlayer(enumPlayer, 0, 0,
              $"\n|cffffcc00MAJOR EVENT - {whichPlayer.GetFaction()?.PrefixCol}{questData.Title}|r\n{questData.RewardFlavour}\n");
    }

    internal static void DisplayFailed(this Faction faction, QuestData questData)
    {
      var display = !string.IsNullOrEmpty(questData.PenaltyFlavour)
        ? $"\n|cffffcc00QUEST FAILED - {questData.Title}|r\n{questData.PenaltyFlavour}\n"
        : $"\n|cffffcc00QUEST FAILED - {questData.Title}|r\n{questData.Flavour}\n";

      foreach (var questItem in questData.Objectives)
        if (questItem.ShowsInQuestLog)
          display = questItem.Progress switch
          {
            QuestProgress.Complete => $"{display} - |cff808080{questItem.Description} (Completed)|r\n",
            QuestProgress.Failed => $"{display} - |cffCD5C5C{questItem.Description} (Failed)|r\n",
            _ => $"{display} - {questItem.Description}\n"
          };
      if (faction.Player != null && faction.Player.GetPlayerSettings().ShowQuestText)
        DisplayTextToPlayer(faction.Player, 0, 0, display);

      faction.Player?.PlaySound(SoundLibrary.Failed);
    }

    internal static void DisplayCompleted(this Faction faction, QuestData questData)
    {
      var display = $"\n|cffffcc00QUEST COMPLETED - {questData.Title}|r\n{questData.RewardFlavour}\n";
      foreach (var questItem in questData.Objectives)
        if (questItem.ShowsInQuestLog)
          display = $"{display} - |cff808080{questItem.Description} (Completed)|r\n";
      if (faction.Player != null && faction.Player.GetPlayerSettings().ShowQuestText)
        DisplayTextToPlayer(faction.Player, 0, 0, display);

      faction.Player?.PlaySound(SoundLibrary.Completed);
    }

    /// <summary>
    /// Indicates to the provided question that the quest has been discovered.
    /// </summary>
    public static void DisplayDiscovered(this Faction faction, QuestData questData)
    {
      var display = $"\n|cffffcc00QUEST DISCOVERED - {questData.Title}|r\n{questData.Flavour}\n";
      foreach (var questItem in questData.Objectives)
        if (questItem.ShowsInQuestLog)
        {
          display = questItem.Progress == QuestProgress.Complete
            ? $"{display} - |cff808080{questItem.Description} (Completed)|r\n"
            : $"{display} - {questItem.Description}\n";
        }
      if (faction.Player != null && faction.Player.GetPlayerSettings().ShowQuestText)
        DisplayTextToPlayer(faction.Player, 0, 0, display);
      var sound = SoundLibrary.Discovered;
      faction.Player?.PlaySound(sound);
    }
  }
}