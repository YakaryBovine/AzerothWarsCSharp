using MacroTools.FactionSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Setup.FactionSetup
{
  /// <summary>
  /// Responsible for setting up and containing the Dragonmaw faction.
  /// </summary>
  public static class DragonmawSetup
  {
    /// <summary>
    /// Orc clan obsessed with dragons.
    /// </summary>
    public static Faction? Dragonmaw { get; private set; }

    /// <summary>
    /// up the Dragonmaw faction.
    /// </summary>
    public static void Setup()
    {
      Dragonmaw = new Faction("Dragonmaw", PLAYER_COLOR_WHEAT, "|C00FFFC01",
        @"ReplaceableTextures\CommandButtons\BTNRedDragon.blp")
      {
        StartingGold = 250,
        StartingLumber = 700,
        IntroText = @"You are playing as the fragmented |cffe4bc00Dragonmaw Clan.
                    |r
Your situation is dire, you have been pushed into the Twilight Highlands and the Dwarves will soon attack whatever is left of your clan.

Your best chance of survival is escaping to Kalimdor, where Warlord Zaela has heard of a New Horde forming. 

The portal to Kalimdor will take 9 minutes to open and will only last for 60 seconds, be ready to escape the Highlands when it does.

Plunder as much as you can from the surrounding lands in preparation for your migration."
      };

      Dragonmaw.ModObjectLimit(FourCC("o063"), Faction.UNLIMITED); //Great Hall
      Dragonmaw.ModObjectLimit(FourCC("o064"), Faction.UNLIMITED); //Stronghold
      Dragonmaw.ModObjectLimit(FourCC("o065"), Faction.UNLIMITED); //Fortress
      Dragonmaw.ModObjectLimit(FourCC("o066"), Faction.UNLIMITED); //Altar of Storms
      Dragonmaw.ModObjectLimit(FourCC("o067"), Faction.UNLIMITED); //Barracks
      Dragonmaw.ModObjectLimit(FourCC("o068"), Faction.UNLIMITED); //War Mill
      Dragonmaw.ModObjectLimit(FourCC("o069"), Faction.UNLIMITED); //Spirit Lodge
      Dragonmaw.ModObjectLimit(FourCC("o05J"), Faction.UNLIMITED); //Bestiary
      Dragonmaw.ModObjectLimit(FourCC("o06D"), Faction.UNLIMITED); //Shipyard
      Dragonmaw.ModObjectLimit(FourCC("o06B"), Faction.UNLIMITED); //Watch Tower
      Dragonmaw.ModObjectLimit(FourCC("o06A"), Faction.UNLIMITED); //Improved Watch Tower
      Dragonmaw.ModObjectLimit(FourCC("u02N"), Faction.UNLIMITED); //Shop
      Dragonmaw.ModObjectLimit(FourCC("o06C"), Faction.UNLIMITED); //Burrow

      Dragonmaw.ModObjectLimit(FourCC("n0CP"), 2); //Black Drake
      Dragonmaw.ModObjectLimit(FourCC("o05M"), 8); //WindRider
      Dragonmaw.ModObjectLimit(FourCC("n07X"), Faction.UNLIMITED); //Fel Orc Warlock
      Dragonmaw.ModObjectLimit(FourCC("o05K"), Faction.UNLIMITED); //Fel Orc Grunt
      Dragonmaw.ModObjectLimit(FourCC("o05P"), Faction.UNLIMITED); //Fel Orc Peon
      Dragonmaw.ModObjectLimit(FourCC("n0CQ"), Faction.UNLIMITED); //Fel Orc Peon
      Dragonmaw.ModObjectLimit(FourCC("o04I"), 6); //Executioner
      Dragonmaw.ModObjectLimit(FourCC("o04K"), 6); //Demolisher
      Dragonmaw.ModObjectLimit(FourCC("n09O"), 6); //DK
      Dragonmaw.ModObjectLimit(FourCC("u01T"), Faction.UNLIMITED); //Necrolyte
      Dragonmaw.ModObjectLimit(FourCC("o05L"), Faction.UNLIMITED); //Phase Grenadier

      Dragonmaw.ModObjectLimit(FourCC("O01Q"), 1); //Nekrosh
      Dragonmaw.ModObjectLimit(FourCC("O05S"), 1); //Zaela
      Dragonmaw.ModObjectLimit(FourCC("O06F"), 1); //Gorfax

      //Ship
      Dragonmaw.ModObjectLimit(FourCC("obot"), Faction.UNLIMITED); //Transport Ship
      Dragonmaw.ModObjectLimit(FourCC("h0AS"), Faction.UNLIMITED); //Scout
      Dragonmaw.ModObjectLimit(FourCC("h0AP"), Faction.UNLIMITED); //Frigate
      Dragonmaw.ModObjectLimit(FourCC("h0B2"), Faction.UNLIMITED); //Fireship
      Dragonmaw.ModObjectLimit(FourCC("h0AY"), Faction.UNLIMITED); //Galley
      Dragonmaw.ModObjectLimit(FourCC("h0B5"), Faction.UNLIMITED); //Boarding
      Dragonmaw.ModObjectLimit(FourCC("h0BC"), Faction.UNLIMITED); //Juggernaut
      Dragonmaw.ModObjectLimit(FourCC("h0AO"), Faction.UNLIMITED); //Bombard

      Dragonmaw.ModObjectLimit(FourCC("R023"), Faction.UNLIMITED); //Spiritual Infusion
      Dragonmaw.ModObjectLimit(FourCC("Rosp"), Faction.UNLIMITED); //Spiked Barricades
      Dragonmaw.ModObjectLimit(FourCC("R06X"), Faction.UNLIMITED); //Magic Training
      Dragonmaw.ModObjectLimit(FourCC("R06Z"), Faction.UNLIMITED); //Herald Training
    }
  }
}