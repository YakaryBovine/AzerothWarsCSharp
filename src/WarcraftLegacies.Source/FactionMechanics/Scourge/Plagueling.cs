using WCSharp.Events;
using static War3Api.Common;

namespace WarcraftLegacies.Source.FactionMechanics.Scourge
{
  public static class Plagueling
  {
    private static readonly int PlaguelingId = FourCC("n08G");
    private const float DURATION = 15;
    
    private static void OnSell()
    {
      UnitApplyTimedLife(GetSoldUnit(), 0, DURATION);
      SetUnitExploded(GetSoldUnit(), true);
    }

    public static void Setup()
    {
      PlayerUnitEvents.Register(UnitTypeEvent.SellsUnit, OnSell, PlaguelingId);
    }
  }
}