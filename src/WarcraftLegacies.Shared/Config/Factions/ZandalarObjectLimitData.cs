using WarcraftLegacies.Shared.Models.Factions;

namespace WarcraftLegacies.Shared.Config.Factions
{
  public static class ZandalarObjectLimitData
  {
    private const int Unlimited = 200;

    public static IEnumerable<ObjectLimit> GetAllObjectLimits()
    {
      yield return new("o03R", Unlimited); //Great Hall
      yield return new("o03Y", Unlimited); //Stronghold
      yield return new("o03Z", Unlimited); //Fortress
      yield return new("o040", Unlimited); //Altar of Storms
      yield return new("o041", Unlimited); //Barracks
      yield return new("o042", Unlimited); //War Mill
      yield return new("o044", Unlimited); //Tauren Totem
      yield return new("o043", Unlimited); //Spirit Lodge
      yield return new("o045", Unlimited); //Orc Burrow
      yield return new("o046", Unlimited); //Watch Tower
      yield return new("o048", Unlimited); //Improved Watch Tower
      yield return new("o047", Unlimited); //Voodoo Lounge
      yield return new("o049", Unlimited); //Shipyard
      yield return new("o04X", Unlimited); //Loa Shrine
      yield return new("o04A", Unlimited); //Peon
      yield return new("h021", Unlimited); //Grunt
      yield return new("o04D", Unlimited); //Troll Headhunter
      yield return new("n09E", 2); //Storm Wyrm
      yield return new("e00Z", 8); //Direhorn
      yield return new("o04F", Unlimited); //Troll Witch Doctor
      yield return new("o04G", Unlimited); //Haruspex
      yield return new("o04E", 6); //Boneseer
      yield return new("h05D", Unlimited); //Raptor Rider
      yield return new("o021", 12); //Ravager
      yield return new("nftk", 12); //Warlord
      yield return new("o02K", 6); //Bear Rider
      yield return new("n0DN", 6); //Medium
      yield return new("e01Z", 3); //Throne of War
      yield return new("obot", Unlimited); //Transport Ship
      yield return new("h0AS", Unlimited); //Scout
      yield return new("h0AP", Unlimited); //Frigate
      yield return new("h0B2", Unlimited); //Fireship
      yield return new("h0AY", Unlimited); //Galley
      yield return new("h0B5", Unlimited); //Boarding
      yield return new("h0BC", Unlimited); //Juggernaut
      yield return new("h0AO", 6); //Bombard
      yield return new("O026", 1); //Rasthakan
      yield return new("O01J", 1); //Zul
      yield return new("U023", 1); //Hakkar
      yield return new("H06Q", 1); //Gazrilla
      yield return new("Rers", Unlimited); //Resistant Skin
      yield return new("R00H", Unlimited); //Animal Companion
      yield return new("R070", Unlimited); //Haruspex Training
      yield return new("R071", Unlimited); //Hex Training
    }
  }
}