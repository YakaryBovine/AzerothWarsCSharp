using WarcraftLegacies.Shared.Models.Factions;
using static Constants;

namespace WarcraftLegacies.Shared.Config.Factions
{
  public static class GilneasObjectLimitData
  {
    private const int Unlimited = 200;

    public static IEnumerable<ObjectLimit> GetAllObjectLimits()
    {
      yield return new("h01R", Unlimited); //Town Hall
      yield return new("h023", Unlimited); //Keep
      yield return new("h02C", Unlimited); //Castle
      yield return new("h02F", Unlimited); //Farm
      yield return new("h02X", Unlimited); //Altar
      yield return new("h039", Unlimited); //Scout Tower
      yield return new("h03A", Unlimited); //Guard Tower
      yield return new("h03B", Unlimited); //Cannon Tower
      yield return new("h03D", Unlimited); //Temple of the cursed
      yield return new("h03E", Unlimited); //Training Hall
      yield return new("n008", Unlimited); //Marketplace
      yield return new("h03H", Unlimited); //Shipyard
      yield return new("h03O", Unlimited); //Blacksmith
      yield return new("h03Q", Unlimited); //Garrison
      yield return new("h052", Unlimited); //Improved Guard Tower
      yield return new("h04N", Unlimited); //Improved Cannon Tower
      yield return new("hpea", Unlimited); //Peasant
      yield return new("n06K", Unlimited); //Druid of the Scythe
      yield return new("h04M", Unlimited); //Cleric
      yield return new("h04E", Unlimited); //Protector
      yield return new("n06L", Unlimited); //Armored Wolf
      yield return new("o01V", 6); //Greyguard
      yield return new("o02J", 12); //Worgen
      yield return new("h03L", Unlimited); //Shotgunner
      yield return new("n067", Unlimited); //Spider summon
      yield return new("o04U", 6); //Cyclone Cannon
      yield return new("o06P", 6); //Worgen Shaman
      yield return new("h04X", 6); //HarvestWitch
      yield return new("E01E", 1); //Goldrinn
      yield return new("Ewar", 1); //Tess
      yield return new("Hhkl", 1); //Genn
      yield return new("Hpb2", 1); //Darius
      yield return new("hbot", Unlimited); //Alliance Transport Ship
      yield return new("h0AR", Unlimited); //Alliance Scout
      yield return new("h0AX", Unlimited); //Alliance Frigate
      yield return new("h0B3", Unlimited); //Alliance Fireship
      yield return new("h0B0", Unlimited); //Alliance Galley
      yield return new("h0B6", Unlimited); //Alliance Boarding
      yield return new("h0AN", Unlimited); //Alliance Juggernaut
      yield return new("h0B7", 6); //Alliance Bombard
      yield return new(UPGRADE_R04O_CLERIC_MASTER_TRAINING_GILNEAS, Unlimited);
      yield return new(UPGRADE_R04P_DRUID_OF_THE_SCYTHE_MASTER_TRAINING_GILNEAS, Unlimited);
      yield return new(
        UPGRADE_RHAC_IMPROVED_MASONRY_ADVANCED_MASONRY_IMBUED_MASONRY_YELLOW_PURPLE_ORANGE_GREEN_DARK_GREEN_RESEARCH,
        Unlimited);
      yield return new(UPGRADE_R09M_HARVEST_WITCH_MASTER_TRAINING_GILNEAS, Unlimited);
    }
  }
}