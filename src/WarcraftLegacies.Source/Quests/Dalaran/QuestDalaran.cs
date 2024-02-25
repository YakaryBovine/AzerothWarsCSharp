using System.Collections.Generic;
using MacroTools.ControlPointSystem;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.ObjectiveSystem.Objectives.ControlPointBased;
using MacroTools.ObjectiveSystem.Objectives.FactionBased;
using MacroTools.ObjectiveSystem.Objectives.QuestBased;
using MacroTools.ObjectiveSystem.Objectives.TimeBased;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using MacroTools.QuestSystem;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Dalaran
{
  /// <summary>
  /// Dalaran acquires the city of Dalaran.
  /// </summary>
  public sealed class QuestDalaran : QuestData
  {
    private readonly List<unit> _rescueUnits = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestDalaran"/> class.
    /// </summary>
    /// <param name="rescueRects">Units inside these rectangles start invulnerable and are rescued when the quest ends.</param>
    /// <param name="prerequisites">These quests must be completed before this one can be completed.</param>
    public QuestDalaran(IEnumerable<Rectangle> rescueRects, IEnumerable<QuestData> prerequisites) : base("Outskirts",
      "The territories of Dalaran are fragmented, secure the lands and protect Dalaran citizens.",
      @"ReplaceableTextures\CommandButtons\BTNArcaneCastle.blp")
    {
      foreach (var prerequisite in prerequisites) 
        AddObjective(new ObjectiveQuestComplete(prerequisite));
      AddObjective(new ObjectiveControlPoint(ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N018_DURNHOLDE)));
      AddObjective(new ObjectiveControlPoint(ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N0EB_JINTHA_ALOR)));
      AddObjective(new ObjectiveUpgrade(Constants.UNIT_H068_OBSERVATORY_DALARAN_T3, Constants.UNIT_H065_REFUGE_DALARAN_T1));
      AddObjective(new ObjectiveExpire(660, Title));
      AddObjective(new ObjectiveSelfExists());
      ResearchId = Constants.UPGRADE_R038_QUEST_COMPLETED_OUTSKIRTS;

      foreach (var rectangle in rescueRects)
        _rescueUnits.AddRange(rectangle.PrepareUnitsForRescue(RescuePreparationMode.HideNonStructures,
          unit => unit.GetTypeId() != Constants.UNIT_N0DK_SKULL_OF_GUL_DAN_PEDESTAL && unit.GetTypeId() != Constants.UNIT_NBSM_BOOK_OF_MEDIVH));
      
    }

    /// <inheritdoc/>
    public override string RewardFlavour =>
      "Dalaran outskirs are now secure, the mages will join Dalaran.";

    /// <inheritdoc/>
    protected override string RewardDescription =>
      "Control of all units in Dalaran, enables Antonidas to be trained at the Altar and you can now build Refuges";

    /// <inheritdoc/>
    protected override void OnFail(Faction completingFaction)
    {
      var rescuer = completingFaction.ScoreStatus == ScoreStatus.Defeated
        ? Player(PLAYER_NEUTRAL_AGGRESSIVE)
        : completingFaction.Player;

      rescuer.RescueGroup(_rescueUnits);
    }

    /// <inheritdoc/>
    protected override void OnComplete(Faction completingFaction)
    {
      completingFaction.Player
        .RescueGroup(_rescueUnits)
        .PlayMusicThematic("war3mapImported\\DalaranTheme.mp3");
    }
  }
}