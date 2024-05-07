using System;
using System.Collections.Generic;
using War3Api.Object;
using War3Api.Object.Abilities;

namespace Launcher.Extensions
{
  public static class AbilityExtensions
  {
    public static bool HasVisibleIcon(this Ability ability)
    {
      return ability is not (InventoryPackMule or Inventory2SlotUnitHuman or Inventory2SlotUnitOrc or Inventory2SlotUnitUndead
        or Inventory2SlotUnitNightElf or Invulnerable or DefenseBonus1 or Ultravision or SellItem or AlliedBuilding or 
        PurchaseItem or LightningAttack or Inventory or AttributeModifierSkill);
    }
    
    public static IEnumerable<Unit> GetUnitSkinListSafe(this Ability ability)
    {
      try
      {
        return ability.DataUnitSkinList;
      }
      catch
      {
        return Array.Empty<Unit>();
      }
    }
    
    public static IEnumerable<Tech> GetTechtreeRequirementsSafe(this Ability ability)
    {
      try
      {
        return ability.TechtreeRequirements;
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