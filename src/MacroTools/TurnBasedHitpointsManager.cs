using System;
using System.Collections.Generic;
using MacroTools.Extensions;
using static War3Api.Common;

namespace MacroTools
{
  /// <summary>A tool for giving units additional hit points per turn.</summary>
  public static class TurnBasedHitpointsManager
  {
    /// <summary>Past this turn, units will not gain maximum hit points.</summary>
    private const float TurnLimit = 45;

    private static readonly Dictionary<unit, TurnBasedHitpointData> UnitData = new();
    private static bool _intialized;
    
    /// <summary>Causes the unit to continously gain maximum hit points as each turn passes.</summary>
    public static void Register(unit whichUnit, float hitPointPercentagePerTurn)
    {
      if (UnitData.ContainsKey(whichUnit))
        throw new InvalidOperationException($"Tried to register {whichUnit.GetName()} to {nameof(TurnBasedHitpointsManager)}, but it's already registered.");
      
      UnitData.Add(whichUnit, new TurnBasedHitpointData
      {
        HitPointPercentagePerTurn = hitPointPercentagePerTurn,
        BaseHitPoints = whichUnit.GetMaximumHitPoints()
      });
      if (_intialized) 
        return;
      
      GameTime.TurnEnded += OnTurnEnded;
      _intialized = true;
    }

    private static void OnTurnEnded(object? sender, EventArgs eventArgs)
    {
      var turn = GameTime.GetTurn();
      foreach (var (unit, turnBasedHitpointData) in UnitData)
      {
        var bonusPercentage = turnBasedHitpointData.HitPointPercentagePerTurn * turn;
        var bonusHitPoints = (int)Math.Ceiling(turnBasedHitpointData.BaseHitPoints * bonusPercentage);
        unit.SetMaximumHitpoints(turnBasedHitpointData.BaseHitPoints + bonusHitPoints);
        
        var heal = turnBasedHitpointData.BaseHitPoints * turnBasedHitpointData.HitPointPercentagePerTurn;
        unit.SetCurrentHitpoints((int)Math.Ceiling(unit.GetHitPoints() + heal));
      }
      
      if (turn >= TurnLimit) 
        GameTime.TurnEnded -= OnTurnEnded;
    }
    
    private sealed class TurnBasedHitpointData
    {
      public float HitPointPercentagePerTurn { get; init; }
      public int BaseHitPoints { get; init; }
    }
  }
}