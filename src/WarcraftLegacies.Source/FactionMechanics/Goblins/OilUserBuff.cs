using MacroTools.Powers;
using WCSharp.Buffs;
using static War3Api.Common;

namespace WarcraftLegacies.Source.FactionMechanics.Goblins
{
  /// <summary>
  /// Units with this buff use oil instead of mana.
  /// </summary>
  public sealed class OilUserBuff : PassiveBuff
  {
    private readonly OilPower _oilPower;
    private trigger? _castTrigger;

    /// <summary>
    /// Construct an <see cref="OilUserBuff"/>.
    /// </summary>
    /// <param name="target">The unit with the buff.</param>
    /// <param name="oilPower">The power providing oil to the unit with the buff.</param>
    public OilUserBuff(unit target, OilPower oilPower) : base(target, target)
    {
      _oilPower = oilPower;
      _oilPower.AmountChanged += OnOilAmountChanged;
      Duration = float.MaxValue;
    }

    private void OnOilAmountChanged(object? sender, OilPower oilPower)
    {
      SetUnitState(Target, UNIT_STATE_MANA, _oilPower.Amount);
    }

    private void OnCast()
    {
      var triggerUnit = GetTriggerUnit();
      var triggerSpell = GetSpellAbilityId();
      var manaCost =
        BlzGetUnitAbilityManaCost(triggerUnit, triggerSpell, GetUnitAbilityLevel(triggerUnit, triggerSpell) - 1);
      _oilPower.Amount -= manaCost;
      SetUnitState(Target, UNIT_STATE_MANA, _oilPower.Amount);
    }

    public override void OnApply()
    {
      _castTrigger = CreateTrigger();
      TriggerRegisterUnitEvent(_castTrigger, Target, EVENT_UNIT_SPELL_EFFECT);
      TriggerAddAction(_castTrigger, OnCast);
      SetUnitState(Target, UNIT_STATE_MANA, _oilPower.Amount);
    }

    public override void OnDispose()
    {
      DestroyTrigger(_castTrigger);
      _oilPower.AmountChanged -= OnOilAmountChanged;
    }
  }
}