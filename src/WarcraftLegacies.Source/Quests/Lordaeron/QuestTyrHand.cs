using System.Collections.Generic;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.FactionBased;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.ObjectiveSystem.Objectives.MetaBased;
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
    /// <param name="capitalCity"></param>
    /// <param name="stratholme"></param>
    /// <param name="rescueRect">Units in this area will start invulnerable and be rescued when the quest is complete.</param>
    /// <param name="lichKing"></param>
    public QuestTyrHand(Capital capitalCity, Capital stratholme, Rectangle rescueRect, Capital lichKing) : base("The Fortified City",
      "The city of Tyr's Hand is considered impregnable, but they will be reluctant to join the war",
      @"ReplaceableTextures\CommandButtons\BTNHumanBarracks.blp")
    {
      AddObjective(new ObjectiveEitherOf(
        new ObjectiveEitherOf(new ObjectiveCapitalDead(capitalCity),new ObjectiveCapitalDead(stratholme)),
        new ObjectiveCapitalDead(lichKing)));
      AddObjective(new ObjectiveSelfExists());
      _rescueUnits = rescueRect.PrepareUnitsForRescue(RescuePreparationMode.HideNonStructures);
      Required = true;
    }

    /// <inheritdoc />
    protected override string RewardFlavour => "The city-fortress of Tyr's Hand has decided to join us!";

    /// <inheritdoc />
    protected override string RewardDescription => "Control of all units in Tyr's Hand and Garithos is trainable";

    /// <inheritdoc />
    protected override void OnFail(Faction completingFaction) => 
      Player(PLAYER_NEUTRAL_AGGRESSIVE).RescueGroup(_rescueUnits);

    /// <inheritdoc />
    protected override void OnComplete(Faction completingFaction) => 
      completingFaction.Player.RescueGroup(_rescueUnits);
  }
}