using WarcraftLegacies.Shared.Models.Factions;
using static WarcraftLegacies.Shared.Constants;

namespace WarcraftLegacies.Shared.Config.Factions
{
  public static class QuelthalasObjectLimitData
  {
    private const int Unlimited = 200;

    public static IEnumerable<ObjectLimit> GetAllObjectLimits()
    {
      yield return new("h033", Unlimited); //Steading
      yield return new("h03S", Unlimited); //Mansion
      yield return new("h03T", Unlimited); //Palace
      yield return new("h01H", Unlimited); //Altar of Prowess
      yield return new("h02Y", Unlimited); //Artisan)s Hall
      yield return new("h034", Unlimited); //Arcane Sanctum (Quel)thalas)
      yield return new("h073", Unlimited); //Scout Tower
      yield return new("h074", Unlimited); //Arcane Tower
      yield return new("h075", Unlimited); //Arcane Tower (Improved)
      yield return new("negt", Unlimited); //High Elven Guard Tower
      yield return new("n003", Unlimited); //High Elven Guard Tower (Improved)
      yield return new("h04V", Unlimited); //Arcane Vault (Elven)
      yield return new("nheb", Unlimited); //Cantonment
      yield return new("n0A2", Unlimited); //Consortium
      yield return new("h03J", Unlimited); //Academy
      yield return new("h077", Unlimited); //Alliance Shipyard
      yield return new("nefm", Unlimited); //Residence
      yield return new("nbee", Unlimited); //Elven Worker
      yield return new("hhes", Unlimited); //Elven Warrior
      yield return new("hmpr", Unlimited); //Priest
      yield return new("hsor", Unlimited); //Sorceress
      yield return new("hdhw", 6); //Dragonhawk Rider
      yield return new("nhea", Unlimited); //Archer
      yield return new("e008", 6); //Elven Ballista
      yield return new("n00A", 6); //Farstrider
      yield return new("e01B", 6); //Arcane Annihilator
      yield return new("n02F", 6); //Warlock
      yield return new("n063", 12); //Magus
      yield return new("hspt", Unlimited); //Spell Breaker
      yield return new("u00J", 2); //Arcane Wagon
      yield return new(UNIT_N048_BLOOD_MAGE_QUEL_THALAS, 6);
      yield return new("hbot", Unlimited); //Alliance Transport Ship
      yield return new("h0AR", Unlimited); //Alliance Scout
      yield return new("h0AX", Unlimited); //Alliance Frigate
      yield return new("h0B3", Unlimited); //Alliance Fireship
      yield return new("h0B0", Unlimited); //Alliance Galley
      yield return new("h0B6", Unlimited); //Alliance Boarding
      yield return new("h0AN", Unlimited); //Alliance Juggernaut
      yield return new("h0B7", 6); //Alliance Bombard
      yield return new("n075", 1); //Vareesa
      yield return new("Hvwd", 1); //Sylvanas
      yield return new("H00Q", 1); //Anasterian
      yield return new("H04F", 1); //Rommath
      yield return new("H02E", 1); //Lorthemar
      yield return new("R01S", Unlimited); //Aimed Shot
      yield return new("R00G", Unlimited); //Feint
      yield return new("R01R", Unlimited); //Improved Bows
      yield return new("R029", Unlimited); //Magus Adept Training
      yield return new("Rhcd", Unlimited); //Cloud
      yield return new("Rhss", Unlimited); //Control Magic
      yield return new("Rhac", Unlimited); //Improved Masonry
      yield return new("Rhse", Unlimited); //Magic Sentry
      yield return new("Rhpt", Unlimited); //Priest Adept Training
      yield return new("Rhst", Unlimited); //Sorceress Adept Training
      yield return new("R004", Unlimited); //Sunfury Warrior Training
      yield return new("R02Y", Unlimited); //Improved Glaives
    }
  }
}