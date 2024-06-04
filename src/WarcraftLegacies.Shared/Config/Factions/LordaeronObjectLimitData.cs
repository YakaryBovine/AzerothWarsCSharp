using WarcraftLegacies.Shared.Models.Factions;
using static WarcraftLegacies.Shared.Constants;

namespace WarcraftLegacies.Shared.Config.Factions
{
  public static class LordaeronObjectLimitData
  {
    private const int Unlimited = 200;

    public static IEnumerable<ObjectLimit> GetAllObjectLimits()
    {
      yield return new("htow", Unlimited); //Town Hall
      yield return new("hkee", Unlimited); //Keep
      yield return new("hcas", Unlimited); //Castle
      yield return new("hhou", Unlimited); //Farm
      yield return new("halt", Unlimited); //Altar of Kings
      yield return new("hbar", Unlimited); //Barracks
      yield return new("hbla", Unlimited); //Blacksmith
      yield return new("h035", Unlimited); //Chapel
      yield return new("hwtw", Unlimited); //Scout Tower
      yield return new("hgtw", Unlimited); //Guard Tower
      yield return new("h006", Unlimited); //Guard Tower (Improved)
      yield return new("hctw", Unlimited); //Cannon Tower
      yield return new("h007", Unlimited); //Cannon Tower (Improved)
      yield return new("hshy", Unlimited); //Alliance Shipyard
      yield return new("nmrk", Unlimited); //Marketplace
      yield return new("h06C", Unlimited); //Aviary
      yield return new("h094", Unlimited); //Siege Camp
      yield return new("hpea", Unlimited); //Peasant
      yield return new("hfoo", Unlimited); //Footman
      yield return new("hkni", Unlimited); //Knight
      yield return new("nchp", Unlimited); //Cleric
      yield return new("h00F", 6); //Paladin
      yield return new("nwe2", 6); //Thunder Eagle
      yield return new("h01C", Unlimited); //Longbowman
      yield return new("n03K", Unlimited); //Chaplain
      yield return new("hcth", 12); //Silver Hand Squire
      yield return new("h02Q", 6); //Pegasus Knight
      yield return new("e017", 8); //Scorpion
      yield return new("o02F", 6); //Mangonel
      yield return new("h09Y", 2); //Throne Guard
      yield return new("hbot", Unlimited); //Alliance Transport Ship
      yield return new("h0AR", Unlimited); //Alliance Scout
      yield return new("h0AX", Unlimited); //Alliance Frigate
      yield return new("h0B3", Unlimited); //Alliance Fireship
      yield return new("h0B0", Unlimited); //Alliance Galley
      yield return new("h0B6", Unlimited); //Alliance Boarding
      yield return new("h0AN", Unlimited); //Alliance Juggernaut
      yield return new("h0B7", 6); //Alliance Bombard
      yield return new(UNIT_H012_CAPTAIN_FALRIC_LORDAERON_DEMI, 1);
      yield return new(UNIT_HART_CROWN_PRINCE_OF_LORDAERON_LORDAERON, 1);
      yield return new(UNIT_HUTH_LEADER_OF_THE_SILVER_HAND_LORDAERON, 1);
      yield return new(UNIT_H01J_THE_ASHBRINGER_LORDAERON, 1);
      yield return new("Hlgr", 1); //Garithos
      yield return new(UNIT_HARF_HIGH_KING_LORDAERON_HIGH_KING, 1);
      yield return new(UPGRADE_R02E_LIGHT_S_PRAISE_MASTER_TRAINING_ARATHOR_LORDAERON, Unlimited);
      yield return new(UPGRADE_R00I_MAGE_MASTER_TRAINING_LORDAERON, Unlimited);
      yield return new(UPGRADE_RHAN_ANIMAL_WAR_TRAINING_DARK_GREEN_PURPLE_RESEARCH, Unlimited);
      yield return new(
        UPGRADE_RHAC_IMPROVED_MASONRY_ADVANCED_MASONRY_IMBUED_MASONRY_YELLOW_PURPLE_ORANGE_GREEN_DARK_GREEN_RESEARCH,
        Unlimited);
      yield return new(UPGRADE_R04D_SEAL_OF_RIGHTEOUSNESS_LORDAERON, Unlimited);
      yield return new(UPGRADE_R01P_ENSNARE_LORDAERON, Unlimited);
      yield return new(UPGRADE_R04A_RAPID_FIRE_LORDAERON, Unlimited);
      yield return new(UPGRADE_R00B_VETERAN_FOOTMEN_LORDAERON, Unlimited);
      yield return new(UPGRADE_R0XZ_THE_SCARLET_CRUSADE_LORDAERON_SCARLET, Unlimited);
    }
  }
}