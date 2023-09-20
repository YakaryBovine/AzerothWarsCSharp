using WarcraftLegacies.Source.Rocks;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Setup
{
  /// <summary>
  /// Sets up all <see cref="RockGroup"/>s in the <see cref="RockSystem"/>.
  /// </summary>
  public static class RockSetup
  {
    private static readonly int RockChunkId = FourCC("LTrc");
    private static readonly int IslandChunkId = FourCC("B013");
    private static readonly int ForestChunkId = FourCC("B008");

    public static void Setup()
    {
      RockSystem.Register(new RockGroup(Regions.KaliRock1, RockChunkId, 480));
      RockSystem.Register(new RockGroup(Regions.KaliRock4, RockChunkId, 300));

      //south rocks
      RockSystem.Register(new RockGroup(Regions.KaliRock8, RockChunkId, 480));
      RockSystem.Register(new RockGroup(Regions.KaliRock9, RockChunkId, 480));
      RockSystem.Register(new RockGroup(Regions.KaliRock15, RockChunkId, 480));
      RockSystem.Register(new RockGroup(Regions.KaliRock12, RockChunkId, 300));
      RockSystem.Register(new RockGroup(Regions.KaliRock14, RockChunkId, 480));

      RockSystem.Register(new RockGroup(Regions.IslandBlocker1, IslandChunkId, 1200));
      RockSystem.Register(new RockGroup(Regions.IslandBlocker2, IslandChunkId, 1200));
      RockSystem.Register(new RockGroup(Regions.IslandBlocker3, IslandChunkId, 1200));
      RockSystem.Register(new RockGroup(Regions.IslandBlocker4, IslandChunkId, 1200));
      RockSystem.Register(new RockGroup(Regions.IslandBlocker5, IslandChunkId, 1200));
      RockSystem.Register(new RockGroup(Regions.IslandBlocker6, IslandChunkId, 1200));

      RockSystem.Register(new RockGroup(Regions.KaliRock7, ForestChunkId, 480));
      RockSystem.Register(new RockGroup(Regions.KaliRock10, ForestChunkId, 480));
      RockSystem.Register(new RockGroup(Regions.KaliRock11, ForestChunkId, 480));
      RockSystem.Register(new RockGroup(Regions.KaliRock13, ForestChunkId, 480));

      RockSystem.Register(new RockGroup(Regions.GilneasUnlock5, RockChunkId, 660));
    }
  }
}