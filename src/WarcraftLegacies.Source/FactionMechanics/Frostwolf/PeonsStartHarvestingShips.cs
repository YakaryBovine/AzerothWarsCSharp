using MacroTools;
using MacroTools.Extensions;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.FactionMechanics.Frostwolf
{
  /// <summary>
  /// At the start of the game, Frostwolf has a Peon harvesting lumber from their ships to indicate that this is possible.
  /// </summary>
  public static class PeonsStartHarvestingShips
  {
    /// <summary>
    /// Sets up <see cref="PeonsStartHarvestingShips"/>.
    /// </summary>
    public static void Setup(PreplacedUnitSystem preplacedUnitSystem)
    {
      var shipPosA = new Point(-8757, -10599);
      var shipPosB = new Point(-8516, -10785);

      preplacedUnitSystem.GetUnit(Constants.UNIT_OPEO_PEON_FROSTWOLF_WARSONG_WORKER, shipPosA)
        .IssueOrder("harvest", preplacedUnitSystem.GetDestructable(FourCC("B00E"), shipPosA));
      
      preplacedUnitSystem.GetUnit(Constants.UNIT_OPEO_PEON_FROSTWOLF_WARSONG_WORKER, shipPosB)
        .IssueOrder("harvest", preplacedUnitSystem.GetDestructable(FourCC("B00E"), shipPosB));
    }
  }
}