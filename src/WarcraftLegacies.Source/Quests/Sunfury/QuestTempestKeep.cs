using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.QuestSystem;
using WCSharp.Shared.Data;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using static War3Api.Common;
using MacroTools.ObjectiveSystem.Objectives.TimeBased;
using System.Collections.Generic;
using MacroTools.ObjectiveSystem.Objectives.FactionBased;

namespace WarcraftLegacies.Source.Quests.Sunfury
{
  /// <summary>
  /// Build various structures inside <see cref="Regions.TempestKeep"/>
  /// </summary>
  public sealed class QuestTempestKeep : QuestData
  {
    private readonly List<unit> _rescueUnits;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestTempestKeep"/> class.
    /// </summary>
    public QuestTempestKeep(Rectangle rescueRect, Rectangle questRect1, Rectangle questRect2, Rectangle questRect3) :
      base("Eco-domes",
        "The Sunfury must learn to adapt to their new home within the inhospitable Netherstorm. There are several eco-domes dotted throughout the region, remnants of Netherstorm's prior existence as the verdant Farahlon. They would make excellent locations for growth facilities.",
        @"ReplaceableTextures\CommandButtons\BTNFeatherMoonAura.blp")
    {
      
      AddObjective(new ObjectiveUpgrade(Constants.UNIT_H0C5_SANCTUARY_SUNFURY_T3,
        Constants.UNIT_H02P_HOLDING_SUNFURY_T1));
      AddObjective(new ObjectiveBuildInRect(questRect1, "in one of the 3 Eco-dome in Netherstorm",
        Constants.UNIT_H0C7_ARBORETUM_SUNFURY_FARM));
      AddObjective(new ObjectiveBuildInRect(questRect2, "in one of the 3 Eco-dome in Netherstorm",
        Constants.UNIT_H0C7_ARBORETUM_SUNFURY_FARM));
      AddObjective(new ObjectiveBuildInRect(questRect3, "in one of the 3 Eco-dome in Netherstorm",
        Constants.UNIT_H0C7_ARBORETUM_SUNFURY_FARM));
      AddObjective(new ObjectiveExpire(600, Title));
      AddObjective(new ObjectiveSelfExists());
      _rescueUnits = rescueRect.PrepareUnitsForRescue(RescuePreparationMode.HideAll);
      ResearchId = Constants.UPGRADE_R09I_QUEST_COMPLETED_ECO_DOMES;
    }

    /// <inheritdoc/>
    protected override void OnFail(Faction completingFaction)
    {
      var rescuer = completingFaction.ScoreStatus == ScoreStatus.Defeated
        ? Player(PLAYER_NEUTRAL_AGGRESSIVE)
        : completingFaction.Player;

      rescuer.RescueGroup(_rescueUnits);
    }

    /// <inheritdoc/>
    protected override void OnComplete(Faction whichFaction)
    {
      if (whichFaction.Player != null)
        whichFaction.Player.RescueGroup(_rescueUnits);
      else
        Player(PLAYER_NEUTRAL_AGGRESSIVE).RescueGroup(_rescueUnits);
    }

    /// <inheritdoc/>
    public override string RewardFlavour =>
      "With food production now secured, we can settle Tempest Keep and start growing Ancients of the Arcane.";

    /// <inheritdoc/>
    protected override string RewardDescription =>
      $"Gain control of Tempest Keep, and learn to build {GetObjectName(Constants.UNIT_H0CA_ANCIENT_POOL_SUNFURY_SPECIALIST)}s and {GetObjectName(Constants.UNIT_H0CI_ARTIFICER_S_COURT_SUNFURY_SIEGE)}s";
  }
}