using MacroTools;
using MacroTools.ArtifactSystem;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using WarcraftLegacies.Source.Setup.FactionSetup;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Mechanics.Scourge
{
  /// <summary>
  ///  When Scourge leaves the game, drop the Helm of Domination to an easily accessible position since the default position is hard to see.
  /// </summary>
  public static class HelmOfDominationDropsWhenScourgeLeaves
  {
    private static Artifact? _helmOfDomination;
    private static trigger? _deathTrigger;
    private static Capital? _lichKing;

    /// <summary>
    /// Sets up <see cref="HelmOfDominationDropsWhenScourgeLeaves"/>.
    /// </summary>
    public static void Setup(Artifact helmOfDomination, Capital lichKing)
    {
      if (ScourgeSetup.Scourge == null)
        throw new SystemNotInitializedException(nameof(ScourgeSetup));

      _lichKing = lichKing;

      _deathTrigger = CreateTrigger()
        .RegisterUnitEvent(lichKing.Unit, EVENT_UNIT_DEATH)
        .AddAction(() =>
        {
          MaybeDropHelmOfDomination();
          UnregisterEvents();
        });

      ScourgeSetup.Scourge.ScoreStatusChanged += OnScourgeScoreStatusChanged;
      _helmOfDomination = helmOfDomination;
    }

    private static void OnScourgeScoreStatusChanged(object? sender, Faction faction)
    {
      if (faction.ScoreStatus != ScoreStatus.Defeated) 
        return;
      
      MaybeDropHelmOfDomination();
      UnregisterEvents();
    }

    private static void UnregisterEvents()
    {
      _deathTrigger?.Destroy();
      if (ScourgeSetup.Scourge != null)
        ScourgeSetup.Scourge.ScoreStatusChanged -= OnScourgeScoreStatusChanged;
    }

    private static void MaybeDropHelmOfDomination()
    {
      if (_lichKing?.Unit == null || !UnitHasItem(_lichKing.Unit, _helmOfDomination?.Item))
        return;

      var lichKingPosition = _lichKing.Unit.GetPosition();
      _lichKing.Unit.DropAllItems();
      _helmOfDomination?.Item.SetPosition(new Point(lichKingPosition.X - 55,
        lichKingPosition.Y + 30));
    }
  }
}