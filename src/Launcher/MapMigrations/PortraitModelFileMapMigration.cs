using System.Linq;
using War3Api.Object;
using War3Api.Object.Enums;
using War3Net.Build;

namespace Launcher.MapMigrations
{
  /// <summary>
  /// Sets the Art Portrait Model File to an empty string for all units in the map.
  /// </summary>
  public sealed class PortraitModelFileMapMigration : IMapMigration
  {
    public void Migrate(Map map, ObjectDatabase objectDatabase)
    {
      foreach (var unit in objectDatabase.GetUnits())
      {
        unit.ArtPortraitModelFile = "";
      }

      var unitData = objectDatabase.GetAllData().UnitData;
      map.UnitObjectData = unitData;
      map.UnitSkinObjectData = unitData; 
    }
  }
}
