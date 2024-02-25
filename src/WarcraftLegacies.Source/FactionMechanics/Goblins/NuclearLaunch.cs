using MacroTools.Extensions;
using MacroTools.SpellSystem;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.FactionMechanics.Goblins
{
  /// <summary>
  /// Fire a giant delayed nuke, with a warning signal placed down beforehand.
  /// </summary>
  public sealed class NuclearLaunch : Spell
  {
    private readonly int _dummyNukeLeftOverId;

    /// <summary>
    /// Initializes a new instance of the <see cref="NuclearLaunch"/> class.
    /// </summary>
    /// <param name="id"><inheritdoc /></param>
    /// <param name="dummyNukeLeftOverId">Unit type ID for the unit to be left behind for a bit after the spell casts.</param>
    public NuclearLaunch(int id, int dummyNukeLeftOverId) : base(id)
    {
      _dummyNukeLeftOverId = dummyNukeLeftOverId;
    }

    /// <inheritdoc />
    public override void OnCast(unit caster, unit target, Point targetPoint)
    {
      var dummyNukeLeftover = CreateUnit(caster.OwningPlayer(), _dummyNukeLeftOverId, targetPoint.X, targetPoint.Y, 0);
      UnitApplyTimedLife(dummyNukeLeftover, 0, 3);
      dummyNukeLeftover.IssueOrder("flamestrike", targetPoint);
    }
  }
}