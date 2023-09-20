#nullable enable
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using Launcher.DataTransferObjects;
using Launcher.DTOMappers;
using Launcher.Extensions;
using Launcher.JsonConverters;
using Launcher.MapMigrations;
using War3Net.Build;
using War3Net.Build.Audio;
using War3Net.Build.Environment;
using War3Net.Build.Info;
using War3Net.Build.Object;
using War3Net.Build.Widget;
using JsonSerializer = System.Text.Json.JsonSerializer;
using static Launcher.MapDataPaths;

namespace Launcher.Services
{
  /// <summary>
  /// Converts a Warcraft 3 map into a collection of loose files so that they can be stored in version control.
  /// </summary>
  public sealed class W3XToMapDataConverter
  {
    private readonly IMapper _mapper;

    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
      IgnoreReadOnlyProperties = true,
      DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
      WriteIndented = true,
      Converters = { new ColorJsonConverter() }
    };

    public W3XToMapDataConverter(IMapper mapper)
    {
      _mapper = mapper;
    }

    /// <summary>
    /// Converts the provided Warcraft 3 map to JSON and saves it in the specified folder.
    /// </summary>
    public void Convert(string baseMapPath, string outputFolderPath)
    {
      var map = Map.Open(baseMapPath);
      var triggerStrings = map.TriggerStrings.ToDictionary();
      SerializeAndWriteMapData(map, triggerStrings, outputFolderPath);
      
      CopyImportedFiles(baseMapPath, outputFolderPath);
      CopyUnserializableFiles(baseMapPath, outputFolderPath);
      CopyGameInterface(baseMapPath, triggerStrings, outputFolderPath);
      ApplyMigrations(map);
    }

    private void SerializeAndWriteMapData(Map map, TriggerStringDictionary triggerStrings, string outputFolderPath)
    {
      if (Directory.Exists(outputFolderPath))
        Directory.Delete(outputFolderPath, true);
      
      var objectDataMapper = new ObjectDataMapper(triggerStrings);
      
      if (map.Doodads != null) 
        SerializeAndWriteDoodads(map.Doodads, Path.Combine(outputFolderPath, DoodadsDirectoryPath));
      if (map.Environment != null) 
        SerializeAndWrite<MapEnvironment, MapEnvironmentDto>(map.Environment, outputFolderPath, EnvironmentPath);
      if (map.Info != null)
        SerializeAndWriteMapInfo(map.Info, new MapInfoMapper(triggerStrings), Path.Combine(outputFolderPath, InfoPath));
      if (map.Regions != null)
        SerializeAndWriteRegions(map.Regions, Path.Combine(outputFolderPath, RegionsDirectoryPath));
      if (map.Units != null)
        SerializeAndWriteUnits(map.Units, Path.Combine(outputFolderPath, UnitsDirectoryPath));
      if (map.PathingMap != null)
        SerializeAndWrite<MapPathingMap, MapPathingMapDto>(map.PathingMap, outputFolderPath, PathingMapPath);
      if (map.PreviewIcons != null)
        SerializeAndWritePreviewIcons(map.PreviewIcons, Path.Combine(outputFolderPath, PreviewIconsPath));
      if (map.ShadowMap != null)
        SerializeAndWrite<MapShadowMap, MapShadowMapDto>(map.ShadowMap, outputFolderPath, ShadowMapPath);
      if (map.Sounds != null)
        SerializeAndWriteSounds(map.Sounds, Path.Combine(outputFolderPath, SoundsDirectoryPath));

      if (map.UnitObjectData != null)
        SerializeAndWriteUnitData(map.UnitObjectData, objectDataMapper, false, outputFolderPath, UnitDataDirectoryPath, CoreDataDirectorySubPath);
      if (map.UnitSkinObjectData != null)
        SerializeAndWriteUnitData(map.UnitSkinObjectData, objectDataMapper, true, outputFolderPath, UnitDataDirectoryPath, SkinDataDirectorySubPath);
      if (map.AbilityObjectData != null)
        SerializeAndWriteAbilityData(map.AbilityObjectData, objectDataMapper, false, outputFolderPath, AbilityDataDirectoryPath, CoreDataDirectorySubPath);
      if (map.AbilitySkinObjectData != null)
        SerializeAndWriteAbilityData(map.AbilitySkinObjectData, objectDataMapper, true, outputFolderPath, AbilityDataDirectoryPath, SkinDataDirectorySubPath);
      if (map.ItemObjectData != null)
        SerializeAndWriteItemData(map.ItemObjectData, objectDataMapper, false, outputFolderPath, ItemDataDirectoryPath, CoreDataDirectorySubPath);
      if (map.ItemSkinObjectData != null)
        SerializeAndWriteItemData(map.ItemSkinObjectData, objectDataMapper, true, outputFolderPath, ItemDataDirectoryPath, SkinDataDirectorySubPath);
      if (map.DestructableObjectData != null)
        SerializeAndWriteDestructableData(map.DestructableObjectData, objectDataMapper, false, outputFolderPath, DestructableDataDirectoryPath, CoreDataDirectorySubPath);
      if (map.DestructableSkinObjectData != null)
        SerializeAndWriteDestructableData(map.DestructableSkinObjectData, objectDataMapper, true, outputFolderPath, DestructableDataDirectoryPath, SkinDataDirectorySubPath);
      if (map.DoodadObjectData != null)
        SerializeAndWriteDoodadData(map.DoodadObjectData, objectDataMapper, true, outputFolderPath, DoodadDataDirectoryPath, CoreDataDirectorySubPath);
      if (map.DoodadSkinObjectData != null)
        SerializeAndWriteDoodadData(map.DoodadSkinObjectData, objectDataMapper, true, outputFolderPath, DoodadDataDirectoryPath, SkinDataDirectorySubPath);
      if (map.BuffObjectData != null)
        SerializeAndWriteBuffData(map.BuffObjectData, objectDataMapper, false, outputFolderPath, BuffDataDirectoryPath, CoreDataDirectorySubPath);
      if (map.BuffSkinObjectData != null)
        SerializeAndWriteBuffData(map.BuffSkinObjectData, objectDataMapper, true, outputFolderPath, BuffDataDirectoryPath, SkinDataDirectorySubPath);
      if (map.UpgradeObjectData != null)
        SerializeAndWriteUpgradeData(map.UpgradeObjectData, objectDataMapper, true, outputFolderPath, UpgradeDataDirectoryPath, CoreDataDirectorySubPath);
      if (map.UpgradeSkinObjectData != null) 
        SerializeAndWriteUpgradeData(map.UpgradeSkinObjectData, objectDataMapper, true, outputFolderPath, UpgradeDataDirectoryPath, SkinDataDirectorySubPath);
    }

    private static void CopyImportedFiles(string baseMapPath, string outputFolderPath)
    {
      var importFileExtensions = new [] {".blp", ".mdx", ".mdl", ".toc", ".fdf", ".mp3", ".webp", ".slk"};
      var files = Directory
        .EnumerateFiles(baseMapPath, "*", SearchOption.AllDirectories)
        .Where(file => importFileExtensions.Any(file.ToLower().EndsWith))
        .ToList();

      var unserializableFilePaths = GetUnserializableFilePaths().ToList();
      foreach (var file in files)
      {
        var relativePath = Path.GetRelativePath(baseMapPath, file);
        if (unserializableFilePaths.Contains(relativePath))
          continue;
        
        var destinationFileName = $@"{outputFolderPath}\{ImportsPath}\{relativePath}";
        Directory.CreateDirectory(Path.GetDirectoryName(destinationFileName)!);
        File.Copy(file, destinationFileName, true);
      }
    }
    
    private static void CopyUnserializableFiles(string baseMapPath, string outputFolderPath)
    {
      foreach (var filePath in GetUnserializableFilePaths())
      {
        var fullSourceFilePath = $"{baseMapPath}/{filePath}";
        if (!File.Exists(fullSourceFilePath)) 
          continue;
        
        var fullOutputFilePath = $@"{outputFolderPath}\{filePath}";
        if (fullSourceFilePath.EndsWith(".txt"))
          //Warcraft 3 maps use CR line endings, so this ensures that those line endings are replaced with CLRF.
          File.WriteAllLines(fullOutputFilePath, File.ReadLines(fullSourceFilePath));
        else
          File.Copy(fullSourceFilePath, fullOutputFilePath, true);
      }
    }
    
    private static void CopyGameInterface(string baseMapPath, TriggerStringDictionary triggerStringDictionary, string outputFolderPath)
    {
      if (!File.Exists($"{baseMapPath}/{GameInterfacePath}")) 
        return;
      var subtitutedText = triggerStringDictionary.SubstituteTriggerStringsInText(File.ReadAllText($"{baseMapPath}/{GameInterfacePath}"));
      File.WriteAllText($@"{outputFolderPath}\{GameInterfacePath}", subtitutedText);
    }
    
    /// <summary>
    /// Converts the provided input into a Data Transfer Object, then serializes it, then writes it.
    /// </summary>
    private void SerializeAndWrite<TInput, TDataTransferObject>(TInput inputValue, string outputDirectoryPath, string fileName)
    {
      if (!Directory.Exists(outputDirectoryPath))
        Directory.CreateDirectory(outputDirectoryPath);
      
      var dataTransferObject = _mapper.Map<TInput, TDataTransferObject>(inputValue);
      var asJson = JsonSerializer.Serialize(dataTransferObject, _jsonSerializerOptions);
      var fullPath = Path.Combine(outputDirectoryPath, fileName);
      File.WriteAllText(fullPath, asJson);
    }

    private void SerializeAndWritePreviewIcons(MapPreviewIcons mapPreviewIcons, string path)
    {
      var dto = _mapper.Map<MapPreviewIcons, MapPreviewIconsDto>(mapPreviewIcons);
      dto.Icons = dto.Icons
        .OrderByDescending(x => x.IconType)
        .ThenByDescending(x => x.X)
        .ThenByDescending(x => x.Y)
        .ToList();
      var asJson = JsonSerializer.Serialize(dto, _jsonSerializerOptions);
      File.WriteAllText(path, asJson);
    }
    
    private void SerializeAndWriteMapInfo(MapInfo mapInfo, MapInfoMapper mapInfoMapper, string path)
    {
      var dto = mapInfoMapper.MapToDto(mapInfo);
      var asJson = JsonSerializer.Serialize(dto, _jsonSerializerOptions);
      File.WriteAllText(path, asJson);
    }
    
    private void SerializeAndWriteUnits(MapUnits units, string path)
    {
      if (!Directory.Exists(path))
        Directory.CreateDirectory(path);
      
      var positionallyChunkedUnitCollection = new PositionallyChunkedUnitSet(ChunkSize);

      foreach (var unit in units.Units)
      {
        var asDto = _mapper.Map<UnitDataDto>(unit);
        positionallyChunkedUnitCollection.Add(asDto);
      }

      foreach (var (chunk, unitSet) in positionallyChunkedUnitCollection.GetAll())
      {
        var fullPath = Path.Combine(path, $"{chunk.Item1}_{chunk.Item2}.json");
        var asJson = JsonSerializer.Serialize(unitSet, _jsonSerializerOptions);
        File.WriteAllText(fullPath, asJson);
      }
    }
    
    private void SerializeAndWriteDoodads(MapDoodads doodads, string path)
    {
      if (!Directory.Exists(path))
        Directory.CreateDirectory(path);
      
      var positionallyChunkedUnitCollection = new PositionallyChunkedDoodadSet(ChunkSize);

      foreach (var doodad in doodads.Doodads)
      {
        var asDto = _mapper.Map<DoodadDataDto>(doodad);
        positionallyChunkedUnitCollection.Add(asDto);
      }

      foreach (var (chunk, doodadSet) in positionallyChunkedUnitCollection.GetAll())
      {
        var fullPath = Path.Combine(path, $"{chunk.Item1}_{chunk.Item2}.json");
        var asJson = JsonSerializer.Serialize(doodadSet, _jsonSerializerOptions);
        File.WriteAllText(fullPath, asJson);
      }
    }
    
    private void SerializeAndWriteSounds(MapSounds sounds, string path)
    {
      foreach (var sound in sounds.Sounds)
        SerializeAndWrite<Sound, SoundDto>(sound, path, $"{sound.Name.Remove(0, 7)}.json");
    }
    
    private void SerializeAndWriteRegions(MapRegions regions, string path)
    {
      foreach (var region in regions.Regions)
        SerializeAndWrite<Region, RegionDto>(region, path, $"{region.Name}.json");
    }
    
    private void SerializeAndWriteUnitData(UnitObjectData unitObjectData, ObjectDataMapper objectDataMapper,
      bool substituteTriggerStrings, params string[] paths)
    {
      var dto = objectDataMapper.MapToDto(unitObjectData, substituteTriggerStrings);
      
      foreach (var unit in dto.BaseUnits) 
        SerializeAndWriteSimpleObjectModification(unit, Path.Combine(paths));
      
      foreach (var unit in dto.NewUnits) 
        SerializeAndWriteSimpleObjectModification(unit, Path.Combine(paths));
    }
    
    private void SerializeAndWriteBuffData(BuffObjectData buffObjectData, ObjectDataMapper objectDataMapper,
      bool substituteTriggerStrings, params string[] paths)
    {
      var dto = objectDataMapper.MapToDto(buffObjectData, substituteTriggerStrings);
      
      foreach (var buff in dto.BaseBuffs) 
        SerializeAndWriteSimpleObjectModification(buff, Path.Combine(paths));
      
      foreach (var buff in dto.NewBuffs) 
        SerializeAndWriteSimpleObjectModification(buff, Path.Combine(paths));
    }

    private void SerializeAndWriteDoodadData(DoodadObjectData doodadObjectData, ObjectDataMapper objectDataMapper,
      bool substituteTriggerStrings, params string[] paths)
    {
      var dto = objectDataMapper.MapToDto(doodadObjectData, substituteTriggerStrings);
      
      foreach (var doodad in dto.BaseDoodads) 
        SerializeAndWriteVariationObjectModification(doodad, Path.Combine(paths));
      
      foreach (var doodad in dto.NewDoodads) 
        SerializeAndWriteVariationObjectModification(doodad, Path.Combine(paths));
    }

    private void SerializeAndWriteDestructableData(DestructableObjectData destructableObjectData,
      ObjectDataMapper objectDataMapper, bool substituteTriggerStrings, params string[] paths)
    {
      var dto = objectDataMapper.MapToDto(destructableObjectData, substituteTriggerStrings);
      
      foreach (var destructable in dto.BaseDestructables) 
        SerializeAndWriteSimpleObjectModification(destructable, Path.Combine(paths));
      
      foreach (var destructable in dto.NewDestructables) 
        SerializeAndWriteSimpleObjectModification(destructable, Path.Combine(paths));
    }

    private void SerializeAndWriteItemData(ItemObjectData itemObjectData, ObjectDataMapper objectDataMapper,
      bool substituteTriggerStrings, params string[] paths)
    {
      var dto = objectDataMapper.MapToDto(itemObjectData, substituteTriggerStrings);
      
      foreach (var item in dto.BaseItems) 
        SerializeAndWriteSimpleObjectModification(item, Path.Combine(paths));
      
      foreach (var item in dto.NewItems) 
        SerializeAndWriteSimpleObjectModification(item, Path.Combine(paths));
    }

    private void SerializeAndWriteAbilityData(AbilityObjectData abilityObjectData, ObjectDataMapper objectDataMapper,
      bool substituteTriggerStrings, params string[] paths)
    {
      var dto = objectDataMapper.MapToDto(abilityObjectData, substituteTriggerStrings);
      
      foreach (var ability in dto.BaseAbilities)
        SerializeAndWriteLevelObjectModification(ability, Path.Combine(paths));
      
      foreach (var ability in dto.NewAbilities)
        SerializeAndWriteLevelObjectModification(ability, Path.Combine(paths));
    }
    
    private void SerializeAndWriteUpgradeData(UpgradeObjectData upgradeObjectData, ObjectDataMapper objectDataMapper,
      bool substituteTriggerStrings, params string[] paths)
    {
      var dto = objectDataMapper.MapToDto(upgradeObjectData, substituteTriggerStrings);
      
      foreach (var upgrade in dto.BaseUpgrades)
        SerializeAndWriteLevelObjectModification(upgrade, Path.Combine(paths));
      
      foreach (var upgrade in dto.NewUpgrades)
        SerializeAndWriteLevelObjectModification(upgrade, Path.Combine(paths));
    }
    
    private void SerializeAndWriteSimpleObjectModification(SimpleObjectModification simpleObject, string outputDirectoryPath)
    {
      if (!Directory.Exists(outputDirectoryPath))
        Directory.CreateDirectory(outputDirectoryPath);
      
      var id = simpleObject.NewId > 0 ? simpleObject.NewId : simpleObject.OldId;
      var asJson = JsonSerializer.Serialize(simpleObject, _jsonSerializerOptions);
      var fullPath = Path.Combine(outputDirectoryPath, $"{id}.json");
      File.WriteAllText(fullPath, asJson);
    }
    
    private void SerializeAndWriteVariationObjectModification(VariationObjectModification variationObject, string outputDirectoryPath)
    {
      if (!Directory.Exists(outputDirectoryPath))
        Directory.CreateDirectory(outputDirectoryPath);
      
      var id = variationObject.NewId > 0 ? variationObject.NewId : variationObject.OldId;
      var asJson = JsonSerializer.Serialize(variationObject, _jsonSerializerOptions);
      var fullPath = Path.Combine(outputDirectoryPath, $"{id}.json");
      File.WriteAllText(fullPath, asJson);
    }
    
    private void SerializeAndWriteLevelObjectModification(LevelObjectModification levelObjectModification, string outputDirectoryPath)
    {
      if (!Directory.Exists(outputDirectoryPath))
        Directory.CreateDirectory(outputDirectoryPath);
      
      var id = levelObjectModification.NewId > 0 ? levelObjectModification.NewId : levelObjectModification.OldId;
      var asJson = JsonSerializer.Serialize(levelObjectModification, _jsonSerializerOptions);
      var fullPath = Path.Combine(outputDirectoryPath, $"{id}.json");
      File.WriteAllText(fullPath, asJson);
    }
    
    private static void ApplyMigrations(Map map)
    {
      var objectDatabase = map.GetObjectDatabaseFromMap();
      foreach (var migration in MapMigrationProvider.GetMapMigrations())
        migration.Migrate(map, objectDatabase);
    }
  }
}