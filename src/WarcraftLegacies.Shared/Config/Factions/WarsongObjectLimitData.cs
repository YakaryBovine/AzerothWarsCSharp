using WarcraftLegacies.Shared.Models.Factions;
using static Constants;

namespace WarcraftLegacies.Shared.Config.Factions
{
  public static class WarsongObjectLimitData
  {
    private const int Unlimited = 200;

    public static IEnumerable<ObjectLimit> GetAllObjectLimits()
    {
      yield return new("o00C", Unlimited); //Great Hall
      yield return new("o02R", Unlimited); //Stronghold
      yield return new("o02S", Unlimited); //Fortress
      yield return new("o020", Unlimited); //Altar of Storms
      yield return new("o01S", Unlimited); //Barracks
      yield return new("o009", Unlimited); //War Mill
      yield return new("o006", Unlimited); //Ogre Barrack
      yield return new("o05G", Unlimited); //Siege Workshop
      yield return new("o02Q", Unlimited); //Bestiary
      yield return new("o028", Unlimited); //Orc Burrow
      yield return new("n03E", Unlimited); //Watch Tower
      yield return new("o01H", Unlimited); //Troll Shrine
      yield return new("n0AL", Unlimited); //Improved Watch Tower
      yield return new("o02T", Unlimited); //Shipyard
      yield return new("o01T", Unlimited); //Goblin Hardware Shop
      yield return new("o04L", Unlimited); //Peon
      yield return new("o02M", Unlimited); //Grunt
      yield return new("orai", Unlimited); //Raider
      yield return new("n08E", Unlimited); //Hexbinder
      yield return new("otbk", Unlimited); //Troll Berseker
      yield return new("nogn", Unlimited); //Stonemaul Ogre Magi
      yield return new("o00I", 6); //Horde War Machine
      yield return new("e01M", 4); //Azerite Siege Engine
      yield return new("okod", 4); //Kodo Beast
      yield return new("o00G", 6); //Blademaster
      yield return new("n03F", 6); //Korkron elite
      yield return new("owyv", 8); //Wind Rider
      yield return new("obot", Unlimited); //Transport Ship
      yield return new("h0AS", Unlimited); //Scout
      yield return new("h0AP", Unlimited); //Frigate
      yield return new("h0B2", Unlimited); //Fireship
      yield return new("h0AY", Unlimited); //Galley
      yield return new("h0B5", Unlimited); //Boarding
      yield return new("h0BC", Unlimited); //Juggernaut
      yield return new("h0AO", 6); //Bombard
      yield return new("Ogrh", 1); //Grom
      yield return new("Obla", 1); //Varok
      yield return new("O06L", 1); //Garrosh
      yield return new(UNIT_NSJS_BREWMASTER_WARSONG, 1);
      yield return new("n0CN", 1); //Gibbs
      yield return
        new(UNIT_OPGH_CORRUPTOR_OF_THE_WARSONG_CLAN_WARSONG_BLOODPACT,
          1); //Blood Pact Grom   Fixes Perma Death Grom Blood Pact bug
      yield return new("Robs", Unlimited); //Berserker Strength
      yield return new("Rotr", Unlimited); //Troll Regeneration
      yield return new("R01J", Unlimited); //Ensnare
      yield return new("R02I", Unlimited); //Ogre Magi Adept Training
      yield return new("R03Q", Unlimited); //Warlock Adept Training
      yield return new("Rorb", Unlimited); //Reinforced Defenses
      yield return new("Rosp", Unlimited); //Spiked Barricades
      yield return new("R016", Unlimited); //Warlords
      yield return new("R019", Unlimited); //Improved Shockwave
      yield return new(UPGRADE_R00D_MASS_BLOODLUST_FROSTWOLF, Unlimited);
      yield return new(UPGRADE_ROVS_ENVENOMED_SPEARS_WARSONG, Unlimited);
      yield return new(UPGRADE_R09N_FLIGHT_PATH_WARSONG, 1);
      yield return new(UPGRADE_R09O_DRINK_THE_BLOOD_OF_MANNOROTH, 1);
      yield return new(UPGRADE_R09P_REVERT_BLOODPACT, 1);
    }
  }
}