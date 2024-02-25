using MacroTools.Extensions;
using MacroTools.PassiveAbilitySystem;
using MacroTools.Sound;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.FactionMechanics.Goblins
{
  /// <summary>
  /// When the unit issues an order, it warns everyone about an incoming nuke.
  /// </summary>
  public sealed class NuclearLaunchWarning : PassiveAbility
  {
    private readonly float _castTime;
    private readonly int _nuclearWarningUnitTypeId;
    private readonly string _warningSoundPath;
    private bool _warningActive;

    /// <summary>
    /// Initializes a new instance of the <see cref="NuclearLaunchWarning"/> class.
    /// </summary>
    /// <param name="unitTypeId"><inheritdoc/></param>
    /// <param name="nuclearWarningUnitTypeId">Unit type ID for the unit to be used as a warning indicator.</param>
    /// <param name="warningSoundPath">File path to the warning sound.</param>
    /// <param name="castTime">How long the spell takes to cast.</param>
    public NuclearLaunchWarning(int unitTypeId, int nuclearWarningUnitTypeId, string warningSoundPath, float castTime) : base(unitTypeId)
    {
      _nuclearWarningUnitTypeId = nuclearWarningUnitTypeId;
      _warningSoundPath = warningSoundPath;
      _castTime = castTime;
    }

    /// <inheritdoc />
    public override void OnOrderIssued()
    {
      if (_warningActive)
        return;

      var caster = GetTriggerUnit();
      var targetPoint = new Point(GetOrderPointX(), GetOrderPointY());
      var sound = SoundUtils.CreateNormalSound(_warningSoundPath);
      StartSound(sound);
      var dummyNukeWarning =
        CreateUnit(caster.OwningPlayer(), _nuclearWarningUnitTypeId, targetPoint.X, targetPoint.Y, 0);
      
      UnitApplyTimedLife(dummyNukeWarning, 0, _castTime);
      BlockLaunchWarningAnimation();
    }

    private void BlockLaunchWarningAnimation()
    {
      _warningActive = true;
      TimerStart(CreateTimer(), _castTime, false, () =>
      {
        _warningActive = false;
        DestroyTimer(GetExpiredTimer());
      });
    }
  }
}