using MacroTools.Extensions;
using WCSharp.Events;
using static War3Api.Common;

namespace WarcraftLegacies.Source.FactionMechanics.Scourge
{
  public static class SacrificeAcolyte
  {
    private static readonly int AcolyteId = FourCC("uaco");
    
    private static void OnSell()
    {
      GetTriggerUnit().Kill();
      GetSoldUnit().SetFacingEx(GetTriggerUnit().GetFacing());
      GetSoldUnit().OwningPlayer().Select(GetSoldUnit());
    }

    public static void Setup()
    {
      PlayerUnitEvents.Register(UnitTypeEvent.SellsUnit, OnSell, AcolyteId);
    }
  }
}