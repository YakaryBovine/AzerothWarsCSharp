using MacroTools.Extensions;
using MacroTools.Powers;
using WCSharp.Buffs;
using WCSharp.Events;
using static War3Api.Common;

namespace WarcraftLegacies.Source.FactionMechanics.Goblins
{
  /// <summary>
  /// Increases the owner's oil income. The owner must have an instance of <see cref="OilPower"/>.
  /// </summary>
  public sealed class OilProducerBuff : PassiveBuff
  {
    private readonly float _incomePerSecond;
    private OilPower? _oilPower;

    /// <summary>
    /// Construct an <see cref="OilUserBuff"/>.
    /// </summary>
    /// <param name="target">The unit with the buff.</param>
    /// <param name="incomePerSecond">How much oil to give the owner per second.</param>
    public OilProducerBuff(unit target, float incomePerSecond) : base(target, target)
    {
      _incomePerSecond = incomePerSecond;
      _oilPower = target.OwningPlayer().GetFaction()?.GetPowerByType<OilPower>();

      if (_oilPower != null)
        _oilPower.AmountChanged += OnOilAmountChanged;

      PlayerUnitEvents.Register(UnitEvent.ChangesOwner, OnChangeOwner, target);
      
      Duration = float.MaxValue;
    }

    private void OnOilAmountChanged(object? sender, OilPower oilPower)
    {
      if (_oilPower != null)
        SetUnitState(Target, UNIT_STATE_MANA, _oilPower.Amount);
    }

    /// <inheritdoc/>
    public override void OnApply()
    {
      if (_oilPower == null) 
        return;
      _oilPower.Income += _incomePerSecond;
      Target.SetMana((int)_oilPower.Amount);
    }

    /// <inheritdoc/>
    public override void OnDispose()
    {
      if (_oilPower == null) 
        return;
      _oilPower.Income -= _incomePerSecond;
      _oilPower.AmountChanged -= OnOilAmountChanged;
      PlayerUnitEvents.Unregister(UnitEvent.ChangesOwner, OnChangeOwner, Target);
    }

    private void OnChangeOwner()
    {
      if (_oilPower != null) 
        _oilPower.Income -= _incomePerSecond;
      _oilPower = Target.OwningPlayer().GetFaction()?.GetPowerByType<OilPower>();
      if (_oilPower != null)
        _oilPower.Income += _incomePerSecond;
    }
  }
}