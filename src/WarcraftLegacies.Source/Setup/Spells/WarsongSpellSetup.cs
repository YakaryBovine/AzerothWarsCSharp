using MacroTools;
using MacroTools.PassiveAbilities;
using MacroTools.PassiveAbilitySystem;
using MacroTools.SpellSystem;
using WarcraftLegacies.Shared;
using WarcraftLegacies.Shared.Models.Abilities;
using WarcraftLegacies.Source.Spells;

namespace WarcraftLegacies.Source.Setup.Spells
{
  public static class WarsongSpellSetup
  {
    /// <summary>
    /// Sets up <see cref="WarsongSpellSetup"/>.
    /// </summary>
    public static void Setup()
    {
      PassiveAbilityManager.Register(new Execute(UNIT_O06L_WARLORD_OF_THE_WARSONG_CLAN_WARSONG)
      {
        DamageMultNonResistant = 4,
        DamageMultResistant = 1.5f,
        DamageMultStructure = 1
      });

      PassiveAbilityManager.Register(new Execute(UNIT_OGRH_CHIEFTAIN_OF_THE_WARSONG_CLAN_WARSONG)
      {
        DamageMultNonResistant = 4,
        DamageMultResistant = 1.5f,
        DamageMultStructure = 1
      });

      PassiveAbilityManager.Register(new RestoreManaFromDamage(UNIT_O06L_WARLORD_OF_THE_WARSONG_CLAN_WARSONG, ABILITY_A0ZG_BLOOD_ABSORPTION_GORFAX)
      {
        ManaPerDamage = new LeveledAbilityField<float>
        {
          Base = 0.25f,
          PerLevel = 0.25f
        },
        Effect = @"Abilities\Spells\Undead\ReplenishMana\SpiritTouchTarget.mdl"
      });

      var stormEarthandFire = new StormEarthandFire(ABILITY_A0HM_STORM_EARTH_AND_FIRE_WARSONG_CHEN_SUMMON)
      {
        UnitType1 = FourCC("npn4"),
        UnitType2 = FourCC("npn5"),
        UnitType3 = FourCC("npn6"),
        Duration = 60.0F,
        EffectTarget = @"Abilities\Spells\Items\AIil\AIilTarget.mdl",
        EffectScaleTarget = 1.0F,
        HealthBonusBase = -0.15F,
        HealthBonusLevel = 0.15F,
        DamageBonusBase = -0.15F,
        DamageBonusLevel = 0.15F
      };
      SpellSystem.Register(stormEarthandFire);
      //Todo: inappropriately named
    }
  }
}