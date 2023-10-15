using MacroTools;
using MacroTools.Extensions;
using WCSharp.Events;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Setup
{
  /// <summary>
  /// When a Black Portal Control Nexus is captured, transfer the ownership of the Dark Portal portals to the capturing player.
  /// </summary>
  public static class BlackPortalControlNexusSetup
  {
    /// <summary>
    /// Sets up <see cref="BlackPortalControlNexusSetup"/>.
    /// </summary>
    public static void Setup(PreplacedUnitSystem preplacedUnitSystem)
    {
      foreach (var controlNexus in preplacedUnitSystem.GetUnits(Constants.UNIT_N03J_BLACK_PORTAL_AURA_CONTROL_NEXUS))
        controlNexus.MakeCapturable();

      //Control Nexus in Northrend
      PlayerUnitEvents.Register(UnitEvent.ChangesOwner, () =>
      {
        var newOwner = GetTriggerUnit().OwningPlayer();
        preplacedUnitSystem.GetUnit(Constants.UNIT_N03H_DEATH_GATE_WAYGATE, Regions.Scholomance_Exterior_1.Center).SetOwner(newOwner);
        preplacedUnitSystem.GetUnit(Constants.UNIT_N03H_DEATH_GATE_WAYGATE, Regions.Scholomance_Exterior_2.Center).SetOwner(newOwner);
      }, preplacedUnitSystem.GetUnit(Constants.UNIT_N03J_BLACK_PORTAL_AURA_CONTROL_NEXUS, new Point(-4137, 16957)));

      //Control Nexus in NA
      PlayerUnitEvents.Register(UnitEvent.ChangesOwner, () =>
      {
        var newOwner = GetTriggerUnit().OwningPlayer();
        preplacedUnitSystem.GetUnit(Constants.UNIT_N03H_DEATH_GATE_WAYGATE, Regions.Wrathgate_Portal_1.Center).SetOwner(newOwner);
        preplacedUnitSystem.GetUnit(Constants.UNIT_N03H_DEATH_GATE_WAYGATE, Regions.Wrathgate_Portal_2.Center).SetOwner(newOwner);
      }, preplacedUnitSystem.GetUnit(Constants.UNIT_N03J_BLACK_PORTAL_AURA_CONTROL_NEXUS, new Point(14198, 6530)));
    }
  }
}