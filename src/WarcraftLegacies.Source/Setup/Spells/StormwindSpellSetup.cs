using MacroTools;
using MacroTools.PassiveAbilities;
using MacroTools.PassiveAbilitySystem;
using MacroTools.Spells;
using MacroTools.SpellSystem;
using WarcraftLegacies.Source.Spells.Slipstream;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Setup.Spells
{
  /// <summary>
  /// Responsible for setting up all Stormwind <see cref="Spell"/>s.
  /// </summary>
  public static class StormwindSpellSetup
  {
    /// <summary>
    /// Sets up all Stormwind <see cref="Spell"/>s.
    /// </summary>
    public static void Setup()
    {
      SpellSystem.Register(new SlipstreamSpell(Constants.ABILITY_A00D_SLIPSTREAM_STORMWIND_KHADGAR)
      {
        PortalUnitTypeId = Constants.UNIT_N0D9_SLIPSTREAM_PORTAL_STORMWIND_KHADGAR,
        OpeningDelay = 5,
        ClosingDelay = 10,
        Color = new Color(40, 40, 255, 255)
      });

      var legendaryWarrior = new ChannelSpellOnAttack(Constants.UNIT_H00R_KING_OF_STORMWIND_DARK_GREEN,
        Constants.ABILITY_A12C_LEGENDARY_WARRIOR_VARIAN)
      {
        DummyAbilityId = Constants.ABILITY_A12D_LEGENDARY_WARRIOR_STORMWIND_DUMMY,
        DummyOrderString = "voodoo",
        ProcChance = 0.15f,
        DurationBase = (int)0.5,
        DurationLevel = (int)0.5
      };
      PassiveAbilityManager.Register(legendaryWarrior);

      SpellSystem.Register(new AnySpellOnTarget(Constants.ABILITY_A12Z_RALLYING_BANNER_STORMWIND_DUMMY)
      {
        DummyAbilityId = Constants.ABILITY_A130_RESURRECTION_STORMWIND_CHAMPION_SINGLE,
        DummyAbilityOrderId = OrderId("resurrection"),
      });
    }
  }
}