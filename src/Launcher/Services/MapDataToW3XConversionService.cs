﻿#nullable enable
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using AutoMapper;
using Launcher.DataTransferObjects;
using Launcher.JsonConverters;
using War3Net.Build;
using War3Net.Build.Audio;
using War3Net.Build.Environment;
using War3Net.Build.Import;
using War3Net.Build.Info;
using War3Net.Build.Object;
using War3Net.Build.Script;
using War3Net.Build.Widget;

namespace Launcher.Services
{
  /// <summary>
  /// Converts collections of loose files into a playable Warcraft 3 map.
  /// </summary>
  public sealed class MapDataToW3XConversionService
  {
    private readonly IMapper _mapper;
    
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public MapDataToW3XConversionService(IMapper mapper, JsonModifierProvider jsonModifierProvider)
    {
      _mapper = mapper;
      _jsonSerializerOptions = new()
      {
        IgnoreReadOnlyProperties = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
        WriteIndented = true,
        TypeInfoResolver = new DefaultJsonTypeInfoResolver
        {
          Modifiers = { jsonModifierProvider.CastModificationSets }
        },
        Converters = { new ColorJsonConverter() }
      };
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
    private const string RegionsPath = "Regions.json";
    private const string InfoPath = "Info.json";
    private const string EnvironmentPath = "Environment.json";
    private const string DoodadsPath = "Doodads.json";
    private const string TriggersPath = "Triggers.json";
    private const string UpgradeSkinObjectDataPath = "UpgradeSkinObjectData.json";
    private const string UnitSkinObjectDataPath = "UnitSkinObjectData.json";
    private const string ItemSkinObjectDataPath = "ItemSkinObjectData.json";
    private const string DoodadSkinObjectDataPath = "DoodadSkinObjectData.json";
    private const string DestructableSkinObjectDataPath = "DestructableSkinObjectData.json";
    private const string BuffSkinObjectDataPath = "BuffSkinObjectData.json";
    private const string AbilitySkinObjectDataPath = "AbilitySkinObjectData.json";
    
    private const string ImportsPath = "Imports";

    /// <summary>
    /// Converts the provided JSON data into a Warcraft 3 map and saves it in the specified folder.
    /// </summary>
    public MapBuilder Convert(string mapDataRootFolder)
    {
      var map = new Map
      {
        Sounds = DeserializeFromFile<MapSounds, MapSoundsDto>(Path.Combine(mapDataRootFolder, SoundsPath)),
        Environment = DeserializeFromFile<MapEnvironment, MapEnvironmentDto>(Path.Combine(mapDataRootFolder, EnvironmentPath)),
        PathingMap = DeserializeFromFile<MapPathingMap, MapPathingMapDto>(Path.Combine(mapDataRootFolder, PathingMapPath)),
        PreviewIcons = DeserializeFromFile<MapPreviewIcons, MapPreviewIconsDto>(Path.Combine(mapDataRootFolder, PreviewIconsPath)),
        Regions = DeserializeFromFile<MapRegions, MapRegionsDto>(Path.Combine(mapDataRootFolder, RegionsPath)),
        ShadowMap = DeserializeFromFile<MapShadowMap, MapShadowMapDto>(Path.Combine(mapDataRootFolder, ShadowMapPath)),
        ImportedFiles = DeserializeFromFile<ImportedFiles, MapImportedFilesDto>(Path.Combine(mapDataRootFolder, ImportedFilesPath)),
        Info = DeserializeFromFile<MapInfo, MapInfoDto>(Path.Combine(mapDataRootFolder, InfoPath)),
        AbilityObjectData = DeserializeFromFile<AbilityObjectData, MapAbilityObjectDataDto>(Path.Combine(mapDataRootFolder, AbilityObjectDataPath)),
        BuffObjectData = DeserializeFromFile<BuffObjectData, MapBuffObjectDataDto>(Path.Combine(mapDataRootFolder, BuffObjectDataPath)),
        DestructableObjectData = DeserializeFromFile<DestructableObjectData, MapDestructableObjectDataDto>(Path.Combine(mapDataRootFolder, DestructableObjectDataPath)),
        DoodadObjectData = DeserializeFromFile<DoodadObjectData, MapDoodadObjectDataDto>(Path.Combine(mapDataRootFolder, DoodadObjectDataPath)),
        ItemObjectData = DeserializeFromFile<ItemObjectData, MapItemObjectDataDto>(Path.Combine(mapDataRootFolder, ItemObjectDataPath)),
        UnitObjectData = DeserializeFromFile<UnitObjectData, MapUnitObjectDataDto>(Path.Combine(mapDataRootFolder, UnitObjectDataPath)),
        UpgradeObjectData = DeserializeFromFile<UpgradeObjectData, MapUpgradeObjectDataDto>(Path.Combine(mapDataRootFolder, UpgradeObjectDataPath)),
        CustomTextTriggers = DeserializeFromFile<MapCustomTextTriggers, MapCustomTextTriggersDto>(Path.Combine(mapDataRootFolder, CustomTextTriggersPath)),
        TriggerStrings = DeserializeFromFile<TriggerStrings, MapTriggerStringsDto>(Path.Combine(mapDataRootFolder, TriggerStringsPath)),
        Doodads = DeserializeFromFile<MapDoodads, MapDoodadsDto>(Path.Combine(mapDataRootFolder, DoodadsPath)),
        Units = DeserializeFromFile<MapUnits, MapUnitsDto>(Path.Combine(mapDataRootFolder, UnitsPath)),
        Triggers = DeserializeFromFile<MapTriggers, MapTriggersDto>(Path.Combine(mapDataRootFolder, TriggersPath)),
        AbilitySkinObjectData = DeserializeFromFile<AbilityObjectData, MapAbilityObjectDataDto>(Path.Combine(mapDataRootFolder, AbilitySkinObjectDataPath)),
        BuffSkinObjectData = DeserializeFromFile<BuffObjectData, MapBuffObjectDataDto>(Path.Combine(mapDataRootFolder, BuffSkinObjectDataPath)),
        DestructableSkinObjectData = DeserializeFromFile<DestructableObjectData, MapDestructableObjectDataDto>(Path.Combine(mapDataRootFolder, DestructableSkinObjectDataPath)),
        DoodadSkinObjectData = DeserializeFromFile<DoodadObjectData, MapDoodadObjectDataDto>(Path.Combine(mapDataRootFolder, DoodadSkinObjectDataPath)),
        ItemSkinObjectData = DeserializeFromFile<ItemObjectData, MapItemObjectDataDto>(Path.Combine(mapDataRootFolder, ItemSkinObjectDataPath)),
        UnitSkinObjectData = DeserializeFromFile<UnitObjectData, MapUnitObjectDataDto>(Path.Combine(mapDataRootFolder, UnitSkinObjectDataPath)),
        UpgradeSkinObjectData = DeserializeFromFile<UpgradeObjectData, MapUpgradeObjectDataDto>(Path.Combine(mapDataRootFolder, UpgradeSkinObjectDataPath)),
        Script = File.ReadAllText(Path.Combine(mapDataRootFolder, "Script.json"))
      };

      var builder = new MapBuilder(map);
      builder.AddFiles($@"{mapDataRootFolder}\{ImportsPath}", "*", SearchOption.AllDirectories);
      return builder;
    }

    private TReturn DeserializeFromFile<TReturn, TDataTransferObject>(string filePath)
    {
      var dto = JsonSerializer.Deserialize<TDataTransferObject>(File.ReadAllText(filePath), _jsonSerializerOptions);
      return _mapper.Map<TReturn>(dto);
    }
  }
}