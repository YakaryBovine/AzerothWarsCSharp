using MacroTools.Extensions;
using MacroTools.QuestSystem;
using static War3Api.Common;

namespace MacroTools.ObjectiveSystem.Objectives.UnitBased
{
  /// <summary>
  /// An <see cref="Objective"/> in which the objective holder must kill a specific unit.
  /// </summary>
  public sealed class ObjectiveKillUnit : Objective
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="ObjectiveKillUnit"/> class.
    /// </summary>
    public ObjectiveKillUnit(unit unitToKill)
    {
      var trig = CreateTrigger();
      TriggerRegisterUnitEvent(trig, unitToKill, EVENT_UNIT_DEATH);
      TriggerAddAction(trig, OnUnitDeath);
      Target = unitToKill;
      TargetWidget = Target;
      InitializeDescription();
      DisplaysPosition = IsUnitType(Target, UNIT_TYPE_STRUCTURE) ||
                         GetOwningPlayer(Target) == Player(PLAYER_NEUTRAL_AGGRESSIVE);
      
      Position = new(GetUnitX(Target), GetUnitY(Target));
    }
    
    /// <summary>
    /// The unit that has to die for the objective to be completed.
    /// </summary>
    public unit Target { get; }

    private void OnUnitDeath()
    {
      Progress = IsPlayerOnSameTeamAsAnyEligibleFaction(GetKillingUnit().OwningPlayer())
        ? QuestProgress.Complete
        : QuestProgress.Failed;
    }

    private void InitializeDescription()
    {
      var killVerb = IsUnitType(Target, UNIT_TYPE_STRUCTURE) || IsUnitType(Target, UNIT_TYPE_ANCIENT)
        ? "Destroy"
        : "Kill";

      Description = $"{killVerb} {Target.GetProperName()}";
    }
  }
}