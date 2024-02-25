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
  /// Rebuild Brill to buff Renault.
  /// </summary>
  public sealed class QuestRebuildBrill : QuestData
  {
    private readonly LegendaryHero _renault;
    private const int ExperienceReward = 6000;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestRebuildBrill"/> class.
    /// </summary>
    public QuestRebuildBrill(Rectangle questRect, LegendaryHero renault) : base(
      "Brill",
      "The desolated village of Brill was once the hometown of Renault Mograine. Though insignificant in the grand scheme of things, the Crusade cares for its members.",
      @"ReplaceableTextures\CommandButtons\BTNStromgardeFarm.blp")
    {
      
      AddObjective(new ObjectiveBuildUniqueBuildingsInRect(questRect, "in Brill", 4));
      AddObjective(new ObjectiveControlLevel(ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N03H_BRILL), 2));
      _renault = renault;
    }

    /// <inheritdoc/>
    protected override void OnComplete(Faction whichFaction)
    {
      _renault.Unit?.AddExperience(ExperienceReward);
    }

    /// <inheritdoc/>
    public override string RewardFlavour =>
      "Nobody had noticed, but until now Renault has been somewhat reserved in his actions. With his hometown now reclaimed, he shines with a new vigour.";

    /// <inheritdoc/>
    protected override string RewardDescription =>
      $"Renault gains {ExperienceReward} experience";
  }
}