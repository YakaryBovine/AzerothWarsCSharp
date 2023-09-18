using MacroTools;
using MacroTools.Extensions;
using MacroTools.ResearchSystems;
using static War3Api.Common;
using WarcraftLegacies.Source.Setup.FactionSetup;
using WCSharp.Events;
using WCSharp.Shared.Data;

namespace WarcraftLegacies.Source.Researches
{
  /// <summary>
  /// When flight path is researched, the two flight path waygates at Orgrimmar and Thunder Bluff become active
  /// and connected to eachother.
  /// </summary>
  public sealed class FlightPath : Research
  {
    private const int ResearchId = Constants.UPGRADE_R09N_FLIGHT_PATH_WARSONG;
    private static unit? _flightToOrgrimmar;
    private static unit? _flightToThunderBluff;
    private static bool _researched;

    /// <inheritdoc />
    public FlightPath(int researchTypeId, int goldCost, int lumberCost, PreplacedUnitSystem preplacedUnitSystem) : base(researchTypeId, goldCost, lumberCost)
    {
      var orgrimmarLocation = new Point(-9704, -858);
      var thunderbluffLocation = new Point(-14445, -4042);
      _flightToOrgrimmar = preplacedUnitSystem.GetUnit(Constants.UNIT_N06Z_FLIGHT_PATH_FROSTWOLF_WARSONG, thunderbluffLocation);
      _flightToThunderBluff = preplacedUnitSystem.GetUnit(Constants.UNIT_N06Z_FLIGHT_PATH_FROSTWOLF_WARSONG, orgrimmarLocation);
    }

    /// <inheritdoc />
    public override void OnResearch(player researchingPlayer)
    {
      if (_researched)
      {
        Refund(researchingPlayer, false);
        return;
      }
      
      var recipient = WarsongSetup.WarsongClan?.Player ?? FrostwolfSetup.Frostwolf?.Player;
      if (recipient == null)
      {
        _flightToOrgrimmar?.Kill();
        _flightToThunderBluff?.Kill();
        return;
      }

      _flightToOrgrimmar?
        .SetOwner(recipient)
        .SetWaygateDestination(Regions.OrgrimmarFlight.Center)
        .SetInvulnerable(false);

      _flightToThunderBluff?
        .SetOwner(recipient)
        .SetWaygateDestination(Regions.ThunderbluffFlight.Center)
        .SetInvulnerable(false);

      FrostwolfSetup.Frostwolf?.SetObjectLevel(ResearchId, 1);
      WarsongSetup.WarsongClan?.SetObjectLevel(ResearchId, 1);
      _researched = true;
    }

    /// <inheritdoc />
    public override void OnRegister()
    {
      PlayerUnitEvents.Register(UnitEvent.Dies, () => { _flightToThunderBluff?.Kill(); }, _flightToOrgrimmar);
      PlayerUnitEvents.Register(UnitEvent.Dies, () => { _flightToOrgrimmar?.Kill(); }, _flightToThunderBluff);
    }
  }
}