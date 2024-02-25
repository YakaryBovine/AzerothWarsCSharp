using MacroTools.Extensions;
using MacroTools.Libraries;
using WCSharp.Buffs;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.FactionMechanics.Scourge.Plague
{
  /// <summary>
  ///   Causes the buff holder to periodically spawn a Zombie unit nearby.
  /// </summary>
  public sealed class PlagueCauldronBuff : TickingBuff
  {
    private readonly Point _attackTarget;

    public PlagueCauldronBuff(unit caster, unit target, Point attackTarget) : base(caster, target)
    {
      Duration = 3000;
      Interval = 30f;
      _attackTarget = attackTarget;
    }

    public int ZombieUnitTypeId { get; init; }

    public override void OnDispose()
    {
      RemoveUnit(Target);
    }

    public override void OnApply()
    {
      GeneralHelpers.EnumDestructablesInCircle(50, new Point(GetUnitX(Caster), GetUnitY(Caster)),
        () => RemoveDestructable(GetEnumDestructable()));
    }

    public override void OnTick()
    {
      var unit = CreateUnit(GetOwningPlayer(Target), ZombieUnitTypeId, GetUnitX(Target), GetUnitY(Target), 0);
      unit.IssueOrder("attack", _attackTarget);
    }
  }
}