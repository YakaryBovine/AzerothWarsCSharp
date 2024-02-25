using System;
using WCSharp.SaveLoad;

namespace MacroTools.Save
{
  internal sealed class PlayerSettings : Saveable
  {
    private int? _camDistance;
    
    public int CamDistance
    {
      get => _camDistance ?? 2400;
      set => _camDistance = Math.Clamp(value, 700, 2701);
    }
    
    internal bool ShowQuestText { get; set; } = true;
    
    internal bool PlayDialogue { get; set; } = true;
    
    internal bool ShowCaptions { get; set; } = true;
  }
}