﻿using System.IO;
using System.Text.Json;
using AutoMapper;
using Launcher.DataTransferObjects;
using Launcher.JsonConverters;
using War3Net.Build;
using War3Net.Build.Environment;
using War3Net.Build.Widget;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Launcher.Services
{
  /// <summary>
  /// Converts a Warcraft 3 map into .json files so that they can be stored in version control.
  /// </summary>
  public sealed class W3XToJsonConversionService
  {
    private readonly IMapper _mapper;

    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
      IgnoreReadOnlyProperties = true,
      WriteIndented = true,
      Converters = { new ColorJsonConverter() }
    };

    public W3XToJsonConversionService(IMapper mapper)
    {
      _mapper = mapper;
    }

    private const string UpgradeObjectDataPath = "UpgradeObjectData.json";
    private const string UnitObjectDataPath = "UnitObjectData.json";
    private const string ItemObjectDataPath = "ItemObjectData.json";
    private const string DoodadObjectDataPath = "DoodadObjectData.json";
    private const string DestructableObjectDataPath = "DestructableObjectData.json";
    private const string CustomTextTriggersPath = "CustomTextTriggers.json";
    private const string BuffObjectDataPath = "BuffObjectData.json";
    private const string AbilityObjectDataPath = "AbilityObjectData.json";
    private const string TriggerStringsPath = "TriggerStrings.json";
    private const string ShadowMapPath = "ShadowMap.json";
    private const string PreviewIconsPath = "PreviewIcons.json";
    private const string PathingMapPath = "PathingMap.json";
    private const string ImportedFilesPath = "ImportedFiles.json";
    private const string UnitsPath = "Units.json";
    private const string SoundsPath = "Sounds.json";
    private const string ScriptPath = "Script.json";
    private const string RegionsPath = "Regions.json";
    private const string InfoPath = "Info.json";
    private const string EnvironmentPath = "Environment.json";
    private const string DoodadsPath = "Doodads.json";
    private const string TriggersPath = "Triggers.json";

    /// <summary>
    /// Converts the provided Warcraft 3 map to JSON and saves it in the specified folder.
    /// </summary>
    public void Convert(string baseMapPath, string outputFolderPath)
    {
      var map = Map.Open(baseMapPath);
      SerializeAndWrite<MapDoodads, MapDoodadsDto>(map.Doodads, outputFolderPath, DoodadsPath);
      SerializeAndWrite(map.Environment, outputFolderPath, EnvironmentPath);
      SerializeAndWrite(map.Info, outputFolderPath, InfoPath);
      SerializeAndWrite<MapRegions, MapRegionsDto>(map.Regions, outputFolderPath, RegionsPath);
      SerializeAndWrite(map.Script, outputFolderPath, ScriptPath);
      SerializeAndWrite(map.Sounds, outputFolderPath, SoundsPath);
      SerializeAndWrite<MapUnits, MapUnitsDto>(map.Units, outputFolderPath, UnitsPath);
      SerializeAndWrite(map.ImportedFiles, outputFolderPath, ImportedFilesPath);
      SerializeAndWrite(map.PathingMap, outputFolderPath, PathingMapPath);
      SerializeAndWrite(map.PreviewIcons, outputFolderPath, PreviewIconsPath);
      SerializeAndWrite(map.ShadowMap, outputFolderPath, ShadowMapPath);
      SerializeAndWrite(map.TriggerStrings, outputFolderPath, TriggerStringsPath);
      SerializeAndWrite(map.AbilityObjectData, outputFolderPath, AbilityObjectDataPath);
      SerializeAndWrite(map.BuffObjectData, outputFolderPath, BuffObjectDataPath);
      SerializeAndWrite(map.CustomTextTriggers, outputFolderPath, CustomTextTriggersPath);
      SerializeAndWrite(map.DestructableObjectData, outputFolderPath, DestructableObjectDataPath);
      SerializeAndWrite(map.DoodadObjectData, outputFolderPath, DoodadObjectDataPath);
      SerializeAndWrite(map.ItemObjectData, outputFolderPath, ItemObjectDataPath);
      SerializeAndWrite(map.UnitObjectData, outputFolderPath, UnitObjectDataPath);
      SerializeAndWrite(map.UpgradeObjectData, outputFolderPath, UpgradeObjectDataPath);
      SerializeAndWrite(map.Triggers, outputFolderPath, TriggersPath);
      SerializeAndWrite(map.Script, outputFolderPath, ScriptPath);
    }

    private void SerializeAndWrite<TInput, TDataTransferObject>(TInput inputValue, string outputFolderPath, string subPath)
    {
      var dataTransferObject = _mapper.Map<TInput, TDataTransferObject>(inputValue);
      SerializeAndWrite(dataTransferObject, outputFolderPath, subPath);
    }
    
    private void SerializeAndWrite<T>(T value, string outputFolderPath, string subPath)
    {
      if (!Directory.Exists(outputFolderPath))
        Directory.CreateDirectory(outputFolderPath!);
      
      var asJson = JsonSerializer.Serialize(value, _jsonSerializerOptions);
      var fullPath = Path.Combine(outputFolderPath, subPath);
      
      File.WriteAllText(fullPath, asJson);
    }
  }
}