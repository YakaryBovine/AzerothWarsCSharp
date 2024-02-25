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
  /// Capture control points close to Duskhaven to gain control of it.
  /// </summary>
  public sealed class QuestDuskhaven : QuestData
  {
    private List<unit> _rescueUnits { get;}

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestDuskhaven"/> class.
    /// </summary>
    public QuestDuskhaven() : base("Duskhaven", "The next town is located at the western coast of Gilneas.", @"ReplaceableTextures\CommandButtons\BTNGilneasTownHall.blp")
    {
      AddObjective(new ObjectiveControlPoint(ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N031_DUSKHAVEN)));
      AddObjective(new ObjectiveControlPoint(ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N06V_BLACKWALD)));
      AddObjective(new ObjectiveExpire(660, "Duskhaven"));
      AddObjective(new ObjectiveSelfExists());
      _rescueUnits = Regions.GilneasUnlock4.PrepareUnitsForRescue(RescuePreparationMode.HideNonStructures);
      
    }

    /// <inheritdoc/>
    public override string RewardFlavour => "Duskhaven has been liberated.";

    /// <inheritdoc/>
    protected override string RewardDescription => "Control of all buildings in Duskhaven.";

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
