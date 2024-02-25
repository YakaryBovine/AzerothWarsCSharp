using System.Collections.Generic;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.FactionBased;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.ObjectiveSystem.Objectives.TimeBased;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using MacroTools.QuestSystem;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Warsong
{
  public sealed class QuestLumberCamp : QuestData
  {
    private readonly List<unit> _rescueUnits;

    public QuestLumberCamp(Rectangle rescueRect, LegendaryHero grom) : base("Landfall",
      "The Barrens are an hostile environement devoid of ressources to start a new civilisation. The Warsong should deeper into the woods to prepare a settlement for the new horde",
      @"ReplaceableTextures\CommandButtons\BTNBundleOfLumber.blp")
    {
      AddObjective(new ObjectiveLegendReachRect(grom, Regions.LumberCampUnlock,
        "Eastern Ashenvale"));
      AddObjective(new ObjectiveUpgrade(Constants.UNIT_O02S_FORTRESS_WARSONG_T3, Constants.UNIT_O00C_GREAT_HALL_WARSONG_T1));
      AddObjective(new ObjectiveExpire(480, Title));
      AddObjective(new ObjectiveSelfExists());
      
      _rescueUnits = rescueRect.PrepareUnitsForRescue(RescuePreparationMode.HideAll);

      
    }

    /// <inheritdoc />
    public override string RewardFlavour =>
      "The Barrens have been cleared and the Warsong has established a base of operation in the Ashenvale Forest";

    /// <inheritdoc />
    protected override string RewardDescription =>
      "Control of all units in the Warsong Lumber Camp and 1500 lumber";

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
      foreach (var unit in _rescueUnits) 
        unit.Rescue(completingFaction.Player);
      
      completingFaction.Player?.AdjustPlayerState(PLAYER_STATE_RESOURCE_LUMBER, 1500);
    }
  }
}