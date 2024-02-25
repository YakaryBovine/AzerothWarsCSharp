using War3Api.Object;
using War3Net.Build;

namespace Launcher.MapMigrations
{
  /// <summary>
  /// Assigns level to all units in the map based on health, attack type and whether it is flying.
  /// </summary>
  public sealed class CreepLevelMapMigration : IMapMigration
  {
    private const int HealthFactor = 200;

    public void Migrate(Map map, ObjectDatabase objectDatabase)
    {
      foreach (var unit in objectDatabase.GetUnits())
      {
        if (!unit.StatsIsABuilding)
        {
          var level = 0;
          var unitHealth = unit.StatsHitPointsMaximumBase;
          var remainder = unitHealth % HealthFactor;

          if (remainder > 0)
            unitHealth -= remainder;

          var healthDividedByFactor = unitHealth / HealthFactor;

          if (healthDividedByFactor < 8)
            level += healthDividedByFactor;
          else
            level += 7;

          switch (unitHealth)
          {
            case >= 3000:
              level += 3;
              break;
            case >= 2400:
              level += 2;
              break;
            case >= 1800:
              level += 1;
              break;
          }

          if (unit.CombatAttack1AttackType.Equals(War3Api.Object.Enums.AttackType.Chaos) ||
              unit.CombatAttack1AttackType.Equals(War3Api.Object.Enums.AttackType.Hero))
            level += 1;

          // is a flying unit
          if (unit.MovementHeight > 100)
            level += 1;

          if (unit.StatsLevel != level)
            unit.StatsLevel = level;
        }
      }

      map.UnitObjectData = objectDatabase.GetAllData().UnitData;
    }
  }
}