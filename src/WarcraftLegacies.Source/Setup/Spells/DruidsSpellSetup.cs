using MacroTools;
using MacroTools.PassiveAbilities;
using MacroTools.PassiveAbilitySystem;
using MacroTools.SpellSystem;
using WarcraftLegacies.Shared;
using WarcraftLegacies.Shared.Models.Abilities;
using WarcraftLegacies.Source.Spells;
using WarcraftLegacies.Source.Spells.MassiveAttack;

namespace WarcraftLegacies.Source.Setup.Spells
{
  public static class DruidsSpellSetup
  {
    public static void Setup()
    {
      SpellSystem.Register(new Devour(ABILITY_A0NP_DEVOUR_TORTOLLA)
      {
        PercentageOfMaxHealth = 0.5f,
        Damage = new LeveledAbilityField<float>
        {
          Base = 100,
          PerLevel = 100
        }
      });

      SpellSystem.Register(new Devour(ABILITY_A0S0_DEVOUR_OURO)
      {
        PercentageOfMaxHealth = 0.5f,
        Damage = new LeveledAbilityField<float>
        {
          Base = 100,
          PerLevel = 100
        }
      });
      
      PassiveAbilityManager.Register(new MassiveAttackAbility(UNIT_E00A_ANCIENT_GUARDIAN_DRUIDS)
      {
        AttackDamagePercentage = 0.5f,
        Distance = 700
      });
    }
  }
}