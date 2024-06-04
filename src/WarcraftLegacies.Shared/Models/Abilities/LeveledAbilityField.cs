namespace WarcraftLegacies.Shared.Models.Abilities
{
  /// <summary>
  /// Represents the value of a leveled ability field, such as damage or duration.
  /// </summary>
  public sealed class LeveledAbilityField<T> where T : struct
  {
    /// <summary>
    /// The base value of the field.
    /// </summary>
    public T Base { get; init; }
    
    /// <summary>
    /// The value of the field which gets multiplied by a hero's level.
    /// </summary>
    public T PerLevel { get; init; }
  }
}