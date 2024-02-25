using MacroTools.FactionSystem;
using MacroTools.QuestSystem;
using System.Collections.Generic;
using static War3Api.Common;
using MacroTools.Extensions;
using MacroTools.ControlPointSystem;
using MacroTools.ObjectiveSystem.Objectives.ControlPointBased;
using MacroTools.ObjectiveSystem.Objectives.FactionBased;
using MacroTools.ObjectiveSystem.Objectives.TimeBased;

namespace WarcraftLegacies.Source.Quests.Gilneas
{
  /// <summary>
  /// Capture control points close to Stormglen to gain control of it.
  /// </summary>
  public sealed class QuestStormglen : QuestData
  {
    private List<unit> _rescueUnits { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestKeelHarbor"/> class.
    /// </summary>
    public QuestStormglen() : base("Stormglen", "The next village is right next to the Blackwald, south west of Tempest Reach. We will need to purify the forest too.", @"ReplaceableTextures\CommandButtons\BTNGilneasWizardTower.blp")
    {
      AddObjective(new ObjectiveControlPoint(ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N06V_BLACKWALD)));
      AddObjective(new ObjectiveControlPoint(ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N084_TEMPEST_REACH)));
      AddObjective(new ObjectiveExpire(660, Title));
      AddObjective(new ObjectiveSelfExists());
      _rescueUnits = Regions.GilneasUnlock2.PrepareUnitsForRescue(RescuePreparationMode.HideNonStructures);
      
    }

    /// <inheritdoc/>
    public override string RewardFlavour => "Stormglen Village has been liberated.";

    /// <inheritdoc/>
    protected override string RewardDescription => "Control of all buildings in Stormglen Village";

    /// <inheritdoc/>
    protected override void OnComplete(Faction whichFaction)
    {
      whichFaction.Player?.RescueGroup(_rescueUnits);
    }

    /// <inheritdoc/>
    protected override void OnFail(Faction completingFaction)
    {
      var rescuer = completingFaction.ScoreStatus == ScoreStatus.Defeated
        ? Player(PLAYER_NEUTRAL_AGGRESSIVE)
        : completingFaction.Player;

      rescuer.RescueGroup(_rescueUnits);
    }
  }
}
