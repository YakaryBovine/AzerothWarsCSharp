using System.Collections.Generic;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.FactionBased;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.QuestSystem;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Quelthalas
{
  /// <summary>
  /// Train sylvanas to take control of Windrunner Spire.
  /// </summary>
  public sealed class QuestUnlockSpire : QuestData
  {
    private readonly List<unit> _rescueUnits;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestUnlockSpire"/> class.
    /// </summary>
    /// <param name="rescueRect">Units in this area will start invulnerable and be rescued when the quest is complete.</param>
    /// <param name="sylvanas"></param>
    public QuestUnlockSpire(Rectangle rescueRect, LegendaryHero sylvanas) : base("Windrunner Spire",
      "The Windrunner tower is a strong asset to Quel'thalas.",
      @"ReplaceableTextures\CommandButtons\BTNElvenScoutTower.blp")
    {
      AddObjective(new ObjectiveControlLegend(sylvanas, true));
      AddObjective(new ObjectiveSelfExists());
      _rescueUnits = rescueRect.PrepareUnitsForRescue(RescuePreparationMode.HideNonStructures);
      
    }

    /// <inheritdoc />
    public override string RewardFlavour => "Sylvanas has been trained, the Spire has joined the Kingdom.";

    /// <inheritdoc />
    protected override string RewardDescription => "Control of the Windrunner Spire";

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