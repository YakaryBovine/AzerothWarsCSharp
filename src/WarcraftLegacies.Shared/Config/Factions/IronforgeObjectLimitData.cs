using WarcraftLegacies.Shared.Models.Factions;
using static Constants;

namespace WarcraftLegacies.Shared.Config.Factions
{
  public static class IronforgeObjectLimitData
  {
    private const int Unlimited = 200;

    public static IEnumerable<ObjectLimit> GetAllObjectLimits()
    {
      yield return new("h07E", Unlimited); //Town Hall
      yield return new("h07F", Unlimited); //Keep
      yield return new("h07G", Unlimited); //Castle
      yield return new("h02P", Unlimited); //Farm  (Dwarven)
      yield return new("h01S", Unlimited); //Tavern
      yield return new("h07B", Unlimited); //Altar of Kings
      yield return new("h07C", Unlimited); //Barracks
      yield return new("hlum", Unlimited); //Lumber Mill
      yield return new("h048", Unlimited); //Blacksmith (Dwarven)
      yield return new("h042", Unlimited); //Machine Factory
      yield return new("harm", Unlimited); //Workshop
      yield return new("hgra", Unlimited); //Gryphon Aviary
      yield return new("h07H", Unlimited); //Scout Tower
      yield return new("h07J", Unlimited); //Cannon Tower
      yield return new("h07K", Unlimited); //Cannon Tower (Improved)
      yield return new("h07D", Unlimited); //Alliance Shipyard
      yield return new("n07U", Unlimited); //Marketplace
      yield return new("h07I", Unlimited); //Guard Tower
      yield return new("h07L", Unlimited); //Guard Tower (Improved)
      yield return new("h019", Unlimited); //Dwarven Worker
      yield return new("hrif", Unlimited); //Rifleman
      yield return new("hmtm", 9); //Mortar Team
      yield return new("n0CZ", 4); //Dreadnaught
      yield return new("hgry", 6); //Gryphon Rider
      yield return new("h018", Unlimited); //Dwarven Warrior
      yield return new("h01L", 6); //Thane
      yield return new("h037", Unlimited); //Engineer
      yield return new("n02D", Unlimited); //War Golem
      yield return new("h01P", 3); //Steam Tank
      yield return new("n00C", Unlimited); //Rune Priest
      yield return new("h03Z", 3); //War Gryphon
      yield return new("h01M", 1); //Baelgun
      yield return new("H00S", 1); //Magni
      yield return new("Hmbr", 1); //Muradin
      yield return new(UNIT_H03G_EMPEROR_OF_BLACKROCK_RAGNAROS, 1);
      yield return new(UNIT_H028_THANE_OF_AERIE_PEAK_IRONFORGE, 1);
      yield return new("hbot", Unlimited); //Alliance Transport Ship
      yield return new("h0AR", Unlimited); //Alliance Scout
      yield return new("h0AX", Unlimited); //Alliance Frigate
      yield return new("h0B3", Unlimited); //Alliance Fireship
      yield return new("h0B0", Unlimited); //Alliance Galley
      yield return new("h0B6", Unlimited); //Alliance Boarding
      yield return new("h0AN", Unlimited); //Alliance Juggernaut
      yield return new("h0B7", 6); //Alliance Bombard
      yield return new("R03H", Unlimited); //Engineering Adept Training
      yield return new("R00F", Unlimited); //Mithril Armor
      yield return new("Rhfl", Unlimited); //Flare
      yield return new("Rhfs", Unlimited); //Dragmentation Shards
      yield return new("Rhac", Unlimited); //Improved Masonry
      yield return new("Rhri", Unlimited); //Long Rifles
      yield return new("Rhhb", Unlimited); //Storm Hammers
      yield return new("R063", Unlimited); //Thunder Ale
      yield return new("R02K", Unlimited); //Gryphon Superior Breed
      yield return new(UPGRADE_RHME_PYRITE_FORGED_WEAPONRY_UNIVERSAL_UPGRADE, 2);
      yield return new(UPGRADE_RHAR_PYRITE_ARMOR_PLATING_UNIVERSAL_UPGRADE, 2);
      yield return new(UPGRADE_R00V_RUNE_PRIEST_MASTER_TRAINING_IRONFORGE, Unlimited);
      yield return new(UPGRADE_R00Z_ARMOR_PENETRATION_ROUNDS_IRONFORGE, Unlimited);
      yield return new(UPGRADE_R010_IMPROVED_SPELL_RESISTANCE_IRONFORGE, Unlimited);
      yield return new(UPGRADE_R00T_OVERCLOCK_IRONFORGE_STEAM_TANK, Unlimited);
      yield return new(UPGRADE_R00N_IMPROVED_SWIG_IRONFORGE_TAVERN, Unlimited);
      yield return new(UPGRADE_R014_DEEPRUN_TRAM_IRONFORGE, 1);
    }
  }
}