﻿using System;
using System.Collections.Generic;
using War3Api.Object;
using War3Api.Object.Abilities;

namespace Launcher.Extensions
{
  public static class AbilityExtensions
  {
    public static bool HasVisibleIcon(this Ability ability)
    {
      if (ability is InventoryPackMule or Inventory2SlotUnitHuman or Inventory2SlotUnitOrc or Inventory2SlotUnitUndead
          or Inventory2SlotUnitNightElf)
        return false;

      if (ability is Invulnerable)
        return false;

      if (ability is DefenseBonus1)
        return false;
      
      return true;
    }
    
    public static IEnumerable<Tech> GetTechtreeRequirementsSafe(this Ability unit)
    {
      try
      {
        return unit.TechtreeRequirements;
      }
      catch
      {
        return Array.Empty<Tech>();
      }
    }

    /// <summary>
    /// Gets a name that can be used to describe an ability. Usually just the name field.
    /// </summary>
    public static string GetNameSafe(this Ability ability)
    {
      try
      {
        return ability.TextName;
      }
      catch
      {
        return "Not found";
      }
    }

    /// <summary>
    /// Determines the order that abilities appear in tooltips.
    /// </summary>
    public static int GetPrioritySafe(this Ability ability)
    {
      try
      {
        var (x, y) = (ability.ArtButtonPositionNormalX, ability.ArtButtonPositionNormalY);
        return x - y * 10;
      }
      catch
      {
        return 0;
      }
    }
  }
}