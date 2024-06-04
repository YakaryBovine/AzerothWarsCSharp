using WarcraftLegacies.Shared.Models.Factions;
using static WarcraftLegacies.Shared.Constants;

namespace WarcraftLegacies.Shared.Config.Factions
{
  public static class DalaranObjectLimitData
  {
    private const int Unlimited = 200;

    public static IEnumerable<ObjectLimit> GetAllObjectLimits()
    {
      yield return new("h065", Unlimited); //Refuge
      yield return new("h066", Unlimited); //Conclave
      yield return new("h068", Unlimited); //Observatory
      yield return new("h063", Unlimited); //Granary
      yield return new("h044", Unlimited); //Altar of Knowledge
      yield return new("h069", Unlimited); //Barracks
      yield return new("h02N", Unlimited); //Trade Quarters
      yield return new("h036", Unlimited); //Arcane Sanctuary
      yield return new("h078", Unlimited); //Scout Tower
      yield return new("h079", Unlimited); //Arcane Tower
      yield return new("h07A", Unlimited); //Arcane Tower (Improved)
      yield return new("hvlt", Unlimited); //Arcane Vault
      yield return new("h076", Unlimited); //Alliance Shipyard
      yield return new("ndgt", Unlimited); //Dalaran Tower
      yield return new("n004", Unlimited); //Dalaran Tower (Improved)
      yield return new("h067", Unlimited); //Laboratory
      yield return new("n0AO", 2); //Way Gate
      yield return new("h022", Unlimited); //Shaper
      yield return new("nhym", Unlimited); //Hydromancer
      yield return new("h032", Unlimited); //Battlemage
      yield return new("h02D", Unlimited); //Geomancer
      yield return new("h01I", Unlimited); //Arcanist
      yield return new("n007", 6); //Kirin Tor
      yield return new("n096", 6); //Earth Golem
      yield return new("n03E", Unlimited); //Pyromancer
      yield return new("n0AK", Unlimited); //Sludge Flinger
      yield return new("o02U", 6); //Crystal Artillery
      yield return new(UNIT_N0AC_BLUE_DRAGON_DALARAN, 6);
      yield return new(UNIT_HBOT_TRANSPORT_SHIP_ALLIANCE, Unlimited);
      yield return new(UNIT_H0AR_SCOUT_SHIP_ALLIANCE, Unlimited);
      yield return new(UNIT_H0AX_FRIGATE_ALLIANCE, Unlimited);
      yield return new(UNIT_H0B3_FIRESHIP_ALLIANCE, Unlimited);
      yield return new(UNIT_H0B0_GALLEY_ALLIANCE, Unlimited);
      yield return new(UNIT_H0B6_BOARDING_VESSEL_ALLIANCE, Unlimited);
      yield return new(UNIT_H0AN_JUGGERNAUT_ALLIANCE, Unlimited);
      yield return new(UNIT_H0B7_BOMBARD_ALLIANCE, 6);
      yield return new(UNIT_NJKS_JAILOR_KASSAN_DALARAN_DEMI, 1);
      yield return new(UNIT_HJAI_ARCHMAGE_OF_DALARAN_DALARAN, 1);
      yield return new(UNIT_HANT_GRAND_MAGUS_OF_THE_KIRIN_TOR_DALARAN, 1);
      yield return new(UNIT_H09N_MATRIARCH_OF_TIRISFAL_DALARAN, 1);
      yield return new(UNIT_HAAH_THE_FALLEN_GUARDIAN_DALARAN, 1);
      yield return new(UPGRADE_R01I_ARCANIST_GRANDMASTER_TRAINING_DALARAN, Unlimited);
      yield return new(UPGRADE_R01V_GEOMANCER_GRANDMASTER_TRAINING_DALARAN, Unlimited);
      yield return new(UPGRADE_R00E_HYDROMANCER_GRANDMASTER_TRAINING_DALARAN, Unlimited);
      yield return new(
        UPGRADE_RHAC_IMPROVED_MASONRY_ADVANCED_MASONRY_IMBUED_MASONRY_YELLOW_PURPLE_ORANGE_GREEN_DARK_GREEN_RESEARCH,
        Unlimited);
      yield return new(UPGRADE_R06J_IMPROVED_SLOW_DALARAN, Unlimited);
      yield return new(UPGRADE_R061_IMPROVED_FORKED_LIGHTNING_DALARAN, Unlimited);
      yield return new(UPGRADE_R06O_IMPROVED_PHASE_BLADE_DALARAN, Unlimited);
    }
  }
}