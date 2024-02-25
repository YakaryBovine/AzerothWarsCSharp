using MacroTools.FactionSystem;
using War3Api;

namespace MacroTools.Powers
{
  public sealed class DummyPower : Power
  {
    public DummyPower(string name, string description)
    {
      Name = name;
      Description = description;
    }

    public override void OnAdd(Common.player whichPlayer)
    {
    }

    public override void OnRemove(Common.player whichPlayer)
    {
    }
  }
}