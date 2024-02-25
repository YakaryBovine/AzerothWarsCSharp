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

namespace WarcraftLegacies.Source.Quests.Druids
{
  public sealed class QuestShrineBase : QuestData
  {
    private readonly List<unit> _rescueUnits = new();

    public QuestShrineBase(Rectangle rescueRect) : base("Hyjal's Rest",
      "Mount Hyjal has been invaded by the corruption already affecting Felwood. Clear them out to awaken the Ancients",
      @"ReplaceableTextures\CommandButtons\BTNAncientOfTheMoon.blp")
    {
      AddObjective(new ObjectiveControlPoint(ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N0BI_SHRINE_TO_MALORNE)));
      AddObjective(new ObjectiveHostilesInAreaAreDead(new List<Rectangle> { Regions.ShrineBaseUnlock }, "in Hyjal"));
      AddObjective(new ObjectiveExpire(480, Title));
      AddObjective(new ObjectiveSelfExists());
      
      _rescueUnits = rescueRect.PrepareUnitsForRescue(RescuePreparationMode.HideNonStructures);

      
    }

    /// <inheritdoc />
    public override string RewardFlavour =>
      "The World Tree has been rid of corruption and the Ancients can awaken";

    /// <inheritdoc />
    protected override string RewardDescription =>
      "Control of all units in the Shrine of Malorne base";

    /// <inheritdoc />
    protected override void OnFail(Faction completingFaction)
    {
      var rescuer = completingFaction.ScoreStatus == ScoreStatus.Defeated
        ? Player(PLAYER_NEUTRAL_AGGRESSIVE)
        : completingFaction.Player;

      rescuer.RescueGroup(_rescueUnits);
    }

    /// <inheritdoc />
    protected override void OnComplete(Faction completingFaction)
    {
      completingFaction.Player.RescueGroup(_rescueUnits);
    }
  }
}