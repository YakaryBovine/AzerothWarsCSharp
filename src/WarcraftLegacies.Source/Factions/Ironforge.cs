using System.Collections.Generic;
using MacroTools;
using MacroTools.FactionSystem;
using MacroTools.ResearchSystems;
using WarcraftLegacies.Shared.Config.Factions;
using WarcraftLegacies.Source.Quests.Ironforge;
using WarcraftLegacies.Source.Researches.Ironforge;
using WarcraftLegacies.Source.Setup;
using WCSharp.Shared.Data;

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
      TraditionalTeam = TeamSetup.SouthAlliance;
      _preplacedUnitSystem = preplacedUnitSystem;
      _allLegendSetup = allLegendSetup;
      UndefeatedResearch = FourCC("R05T");
      StartingGold = 200;
      CinematicMusic = "PursuitTheme";
      ControlPointDefenderUnitTypeId = UNIT_H0AL_CONTROL_POINT_DEFENDER_IRONFORGE;
      IntroText = @"You are playing as the long-enduring |cffe4bc00Kingdom of 
                    |r
You begin in the Wetlands, separated from the rest of your forces. Conquer Loch Modan and Dun Morough to gain access to 
 
Stormwind is preparing for an invasion through the Dark Portal in the South. Muster the throng and help them, or you may lose your strongest ally.";
      GoldMines = new List<unit>
      {
        _preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(12079, -2768))
      };
      Nicknames = new List<string>
      {
        "if",
        "dwarf",
        "dwarfs",
        "dwarves"
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
      foreach (var (objectTypeId, objectLimit) in IronforgeObjectLimitData.GetAllObjectLimits())
        ModObjectLimit(FourCC(objectTypeId), objectLimit);

      ModAbilityAvailability(ABILITY_A0IH_SPIKED_BARRICADES_DWARF_KEEP, -1);
      ModAbilityAvailability(ABILITY_A0GA_SUMMON_GARRISON_LORDAERON, -1);
      ModAbilityAvailability(ABILITY_A0GD_SUMMON_GARRISON_STORMWIND, -1);
      ModAbilityAvailability(ABILITY_A0GC_REPLENISH_MANA_ORANGE_KEEPS_CAPITALS, -1);
      ModAbilityAvailability(ABILITY_A0IH_SPIKED_BARRICADES_DWARF_KEEP, -1);
    }

    private void RegisterQuests()
    {
      var questThelsamar = AddQuest(new QuestThelsamar(Regions.ThelUnlock));
      StartingQuest = questThelsamar;
      var questDunMorogh = AddQuest(new QuestDunMorogh());
      AddQuest(new QuestDominion(Regions.IronforgeAmbient, questThelsamar, questDunMorogh));
      AddQuest(new QuestGnomeregan(Regions.Gnomergan));
      AddQuest(new QuestDarkIron(Regions.Shadowforge_City, _allLegendSetup.FelHorde.BlackTemple, _allLegendSetup.Ironforge.Magni));
      AddQuest(new QuestWildhammer(_allLegendSetup.Ironforge.Magni));

      var missingArtifacts = new int[]
      {
        ITEM_I01A_DEMON_SOUL,
        ITEM_I00F_GLOVES_OF_AHN_QIRAJ,
        ITEM_I00Z_THUNDERFURY,
        ITEM_I015_XAL_ATATH_BLADE_OF_THE_BLACK_EMPIRE,
        ITEM_I01T_FANDRAL_S_FLAMESCYTHE
      };
      AddQuest(new QuestExpedition(missingArtifacts[GetRandomInt(0, missingArtifacts.Length - 1)]));
    }
    
    private void RegisterStormwindResearches(Stormwind stormwind)
    {
      ResearchManager.Register(new DeeprunTram(this, stormwind, UPGRADE_R014_DEEPRUN_TRAM_IRONFORGE, 70,
        _preplacedUnitSystem));
    }
  }
}