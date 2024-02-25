using MacroTools.Extensions;
using MacroTools.Hazards;
using WCSharp.Buffs;
using static War3Api.Common;

namespace WarcraftLegacies.Source.FactionMechanics.Goblins
{
  /// <summary>
  /// Units with this buff harvest oil from <see cref="OilPool"/>s around them.
  /// </summary>
  public sealed class OilHarvesterBuff : TickingBuff
  {
    private readonly OilPool _oilPool;

    /// <summary>
    /// How much oil this buff harvests per second.
    /// </summary>
    public int OilHarvestedPerSecond { get; init; }
    
    /// <summary>
    /// Construct an <see cref="OilUserBuff"/>.
    /// </summary>
    /// <param name="target">The unit with the buff.</param>
    /// <param name="oilPool">The pool that this harvester is harvesting from.</param>
    public OilHarvesterBuff(unit target, OilPool oilPool) : base(target, target)
    {
      _oilPool = oilPool;
      Duration = float.MaxValue;
      Interval = 1f;
    }

    /// <inheritdoc />
    public override void OnApply()
    {
      _oilPool.OilPower.Income += OilHarvestedPerSecond;
      Target
        .SetMaximumMana(_oilPool.OilAmount)
        .SetMana(_oilPool.OilAmount);
    }

    /// <inheritdoc />
    public override void OnDispose() => _oilPool.OilPower.Income -= OilHarvestedPerSecond;

    /// <inheritdoc />
    public override void OnTick()
    {
      if (!_oilPool.Active || _oilPool.OilAmount <= OilHarvestedPerSecond)
      {
        Caster.Kill();
        _oilPool.OilAmount -= OilHarvestedPerSecond;
        _oilPool.OilPower.Amount += OilHarvestedPerSecond;
        _oilPool.Dispose();
      }
      else
      {
        _oilPool.OilAmount -= OilHarvestedPerSecond;
        Target.SetMana(_oilPool.OilAmount);
      }
    }
  }
}