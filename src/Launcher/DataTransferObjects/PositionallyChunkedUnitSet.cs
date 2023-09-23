using System.Collections.Generic;
using Launcher.Extensions;

namespace Launcher.DataTransferObjects
{
  /// <summary>
  /// Seperates a set of <see cref="UnitDataDto"/>s by map chunks of a variable size.
  /// </summary>
  public sealed class PositionallyChunkedUnitSet
  {
    private readonly Dictionary<(int, int), HashSet<UnitDataDto>> _unitsByChunk = new();

    private readonly int _chunkSize;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="PositionallyChunkedUnitSet"/> class.
    /// </summary>
    /// <param name="chunkSize">How big of a square each chunk of separation should be.</param>
    public PositionallyChunkedUnitSet(int chunkSize)
    {
      _chunkSize = chunkSize;
    }

    public void Add(UnitDataDto unit)
    {
      var chunkPosition = (unit.Position.X.RoundToNearestMultipleOf(_chunkSize), unit.Position.Y.RoundToNearestMultipleOf(_chunkSize));

      if (!_unitsByChunk.ContainsKey(chunkPosition))
        _unitsByChunk.Add(chunkPosition, new HashSet<UnitDataDto>());
        
      _unitsByChunk[chunkPosition].Add(unit);
    }

    public Dictionary<(int, int), HashSet<UnitDataDto>> GetAll() => _unitsByChunk;
  }
}