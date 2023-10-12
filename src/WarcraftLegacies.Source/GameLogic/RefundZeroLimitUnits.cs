using MacroTools.Extensions;
using MacroTools.FactionSystem;
using WCSharp.Events;
using static War3Api.Common;


namespace WarcraftLegacies.Source.GameLogic
{
  /// <summary>
  ///   If a unit with a tech limit of 0 is trained or revived, delete and refund it instantly.
  /// </summary>
  public static class RefundZeroLimitUnits
  {
    private static void VerifyUnitIntegrity(unit whichUnit)
    {
      var player = GetOwningPlayer(whichUnit);
      if (player.GetObjectLimit(GetUnitTypeId(whichUnit)) != 0) 
        return;

      if (IsUnitType(whichUnit, UNIT_TYPE_HERO))
      {
        player.AdjustPlayerState(PLAYER_STATE_RESOURCE_GOLD, PlayerDistributor.HeroCost);
      }
      else
      {
        player.AdjustPlayerState(PLAYER_STATE_RESOURCE_GOLD, GetUnitGoldCost(GetUnitTypeId(whichUnit)));
        player.AdjustPlayerState(PLAYER_STATE_RESOURCE_LUMBER, GetUnitWoodCost(GetUnitTypeId(whichUnit)));
      }
      
      RemoveUnit(whichUnit);
    }

    /// <summary>
    /// Registers triggers that cause the following:
    /// <para>Whenever a unit with a tech limit of 0 is trained or revived, delete and refund it instantly.</para>
    /// </summary>
    public static void Setup()
    {
      PlayerUnitEvents.Register(UnitTypeEvent.FinishesTraining,
        () => { VerifyUnitIntegrity(GetTrainedUnit()); });
      PlayerUnitEvents.Register(HeroTypeEvent.FinishesRevive,
        () => { VerifyUnitIntegrity(GetRevivingUnit()); });
    }
  }
}