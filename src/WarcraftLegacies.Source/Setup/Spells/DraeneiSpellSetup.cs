using MacroTools;
using MacroTools.PassiveAbilitySystem;
using MacroTools.Spells;
using MacroTools.SpellSystem;
using WarcraftLegacies.Source.Spells.MassiveAttack;
using WarcraftLegacies.Source.Spells.Slipstream;
using WCSharp.Shared.Data;

namespace WarcraftLegacies.Source.Setup.Spells
{
  /// <summary>
  /// Responsible for setting up all Draenei <see cref="Spell"/>s.
  /// </summary>
  public static class DraeneiSpellSetup
  {
    /// <summary>
    /// Sets up all Draenei <see cref="Spell"/>s.
    /// </summary>
    public static void Setup()
    {
      var slipstreamOrigin = new Point(-22259.9f, 7104.2f);
      
      //Azuremyst
      SpellSystem.Register(new SlipstreamSpellSpecificOriginAndDestination(ABILITY_A0P9_PORTAL_TO_AZUREMYST_DRAENEI)
      {
        PortalUnitTypeId = UNIT_N0D9_SLIPSTREAM_PORTAL_STORMWIND_KHADGAR,
        OpeningDelay = 20,
        ClosingDelay = 10,
        OriginLocation = slipstreamOrigin,
        TargetLocation = new Point(-20940, 10412),
        Color = new Color(155, 250, 50, 255)
      });

      //Argus
      SpellSystem.Register(new SlipstreamSpellSpecificOriginAndDestination(ABILITY_A0RB_PORTAL_TO_ARGUS_DRAENEI)
      {
        PortalUnitTypeId = UNIT_N0D9_SLIPSTREAM_PORTAL_STORMWIND_KHADGAR,
        OpeningDelay = 20,
        ClosingDelay = 10,
        OriginLocation = slipstreamOrigin,
        TargetLocation = new Point(22010, -26816),
        Color = new Color(255, 50, 50, 255)
      });

      //Outland
      SpellSystem.Register(new SlipstreamSpellSpecificOriginAndDestination(ABILITY_A0SR_PORTAL_TO_TEMPEST_KEEP_DRAENEI)
      {
        PortalUnitTypeId = UNIT_N0D9_SLIPSTREAM_PORTAL_STORMWIND_KHADGAR,
        OpeningDelay = 20,
        ClosingDelay = 10,
        OriginLocation = slipstreamOrigin,
        TargetLocation = new Point(2943, -21644),
        Color = new Color(55, 50, 250, 255)
      });
      
      PassiveAbilityManager.Register(new MassiveAttackAbility(UNIT_N0CX_LIGHTFORGED_WARFRAME_DRAENEI)
      {
        AttackDamagePercentage = 0.3f,
        Distance = 700
      });

      var warStompAdal = new Stomp(ABILITY_A105_BLINDING_STARLIGHT_ADAL)
      {
        Radius = 1000,
        DamageBase = 00,
        DamageLevel = 00,
        DurationBase = 6,
        DurationLevel = 3,
        StunAbilityId = ABILITY_A106_CURSE_DRAENEI,
        StunOrderId = OrderId("curse"),
        SpecialEffect = @"war3mapImported\FrostNova.mdx"
      };
      SpellSystem.Register(warStompAdal);

      var summonGateway = new SummonUnitsTarget(ABILITY_A0LX_GATEWAY_REINFORCEMENTS_DRAENEI)
      {
        SummonUnitTypeId = UNIT_O05B_DEFENDER_DRAENEI,
        SummonCount = 1,
        Radius = 50,
        Duration = 0,
      };
      SpellSystem.Register(summonGateway);

      var manaSyphon2 = new GrantMana(ABILITY_ADMS_RESTORE_MANA_DRAENEI_CASTER_BUILDING)
      {
        ManaToGrant = 240
      };
      SpellSystem.Register(manaSyphon2);
    }
  }
}