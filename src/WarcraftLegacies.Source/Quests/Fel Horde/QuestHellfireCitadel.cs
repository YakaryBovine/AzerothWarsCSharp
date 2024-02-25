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

namespace WarcraftLegacies.Source.Quests.Fel_Horde
{
  /// <summary>
  /// Drive the Draenai out of Outland, take control of various control points in outland and upgrade main to t3 in order to unlock Hellfire Citadel.
  /// </summary>
  public sealed class QuestHellfireCitadel : QuestData
  {
    private readonly List<unit> _rescueUnits;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestHellfireCitadel"/> class.
    /// </summary>
    /// <param name="rescueRect">Units in this area start invulnerable then get rescued when the quest is complete.</param>
    public QuestHellfireCitadel(Rectangle rescueRect) : base("The Citadel",
      "The clans holding Hellfire Citadel do not respect Kargath's authority yet, Magtheridon is being kept alive by Illidan, if we break him, he could serve us well.",
      @"ReplaceableTextures\CommandButtons\BTNFelOrcFortress.blp")
    {
      AddObjective(new ObjectiveControlPoint(ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N01J_ZANGARMARSH)));
      AddObjective(new ObjectiveControlPoint(ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N02N_BLADE_S_EDGE_MOUNTAINS)));
      AddObjective(new ObjectiveControlPoint(ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N00B_NAGRAND)));
      AddObjective(new ObjectiveControlPoint(ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N0CV_HALAAR)));
      AddObjective(new ObjectiveUpgrade(Constants.UNIT_O030_FORTRESS_FEL_HORDE_T3, Constants.UNIT_O02Y_GREAT_HALL_FEL_HORDE_T1));
      AddObjective(new ObjectiveExpire(600, Title));
      AddObjective(new ObjectiveSelfExists());
      ResearchId = Constants.UPGRADE_R00P_QUEST_COMPLETED_THE_CITADEL;
      _rescueUnits = rescueRect.PrepareUnitsForRescue(RescuePreparationMode.Invulnerable);
      
    }
    
    /// <inheritdoc/>
    public override string RewardFlavour =>
      "Hellfire Citadel has been subjugated, and its military is now free to assist the Fel Horde.";
    
    /// <inheritdoc/>
    protected override string RewardDescription =>
      "Control of all units in Hellfire Citadel and enable Magtheridon to be trained at the altar";
    
    /// <inheritdoc/>
    protected override void OnComplete(Faction completingFaction)
    {
      completingFaction.Player?
        .RescueGroup(_rescueUnits)
        .PlayMusicThematic("war3mapImported\\FelTheme.mp3");
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