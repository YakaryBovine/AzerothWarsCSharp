using War3Api.Object;
using War3Net.Build;

namespace Launcher.MapMigrations
{
  /// <summary>
  /// Assigns gold bounties based on level to all units in the map.
  /// </summary>
  public sealed class GoldBountyMapMigration : IMapMigration
  {
    public void Migrate(Map map, ObjectDatabase objectDatabase)
    {
      foreach (var unit in objectDatabase.GetUnits())
      {
        var level = unit.StatsLevel;
        
        if (level == 0)
        {
          if (unit.StatsGoldBountyAwardedBase != 0)
            unit.StatsGoldBountyAwardedBase = 0;
          
          if (unit.StatsGoldBountyAwardedNumberOfDice != 0)
            unit.StatsGoldBountyAwardedNumberOfDice = 0;
          
          if (unit.StatsGoldBountyAwardedSidesPerDie != 0)
            unit.StatsGoldBountyAwardedSidesPerDie = 0;
        }
        else
        {
          const int scalingFactor = 4;
          const int numberOfDiceConstant = 1;
          const int maxValueOnDice = 2;
          const int baseConstant = -2;
          var baseGold = level * scalingFactor + baseConstant;

          if (level >= 7)
            baseGold += 5;

          if (unit.StatsHitPointsMaximumBase >= 1500)
            baseGold += 7;

          if (unit.StatsHitPointsMaximumBase >= 2000)
            baseGold += 10;

          if (unit.StatsHitPointsMaximumBase >= 2500)
            baseGold += 15;

          if (unit.StatsGoldBountyAwardedBase != baseGold)
            unit.StatsGoldBountyAwardedBase = baseGold;

          if (unit.StatsGoldBountyAwardedNumberOfDice != level + numberOfDiceConstant)
            unit.StatsGoldBountyAwardedNumberOfDice = level + numberOfDiceConstant;

          if (unit.StatsGoldBountyAwardedNumberOfDice != maxValueOnDice)
            unit.StatsGoldBountyAwardedSidesPerDie = maxValueOnDice;
        }
      }

      map.UnitObjectData = objectDatabase.GetAllData().UnitData;
    }
  }
}