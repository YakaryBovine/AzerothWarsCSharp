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

namespace WarcraftLegacies.Source.Quests.Druids
{
  /// <summary>
  /// Get Cenarius and some treants.
  /// </summary>
  public sealed class QuestAshenvale : QuestData
  {
    private readonly List<unit> _rescueUnits;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestAshenvale"/> class.
    /// </summary>
    /// <param name="ashenvaleRect">Units in this rectangle start invulnerable and are rescued when the quest is completed.</param>
    public QuestAshenvale(Rectangle ashenvaleRect) : base("The Spirits of Ashenvale",
      "The forest needs healing. Regain control of it to awaken it.",
      @"ReplaceableTextures\CommandButtons\BTNKeeperC.blp")
    {
      AddObjective(new ObjectiveControlPoint(ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N07C_FELWOOD_10GOLD_MIN)));
      AddObjective(new ObjectiveControlPoint(ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N01Q_NORTHERN_ASHENVALE_10GOLD_MIN)));
      AddObjective(new ObjectiveExpire(600, Title));
      AddObjective(new ObjectiveSelfExists());
      ResearchId = Constants.UPGRADE_R06R_QUEST_COMPLETED_THE_SPIRITS_OF_ASHENVALE;
      _rescueUnits = ashenvaleRect.PrepareUnitsForRescue(RescuePreparationMode.HideNonStructures);
      Required = true;
    }

    /// <inheritdoc />
    protected override string RewardFlavour => "Ashenvale has awakened!";

    /// <inheritdoc />
    protected override string RewardDescription =>
      "Control of all units in Ashenvale";

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
      if (GetLocalPlayer() == completingFaction.Player) 
        PlayThematicMusic("war3mapImported\\DruidTheme.mp3");
    }
  }
}