using MacroTools;
using MacroTools.PassiveAbilities;
using MacroTools.PassiveAbilitySystem;

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
            Constants.UNIT_N0E7_BLOODWARDER_SUNFURY, 
            Constants.UNIT_H05Y_LORD_WIZARD_STORMWIND,
            Constants.UNIT_N0E8_SKYSHIP_SUNFURY
          }, Constants.ABILITY_A11N_ARCANE_ABSORPTION_SUNFURY_STORMWIND)
      {
        ManaPerDamage = new LeveledAbilityField<float>
        {
          Base = 0.20f,
          PerLevel = 0.20f
        },
        Effect = @"Abilities\Spells\Undead\ReplenishMana\SpiritTouchTarget.mdl"
      });
    }
  }
}
