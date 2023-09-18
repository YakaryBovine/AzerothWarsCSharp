using System;
using MacroTools.ChannelSystem;
using MacroTools.Extensions;
using MacroTools.Libraries;
using WCSharp.Lightnings;
using static War3Api.Common;

namespace MacroTools.Channels
{
  /// <summary>
  /// Periodically transfer hitpoints and mana from the target to the caster.
  /// </summary>
  public sealed class SiphoningRitualChannel : Channel
  {
    private readonly unit _target;
    private Lightning? _lightning;

    /// <summary>
    /// How many hit points are transferred from the target to the caster per second.
    /// </summary>
    public float LifeDrainedPerSecond { get; init; }

    /// <summary>
    /// How much mana is transferred from the target to the caster per second.
    /// </summary>
    public float ManaDrainedPerSecond { get; init; }

    /// <summary>
    /// Beyond this range, the <see cref="SiphoningRitualChannel"/> is cancelled.
    /// </summary>
    public float Range { get; init; } = float.MaxValue;

    /// <summary>
    /// Initializes a new instance of the <see cref="SiphoningRitualChannel"/> class.
    /// </summary>
    /// <param name="caster"><inheritdoc /></param>
    /// <param name="target">The target having its life force drained.</param>
    /// <param name="spellId"><inheritdoc /></param>
    public SiphoningRitualChannel(unit caster, unit target, int spellId) : base(caster, spellId)
    {
      _target = target;
    }

    /// <inheritdoc />
    public override void OnCreate()
    {
      _lightning = new Lightning("DRAB", Caster, _target)
      {
        Duration = float.MaxValue
      };
      LightningSystem.Add(_lightning);
    }

    /// <inheritdoc />
    protected override void OnPeriodic()
    {
      if (MathEx.GetDistanceBetweenPoints(Caster.GetPosition(), _target.GetPosition()) > Range || !UnitAlive(_target))
      {
        Active = false;
        return;
      }
      
      if (GetUnitState(Caster, UNIT_STATE_LIFE) < GetUnitState(Caster, UNIT_STATE_MAX_LIFE) ||
          !IsUnitAlly(_target, GetOwningPlayer(Caster)))
      {
        _target.TakeDamage(Caster, LifeDrainedPerSecond * Interval, ranged: true, damageType: DAMAGE_TYPE_MAGIC);
        Caster.Heal(Math.Min(LifeDrainedPerSecond * Interval, GetUnitState(_target, UNIT_STATE_LIFE)));
      }

      if (GetUnitState(Caster, UNIT_STATE_MANA) < GetUnitState(Caster, UNIT_STATE_MAX_MANA) ||
          !IsUnitAlly(_target, GetOwningPlayer(Caster)))
      {
        Caster.RestoreMana(Math.Min(ManaDrainedPerSecond * Interval, _target.GetMana()));
        _target.RestoreMana(-ManaDrainedPerSecond * Interval);
      }
    }

    /// <inheritdoc />
    protected override void OnDispose()
    {
      if (_lightning != null)
        _lightning.Active = false;
    }
  }
}