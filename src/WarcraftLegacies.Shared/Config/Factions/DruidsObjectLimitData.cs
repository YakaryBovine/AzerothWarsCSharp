using WarcraftLegacies.Shared.Models.Factions;
using static WarcraftLegacies.Shared.Constants;

namespace WarcraftLegacies.Shared.Config.Factions
{
  public static class DruidsObjectLimitData
  {
    private const int Unlimited = 200;

    public static IEnumerable<ObjectLimit> GetAllObjectLimits()
    {
      yield return new("etol", Unlimited); //Tree of Life
      yield return new("etoa", Unlimited); //Tree of Ages
      yield return new("etoe", Unlimited); //Tree of Eternity
      yield return new("emow", Unlimited); //Moon Well
      yield return new("eate", Unlimited); //Altar of Elders
      yield return new("eaoe", Unlimited); //Ancient of Lore
      yield return new("eaow", Unlimited); //Ancient of Wind
      yield return new("eaom", Unlimited); //Ancient of war
      yield return new("etrp", Unlimited); //Ancient Protector
      yield return new("e010", Unlimited); //Hunter)s Hall
      yield return new("e019", Unlimited); //Ancient of Wonders
      yield return new("eshy", Unlimited); //Night Elf Shipyard
      yield return new("e000", Unlimited); //Improved Ancient Protector
      yield return new("ewsp", Unlimited); //Wisp
      yield return new("edry", Unlimited); //Dryad
      yield return new("edot", Unlimited); //Druid of the Talon
      yield return new("emtg", 12); //Mountain Giant
      yield return new("efdr", 6); //Faerie Dragon
      yield return new("edoc", Unlimited); //Druid of the Claw
      yield return new("edcm", Unlimited); //Druid of the Claw bear form
      yield return new("e00N", 6); //Keeper of the Grove
      yield return new("n05H", Unlimited); //Furbolg
      yield return new("n065", 6); //Green Dragon
      yield return new(UNIT_E012_SIEGE_ANCIENT_DRUIDS_ELITE, 6);
      yield return new("etrs", Unlimited); //Night Elf Transport Ship
      yield return new("h0AU", Unlimited); // Scout
      yield return new("h0AV", Unlimited); // Frigate
      yield return new("h0B1", Unlimited); // Fireship
      yield return new("h057", Unlimited); // Galley
      yield return new("h0B4", Unlimited); // Boarding
      yield return new("h0BA", Unlimited); // Juggernaut
      yield return new("h0B8", 6); // Bombard
      yield return new("Ecen", 1); //Cenarius
      yield return new("E00H", 1); //Cenarius
      yield return new("E00K", 1); //Tortolla
      yield return new("Efur", 1); //Furion
      yield return new(UNIT_E00A_ANCIENT_GUARDIAN_DRUIDS, 1);
      yield return new(UNIT_E00X_ELEMENTAL_GUARDIAN_DRUIDS_DEMI, 1);
      yield return new(UNIT_H04U_DEMIGOD_DRUIDS, 1);
      yield return new("Redt", Unlimited); //Druid of the Talon Adept Training
      yield return new("Renb", Unlimited); //Nature)s Blessing
      yield return new("Rers", Unlimited); //Resistant Skin
      yield return new("Reuv", Unlimited); //Ultravision
      yield return new("Rews", Unlimited); //Well Spring
      yield return new("Redc", Unlimited); //Druid of the Claw Adept Training
      yield return new("R04E", Unlimited); //Ysera)s Gift
      yield return new("R02G", Unlimited); //Emerald Flames
      yield return new("R05X", Unlimited); //Blessing of Ursoc
      yield return new("R002", Unlimited); //Blackwald Enhancement
      yield return new("R00A", Unlimited); //Improved Thorns
      yield return new("R02T", Unlimited); //Improved Moonwells
      yield return new("R033", Unlimited); //Limber Timber
      yield return new("R046", Unlimited); //Grasping Vines
      yield return new("R047", Unlimited); //Crippling Poison
      yield return new("R048", Unlimited); //Deadly Poison
      yield return new("R008", Unlimited); //Improved Natures FuryR015
      yield return new("R015", UPGRADE_R015_IMPROVED_MANA_FLARE_DRUIDS);
      yield return new(UPGRADE_R09V_STORM_CROW_FORM_DRUIDS, Unlimited);
    }
  }
}