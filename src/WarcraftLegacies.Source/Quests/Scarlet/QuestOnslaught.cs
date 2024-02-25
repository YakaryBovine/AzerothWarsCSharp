using MacroTools.ControlPointSystem;
using MacroTools.ObjectiveSystem.Objectives.ControlPointBased;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using MacroTools.QuestSystem;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Scarlet
{
  public sealed class QuestOnslaught : QuestData
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="QuestOnslaught"/> class.
    /// </summary>
    public QuestOnslaught(Rectangle questRect) : base(
      "Onslaught",
      "Death awaits the living at the roof of the world. It is there that the Crusade must undertake its ultimate vengeance.",
      @"ReplaceableTextures\CommandButtons\BTNVampire.blp")
    {
      AddObjective(new ObjectiveBuildInRect(questRect, "in Dragonblight", Constants.UNIT_H0AG_HALL_OF_SWORDS_CRUSADE_BARRACKS));
      AddObjective(new ObjectiveBuildInRect(questRect, "in Dragonblight", Constants.UNIT_H0BM_TOWN_HALL_CRUSADE_T1));
      AddObjective(new ObjectiveControlLevel(ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N02Q_DRAGONBLIGHT), 5));
      ResearchId = Constants.UPGRADE_R040_QUEST_COMPLETED_ONSLAUGHT;
    }

    /// <inheritdoc/>
    public override string RewardFlavour =>
      "The Crusade finally manages to establish a foothold in Northrend. Already the land's dark influence pierces the mind of even its most stalwart Archons.";

    /// <inheritdoc/>
    protected override string RewardDescription =>
      $"Your {GetObjectName(Constants.UNIT_N09N_BISHOP_OF_THE_LIGHT_SCARLET)} gain the Unholy Archon ability.";
  }
}