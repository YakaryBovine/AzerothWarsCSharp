using WarcraftLegacies.Shared.Models.Factions;
using static Constants;

namespace WarcraftLegacies.Shared.Config.Factions
{
  public static class SentinelsObjectLimitData
  {
    private const int Unlimited = 200;

    public static IEnumerable<ObjectLimit> GetAllObjectLimits()
    {
      yield return new("e00V", Unlimited); //Temple of Elune
      yield return new("e00R", Unlimited); //Altar of Watchers
      yield return new("e00L", Unlimited); //War Academy
      yield return new("edob", Unlimited); //Hunter)s Hall
      yield return new("eden", Unlimited); //Ancient of Wonders
      yield return new("e011", Unlimited); //Night Elf Shipyard
      yield return new("h03N", Unlimited); //Enchanged Runestone
      yield return new("h03M", Unlimited); //Runestone
      yield return new("n06O", Unlimited); //Sentinel Embassy
      yield return new("n06P", Unlimited); //Sentinel Enclave
      yield return new("n06J", Unlimited); //Sentinel Outpost
      yield return new("n06M", Unlimited); //Residence
      yield return new("edos", Unlimited); //Roost
      yield return new("e00T", Unlimited); //Bastion
      yield return new("ewsp", Unlimited); //Wisp
      yield return new("e006", Unlimited); //Priestess
      yield return new("n06C", Unlimited); //Trapper
      yield return new("h04L", 6); //Priestess of the Moon
      yield return new("earc", Unlimited); //Archer
      yield return new("esen", Unlimited); //Huntress
      yield return new("h08V", Unlimited); //Nightsaber Knight
      yield return new("ebal", 8); //Glaive Thrower
      yield return new("ehpr", 6); //Hippogryph Rider
      yield return new("n034", 12); //Guild Ranger
      yield return new("nwat", Unlimited); //Nightblade
      yield return new("nnmg", 12); //Redeemed Highborne
      yield return new("e022", 2); //Moon Rider
      yield return new(UNIT_ECHM_CHIMAERA_SENTINELS, 6);
      yield return new(UNIT_H045_WARDEN_SENTINELS, 8);
      yield return new("etrs", Unlimited); //Night Elf Transport Ship
      yield return new("h0AU", Unlimited); // Scout
      yield return new("h0AV", Unlimited); // Frigate
      yield return new("h0B1", Unlimited); // Fireship
      yield return new("h057", Unlimited); // Galley
      yield return new("h0B4", Unlimited); // Boarding
      yield return new("h0BA", Unlimited); // Juggernaut
      yield return new("h0B8", 6); // Bombard
      yield return new("E025", 1); //Naisha
      yield return new("Etyr", 1); //Tyrande
      yield return new("E002", 1); //Shandris
      yield return new("Ewrd", 1); //Maiev
      yield return new("R00S", Unlimited); //Priestess Adept Training
      yield return new("R064", Unlimited); //Sentinel Fortifications
      yield return new("R01W", Unlimited); //Trapper Adept Training
      yield return new("Reib", Unlimited); //Improved Bows
      yield return new("Reuv", Unlimited); //Ultravision
      yield return new("Remg", Unlimited); //Upgraded Moon Glaive
      yield return new("Roen", Unlimited); //Ensnare
      yield return new(UPGRADE_R04E_YSERA_S_GIFT_DRUIDS, Unlimited);
      yield return new(UPGRADE_R03J_WIND_WALK_SENTINELS, Unlimited);
      yield return new(UPGRADE_R018_IMPROVED_LIGHTNING_BARRAGE_SENTINELS, Unlimited);
    }
  }
}