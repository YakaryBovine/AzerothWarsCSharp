using MacroTools.PassiveAbilities;
using MacroTools.PassiveAbilitySystem;
using MacroTools.Spells;
using MacroTools.SpellSystem;

namespace WarcraftLegacies.Source.Setup.Spells
{
  /// <summary>
  /// Responsible for setting up all Fel Horde <see cref="Spell"/>s and <see cref="PassiveAbility"/>s.
  /// </summary>
  public static class TrollSpellSetup
  {
    /// <summary>
    /// Sets up <see cref="TrollSpellSetup"/>.
    /// </summary>
    public static void Setup()
    {

      var massFrostArmor = new MassAnySpell(ABILITY_A0H3_MASS_ICE_ARMOR_WARSONG_GAHZ_RILLA)
      {
        DummyAbilityId = ABILITY_A0H6_MASS_ICE_ARMOR_WARSONG_GAHZ_RILLA_DUMMY,
        DummyAbilityOrderId = OrderId("frostarmor"),
        Radius = 200,
        CastFilter = CastFilters.IsTargetAllyAndAlive,
        TargetType = SpellTargetType.Point
      };
      SpellSystem.Register(massFrostArmor);
    }
  }
}