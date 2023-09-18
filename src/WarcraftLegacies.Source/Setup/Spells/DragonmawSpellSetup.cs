using System.Collections.Generic;
using static War3Api.Common;
using MacroTools;
using MacroTools.PassiveAbilities;
using MacroTools.PassiveAbilitySystem;
using MacroTools.Spells;
using MacroTools.SpellSystem;

namespace WarcraftLegacies.Source.Setup.Spells
{
  /// <summary>
  /// Responsible for setting up all Dragonmaw <see cref="Spell"/>s and <see cref="PassiveAbility"/>s.
  /// </summary>
  public static class DragonmawSpellSetup
  {
    /// <summary>
    /// Sets up all Dragonmaw <see cref="Spell"/>s and <see cref="PassiveAbility"/>s.
    /// </summary>
    public static void Setup()
    {
      SpellSystem.Register(new MassAnySpell(Constants.ABILITY_A0OJ_MASS_UNHOLY_ARMOR_DRAGONMAW_GORFAX)
      {
        DummyAbilityId = Constants.ABILITY_A0HG_UNHOLY_ARMOR_DRAGONMAW_GORFAX,
        DummyAbilityOrderString = "innerfire",
        Radius = 400,
        CastFilter = CastFilters.IsTargetAllyAndAlive,
        TargetType = SpellTargetType.Point
      });
      
      SpellSystem.Register(new UnholyArmor(Constants.ABILITY_A0HG_UNHOLY_ARMOR_DRAGONMAW_GORFAX)
      {
        PercentageDamage = 0.06f
      });

      PassiveAbilityManager.Register(new RestoreManaFromDamage(Constants.UNIT_O06F_BLOOD_WARRIOR_DRAGONMAW, Constants.ABILITY_A0FR_TRANSFUSION_GORFAX)
      {
        ManaPerDamage = new LeveledAbilityField<float>
        {
          Base = 0.25f,
          PerLevel = 0.25f
        },
        Effect = @"Abilities\Spells\Undead\ReplenishMana\SpiritTouchTarget.mdl"
      });

      PassiveAbilityManager.Register(new LocationBasedFlavourAbility(Constants.UNIT_O05J_DRAGON_HATCHERY_DRAGONMAW_SPECIALIST)
      {
        LocationBasedFlavourSettings = new List<LocationBasedFlavourSetting>
        {
          new("Green Dragon Hatchery", FourCC("ndrg"), Regions.AshenvaleAmbient.Center),
          new("Red Dragon Hatchery", FourCC("ndrr"), Regions.Stormwind.Center),
          new("Blue Dragon Hatchery", FourCC("ndru"), Regions.Central_Northrend.Center),
          new("Nether Dragon Hatchery", FourCC("ndro"), Regions.InstanceOutland.Center),
          new("Bronze Dragon Hatchery", FourCC("ndrz"), Regions.Zulfarrak.Center),
          new("Green Dragon Hatchery", FourCC("ndrg"), Regions.LordaeronAmbient2.Center)
        }
      });

      PassiveAbilityManager.Register(new LocationBasedFlavourAbility(Constants.UNIT_N0CP_ENSLAVED_DRAGON_DRAGONMAW_BASE)
      {
        LocationBasedFlavourSettings = new List<LocationBasedFlavourSetting>
        {
          new("Enslaved Green Dragon", Constants.UNIT_N0DI_ENSLAVED_GREEN_DRAGON_DRAGONMAW, Regions.AshenvaleAmbient.Center),
          new("Enslaved Red Dragon", Constants.UNIT_N0CP_ENSLAVED_DRAGON_DRAGONMAW_BASE, Regions.Stormwind.Center),
          new("Enslaved Blue Dragon", Constants.UNIT_N0AQ_ENSLAVED_BLUE_DRAGON_DRAGONMAW, Regions.Central_Northrend.Center),
          new("Enslaved Nether Dragon", Constants.UNIT_N0DH_ENSLAVED_NETHER_DRAGON_DRAGONMAW, Regions.InstanceOutland.Center),
          new("Enslaved Bronze Dragon", Constants.UNIT_N0DG_ENSLAVED_BRONZE_DRAGON_DRAGONMAW, Regions.Zulfarrak.Center),
          new("Enslaved Green Dragon", Constants.UNIT_N0DI_ENSLAVED_GREEN_DRAGON_DRAGONMAW, Regions.LordaeronAmbient2.Center)
        }
      });
    }
  }
}