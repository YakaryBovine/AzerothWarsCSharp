using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.QuestSystem;

namespace WarcraftLegacies.Source.Quests.Warsong
{
  /// <summary>
  /// Warsong kills the Druids and Grom is finally happy.
  /// </summary>
  public sealed class QuestWarsongKillDruids : QuestData
  {
    private readonly LegendaryHero _grom;
    private const int ExperienceReward = 8000;

    /// <inheritdoc/>
    public override string RewardFlavour => "Nordrassil has been captured. The Warsong is supreme!";

    /// <inheritdoc/>
    protected override string RewardDescription =>
      $"Grom Hellscream gains {ExperienceReward} experience and you can now train Foreman Glibbs";

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestWarsongKillDruids"/> class.
    /// </summary>
    public QuestWarsongKillDruids(Capital nordrassil, LegendaryHero grom) : base("Tear It Down",
      "The World Tree, Nordrassil, is the Night Elves' source of immortality. Capture it to cripple them permanently.",
      @"ReplaceableTextures\CommandButtons\BTNFountainOfLife.blp")
    {
      _grom = grom;
      AddObjective(new ObjectiveControlCapital(nordrassil, false));
      AddObjective(new ObjectiveLegendNotPermanentlyDead(grom));
      ResearchId = Constants.UPGRADE_R08M_QUEST_COMPLETED_TEAR_IT_DOWN;
      
    }

    /// <inheritdoc/>
    protected override void OnComplete(Faction completingFaction) => 
      _grom.Unit?.AddExperience(ExperienceReward);
  }
}