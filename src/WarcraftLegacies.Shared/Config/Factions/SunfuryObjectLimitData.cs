using WarcraftLegacies.Shared.Models.Factions;
using static WarcraftLegacies.Shared.Constants;

namespace WarcraftLegacies.Shared.Config.Factions
{
  public static class SunfuryObjectLimitData
  {
    private const int Unlimited = 200;

    public static IEnumerable<ObjectLimit> GetAllObjectLimits()
    {
      yield return new("h02P", Unlimited); //t1
      yield return new("h0C4", Unlimited); //t2
      yield return new("h0C5", Unlimited); //t3
      yield return new("h0C7", 3); //house
      yield return new("h0C8", Unlimited); //forge
      yield return new("h0C9", Unlimited); //barrack
      yield return new("h0CB", Unlimited); //magic
      yield return new("h0CA", Unlimited); //VOid Well
      yield return new("h0CI", Unlimited); //Tempest-Forge
      yield return new("h0C6", Unlimited); //Altar
      yield return new("h0CC", Unlimited); //Vault
      yield return new("h0CD", Unlimited); //Scout tower
      yield return new("n0E0", Unlimited); //Skyfury tower
      yield return new("n0E1", Unlimited); //improved skyfury tower
      yield return new("N0DZ", 1); //Fountain
      yield return new("tp04", Unlimited); //Alliance Shipyard
      yield return new("n0E2", Unlimited); //worker
      yield return new("n09S", Unlimited); //Elven Warrior
      yield return new("h0CF", Unlimited); //Elven Ranger
      yield return new("u02W", 2); //Energy Wagon
      yield return new("h0CH", Unlimited); //Astromancer
      yield return new("h0CG", Unlimited); //Flamekeeper
      yield return new(UNIT_H0CE_BLOOD_KNIGHT_SQUIRE_SUNFURY, 12);
      yield return new("n0E3", 6); //Warlock
      yield return new("n0E4", 6); //Elven Ballista
      yield return new("n0E8", 3); //Skyship
      yield return new("n0E7", 6); //Bloodwarder
      yield return new("e01B", 6); //Arcane Annihilator
      yield return new("n006", 2); //Ancient of the Arcane
      yield return new("hbot", Unlimited); //Alliance Transport Ship
      yield return new("h0AR", Unlimited); //Alliance Scout
      yield return new("h0AX", Unlimited); //Alliance Frigate
      yield return new("h0B3", Unlimited); //Alliance Fireship
      yield return new("h0B0", Unlimited); //Alliance Galley
      yield return new("h0B6", Unlimited); //Alliance Boarding
      yield return new("h0AN", Unlimited); //Alliance Juggernaut
      yield return new("h0B7", 6); //Alliance Bombard
      yield return new("H098", 1); //Pathaleon
      yield return new("U02V", 1); //Solarian
      yield return new("Hkal", 1); //Kael
      yield return new("U004", 1); //Kil
      yield return new(UNIT_N0E5_VOID_REAVER_SUNFURY_DEMI, 1);
      yield return new(UPGRADE_R09H_ASTROMANCER_MASTER_TRAINING_SUNFURY, Unlimited);
      yield return new(UPGRADE_R09G_FLAMEKEEPER_MASTER_TRAINING_SUNFURY, Unlimited);
      yield return new(UPGRADE_R09U_SEAL_OF_BLOOD_SUNFURY, Unlimited);
    }
  }
}