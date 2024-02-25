using System.Collections.Generic;
using MacroTools;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.Libraries;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.FactionMechanics.Scourge.Blight
{
  /// <summary>
  ///   Units can be registered to the <see cref="BlightSystem" /> so that when they are killed by allies of a specicic
  ///   <see cref="Faction" />, they spread a certain amount of blight around them.
  /// </summary>
  public static class BlightSystem
  {
    private static readonly Dictionary<unit, BlightParameters> BlightParameters = new();
    private static Faction? _blightFaction;

    private static bool _initialized;

    private static void UnitDies()
    {
      var triggerUnit = GetTriggerUnit();
      var parameters = BlightParameters[triggerUnit];

      if (_blightFaction?.Player?.GetTeam() is null || _blightFaction.Player is null ||
          !_blightFaction.Player.GetTeam()?.Contains(GetOwningPlayer(GetKillingUnit())) != true) return;
      GeneralHelpers.SetBlightRadius(_blightFaction.Player, new Point(GetUnitX(triggerUnit), GetUnitY(triggerUnit)),
        parameters.PrimaryBlightRadius, true);
      if (parameters.RandomBlightRectangle is null) return;
      for (var i = 0; i < parameters.RandomBlightCount; i++)
        GeneralHelpers.SetBlightRadius(_blightFaction.Player, parameters.RandomBlightRectangle.GetRandomPoint(),
          parameters.RandomBlightRadius, true);
    }

    /// <summary>
    ///   Causes the specified unit to create blight around it when it dies.
    /// </summary>
    /// <param name="whichUnit">The dying unit.</param>
    /// <param name="blightParameters">Instructions as to how much blight should be created and where.</param>
    /// <exception cref="SystemNotInitializedException">Thrown if the system hasn't been initialized.</exception>
    public static void Register(unit whichUnit, BlightParameters blightParameters)
    {
      if (!_initialized) throw new SystemNotInitializedException($"{nameof(BlightSystem)} has not been initialized.");
      if (BlightParameters.ContainsKey(whichUnit)) return;
      BlightParameters.Add(whichUnit, blightParameters);
      var deathTrigger = CreateTrigger();
      TriggerRegisterUnitEvent(deathTrigger, whichUnit, EVENT_UNIT_DEATH);
      TriggerAddAction(deathTrigger, UnitDies);
    }

    public static void Setup(Faction blightFaction)
    {
      if (_initialized)
        throw new SystemAlreadyInitializedException($"{nameof(BlightSystem)} has already been initialized.");

      _blightFaction = blightFaction;
      _initialized = true;
    }
  }
}