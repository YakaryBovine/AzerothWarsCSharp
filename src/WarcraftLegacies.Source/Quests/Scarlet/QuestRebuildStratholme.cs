using MacroTools.ControlPointSystem;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.ObjectiveSystem.Objectives.ControlPointBased;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using MacroTools.QuestSystem;
using MacroTools.LegendSystem;
using WCSharp.Shared.Data;

namespace WarcraftLegacies.Source.Quests.Scarlet
{
  /// <summary>
  /// Rebuild Stratholme and get a bunch of exp on Saiden.
  /// </summary>
  public sealed class QuestRebuildStratholme : QuestData
  {
    private readonly LegendaryHero _saiden;
    private const int ExperienceReward = 6000;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="QuestRebuildStratholme"/> class.
    /// </summary>
    public QuestRebuildStratholme(Rectangle questRect, LegendaryHero saiden) : base(
      "Stratholme",
      "Before the Plague wiped out Stratholme, Saiden had established himself there as Lord Commander of the Silver Hand. This once-glorious city must be reclaimed.",
      @"ReplaceableTextures\CommandButtons\BTNStromgardeCastle.blp")
    {
      
      AddObjective(new ObjectiveBuildUniqueBuildingsInRect(questRect, "in Stratholme", 5));
      AddObjective(new ObjectiveControlLevel(
        ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N01M_STRATHOLME), 4));
      _saiden = saiden;
    }

    /// <inheritdoc/>
    protected override void OnComplete(Faction whichFaction)
    {
      _saiden.Unit?.AddExperience(ExperienceReward);
    }

    /// <inheritdoc/>
    public override string RewardFlavour =>
      "The city of Stratholme once more stands as a bastion of human civilization. Though still a mere shadow of its former glory, it will reclaim its majesty in time.";

    /// <inheritdoc/>
    protected override string RewardDescription =>
      $"Saiden Dathrohan gains {ExperienceReward} experience";
  }
}