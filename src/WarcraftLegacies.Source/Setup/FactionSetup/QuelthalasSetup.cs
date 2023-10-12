using MacroTools;
using MacroTools.FactionSystem;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Setup.FactionSetup
{
  public static class QuelthalasSetup
  {
    public static Faction? Quelthalas { get; private set; }

    public static void Setup(PreplacedUnitSystem preplacedUnitSystem)
    {
      Quelthalas = new Faction("Quel'thalas", PLAYER_COLOR_CYAN, "|C0000FFFF",
          @"ReplaceableTextures\CommandButtons\BTNSylvanusWindrunner.blp")
      {
        UndefeatedResearch = FourCC("R05U"),
        StartingGold = 200,
        StartingLumber = 700,
        CinematicMusic = "BloodElfTheme",
        ControlPointDefenderUnitTypeId = Constants.UNIT_N0BC_CONTROL_POINT_DEFENDER_QUELTHALAS,
        IntroText = @"You are playing as the proud |cff32e1e1Kingdom of Quel'thalas|r.

You begin in Tranquilien, separated from Silvermoon.
The Trolls of Zul'Aman have laid siege to the city, and are preparing attacks on your base.

Train soldiers to repel the attacks, then gather enough strength to besiege Zul'Aman and take the head of Zul'jin.

The Plague of Undeath is coming and Lordaeron will need your help with the Scourge soon. Be ready to join them as once you have secured Silvermoon and dealt with the Amani invasion."
      };

      //Structures
      Quelthalas.ModObjectLimit(FourCC("h033"), Faction.UNLIMITED); //Steading
      Quelthalas.ModObjectLimit(FourCC("h03S"), Faction.UNLIMITED); //Mansion
      Quelthalas.ModObjectLimit(FourCC("h03T"), Faction.UNLIMITED); //Palace
      Quelthalas.ModObjectLimit(FourCC("h01H"), Faction.UNLIMITED); //Altar of Prowess
      Quelthalas.ModObjectLimit(FourCC("h02Y"), Faction.UNLIMITED); //Artisan)s Hall
      Quelthalas.ModObjectLimit(FourCC("h034"), Faction.UNLIMITED); //Arcane Sanctum (Quel)thalas)
      Quelthalas.ModObjectLimit(FourCC("h073"), Faction.UNLIMITED); //Scout Tower
      Quelthalas.ModObjectLimit(FourCC("h074"), Faction.UNLIMITED); //Arcane Tower
      Quelthalas.ModObjectLimit(FourCC("h075"), Faction.UNLIMITED); //Arcane Tower (Improved)
      Quelthalas.ModObjectLimit(FourCC("negt"), Faction.UNLIMITED); //High Elven Guard Tower
      Quelthalas.ModObjectLimit(FourCC("n003"), Faction.UNLIMITED); //High Elven Guard Tower (Improved)
      Quelthalas.ModObjectLimit(FourCC("h04V"), Faction.UNLIMITED); //Arcane Vault (Elven)
      Quelthalas.ModObjectLimit(FourCC("nheb"), Faction.UNLIMITED); //Cantonment
      Quelthalas.ModObjectLimit(FourCC("n0A2"), Faction.UNLIMITED); //Consortium
      Quelthalas.ModObjectLimit(FourCC("h03J"), Faction.UNLIMITED); //Academy
      Quelthalas.ModObjectLimit(FourCC("h077"), Faction.UNLIMITED); //Alliance Shipyard
      Quelthalas.ModObjectLimit(FourCC("nefm"), Faction.UNLIMITED); //Residence

      //Units
      Quelthalas.ModObjectLimit(FourCC("nbee"), Faction.UNLIMITED); //Elven Worker
      Quelthalas.ModObjectLimit(FourCC("hhes"), Faction.UNLIMITED); //Elven Warrior
      Quelthalas.ModObjectLimit(FourCC("hmpr"), Faction.UNLIMITED); //Priest
      Quelthalas.ModObjectLimit(FourCC("hsor"), Faction.UNLIMITED); //Sorceress
      Quelthalas.ModObjectLimit(FourCC("hdhw"), 6); //Dragonhawk Rider
      Quelthalas.ModObjectLimit(FourCC("nhea"), Faction.UNLIMITED); //Archer
      Quelthalas.ModObjectLimit(FourCC("e008"), 6); //Elven Ballista
      Quelthalas.ModObjectLimit(FourCC("n00A"), 6); //Farstrider
      Quelthalas.ModObjectLimit(FourCC("e01B"), 6); //Arcane Annihilator
      Quelthalas.ModObjectLimit(FourCC("n02F"), 6); //Warlock
      Quelthalas.ModObjectLimit(FourCC("n063"), 12); //Magus
      Quelthalas.ModObjectLimit(FourCC("hspt"), Faction.UNLIMITED); //Spell Breaker
      Quelthalas.ModObjectLimit(FourCC("u00J"), 2); //Arcane Wagon
      Quelthalas.ModObjectLimit(Constants.UNIT_N048_BLOOD_MAGE_QUEL_THALAS, 6);

      //Ships
      Quelthalas.ModObjectLimit(FourCC("hbot"), Faction.UNLIMITED); //Alliance Transport Ship
      Quelthalas.ModObjectLimit(FourCC("h0AR"), Faction.UNLIMITED); //Alliance Scout
      Quelthalas.ModObjectLimit(FourCC("h0AX"), Faction.UNLIMITED); //Alliance Frigate
      Quelthalas.ModObjectLimit(FourCC("h0B3"), Faction.UNLIMITED); //Alliance Fireship
      Quelthalas.ModObjectLimit(FourCC("h0B0"), Faction.UNLIMITED); //Alliance Galley
      Quelthalas.ModObjectLimit(FourCC("h0B6"), Faction.UNLIMITED); //Alliance Boarding
      Quelthalas.ModObjectLimit(FourCC("h0AN"), Faction.UNLIMITED); //Alliance Juggernaut
      Quelthalas.ModObjectLimit(FourCC("h0B7"), 6); //Alliance Bombard

      //Demi-heroes
      Quelthalas.ModObjectLimit(FourCC("n075"), 1); //Vareesa
      Quelthalas.ModObjectLimit(FourCC("Hvwd"), 1); //Sylvanas
      Quelthalas.ModObjectLimit(FourCC("H00Q"), 1); //Anasterian
      Quelthalas.ModObjectLimit(FourCC("H04F"), 1); //Rommath
      Quelthalas.ModObjectLimit(FourCC("H02E"), 1); //Lorthemar

      //Upgrades
      Quelthalas.ModObjectLimit(FourCC("R01S"), Faction.UNLIMITED); //Aimed Shot
      Quelthalas.ModObjectLimit(FourCC("R00G"), Faction.UNLIMITED); //Feint
      Quelthalas.ModObjectLimit(FourCC("R01R"), Faction.UNLIMITED); //Improved Bows
      Quelthalas.ModObjectLimit(FourCC("R029"), Faction.UNLIMITED); //Magus Adept Training
      Quelthalas.ModObjectLimit(FourCC("Rhcd"), Faction.UNLIMITED); //Cloud
      Quelthalas.ModObjectLimit(FourCC("Rhss"), Faction.UNLIMITED); //Control Magic
      Quelthalas.ModObjectLimit(FourCC("Rhlh"), Faction.UNLIMITED); //Improved Lumber Harvesting
      Quelthalas.ModObjectLimit(FourCC("Rhac"), Faction.UNLIMITED); //Improved Masonry
      Quelthalas.ModObjectLimit(FourCC("Rhse"), Faction.UNLIMITED); //Magic Sentry
      Quelthalas.ModObjectLimit(FourCC("Rhpt"), Faction.UNLIMITED); //Priest Adept Training
      Quelthalas.ModObjectLimit(FourCC("Rhst"), Faction.UNLIMITED); //Sorceress Adept Training
      Quelthalas.ModObjectLimit(FourCC("R004"), Faction.UNLIMITED); //Sunfury Warrior Training
      Quelthalas.ModObjectLimit(FourCC("R02Y"), Faction.UNLIMITED); //Improved Glaives
      Quelthalas.ModObjectLimit(Constants.UPGRADE_R0A6_DRAGONHAWK_RIDERS_QUELTHALAS, Faction.UNLIMITED);

      Quelthalas.ModAbilityAvailability(Constants.ABILITY_A0K5_DWARVEN_MASONRY_CASTLES_YELLOW, -1);
      Quelthalas.ModAbilityAvailability(Constants.ABILITY_A0OC_EXTRACT_VIAL_ALL, -1);

      Quelthalas.AddGoldMine(preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(17716, 13000)));

      FactionManager.Register(Quelthalas);
    }
  }
}