using System.Collections.Generic;
using MacroTools.ControlPointSystem;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.ObjectiveSystem.Objectives.ControlPointBased;
using MacroTools.ObjectiveSystem.Objectives.FactionBased;
using MacroTools.QuestSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Warsong
{
  /// <summary>
  /// The Warsong clan can acquire the Warsong Outpost in Uldum.
  /// </summary>
  public sealed class QuestWarsongOutpost : QuestData
  {
    private readonly List<unit> _rescueUnits;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestWarsongOutpost"/>.
    /// </summary>
    public QuestWarsongOutpost() : base("Uldum Excavation",
      "The deserts of Uldum are littered with ancient debris from a lost age, and it seems some of its secrets remain intact even now.  This matters little to the Warsong, however; this land is merely another target ripe for conquest.",
      @"ReplaceableTextures\CommandButtons\BTNIronHordeWatchTower.blp")
    {
      AddObjective(new ObjectiveControlPoint(ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N0BK_LOST_CITY_OF_THE_TOL_VIR)));
      AddObjective(new ObjectiveControlPoint(ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N0BD_ULDUM)));
      AddObjective(new ObjectiveSelfExists());
      
      ResearchId = Constants.UPGRADE_VQ03_QUEST_COMPLETED_ULDUM_EXCAVATION;
      _rescueUnits = Regions.Warsong_Uldum_Unlock.PrepareUnitsForRescue(RescuePreparationMode.HideNonStructures);
    }

    /// <inheritdoc />
    public override string RewardFlavour =>
      $"The Warsong and their allies have taken control of the deserts of Uldum and established an outpost in the region.";

    /// <inheritdoc />
    protected override string RewardDescription => "Gain control of a small outpost in western Uldum";

    /// <inheritdoc />
    protected override void OnFail(Faction completingFaction)
    {
      var rescuer = completingFaction.ScoreStatus == ScoreStatus.Defeated
        ? Player(PLAYER_NEUTRAL_AGGRESSIVE)
        : completingFaction.Player;

      rescuer.RescueGroup(_rescueUnits);
    }

    /// <inheritdoc />
    protected override void OnComplete(Faction completingFaction) => completingFaction.Player.RescueGroup(_rescueUnits);
  }
}