using MacroTools.Extensions;
using MacroTools.FactionSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Setup.FactionSetup
{
  public static class ForsakenSetup
  {
    public static Faction? Forsaken { get; private set; }
    
    public static void Setup()
    {
      Forsaken = new Faction("Forsaken", PLAYER_COLOR_LIGHT_BLUE, "|cff8080ff",
        @"ReplaceableTextures\CommandButtons\BTNAcolyte.blp")
      {
        StartingGold = 0,
        StartingLumber = 100,
        StartingCameraPosition = Regions.ForsakenStartPos.Center,
        StartingUnits = Regions.ForsakenStartPos.PrepareUnitsForRescue(RescuePreparationMode.Invulnerable),
        IntroText = @"You are playing as the insidious Cult of the Damned.

Your first objective is to use your three Cultists of the Damned to corrupt buildings in Lordaeron. These corrupted buildings will provide income for you until the plague is unleashed.

You are enslaved to the Scourge and Burning Legion, but you can break free from their shackles.

To do so, march North to Quel'thalas and capture the Sunwell. This will unlock Sylvanas, allowing you to reform as the Forsaken and declare independence from the Lich King."
      };

      //Buildings
      Forsaken.ModObjectLimit(FourCC("h089"), Faction.UNLIMITED); //Necropolis
      Forsaken.ModObjectLimit(FourCC("h08A"), Faction.UNLIMITED); //Halls of the Dead
      Forsaken.ModObjectLimit(FourCC("h08B"), Faction.UNLIMITED); //Black Citadel
      Forsaken.ModObjectLimit(FourCC("h08C"), Faction.UNLIMITED); //Ziggurat
      Forsaken.ModObjectLimit(FourCC("h08D"), Faction.UNLIMITED); //Spirit Tower
      Forsaken.ModObjectLimit(FourCC("h08E"), Faction.UNLIMITED); //Nerubian Tower
      Forsaken.ModObjectLimit(FourCC("u010"), Faction.UNLIMITED); //Altar of Darkness
      Forsaken.ModObjectLimit(FourCC("u011"), Faction.UNLIMITED); //Crypt
      Forsaken.ModObjectLimit(FourCC("u01J"), Faction.UNLIMITED); //Graveyard
      Forsaken.ModObjectLimit(FourCC("u016"), Faction.UNLIMITED); //Slaughterhouse
      Forsaken.ModObjectLimit(FourCC("u01W"), Faction.UNLIMITED); //Royal Sepulcur
      Forsaken.ModObjectLimit(FourCC("u014"), Faction.UNLIMITED); //Temple of the Damned
      Forsaken.ModObjectLimit(FourCC("u017"), Faction.UNLIMITED); //Tomb of Relics
      Forsaken.ModObjectLimit(FourCC("u01A"), Faction.UNLIMITED); //Undead Shipyard
      Forsaken.ModObjectLimit(FourCC("h08F"), Faction.UNLIMITED); //Improved Spirit Tower

      //Units
      Forsaken.ModObjectLimit(FourCC("u01K"), Faction.UNLIMITED); //Acolyte
      Forsaken.ModObjectLimit(FourCC("h08O"), Faction.UNLIMITED); //Ghoul
      Forsaken.ModObjectLimit(FourCC("h08N"), Faction.UNLIMITED); //Abomination
      Forsaken.ModObjectLimit(FourCC("u01P"), 6); //Plague Catapult
      Forsaken.ModObjectLimit(FourCC("n07S"), Faction.UNLIMITED); //Deadeye
      Forsaken.ModObjectLimit(FourCC("h08P"), Faction.UNLIMITED); //Sorceress
      Forsaken.ModObjectLimit(FourCC("u01R"), Faction.UNLIMITED); //Apothecary
      Forsaken.ModObjectLimit(FourCC("u02G"), 12); //Undercity Guardian
      Forsaken.ModObjectLimit(FourCC("n07V"), 6); //Elder Banshee
      Forsaken.ModObjectLimit(FourCC("o05H"), 8); //PlagueFlyer
      Forsaken.ModObjectLimit(FourCC("n0BY"), 6); //dread knight
      Forsaken.ModObjectLimit(FourCC("u01V"), 2); //Valyr
      Forsaken.ModObjectLimit(FourCC("ubot"), Faction.UNLIMITED); //Undead Transport Ship
      Forsaken.ModObjectLimit(Constants.UNIT_N07W_PLAGUE_REVENANT_FORSAKEN, 12);

      //Ship
      Forsaken.ModObjectLimit(FourCC("ubot"), Faction.UNLIMITED); //Undead Transport Ship
      Forsaken.ModObjectLimit(FourCC("h0AT"), Faction.UNLIMITED); //Scout
      Forsaken.ModObjectLimit(FourCC("h0AW"), Faction.UNLIMITED); //Frigate
      Forsaken.ModObjectLimit(FourCC("h0BM"), Faction.UNLIMITED); //Fireship
      Forsaken.ModObjectLimit(FourCC("h0AZ"), Faction.UNLIMITED); //Galley
      Forsaken.ModObjectLimit(FourCC("h0AQ"), Faction.UNLIMITED); //Boarding
      Forsaken.ModObjectLimit(FourCC("h0BB"), Faction.UNLIMITED); //Juggernaut
      Forsaken.ModObjectLimit(FourCC("h0B9"), 6); //Bombard

      Forsaken.ModObjectLimit(FourCC("U01O"), 1); //Putress
      Forsaken.ModObjectLimit(FourCC("Usyl"), 1); //Sylvanas
      Forsaken.ModObjectLimit(FourCC("U02I"), 1); //Farenell

      //Upgrades
      Forsaken.ModObjectLimit(FourCC("Ruba"), Faction.UNLIMITED); //Banshee Adept Training
      Forsaken.ModObjectLimit(FourCC("R05C"), Faction.UNLIMITED); //Banshee Adept Training
      Forsaken.ModObjectLimit(FourCC("R051"), Faction.UNLIMITED); //Apotechary Training
      Forsaken.ModObjectLimit(FourCC("R02X"), Faction.UNLIMITED); // Open Scholomance
      Forsaken.ModObjectLimit(FourCC("R02A"), Faction.UNLIMITED); //Chaos Infusion

      FactionManager.Register(Forsaken);
    }
  }
}