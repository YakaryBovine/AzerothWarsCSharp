using System.Collections.Generic;
using MacroTools.ControlPointSystem;
using MacroTools.Extensions;
using static War3Api.Common;


namespace WarcraftLegacies.Source.Setup
{
  public static class ControlPointSetup
  {
    private static readonly Dictionary<int, int> ControlPointValues = new()
    {
      {Constants.BUFF_B025_COMMAND_POINT_8_GOLD_PER_MINUTE, 8},
      {Constants.BUFF_B050_COMMAND_POINT_12_GOLD_PER_MINUTE, 12},
      {Constants.BUFF_B051_COMMAND_POINT_16_GOLD_PER_MINUTE, 16},
      {Constants.BUFF_B052_COMMAND_POINT_20_GOLD_PER_MINUTE, 20},
      {Constants.BUFF_B053_COMMAND_POINT_24_GOLD_PER_MINUTE, 24},
      {Constants.BUFF_B054_COMMAND_POINT_40_GOLD_PER_MINUTE, 40},
    };

    private static void InitializeControlPoint(unit unit)
    {
      foreach (var (unitTypeId, goldValue) in ControlPointValues)
        if (GetUnitAbilityLevel(unit, unitTypeId) > 0)
          ControlPointManager.Instance.Register(new ControlPoint(unit, goldValue));
    }

    public static void Setup()
    {
      foreach (var unit in CreateGroup().EnumUnitsInRect(WCSharp.Shared.Data.Rectangle.WorldBounds).EmptyToList())
        InitializeControlPoint(unit);
    }
  }
}