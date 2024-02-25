using MacroTools.ObjectiveSystem.Objectives.QuestBased;
using MacroTools.QuestSystem;

namespace WarcraftLegacies.Source.Quests.Scarlet
{
  /// <summary>
  /// Rebuild all the towns in Lordaeron to unlock the Divine Bastion and it's units.
  /// </summary>
  public sealed class QuestReconquerLordaeron : QuestData
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="QuestReconquerLordaeron"/> class.
    /// </summary>
    public QuestReconquerLordaeron(QuestData stratholme, QuestData capital, QuestData hearthglen, QuestData brill, QuestData andorhal) : base(
      "A Once Great People",
      "The onslaught of the Scourge devastated Lordaeron beyond recognition, slaughtering its people and levelling its cities. Only the Scarlet Crusade stands strong, a faint light of hope in the darkness. If the cities and village of Lordaeron could be reclaimed and rebuilt, humanity could begin again.",
      "ReplaceableTextures/CommandButtons/BTNSalvationSpire.blp")
    {
      
      AddObjective(new ObjectiveQuestComplete(stratholme));
      AddObjective(new ObjectiveQuestComplete(capital));
      AddObjective(new ObjectiveQuestComplete(hearthglen));
      AddObjective(new ObjectiveQuestComplete(brill));
      AddObjective(new ObjectiveQuestComplete(andorhal));
      ResearchId = Constants.UPGRADE_R02A_QUEST_COMPLETED_A_ONCE_GREAT_PEOPLE;
      Global = true;
    }

    /// <inheritdoc/>
    public override string RewardFlavour =>
      "Against all odds, the Scarlet Crusade has reclaimed and rebuilt the lands of Lordaeron, filling its cities and fields with the beginnings of a new generation. Having proven itself capable of far more than simple vengeance, the Crusade receives the Light's ultimate blessing.";

    /// <inheritdoc/>
    protected override string RewardDescription => "Learn to build the Divine Bastion";
  }
}