using MacroTools;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.ResearchSystems;
using static War3Api.Common;
using WCSharp.Events;
using WCSharp.Shared.Data;

namespace WarcraftLegacies.Source.Researches.Ironforge
{
  /// <summary>
  /// When Deeprun Tram is researched, the two Deeprun Tram waygates at Ironforge and Stormwind Keep become active
  /// and connected to eachother.
  /// </summary>
  public sealed class DeeprunTram : Research
  {
    private readonly Faction _ironforge;
    private readonly Faction _stormwind;
    private const int ResearchId = Constants.UPGRADE_R014_DEEPRUN_TRAM_IRONFORGE;
    private static unit? _tramToIronforge;
    private static unit? _tramToStormwind;
    private static bool _researched;

    /// <inheritdoc />
    public DeeprunTram(Faction ironforge, Faction stormwind, int researchTypeId, int goldCost, int lumberCost, PreplacedUnitSystem preplacedUnitSystem) : base(researchTypeId, goldCost, lumberCost)
    {
      _ironforge = ironforge;
      _stormwind = stormwind;
      var ironforgeLocation = new Point(9761, -5723);
      var stormwindLocation = new Point(11126, -9970);
      _tramToIronforge = preplacedUnitSystem.GetUnit(Constants.UNIT_N03B_DEEPRUN_TRAM, stormwindLocation);
      _tramToStormwind = preplacedUnitSystem.GetUnit(Constants.UNIT_N03B_DEEPRUN_TRAM, ironforgeLocation);
    }

    /// <inheritdoc />
    public override void OnResearch(player researchingPlayer)
    {
      if (_researched)
      {
        Refund(researchingPlayer, false);
        return;
      }
      
      var recipient = _ironforge.Player ?? _stormwind.Player;
      if (recipient == null)
      {
        _tramToIronforge?.Kill();
        _tramToStormwind?.Kill();
        return;
      }
      
      _tramToIronforge?
        .SetOwner(recipient)
        .SetWaygateDestination(Regions.Ironforge.Center)
        .SetInvulnerable(false);

      _tramToStormwind?
        .SetOwner(recipient)
        .SetWaygateDestination(Regions.Stormwind.Center)
        .SetInvulnerable(false);
      
      _stormwind.SetObjectLevel(ResearchId, 1);
      _ironforge.SetObjectLevel(ResearchId, 1);
      _researched = true;
    }

    /// <inheritdoc />
    public override void OnRegister()
    {
      PlayerUnitEvents.Register(UnitEvent.Dies, () => { _tramToStormwind?.Kill(); }, _tramToIronforge);
      PlayerUnitEvents.Register(UnitEvent.Dies, () => { _tramToIronforge?.Kill(); }, _tramToStormwind);
    }
  }
}