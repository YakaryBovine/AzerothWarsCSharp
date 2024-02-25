using System.Collections.Generic;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.QuestBased;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using MacroTools.QuestSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Draenei
{
  /// <summary>
  /// Bring the generator inside the Exodar to full health to make it more defensible
  /// </summary>
  public sealed class QuestRepairGenerator : QuestData
  {
    private readonly Capital _exodarGenerator;
    private readonly IEnumerable<unit> _protectors;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestRepairGenerator"/> class.
    /// </summary>
    public QuestRepairGenerator(Capital exodarGenerator, QuestData prerequisite, IEnumerable<unit> protectors) : base("Core of the Ship",
      "The broken core of the Exodar should be rebuilt, bringing us one step closer to making it usable again.",
      @"ReplaceableTextures\CommandButtons\BTNPowerGenerator.blp")
    {
      _exodarGenerator = exodarGenerator;
      _protectors = protectors;
      
      AddObjective(new ObjectiveQuestComplete(prerequisite));
      AddObjective(new ObjectiveUnitReachesFullHealth(exodarGenerator.Unit));
    }

    /// <inheritdoc/>
    public override string RewardFlavour =>
      "The Exodar's core has been rebuilt - the Crystal Protectors around it now shield it from any harm.";

    /// <inheritdoc/>
    protected override string RewardDescription =>
      "The Exodar Generator becomes invulnerable until all Crystal Protectors around it have been destroyed";

    /// <inheritdoc/>
    protected override void OnComplete(Faction whichFaction)
    {
      foreach (var unit in _protectors) 
        _exodarGenerator.AddProtector(unit);
    }
  }
}
