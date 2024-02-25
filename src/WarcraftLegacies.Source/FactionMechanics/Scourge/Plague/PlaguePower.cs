using System;
using System.Collections.Generic;
using System.Linq;
using MacroTools.Buffs;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using WCSharp.Buffs;
using WCSharp.Events;
using static War3Api.Common;

namespace WarcraftLegacies.Source.FactionMechanics.Scourge.Plague
{
  /// <summary>
  ///   Grants the <see cref="Power" /> holder several Plague Cauldrons that periodically spawn undead units.
  /// </summary>
  public sealed class PlaguePower : Power
  {
    private readonly Faction _victim;

    /// <summary>
    /// A list of all players that have this <see cref="Power"/>.
    /// </summary>
    private readonly List<player> _holders = new();
    
    private readonly List<int> _cityBuildings = new()
    {
      FourCC("ncb0"),
      FourCC("ncb1"),
      FourCC("ncb2"),
      FourCC("ncb3"),
      FourCC("ncb4"),
      FourCC("ncb5"),
      FourCC("ncb6"),
      FourCC("ncb7"),
      FourCC("ncb8"),
      FourCC("ncb9"),
      FourCC("ncba"),
      FourCC("ncbb"),
      FourCC("ncbc"),
      FourCC("ncbd"),
      FourCC("ncbb"),
      FourCC("ncbe"),
      FourCC("ncbf")
    };

    private readonly List<int> _villagerUnitTypeIds = new()
    {
      FourCC("nvlw"),
      FourCC("nvl2"),
      FourCC("nvil"),
      FourCC("nvlk"),
      FourCC("nvk2")
    };

    private readonly Dictionary<int, Action> _villagerPlagueConversionActions = new();
    
    /// <summary>
    /// Causes <see cref="Power" /> holder to periodically convert villagers on the map into Zombies.
    /// <param name="victim">Only this faction will spawn villagers from their buildings when they die.</param>
    /// </summary>
    public PlaguePower(Faction victim)
    {
      Name = "Plague of Undeath";
      Description = "All villagers on the map are periodically transformed into Zombies.";
      _victim = victim;
    }

    /// <inheritdoc />
    public override void OnAdd(player whichPlayer)
    {
      if (_holders.Count == 0)
      {
        foreach (var unitTypeId in _cityBuildings)
          PlayerUnitEvents.Register(UnitTypeEvent.Dies, SpawnPeasants, unitTypeId);
      }
      _holders.Add(whichPlayer);

      var darkConversionBuffOwner = Player(PLAYER_NEUTRAL_AGGRESSIVE);

      foreach (var player in _holders)
      {
        if (player.GetFaction()?.ScoreStatus == ScoreStatus.Defeated) 
          continue;
        darkConversionBuffOwner = player;
        break;
      }

      if (darkConversionBuffOwner == Player(PLAYER_NEUTRAL_PASSIVE) || darkConversionBuffOwner == null)
        darkConversionBuffOwner = Player(PLAYER_NEUTRAL_AGGRESSIVE);
      
      var villagers = CreateGroup().EnumUnitsOfPlayer(Player(PLAYER_NEUTRAL_PASSIVE))
        .EmptyToList()
        .Where(x => _villagerUnitTypeIds.Contains(GetUnitTypeId(x)) && x.IsAlive() && !BlzIsUnitInvulnerable(x));
      foreach (var villager in villagers) 
        ApplyDarkConversion(villager, darkConversionBuffOwner);

      if (_holders.Count != 1) 
        return;
      
      foreach (var villagerTypeId in _villagerUnitTypeIds)
      {
        void Action() => ApplyDarkConversion(GetTriggerUnit(), darkConversionBuffOwner);
        _villagerPlagueConversionActions.Add(villagerTypeId, Action);
        PlayerUnitEvents.Register(UnitTypeEvent.IsCreated, Action, villagerTypeId);
      }
    }

    /// <inheritdoc />
    public override void OnRemove(player whichPlayer)
    {
      _holders.Remove(whichPlayer);

      PlayerUnitEvents.Unregister(UnitTypeEvent.Dies, SpawnPeasants);
      foreach (var villagerTypeId in _villagerUnitTypeIds)
        PlayerUnitEvents.Unregister(UnitTypeEvent.IsCreated, _villagerPlagueConversionActions[villagerTypeId], villagerTypeId);
    }

    private static void ApplyDarkConversion(unit whichUnit, player buffOwner)
    {
      var darkConversionBuff = new DarkConversionBuff(buffOwner, whichUnit)
      {
        TransformUnitTypeId = Constants.UNIT_NZOM_ZOMBIE_SCOURGE,
        Duration = GetRandomReal(4, 8),
        TransformEffect = @"Abilities\Spells\Demon\DarkConversion\ZombifyTarget.mdl",
        DiseaseCloudAbilityId = Constants.ABILITY_AAP1_DISEASE_CLOUD_RED_ABOMINATION_ZOMBIE
      };
      BuffSystem.Add(darkConversionBuff, StackBehaviour.Stack);
    }
    
    private void SpawnPeasants()
    {
      var triggerUnit = GetTriggerUnit();
      if (_victim.Player?.GetTeam()?.Contains(triggerUnit.OwningPlayer()) != true)
        return;
      
      var x = GetUnitX(triggerUnit);
      var y = GetUnitY(triggerUnit);

      for (var i = 0; i < 2; i++)
        CreateUnit(Player(PLAYER_NEUTRAL_PASSIVE),
          _villagerUnitTypeIds[GetRandomInt(0, _villagerUnitTypeIds.Count - 1)], x, y, 0);
    }
  }
}