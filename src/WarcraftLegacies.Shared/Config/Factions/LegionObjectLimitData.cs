using WarcraftLegacies.Shared.Models.Factions;
using static WarcraftLegacies.Shared.Constants;

namespace WarcraftLegacies.Shared.Config.Factions
{
  public static class LegionObjectLimitData
  {
    private const int Unlimited = 200;

    public static IEnumerable<ObjectLimit> GetAllObjectLimits()
    {
      yield return new("u00H", Unlimited); //Legion Defensive Pylon
      yield return new("u00I", Unlimited); //Improved Defensive Pylon
      yield return new("u00F", Unlimited); //Dormant Spire
      yield return new("u00C", Unlimited); //Legion Bastion
      yield return new("u00N", Unlimited); //Burning Citadel
      yield return new("n040", Unlimited); //Armory
      yield return new("u009", Unlimited); //Undead Shipyard
      yield return new("u00E", Unlimited); //Generator
      yield return new("u01N", Unlimited); //Burning Altar
      yield return new(UNIT_U015_UNHOLY_RELIQUARY_LEGION_SHOP, Unlimited);
      yield return new("ndmg", 6); //Demon Gate
      yield return new(UNIT_N04N_INFERNAL_SIEGEWORKS_LEGION_SPECIALIST, Unlimited);
      yield return new(UNIT_U006_SUMMONING_CIRCLE_LEGION_MAGIC, 3);
      yield return new(UNIT_N04Q_NETHER_PIT_LEGION_BARRACKS, 3);
      yield return new(UNIT_U00F_DORMANT_SPIRE_LEGION_T1, Unlimited);
      yield return new(UNIT_U00C_LEGION_BASTION_LEGION_T2, Unlimited);
      yield return new(UNIT_U00N_BURNING_CITADEL_LEGION_T3, Unlimited);
      yield return new("u00D", Unlimited); //Legion Herald
      yield return new("u007", 6); //Dreadlord
      yield return new("n04P", Unlimited); //Warlock
      yield return new("ninc", Unlimited); //Burning archer
      yield return new("n04K", Unlimited); //Succubus
      yield return new("n04J", Unlimited); //Felstalker
      yield return new("n0DO", 12); //Doom Guard
      yield return new("n04O", 6); //Doom lord
      yield return new("n04L", 6); //Infernal Juggernaut
      yield return new("o04P", 6); //Nathrezim
      yield return new(UNIT_NINF_INFERNAL_LEGION, 6);
      yield return new("n04H", Unlimited); //Fel Guard
      yield return new("n04U", 4); //Dragon
      yield return new("n03L", 4); //Barge
      yield return new("ubot", Unlimited); //Undead Transport Ship
      yield return new("h0AT", Unlimited); //Scout
      yield return new("h0AW", Unlimited); //Frigate
      yield return new("h0AM", Unlimited); //Fireship
      yield return new("h0AZ", Unlimited); //Galley
      yield return new("h0AQ", Unlimited); //Boarding
      yield return new("h0BB", Unlimited); //Juggernaut
      yield return new("h0B9", 6); //Bombard
      yield return new("n05R", 1); //Felguard
      yield return new("n06H", 1); //Pit Fiend
      yield return new("n07B", 1); //Queen
      yield return new("n07D", 1); //Maiden
      yield return new("n07o", 1); //Terror
      yield return new("n07N", 1); //Lord
      yield return new("R02C", Unlimited); //Acute Sensors
      yield return new("R028", Unlimited); //Shadow Priest Adept Training
      yield return new("R042", Unlimited); //Nathrezim Adept Training
      yield return new("R027", Unlimited); //Warlock Adept Training
      yield return new("R04G", Unlimited); //Improved Carrion Swarm
      yield return new("R03Z", Unlimited); //War Plating
      yield return new(UPGRADE_R096_REMATERIALIZATION_LEGION, 1);
      yield return new(UPGRADE_R04R_FORTIFIED_HULLS_UNIVERSAL_UPGRADE, 1);
      yield return new(UPGRADE_R03L_IMPROVED_HEAL_FEL_HORDE, 1);
      yield return new("U00L", 1); //Anetheron
      yield return new(UNIT_UMAL_THE_CUNNING_LEGION, 1);
      yield return new(UNIT_UTIC_THE_DARKENER_LEGION, 1);
    }
  }
}