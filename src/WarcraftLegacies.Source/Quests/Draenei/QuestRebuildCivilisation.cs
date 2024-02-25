using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.QuestSystem;
using WCSharp.Shared.Data;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using static War3Api.Common;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using System.Collections.Generic;
using MacroTools.ObjectiveSystem.Objectives.FactionBased;

namespace WarcraftLegacies.Source.Quests.Draenei
{
  /// <summary>
  /// Build various structures inside <see cref="Regions.AzuremystAmbient"/>
  /// </summary>
  public sealed class QuestRebuildCivilisation : QuestData
  {
    private readonly List<unit> _rescueUnits;
    /// <summary>
    /// Initializes a new instance of the <see cref="QuestRebuildCivilisation"/> class.
    /// </summary>
    public QuestRebuildCivilisation(Rectangle rescueRect, LegendaryHero velen) : base("The Way Forward", "The Draenei will need to rebuild their civilisation in Azeroth. Desolace seems like a perfect place for the birth of the second Draenei settlement.", @"ReplaceableTextures\CommandButtons\BTNDraeneiDivineCitadel.blp")
    {
      
      AddObjective(new ObjectiveHostilesInAreaAreDead(new List<Rectangle> { Regions.DraeneiQuestKill }, "in Desolace"));
      AddObjective(new ObjectiveLegendReachRect(velen, Regions.DesolaceUnlock, "Desolace"));
      AddObjective(new ObjectiveSelfExists());
      _rescueUnits = rescueRect.PrepareUnitsForRescue(RescuePreparationMode.HideAll);
      ResearchId = Constants.UPGRADE_R082_QUEST_COMPLETED_THE_WAY_FORWARD;
    }

    /// <inheritdoc/>
    protected override void OnComplete(Faction whichFaction)
    {
      if (whichFaction.Player != null)
        whichFaction.Player.AddLumber(200);
      if (whichFaction.Player != null)
        whichFaction.Player.RescueGroup(_rescueUnits);
      else
        Player(PLAYER_NEUTRAL_AGGRESSIVE).RescueGroup(_rescueUnits);
    }

    /// <inheritdoc/>
    public override string RewardFlavour => "Maraad joins the Draenai and the new settlement is born";

    /// <inheritdoc/>
    protected override string RewardDescription =>
      "Gain 200 Lumber, an Outpost in Desolace and Maraad is now trainable at the altar.";
  }
}