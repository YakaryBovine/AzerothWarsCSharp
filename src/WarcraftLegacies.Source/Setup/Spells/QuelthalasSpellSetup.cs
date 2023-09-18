using System.Collections.Generic;
using MacroTools;
using MacroTools.PassiveAbilities;
using MacroTools.PassiveAbilitySystem;
using MacroTools.Spells;
using MacroTools.SpellSystem;
using WarcraftLegacies.Source.Spells;

namespace WarcraftLegacies.Source.Setup.Spells
{
  public static class QuelthalasSpellSetup
  {
    public static void Setup()
    {
      var summonGraniteGolems = new SummonUnits(Constants.ABILITY_A0EP_SUMMON_GRANITE_GOLEMS_QUEL_THALAS_SUNWELL)
      {
        SummonUnitTypeId = Constants.UNIT_NGGR_GRANITE_GOLEM_QUEL_THALAS,
        SummonCount = 4,
        Duration = 60,
        Radius = 400,
        AngleOffset = 45,
        Effect = @"war3mapImported\Earth NovaTarget.mdx"
      };
      SpellSystem.Register(summonGraniteGolems);

      var resurgentFlameStrike = new ResurgentSpell(Constants.ABILITY_A04H_RESURGENT_FLAME_STRIKE_QUEL_THALAS_KAEL_THAS,
        Constants.ABILITY_A0F9_RESURGENT_FLAME_STRIKE_QUEL_THALAS_KAEL_THAS_DUMMY, "flamestrike")
      {
        Duration = 14,
        Interval = 7
      };
      SpellSystem.Register(resurgentFlameStrike);
      
      var massBanish = new MassAnySpell(Constants.ABILITY_A0FD_MASS_BANISH_QUEL_THALAS_KAEL_THAS)
      {
        DummyAbilityId = Constants.ABILITY_A0FE_MASS_BANISH_QUEL_THALAS_KAEL_THAS_DUMMY_CASTER,
        DummyAbilityOrderString = "banish",
        Radius = 250,
        CastFilter = CastFilters.IsTargetOrganicAndAlive,
        TargetType = SpellTargetType.Point
      };
      SpellSystem.Register(massBanish);
      
      var siphoningRitual = new SiphoningRitualSpell(Constants.ABILITY_A0FA_SIPHONING_RITUAL_QUEL_THALAS_KAEL_THAS)
      {
        TargetCountBase = 24,
        TargetCountLevel = 0,
        LifeDrainedPerSecondBase = 30,
        LifeDrainedPerSecondLevel = 10,
        ManaDrainedPerSecondBase = 15,
        ManaDrainedPerSecondLevel = 5,
        Range = 800,
        Radius = 225,
        Interval = 0.1f
      };
      SpellSystem.Register(siphoningRitual);
      
      PassiveAbilityManager.Register(new DefensiveOrbs(Constants.UNIT_H00Q_KING_OF_QUEL_THALAS_QUEL_THALAS, Constants.ABILITY_A055_DEFENSIVE_ORBS_QUEL_THALAS_ANASTERIAN)
      {
        OrbitRadius = 350,
        OrbitalPeriod = 4,
        OrbEffectPath = @"war3mapImported\OrbFireX.mdx",
        Damage = new LeveledAbilityField<float> { Base = 25, PerLevel = 25 },
        CollisionRadius = new LeveledAbilityField<float> { Base = 100, PerLevel = 0},
        OrbDuration = 30,
        AbilityWhitelist = new List<int>
        {
          Constants.ABILITY_A04J_ARCANE_BURST_HIGH_ELVES_ANASTERIAN,
          Constants.ABILITY_A013_DEVOUR_MAGIC_GUL_DAN,
          Constants.ABILITY_AHPX_ASHES_OF_AL_AR_QUEL_THALAS_ANASTERIAN_KAEL_THAS
        }
      });

      PassiveAbilityManager.Register(new RestoreManaFromDamage(Constants.UNIT_N0E7_BLOODWARDER_SUNFURY, Constants.ABILITY_A11N_ARCANE_ABSORPTION_SUNFURY_STORMWIND)
      {
        ManaPerDamage = new LeveledAbilityField<float>
        {
          Base = 0.20f,
          PerLevel = 0.20f
        },
        Effect = @"Abilities\Spells\Undead\ReplenishMana\SpiritTouchTarget.mdl"
      });

      SpellSystem.Register(new RegrowTrees(Constants.ABILITY_A12L_REGROW_TREES_DOMES)
      {
        Radius = 1500
      });

      //Todo: create an "Extract Vial" spell for the Sunwell and assign it below
      SpellSystem.Register(new ExtractSunwellVial(Constants.ABILITY_A0OC_EXTRACT_VIAL_ALL, Constants.ITEM_I018_VIAL_OF_THE_SUNWELL));
    }
  }
}