using AutoMapper;
using Launcher.DataTransferObjects;
using War3Net.Build.Widget;

namespace Launcher.ValueResolvers
{
  /// <summary>
  /// MapData stores Unit positions without their height, since that can be recalculated from their position
  /// on the terrain.
  /// </summary>
  public sealed class UnitDataDtoZPositionValueResolver : IValueResolver<UnitData, UnitDataDto, Vector2Dto>
  {
    /// <inheritdoc />
    public Vector2Dto Resolve(UnitData source, UnitDataDto destination, Vector2Dto destMember, ResolutionContext context)
    {
      return new Vector2Dto
      {
        X = source.Position.X,
        Y = source.Position.Y
      };
    }
  }
}