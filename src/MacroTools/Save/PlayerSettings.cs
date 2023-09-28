using System;
using WCSharp.SaveLoad;

namespace MacroTools.Save
{
  public sealed class PlayerSettings : Saveable
  {
    private int? _camDistance;
    
    public int CamDistance
    {
      get => _camDistance ?? 2400;
      set => _camDistance = Math.Clamp(value, 700, 2701);
    }
    public bool ShowQuestText { get; set; } = true;
    
    public bool PlayDialogue { get; set; } = true;
    
    public bool ShowCaptions { get; set; } = true;
  }
}