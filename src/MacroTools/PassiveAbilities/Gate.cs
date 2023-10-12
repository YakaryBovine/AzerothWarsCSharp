using MacroTools.Extensions;
using MacroTools.PassiveAbilitySystem;
using static War3Api.Common;

namespace MacroTools.PassiveAbilities
{
  /// <summary>
  /// Gates are buildings that can open and close.
  /// </summary>
  public sealed class Gate : PassiveAbility
  {
    /// <summary>Gates will gain this many hit points, as a percentage of their maximum, per turn.</summary>
    public const float HitPointPercentagePerTurn = 0.05f;
    
    private readonly int _openedId;
    private readonly int _deadId;
    
    /// <summary>
    /// Constructs a new <see cref="Gate"/>.
    /// </summary>
    /// <param name="openedId">The unit type ID of the gate while open.</param>
    /// <param name="closedId">The unit type ID of the gate while closed.</param>
    /// <param name="deadId">The unit type ID of the gate while dead.</param>
    public Gate(int openedId, int closedId, int deadId) : base(new[]{openedId, closedId, deadId})
    {
      _openedId = openedId;
      _deadId = deadId;
    }

    /// <inheritdoc/>
    public override void OnDeath()
    {
      var dyingGate = GetTriggerUnit();
      var dyingGatePos = dyingGate.GetPosition();
      var dyingGateFacing = GetUnitFacing(dyingGate);
      dyingGate.Remove();
      CreateUnit(GetOwningPlayer(GetKillingUnit()), _deadId, dyingGatePos.X, dyingGatePos.Y, dyingGateFacing)
        .SetAnimation("death");
    }

    /// <inheritdoc/>
    public override void OnSpellFinish()
    {
      if (GetTriggerUnit().GetTypeId() == _openedId) 
        GetTriggerUnit().SetAnimation("death alternate");
    }
    
    /// <inheritdoc/>
    public override void OnCreated(unit createdUnit)
    {
      if (createdUnit.GetTypeId() == _openedId) 
        createdUnit.SetAnimation("death alternate");
      TurnBasedHitpointsManager.Register(createdUnit, HitPointPercentagePerTurn);
    }

    /// <inheritdoc />
    public override void OnCancelUpgrade() => 
      GetTriggerUnit().SetAnimation("death");
  }
}