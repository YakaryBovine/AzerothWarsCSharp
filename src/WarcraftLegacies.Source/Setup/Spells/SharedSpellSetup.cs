using MacroTools;
using MacroTools.PassiveAbilities;
using MacroTools.PassiveAbilitySystem;
using WarcraftLegacies.Shared;
using WarcraftLegacies.Shared.Models.Abilities;

namespace WarcraftLegacies.Source.Setup.Spells
{
  public static class SharedSpellSetup
  {
    public static void Setup()
    {
      PassiveAbilityManager.Register(
        new RestoreManaFromDamage(
          new[]
          {
            UNIT_N0E7_BLOODWARDER_SUNFURY, 
            UNIT_H05Y_LORD_WIZARD_STORMWIND,
            UNIT_N0E8_SKYSHIP_SUNFURY
          }, ABILITY_A11N_ARCANE_ABSORPTION_SUNFURY_STORMWIND)
      {
        ManaPerDamage = new LeveledAbilityField<float>
        {
          Base = 0.20f,
          PerLevel = 0.20f
        },
        Effect = @"Abilities\Spells\Undead\ReplenishMana\SpiritTouchTarget.mdl"
      });

      PassiveAbilityManager.Register(new ProvidesIncome(UNIT_NBOT_GOBLIN_PRIVATEER_NEUTRAL_GOBLIN, -2));
    }
  }
}
