using System;
using MacroTools.Extensions;
using WCSharp.Events;

namespace TestMap.Source
{
  public static class BurrowPrototype
  {
    public static void Setup()
    {
      // PlayerUnitEvents.Register(UnitTypeEvent.IsLoaded, () =>
      // {
      //   var loadedUnit = GetLoadedUnit();
      //   
      //   CreateTimer().Start(1, false, () =>
      //   {
      //     //RemoveUnit(loadedUnit);
      //     ShowUnit(GetLoadedUnit(), false);
      //     //SetUnitPosition(loadedUnit, 0, 0);
      //   });
      //   
      //   Console.WriteLine(GetUnitName(GetLoadedUnit()));
      // });
      
      PlayerUnitEvents.Register(UnitTypeEvent.SpellEffect, () =>
      {
        GetUnitName(GetTriggerUnit());
      });
      
      PlayerUnitEvents.Register(UnitTypeEvent.ReceivesPointOrder, () =>
      {
        Console.WriteLine(OrderId2String(GetIssuedOrderId()));
      });
    }
  }
}