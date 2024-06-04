using Launcher.MapMigrations.AbilityMigrations;
using WarcraftLegacies.Shared;
using WarcraftLegacies.Shared.Config.Abilities;

namespace Launcher.MapMigrations
{
  public static class MapMigrationProvider
  {
    public static IMapMigration[] GetMapMigrations()
    {
      return new IMapMigration[]
      {
        new ControlPointMapMigration(),
        new CreepLevelMapMigration(),
        new GoldBountyMapMigration(),
        new FlightMigration(),
        new UnitTooltipMigration(),
        new SpawnTentacleMigration(Constants.ABILITY_ZBST_SPAWN_TENTACLE_C_THUN, SpawnTentacleFactory.CthunSpawnTentacle())
      };
    }
  }
}