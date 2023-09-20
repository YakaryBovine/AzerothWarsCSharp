using System.Collections.Generic;
using MacroTools.ControlPointSystem;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.ObjectiveSystem.Objectives.ControlPointBased;
using MacroTools.ObjectiveSystem.Objectives.FactionBased;
using MacroTools.ObjectiveSystem.Objectives.TimeBased;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using MacroTools.QuestSystem;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Dalaran
{
  /// <summary>
  /// Kill a wolf to unlock a base.
  /// </summary>
  public sealed class QuestShadowfang : QuestData
  {
    private readonly List<unit> _rescueUnits;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestShadowfang"/> class.
    /// </summary>
    /// <param name="rescueRect">Units in this region will start invulnerable and be rescued when the quest completes.</param>
    /// <param name="direwolfToKill">This unit must be killed to complete the quest.</param>
    public QuestShadowfang(Rectangle rescueRect, unit direwolfToKill) : base("Shadows of Silverpine Forest",
      "The woods of Silverspine are unsafe for travellers, they need to be investigated",
      @"ReplaceableTextures\CommandButtons\BTNworgen.blp")
    {
      AddObjective(new ObjectiveUnitIsDead(direwolfToKill));
      AddObjective(new ObjectiveControlPoint(ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N01D_SILVERPINE_FOREST_15GOLD_MIN)));
      AddObjective(new ObjectiveExpire(660, Title));
      AddObjective(new ObjectiveSelfExists());
      _rescueUnits = rescueRect.PrepareUnitsForRescue(RescuePreparationMode.HideNonStructures);
      Required = true;
    }

    /// <inheritdoc />
    protected override string RewardFlavour =>
      "Shadowfang has been liberated, and its military is now free to assist Dalaran.";

    /// <inheritdoc />
    protected override string RewardDescription => "Control of all units in Shadowfang";

    /// <inheritdoc />
    protected override void OnFail(Faction completingFaction)
    {
      var rescuer = completingFaction.ScoreStatus == ScoreStatus.Defeated
        ? Player(PLAYER_NEUTRAL_AGGRESSIVE)
        : completingFaction.Player;

      rescuer.RescueGroup(_rescueUnits);
    }

    /// <inheritdoc />
    protected override void OnComplete(Faction completingFaction) =>
      completingFaction.Player.RescueGroup(_rescueUnits);
  }
}