using System;
using WCSharp.Events;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace MacroTools.SpellSystem
{
  /// <summary>
  ///   A visible but unselectabe effect on the map that causes some periodic effect around it,
  ///   like a tornado, volcano, or whirlpool.
  /// </summary>
  public abstract class Hazard : IPeriodicDisposableAction
  {
    protected Hazard(unit? caster, float x, float y)
    {
      Position = new Point(x, y);
      Caster = caster;
      Active = true;
    }
    
    protected unit? Caster { get; }

    private readonly float _interval;

    /// <summary>
    /// The location of the <see cref="Hazard"/>.
    /// </summary>
    public Point Position { get; init; }
    
    /// <summary>
    ///   The interval at which the missile will call <see cref="OnPeriodic" />. Leave at default (0) to disable.
    /// </summary>
    public float Interval
    {
      get => _interval;
      init
      {
        _interval = value;
        _intervalLeft = Math.Max(_interval, _intervalLeft);
      }
    }

    /// <summary>
    ///   The time left until the next call to <see cref="OnPeriodic" />.
    /// </summary>
    private float _intervalLeft;

    /// <summary>
    ///   How long until the Hazard disappears.
    /// </summary>
    public float Duration { get; set; } = 1;

    /// <summary>
    ///   Called by the system. Do not call yourself.
    /// </summary>
    public void Action()
    {
      if (Interval > 0) RunInterval();
    }

    /// <inheritdoc />
    public void Dispose()
    {
      Active = false;
      OnDispose();
    }

    /// <inheritdoc />
    public bool Active { get; set; } = true;

    /// <summary>
    /// Fired when the <see cref="Hazard"/> is initially registered.
    /// </summary>
    public virtual void OnCreate()
    {
    }

    /// <summary>
    ///   <para>Override this method if your Hazard has a periodic effect.</para>
    ///   <para>For this to be active, <see cref="Interval" /> must be greater than 0.</para>
    /// </summary>
    protected virtual void OnPeriodic()
    {
    }

    /// <summary>
    ///   Override this method if your hazard has an effect that should trigger when it is destroyed for any reason.
    /// </summary>
    protected virtual void OnDispose()
    {
    }

    /// <summary>
    ///   Runs the Interval related code. Do not call if <see cref="Interval" /> is 0.
    /// </summary>
    private void RunInterval()
    {
      _intervalLeft -= PeriodicEvents.SYSTEM_INTERVAL;
      if (_intervalLeft <= 0)
      {
        _intervalLeft += Interval;
        OnPeriodic();
      }

      Duration -= PeriodicEvents.SYSTEM_INTERVAL;
      if (Duration <= 0) Dispose();
    }
  }
}