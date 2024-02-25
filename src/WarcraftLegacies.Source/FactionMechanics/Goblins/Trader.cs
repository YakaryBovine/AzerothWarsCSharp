using System;
using System.Collections.Generic;
using System.Linq;
using MacroTools.Buffs;
using MacroTools.Extensions;
using MacroTools.PassiveAbilitySystem;
using WCSharp.Buffs;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.FactionMechanics.Goblins
{
  /// <summary>
  /// When trained, the <see cref="Trader"/> patrols between the originating point and a random trade destination.
  /// If the training unit dies, the <see cref="Trader"/> dies too.
  /// </summary>
  public sealed class Trader : PassiveAbility
  {
    private readonly int _goldIncomeBonus;
    private readonly int _lumberIncomeBonus;
    private readonly Point[] _tradeTargets;

    public Trader(int unitTypeId, int goldIncomeBonus, int lumberIncomeBonus, IEnumerable<Point> tradeTargets) :
      base(unitTypeId)
    {
      _goldIncomeBonus = goldIncomeBonus;
      _lumberIncomeBonus = lumberIncomeBonus;
      _tradeTargets = tradeTargets.ToArray();
    }

    public override void OnTrained()
    {
      try
      {
        var tradingCenter = GetTriggerUnit();
        var trainedUnit = GetTrainedUnit();
        trainedUnit.IssueOrder("patrol", _tradeTargets[GetRandomInt(0, _tradeTargets.Length - 1)]);
        var incomeBuff =
          new TraderBuff(tradingCenter, trainedUnit, _goldIncomeBonus, _lumberIncomeBonus, tradingCenter);
        BuffSystem.Add(incomeBuff);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
      }
    }
  }
}