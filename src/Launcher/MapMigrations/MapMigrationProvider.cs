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
        new GoldBountyMapMigration()
      };
    }
  }
}