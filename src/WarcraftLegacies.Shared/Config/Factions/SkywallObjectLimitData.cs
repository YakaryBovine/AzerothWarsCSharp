using WarcraftLegacies.Shared.Models.Factions;

namespace WarcraftLegacies.Shared.Config.Factions
{
  public static class SkywallObjectLimitData
  {
    private const int Unlimited = 200;

    public static IEnumerable<ObjectLimit> GetAllObjectLimits()
    {
      yield return new(Constants.UNIT_N05Q_PILLAR_OF_WAVES_TOLVIR_T1, Unlimited);
      yield return new(Constants.UNIT_N05W_MONUMENT_OF_THE_DEEP_TOLVIR_T2, Unlimited);
      yield return new(Constants.UNIT_N06R_TEMPLE_OF_TIDES_TOLVIR_T3, Unlimited);
      yield return new(Constants.UNIT_H03I_CORAL_FORGE_TOLVIR_RESEARCH, Unlimited);
      yield return new(Constants.UNIT_N08A_SPAWNING_GROUNDS_TOLVIR_BARRACKS, Unlimited);
      yield return new(Constants.UNIT_N07N_ROYAL_TEMPLE_TOLVIR_MAGIC, Unlimited);
      yield return new(Constants.UNIT_N08B_SHRINE_OF_AZSHARA_TOLVIR_SPECIALIST, Unlimited);
      yield return new(Constants.UNIT_N08V_PORTAL_OF_THE_DEEP_NZOTH_SIEGE, Unlimited);
      yield return new(Constants.UNIT_N07J_DEEP_VAULT_TOLVIR_SHOP, Unlimited);
      yield return new(Constants.UNIT_N078_ALTAR_OF_THE_QUEEN_TOLVIR_ALTAR, Unlimited);
      yield return new(Constants.UNIT_N08L_TEMPLE_PROTECTOR_TOLVIR_TOWER, Unlimited);
      yield return new(Constants.UNIT_N08N_IMPROVED_TEMPLE_PROTECTOR_TOLVIR_TOWER, Unlimited);
      yield return new(Constants.UNIT_N0D7_DEEP_FORAGER_N_ZOTH_WORKER, Unlimited);
      yield return new(Constants.UNIT_N0DT_TIDEMISTRESS_NZOTH, 6);
      yield return new(Constants.UNIT_N0CA_NAGA_INCURSOR_NZOTH, Unlimited);
      yield return new(Constants.UNIT_N0CE_HATCHLING_NZOTH, Unlimited);
      yield return new(Constants.UNIT_N0CL_GARGANTUAN_SEA_TURTLE_NZOTH, 8);
      yield return new(Constants.UNIT_N0CB_DEEPSEER_NZOTH, Unlimited);
      yield return new(Constants.UNIT_N0CD_BLOODKIN_NZOTH, Unlimited);
      yield return new(Constants.UNIT_O060_RIPTIDE_DRAKE_NZOTH, 6);
      yield return new(Constants.UNIT_U02J_TIDAL_TERROR_NZOTH, 4);
      yield return new(Constants.UNIT_H01Q_IMMORTAL_GUARDIAN_NZOTH, 4);
      yield return new(Constants.UNIT_N0CO_TRENCH_HYDRA_NZOTH, 2);
      yield return new("e020", Unlimited); //Shipyard
      yield return new("etrs", Unlimited); //Night Elf Transport Ship
      yield return new("h0AU", Unlimited); // Scout
      yield return new("h0AV", Unlimited); // Frigate
      yield return new("h0B1", Unlimited); // Fireship
      yield return new("h057", Unlimited); // Galley
      yield return new("h0B4", Unlimited); // Boarding
      yield return new("h0BA", Unlimited); // Juggernaut
      yield return new("h0B8", 6); // Bombard
      yield return new(Constants.UNIT_U02K_LORD_OF_THE_FIRELANDS_RAGNAROS, 1);
      yield return new(Constants.UNIT_E023_GRAND_VIZIER_TOLVIR, 1);
      yield return new(Constants.UNIT_U01S_WINDLORD_TWILIGHT, 1);
    }
  }
}