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


namespace WarcraftLegacies.Source.Quests.Scourge
{
  public sealed class QuestSpiderWar : QuestData
  {
    private const int QuestResearchId = Constants.UPGRADE_R03A_QUEST_COMPLETED_WAR_OF_THE_SPIDER;
    private readonly List<unit> _rescueUnits;

    public QuestSpiderWar(Rectangle rescueRect, unit spiderQueen) : base("War of the Spider",
      "The proud Nerubians have declared war on the newly formed Lich King, destroy them to secure the continent of Northrend.",
      @"ReplaceableTextures\CommandButtons\BTNNerubianQueen.blp")
    {
      AddObjective(new ObjectiveControlPoint(ControlPointManager.Instance.GetFromUnitType(FourCC("n08D"))));
      AddObjective(new ObjectiveControlPoint(ControlPointManager.Instance.GetFromUnitType(FourCC("n00G"))));
      AddObjective(new ObjectiveControlPoint(ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N09H_EN_KILAH)));
      AddObjective(new ObjectiveUnitIsDead(spiderQueen));
      AddObjective(new ObjectiveUpgrade(FourCC("unp2"), FourCC("unp1")));
      AddObjective(new ObjectiveExpire(660, Title));
      AddObjective(new ObjectiveSelfExists());

      _rescueUnits = rescueRect.PrepareUnitsForRescue(RescuePreparationMode.HideNonStructures);

      
    }

    /// <inheritdoc/>
    public override string RewardFlavour =>
      "Northrend and the Icecrown Citadel is now under full control of the Lich King and the Scourge.";

    /// <inheritdoc/>
    protected override string RewardDescription =>
      "Gain control of a base in Icecrown";

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
        .PlayMusicThematic("war3mapImported\\ScourgeTheme.mp3");
    }

    /// <inheritdoc/>
    protected override void OnAdd(Faction whichFaction)
    {
      whichFaction.ModObjectLimit(QuestResearchId, 1);
    }
  }
}