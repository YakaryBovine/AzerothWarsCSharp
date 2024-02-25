using System.Collections.Generic;
using WarcraftLegacies.Source.FactionMechanics.Scourge.Plague;
using WCSharp.Shared.Data;

namespace WarcraftLegacies.Source.Quests.Scourge
{
  /// <summary>
  /// Provides information about how the Plague should work.
  /// </summary>
  public sealed class PlagueParameters
  {
    /// <summary>
    /// Where to spawn Plague Cauldrons. Each <see cref="Rectangle"/> gets 1 Cauldron.
    /// </summary>
    public List<Rectangle> PlagueRects { get; set; } = new();

    /// <summary>
    /// The unit type ID for Plague Cauldrons.
    /// </summary>
    public int PlagueCauldronUnitTypeId { get; set; }

    /// <summary>
    /// Which units to spawn around Plague Cauldrons and how many.
    /// </summary>
    public List<PlagueCauldronSummonParameter> PlagueCauldronSummonParameters { get; set; } = new();

    /// <summary>
    /// How long the spawned Plague Cauldrons should last.
    /// </summary>
    public float Duration { get; set; }

    /// <summary>
    /// List of potential points plague cauldrons can send units too
    /// </summary>
    public List<Point> AttackTargets { get; set; } = new();
  }
}