using MacroTools;
using MacroTools.ControlPointSystem;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.ObjectiveSystem.Objectives.ControlPointBased;
using MacroTools.ObjectiveSystem.Objectives.FactionBased;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using MacroTools.QuestSystem;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Legion
{
  public sealed class QuestArgusControl : QuestData
  {
    private readonly unit _legionTeleporter1;
    private readonly unit _legionTeleporter2;

    public QuestArgusControl(PreplacedUnitSystem preplacedUnitSystem) : base("Argus Incursion",
      "The planet of Argus is not fully under the control of the Legion. Bring it under control!",
      @"ReplaceableTextures\CommandButtons\BTNMastersLodge.blp")
    {
      AddObjective(new ObjectiveControlPoint(
        ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N0BF_ANTORAN_WASTES)));
      AddObjective(
        new ObjectiveControlPoint(
          ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N0BH_EREDATH)));
      AddObjective(
        new ObjectiveControlPoint(
          ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N0BG_KROKUUN)));
      AddObjective(new ObjectiveUpgrade(Constants.UNIT_U00N_BURNING_CITADEL_LEGION_T3,
        Constants.UNIT_U00C_LEGION_BASTION_LEGION_T2));
      AddObjective(new ObjectiveSelfExists());
      ResearchId = Constants.UPGRADE_R055_QUEST_COMPLETED_ARGUS_INCURSION;
      Required = true;

      _legionTeleporter1 =
        preplacedUnitSystem.GetUnit(Constants.UNIT_N0BE_LEGION_TELEPORTERS_LEGION_OTHER, new Point(22939, -29345));
      _legionTeleporter2 =
        preplacedUnitSystem.GetUnit(Constants.UNIT_N0BE_LEGION_TELEPORTERS_LEGION_OTHER, new Point(23536, -29975));
    }

    /// <inheritdoc/>
    protected override string RewardDescription =>
      "With Argus finally under the Legion's control, the invasion of Azeroth can begin! ";

    /// <inheritdoc/>
    protected override string RewardFlavour =>
      "Learn to research Astral Walk from the Burning Citadel, train Tichondrius and Anetheron from the Altar of Destruction, and cast the Portal spells from the Legion Teleporter";

    /// <inheritdoc/>
    protected override void OnComplete(Faction completingFaction)
    {
      _legionTeleporter1.SetWaygateDestination(Regions.NorthrendLegionLanding.Center);
      _legionTeleporter2.SetWaygateDestination(Regions.AlteracLegionLanding.Center);
    }
  }
}