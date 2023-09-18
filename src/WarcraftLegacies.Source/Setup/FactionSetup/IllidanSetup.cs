using MacroTools.FactionSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Setup.FactionSetup
{
  public static class IllidanSetup
  {
    public static Faction? Illidan { get; private set; }
    
    public static void Setup()
    {
      Illidan = new Faction("Illidan's Forces", PLAYER_COLOR_VIOLET, "|cffff00ff",
        @"ReplaceableTextures\CommandButtons\BTNHeroDemonHunter.blp")
      {
        UndefeatedResearch = FourCC("R02L"),
        StartingGold = 200,
        StartingLumber = 700,
        IntroText = @"You are playing as the renegade Illidan.|r|r.

While shunned by most of Night Elven society, you still support Tyrande, the object of your eternal affection.

Start by expanding east and north, taking territory and unlocking the secrets of the Aetheneum.

The goblins will be vying for the support of Tanaris, be ready for a tough fight."
      };

      Illidan.ModObjectLimit(FourCC("e01P"), Faction.UNLIMITED); //Coral of Life
      Illidan.ModObjectLimit(FourCC("e01Q"), Faction.UNLIMITED); //Coral of Ages
      Illidan.ModObjectLimit(FourCC("e01R"), Faction.UNLIMITED); //Coral of Eternity
      Illidan.ModObjectLimit(FourCC("e01S"), Faction.UNLIMITED); //Altar of the Depths
      Illidan.ModObjectLimit(FourCC("e01T"), Faction.UNLIMITED); //Coral Of War
      Illidan.ModObjectLimit(FourCC("e01U"), Faction.UNLIMITED); //Coral of Lore
      Illidan.ModObjectLimit(FourCC("e01V"), Faction.UNLIMITED); //Coral of Wind
      Illidan.ModObjectLimit(FourCC("h0AD"), Faction.UNLIMITED); //Coral Forge
      Illidan.ModObjectLimit(FourCC("n0CU"), Faction.UNLIMITED); //Deep Sea Vault
      Illidan.ModObjectLimit(FourCC("n09I"), Faction.UNLIMITED); //Coral Beds
      Illidan.ModObjectLimit(FourCC("n0D4"), Faction.UNLIMITED); //Improved Tidal Guardian
      Illidan.ModObjectLimit(FourCC("n0D5"), Faction.UNLIMITED); //Tidal Guardian
      Illidan.ModObjectLimit(FourCC("n08H"), Faction.UNLIMITED); //Hunter Proving Ground

      Illidan.ModObjectLimit(FourCC("nmpe"), Faction.UNLIMITED); //Murgul Slave
      Illidan.ModObjectLimit(FourCC("o061"), Faction.UNLIMITED); //Cliffrunner
      Illidan.ModObjectLimit(FourCC("nmyr"), Faction.UNLIMITED); //Myrmidon
      Illidan.ModObjectLimit(FourCC("nsnp"), Faction.UNLIMITED); //Snap Dragon
      Illidan.ModObjectLimit(FourCC("nnsw"), Faction.UNLIMITED); //Siren
      Illidan.ModObjectLimit(FourCC("nmsc"), Faction.UNLIMITED); //Shadowcaster
      Illidan.ModObjectLimit(FourCC("e00S"), Faction.UNLIMITED); //Glaive Hunter
      Illidan.ModObjectLimit(FourCC("nnsu"), 6); //Summoner
      Illidan.ModObjectLimit(FourCC("h08W"), 6); //Demon Hunter
      Illidan.ModObjectLimit(FourCC("nnrg"), 6); //Royal Guard
      Illidan.ModObjectLimit(FourCC("h0AC"), 6); //Sea Witch
      Illidan.ModObjectLimit(FourCC("nhyc"), 8); //Dragon Turtle
      Illidan.ModObjectLimit(FourCC("nwgs"), 8); //Couatl
      Illidan.ModObjectLimit(FourCC("e00Y"), 4); //Scylla

      Illidan.ModObjectLimit(FourCC("Hvsh"), 1); //Vashj
      Illidan.ModObjectLimit(FourCC("U00S"), 1); //Najentus
      Illidan.ModObjectLimit(FourCC("Naka"), 1); //Akama
      Illidan.ModObjectLimit(FourCC("E015"), 1); //Altruis
      Illidan.ModObjectLimit(FourCC("Eill"), 1); //Illidan

      Illidan.ModObjectLimit(FourCC("R062"), Faction.UNLIMITED); //Redemption path
      Illidan.ModObjectLimit(FourCC("R063"), Faction.UNLIMITED); //Exile Path
      Illidan.ModObjectLimit(FourCC("R065"), Faction.UNLIMITED); //Madness Path

      Illidan.ModObjectLimit(FourCC("Rnsw"), Faction.UNLIMITED); //Siren Adept Training
      Illidan.ModObjectLimit(FourCC("R02V"), Faction.UNLIMITED); //Shadowcaster Adept Training

      FactionManager.Register(Illidan);
    }
  }
}