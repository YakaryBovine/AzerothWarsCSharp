using WarcraftLegacies.Shared.Models.Factions;
using static Constants;

namespace WarcraftLegacies.Shared.Config.Factions
{
  public static class FrostwolfObjectLimitData
  {
    private const int Unlimited = 200;

    public static IEnumerable<ObjectLimit> GetAllObjectLimits()
    {
      yield return new(UNIT_OGRE_GREAT_HALL_FROSTWOLF_T1, Unlimited);
      yield return new(UNIT_OSTR_STRONGHOLD_FROSTWOLF_T2, Unlimited);
      yield return new(UNIT_OFRT_FORTRESS_FROSTWOLF_T3, Unlimited);
      yield return new(UNIT_OALT_ALTAR_OF_STORMS_FROSTWOLF_ALTAR, Unlimited);
      yield return new(UNIT_OBAR_WAR_CAMP_FROSTWOLF_BARRACKS, Unlimited);
      yield return new(UNIT_OFOR_WAR_MILL_FROSTWOLF_RESEARCH, Unlimited);
      yield return new(UNIT_OTTO_TAUREN_TOTEM_FROSTWOLF_SIEGE, Unlimited);
      yield return new(UNIT_OSLD_SPIRIT_LODGE_FROSTWOLF_MAGIC, Unlimited);
      yield return new(UNIT_OTRB_BURROW_FROSTWOLF_FARM, Unlimited);
      yield return new(UNIT_OWTW_WATCH_TOWER_FROSTWOLF_TOWER, Unlimited);
      yield return new(UNIT_O002_IMPROVED_WATCH_TOWER_FROSTWOLF_TOWER_2, Unlimited);
      yield return new(UNIT_OVLN_VOODOO_LOUNGE_FROSTWOLF_SHOP, Unlimited);
      yield return new(UNIT_OSHY_HORDE_PIER_FROSTWOLF_SHIPYARD, Unlimited);
      yield return new(UNIT_OOSC_PACK_KODO_FROSTWOLF, Unlimited);
      yield return new(UNIT_OBEA_BEASTIARY_FROSTWOLF_SPECIALIST, Unlimited);
      yield return new(UNIT_OPEO_PEON_FROSTWOLF_WARSONG_WORKER, Unlimited);
      yield return new(UNIT_OGRU_GRUNT_FROSTWOLF, Unlimited);
      yield return new(UNIT_OTAU_TAUREN_FROSTWOLF, Unlimited);
      yield return new(UNIT_OHUN_HEADHUNTER_FROSTWOLF, Unlimited);
      yield return new(UNIT_OCAT_CATAPULT_FROSTWOLF, 6);
      yield return new(UNIT_OTBR_BATRIDER_FROSTWOLF, 12);
      yield return new(UNIT_ODOC_WITCH_DOCTOR_FROSTWOLF, Unlimited);
      yield return new(UNIT_OSHM_SHAMAN_FROSTWOLF, Unlimited);
      yield return new(UNIT_OSPW_SPIRIT_WALKER_FROSTWOLF, Unlimited);
      yield return new(UNIT_O00A_FAR_SEER_FROSTWOLF_ELITE, 6);
      yield return new(UNIT_O06T_TAUREN_GLADIATOR_FROSTWOLF, 6);
      yield return new(UNIT_H0CN_PACKLEADER_FROSTWOLF, 4);
      yield return new(UNIT_H0CO_MAMMOTH_WRANGLER_FROSTWOLF, 2);
      yield return new(UNIT_N049_WANDERER_FROSTWOLF, 4);
      yield return new("obot", Unlimited); //Transport Ship
      yield return new("h0AS", Unlimited); //Scout
      yield return new("h0AP", Unlimited); //Frigate
      yield return new("h0B2", Unlimited); //Fireship
      yield return new("h0AY", Unlimited); //Galley
      yield return new("h0B5", Unlimited); //Boarding
      yield return new("h0BC", Unlimited); //Juggernaut
      yield return new("h0AO", 6); //Bombard
      yield return new("h00C", 1); //Drek)thar
      yield return new("Othr", 1); //Thrall
      yield return new("Ocbh", 1); //Cairne
      yield return new("Orkn", 1); //Voljin
      yield return new("Orex", 1); //Rexxar
      yield return new("Rows", Unlimited); //Improved Pulverize
      yield return new("Rost", Unlimited); //Shaman Adept Training
      yield return new("Rowd", Unlimited); //Witch Doctor Adept Training
      yield return new("Rowt", Unlimited); //Spirit Walker Adept Training
      yield return new("Rolf", Unlimited); //Liquid Fire
      yield return new("Rosp", Unlimited); //Spiked Barricades
      yield return new("Rorb", Unlimited); //reinforced Defenses
      yield return new("R00H", Unlimited); //Animal Companion
      yield return new("R00R", Unlimited); //Improved Chain Lightning
      yield return new("R00W", Unlimited); //Toughened Hides
      yield return new("R01Z", Unlimited); //Battle Stations
      yield return new(UPGRADE_R09N_FLIGHT_PATH_WARSONG, 1);
    }
  }
}