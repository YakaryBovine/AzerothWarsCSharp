using System.Collections.Generic;
using MacroTools;
using MacroTools.FactionSystem;
using MacroTools.ResearchSystems;
using WarcraftLegacies.Source.Quests.Ironforge;
using WarcraftLegacies.Source.Researches.Ironforge;
using WarcraftLegacies.Source.Setup;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Factions
{
  public sealed class Ironforge : Faction
  {
    private readonly PreplacedUnitSystem _preplacedUnitSystem;
    private readonly AllLegendSetup _allLegendSetup;

    /// <inheritdoc />
    public Ironforge(PreplacedUnitSystem preplacedUnitSystem, AllLegendSetup allLegendSetup) : base("Ironforge",
      PLAYER_COLOR_YELLOW, "|C00FFFC01", @"ReplaceableTextures\CommandButtons\BTNHeroMountainKing.blp")
    {
      _preplacedUnitSystem = preplacedUnitSystem;
      _allLegendSetup = allLegendSetup;
      UndefeatedResearch = FourCC("R05T");
      StartingGold = 200;
      StartingLumber = 700;
      CinematicMusic = "PursuitTheme";
      ControlPointDefenderUnitTypeId = Constants.UNIT_H0AL_CONTROL_POINT_DEFENDER_IRONFORGE;
      IntroText = @"You are playing as the long-enduring |cffe4bc00Kingdom of 
                    |r
You begin in the Wetlands, separated from the rest of your forces. Conquer Loch Modan and Dun Morough to gain access to 
 
Stormwind is preparing for an invasion through the Dark Portal in the South. Muster the throng and help them, or you may lose your strongest ally.";
      GoldMines = new List<unit>
      {
        _preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(12079, -2768))
      };
      RegisterFactionDependentInitializer<Stormwind>(RegisterStormwindResearches);
    }

    /// <inheritdoc />
    public override void OnRegistered()
    {
      RegisterObjectLimits();
      RegisterQuests();
      SharedFactionConfigSetup.AddSharedFactionConfig(this);
    }

    private void RegisterObjectLimits()
    {
      //Structures
      ModObjectLimit(FourCC("h07E"), UNLIMITED); //Town Hall
      ModObjectLimit(FourCC("h07F"), UNLIMITED); //Keep
      ModObjectLimit(FourCC("h07G"), UNLIMITED); //Castle
      ModObjectLimit(FourCC("h02P"), UNLIMITED); //Farm  (Dwarven)
      ModObjectLimit(FourCC("h01S"), UNLIMITED); //Tavern
      ModObjectLimit(FourCC("h07B"), UNLIMITED); //Altar of Kings
      ModObjectLimit(FourCC("h07C"), UNLIMITED); //Barracks
      ModObjectLimit(FourCC("hlum"), UNLIMITED); //Lumber Mill
      ModObjectLimit(FourCC("h048"), UNLIMITED); //Blacksmith (Dwarven)
      ModObjectLimit(FourCC("h042"), UNLIMITED); //Machine Factory
      ModObjectLimit(FourCC("harm"), UNLIMITED); //Workshop
      ModObjectLimit(FourCC("hgra"), UNLIMITED); //Gryphon Aviary
      ModObjectLimit(FourCC("h07H"), UNLIMITED); //Scout Tower
      ModObjectLimit(FourCC("h07J"), UNLIMITED); //Cannon Tower
      ModObjectLimit(FourCC("h07K"), UNLIMITED); //Cannon Tower (Improved)
      ModObjectLimit(FourCC("h07D"), UNLIMITED); //Alliance Shipyard
      ModObjectLimit(FourCC("n07U"), UNLIMITED); //Marketplace
      ModObjectLimit(FourCC("h07I"), UNLIMITED); //Guard Tower
      ModObjectLimit(FourCC("h07L"), UNLIMITED); //Guard Tower (Improved)

      //Units
      ModObjectLimit(FourCC("h019"), UNLIMITED); //Dwarven Worker
      ModObjectLimit(FourCC("hrif"), UNLIMITED); //Rifleman
      ModObjectLimit(FourCC("hmtm"), 9); //Mortar Team
      ModObjectLimit(FourCC("n0CZ"), 4); //Dreadnaught
      ModObjectLimit(FourCC("hgry"), 6); //Gryphon Rider
      ModObjectLimit(FourCC("h018"), UNLIMITED); //Dwarven Warrior
      ModObjectLimit(FourCC("h01L"), 6); //Thane
      ModObjectLimit(FourCC("h037"), UNLIMITED); //Engineer
      ModObjectLimit(FourCC("n02D"), UNLIMITED); //War Golem
      ModObjectLimit(FourCC("h01P"), 3); //Steam Tank
      ModObjectLimit(FourCC("n00C"), UNLIMITED); //Rune Priest
      ModObjectLimit(FourCC("h03Z"), 3); //War Gryphon

      ModObjectLimit(FourCC("h01M"), 1); //Baelgun
      ModObjectLimit(FourCC("H00S"), 1); //Magni
      ModObjectLimit(FourCC("Hmbr"), 1); //Muradin
      ModObjectLimit(Constants.UNIT_H03G_EMPEROR_OF_BLACKROCK_RAGNAROS, 1);
      ModObjectLimit(Constants.UNIT_H028_THANE_OF_AERIE_PEAK_IRONFORGE, 1);

      //Ships
      ModObjectLimit(FourCC("hbot"), UNLIMITED); //Alliance Transport Ship
      ModObjectLimit(FourCC("h0AR"), UNLIMITED); //Alliance Scout
      ModObjectLimit(FourCC("h0AX"), UNLIMITED); //Alliance Frigate
      ModObjectLimit(FourCC("h0B3"), UNLIMITED); //Alliance Fireship
      ModObjectLimit(FourCC("h0B0"), UNLIMITED); //Alliance Galley
      ModObjectLimit(FourCC("h0B6"), UNLIMITED); //Alliance Boarding
      ModObjectLimit(FourCC("h0AN"), UNLIMITED); //Alliance Juggernaut
      ModObjectLimit(FourCC("h0B7"), 6); //Alliance Bombard

      //Upgrades
      ModObjectLimit(FourCC("R03H"), UNLIMITED); //Engineering Adept Training
      ModObjectLimit(FourCC("R00F"), UNLIMITED); //Mithril Armor
      ModObjectLimit(FourCC("Rhfl"), UNLIMITED); //Flare
      ModObjectLimit(FourCC("Rhfs"), UNLIMITED); //Dragmentation Shards
      ModObjectLimit(FourCC("Rhlh"), UNLIMITED); //Improved Lumber Harvesting
      ModObjectLimit(FourCC("Rhac"), UNLIMITED); //Improved Masonry
      ModObjectLimit(FourCC("Rhri"), UNLIMITED); //Long Rifles
      ModObjectLimit(FourCC("Rhhb"), UNLIMITED); //Storm Hammers
      ModObjectLimit(FourCC("R063"), UNLIMITED); //Thunder Ale
      ModObjectLimit(FourCC("R02K"), UNLIMITED); //Gryphon Superior Breed
      ModObjectLimit(Constants.UPGRADE_RHME_PYRITE_FORGED_WEAPONRY_UNIVERSAL_UPGRADE, 2);
      ModObjectLimit(Constants.UPGRADE_RHAR_PYRITE_ARMOR_PLATING_UNIVERSAL_UPGRADE, 2);
      ModObjectLimit(Constants.UPGRADE_R00V_RUNE_PRIEST_MASTER_TRAINING_IRONFORGE, UNLIMITED);
      ModObjectLimit(Constants.UPGRADE_R00Z_ARMOR_PENETRATION_ROUNDS_IRONFORGE, UNLIMITED);
      ModObjectLimit(Constants.UPGRADE_R010_IMPROVED_SPELL_RESISTANCE_IRONFORGE, UNLIMITED);
      ModObjectLimit(Constants.UPGRADE_R00T_OVERCLOCK_IRONFORGE_STEAM_TANK, UNLIMITED);
      ModObjectLimit(Constants.UPGRADE_R00N_IMPROVED_SWIG_IRONFORGE_TAVERN, UNLIMITED);
      ModObjectLimit(Constants.UPGRADE_R014_DEEPRUN_TRAM_IRONFORGE, 1);

      ModAbilityAvailability(Constants.ABILITY_A0IH_SPIKED_BARRICADES_DWARF_KEEP, -1);
      ModAbilityAvailability(Constants.ABILITY_A0GA_SUMMON_GARRISON_LORDAERON, -1);
      ModAbilityAvailability(Constants.ABILITY_A0GD_SUMMON_GARRISON_STORMWIND, -1);
      ModAbilityAvailability(Constants.ABILITY_A0GC_REPLENISH_MANA_ORANGE_KEEPS_CAPITALS, -1);
      ModAbilityAvailability(Constants.ABILITY_A0IH_SPIKED_BARRICADES_DWARF_KEEP, -1);
    }

    private void RegisterQuests()
    {
      var newQuest = AddQuest(new QuestThelsamar(_preplacedUnitSystem, Regions.ThelUnlock));
      StartingQuest = newQuest;
      AddQuest(new QuestDunMorogh(_preplacedUnitSystem));
      AddQuest(new QuestDominion(Regions.IronforgeAmbient));
      AddQuest(new QuestGnomeregan(Regions.Gnomergan, _preplacedUnitSystem));
      AddQuest(new QuestDarkIron(Regions.Shadowforge_City, _allLegendSetup.FelHorde.BlackTemple, _allLegendSetup.Ironforge.Magni));
      AddQuest(new QuestWildhammer(_allLegendSetup.Ironforge.Magni));

      var missingArtifacts = new int[]
      {
        Constants.ITEM_I01A_DEMON_SOUL,
        Constants.ITEM_I00F_GLOVES_OF_AHN_QIRAJ,
        Constants.ITEM_I00Z_THUNDERFURY,
        Constants.ITEM_I015_XAL_ATATH_BLADE_OF_THE_BLACK_EMPIRE,
        Constants.ITEM_I01T_FANDRAL_S_FLAMESCYTHE
      };
      AddQuest(new QuestExpedition(missingArtifacts[GetRandomInt(0, missingArtifacts.Length - 1)]));
    }
    
    private void RegisterStormwindResearches(Stormwind stormwind)
    {
      ResearchManager.Register(new DeeprunTram(this, stormwind, Constants.UPGRADE_R014_DEEPRUN_TRAM_IRONFORGE, 70, 75,
        _preplacedUnitSystem));
    }
  }
}