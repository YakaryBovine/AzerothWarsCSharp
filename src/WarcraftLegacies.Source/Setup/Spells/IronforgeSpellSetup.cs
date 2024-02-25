using MacroTools.PassiveAbilities;
using MacroTools.PassiveAbilitySystem;
using MacroTools.SpellSystem;
using WarcraftLegacies.Source.Spells;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Setup.Spells
{
  public static class IronforgeSpellSetup
  {
    /// <summary>
    /// Sets up <see cref="IronforgeSpellSetup"/>.
    /// </summary>
    public static void Setup()
    {
      SpellSystem.Register(new TitanForgeArtifact(Constants.ABILITY_A0T3_TITANFORGE_ARTIFACT_IRONFORGE, 0, 75));

      var lightningAttack = new SpellOnAttack(Constants.UNIT_H03Z_STORMRIDER_IRONFORGE,
        Constants.ABILITY_A10J_MASTER_OF_LIGHTNING_STORMRIDERS)
      {
        DummyAbilityId = Constants.ABILITY_ACFL_FORKED_LIGHTNING_LIGHT_BLUE_HIGHBORNE,
        DummyOrderId = OrderId("forkedlightning"),
        ProcChance = 0.15f
      };
      PassiveAbilityManager.Register(lightningAttack);
    }
  }
}