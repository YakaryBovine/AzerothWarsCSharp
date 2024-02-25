using MacroTools.Extensions;
using MacroTools.Powers;
using MacroTools.SpellSystem;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Spells
{
  /// <summary>
  /// Pings all oil deposits.
  /// </summary>
  public sealed class PingOilDeposits : Spell
  {
    /// <summary>
    /// How long the ping should last.
    /// </summary>
    public float Duration { get; init; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="PingOilDeposits"/> class.
    /// </summary>
    /// <param name="id"><inheritdoc /></param>
    public PingOilDeposits(int id) : base(id)
    {
    }

    /// <inheritdoc />
    public override void OnCast(unit caster, unit target, Point targetPoint)
    {
      var oilPower = GetTriggerUnit().OwningPlayer().GetFaction()?.GetPowerByType<OilPower>();
      if (oilPower == null) 
        return;
      foreach (var oilDeposit in oilPower.GetAllOilPools())
        caster.OwningPlayer().PingMinimapSimple(oilDeposit.Position.X, oilDeposit.Position.Y, Duration);
    }
  }
}