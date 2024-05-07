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