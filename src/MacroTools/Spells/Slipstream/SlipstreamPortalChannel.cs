using System;
using MacroTools.ChannelSystem;
using MacroTools.Extensions;
using WCSharp.Buffs;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace MacroTools.Spells.Slipstream
{
  /// <summary>
  /// Channel a portal between two locations.
  /// </summary>
  public sealed class SlipstreamPortalChannel : Channel
  {
    /// <summary>
    /// The unit to spawn to act as the actual portal.
    /// </summary>
    public int PortalUnitTypeId { get; init; }

    /// <summary>
    /// The portal won't be useable until the caster has channeled for this long.
    /// </summary>
    public float OpeningDelay { get; init; }

    /// <summary>
    /// It takes this long for the portal to close after the caster stops channeling.
    /// </summary>
    public float ClosingDelay { get; init; }
    
    /// <summary>
    /// A function that tells the <see cref="SlipstreamPortalChannel"/> how to refund its cost to its caster if necessary.
    /// </summary>
    public Action<unit>? RefundFunc { get; init; }

    /// <summary>
    /// The color of the created portals.
    /// </summary>
    public Color Color { get; init; } = new(255, 255, 255, 255);

    private unit? _portalOrigin;
    private unit? _portalDestination;
    private SlipstreamPortalBuff? _portalOriginBuff;
    private SlipstreamPortalBuff? _portalDestinationBuff;
    private readonly Point _origin;
    private readonly Point _target;

    /// <summary>
    /// Initializes a new instance of the <see cref="SlipstreamPortalChannel"/> class.
    /// </summary>
    /// <param name="caster"><inheritdoc /></param>
    /// <param name="spellId"><inheritdoc /></param>
    /// <param name="origin">The position of the portal that spawns near the caster.</param>
    /// <param name="target">The destination point of the portal.</param>
    public SlipstreamPortalChannel(unit caster, int spellId, Point origin, Point target) : base(caster, spellId)
    {
      _target = target;
      _origin = origin;
    }
    
    /// <inheritdoc />
    public override void OnCreate()
    {
      _portalOrigin = CreateUnit(Caster.OwningPlayer(), PortalUnitTypeId, _origin.X, _origin.Y, Caster.GetFacing() - 180)
        .SetWaygateDestination(_target)
        .SetColor(Color.Red, Color.Green, Color.Blue, Color.Alpha);
      _portalOriginBuff = new SlipstreamPortalBuff(Caster, _portalOrigin);
      BuffSystem.Add(_portalOriginBuff);
      _portalOriginBuff.Open(OpeningDelay);
      
      _portalDestination = CreateUnit(Caster.OwningPlayer(), PortalUnitTypeId, _target.X, _target.Y, Caster.GetFacing())
        .SetWaygateDestination(new Point(_origin.X, _origin.Y))
        .SetColor(Color.Red, Color.Green, Color.Blue, Color.Alpha);
      _portalDestinationBuff = new SlipstreamPortalBuff(Caster, _portalDestination)
      {
        RefundFunc = RefundFunc
      };
      BuffSystem.Add(_portalDestinationBuff);
      _portalDestinationBuff.Open(OpeningDelay);
    }

    /// <inheritdoc />
    protected override void OnDispose()
    {
      _portalOriginBuff?.Close(ClosingDelay);
      _portalDestinationBuff?.Close(ClosingDelay);
    }
  }
}