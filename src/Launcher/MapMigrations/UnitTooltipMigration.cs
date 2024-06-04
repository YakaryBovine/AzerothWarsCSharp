using System;
using System.Linq;
using System.Text;
using Launcher.Extensions;
using War3Api.Object;
using War3Net.Build;
using WarcraftLegacies.Shared.Config.Factions;

namespace Launcher.MapMigrations
{
  /// <summary>
  /// Sets all unit tooltips in the game.
  /// </summary>
  public sealed class UnitTooltipMigration : IMapMigration
  {
    private const string LineSeperator = "|n";
    private const string AbilitiesKnown = "|cfff5962dAbilities:|r ";
    private const string AbilitiesLearnable = "|cfff5962dAbilities (unlockable):|r ";
    private const string HeroAbilitiesKnown = "|cfff5962dAbilities (hero):|r ";
    private const string UnitsTrained = "|cfff5962dTrains:|r ";
    private const string UnlockableUnitsTrained = "|cfff5962dTrains (unlockable):|r ";
    private const string ResearchesAvailable = "|cfff5962dResearches:|r ";
    private const string UpgradesTo = "|cfff5962dUpgrades to:|r ";
    private const string ItemsSold = "|cfff5962dSells items:|r ";
    private const string UnitsSold = "|cfff5962dSells units:|r ";

    private readonly ObjectLimitRepository _objectLimitRepository = new();

    /// <inheritdoc />
    public void Migrate(Map map, ObjectDatabase objectDatabase)
    {
      var units = objectDatabase.GetUnits();
      var copiedUnits = units.ToList();
      foreach (var unit in copiedUnits)
      {
        try
        {
          DetermineTooltip(unit);
        }
        catch (Exception ex)
        {
          Console.WriteLine($"Failed to apply tooltip migration for {unit.TextName}: {ex}");
        }
      }

      var unitData = objectDatabase.GetAllData().UnitData;
      map.UnitObjectData = unitData;
      map.UnitSkinObjectData = unitData;
    }

    private void DetermineTooltip(Unit unit)
    {
      var tooltipBuilder = new StringBuilder();

      tooltipBuilder.Append(unit.TextTooltipExtended);
      AppendInnateUnitsTrained(tooltipBuilder, unit);
      AppendUnlockableUnitsTrained(tooltipBuilder, unit);
      AppendResearchesAvailable(tooltipBuilder, unit);
      AppendUpgradesTo(tooltipBuilder, unit);
      AppendInnateAbilities(tooltipBuilder, unit);
      AppendLearnedAbilities(tooltipBuilder, unit);
      AppendHeroAbilities(tooltipBuilder, unit);
      AppendSoldItems(tooltipBuilder, unit);
      AppendUnitsSold(tooltipBuilder, unit);
      AppendObjectLimit(tooltipBuilder, unit);
      AppendTargetsAllowed(tooltipBuilder, unit);
      
      var extendedTooltip = tooltipBuilder.ToString();
      if (unit.TextTooltipExtended != extendedTooltip)
        unit.TextTooltipExtended = extendedTooltip;
    }

    private static void AppendInnateAbilities(StringBuilder tooltipBuilder, Unit unit)
    {
      if (!unit.IsAbilitiesNormalModified) return;
      var innateAbilities = unit.AbilitiesNormal
        .Where(x => x.HasVisibleIcon())
        .Where(x => !x.TechtreeRequirements.Any())
        .OrderBy(AbilityExtensions.GetPriority)
        .Select((x) => x.TextName)
        .ToArray();
      if (innateAbilities.Any())
      {
        tooltipBuilder.Append($"{LineSeperator}{AbilitiesKnown}{string.Join(", ", innateAbilities)}");
      }
    }
    
    private static void AppendLearnedAbilities(StringBuilder tooltipBuilder, Unit unit)
    {
      if (!unit.IsAbilitiesNormalModified) return;
      var learnableAbilities = unit.AbilitiesNormal
        .Where(x => x.TechtreeRequirements.Any())
        .OrderBy(AbilityExtensions.GetPriority)
        .Select(x => x.TextName)
        .ToArray();
      if (learnableAbilities.Any())
      {
        tooltipBuilder.Append($"{LineSeperator}{AbilitiesLearnable}{string.Join(", ", learnableAbilities)}");
      }
    }

    private static void AppendHeroAbilities(StringBuilder tooltipBuilder, Unit unit)
    {
      if (!unit.IsAbilitiesHeroModified) return;
      var heroAbilities = unit.AbilitiesHero
        .OrderBy(AbilityExtensions.GetPriority)
        .Select((x) => x.TextName)
        .ToArray();
      
      if (heroAbilities.Any())
        tooltipBuilder.Append($"{LineSeperator}{HeroAbilitiesKnown}{string.Join(", ", heroAbilities)}");
    }
    
    private static void AppendUnitsSold(StringBuilder tooltipBuilder, Unit unit)
    {
      if (!unit.IsTechtreeUnitsSoldModified) 
        return;
      
      var unitsSold = unit.TechtreeUnitsSold
        .OrderBy(x => x.GetPriority())
        .Select(x => x.TextName)
        .ToArray();
      
      if (unitsSold.Any())
        tooltipBuilder.Append($"{LineSeperator}{UnitsSold}{string.Join(", ", unitsSold)}");
    }
    
    private static void AppendInnateUnitsTrained(StringBuilder tooltipBuilder, Unit unit)
    {
      var unitsTrained = unit.TechtreeUnitsTrained
        .Where(x => !x.TechtreeRequirements.Any(x1 => x1.IsUpgrade()))
        .OrderBy(x => x.GetPriority())
        .Select(GetBestName)
        .ToArray();
      
      if (unitsTrained.Any()) 
        tooltipBuilder.Append($"{LineSeperator}{UnitsTrained}{string.Join(", ", unitsTrained)}");
    }
    
    private static void AppendUnlockableUnitsTrained(StringBuilder tooltipBuilder, Unit unit)
    {
      var unitsTrained = unit.TechtreeUnitsTrained
        .Where(x => x.TechtreeRequirements.Any(x1 => x1.IsUpgrade()))
        .OrderBy(x => x.GetPriority())
        .Select(GetBestName)
        .ToArray();
      
      if (unitsTrained.Any()) 
        tooltipBuilder.Append($"{LineSeperator}{UnlockableUnitsTrained}{string.Join(", ", unitsTrained)}");
    }
    
    private static void AppendResearchesAvailable(StringBuilder tooltipBuilder, Unit unit)
    {
      var researchesAvailable = unit.TechtreeResearchesAvailable
        .OrderBy(x => x.GetPriority())
        .Select(x => x.GetTextName())
        .ToArray();
      
      if (researchesAvailable.Any())
      {
        tooltipBuilder.Append($"{LineSeperator}{ResearchesAvailable}{string.Join(", ", researchesAvailable)}");
      }
    }
    
    private static void AppendUpgradesTo(StringBuilder tooltipBuilder, Unit unit)
    {
      var upgradesTo = unit.TechtreeUpgradesTo
        .OrderBy(x => x.GetPriority())
        .Select(x => x.TextName)
        .ToArray();
      
      if (upgradesTo.Any())
      {
        tooltipBuilder.Append($"{LineSeperator}{UpgradesTo}{string.Join(", ", upgradesTo)}");
      }
    }
    
    private static void AppendSoldItems(StringBuilder tooltipBuilder, Unit unit)
    {
      var soldItems = unit.GetTechtreeItemsSoldAndMade()
        .OrderBy(x => x.GetPriority())
        .Select(x => x.TextName)
        .ToArray();
      
      if (soldItems.Any())
      {
        tooltipBuilder.Append($"{LineSeperator}{ItemsSold}{string.Join(", ", soldItems)}");
      }
    }

    private void AppendObjectLimit(StringBuilder tooltipBuilder, Unit unit)
    {
      if (unit.IsAbilitiesHeroModified && unit.AbilitiesHero.Any())
        return;
      
      var unitId = unit.NewId != 0 ? unit.NewId : unit.OldId;
      if (!_objectLimitRepository.TryGetObjectLimit(unitId, out var limit)) 
        return;
      
      var isABuilding = unit.StatsIsABuilding;
      var trainType = isABuilding ? "build" : "train";
      
      if (limit is > 0 and < 200)
      {
        tooltipBuilder.Append($"{LineSeperator}|cff99b4d1Can only {trainType} {limit}.|r");
      }
    }
    
    private static void AppendTargetsAllowed(StringBuilder tooltipBuilder, Unit unit)
    {
      var targetsAllowed = unit.GetAllTargetsAllowed();

      if (!targetsAllowed.Any())
        return;

      tooltipBuilder.Append(LineSeperator + LineSeperator);
      
      if (targetsAllowed.CanTargetGround())
      {
        tooltipBuilder.Append(targetsAllowed.CanTargetAir()
          ? "|cffffcc00Attacks land and air units.|r"
          : "|cffffcc00Attacks land units.|r");
      }
    }

    /// <summary>
    /// Gets the best name for a unit, preferring hero proper name over their text name.
    /// </summary>
    private static string GetBestName(Unit unit)
    {
      try
      {
        if (unit.TextProperNames.Any())
          return unit.TextProperNames.First();
      }
      catch
      {
        //do nothing
      }
      return unit.TextName;
    }
  }
}