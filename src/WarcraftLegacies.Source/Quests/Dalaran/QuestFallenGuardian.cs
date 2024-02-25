using MacroTools.ControlPointSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.ControlPointBased;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.QuestSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Dalaran
{
  public sealed class QuestFallenGuardian : QuestData
  {

    public QuestFallenGuardian(Capital karazhan) : base("The Fallen Guardian",
      "Medivh's body was corrupted by Sargeras at conception. Now that he is dead, the secrets of the Tomb of Sargeras and Karazhan combined might allow the mages of Dalaran to cleanse his spirit.",
      @"ReplaceableTextures\CommandButtons\BTNMedivh.blp")
    {
      AddObjective(new ObjectiveControlCapital(karazhan, false));
      AddObjective(new ObjectiveControlPoint(ControlPointManager.Instance.GetFromUnitType(FourCC("n00J"))));
      ResearchId = FourCC("R04K");
    }

    /// <inheritdoc/>
    public override string RewardFlavour =>
      "Medivh's spirit has been cleansed of Sargeras' influence, allowing him to return to Azeroth for a time.";

    /// <inheritdoc/>
    protected override string RewardDescription => "You can summon Medivh from the Altar of Knowledge";
  }
}