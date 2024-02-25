using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using MacroTools.QuestSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Fel_Horde
{
  /// <summary>
  /// Visit Gul'dan's corpse to unlock a research.
  /// </summary>
  public sealed class QuestGuldansLegacy : QuestData
  {
    /// <inheritdoc/>
    protected override string RewardDescription =>
      $"Learn to research {GetObjectName(Constants.UPGRADE_R03O_BLOOD_RUNES_FEL_HORDE)} from the {GetObjectName(Constants.UNIT_O031_WAR_MILL_FEL_HORDE_RESEARCH)}";

    /// <inheritdoc/>
    public override string RewardFlavour =>
      "Gul'dan's remains have been located within the Tomb of Sargeras. His eldritch knowledge may now be put to purpose.";

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestGuldansLegacy"/> class.
    /// </summary>
    public QuestGuldansLegacy() : base("Gul'dans Legacy",
      "The Orc Warlock Gul'dan is ultimately responsible for the formation of the Fel Horde. Though long dead, his teachings could still be extracted from his body.",
      @"ReplaceableTextures\CommandButtons\BTNGuldan.blp")
    {
      AddObjective(new ObjectiveAnyUnitInRect(Regions.GuldansCorpse, "Gul'dan's corpse in the Tomb of Sargeras", true));
      ResearchId = Constants.UPGRADE_R041_QUEST_COMPLETED_GUL_DANS_LEGACY_FEL_HORDE;
    }
  }
}