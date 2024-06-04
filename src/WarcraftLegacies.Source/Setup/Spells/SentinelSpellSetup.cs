using System.Collections.Generic;
using MacroTools;
using MacroTools.PassiveAbilities;
using MacroTools.PassiveAbilitySystem;
using MacroTools.Spells;
using MacroTools.SpellSystem;
using WarcraftLegacies.Shared;
using WarcraftLegacies.Shared.Models.Abilities;
using WarcraftLegacies.Source.PassiveAbilities.Vengeance;

namespace WarcraftLegacies.Source.Setup.Spells
{
  public static class SentinelSpellSetup
  {
    public static void Setup()
    { 
      PassiveAbilityManager.Register(new DefensiveOrbs(UNIT_E025_LIEUTENANT_OF_THE_WATCHERS_SENTINELS, ABILITY_A10A_GLAIVE_STORM_ICON_NAISHA)
      {
        OrbitRadius = 350,
        OrbitalPeriod = 4,
        OrbEffectPath = @"war3mapImported\ArcaneGlaive_2.mdx",
        Damage = new LeveledAbilityField<float> { Base = 25, PerLevel = 25 },
        CollisionRadius = new LeveledAbilityField<float> { Base = 150, PerLevel = 0},
        OrbDuration = 20,
        AbilityWhitelist = new List<int>
        {
          ABILITY_A0FC_BARBED_NET_NAISHA,
          ABILITY_A0MG_QUICK_KNIVES_NAISHA,
        }
      });

      var maievVengeance = new VengeanceAbility(UNIT_EWRD_LEADER_OF_THE_WATCHERS_SENTINELS,
  ABILITY_A017_TAKE_VENGEANCE_SENTINELS_MAIEV)
      {
        AlternateFormId = UNIT_ESPV_AVATAR_OF_VENGEANCE_SENTINELS_MAIEV,
        HitsReviveThreshold = 9,
        HealBase = 900,
        HealLevel = 300,
        BonusDamageBase = 20,
        BonusDamageLevel = 20,
        Duration = 20,
        ReviveEffect = "Heal Blue.mdx"
      };
      PassiveAbilityManager.Register(maievVengeance);

      var elunesGaze = new MassAnySpell(ABILITY_ASEG_ELUNE_S_GAZE_SENTINELS_REAL)
      {
        DummyAbilityId = ABILITY_A0VY_INVISIBILITY_LB,
        DummyAbilityOrderId = OrderId("invisibility"),
        Radius = 350,
        CastFilter = CastFilters.IsTargetOrganicAndAlive,
        TargetType = SpellTargetType.None
      };
      SpellSystem.Register(elunesGaze);

    }
  }
}