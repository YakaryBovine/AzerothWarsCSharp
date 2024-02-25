using System;
using System.Collections.Generic;
using MacroTools.Extensions;
using static War3Api.Common;
using WCSharp.SaveLoad;
using WCSharp.Shared;

namespace MacroTools.Save
{
  /// <summary>
  /// Manager class for saving and loading player settings.
  /// </summary>
  public static class SaveManager
  {
    internal static Dictionary<player, PlayerSettings> SavesByPlayer { get; } = new();
    private static SaveSystem<PlayerSettings>? _saveSystem;
	
    public static void Initialize()
    {
      _saveSystem = new SaveSystem<PlayerSettings>(new SaveSystemOptions
      {
        Hash1 = 36653,
        Hash2 = 612319,
        Salt = "zCi5fkypenPpgukyoEW8H6YC",
        BindSavesToPlayerName = true,
        SaveFolder = "WarcraftLegacies"
      });

      _saveSystem.OnSaveLoaded += SaveManager_OnSaveLoaded;

      foreach (var player in Util.EnumeratePlayers())
      {
        _saveSystem.Load(player,0);
      }
    }

    private static void SaveManager_OnSaveLoaded(PlayerSettings save, LoadResult loadResult)
    {
      SavesByPlayer[save.GetPlayer()] = save;
        
      if (loadResult != LoadResult.Success)
      {
        save.CamDistance = 2400;
        save.ShowQuestText = true;
        save.PlayDialogue = true;
        save.ShowCaptions = true;
      }
      save.GetPlayer().ApplyCameraField(CAMERA_FIELD_TARGET_DISTANCE, save.CamDistance, 1);
		
      // If the load result is anything except success, the save will be a newly created object
      if (loadResult == LoadResult.FailedHash)
      {
        Console.WriteLine($"Validating save file for {GetPlayerName(save.GetPlayer())} failed! The game should probably be restarted.");
      }
      // Extension method for determining whether the load result is any of the failed states
      if (loadResult.Failed())
      {
        Console.WriteLine("An existing save failed to load correctly!");
      }
    }
	
    /// <summary>
    /// Saves the player settings for the given player.
    /// </summary>
    /// <param name="save"></param>
    internal static void Save(PlayerSettings save)
    {
      _saveSystem?.Save(save);
    }
  }
}