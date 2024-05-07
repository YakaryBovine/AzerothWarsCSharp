using Launcher.Extensions;
using War3Api.Object;
using War3Api.Object.Abilities;

namespace Launcher.IntegrityChecker.TestSupport
{
  /// <summary>
  /// Contains <see cref="BaseObject"/>s that are inaccessible to the game.
  /// </summary>
  public sealed class InaccessibleObjectCollection
  {
    public List<Unit> Units { get; }
    
    public List<Upgrade> Upgrades { get; }
    
    public List<Ability> Abilities { get; }

    public InaccessibleObjectCollection(List<Unit> units, List<Upgrade> upgrades, List<Ability> abilities)
    {
      Units = units;
      Upgrades = upgrades;
      Abilities = abilities;
    }

    /// <summary>
    /// Returns all <see cref="BaseObject"/>s in the collection.
    /// </summary>
    public List<BaseObject> GetAllObjects()
    {
      var objects = new List<BaseObject>();
      objects.AddRange(Units);
      objects.AddRange(Upgrades);
      return objects;
    }
    
    public void RemoveWithChildren(BaseObject baseObject)
    {
      switch (baseObject)
      {
        case Unit unit:
          RemoveWithChildren(unit);
          break;
        case Upgrade upgrade:
          RemoveWithChildren(upgrade);
          break;
        case ArchMageWaterElemental summonWaterElemental:
          RemoveWithChildren(summonWaterElemental);
          break;
        case SummonSeaElemental ability:
          RemoveWithChildren(ability);
          break;
        case ChaosGrom ability:
          RemoveWithChildren(ability);
          break;
        case ChaosGrunt ability:
          RemoveWithChildren(ability);
          break;
        case ChaosKodo ability:
          RemoveWithChildren(ability);
          break;
        case ChaosPeon ability:
          RemoveWithChildren(ability);
          break;
        case ChaosShaman ability:
          RemoveWithChildren(ability);
          break;
        case ChaosRaider ability:
          RemoveWithChildren(ability);
          break;
        case EtherealForm ability:
          RemoveWithChildren(ability);
          break;
        case CorporealForm ability:
          RemoveWithChildren(ability);
          break;
        case SentryWard ability:
          RemoveWithChildren(ability);
          break;
        case SentryWardItem ability:
          RemoveWithChildren(ability);
          break;
        case StasisTrap ability:
          RemoveWithChildren(ability);
          break;
        case Militia ability:
          RemoveWithChildren(ability);
          break;
        case Polymorph ability:
          RemoveWithChildren(ability);
          break;
        case PolymorphCreep ability:
          RemoveWithChildren(ability);
          break;
        case HexCreep ability:
          RemoveWithChildren(ability);
          break;
        case ShadowHunterHex ability:
          RemoveWithChildren(ability);
          break;
        case ShadowHunterSerpentWard ability:
          RemoveWithChildren(ability);
          break;
        case Burrow ability:
          RemoveWithChildren(ability);
          break;
        case BurrowScarabLvl2 ability:
          RemoveWithChildren(ability);
          break;
        case BurrowScarabLvl3 ability:
          RemoveWithChildren(ability);
          break;
        case Graveyard ability:
          RemoveWithChildren(ability);
          break;
        case Exhume ability:
          RemoveWithChildren(ability);
          break;
        case AvengerForm ability:
          RemoveWithChildren(ability);
          break;
        case PlagueToss ability:
          RemoveWithChildren(ability);
          break;
        case AuraPlagueAbomination ability:
          RemoveWithChildren(ability);
          break;
        case AuraPlagueCreep ability:
          RemoveWithChildren(ability);
          break;
        case AuraPlagueAnimatedDead ability:
          RemoveWithChildren(ability);
          break;
        case AuraPlagueCreepGfx ability:
          RemoveWithChildren(ability);
          break;
        case AuraPlaguePlagueWard ability:
          RemoveWithChildren(ability);
          break;
        case RaiseDead ability:
          RemoveWithChildren(ability);
          break;
        case RaiseDeadCreep ability:
          RemoveWithChildren(ability);
          break;
        case RaiseDeadItem ability:
          RemoveWithChildren(ability);
          break;
        case StoneForm ability:
          RemoveWithChildren(ability);
          break;
        case Inferno ability:
          RemoveWithChildren(ability);
          break;
        case ItemInferno ability:
          RemoveWithChildren(ability);
          break;
        case DreadlordInferno ability:
          RemoveWithChildren(ability);
          break;
        case TichondriusInferno ability:
          RemoveWithChildren(ability);
          break;
        case CryptLordLocustSwarm ability:
          RemoveWithChildren(ability);
          break;
        case Ability ability:
          RemoveWithChildren(ability);
          break;
      }
    }
    
    public void RemoveWithChildren(Unit unit)
    {
      if (!Units.Contains(unit))
        return;
      
      Units.Remove(unit);
      
      foreach (var trainedUnit in unit.GetUnitsTrainedSafe())
        RemoveWithChildren(trainedUnit);

      if (unit.IsTechtreeStructuresBuiltModified)
        foreach (var builtStructure in unit.TechtreeStructuresBuilt)
          RemoveWithChildren(builtStructure);
      
      foreach (var upgradesTo in unit.GetUpgradesToSafe())
        RemoveWithChildren(upgradesTo);
      
      foreach (var research in unit.GetResearchesAvailableSafe())
        RemoveWithChildren(research);
      
      if (unit.IsTechtreeUnitsSoldModified)
        foreach (var unitSold in unit.TechtreeUnitsSold)
          RemoveWithChildren(unitSold);
      
      foreach (var unitAbility in unit.GetUnitAbilitiesSafe())
        RemoveWithChildren(unitAbility);
      
      foreach (var heroAbility in unit.GetHeroAbilitiesSafe())
        RemoveWithChildren(heroAbility);
    }

    private void RemoveWithChildren(ArchMageWaterElemental archmageWaterElemental)
    {
      if (!Abilities.Contains(archmageWaterElemental))
        return;
      
      Abilities.Remove(archmageWaterElemental);
      for (var i = 0; i < archmageWaterElemental.StatsLevels; i++)
      {
        RemoveWithChildren(archmageWaterElemental.DataSummonedUnitType[i]);
      }
    }
    
    private void RemoveWithChildren(SummonSeaElemental ability)
    {
      if (!Abilities.Contains(ability))
        return;
      
      Abilities.Remove(ability);
      for (var i = 0; i < ability.StatsLevels; i++)
      {
        RemoveWithChildren(ability.DataSummonedUnitType[i]);
      }
    }
    
    private void RemoveWithChildren(ChaosGrom ability)
    {
      if (!Abilities.Contains(ability))
        return;
      
      Abilities.Remove(ability);
      for (var i = 0; i < ability.StatsLevels; i++)
      {
        RemoveWithChildren(ability.DataNewUnitType[i]);
      }
    }
    
    private void RemoveWithChildren(ChaosGrunt ability)
    {
      if (!Abilities.Contains(ability))
        return;
      
      Abilities.Remove(ability);
      for (var i = 0; i < ability.StatsLevels; i++)
      {
        RemoveWithChildren(ability.DataNewUnitType[i]);
      }
    }
    
    private void RemoveWithChildren(ChaosKodo ability)
    {
      if (!Abilities.Contains(ability))
        return;
      
      Abilities.Remove(ability);
      for (var i = 0; i < ability.StatsLevels; i++)
      {
        RemoveWithChildren(ability.DataNewUnitType[i]);
      }
    }
    
    private void RemoveWithChildren(ChaosPeon ability)
    {
      if (!Abilities.Contains(ability))
        return;
      
      Abilities.Remove(ability);
      for (var i = 0; i < ability.StatsLevels; i++)
      {
        RemoveWithChildren(ability.DataNewUnitType[i]);
      }
    }
    
    private void RemoveWithChildren(ChaosRaider ability)
    {
      if (!Abilities.Contains(ability))
        return;
      
      Abilities.Remove(ability);
      for (var i = 0; i < ability.StatsLevels; i++)
      {
        RemoveWithChildren(ability.DataNewUnitType[i]);
      }
    }
    
    private void RemoveWithChildren(ChaosShaman ability)
    {
      if (!Abilities.Contains(ability))
        return;
      
      Abilities.Remove(ability);
      for (var i = 0; i < ability.StatsLevels; i++)
      {
        RemoveWithChildren(ability.DataNewUnitType[i]);
      }
    }
    
    private void RemoveWithChildren(EtherealForm ability)
    {
      if (!Abilities.Contains(ability))
        return;
      
      Abilities.Remove(ability);
      for (var i = 0; i < ability.StatsLevels; i++)
      {
        RemoveWithChildren(ability.DataAlternateFormUnit[i]);
      }
    }
    
    private void RemoveWithChildren(CorporealForm ability)
    {
      if (!Abilities.Contains(ability))
        return;
      
      Abilities.Remove(ability);
      for (var i = 0; i < ability.StatsLevels; i++)
      {
        RemoveWithChildren(ability.DataAlternateFormUnit[i]);
      }
    }
    
    private void RemoveWithChildren(SentryWard ability)
    {
      if (!Abilities.Contains(ability))
        return;
      
      Abilities.Remove(ability);
      for (var i = 0; i < ability.StatsLevels; i++)
      {
        RemoveWithChildren(ability.DataWardUnitType[i]);
      }
    }
    
    private void RemoveWithChildren(SentryWardItem ability)
    {
      if (!Abilities.Contains(ability))
        return;
      
      Abilities.Remove(ability);
      for (var i = 0; i < ability.StatsLevels; i++)
      {
        RemoveWithChildren(ability.DataWardUnitType[i]);
      }
    }
    
    private void RemoveWithChildren(StasisTrap ability)
    {
      if (!Abilities.Contains(ability))
        return;
      
      Abilities.Remove(ability);
      for (var i = 0; i < ability.StatsLevels; i++)
      {
        RemoveWithChildren(ability.DataWardUnitType[i]);
      }
    }
    
    private void RemoveWithChildren(Militia ability)
    {
      if (!Abilities.Contains(ability))
        return;
      
      Abilities.Remove(ability);
      for (var i = 0; i < ability.StatsLevels; i++)
      {
        RemoveWithChildren(ability.DataAlternateFormUnit[i]);
      }
    }
    
    private void RemoveWithChildren(Polymorph ability)
    {
      if (!Abilities.Contains(ability))
        return;
      
      Abilities.Remove(ability);
      for (var i = 0; i < ability.StatsLevels; i++)
      {
        foreach (var unit in ability.DataMorphUnitsAir[i])
        {
          RemoveWithChildren(unit);
        }
        foreach (var unit in ability.DataMorphUnitsAmphibious[i])
        {
          RemoveWithChildren(unit);
        }
        foreach (var unit in ability.DataMorphUnitsGround[i])
        {
          RemoveWithChildren(unit);
        }
        foreach (var unit in ability.DataMorphUnitsWater[i])
        {
          RemoveWithChildren(unit);
        }
      }
    }
    
    private void RemoveWithChildren(PolymorphCreep ability)
    {
      if (!Abilities.Contains(ability))
        return;
      
      Abilities.Remove(ability);
      for (var i = 0; i < ability.StatsLevels; i++)
      {
        foreach (var unit in ability.DataMorphUnitsAir[i])
        {
          RemoveWithChildren(unit);
        }
        foreach (var unit in ability.DataMorphUnitsAmphibious[i])
        {
          RemoveWithChildren(unit);
        }
        foreach (var unit in ability.DataMorphUnitsGround[i])
        {
          RemoveWithChildren(unit);
        }
        foreach (var unit in ability.DataMorphUnitsWater[i])
        {
          RemoveWithChildren(unit);
        }
      }
    }
    
    private void RemoveWithChildren(HexCreep ability)
    {
      if (!Abilities.Contains(ability))
        return;
      
      Abilities.Remove(ability);
      for (var i = 0; i < ability.StatsLevels; i++)
      {
        foreach (var unit in ability.DataMorphUnitsAir[i])
        {
          RemoveWithChildren(unit);
        }
        foreach (var unit in ability.DataMorphUnitsAmphibious[i])
        {
          RemoveWithChildren(unit);
        }
        foreach (var unit in ability.DataMorphUnitsGround[i])
        {
          RemoveWithChildren(unit);
        }
        foreach (var unit in ability.DataMorphUnitsWater[i])
        {
          RemoveWithChildren(unit);
        }
      }
    }
    
    private void RemoveWithChildren(ShadowHunterHex ability)
    {
      if (!Abilities.Contains(ability))
        return;
      
      Abilities.Remove(ability);
      for (var i = 0; i < ability.StatsLevels; i++)
      {
        foreach (var unit in ability.DataMorphUnitsAir[i])
        {
          RemoveWithChildren(unit);
        }
        foreach (var unit in ability.DataMorphUnitsAmphibious[i])
        {
          RemoveWithChildren(unit);
        }
        foreach (var unit in ability.DataMorphUnitsGround[i])
        {
          RemoveWithChildren(unit);
        }
        foreach (var unit in ability.DataMorphUnitsWater[i])
        {
          RemoveWithChildren(unit);
        }
      }
    }
    
    private void RemoveWithChildren(ShadowHunterSerpentWard ability)
    {
      if (!Abilities.Contains(ability))
        return;
      
      Abilities.Remove(ability);
      for (var i = 0; i < ability.StatsLevels; i++)
      {
        RemoveWithChildren(ability.DataSummonedUnitType[i]);
      }
    }
    
    private void RemoveWithChildren(Burrow ability)
    {
      if (!Abilities.Contains(ability))
        return;
      
      Abilities.Remove(ability);
      for (var i = 0; i < ability.StatsLevels; i++)
      {
        RemoveWithChildren(ability.DataAlternateFormUnit[i]);
      }
    }
    
    private void RemoveWithChildren(BurrowScarabLvl2 ability)
    {
      if (!Abilities.Contains(ability))
        return;
      
      Abilities.Remove(ability);
      for (var i = 0; i < ability.StatsLevels; i++)
      {
        RemoveWithChildren(ability.DataAlternateFormUnit[i]);
      }
    }
    
    private void RemoveWithChildren(BurrowScarabLvl3 ability)
    {
      if (!Abilities.Contains(ability))
        return;
      
      Abilities.Remove(ability);
      for (var i = 0; i < ability.StatsLevels; i++)
      {
        RemoveWithChildren(ability.DataAlternateFormUnit[i]);
      }
    }
    
    private void RemoveWithChildren(Graveyard ability)
    {
      if (!Abilities.Contains(ability))
        return;
      
      Abilities.Remove(ability);
      for (var i = 0; i < ability.StatsLevels; i++)
      {
        RemoveWithChildren(ability.DataCorpseUnitType[i]);
      }
    }
    
    private void RemoveWithChildren(Exhume ability)
    {
      if (!Abilities.Contains(ability))
        return;
      
      Abilities.Remove(ability);
      for (var i = 0; i < ability.StatsLevels; i++)
      {
        RemoveWithChildren(ability.DataUnitType[i]);
      }
    }
    
    private void RemoveWithChildren(AvengerForm ability)
    {
      if (!Abilities.Contains(ability))
        return;
      
      Abilities.Remove(ability);
      for (var i = 0; i < ability.StatsLevels; i++)
      {
        RemoveWithChildren(ability.DataAlternateFormUnit[i]);
      }
    }
    
    private void RemoveWithChildren(PlagueToss ability)
    {
      if (!Abilities.Contains(ability))
        return;
      
      Abilities.Remove(ability);
      for (var i = 0; i < ability.StatsLevels; i++)
      {
        RemoveWithChildren(ability.DataWardUnitType[i]);
      }
    }
    
    private void RemoveWithChildren(AuraPlagueAbomination ability)
    {
      if (!Abilities.Contains(ability))
        return;
      
      Abilities.Remove(ability);
      for (var i = 0; i < ability.StatsLevels; i++)
      {
        RemoveWithChildren(ability.DataPlagueWardUnitType[i]);
      }
    }
    
    private void RemoveWithChildren(AuraPlagueCreep ability)
    {
      if (!Abilities.Contains(ability))
        return;
      
      Abilities.Remove(ability);
      for (var i = 0; i < ability.StatsLevels; i++)
      {
        RemoveWithChildren(ability.DataPlagueWardUnitType[i]);
      }
    }
    
    private void RemoveWithChildren(AuraPlagueAnimatedDead ability)
    {
      if (!Abilities.Contains(ability))
        return;
      
      Abilities.Remove(ability);
      for (var i = 0; i < ability.StatsLevels; i++)
      {
        RemoveWithChildren(ability.DataPlagueWardUnitType[i]);
      }
    }
    
    private void RemoveWithChildren(AuraPlagueCreepGfx ability)
    {
      if (!Abilities.Contains(ability))
        return;
      
      Abilities.Remove(ability);
      for (var i = 0; i < ability.StatsLevels; i++)
      {
        RemoveWithChildren(ability.DataPlagueWardUnitType[i]);
      }
    }
    
    private void RemoveWithChildren(AuraPlaguePlagueWard ability)
    {
      if (!Abilities.Contains(ability))
        return;
      
      Abilities.Remove(ability);
      for (var i = 0; i < ability.StatsLevels; i++)
      {
        RemoveWithChildren(ability.DataPlagueWardUnitType[i]);
      }
    }
    
    private void RemoveWithChildren(RaiseDead ability)
    {
      if (!Abilities.Contains(ability))
        return;
      
      Abilities.Remove(ability);
      for (var i = 0; i < ability.StatsLevels; i++)
      {
        RemoveWithChildren(ability.DataUnitTypeOne[i]);
        RemoveWithChildren(ability.DataUnitTypeTwo[i]);
      }
    }
    
    private void RemoveWithChildren(RaiseDeadCreep ability)
    {
      if (!Abilities.Contains(ability))
        return;
      
      Abilities.Remove(ability);
      for (var i = 0; i < ability.StatsLevels; i++)
      {
        RemoveWithChildren(ability.DataUnitTypeOne[i]);
        RemoveWithChildren(ability.DataUnitTypeTwo[i]);
      }
    }
    
    private void RemoveWithChildren(RaiseDeadItem ability)
    {
      if (!Abilities.Contains(ability))
        return;
      
      Abilities.Remove(ability);
      for (var i = 0; i < ability.StatsLevels; i++)
      {
        RemoveWithChildren(ability.DataUnitTypeOne[i]);
        RemoveWithChildren(ability.DataUnitTypeTwo[i]);
      }
    }
    
    private void RemoveWithChildren(StoneForm ability)
    {
      if (!Abilities.Contains(ability))
        return;
      
      Abilities.Remove(ability);
      for (var i = 0; i < ability.StatsLevels; i++)
      {
        RemoveWithChildren(ability.DataAlternateFormUnit[i]);
      }
    }
    
    private void RemoveWithChildren(CryptLordCarrionScarabs ability)
    {
      if (!Abilities.Contains(ability))
        return;
      
      Abilities.Remove(ability);
      for (var i = 0; i < ability.StatsLevels; i++)
      {
        RemoveWithChildren(ability.DataUnitTypeOne[i]);
        RemoveWithChildren(ability.DataUnitTypeTwo[i]);
      }
    }
    
    private void RemoveWithChildren(Inferno ability)
    {
      if (!Abilities.Contains(ability))
        return;
      
      Abilities.Remove(ability);
      for (var i = 0; i < ability.StatsLevels; i++) 
        RemoveWithChildren(ability.DataSummonedUnit[i]);
    }
    
    private void RemoveWithChildren(DreadlordInferno ability)
    {
      if (!Abilities.Contains(ability))
        return;
      
      Abilities.Remove(ability);
      for (var i = 0; i < ability.StatsLevels; i++) 
        RemoveWithChildren(ability.DataSummonedUnit[i]);
    }
    
    private void RemoveWithChildren(ItemInferno ability)
    {
      if (!Abilities.Contains(ability))
        return;
      
      Abilities.Remove(ability);
      for (var i = 0; i < ability.StatsLevels; i++) 
        RemoveWithChildren(ability.DataSummonedUnit[i]);
    }
    
    private void RemoveWithChildren(TichondriusInferno ability)
    {
      if (!Abilities.Contains(ability))
        return;
      
      Abilities.Remove(ability);
      for (var i = 0; i < ability.StatsLevels; i++) 
        RemoveWithChildren(ability.DataSummonedUnit[i]);
    }
    
    private void RemoveWithChildren(CryptLordLocustSwarm ability)
    {
      if (!Abilities.Contains(ability))
        return;
      
      Abilities.Remove(ability);
      for (var i = 0; i < ability.StatsLevels; i++) 
        RemoveWithChildren(ability.DataSwarmUnitType[i]);
    }
    
    private void RemoveWithChildren(Ability ability)
    {
      if (!Abilities.Contains(ability))
        return;
      
      Abilities.Remove(ability);
      foreach (var unit in ability.GetUnitSkinListSafe())
      {
        RemoveWithChildren(unit);
      }
    }
    
    private void RemoveWithChildren(Upgrade upgrade)
    {
      if (!Upgrades.Contains(upgrade))
        return;

      Upgrades.Remove(upgrade);
    }
  }
}