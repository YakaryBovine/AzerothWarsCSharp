using System;
using System.Collections.Generic;
using System.Linq;
using WCSharp.Shared.Data;
using static War3Api.Common;


namespace MacroTools.Extensions
{
  public static class RectangleExtensions
  {
    public static void AddSound(this Rectangle region, sound soundHandle)
    {
      var width = GetRectMaxX(region.Rect) - GetRectMinX(region.Rect);
      var height = GetRectMaxY(region.Rect) - GetRectMinY(region.Rect);
      SetSoundPosition(soundHandle, GetRectCenterX(region.Rect), GetRectCenterY(region.Rect), 0);
      RegisterStackedSound(soundHandle, true, width, height);
    }

    public static List<unit> PrepareUnitsForRescue(this Rectangle rectangle, RescuePreparationMode rescuePreparationMode, Func<unit, bool>? filter = null)
    {
      filter ??= _ => true;
      return rescuePreparationMode switch
      {
        RescuePreparationMode.Invulnerable => PrepareUnitsForRescue(rectangle, false, false, filter),
        RescuePreparationMode.HideNonStructures => PrepareUnitsForRescue(rectangle, true, false, filter),
        RescuePreparationMode.HideAll => PrepareUnitsForRescue(rectangle, true, true, filter),
        _ => throw new ArgumentException($"{nameof(rescuePreparationMode)} is not implemented for this function.",
            nameof(rescuePreparationMode))
      };
    }

    public static void CleanupNeutralPassiveUnits(this Rectangle area, NeutralPassiveCleanupType cleanupType = NeutralPassiveCleanupType.RemoveUnits)
    {
      var unitsInArea = CreateGroup()
          .EnumUnitsInRect(area)
          .EmptyToList();

      foreach (var unit in unitsInArea)
      {
        if (unit.OwningPlayer() != Player(PLAYER_NEUTRAL_PASSIVE) || unit.GetTypeId() == FourCC("ngol"))
          continue;

        if (!unit.IsRemovable())
        {
          unit.SetOwner(Player(PLAYER_NEUTRAL_AGGRESSIVE));
          continue;
        }

        if (unit.IsRemovable() && !BlzIsUnitInvulnerable(unit) && (cleanupType == NeutralPassiveCleanupType.RemoveUnits || unit.IsType(UNIT_TYPE_STRUCTURE)))
          unit.Remove();
        else
          unit.SetOwner(Player(PLAYER_NEUTRAL_AGGRESSIVE));
      }
    }

    public static void CleanupHostileUnits(this Rectangle area)
    {
      var unitsInArea = CreateGroup()
          .EnumUnitsInRect(area)
          .EmptyToList();
      foreach (var unit in unitsInArea)
      {
        if (unit.OwningPlayer() == Player(PLAYER_NEUTRAL_AGGRESSIVE) && unit.IsRemovable())
        {
          unit.Remove();
        }
      }
    }

    private static List<unit> PrepareUnitsForRescue(this Rectangle rectangle, bool hideUnits, bool hideStructures, Func<unit, bool> filter)
    {
      var group = CreateGroup()
          .EnumUnitsInRect(rectangle)
          .EmptyToList()
          .Where(x => x.OwningPlayer() == Player(PLAYER_NEUTRAL_PASSIVE) && filter.Invoke(x))
          .ToList();
      foreach (var unit in group)
      {
        if (IsUnitType(unit, UNIT_TYPE_STRUCTURE) && hideStructures && !IsUnitType(unit, UNIT_TYPE_ANCIENT) ||
            !IsUnitType(unit, UNIT_TYPE_STRUCTURE) && hideUnits)
          unit.Show(false);
        unit
            .SetInvulnerable(true)
            .PauseEx(true);
      }
      return group;
    }


    public static List<unit> ReplaceWorkers(this List<unit> units, player pickingPlayer, int factionWorker)
    {
      var newUnits = new List<unit>();

      foreach (var unit in units)
      {
        if (IsUnitType(unit, UNIT_TYPE_PEON))
        {
          var x = GetUnitX(unit);
          var y = GetUnitY(unit);
          var facing = GetUnitFacing(unit);

          RemoveUnit(unit);

          var newUnit = CreateUnit(pickingPlayer, factionWorker, x, y, facing);
          newUnits.Add(newUnit);
         
        }
        else
        {
          newUnits.Add(unit);
        }
      }

      return newUnits;
    }

    public static List<unit> ReplaceTownHall(this List<unit> units, player pickingPlayer, int townHallUnitTypeId)
    {
      var newUnits = new List<unit>();

      foreach (var unit in units)
      {
        if (IsUnitType(unit, UNIT_TYPE_TOWNHALL))
        {
          var x = GetUnitX(unit);
          var y = GetUnitY(unit);
          var facing = GetUnitFacing(unit);

          RemoveUnit(unit);

          var newUnit = CreateUnit(pickingPlayer, townHallUnitTypeId, x, y, facing);
          newUnits.Add(newUnit);
          
        }
        else
        {
          newUnits.Add(unit);
        }
      }

      return newUnits;
    }

  }
}
