using MacroTools.ControlPointSystem;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.ControlPointBased;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.QuestSystem;
using WarcraftLegacies.Source.Setup.Legends;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Lordaeron
{
  /// <summary>
  /// Gain control of <see cref="LegendNeutral.DraktharonKeep"/> and the Storm Peaks control point to unlock Thunder Eagles.
  /// </summary>
  public sealed class QuestThunderEagle : QuestData
  {
    private new const int ResearchId = Constants.UPGRADE_R04L_QUEST_COMPLETED_TO_THE_SKIES_LORDAERON;
    private const int ThunderEagleId = Constants.UNIT_NWE2_THUNDER_EAGLE_LORDAERON;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestThunderEagle"/> class.
    /// </summary>
    public QuestThunderEagle(Capital draktharonKeep) : base("To the Skies!",
      "The Thunder Eagles of the Storm Peaks live in fear of the Legion. Wipe out the Legion Nexus to bring these great birds out into the open.",
      @"ReplaceableTextures\CommandButtons\BTNWarEagle.blp")
    {
      AddObjective(new ObjectiveControlCapital(draktharonKeep, false));
      AddObjective(new ObjectiveControlPoint(ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N02S_STORM_PEAKS)));
    }

    //Todo: bad flavour
    /// <inheritdoc/>
    public override string RewardFlavour => "The Thunder Eagles are now in the safe hands of Lordaeron.";

    /// <inheritdoc/>
    protected override string RewardDescription => "Learn to train " + GetObjectName(ThunderEagleId) + "s";

    /// <inheritdoc/>
    protected override void OnComplete(Faction completingFaction)
    {
      SetPlayerTechResearched(completingFaction.Player, ResearchId, 1);
      completingFaction.Player?.DisplayUnitTypeAcquired(ThunderEagleId,
        "You can now train Thunder Eagles at the High Tower.");
    }
  }
}