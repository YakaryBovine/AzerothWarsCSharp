using WarcraftLegacies.Shared.Models.Factions;
using static WarcraftLegacies.Shared.Constants;

namespace WarcraftLegacies.Shared.Config.Factions
{
  public static class KultirasObjectLimitData
  {
    private const int Unlimited = 200;

    public static IEnumerable<ObjectLimit> GetAllObjectLimits()
    {
      yield return new("h062", Unlimited); //Town Hall
      yield return new("h064", Unlimited); //Keep
      yield return new("h06I", Unlimited); //Castle
      yield return new("h07N", Unlimited); //Farm
      yield return new("h07M", Unlimited); //Altar
      yield return new("h07R", Unlimited); //Scout Tower
      yield return new("h07S", Unlimited); //Guard Tower
      yield return new("h07T", Unlimited); //Improved Guard Tower
      yield return new("h07U", Unlimited); //Cannon Tower
      yield return new("h07V", Unlimited); //Improved Cannon Tower
      yield return new("h07O", Unlimited); //Blacksmith
      yield return new("h07Q", Unlimited); //Arcane Sanctum
      yield return new("n07H", Unlimited); //Marketplace
      yield return new("h07W", Unlimited); //Shipyard
      yield return new("h06R", Unlimited); //Garrison
      yield return new("h07P", Unlimited); //Workshop
      yield return new("h093", Unlimited); //Workshop
      yield return new("h01E", Unlimited); //Deckhand
      yield return new("e007", Unlimited); //Thornspeaker
      yield return new("n09A", 12); //Ember Cleric
      yield return new("n09B", 8); //Witch Hunter
      yield return new("h092", 4); //Order Inquisitor
      yield return new("h05K", Unlimited); //Tidesage
      yield return new("h041", Unlimited); //Marine
      yield return new("e00B", Unlimited); //Thornspeaker Bear
      yield return new("n009", 12); //Revenant of the Tides
      yield return new("n07G", 6); //muskateer
      yield return new("n029", 12); //Sea Giant
      yield return new("h06J", Unlimited); //Sniper
      yield return new("o01A", 6); //Cannon Team
      yield return new("h04O", 12); //Bomber
      yield return new("h04W", 3); //Siege Tank
      yield return new("h0A0", 8); //Fusillier
      yield return new("hbot", Unlimited); //Alliance Transport Ship
      yield return new("h0AR", Unlimited); //Alliance Scout
      yield return new("h0AX", Unlimited); //Alliance Frigate
      yield return new("h0B3", Unlimited); //Alliance Fireship
      yield return new("h0B0", Unlimited); //Alliance Galley
      yield return new("h0B6", Unlimited); //Alliance Boarding
      yield return new("h0AN", Unlimited); //Alliance Juggernaut
      yield return new("h0B7", 6); //Alliance Bombard
      yield return new("R001", Unlimited); //Rising Tides
      yield return new("R000", Unlimited); //Tidesage Adept Training
      yield return new("R01O", Unlimited); //Crushing Wave
      yield return new("R01T", Unlimited); //Cluster Rockets
      yield return new("R01U", Unlimited); //Improved Barrage
      yield return new("R05G", Unlimited); //Thornspeaker Training
      yield return new("Rhac", Unlimited); //Improved Masonry
      yield return new("R08B", Unlimited); //Long Rifles
      yield return new("R05J", Unlimited); //Expedition
      yield return new(UNIT_HAPM_LORD_ADMIRAL_OF_KUL_TIRAS_KUL_TIRAS, 1);
      yield return new(UNIT_H05L_LADY_OF_HOUSE_PROUDMOORE_KUL_TIRAS, 1);
      yield return new(UNIT_U026_MATRIARCH_OF_HOUSE_WAYCREST_KULTIRAS, 1);
    }
  }
}