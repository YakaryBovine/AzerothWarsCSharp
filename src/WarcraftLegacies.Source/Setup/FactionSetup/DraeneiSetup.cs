using MacroTools.FactionSystem;
using MacroTools.Powers;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Setup.FactionSetup
{
  public static class DraeneiSetup
  {
    public static Faction? Draenei { get; private set; }

    public static void Setup()
    {
      Draenei = new Faction("The Exodar", PLAYER_COLOR_NAVY, "|cff000080",
        @"ReplaceableTextures\CommandButtons\BTNBOSSVelen.blp")
      {
        StartingGold = 200,
        StartingLumber = 700,
        ControlPointDefenderUnitTypeId = Constants.UNIT_U008_CONTROL_POINT_DEFENDER_DRAENEI,
        IntroText = @"You are playing as the exiled |cff000080Draenei|r.

You begin on Azuremyst Island, amid the wreckage of your flight from the Burning Legion.

Further inland your Night-elf allies will need your help against the Orcish Horde, quickly build your base and gain entry to the Exodar.

The Exodar is a mighty fortress-base with the ability to move around the map, but it will take a long time to repair."
      };

      Draenei.ModObjectLimit(FourCC("o02P"), Faction.UNLIMITED); //Crystal Hall
      Draenei.ModObjectLimit(FourCC("o050"), Faction.UNLIMITED); //Metropolis
      Draenei.ModObjectLimit(FourCC("o051"), Faction.UNLIMITED); //Divine Citadel
      Draenei.ModObjectLimit(FourCC("o058"), Faction.UNLIMITED); //Altar of Light
      Draenei.ModObjectLimit(FourCC("o052"), Faction.UNLIMITED); //Ceremonial Altar
      Draenei.ModObjectLimit(FourCC("o053"), Faction.UNLIMITED); //Smithery
      Draenei.ModObjectLimit(FourCC("o054"), Faction.UNLIMITED); //Astral Sanctum
      Draenei.ModObjectLimit(FourCC("o055"), Faction.UNLIMITED); //Crystal Spire
      Draenei.ModObjectLimit(FourCC("o056"), 48); //Arcane Well
      Draenei.ModObjectLimit(FourCC("o057"), Faction.UNLIMITED); //Vaults of Relic
      Draenei.ModObjectLimit(FourCC("u00U"), Faction.UNLIMITED); //Crystal Protector
      Draenei.ModObjectLimit(FourCC("u01Q"), Faction.UNLIMITED); //Crystal Protector improved
      Draenei.ModObjectLimit(FourCC("o059"), Faction.UNLIMITED); //Improved Ancient Protector
      Draenei.ModObjectLimit(FourCC("o05U"), Faction.UNLIMITED); //Lightforged Gateway

      Draenei.ModObjectLimit(FourCC("o05A"), Faction.UNLIMITED); //Wisp
      Draenei.ModObjectLimit(FourCC("o05B"), Faction.UNLIMITED); //Defender
      Draenei.ModObjectLimit(FourCC("h09T"), Faction.UNLIMITED); //Rangari
      Draenei.ModObjectLimit(FourCC("e01K"), 3); //Polybolos
      Draenei.ModObjectLimit(FourCC("o05D"), Faction.UNLIMITED); //Elementalist
      Draenei.ModObjectLimit(FourCC("o05C"), Faction.UNLIMITED); //Luminarch
      Draenei.ModObjectLimit(FourCC("h09R"), 6); //Vindicator
      Draenei.ModObjectLimit(FourCC("nmdr"), Faction.UNLIMITED); //Elekk
      Draenei.ModObjectLimit(FourCC("h09U"), 4); //Elekk Knight
      Draenei.ModObjectLimit(FourCC("u02H"), 6); //Nether Ray

      Draenei.ModObjectLimit(FourCC("n0BJ"), 6); //Sharpshooter
      Draenei.ModObjectLimit(FourCC("n0BP"), 4); //Juggernaut
      Draenei.ModObjectLimit(FourCC("n0BM"), 8); //Nether Ray

      //Ships
      Draenei.ModObjectLimit(FourCC("etrs"), Faction.UNLIMITED); //Night Elf Transport Ship
      Draenei.ModObjectLimit(FourCC("h0AU"), Faction.UNLIMITED); // Scout
      Draenei.ModObjectLimit(FourCC("h0AV"), Faction.UNLIMITED); // Frigate
      Draenei.ModObjectLimit(FourCC("h0B1"), Faction.UNLIMITED); // Fireship
      Draenei.ModObjectLimit(FourCC("h057"), Faction.UNLIMITED); // Galley
      Draenei.ModObjectLimit(FourCC("h0B4"), Faction.UNLIMITED); // Boarding
      Draenei.ModObjectLimit(FourCC("h0BA"), Faction.UNLIMITED); // Juggernaut
      Draenei.ModObjectLimit(FourCC("h0B8"), 6); // Bombard

      Draenei.ModObjectLimit(FourCC("H09S"), 1); //Maraad
      Draenei.ModObjectLimit(FourCC("E01I"), 1); //Velen
      Draenei.ModObjectLimit(FourCC("E01J"), 1); //Nobundo
      Draenei.ModObjectLimit(FourCC("H09M"), 1); //Adal

      Draenei.ModObjectLimit(FourCC("R023"), Faction.UNLIMITED); //Spiritual Infusion
      Draenei.ModObjectLimit(FourCC("R078"), Faction.UNLIMITED); //Elementalist training
      Draenei.ModObjectLimit(FourCC("R07C"), Faction.UNLIMITED); //Luminarch training

      //Powers
      var dummyPower = new DummyPower("Crystallization",
        "Arcane Wells placed directly near Divine Citadels will generate mana for them over time. You can then convert that mana into units. The maximum number of Arcane Well around a Divine Citadel is 12 if placed optimally",
        "ManaGem.blp");
      Draenei.AddPower(dummyPower);

      FactionManager.Register(Draenei);
    }
  }
}