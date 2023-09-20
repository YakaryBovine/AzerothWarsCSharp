using System.Numerics;
using AutoMapper;
using Launcher.DataTransferObjects;
using War3Net.Build.Widget;

namespace Launcher.ValueResolvers
{
  /// <summary>
  /// MapData stores Unit positions without their height, since that can be recalculated from their position
  /// on the terrain.
  /// </summary>
  public sealed class UnitDataZPositionValueResolver : IValueResolver<UnitDataDto, UnitData, Vector3>
  {
    /// <inheritdoc />
    public Vector3 Resolve(UnitDataDto source, UnitData destination, Vector3 destMember, ResolutionContext context)
    {
      return new Vector3
      {
        X = source.Position.X,
        Y = source.Position.Y
      };
    }
  }
}