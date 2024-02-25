using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.Libraries;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using MacroTools.QuestSystem;
using static MacroTools.Libraries.GeneralHelpers;
using static War3Api.Common; 

namespace WarcraftLegacies.Source.Quests.Lordaeron
{
  /// <summary>
  /// Research 'Northrend Expidition' to gain a base at the shores of Dragonblight.
  /// </summary>
  public sealed class QuestShoresOfNorthrend : QuestData
  {
    private readonly LegendaryHero _arthas;
    private new const int ResearchId = Constants.UPGRADE_R06F_NORTHREND_EXPEDITION_LORDAERON;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="QuestShoresOfNorthrend"/> class.
    /// </summary>
    public QuestShoresOfNorthrend(LegendaryHero arthas, Capital caerDarrow) : base("Shores of Northrend", "Mal'ganis' citadel lies somewhere within the arctic wastes of the north. In order to assault the Dreadlord, Arthas must first establish a base camp at the shores of Northrend.", @"ReplaceableTextures\CommandButtons\BTNHumanTransport.blp")
    {
      _arthas = arthas;
      AddObjective(new ObjectiveControlLegend(arthas, true));
      AddObjective(new ObjectiveControlCapital(caerDarrow, false));
      AddObjective(new ObjectiveResearch(ResearchId, Constants.UNIT_HSHY_SHIPYARD_LORDAERON_SHIPYARD));
    }

    /// <inheritdoc/>
    public override string RewardFlavour =>
      "Crown Prince Arthas, and what remains of his forces, have landed on the shores of Northrend and established a base camp.";

    /// <inheritdoc/>
    protected override string RewardDescription =>
      "A new base near Dragonblight in Northrend, and Arthas revives there";

    /// <inheritdoc/>
    protected override void OnFail(Faction completingFaction)
    {
      completingFaction.ModObjectLimit(ResearchId, -Faction.UNLIMITED);
    }

    /// <inheritdoc/>
    protected override void OnComplete(Faction completingFaction)
    {
      KillNeutralHostileUnitsInRadius(-512, 15776, 2000);
      if (GetOwningPlayer(_arthas.Unit) == completingFaction.Player)
      {
        ReviveHero(_arthas.Unit, 400, 16102, true);
        BlzSetUnitFacingEx(_arthas.Unit, 112);
      }
      if(completingFaction.Player != null)
      {
        CreateStructureForced(completingFaction.Player, Constants.UNIT_H01C_HUNTSMAN_LORDAERON, -513, 16679, 4.757993f * MathEx.DegToRad, 256);
        CreateStructureForced(completingFaction.Player, Constants.UNIT_NMRK_MARKETPLACE_LORDAERON_SHOP, 1280, 16064, 4.712389f * MathEx.DegToRad, 256);
        CreateStructureForced(completingFaction.Player, Constants.UNIT_HCTW_CANNON_TOWER_LORDAERON_TOWER, -640, 16576, 4.712389f * MathEx.DegToRad, 256);
        CreateStructureForced(completingFaction.Player, Constants.UNIT_HBAR_BARRACKS_LORDAERON_BARRACKS, -256, 16832, 4.712389f * MathEx.DegToRad, 256);
        CreateStructureForced(completingFaction.Player, Constants.UNIT_HALT_ALTAR_OF_KINGS_LORDAERON_ALTAR, 416, 16416, 4.712389f * MathEx.DegToRad, 256);
        CreateStructureForced(completingFaction.Player, Constants.UNIT_HPEA_PEASANT_LORDAERON_STORMWIND_WORKER, 818, 16864, 6.156587f * MathEx.DegToRad, 256);
        CreateStructureForced(completingFaction.Player, Constants.UNIT_HPEA_PEASANT_LORDAERON_STORMWIND_WORKER, 624, 16725, 4.578159f * MathEx.DegToRad, 256);
        CreateStructureForced(completingFaction.Player, Constants.UNIT_HGTW_GUARD_TOWER_LORDAERON_TOWER, -960, 15872, 4.712389f * MathEx.DegToRad, 256);
        CreateStructureForced(completingFaction.Player, Constants.UNIT_HDES_DESTROYER_UNUSED, 582, 15512, 4.3173f * MathEx.DegToRad, 256);
        CreateStructureForced(completingFaction.Player, Constants.UNIT_HSHY_SHIPYARD_LORDAERON_SHIPYARD, 800, 15776, 4.712389f * MathEx.DegToRad, 256);
        CreateStructureForced(completingFaction.Player, Constants.UNIT_HCAS_CASTLE_LORDAERON_T3, -512, 15744, 4.712389f * MathEx.DegToRad, 512);
        CreateStructureForced(completingFaction.Player, Constants.UNIT_HBLA_BLACKSMITH_LORDAERON_RESEARCH, 672, 16928, 4.712389f * MathEx.DegToRad, 256);
        CreateStructureForced(completingFaction.Player, Constants.UNIT_HFOO_FOOTMAN_LORDAERON, 771, 16064, 0.6401012f * MathEx.DegToRad, 256);
        CreateStructureForced(completingFaction.Player, Constants.UNIT_HGTW_GUARD_TOWER_LORDAERON_TOWER, -448, 16128, 4.712389f * MathEx.DegToRad, 256);
        CreateStructureForced(completingFaction.Player, Constants.UNIT_HWTW_SCOUT_TOWER_LORDAERON_TOWER, 704, 17152, 4.712389f * MathEx.DegToRad, 256);
        CreateStructureForced(completingFaction.Player, Constants.UNIT_HHOU_FARM_LORDAERON_FARM, -1088, 16576, 4.712389f * MathEx.DegToRad, 256);
        CreateStructureForced(completingFaction.Player, Constants.UNIT_H035_ARCANE_STUDY_LORDAERON_MAGIC, -928, 16736, 4.712389f * MathEx.DegToRad, 256);
        CreateStructureForced(completingFaction.Player, Constants.UNIT_HFOO_FOOTMAN_LORDAERON, -174, 16631, 3.987584f * MathEx.DegToRad, 256);
        CreateStructureForced(completingFaction.Player, Constants.UNIT_HFOO_FOOTMAN_LORDAERON, -388, 16871, 4.113693f * MathEx.DegToRad, 256);
        CreateStructureForced(completingFaction.Player, Constants.UNIT_HFOO_FOOTMAN_LORDAERON, -561, 16521, 6.02386f * MathEx.DegToRad, 256);
        CreateStructureForced(completingFaction.Player, Constants.UNIT_H0B0_GALLEY_ALLIANCE, 251, 15569, 5.33097f * MathEx.DegToRad, 256);
        CreateStructureForced(completingFaction.Player, Constants.UNIT_HBLA_BLACKSMITH_LORDAERON_RESEARCH, 800, 16288, 4.712389f * MathEx.DegToRad, 256);
        CreateStructureForced(completingFaction.Player, Constants.UNIT_HGTW_GUARD_TOWER_LORDAERON_TOWER, 1472, 16384, 4.712389f * MathEx.DegToRad, 256);
        CreateStructureForced(completingFaction.Player, Constants.UNIT_HKNI_KNIGHT_LORDAERON, 893, 16175, 4.130178f * MathEx.DegToRad, 256);
        CreateStructureForced(completingFaction.Player, Constants.UNIT_NCHP_MAGE_LORDAERON, -931, 16554, 5.458206f * MathEx.DegToRad, 256);
      }
    
      completingFaction.ModObjectLimit(ResearchId, -Faction.UNLIMITED);
    }

    /// <inheritdoc/>
    protected override void OnAdd(Faction whichFaction)
    {
      whichFaction.ModObjectLimit(ResearchId, Faction.UNLIMITED);
    }
  }
}