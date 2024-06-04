namespace WarcraftLegacies.Shared.Models.Abilities
{
  public abstract class ActivatedAbilityModel
  {
    public required int ManaCost { get; init; }

    public required int Cooldown { get; init; }
  }
}