using System.Linq;
using MacroTools.Extensions;
using MacroTools.QuestSystem;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace MacroTools.ObjectiveSystem.Objectives.UnitBased
{
  /// <summary>
  /// Completed when an eligible player enters the provided <see cref="Rectangle"/> with a hero of the appropriate level.
  /// </summary>
  public sealed class ObjectiveHeroWithLevelInRect : Objective, IHasCompletingUnit
  {
    private readonly rect _targetRect;
    private readonly int _targetLevel;

    /// <summary>
    /// Initializes a new instance of the <see cref="ObjectiveHeroWithLevelInRect"/> class.
    /// </summary>
    /// <param name="targetLevel">Minimum required level to complete the objective</param>
    /// <param name="targetRect">Region that the unit has to enter in order to complete the objective</param>
    /// <param name="rectName">Display name of the region the unit has to enter. Used for flavor only.</param>
    public ObjectiveHeroWithLevelInRect(int targetLevel, Rectangle targetRect, string rectName)
    {
      _targetRect = targetRect.Rect;
      _targetLevel = targetLevel;
      Description = $"Reach {rectName} with a level {targetLevel}+ hero";
      PingPath = "MinimapQuestTurnIn";
      DisplaysPosition = true;
      
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
      EligibleFactions.Contains(whichUnit.OwningPlayer()) && whichUnit.IsAlive() && IsUnitType(whichUnit, UNIT_TYPE_HERO) &&
      GetHeroLevel(whichUnit) >= _targetLevel;

    private bool IsValidUnitInRect() => CreateGroup().EnumUnitsInRect(_targetRect).EmptyToList().Any(IsUnitValid);
  }
}