using MacroTools.ControlPointSystem;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.ControlPointBased;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using MacroTools.QuestSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Druids
{
  public sealed class QuestAndrassil : QuestData
  {
    private const int UrsocId = Constants.UNIT_E00A_ANCIENT_GUARDIAN_DRUIDS;

    public QuestAndrassil(Capital lichKing) : base("Crown of the Snow",
      "Long ago, Fandral Staghelm cut a sapling from Nordrassil and used it to grow Andrassil in Northrend. Without the blessing of the Aspects, it fell to the Old Gods' corruption. If Northrend were to be reclaimed, Andrassil's growth could begin anew.",
      @"ReplaceableTextures\CommandButtons\BTNTreant.blp")
    {
      AddObjective(new ObjectiveCapitalDead(lichKing));
      AddObjective(new ObjectiveControlPoint(ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N03U_GRIZZLY_HILLS)));
      AddObjective(new ObjectiveAnyUnitInRect(Regions.GrizzlyHills, "Grizzly Hills", true));
      ResearchId = Constants.UPGRADE_R002_QUEST_COMPLETED_CROWN_OF_THE_SNOW_DRUIDS;
    }
    
    /// <inheritdoc/>
    protected override string RewardFlavour =>
      "With Northrend finally free of the Lich King's influence, the time is ripe to regrow Andrassil in the hope that it can help reclaim this barren land.";

    /// <inheritdoc/>
    protected override string RewardDescription =>
      "A new capital at Grizzly Hills that can research a powerful upgrade for your Druids of the Claw, and you can train the hero Ursoc from the Altar of Elders";

    /// <inheritdoc/>
    protected override void OnComplete(Faction completingFaction)
    {
      CreateUnit(completingFaction.Player, Constants.UNIT_N04F_ANDRASSIL_DRUID_OTHER, GetRectCenterX(Regions.Andrassil.Rect),
        GetRectCenterY(Regions.Andrassil.Rect), 0);
    }

    /// <inheritdoc/>
    protected override void OnAdd(Faction whichFaction)
    {
      whichFaction.ModObjectLimit(Constants.UPGRADE_R05X_BLESSING_OF_URSOL_DRUIDS, Faction.UNLIMITED);
      whichFaction.ModObjectLimit(UrsocId, 1);
    }
  }
}