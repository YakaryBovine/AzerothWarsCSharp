using MacroTools;
using MacroTools.PassiveAbilities;
using MacroTools.PassiveAbilitySystem;

namespace WarcraftLegacies.Source.Setup.Spells
{
  public static class WarsongSpellSetup
  {
    /// <summary>
    /// Sets up <see cref="WarsongSpellSetup"/>.
    /// </summary>
    public static void Setup()
    {
      PassiveAbilityManager.Register(new Execute(Constants.UNIT_O06L_WARLORD_OF_THE_WARSONG_CLAN_WARSONG)
      {
        DamageMultNonResistant = 4,
        DamageMultResistant = 1.5f,
        DamageMultStructure = 1
      });

      PassiveAbilityManager.Register(new Execute(Constants.UNIT_OGRH_CHIEFTAIN_OF_THE_WARSONG_CLAN_WARSONG)
      {
        DamageMultNonResistant = 4,
        DamageMultResistant = 1.5f,
        DamageMultStructure = 1
      });

      PassiveAbilityManager.Register(new RestoreManaFromDamage(Constants.UNIT_O06L_WARLORD_OF_THE_WARSONG_CLAN_WARSONG, Constants.ABILITY_A0ZG_BLOOD_ABSORPTION_GORFAX)
      {
        ManaPerDamage = new LeveledAbilityField<float>
        {
          Base = 0.25f,
          PerLevel = 0.25f
        },
        Effect = @"Abilities\Spells\Undead\ReplenishMana\SpiritTouchTarget.mdl"
      });
    }
  }
}