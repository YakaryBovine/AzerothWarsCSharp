using WCSharp.Shared.Data;

namespace WarcraftLegacies.Source.FactionMechanics.Scourge.Blight
{
  /// <summary>
  ///   Dictates how the <see cref="BlightSystem" /> places blight.
  /// </summary>
  public sealed class BlightParameters
  {
    /// <summary>
    ///   How large the patch of blight that appears at the killed unit should be.
    /// </summary>
    public float PrimaryBlightRadius { get; init; } = 512;

    /// <summary>
    ///   How large the random patches of blight should be.
    /// </summary>
    public float RandomBlightRadius { get; init; } = 200;

    /// <summary>
    ///   How many random patches of blight to spawn.
    /// </summary>
    public int RandomBlightCount { get; init; } = 0;

    /// <summary>
    ///   The <see cref="Rectangle" /> in which to randomly place blight.
    /// </summary>
    public Rectangle? RandomBlightRectangle { get; init; }
  }
}