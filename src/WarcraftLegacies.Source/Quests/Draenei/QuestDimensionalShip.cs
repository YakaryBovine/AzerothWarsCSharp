using MacroTools.ControlPointSystem;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.ObjectiveSystem.Objectives.ControlPointBased;
using MacroTools.ObjectiveSystem.Objectives.FactionBased;
using MacroTools.ObjectiveSystem.Objectives.QuestBased;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using MacroTools.QuestSystem;
using MacroTools.LegendSystem;
using WarcraftLegacies.Source.Objectives;
using WCSharp.Shared.Data;
using static War3Api.Common;
using MacroTools.ObjectiveSystem.Objectives.TimeBased;

namespace WarcraftLegacies.Source.Quests.Draenei
{
  /// <summary>
  /// The Draenei acquire some kind of power source to power their ship.
  /// </summary>
  public sealed class QuestDimensionalShip : QuestData
  {
    private readonly ObjectivePowerSource _objectivePowerSource;
    private readonly unit _dimensionalGenerator;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestDimensionalShip"/> class.
    /// </summary>
    public QuestDimensionalShip(Rectangle questRect, QuestData prerequisite, Capital dimensionalGenerator) : base(
      "The Dimensional Ship",
      "The core of the Exodar is rebuilt, but it requires a great source of power to function again. Finding that source of power would make the Exodar a powerful asset for the Draenei.",
      @"ReplaceableTextures\CommandButtons\BTNArcaneEnergy.blp")
    {
      _dimensionalGenerator = dimensionalGenerator.Unit;
      
      AddObjective(new ObjectiveQuestComplete(prerequisite));
      AddObjective(new ObjectiveTime(1200));
      AddObjective(new ObjectiveBuildInRect(questRect, "inside the Exodar", Constants.UNIT_O056_ARCANE_WELL_DRAENEI_FARM, 10));
      AddObjective(new ObjectiveControlLevel(
        ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N0BL_EXODAR_REGALIS), 20));
      _objectivePowerSource = new ObjectivePowerSource(_dimensionalGenerator, new[]
      {
        Constants.ITEM_I006_BOOK_OF_MEDIVH,
        Constants.ITEM_I003_EYE_OF_SARGERAS,
        Constants.ITEM_I011_CROWN_OF_THE_TRIUMVIRATE,
        Constants.ITEM_I018_VIAL_OF_THE_SUNWELL,
        Constants.ITEM_I00H_SULFURAS_HAND_OF_RAGNAROS,
        Constants.ITEM_I01Y_HELM_OF_DOMINATION
      });
      AddObjective(_objectivePowerSource);
      AddObjective(new ObjectiveSelfExists());
      ResearchId = Constants.UPGRADE_R09A_QUEST_COMPLETED_THE_DIMENSIONAL_SHIP;
    }

    /// <inheritdoc/>
    protected override void OnComplete(Faction whichFaction)
    {
      _objectivePowerSource.UsedPowerSource?.SetDroppable(false);
      CreateTrigger()
        .RegisterUnitEvent(_dimensionalGenerator, EVENT_UNIT_DEATH)
        .AddAction(() => { _objectivePowerSource.UsedPowerSource?.SetDroppable(true); });
    }

    /// <inheritdoc/>
    public override string RewardFlavour =>
      "With the acquisition of a replacement power source, the Exodar's gemcrafters set to work reigniting the ship's dimensional portals. The Dimensional Generator can now now be used to travel the planes once more.";

    /// <inheritdoc/>
    protected override string RewardDescription =>
      "The Dimensional Generator gains the ability to channel portals to Argus, Azuremyst, and Outland. The Lightforged units and A'dal will become available";
  }
}