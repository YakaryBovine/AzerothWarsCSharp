using MacroTools.ControlPointSystem;
using MacroTools.ObjectiveSystem.Objectives.ControlPointBased;
using MacroTools.QuestSystem;

namespace WarcraftLegacies.Source.Quests.Naga
{

  /// <summary>
  /// Caputre various control points to unlock Sea Witches
  /// </summary>
  public sealed class QuestRegroupCastaway : QuestData
  {
    /// <inheritdoc/>
    public QuestRegroupCastaway() : base("Ancient Libraries",
      "Illidan will need to collect more lost knowledge to be form a new generation of Naga Sea Witch",
      @"ReplaceableTextures\CommandButtons\BTNNagaSeaWitch.blp")
    {
      AddObjective(new ObjectiveControlPoint(ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N00W_ZUL_GURUB)));
      AddObjective(new ObjectiveControlPoint(ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N00Y_DEADWIND_PASS)));
      AddObjective(new ObjectiveControlPoint(ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N00U_SWAMP_OF_SORROWS)));
      ResearchId = Constants.UPGRADE_R093_QUEST_COMPLETED_ANCIENT_LIBRARIES;
      
    }

    /// <inheritdoc/>
    public override string RewardFlavour => "The powerful Summoners are serving their Master once again.";

    /// <inheritdoc/>
    protected override string RewardDescription => $"Summoners can be trained from the Archives.";
  }
}