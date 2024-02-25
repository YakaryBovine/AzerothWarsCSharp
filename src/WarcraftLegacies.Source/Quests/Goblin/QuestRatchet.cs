using System.Collections.Generic;
using MacroTools.ControlPointSystem;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.ObjectiveSystem.Objectives.ControlPointBased;
using MacroTools.ObjectiveSystem.Objectives.FactionBased;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using MacroTools.QuestSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Goblin
{
  /// <summary>
  /// The Goblins can acquire Ratchet.
  /// </summary>
  public sealed class QuestRatchet : QuestData
  {
    private readonly List<unit> _rescueUnits;
    private readonly ObjectiveAnyUnitInRect _enterRatchetRegion;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestRatchet"/>.
    /// </summary>
    public QuestRatchet() : base("Ratchet Port",
      "The port of Ratchet would be a great expansion of our trade empire.",
      @"ReplaceableTextures\CommandButtons\BTNGoblinShop2.blp")
    {
      AddObjective(
        new ObjectiveControlPoint(ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N0A6_RATCHET)));
      AddObjective(_enterRatchetRegion = new ObjectiveAnyUnitInRect(Regions.Ratchet_Unlock_1, "Ratchet", true));
      AddObjective(new ObjectiveSelfExists());
      
      ResearchId = Constants.UPGRADE_VQ01_QUEST_COMPLETED_RATCHET_PORT;
      _rescueUnits = Regions.Ratchet_Unlock_1.PrepareUnitsForRescue(RescuePreparationMode.HideNonStructures,
        filterUnit => filterUnit.GetTypeId() != FourCC("ngme"));
    }

    /// <inheritdoc />
    public override string RewardFlavour =>
      $"{_enterRatchetRegion.CompletingUnitName} and his cronies have established the city of Ratchet, an ostensibly neutral port populated by smugglers and outcasts.";

    /// <inheritdoc />
    protected override string RewardDescription => "Gain control of a small outpost in Ratchet";

    /// <inheritdoc />
    protected override void OnFail(Faction completingFaction)
    {
      var rescuer = completingFaction.ScoreStatus == ScoreStatus.Defeated
        ? Player(PLAYER_NEUTRAL_AGGRESSIVE)
        : completingFaction.Player;

      rescuer.RescueGroup(_rescueUnits);
    }

    /// <inheritdoc />
    protected override void OnComplete(Faction completingFaction) => completingFaction.Player.RescueGroup(_rescueUnits);
  }
}