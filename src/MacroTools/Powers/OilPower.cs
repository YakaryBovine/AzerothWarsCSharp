using System;
using System.Collections.Generic;
using System.Linq;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.Hazards;
using MacroTools.SpellSystem;
using WCSharp.Events;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace MacroTools.Powers
{
  /// <summary>
  /// Gives the ability to store Oil as an additional resource.
  /// </summary>
  public sealed class OilPower : Power
  {
    private static readonly PeriodicTrigger<OilIncomePeriodicAction> OilIncomePeriodicTrigger = new(1f);
    private float _amount;
    private float _income;
    private OilIncomePeriodicAction? _oilIncomePeriodicAction;
    private readonly List<OilPool> _oilPools = new();
    private readonly List<player> _owners = new();
    private timer? _oilTimer;

    /// <summary>Fired when the amount of oil stored changes.</summary>
    public event EventHandler<OilPower>? AmountChanged;

    /// <summary><see cref="OilPool"/>s cannot spawn within this distance of eachother.</summary>
    public float OilPoolBorderDistance { get; init; } = 600;

    /// <summary>The amount of oil the <see cref="OilPower"/> has.</summary>
    public float Amount
    {
      get => _amount;
      set
      {
        _amount = value;
        AmountChanged?.Invoke(this, this);
        RefreshDescription();
      }
    }

    /// <summary>The amount of oil the <see cref="OilPower"/> gains per second.</summary>
    public float Income
    {
      get => _income;
      set
      {
        _income = value;
        RefreshDescription();
      }
    }

    /// <summary>The maximum number of oil pools that will generate on the map.</summary>
    public int MaximumOilPoolCount { get; init; }

    /// <summary>The number of oil pools that are randomly spawned when this power is registered.</summary>
    public int StartingRandomOilPoolCount { get; init; }

    /// <summary>Oil Pools will be spawned at all of these locations when this power is registered.</summary>
    public IEnumerable<Point> ForcedStartingOilPoolSpawnLocations { get; init; } = Array.Empty<Point>();
    
    /// <summary>The maximum amount of oil that a given <see cref="OilPool"/> can start with.</summary>
    public int OilPoolMaximumValue { get; init; }

    /// <summary>The minimum amount of oil that a given <see cref="OilPool"/> can start with.</summary>
    public int OilPoolMinimumValue { get; init; }
  
    /// <summary>Initializes a new instance of the <see cref="OilPower"/> class.</summary>
    public OilPower()
    {
      RefreshDescription();
    }
    
    /// <summary>
    /// Returns all <see cref="OilPool"/>s managed by this <see cref="OilPower"/>.
    /// </summary>
    public IEnumerable<OilPool> GetAllOilPools() => _oilPools.AsReadOnly();

    /// <inheritdoc/>
    public override void OnAdd(player whichPlayer)
    {
      _owners.Add(whichPlayer);
      _oilIncomePeriodicAction = new OilIncomePeriodicAction(this);
      OilIncomePeriodicTrigger.Add(_oilIncomePeriodicAction);
      
      _oilTimer = CreateTimer().Start(150, true, GenerateRandomOilPool);

      foreach (var position in ForcedStartingOilPoolSpawnLocations)
        GenerateOilPool(position);
      
      for (var i = 0; i < StartingRandomOilPoolCount; i++)
        GenerateRandomOilPool();
    }

    /// <inheritdoc/>
    public override void OnRemove(player whichPlayer)
    {
      if (_oilIncomePeriodicAction == null) return;
      _oilIncomePeriodicAction.Active = false;
      _oilIncomePeriodicAction = null;
      _owners.Remove(whichPlayer);
      _oilTimer?.Destroy();
    }
    
    /// <summary>
    /// Returns all <see cref="OilPool"/>s in the specified radius around the specified <see cref="Point"/>.
    /// </summary>
    public IEnumerable<OilPool> GetOilPoolsInRadius(Point fromPoint, float radius)
    {
      return GetAllOilPools()
        .Where(x =>
        {
          var oilPoolPosition = x.Position;
          return WCSharp.Shared.Util.DistanceBetweenPoints(fromPoint.X, fromPoint.Y, oilPoolPosition.X,
            oilPoolPosition.Y) < radius;
        });
    }

    private void GenerateRandomOilPool()
    {
      if (_oilPools.Count >= MaximumOilPoolCount)
        return;
      
      GenerateOilPool(GetRandomPointAtSea());
    }
    
    private void GenerateOilPool(Point position)
    {
      var oilPool = new OilPool(_owners.First(), position, "Tar Pool.mdx", this)
      {
        Active = true,
        Duration = float.MaxValue,
        OilAmount = GetRandomInt(OilPoolMinimumValue, OilPoolMaximumValue)
      };
      oilPool.Disposed += OnOilPoolDisposed;
      HazardSystem.Add(oilPool);
      _oilPools.Add(oilPool);
    }

    private void OnOilPoolDisposed(object? sender, OilPool oilPool)
    {
      _oilPools.Remove(oilPool);
      oilPool.Disposed -= OnOilPoolDisposed;
    }

    private void RefreshDescription()
    {
      Description =
        $"You can harvest oil and use it to enhance your mechanical units.|n|cffffcc00Oil:|r {Amount}|n|cffffcc00Income:|r {Income}";
    }

    private Point GetRandomPointAtSea()
    {
      const int callLimit = 200;
      var callCount = 0;
      Point randomPoint;
      do
      {
        randomPoint = Rectangle.WorldBounds.GetRandomPoint();
        callCount++;
        if (callCount == callLimit)
          throw new InvalidOperationException($"{nameof(GetRandomPointAtSea)} failed to find a point {callLimit} times.");
      } while (!IsPointValidForOilPool(randomPoint));

      return randomPoint;
    }

    private bool IsPointValidForOilPool(Point whichPoint) =>
      whichPoint.IsPathable(PATHING_TYPE_FLOATABILITY) && !whichPoint.IsPathable(PATHING_TYPE_WALKABILITY) &&
      whichPoint.TerrainType() == FourCC("Gsqd") && !GetOilPoolsInRadius(whichPoint, OilPoolBorderDistance).Any();
  }
}