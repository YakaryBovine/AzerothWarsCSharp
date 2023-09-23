using System.Collections.Generic;
using Launcher.Extensions;

namespace Launcher.DataTransferObjects
{
  /// <summary>
  /// Seperates a set of <see cref="DoodadDataDto"/>s by map chunks of a variable size.
  /// </summary>
  public sealed class PositionallyChunkedDoodadSet
  {
    private readonly Dictionary<(int, int), HashSet<DoodadDataDto>> _doodadsByChunk = new();

    private readonly int _chunkSize;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="PositionallyChunkedDoodadSet"/> class.
    /// </summary>
    /// <param name="chunkSize">How big of a square each chunk of separation should be.</param>
    public PositionallyChunkedDoodadSet(int chunkSize)
    {
      _chunkSize = chunkSize;
    }

    public void Add(DoodadDataDto doodad)
    {
      var chunkPosition = (doodad.Position.X.RoundToNearestMultipleOf(_chunkSize), doodad.Position.Y.RoundToNearestMultipleOf(_chunkSize));

      if (!_doodadsByChunk.ContainsKey(chunkPosition))
        _doodadsByChunk.Add(chunkPosition, new HashSet<DoodadDataDto>());
        
      _doodadsByChunk[chunkPosition].Add(doodad);
    }

    public Dictionary<(int, int), HashSet<DoodadDataDto>> GetAll() => _doodadsByChunk;
  }
}