using MacroTools.ControlPointSystem;
using MacroTools.ObjectiveSystem.Objectives.ControlPointBased;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using MacroTools.QuestSystem;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Scarlet
{
  /// <summary>
  /// Rebuild Andhoral to buff your air units
  /// </summary>
  public sealed class QuestRebuildAndorhal : QuestData
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="QuestRebuildAndorhal"/> class.
    /// </summary>
    public QuestRebuildAndorhal(Rectangle questRect) : base(
      "Andorhal",
      "Once the breadbasket of Lordaeron, Andorhal is now nothing but ashes. Were it to be rebuilt, its proximity to Aerie Peak would allow the Scarlet Crusade to breed powerful Eagles and Gryphons.",
      @"ReplaceableTextures\CommandButtons\BTNAlteracGryphonAviary.blp")
    {
      
      AddObjective(new ObjectiveBuildUniqueBuildingsInRect(questRect, "in Andorhal", 5));
      AddObjective(new ObjectiveControlLevel(ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N01H_ANDORHAL), 2));
      ResearchId = Constants.UPGRADE_R03P_QUEST_COMPLETED_ANDORHAL; 
    }

    /// <inheritdoc/>
    public override string RewardFlavour =>
      "Survivors from Lordaeron's fall are once more pouring into Andorhal. Eagles and Gryphons from Aerie Peak soar down to the renewed agricultural center to enjoy its renewed production.";

    /// <inheritdoc/>
    protected override string RewardDescription =>
      $"Your {GetObjectName(Constants.UNIT_O06V_EAGLE_RIDER_SCARLET)}s and {GetObjectName(Constants.UNIT_E01L_GRYPHON_MARKSMAN_SCARLET)} gain 400 hit points";
  }
}