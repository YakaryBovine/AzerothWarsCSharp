using System.Collections.Generic;
using MacroTools.ControlPointSystem;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.ControlPointBased;
using MacroTools.ObjectiveSystem.Objectives.FactionBased;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.ObjectiveSystem.Objectives.TimeBased;
using MacroTools.QuestSystem;
using WarcraftLegacies.Source.Setup.Legends;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Scourge
{
  /// <summary>
  /// Capture <see cref="LegendNeutral.DraktharonKeep"/> and its control point to gain control of all buildings in the area.
  /// </summary>
  public sealed class QuestDrakUnlock : QuestData
  {
    private readonly List<unit> _rescueUnits;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestDrakUnlock"/> class.
    /// </summary>
    public QuestDrakUnlock(Rectangle rescueRect, Capital draktharonKeep) : base(
      "Drak'tharon Keep", "Drak'tharon Keep will be the perfect place for an outpost by the sea.",
      @"ReplaceableTextures\CommandButtons\BTNUndeadShipyard.blp")
    {
      AddObjective(new ObjectiveControlPoint(ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N030_DRAK_THARON_KEEP_30GOLD_MIN)));
      AddObjective(new ObjectiveControlCapital(draktharonKeep, false));
      AddObjective(new ObjectiveExpire(1140, Title));
      AddObjective(new ObjectiveSelfExists());
      ResearchId = Constants.UPGRADE_R08J_QUEST_COMPLETED_DRAK_THARON_KEEP;
      _rescueUnits = rescueRect.PrepareUnitsForRescue(RescuePreparationMode.HideNonStructures);
    }

    /// <inheritdoc/>
    protected override string RewardFlavour => "Drak'tharon Keep is now under the control of the Scourge.";

    /// <inheritdoc/>
    protected override string RewardDescription => "Control of all buildings in Drak'tharon Keep";

    /// <inheritdoc/>
    protected override void OnFail(Faction completingFaction)
    {
      Player(PLAYER_NEUTRAL_AGGRESSIVE).RescueGroup(_rescueUnits);
    }

    /// <inheritdoc/>
    protected override void OnComplete(Faction completingFaction)
    {
      if (completingFaction.Player != null)
        completingFaction.Player.RescueGroup(_rescueUnits);
    }
  }
}