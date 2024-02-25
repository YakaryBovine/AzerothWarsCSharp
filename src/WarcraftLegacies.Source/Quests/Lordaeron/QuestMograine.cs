using MacroTools.ObjectiveSystem.Objectives.TimeBased;
using MacroTools.QuestSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Lordaeron
{
  /// <summary>
  /// Kill the Lich King to gain Alexandros Mograine as a Hero.
  /// </summary>
  public sealed class QuestMograine : QuestData
  {
    private const int AltarId = Constants.UNIT_HALT_ALTAR_OF_KINGS_LORDAERON_ALTAR;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestMograine"/> class.
    /// </summary>
    public QuestMograine() : base("The Exile",
      "Mograine has been gone for a long time, if Lordaeron would be in great peril, he would surely come back to defend it!",
      @"ReplaceableTextures\CommandButtons\BTNAlexandros.blp")
    {
      AddObjective(new ObjectiveTime(900));
      ResearchId = Constants.UPGRADE_R06P_QUEST_COMPLETED_THE_EXILE_LORDAERON;
    }

    /// <inheritdoc/>
    public override string RewardFlavour =>
      "With the threat of the Scourge and the Plague, Mograine has returned to help Lordaeorn in their dire times.";

    /// <inheritdoc/>
    protected override string RewardDescription =>
      $"Learn to train Alexandros Mograine from the {GetObjectName(AltarId)}";
  }
}