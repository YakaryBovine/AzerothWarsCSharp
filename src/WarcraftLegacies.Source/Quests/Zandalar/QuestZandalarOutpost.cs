﻿using System.Collections.Generic;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.ObjectiveSystem.Objectives.FactionBased;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using MacroTools.QuestSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Zandalar
{
  /// <summary>
  /// The Trolls can establish an outpost in the Echo Isles.
  /// </summary>
  public sealed class QuestZandalarOutpost : QuestData
  {
    private readonly List<unit> _rescueUnits;
    private readonly ObjectiveAnyUnitInRect _enterZandalarRegion;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestZandalar"/>.
    /// </summary>
    public QuestZandalarOutpost() : base("Zandalar Outpost",
      "The trolls of the echo islands have said we can set up an outpost on one of the islands.",
      @"ReplaceableTextures\CommandButtons\BTNtrollmedium.blp")
    {
      AddObjective(_enterZandalarRegion = new ObjectiveAnyUnitInRect(Regions.Zandalari_Echo_Unlock, "Zandalar Outpost", true));
      AddObjective(new ObjectiveSelfExists());
      Required = true;
      ResearchId = Constants.UPGRADE_VQ01_QUEST_COMPLETED_RATCHET_PORT;
      _rescueUnits = Regions.Zandalari_Echo_Unlock.PrepareUnitsForRescue(RescuePreparationMode.HideNonStructures,
        filterUnit => filterUnit.GetTypeId() != FourCC("ngme"));
    }

    /// <inheritdoc />
    protected override string RewardFlavour =>
      $"{_enterZandalarRegion.CompletingUnitName} has spoken with the elders of the Echo Isles and they have agreed to let the Zandalar trolls set up an outpost on one of their islands.";

    /// <inheritdoc />
    protected override string RewardDescription => "Gain control of a small outpost in the Echo Isles";

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