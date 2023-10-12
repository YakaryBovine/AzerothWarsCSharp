using MacroTools;
using MacroTools.PassiveAbilities;
using MacroTools.PassiveAbilitySystem;
using MacroTools.ResearchSystems;
using MacroTools.Spells;
using MacroTools.Spells.Slipstream;
using MacroTools.SpellSystem;
using WarcraftLegacies.Source.Spells;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Setup.Spells
{
  public static class FrostwolfSpellSetup
  {
    public static void Setup()
    {
      var devour = new Devour(Constants.ABILITY_ADEV_DEVOUR_KODO_BEAST)
      {
        PercentageOfMaxHealth = 0.5f
      };
      SpellSystem.Register(devour);
      
      var warStompCairne = new Stomp(Constants.ABILITY_A0WM_WAR_STOMP_CAIRNE_MANNOROTH)
      {
        Radius = 300,
        DamageBase = 20,
        DamageLevel = 30,
        DurationBase = 0,
        DurationLevel = 1,
        StunAbilityId = Constants.ABILITY_A0WN_STUN_UNIT_DUMMY,
        StunOrderString = "thunderbolt",
        SpecialEffect = @"Abilities\Spells\Orc\WarStomp\WarStompCaster.mdl"
      };
      SpellSystem.Register(warStompCairne);

      var CripplingShout = new Stomp(Constants.ABILITY_TP07_CRIPPLING_SHOUT_FROSTWOLF)
      {
        Radius = 700,
        DamageBase = 00,
        DamageLevel = 00,
        DurationBase = 15,
        StunAbilityId = Constants.ABILITY_TP08_CRIPPLE_DUMMY,
        StunOrderString = "cripple",
        SpecialEffect = @"abilities\spells\nightelf\battleroar\roarcaster.mdx"
      };
      SpellSystem.Register(CripplingShout);

      ParentChildResearchSystem.Register(Constants.UPGRADE_RHME_PYRITE_FORGED_WEAPONRY_UNIVERSAL_UPGRADE,
        Constants.UPGRADE_R06C_KABOOM_LEVEL_UP);

      SpellSystem.Register(new SlipstreamSpellSpecificLocation(Constants.ABILITY_A0ZJ_PORTAL_TO_NAGRAND_ITEM)
      {
        PortalUnitTypeId = Constants.UNIT_N0D9_SLIPSTREAM_PORTAL_STORMWIND_KHADGAR,
        OpeningDelay = 5,
        ClosingDelay = 10,
        TargetLocation = new Point(-3169, -29714),
        Color = new Color(255, 50, 50, 255)
      });
      
      SpellSystem.Register(new AncestralLegion(Constants.ABILITY_A0YX_ANCESTRAL_LEGION_FROSTWOLF_CAIRNE)
      {
        Duration = 60,
        HealthBonus = new LeveledAbilityField<float>
        {
          Base = 0.2f,
          PerLevel = 0.1f
        },
        DamageBonus = new LeveledAbilityField<float>
        {
          Base = 0.2f,
          PerLevel = 0.1f
        },
        SummonCap = new LeveledAbilityField<int>
        {
          Base = 6,
          PerLevel = 6
        },
        RememberChance = 1f,
        RememberableUnitTypeId = Constants.UNIT_OTAU_TAUREN_FROSTWOLF,
        SummonEffect = @"Abilities\Spells\Demon\DarkPortal\DarkPortalTarget.mdl",
        DeathEffect = @"Abilities\Spells\Orc\Disenchant\DisenchantSpecialArt.mdl"
      });
    }
  }
}