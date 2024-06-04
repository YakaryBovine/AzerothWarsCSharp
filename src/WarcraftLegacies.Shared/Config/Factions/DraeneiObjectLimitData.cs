using WarcraftLegacies.Shared.Models.Factions;

namespace WarcraftLegacies.Shared.Config.Factions
{
  public static class DraeneiObjectLimitData
  {
    private const int Unlimited = 200;

    public static IEnumerable<ObjectLimit> GetAllObjectLimits()
    {
      yield return new("o02P", Unlimited); //Crystal Hall
      yield return new("o050", Unlimited); //Metropolis
      yield return new("o051", Unlimited); //Divine Citadel
      yield return new("o058", Unlimited); //Altar of Light
      yield return new("o052", Unlimited); //Ceremonial Altar
      yield return new("o053", Unlimited); //Smithery
      yield return new("o054", Unlimited); //Astral Sanctum
      yield return new("o055", Unlimited); //Crystal Spire
      yield return new("o056", 48); //Arcane Well
      yield return new("o057", Unlimited); //Vaults of Relic
      yield return new("u00U", Unlimited); //Crystal Protector
      yield return new("u01Q", Unlimited); //Crystal Protector improved
      yield return new("o059", Unlimited); //Improved Ancient Protector
      yield return new("o05U", Unlimited); //Lightforged Gateway
      yield return new("o05A", Unlimited); //Wisp
      yield return new("o05B", Unlimited); //Defender
      yield return new("h09T", Unlimited); //Rangari
      yield return new("e01K", 3); //Polybolos
      yield return new("o05D", Unlimited); //Elementalist
      yield return new("o05C", Unlimited); //Luminarch
      yield return new("h09R", 6); //Vindicator
      yield return new("nmdr", Unlimited); //Elekk
      yield return new("h09U", 4); //Elekk Knight
      yield return new("u02H", 6); //Nether Ray
      yield return new("n0BJ", 6); //Sharpshooter
      yield return new("n0BP", 4); //Juggernaut
      yield return new("n0BM", 8); //Nether Ray
      yield return new("etrs", Unlimited); //Night Elf Transport Ship
      yield return new("h0AU", Unlimited); // Scout
      yield return new("h0AV", Unlimited); // Frigate
      yield return new("h0B1", Unlimited); // Fireship
      yield return new("h057", Unlimited); // Galley
      yield return new("h0B4", Unlimited); // Boarding
      yield return new("h0BA", Unlimited); // Juggernaut
      yield return new("h0B8", 6); // Bombard
      yield return new("H09S", 1); //Maraad
      yield return new("E01I", 1); //Velen
      yield return new("E01J", 1); //Nobundo
      yield return new("H09M", 1); //Adal
      yield return new("R078", Unlimited); //Elementalist training
      yield return new("R07C", Unlimited); //Luminarch training
    }
  }
}