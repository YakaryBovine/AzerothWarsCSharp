using System.Collections.Generic;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.ObjectiveSystem.Objectives.FactionBased;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using MacroTools.QuestSystem;
using WCSharp.Shared.Data;

namespace WarcraftLegacies.Source.Quests.Quelthalas
{
  public sealed class QuestQueldanil : QuestData
  {
    private readonly List<unit> _rescueUnits;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestQueldanil"/> class.
    /// </summary>
    public QuestQueldanil(Rectangle rescueRect) :
      base("Quel'danil Lodge",
        "Quel'danil Lodge is a High Elven outpost situated in the Hinterlands. It's been some time since the rangers there have been in contact with Quel'thalas.",
        @"ReplaceableTextures\CommandButtons\BTNBearDen.blp")
    {
      ResearchId = UPGRADE_R074_QUEST_COMPLETED_QUEL_DANIL_LODGE;
      _rescueUnits = rescueRect.PrepareUnitsForRescue(RescuePreparationMode.HideNonStructures);
      AddObjective(new ObjectiveSelfExists());
      AddObjective(new ObjectiveAnyUnitInRect(Regions.QuelDanil_Lodge, "Quel'danil Lodge", true));
    }

    /// <inheritdoc/>
    public override string RewardFlavour =>
      "The rangers of Quel'danil have been reunited with the forces of Quel'thalas.";

    /// <inheritdoc/>
    protected override string RewardDescription => "Gain control of Quel'danil Lodge and its rangers";

    /// <inheritdoc/>
    protected override void OnComplete(Faction completingFaction)
    {
      completingFaction.Player.RescueGroup(_rescueUnits);
    }
  }
}