using WarcraftLegacies.Shared.Models.Factions;
using static Constants;

namespace WarcraftLegacies.Shared.Config.Factions
{
  public static class StormwindObjectLimitData
  {
    private const int Unlimited = 200;

    public static IEnumerable<ObjectLimit> GetAllObjectLimits()
    {
      yield return new("h06K", Unlimited); //Town Hall
      yield return new("h06M", Unlimited); //Keep
      yield return new("h06N", Unlimited); //Castle
      yield return new("h072", Unlimited); //Farm
      yield return new("h06T", Unlimited); //Altar of Kings
      yield return new("h06E", Unlimited); //Barracks
      yield return new("h06F", Unlimited); //Blacksmith
      yield return new("h06A", Unlimited); //Workshop (Stormwind)
      yield return new("hars", Unlimited); //Arcane Sanctum
      yield return new("h06V", Unlimited); //Scout Tower
      yield return new("h06W", Unlimited); //Guard Tower
      yield return new("h070", Unlimited); //Guard Tower (Improved)
      yield return new("h06X", Unlimited); //Cannon Tower
      yield return new("h071", Unlimited); //Cannon Tower (Improved)
      yield return new("n07T", Unlimited); //Marketplace
      yield return new("h06D", Unlimited); //Alliance Shipyard
      yield return new("h06Y", Unlimited); //Arcane Tower
      yield return new("h06Z", Unlimited); //Arcane Tower (Improved)
      yield return new("h024", Unlimited); //Light House
      yield return new(UNIT_H05J_CHAMPION_S_HALL_STORMWIND_OTHER, 1);
      yield return new(UNIT_H05A_WIZARD_S_SANCTUM_STORMWIND_OTHER, 1);
      yield return new("hpea", Unlimited); //Peasant
      yield return new("h02O", Unlimited); //Militia
      yield return new("h03K", 12); //Marshal
      yield return new("h03Z", 12); //Marshal
      yield return new("h00A", Unlimited); //Spearman
      yield return new("h01B", Unlimited); //Outriders
      yield return new("h05F", 6); //Stormwind Champion
      yield return new("n05L", 6); //Conjurer
      yield return new("h00J", Unlimited); //Clergyman
      yield return new(UNIT_N06N_GUNSHIP_STORMWIND, 6);
      yield return new("n093", Unlimited); //Chaplain
      yield return new("o06K", 5); //Siege Tower
      yield return new("hbot", Unlimited); //Alliance Transport Ship
      yield return new("h0AR", Unlimited); //Alliance Scout
      yield return new("h0AX", Unlimited); //Alliance Frigate
      yield return new("h0B3", Unlimited); //Alliance Fireship
      yield return new("h0B0", Unlimited); //Alliance Galley
      yield return new("h0B6", Unlimited); //Alliance Boarding
      yield return new("h0AN", Unlimited); //Alliance Juggernaut
      yield return new("h0B7", 6); //Alliance Bombard
      yield return new("h060", 3); //Arathor Flagship
      yield return new("H00R", 1); //Varian
      yield return new("H017", 1); //Bolvar
      yield return new(UNIT_H05Y_LORD_WIZARD_STORMWIND, 1);
      yield return new(UNIT_H00Z_CROWN_PRINCE_OF_STROMGARDE_STORMWIND, 1);
      yield return new("R02E", Unlimited); //Chaplain Adept Training
      yield return new("R005", Unlimited); //Clergyman Adept Training
      yield return new("R02B", Unlimited); //Steel Plating
      yield return new("Rhan", Unlimited); //Animal War Training
      yield return new("Rhac", Unlimited); //Improved Masonry
      yield return new("Rhse", Unlimited); //Magic Sentry
      yield return new(UPGRADE_R014_DEEPRUN_TRAM_IRONFORGE, 1);
      yield return new(UPGRADE_R02Z_REFLECTIVE_PLATING_ARATHOR_T2, Unlimited);
      yield return new("R030", Unlimited); //Code of Chivalry
      yield return new("R031", Unlimited); //Elven Refugees
      yield return new(UPGRADE_R03V_MAGES_OF_STROMGARDE_ARATHOR_T2, Unlimited);
      yield return new(UPGRADE_R03W_KNOWLEDGE_OF_HONOR_HOLD_ARATHOR_T2, Unlimited);
      yield return new("R038", Unlimited); //Enforcer Training
      yield return new("R03E", Unlimited); //Saboteur Training
      yield return new("R02Y", Unlimited); //Battle Tactics
      yield return new("R03D", Unlimited); //Veteran Guard
      yield return new("R02W", Unlimited); //Sanctuary of Light
      yield return new("R03A", Unlimited); //Focus In The Light
      yield return new("R03T", Unlimited); //Electric Strike Ritual
      yield return new("R03U", Unlimited); //Solar Flare Ritual
      yield return new("R03X", Unlimited); //Conjurers
    }
  }
}