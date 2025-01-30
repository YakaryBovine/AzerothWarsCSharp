using System;
using System.Linq;
using War3Api.Object;
using War3Api.Object.Enums;
using War3Net.Build;
using War3Net.Build.Widget;
using War3Net.CodeAnalysis.Jass.Extensions;

namespace Launcher.MapMigrations
{
  /// <summary>
  /// Forces all flying units to require the Flight research.
  /// </summary>
  public sealed class FlightMigration : IMapMigration
  {
    /// <inheritdoc />
    public void Migrate(Map map, ObjectDatabase objectDatabase)
    {
      var units = objectDatabase.GetUnits().ToList();
      var flight = objectDatabase.GetUpgrade(Constants.UPGRADE_R09X_FORTIFIED_HULLS_UNIVERSAL_UPGRADE.InvertEndianness());

      foreach (var unit in units)
      {
        try
        {
          AddFlightResearch(unit, new Tech(flight));
        }
        catch (Exception)
        {
          //ignore
        }
      }
      var unitData = objectDatabase.GetAllData().UnitData;
      map.UnitObjectData = unitData;
      map.UnitSkinObjectData = unitData;

    }

    private static void AddFlightResearch(Unit unit, Tech flight)
    {
      if (unit.MovementType is not MoveType.Fly)
        return;

      if (unit.TechtreeRequirements.Any(x => x.Key == flight.Key))
        return;

      unit.TechtreeRequirements = unit.TechtreeRequirements.Append(flight);
    }
  }
}
