namespace WarcraftLegacies.Shared.Models.Abilities
{
  /// <summary>
  /// Summons a Tentacle at a target location.
  /// </summary>
  public sealed class SpawnTentacleModel : ActivatedAbilityModel
  {
    /// <summary>
    /// The name for the ability that the player sees.
    /// </summary>
    public string Name { get; init; }
    
    /// <summary>
    /// The unit type ID of the Tentacle.
    /// </summary>
    public required int SummonedUnitTypeId { get; init; }
    
    /// <summary>
    /// How many hit points the summoned Tentacle has.
    /// </summary>
    public required LeveledAbilityField<int> HitPoints { get; init; }
    
    /// <summary>
    /// How many base damage the Tentacle has.
    /// </summary>
    public required LeveledAbilityField<int> DamageBase { get; init; }
    
    /// <summary>
    /// Extra abilities to give the spawned Tentacle.
    /// </summary>
    public required IEnumerable<int> Abilities { get; init; }
  }
}