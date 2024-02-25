using System.Collections.Generic;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.FactionBased;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.ObjectiveSystem.Objectives.MetaBased;
using MacroTools.ObjectiveSystem.Objectives.TimeBased;
using MacroTools.QuestSystem;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Lordaeron
{
  /// <summary>
  /// Wait long enough, get Tyr Hand.
  /// </summary>
  public sealed class QuestTyrHand : QuestData
  {
    private readonly List<unit> _rescueUnits;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestTyrHand"/> class.
    /// </summary>
    /// <param name="stratholme"></param>
    /// <param name="rescueRect">Units in this area will start invulnerable and be rescued when the quest is complete.</param>
    public QuestTyrHand(Capital stratholme, Rectangle rescueRect) : base("The Fortified City",
      "The city of Tyr's Hand is considered impregnable, but they will be reluctant to join the war.",
      @"ReplaceableTextures\CommandButtons\BTNHumanBarracks.blp")
    {
      AddObjective(new ObjectiveEitherOf(new ObjectiveCapitalDead(stratholme),
        new ObjectiveTime(840)));
      AddObjective(new ObjectiveSelfExists());
      _rescueUnits = rescueRect.PrepareUnitsForRescue(RescuePreparationMode.HideNonStructures);
      ResearchId = Constants.UPGRADE_R023_QUEST_COMPLETED_THE_FORTIFIED_CITY;
      
    }

    /// <inheritdoc />
    public override string RewardFlavour => "The city-fortress of Tyr's Hand has decided to join us! Renowed for their siege engineers, we can now build siege workshops.";

    /// <inheritdoc />
    protected override string RewardDescription => $"Gain control of all units in Tyr's Hand, learn to train Garithos from the {GetObjectName(Constants.UNIT_HALT_ALTAR_OF_KINGS_LORDAERON_ALTAR)}, and learn to build {GetObjectName(Constants.UNIT_H094_SIEGE_WORKSHOP_LORDAERON_SIEGE)}s";

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