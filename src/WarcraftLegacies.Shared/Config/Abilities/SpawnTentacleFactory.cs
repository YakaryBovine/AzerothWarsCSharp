using WarcraftLegacies.Shared.Models.Abilities;

namespace WarcraftLegacies.Shared.Config.Abilities
{
  public static class SpawnTentacleFactory
  {
    public static SpawnTentacleModel CthunSpawnTentacle()
    {
      return new SpawnTentacleModel
      {
        Name = "Spawn Tentacle",
        SummonedUnitTypeId = Constants.UNIT_N073_TENTACLE_C_THUN,
        HitPoints = new LeveledAbilityField<int>
        {
          Base = 750,
          PerLevel = 250
        },
        DamageBase = new LeveledAbilityField<int>
        {
          Base = 30,
          PerLevel = 30
        },
        ManaCost = 150,
        Cooldown = 15,
        Abilities = new int[]
        {
          Constants.ABILITY_A103_MASSIVE_ATTACK_DRAENEI
        }
      };
    }
  }
}