using System.Linq;
using MacroTools.Extensions;
using MacroTools.QuestSystem;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace MacroTools.ObjectiveSystem.Objectives.UnitBased
{
  /// <summary>
  /// Completed when an eligible player moves a unit into the specified <see cref="Rectangle"/>.
  /// </summary>
  public sealed class ObjectiveAnyUnitInRect : Objective, IHasCompletingUnit
  {
    private readonly bool _heroOnly;
    private readonly rect _targetRect;

    /// <summary>
    /// Initializes a new instance of the <see cref="ObjectiveAnyUnitInRect"/> class.
    /// </summary>
    /// <param name="targetRect">Where the player has to move a unit.</param>
    /// <param name="rectName">A user-friendly name for the area.</param>
    /// <param name="heroOnly">If true, can only be completed with a hero.</param>
    public ObjectiveAnyUnitInRect(Rectangle targetRect, string rectName, bool heroOnly)
    {
      _targetRect = targetRect.Rect;
      Description = heroOnly ? $"You have a hero at {rectName}" : $"You have a unit at {rectName}";
      _heroOnly = heroOnly;
      DisplaysPosition = true;
      PingPath = "MinimapQuestTurnIn";

      CreateTrigger()
        .RegisterEnterRegion(targetRect)
        .AddAction(() =>
        {
          var triggerUnit = GetTriggerUnit();
          if (!IsUnitValid(triggerUnit)) 
            return;
          CompletingUnit = triggerUnit;
          Progress = QuestProgress.Complete;
        });
      CreateTrigger()
        .RegisterLeaveRegion(targetRect)
        .AddAction(() =>
        {
          if (!IsValidUnitInRect()) 
            Progress = QuestProgress.Incomplete;
        });
      Position = new(GetRectCenterX(_targetRect), GetRectCenterY(_targetRect));
    }

    /// <inheritdoc />
    public unit? CompletingUnit { get; private set; }

    /// <inheritdoc />
    public string CompletingUnitName => CompletingUnit != null ? CompletingUnit.GetProperName() : "an unknown hero";

    private bool IsUnitValid(unit whichUnit) =>
      EligibleFactions.Contains(whichUnit.OwningPlayer()) && whichUnit.IsAlive() &&
      (IsUnitType(whichUnit, UNIT_TYPE_HERO) || !_heroOnly);

    private bool IsValidUnitInRect() => CreateGroup().EnumUnitsInRect(_targetRect).EmptyToList().Any(IsUnitValid);

    internal override void OnAdd(FactionSystem.Faction whichFaction)
    {
      Progress = IsValidUnitInRect() ? QuestProgress.Complete : QuestProgress.Incomplete;
    }
  }
}