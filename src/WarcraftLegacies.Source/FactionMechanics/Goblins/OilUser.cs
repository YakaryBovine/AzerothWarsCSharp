using System;
using MacroTools.Extensions;
using MacroTools.PassiveAbilitySystem;
using MacroTools.Powers;
using WCSharp.Buffs;
using static War3Api.Common;

namespace WarcraftLegacies.Source.FactionMechanics.Goblins
{
  /// <summary>
  /// Oil users use oil instead of mana. The oil is provided by the owner's <see cref="OilPower"/> if they have one.
  /// </summary>
  public sealed class OilUser : PassiveAbility
  {
    /// <inheritdoc />
    public OilUser(int unitTypeId) : base(unitTypeId)
    {
    }

    /// <inheritdoc />
    public override void OnCreated(unit createdUnit)
    {
      var owningFaction = createdUnit.OwningPlayer().GetFaction();
      var oilPower = owningFaction?.GetPowerByType<OilPower>();
      if (oilPower == null)
      {
        var unitPosition = createdUnit.GetPosition();
        throw new Exception(
          $"Oil user {GetUnitName(createdUnit)} at ({unitPosition.X}, {unitPosition.Y}) was created but owning player {owningFaction?.Name ?? GetPlayerName(createdUnit.OwningPlayer())} doesn't have a power that stores oil.");
      }

      var oilBuff = new OilUserBuff(createdUnit, oilPower);
      BuffSystem.Add(oilBuff);
    }
  }
}