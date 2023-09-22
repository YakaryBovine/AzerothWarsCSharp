using MacroTools.FactionSystem;

namespace WarcraftLegacies.Source.Setup
{
  /// <summary>
  /// Responsible for registering all researches shared by all <see cref="Faction"/>s.
  /// </summary>
  public static class SharedFactionConfigSetup
  {
    /// <summary>
    /// Sets up <see cref="SharedFactionConfigSetup"/>.
    /// </summary>
    public static void Setup()
    {
      foreach (var faction in FactionManager.GetAllFactions())
      {
        faction.ModObjectLimit(Constants.UPGRADE_RHME_PYRITE_FORGED_WEAPONRY_UNIVERSAL_UPGRADE, 8);
        faction.ModObjectLimit(Constants.UPGRADE_RHAR_PYRITE_ARMOR_PLATING_UNIVERSAL_UPGRADE, 8);
        faction.ModObjectLimit(Constants.UPGRADE_R00K_POWER_INFUSION_4_SHARED, Faction.UNLIMITED);
        faction.ModObjectLimit(Constants.UPGRADE_R04R_FORTIFIED_HULLS_UNIVERSAL_UPGRADE, Faction.UNLIMITED); //Actually Full Scale Escalation
        faction.ModObjectLimit(Constants.UPGRADE_R00C_IMPROVED_CANNONS_ALL_TEAMS, Faction.UNLIMITED);
        faction.ModObjectLimit(Constants.UPGRADE_R006_FORTIFIED_HULLS_UNIVERSAL_UPGRADE, Faction.UNLIMITED);
        faction.ModObjectLimit(Constants.UPGRADE_R07L_FORTIFIED_HULLS, Faction.UNLIMITED); //path of the old gods
      }
    }
  }
}