using System.Collections.Generic;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.ObjectiveSystem.Objectives.FactionBased;
using MacroTools.ObjectiveSystem.Objectives.QuestBased;
using MacroTools.ObjectiveSystem.Objectives.TimeBased;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using MacroTools.QuestSystem;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Fel_Horde
{
  /// <summary>
  /// A <see cref="QuestData"/> that unlocks Blackrock when completed.
  /// </summary>
  public sealed class QuestBlackrock : QuestData
  {
    private readonly List<unit> _rescueUnits1;
    private readonly List<unit> _rescueUnits2;
    private Rectangle _rescueRect;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestBlackrock"/> class.
    /// </summary>
    public QuestBlackrock(Rectangle rescueRect1, Rectangle rescueRect2, IEnumerable<QuestData> prequisites) : base("Blackrock Unification",
      "Make contact with the Blackrock clan and convince them to join Magtheridon",
      @"ReplaceableTextures\CommandButtons\BTNBlackhand.blp")
    {
      foreach (var prequisite in prequisites)
        AddObjective(new ObjectiveQuestComplete(prequisite));
      AddObjective(new ObjectiveResearch(Constants.UPGRADE_R090_ACTIVATE_THE_BLACKROCK_CLAN_FEL, Constants.UNIT_O008_HELLFIRE_CITADEL_FEL_HORDE));
      AddObjective(new ObjectiveTime(540));
      AddObjective(new ObjectiveExpire(660, Title));
      AddObjective(new ObjectiveSelfExists());
      ResearchId = Constants.UPGRADE_R03C_QUEST_COMPLETED_BLACKROCK_UNIFICATION;
      _rescueUnits1 = rescueRect1.PrepareUnitsForRescue(RescuePreparationMode.HideNonStructures);
      _rescueUnits2 = rescueRect2.PrepareUnitsForRescue(RescuePreparationMode.HideAll);
      _rescueRect = rescueRect1;
      
    }

    //Todo: bad flavour
    /// <inheritdoc />
    public override string RewardFlavour =>
      "Blackrock Citadel has been subjugated, and its military is now free to assist the Fel Horde.";

    /// <inheritdoc />
    protected override string RewardDescription =>
      "Control of all units in Blackrock Citadel, a small outpost near the Dark Portal and enable Dal'rend Blackhand to be trained at the altar";

    /// <inheritdoc />
    protected override void OnFail(Faction completingFaction)
    {
      var rescuer = completingFaction.ScoreStatus == ScoreStatus.Defeated
        ? Player(PLAYER_NEUTRAL_AGGRESSIVE)
        : completingFaction.Player;

      rescuer.RescueGroup(_rescueUnits1);
      rescuer.RescueGroup(_rescueUnits2);
    }

    /// <inheritdoc />
    protected override void OnComplete(Faction completingFaction)
    {
      completingFaction.Player.RescueGroup(_rescueUnits1);
      completingFaction.Player.RescueGroup(_rescueUnits2);
      foreach (var unit in _rescueUnits1)
        if (!unit.IsType(UNIT_TYPE_PEON))
          unit.IssueOrder("attack", new Point(14131.0f, -13207.0f));
    }
    

    /// <inheritdoc />
    protected override void OnAdd(Faction whichFaction) => 
      whichFaction.ModObjectLimit(Constants.UPGRADE_R090_ACTIVATE_THE_BLACKROCK_CLAN_FEL, Faction.UNLIMITED);
  }
}