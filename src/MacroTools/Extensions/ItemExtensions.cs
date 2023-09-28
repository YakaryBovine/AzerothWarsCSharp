using System;
using MacroTools.ShoreSystem;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace MacroTools.Extensions
{
  public static class ItemExtensions
  {
    /// <summary>
    /// Determines whether or not the item can be manually dropped.
    /// </summary>
    public static item SetDroppable(this item whichItem, bool canBeDropped)
    {
      SetItemDroppable(whichItem, canBeDropped);
      return whichItem;
    }
    
    /// <summary>
    /// Drop the item at the given position. If the position turns out to be non-ground-pathable,
    /// return it to a nearby <see cref="Shore"/> instead.
    /// </summary>
    public static void SetPositionSafe(this item whichItem, Point position)
    {
      if (!IsTerrainPathable(position.X, position.Y, PATHING_TYPE_WALKABILITY))
      {
        whichItem.SetPosition(position);
        return;
      }

      var shore = ShoreManager.GetNearestShore(position);
      if (shore == null)
      {
        throw new InvalidOperationException(
          $"{nameof(ItemExtensions)} could not find a {nameof(Shore)} to dump a {GetItemName(whichItem)}.");
      }
      whichItem.SetPosition(shore.Position);
    }

    public static item SetPosition(this item whichItem, float x, float y)
    {
      SetItemPosition(whichItem, x, y);
      return whichItem;
    }
    
    public static item SetPosition(this item whichItem, Point position)
    {
      SetItemPosition(whichItem, position.X, position.Y);
      return whichItem;
    }

    public static Point GetPosition(this item whichItem)
    {
      return new Point(GetItemX(whichItem), GetItemY(whichItem));
    }
  }
}