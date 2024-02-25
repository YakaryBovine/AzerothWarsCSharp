using MacroTools.ObjectiveSystem.Objectives.TimeBased;
using MacroTools.QuestSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Gilneas
{
  /// <summary>
  /// Darius gets released from Prison " 
  /// </summary>
  public sealed class QuestCrowley : QuestData
  {
    private const int AltarId = Constants.UNIT_H02X_ALTAR_OF_KINGS_GILNEAS_ALTAR;

    /// <summary>
    /// IniInitializes a new instance of the <see cref="QuestCrowley"/> class.
    /// </summary>
    public QuestCrowley() : base("The Rebel",
      "Darius Crowley has been imprisoned since the Northgate rebellion. If Gilneas were to fall into grave peril, an early release might be necessary.",
      @"ReplaceableTextures/CommandButtons/BTNWorgenHunt.blp")
    {
      AddObjective(new ObjectiveTime(900));
      ResearchId = Constants.UPGRADE_MD05_QUEST_COMPLETED_THE_REBEL;
      
    }

    /// <inheritdoc/>
    public override string RewardFlavour =>
      "Facing the looming threat of the Scourge and its horrific Plague, Genn Greymane has decided to pardon Darius Crowley for the Northgate rebellion so that he can assist in Gilneas' defense.";

    /// <inheritdoc/>
    protected override string RewardDescription =>
      $"Learn to train Darius Crowley from the {GetObjectName(AltarId)}";
  }
}
