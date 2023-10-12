using System.Collections.Generic;
using static War3Api.Common;

namespace MacroTools
{
  /// <summary>
  /// Contains extra Warcraft Legacies-specific information about Warcraft 3 unit types.
  /// </summary>
  public sealed class UnitType
  {
    private static readonly List<UnitType> All = new();
    private static readonly Dictionary<int, UnitType> ById = new();
    
    /// <summary>
    /// The Warcraft 3 unit type ID for the <see cref="UnitType"/> wrapper.
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Returns all registered <see cref="UnitType"/>s.
    /// </summary>
    public static IEnumerable<UnitType> GetAll() => All.AsReadOnly();

    /// <summary>
    /// How much gold the UnitType costs to train or build.
    /// </summary>
    public int GoldCost => GetUnitGoldCost(Id);

    /// <summary>
    /// How much lumber the UnitType costs to train or build.
    /// </summary>
    public int LumberCost => GetUnitWoodCost(Id);

    /// <summary>
    /// If true, this unit should never be deleted.
    /// </summary>
    public bool NeverDelete { internal get; init; }

    /// <summary>
    /// An arbitrary category, like "Shipyard" or "Shop".
    /// </summary>
    public UnitCategory Category { get; init; }

    /// <summary>
    /// Returns the UnitType representation of a unit on the map.
    /// </summary>
    public static UnitType GetFromHandle(unit whichUnit) => 
      ById.TryGetValue(GetUnitTypeId(whichUnit), out var unitType) ? unitType : new UnitType(GetUnitTypeId(whichUnit));

    /// <summary>
    /// Returns the UnitType representation of a particular UnitTypeId.
    /// </summary>
    public static UnitType GetFromId(int id)
    {
      return ById[id];
    }

    /// <summary>
    /// Registers a <see cref="UnitType"/> to the system.
    /// </summary>
    public static void Register(UnitType unitType)
    {
      ById[unitType.Id] = unitType;
      All.Add(unitType);
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="UnitType"/> class.
    /// </summary>
    /// <param name="unitId">The Warcraft 3 unit type ID you want to record extra information about.</param>
    public UnitType(int unitId)
    {
      Id = unitId;
    }
  }
}