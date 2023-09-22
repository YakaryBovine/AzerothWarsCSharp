using MacroTools;
using MacroTools.FactionSystem;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Setup.FactionSetup
{
  public static class KultirasSetup
  {
    public static Faction? Kultiras { get; private set; }

    public static void Setup(PreplacedUnitSystem preplacedUnitSystem)
    {
      Kultiras =
        new Faction("Kul'tiras", PLAYER_COLOR_EMERALD, "|cff00781e", @"ReplaceableTextures\CommandButtons\BTNProudmoore.blp")
        {
          StartingGold = 200,
          StartingLumber = 700,
          ControlPointDefenderUnitTypeId = Constants.UNIT_H09W_CONTROL_POINT_DEFENDER_KUL_TIRAS,
          IntroText = @"You are playing as the maritime |cff008000Kingdom of Kul'tiras|r.

You begin on Balor island, separated from your main forces in Kul Tiras. Unite your forces by eliminating your enemies in Tiragarde, Drustvar and Stormsong Valley.

Stormwind is preparing for an invasion through the Dark Portal in the South. Muster the Admiralty and help them, or you may lose your strongest ally."
        };

      //Structures
      Kultiras.ModObjectLimit(FourCC("h062"), Faction.UNLIMITED); //Town Hall
      Kultiras.ModObjectLimit(FourCC("h064"), Faction.UNLIMITED); //Keep
      Kultiras.ModObjectLimit(FourCC("h06I"), Faction.UNLIMITED); //Castle
      Kultiras.ModObjectLimit(FourCC("h07N"), Faction.UNLIMITED); //Farm
      Kultiras.ModObjectLimit(FourCC("h07M"), Faction.UNLIMITED); //Altar
      Kultiras.ModObjectLimit(FourCC("h07R"), Faction.UNLIMITED); //Scout Tower
      Kultiras.ModObjectLimit(FourCC("h07S"), Faction.UNLIMITED); //Guard Tower
      Kultiras.ModObjectLimit(FourCC("h07T"), Faction.UNLIMITED); //Improved Guard Tower
      Kultiras.ModObjectLimit(FourCC("h07U"), Faction.UNLIMITED); //Cannon Tower
      Kultiras.ModObjectLimit(FourCC("h07V"), Faction.UNLIMITED); //Improved Cannon Tower
      Kultiras.ModObjectLimit(FourCC("h07O"), Faction.UNLIMITED); //Blacksmith
      Kultiras.ModObjectLimit(FourCC("h07Q"), Faction.UNLIMITED); //Arcane Sanctum
      Kultiras.ModObjectLimit(FourCC("n07H"), Faction.UNLIMITED); //Marketplace
      Kultiras.ModObjectLimit(FourCC("h07W"), Faction.UNLIMITED); //Shipyard
      Kultiras.ModObjectLimit(FourCC("h06R"), Faction.UNLIMITED); //Garrison
      Kultiras.ModObjectLimit(FourCC("h07P"), Faction.UNLIMITED); //Workshop
      Kultiras.ModObjectLimit(FourCC("h093"), Faction.UNLIMITED); //Workshop

      //Units
      Kultiras.ModObjectLimit(FourCC("h01E"), Faction.UNLIMITED); //Deckhand
      Kultiras.ModObjectLimit(FourCC("e007"), Faction.UNLIMITED); //Thornspeaker
      Kultiras.ModObjectLimit(FourCC("n09A"), 12); //Ember Cleric
      Kultiras.ModObjectLimit(FourCC("n09B"), 8); //Witch Hunter
      Kultiras.ModObjectLimit(FourCC("h092"), 4); //Order Inquisitor
      Kultiras.ModObjectLimit(FourCC("h05K"), Faction.UNLIMITED); //Tidesage
      Kultiras.ModObjectLimit(FourCC("h041"), Faction.UNLIMITED); //Marine
      Kultiras.ModObjectLimit(FourCC("e00B"), Faction.UNLIMITED); //Thornspeaker Bear
      Kultiras.ModObjectLimit(FourCC("n009"), 12); //Revenant of the Tides
      Kultiras.ModObjectLimit(FourCC("n07G"), 6); //muskateer
      Kultiras.ModObjectLimit(FourCC("n029"), 12); //Sea Giant
      Kultiras.ModObjectLimit(FourCC("h06J"), Faction.UNLIMITED); //Sniper
      Kultiras.ModObjectLimit(FourCC("o01A"), 6); //Cannon Team
      Kultiras.ModObjectLimit(FourCC("h04O"), 12); //Bomber
      Kultiras.ModObjectLimit(FourCC("h04W"), 3); //Siege Tank
      Kultiras.ModObjectLimit(FourCC("h0A0"), 8); //Fusillier

      //Ships
      Kultiras.ModObjectLimit(FourCC("hbot"), Faction.UNLIMITED); //Alliance Transport Ship
      Kultiras.ModObjectLimit(FourCC("h0AR"), Faction.UNLIMITED); //Alliance Scout
      Kultiras.ModObjectLimit(FourCC("h0AX"), Faction.UNLIMITED); //Alliance Frigate
      Kultiras.ModObjectLimit(FourCC("h0B3"), Faction.UNLIMITED); //Alliance Fireship
      Kultiras.ModObjectLimit(FourCC("h0B0"), Faction.UNLIMITED); //Alliance Galley
      Kultiras.ModObjectLimit(FourCC("h0B6"), Faction.UNLIMITED); //Alliance Boarding
      Kultiras.ModObjectLimit(FourCC("h0AN"), Faction.UNLIMITED); //Alliance Juggernaut
      Kultiras.ModObjectLimit(FourCC("h0B7"), 6); //Alliance Bombard

      //Upgrades
      Kultiras.ModObjectLimit(FourCC("R001"), Faction.UNLIMITED); //Rising Tides
      Kultiras.ModObjectLimit(FourCC("R000"), Faction.UNLIMITED); //Tidesage Adept Training
      Kultiras.ModObjectLimit(FourCC("R01O"), Faction.UNLIMITED); //Crushing Wave
      Kultiras.ModObjectLimit(FourCC("R01T"), Faction.UNLIMITED); //Cluster Rockets
      Kultiras.ModObjectLimit(FourCC("R01U"), Faction.UNLIMITED); //Improved Barrage
      Kultiras.ModObjectLimit(FourCC("R05G"), Faction.UNLIMITED); //Thornspeaker Training
      Kultiras.ModObjectLimit(FourCC("Rhlh"), Faction.UNLIMITED); //Improved Lumber Harvesting
      Kultiras.ModObjectLimit(FourCC("Rhac"), Faction.UNLIMITED); //Improved Masonry
      Kultiras.ModObjectLimit(FourCC("R08B"), Faction.UNLIMITED); //Long Rifles
      Kultiras.ModObjectLimit(FourCC("R05J"), Faction.UNLIMITED); //Expedition

      //Heroes
      Kultiras.ModObjectLimit(FourCC("Hapm"), 1); //Admiral
      Kultiras.ModObjectLimit(FourCC("H05L"), 1); //Lady Ashvane
      Kultiras.ModObjectLimit(FourCC("U026"), 1); //Meredith

      Kultiras.AddGoldMine(preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(4585, -13038)));

      FactionManager.Register(Kultiras);
    }
  }
}