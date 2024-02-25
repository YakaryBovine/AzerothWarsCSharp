using MacroTools;
using MacroTools.Extensions;
using WCSharp.Events;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.FactionMechanics.Fel_Horde
{
  /// <summary>
  /// Fel Horde's Juggernaut towers start invulnerable and can be destroyed by destroying their corresponding generator.
  /// </summary>
  internal static class JuggernautDeath
  {
    /// <summary>
    /// Sets up <see cref="JuggernautDeath"/>.
    /// </summary>
    public static void Setup(PreplacedUnitSystem preplacedUnitSystem)
    {
      var powerGenerator1 = preplacedUnitSystem.GetUnit(Constants.UNIT_NPGR_POWER_GENERATOR_TEAL, new Point(5511.9f, -29688.2f));
      var powerGenerator2 = preplacedUnitSystem.GetUnit(Constants.UNIT_NPGR_POWER_GENERATOR_TEAL, new Point(5513.1f, -30467.4f));
      var inferJuggernaut1 = preplacedUnitSystem.GetUnit(Constants.UNIT_N066_INFERNAL_JUGGERNAUT_TEAL_TOWER, new Point(4356.6f, -29397.8f));
      var inferJuggernaut2 = preplacedUnitSystem.GetUnit(Constants.UNIT_N066_INFERNAL_JUGGERNAUT_TEAL_TOWER, new Point(4245.3f, -29863.1f));
      var inferJuggernaut3 = preplacedUnitSystem.GetUnit(Constants.UNIT_N066_INFERNAL_JUGGERNAUT_TEAL_TOWER, new Point(4537.8f, -30988.1f));
      var inferJuggernaut4 = preplacedUnitSystem.GetUnit(Constants.UNIT_N066_INFERNAL_JUGGERNAUT_TEAL_TOWER, new Point(4631.4f, -31433.0f));

      SetupPowerGenerators(powerGenerator1, inferJuggernaut1, inferJuggernaut2);
      SetupPowerGenerators(powerGenerator2, inferJuggernaut3, inferJuggernaut4);
    }

    private static void SetupPowerGenerators(unit powerGenerator, params unit[] dependentJuggernauts)
    {
      PlayerUnitEvents.Register(UnitEvent.Dies, () =>
      {
        foreach (var juggernaut in dependentJuggernauts)
          juggernaut.Kill();
      }, powerGenerator);

      PlayerUnitEvents.Register(UnitEvent.ChangesOwner, () =>
      {
        foreach (var juggernaut in dependentJuggernauts)
          juggernaut.SetOwner(GetTriggerUnit().OwningPlayer());
      }, powerGenerator);
    }
  }
}