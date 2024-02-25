using System.Collections.Generic;
using MacroTools.ControlPointSystem;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.ObjectiveSystem.Objectives.ControlPointBased;
using MacroTools.ObjectiveSystem.Objectives.FactionBased;
using MacroTools.ObjectiveSystem.Objectives.TimeBased;
using MacroTools.QuestSystem;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Lordaeron
{
  /// <summary>
  /// Capture Strahnbrad's control point to gain control of the village.
  /// </summary>
  public sealed class QuestStrahnbrad : QuestData
  {
    private readonly List<unit> _rescueUnits;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestStrahnbrad"/> class.
    /// </summary>
    /// <param name="rescueRect"></param>
    public QuestStrahnbrad(Rectangle rescueRect) : base("The Defense of Strahnbrad",
      "The Strahnbrad is under attack by some brigands, clear them out",
      @"ReplaceableTextures\CommandButtons\BTNFarm.blp")
    {
      AddObjective(
        new ObjectiveControlPoint(
          ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N01C_STRAHNBRAD)));
      AddObjective(new ObjectiveExpire(660, Title));
      AddObjective(new ObjectiveSelfExists());
      _rescueUnits = rescueRect.PrepareUnitsForRescue(RescuePreparationMode.HideNonStructures);
      
    }

    /// <inheritdoc/>
    public override string RewardFlavour => "Strahnbrad has been liberated.";

    /// <inheritdoc/>
    protected override string RewardDescription => "Control of all buildings in Strahnbrad";

    /// <inheritdoc/>
    protected override void OnFail(Faction completingFaction)
    {
      var rescuer = completingFaction.ScoreStatus == ScoreStatus.Defeated
        ? Player(PLAYER_NEUTRAL_AGGRESSIVE)
        : completingFaction.Player;

      rescuer.RescueGroup(_rescueUnits);
    }

    /// <inheritdoc/>
    protected override void OnComplete(Faction completingFaction)
    {
      if (completingFaction.Player != null)
        completingFaction.Player.RescueGroup(_rescueUnits);
    }
  }
}