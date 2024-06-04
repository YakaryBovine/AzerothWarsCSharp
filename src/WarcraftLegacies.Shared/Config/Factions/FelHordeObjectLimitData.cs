using WarcraftLegacies.Shared.Models.Factions;
using static WarcraftLegacies.Shared.Constants;

namespace WarcraftLegacies.Shared.Config.Factions
{
  public static class FelHordeObjectLimitData
  {
    private const int Unlimited = 200;

    public static IEnumerable<ObjectLimit> GetAllObjectLimits()
    {
      yield return new("o02Y", Unlimited); //Great Hall
      yield return new("o02Z", Unlimited); //Stronghold
      yield return new("o030", Unlimited); //Fortress
      yield return new("o02V", Unlimited); //Altar of Storms
      yield return new("o02W", Unlimited); //Barracks
      yield return new("o031", Unlimited); //War Mill
      yield return new("o033", Unlimited); //Spirit Lodge
      yield return new("o02X", Unlimited); //Bestiary
      yield return new("o032", Unlimited); //Shipyard
      yield return new("o034", Unlimited); //Watch Tower
      yield return new("o035", Unlimited); //Improved Watch Tower
      yield return new("u00Q", Unlimited); //Hellforge
      yield return new("n0AM", Unlimited); //Boulder Tower
      yield return new("n0AN", Unlimited); //Advanced Boulder Tower
      yield return new("ocbw", Unlimited); //Burrow
      yield return new("n0AP", Unlimited); //Focal Demon Gate
      yield return new("nbdk", 6); //Black Drake
      yield return new("odkt", 6); //Eredar Warlock
      yield return new("nchw", Unlimited); //Fel Orc Warlock
      yield return new("nchg", Unlimited); //Fel Orc Grunt
      yield return new("nchr", Unlimited); //Fel Orc Raider
      yield return new("ncpn", Unlimited); //Fel Orc Peon
      yield return new("owar", 12); //Horde Cavarly - Bonebreaker
      yield return new("o01L", 6); //Executioner
      yield return new("o01O", 8); //Demolisher
      yield return new("u018", 10); //Eye of Grillok
      yield return new("u00V", Unlimited); //Necrolyte
      yield return new("n058", Unlimited); //Troll Axethrowers
      yield return new(UNIT_NINA_INFERNAL_JUGGERNAUT_FEL_HORDE, 4);
      yield return new(UNIT_N086_FEL_DEATH_KNIGHT_FEL_HORDE_ELITE_TIER, 6);
      yield return new("obot", Unlimited); //Transport Ship
      yield return new("h0AS", Unlimited); //Scout
      yield return new("h0AP", Unlimited); //Frigate
      yield return new("h0B2", Unlimited); //Fireship
      yield return new("h0AY", Unlimited); //Galley
      yield return new("h0B5", Unlimited); //Boarding
      yield return new("h0BC", Unlimited); //Juggernaut
      yield return new("h0AO", 6); //Bombard
      yield return new("n05T", 1); //Kazzak
      yield return new("n08A", 1); //neltharaktu
      yield return new("N03D", 1); //Kargath
      yield return new("Nbbc", 1); //Rend
      yield return new("U02D", 1); //Teron
      yield return new("Nmag", 1); //Magtheridon
      yield return new("Robf", Unlimited); //Demonic Flux
      yield return new("R066", Unlimited); //Burning Oil
      yield return new("R00O", Unlimited); //Ensnare
      yield return new("Rorb", Unlimited); //Reinforced Defenses
      yield return new("Rosp", Unlimited); //Spiked Barricades
      yield return new("R024", Unlimited); //Necrolyte adept Training
      yield return new("R00M", Unlimited); //Warlock Adept Training
      yield return new("R03I", Unlimited); //Eredar Warlock Adept Training
      yield return new("R00Y", Unlimited); //Improved Self-Bloodlust
      yield return new("R03L", Unlimited); //Improved Shadow Infusion
      yield return new("R036", Unlimited); //Incinerate
      yield return new("R02L", Unlimited); //Bloodcraze
      yield return new("R03O", Unlimited); //Bloodcraze
      yield return new("R034", Unlimited); //Enhanced Breath
      yield return new("R035", Unlimited); //Improved Firebolt
      yield return new("R01Z", Unlimited); //Battle Stations
      yield return new(UPGRADE_R098_FEL_INFUSED_SKELETON_FEL_HORDE, Unlimited);
      yield return new(UPGRADE_R09W_IMPROVED_GREATER_CARRION_SWARM_LEGION, Unlimited);
      yield return new("n05R", Unlimited); //Felguard
      yield return new("n06H", Unlimited); //Pit Fiend
      yield return new("n07B", Unlimited); //Queen
      yield return new("n07D", Unlimited); //Maiden
      yield return new("n07o", Unlimited); //Terror
      yield return new("n07N", Unlimited); //Lord
      yield return new("R090", Unlimited); //Blackrock
    }
  }
}