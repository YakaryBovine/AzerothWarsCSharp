#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using CSharpLua;
using Launcher.Extensions;
using Launcher.MapMigrations;
using Launcher.Settings;
using Microsoft.CodeAnalysis;
using War3Net.Build;
using War3Net.Build.Extensions;
using War3Net.Build.Info;
using War3Net.IO.Mpq;
using CoreSystemProvider = CSharpLua.CoreSystem.CoreSystemProvider;

namespace Launcher.Services
{
  /// <summary>
  /// Builds a Warcraft 3 map based on inputs.
  /// </summary>
  public sealed class AdvancedMapBuilder
  {
    private readonly CompilerSettings _compilerSettings;
    private readonly MapSettings _mapSettings;
    private const string War3MapLua = MapDataPaths.ScriptPath;
    
    private const string GraphicsApi = "Direct3D9";
#if DEBUG
    private const bool Debug = true;
#else
		private const bool Debug = false;
#endif

    public AdvancedMapBuilder(CompilerSettings compilerSettings, MapSettings mapSettings)
    {
      _compilerSettings = compilerSettings;
      _mapSettings = mapSettings;
    }
    
    /// <summary>
    /// Builds a Warcraft 3 map based on the provided inputs and saves it as a file.
    /// </summary>
    public void SaveMapFile(Map map, IEnumerable<DirectoryEnumerationOptions> additionalFileDirectories, AdvancedMapBuilderOptions options)
    {
      var mapFilePath = GetMapFullFilePath(options);
      SupplementMap(map, options);
      
      var mapBuilder = new MapBuilder(map);
      if (Directory.Exists(_compilerSettings.SharedAssetsPath))
        mapBuilder.AddFiles(_compilerSettings.SharedAssetsPath);

      foreach (var additionalFileDirectory in additionalFileDirectories)
        mapBuilder.AddFiles(additionalFileDirectory.Path, additionalFileDirectory.SearchPattern ?? "*", SearchOption.AllDirectories);

      var archiveCreateOptions = new MpqArchiveCreateOptions
      {
        BlockSize = 3,
        AttributesCreateMode = MpqFileCreateMode.Overwrite,
        ListFileCreateMode = MpqFileCreateMode.Overwrite
      };

      mapBuilder.Build(mapFilePath, archiveCreateOptions);
      if (options.Launch)
        LaunchGame(_compilerSettings.Warcraft3ExecutablePath, mapFilePath);
    }

    /// <summary>
    /// Builds a Warcraft 3 map based on the provided inputs and saves it as a directory.
    /// </summary>
    public void SaveMapDirectory(Map map, IEnumerable<PathData> additionalFiles, AdvancedMapBuilderOptions options)
    {
      var mapFilePath = GetMapFullFilePath(options);
      SupplementMap(map, options);
      
      if (options.BackupDirectory != null)
        BackupFiles(options.BackupDirectory, mapFilePath);
      
      Directory.Delete(mapFilePath, true);
      map.BuildDirectory(@$"{mapFilePath}\", additionalFiles);
    }
    
    private void SupplementMap(Map map, AdvancedMapBuilderOptions options)
    {
      ApplyMigrations(map);
      
      if (options.SetMapTitles)
        SetMapTitles(map, _mapSettings.Version);

      if (options.Launch)
        SetTestPlayerSlot(map, _compilerSettings.TestingPlayerSlot);

      if (options.SourceCodeProjectFolderPath != null)
        AddCSharpCode(map, options.SourceCodeProjectFolderPath, _compilerSettings);
    }

    private static void ApplyMigrations(Map map)
    {
      var objectDatabase = map.GetObjectDatabaseFromMap();
      foreach (var migration in MapMigrationProvider.GetMapMigrations())
        migration.Migrate(map, objectDatabase);

      map.UnitObjectData.FixUnkValues();
    }
    
    private static void SetMapTitles(Map map, string version)
    {
      if (map.Info == null)
        return;
      map.Info.MapName = $"Warcraft Legacies {version}";
      map.Info.LoadingScreenTitle = $"Warcraft Legacies {version}";
    }
    
    /// <summary>
    ///   Makes all players prior to the given player slot computers,
    ///   so that the given player slot is what the tester will play as when the map is launched.
    /// </summary>
    private static void SetTestPlayerSlot(Map map, int playerSlot)
    {
      if (map.Info == null) return;
      foreach (var player in map.Info.Players)
        if (player.Id < playerSlot && player.Id != playerSlot && player.Controller != PlayerController.None)
          player.Controller = PlayerController.Computer;
    }
    
    public static void AddCSharpCode(Map map, string projectFolderPath, CompilerSettings compilerSettings)
    {
      //Set debug options if necessary, configure compiler
      const string csc = Debug ? "-debug -define:DEBUG" : null;
      var csproj = Directory.EnumerateFiles(projectFolderPath, "*.csproj", SearchOption.TopDirectoryOnly).Single();
      var compiler = new Compiler(csproj, compilerSettings.ArtifactsPath, string.Empty, null!,
        "War3Api.*;WCSharp.*;MacroTools.*", "", csc, false, null,
        string.Empty)
      {
        IsExportMetadata = true,
        IsModule = false,
        IsInlineSimpleProperty = false,
        IsPreventDebugObject = true,
        IsCommentsDisabled = !Debug
      };
      
      // Collect required paths and compile
      var coreSystemFiles = CoreSystemProvider.GetCoreSystemFiles();
      var blizzardJ = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
        "Warcraft III/JassHelper/Blizzard.j");
      var commonJ = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
        "Warcraft III/JassHelper/common.j");
      var mapScriptBuilder = new MapScriptBuilder();
      mapScriptBuilder.SetDefaultOptionsForCSharpLua();
      mapScriptBuilder.ForceGenerateGlobalDestructableVariable = false;
      
      var compileResult = map.CompileScript(compiler, mapScriptBuilder, coreSystemFiles, blizzardJ, commonJ);

      // If compilation failed, output an error
      if (!compileResult.Success)
        throw new Exception(compileResult.Diagnostics.First(x => x.Severity == DiagnosticSeverity.Error).GetMessage());

      // Update war3map.lua so you can inspect the generated Lua code easily
      if (compilerSettings.ArtifactsPath != null)
      {
        Directory.CreateDirectory(compilerSettings.ArtifactsPath);
        File.WriteAllText(Path.Combine(compilerSettings.ArtifactsPath, War3MapLua), map.Script);
      }
    }
    
    private string GetMapFullFilePath(AdvancedMapBuilderOptions options)
    {
      return options.SetMapTitles
        ? $"{Path.Combine(options.OutputDirectory, options.MapName)}{_mapSettings.Version}.w3x"
        : $"{Path.Combine(options.OutputDirectory, options.MapName)}.w3x";
    }
    
    private static void BackupFiles(string backupDirectory, string mapPath)
    {
      if (File.Exists(mapPath))
      {
        Directory.CreateDirectory(backupDirectory);
        var backupName = $"{Path.GetFileNameWithoutExtension(mapPath)}-{DateTime.Now:yyyyMMdd_HHmmss}.w3x";
        File.Copy(mapPath, Path.Combine(backupDirectory, backupName));
      }

      if (Directory.Exists(mapPath))
      {
        Directory.CreateDirectory(backupDirectory);
        var backupName = $"{Path.GetFileNameWithoutExtension(mapPath)}-{DateTime.Now:yyyyMMdd_HHmmss}.w3x";
        DirectoryUtils.CopyDirectory(mapPath, Path.Combine(backupDirectory, backupName), true);
      }
    }

    private static void LaunchGame(string warcraft3ExecutablePath, string mapFilePath)
    {
      if (File.Exists(warcraft3ExecutablePath))
      {
        var commandLineArgs = new StringBuilder();
        var fileVersion = FileVersionInfo.GetVersionInfo(warcraft3ExecutablePath).FileVersion;
        var isReforged = fileVersion != null && Version.Parse(fileVersion) >= new Version(1, 32);
        commandLineArgs.Append(isReforged ? " -launch" : $" -graphicsapi {GraphicsApi}");

        commandLineArgs.Append(" -nowfpause");

        var absoluteMapPath = new FileInfo(mapFilePath).FullName;
        commandLineArgs.Append($" -loadfile \"{absoluteMapPath}\"");

        Process.Start(warcraft3ExecutablePath, commandLineArgs.ToString());
      }
      else
      {
        throw new Exception(
          "Please set Warcraft3ExecutablePath in Launcher/appsettings.json to the path of your Warcraft III executable.");
      }
    }
  }
}