using MacroTools;
using MacroTools.FactionSystem;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Setup.FactionSetup
{
  /// <summary>
  /// Responsible for creating and containing the Ironforge <see cref="Faction"/>.
  /// </summary>
  public static class IronforgeSetup
  {
    public static Faction? Ironforge { get; private set; }
    
    public static void Setup(PreplacedUnitSystem preplacedUnitSystem)
    {
      Ironforge = new Faction("Ironforge", PLAYER_COLOR_YELLOW, "|C00FFFC01",
        @"ReplaceableTextures\CommandButtons\BTNHeroMountainKing.blp")
      {
        UndefeatedResearch = FourCC("R05T"),
        StartingGold = 200,
        StartingLumber = 700,
        CinematicMusic = "PursuitTheme",
        ControlPointDefenderUnitTypeId = Constants.UNIT_H0AL_CONTROL_POINT_DEFENDER_IRONFORGE,
        IntroText = @"You are playing as the long-enduring |cffe4bc00Kingdom of Ironforge.
                    |r
You begin in the Wetlands, separated from the rest of your forces. Conquer Loch Modan and Dun Morough to gain access to Ironforge.
 
Stormwind is preparing for an invasion through the Dark Portal in the South. Muster the throng and help them, or you may lose your strongest ally."
      };

      //Structures
      Ironforge.ModObjectLimit(FourCC("h07E"), Faction.UNLIMITED); //Town Hall
      Ironforge.ModObjectLimit(FourCC("h07F"), Faction.UNLIMITED); //Keep
      Ironforge.ModObjectLimit(FourCC("h07G"), Faction.UNLIMITED); //Castle
      Ironforge.ModObjectLimit(FourCC("h02P"), Faction.UNLIMITED); //Farm  (Dwarven)
      Ironforge.ModObjectLimit(FourCC("h01S"), Faction.UNLIMITED); //Tavern
      Ironforge.ModObjectLimit(FourCC("h07B"), Faction.UNLIMITED); //Altar of Kings
      Ironforge.ModObjectLimit(FourCC("h07C"), Faction.UNLIMITED); //Barracks
      Ironforge.ModObjectLimit(FourCC("hlum"), Faction.UNLIMITED); //Lumber Mill
      Ironforge.ModObjectLimit(FourCC("h048"), Faction.UNLIMITED); //Blacksmith (Dwarven)
      Ironforge.ModObjectLimit(FourCC("h042"), Faction.UNLIMITED); //Machine Factory
      Ironforge.ModObjectLimit(FourCC("harm"), Faction.UNLIMITED); //Workshop
      Ironforge.ModObjectLimit(FourCC("hgra"), Faction.UNLIMITED); //Gryphon Aviary
      Ironforge.ModObjectLimit(FourCC("h07H"), Faction.UNLIMITED); //Scout Tower
      Ironforge.ModObjectLimit(FourCC("h07J"), Faction.UNLIMITED); //Cannon Tower
      Ironforge.ModObjectLimit(FourCC("h07K"), Faction.UNLIMITED); //Cannon Tower (Improved)
      Ironforge.ModObjectLimit(FourCC("h07D"), Faction.UNLIMITED); //Alliance Shipyard
      Ironforge.ModObjectLimit(FourCC("n07U"), Faction.UNLIMITED); //Marketplace
      Ironforge.ModObjectLimit(FourCC("h07I"), Faction.UNLIMITED); //Guard Tower
      Ironforge.ModObjectLimit(FourCC("h07L"), Faction.UNLIMITED); //Guard Tower (Improved)

      //Units
      Ironforge.ModObjectLimit(FourCC("h019"), Faction.UNLIMITED); //Dwarven Worker
      Ironforge.ModObjectLimit(FourCC("hrif"), Faction.UNLIMITED); //Rifleman
      Ironforge.ModObjectLimit(FourCC("hmtm"), 9); //Mortar Team
      Ironforge.ModObjectLimit(FourCC("n0CZ"), 4); //Dreadnaught
      Ironforge.ModObjectLimit(FourCC("hgry"), 6); //Gryphon Rider
      Ironforge.ModObjectLimit(FourCC("h018"), Faction.UNLIMITED); //Dwarven Warrior
      Ironforge.ModObjectLimit(FourCC("h01L"), 6); //Thane
      Ironforge.ModObjectLimit(FourCC("h037"), Faction.UNLIMITED); //Engineer
      Ironforge.ModObjectLimit(FourCC("n02D"), Faction.UNLIMITED); //War Golem
      Ironforge.ModObjectLimit(FourCC("h01P"), 3); //Steam Tank
      Ironforge.ModObjectLimit(FourCC("n00C"), Faction.UNLIMITED); //Rune Priest
      Ironforge.ModObjectLimit(FourCC("h03Z"), 3); //War Gryphon

      Ironforge.ModObjectLimit(FourCC("h01M"), 1); //Baelgun
      Ironforge.ModObjectLimit(FourCC("H00S"), 1); //Magni
      Ironforge.ModObjectLimit(FourCC("Hmbr"), 1); //Muradin

      //Ships
      Ironforge.ModObjectLimit(FourCC("hbot"), Faction.UNLIMITED); //Alliance Transport Ship
      Ironforge.ModObjectLimit(FourCC("h0AR"), Faction.UNLIMITED); //Alliance Scout
      Ironforge.ModObjectLimit(FourCC("h0AX"), Faction.UNLIMITED); //Alliance Frigate
      Ironforge.ModObjectLimit(FourCC("h0B3"), Faction.UNLIMITED); //Alliance Fireship
      Ironforge.ModObjectLimit(FourCC("h0B0"), Faction.UNLIMITED); //Alliance Galley
      Ironforge.ModObjectLimit(FourCC("h0B6"), Faction.UNLIMITED); //Alliance Boarding
      Ironforge.ModObjectLimit(FourCC("h0AN"), Faction.UNLIMITED); //Alliance Juggernaut
      Ironforge.ModObjectLimit(FourCC("h0B7"), 6); //Alliance Bombard

      //Upgrades
      Ironforge.ModObjectLimit(FourCC("R03H"), Faction.UNLIMITED); //Engineering Adept Training
      Ironforge.ModObjectLimit(FourCC("R00K"), Faction.UNLIMITED); //Power Infusion
      Ironforge.ModObjectLimit(FourCC("R00F"), Faction.UNLIMITED); //Mithril Armor
      Ironforge.ModObjectLimit(FourCC("Rhfl"), Faction.UNLIMITED); //Flare
      Ironforge.ModObjectLimit(FourCC("Rhfs"), Faction.UNLIMITED); //Dragmentation Shards
      Ironforge.ModObjectLimit(FourCC("Rhlh"), Faction.UNLIMITED); //Improved Lumber Harvesting
      Ironforge.ModObjectLimit(FourCC("Rhac"), Faction.UNLIMITED); //Improved Masonry
      Ironforge.ModObjectLimit(FourCC("Rhri"), Faction.UNLIMITED); //Long Rifles
      Ironforge.ModObjectLimit(FourCC("Rhhb"), Faction.UNLIMITED); //Storm Hammers
      Ironforge.ModObjectLimit(FourCC("R063"), Faction.UNLIMITED); //Thunder Ale
      Ironforge.ModObjectLimit(FourCC("R02K"), Faction.UNLIMITED); //Gryphon Superior Breed
      Ironforge.ModObjectLimit(Constants.UPGRADE_RHME_PYRITE_FORGED_WEAPONRY_UNIVERSAL_UPGRADE, 2);
      Ironforge.ModObjectLimit(Constants.UPGRADE_RHAR_PYRITE_ARMOR_PLATING_UNIVERSAL_UPGRADE, 2);
      Ironforge.ModObjectLimit(Constants.UPGRADE_R00V_RUNE_PRIEST_MASTER_TRAINING_IRONFORGE, Faction.UNLIMITED);
      Ironforge.ModObjectLimit(Constants.UPGRADE_R00Z_ARMOR_PENETRATION_ROUNDS_IRONFORGE, Faction.UNLIMITED);
      Ironforge.ModObjectLimit(Constants.UPGRADE_R010_IMPROVED_SPELL_RESISTANCE_IRONFORGE, Faction.UNLIMITED);
      Ironforge.ModObjectLimit(Constants.UPGRADE_R00T_OVERCLOCK_IRONFORGE_STEAM_TANK, Faction.UNLIMITED);
      Ironforge.ModObjectLimit(Constants.UPGRADE_R00N_IMPROVED_SWIG_IRONFORGE_TAVERN, Faction.UNLIMITED);
      Ironforge.ModObjectLimit(Constants.UPGRADE_R014_DEEPRUN_TRAM_IRONFORGE, 1);

      Ironforge.ModAbilityAvailability(Constants.ABILITY_A0IH_SPIKED_BARRICADES_DWARF_KEEP, -1);
      Ironforge.ModAbilityAvailability(Constants.ABILITY_A0GA_SUMMON_GARRISON_LORDAERON, -1);
      Ironforge.ModAbilityAvailability(Constants.ABILITY_A0GD_SUMMON_GARRISON_STORMWIND, -1);
      Ironforge.ModAbilityAvailability(Constants.ABILITY_A0GC_REPLENISH_MANA_ORANGE_KEEPS_CAPITALS, -1);
      Ironforge.ModAbilityAvailability(Constants.ABILITY_A0IH_SPIKED_BARRICADES_DWARF_KEEP, -1);
      
      Ironforge.AddGoldMine(preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(12079, -2768)));
      
      FactionManager.Register(Ironforge);
    }
  }
}