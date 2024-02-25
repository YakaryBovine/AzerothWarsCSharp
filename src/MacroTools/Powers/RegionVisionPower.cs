using System.Collections.Generic;
using MacroTools.FactionSystem;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace MacroTools.Powers
{
  /// <summary>
  ///   Grants the <see cref="Faction" /> permanent visibility over one or more <see cref="Rectangle" />s.
  /// </summary>
  public sealed class RegionVisionPower : Power
  {
    private readonly List<fogmodifier> _fogModifiers = new();
    private readonly IEnumerable<Rectangle> _visionRects;

    public RegionVisionPower(string name, string description, IEnumerable<Rectangle> visionRects)
    {
      Name = name;
      Description = description;
      _visionRects = visionRects;
    }

    public override void OnAdd(player whichPlayer)
    {
      foreach (var region in _visionRects)
      {
        var newFogModifier = CreateFogModifierRect(whichPlayer, FOG_OF_WAR_VISIBLE, region.Rect, true, true);
        _fogModifiers.Add(newFogModifier);
        FogModifierStart(newFogModifier);
      }
    }

    public override void OnRemove(player whichPlayer)
    {
      foreach (var fogModifier in _fogModifiers) DestroyFogModifier(fogModifier);
      _fogModifiers.Clear();
    }
  }
}