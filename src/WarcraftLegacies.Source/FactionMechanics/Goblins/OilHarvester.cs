using System;
using System.Linq;
using MacroTools.Extensions;
using MacroTools.Hazards;
using MacroTools.PassiveAbilitySystem;
using MacroTools.Powers;
using WCSharp.Buffs;
using static War3Api.Common;

namespace WarcraftLegacies.Source.FactionMechanics.Goblins
{
  /// <summary>
  /// Oil harvesters consume oil from <see cref="OilPool"/>s around them.
  /// </summary>
  public sealed class OilHarvester : PassiveAbility
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="OilHarvester"/> class.
    /// </summary>
    /// <param name="unitTypeId"><inheritdoc /></param>
    public OilHarvester(int unitTypeId) : base(unitTypeId)
    {
    }

    /// <summary>
    /// How much oil this unit harvests per second.
    /// </summary>
    public int OilHarvestedPerSecond { get; init; }

    /// <summary>
    /// The radius in which this unit can harvest from <see cref="OilPool"/>s.
    /// </summary>
    public float Radius { get; init; }

    /// <inheritdoc />
    public override void OnCreated(unit createdUnit)
    {
      if (!EnsureValidPositioning(createdUnit))
        return;

      var owningFaction = createdUnit.OwningPlayer().GetFaction();
      var oilPower = owningFaction?.GetPowerByType<OilPower>();
      if (oilPower == null)
        throw new Exception(
          $"Oil user {GetUnitName(GetTriggerUnit())} was created but owning faction {owningFaction?.Name} doesn't have a power that stores oil.");
      
      var oilPoolNearby = oilPower.GetOilPoolsInRadius(createdUnit.GetPosition(), Radius).FirstOrDefault();

      if (oilPoolNearby == null)
      {
        createdUnit.Kill().Remove();
        return;
      }
      
      var oilBuff = new OilHarvesterBuff(createdUnit, oilPoolNearby)
      {
        Active = true,
        Duration = float.MaxValue,
        OilHarvestedPerSecond = OilHarvestedPerSecond
      };
      BuffSystem.Add(oilBuff);
    }

    private static bool EnsureValidPositioning(unit createdUnit)
    {
      if (CreateGroup().EnumUnitsInRange(createdUnit.GetPosition(), 900)
          .EmptyToList()
          .All(x => x.GetTypeId() != createdUnit.GetTypeId() || x == createdUnit || !UnitAlive(x)))
        return true;
      createdUnit.Kill().Remove();
      return false;
    }
  }
}