using MacroTools.SpellSystem;
using static War3Api.Common;

namespace MacroTools.Hazards
{
  /// <summary>
  /// A <see cref="Hazard"/> that causes a particular spell to be repeatedly cast at its location.
  /// </summary>
  public sealed class RecurrentSpellHazard : Hazard
  {
    private readonly int _dummySpellId;
    private readonly string _dummySpellOrder;
    private readonly int _level;

    /// <inheritdoc />
    protected override void OnPeriodic()
    {
      DummyCast.DummyCastPoint(GetOwningPlayer(Caster), _dummySpellId, _dummySpellOrder, _level, Position);
    }

    /// <inheritdoc />
    public override void OnCreate()
    {
      DummyCast.DummyCastPoint(GetOwningPlayer(Caster), _dummySpellId, _dummySpellOrder, _level, Position);
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="RecurrentSpellHazard"/> class.
    /// </summary>
    public RecurrentSpellHazard(unit caster, float x, float y, string dummySpellOrder, int level, int dummySpellId) : base(caster, x, y)
    {
      _dummySpellOrder = dummySpellOrder;
      _level = level;
      _dummySpellId = dummySpellId;
    }
  }
}