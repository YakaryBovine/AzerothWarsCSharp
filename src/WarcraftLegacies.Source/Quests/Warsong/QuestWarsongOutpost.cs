﻿using System.Collections.Generic;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.ObjectiveSystem.Objectives.FactionBased;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using MacroTools.QuestSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Warsong
{
  /// <summary>
  /// The Warsong clan can acquire the Warsong Outpost in Uldum.
  /// </summary>
  public sealed class QuestWarsongOutpost : QuestData
  {
    private readonly List<unit> _rescueUnits;
    private readonly ObjectiveAnyUnitInRect _enterWarsongOutpostRegion;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestWarsongOutpost"/>.
    /// </summary>
    public QuestWarsongOutpost() : base("Warsong Outpost in Uldum",
      "The desert of Uldum is rumoured to conceal a powerful weapon. We must send one of our heroes to claim leadership of the outpost and explore further.",
      @"ReplaceableTextures\CommandButtons\BTNIronHordeWatchTower.blp")
    {
      AddObjective(_enterWarsongOutpostRegion = new ObjectiveAnyUnitInRect(Regions.Warsong_Uldum_Unlock, "the outpost in western Uldum", true));
      AddObjective(new ObjectiveSelfExists());
      Required = true;
      ResearchId = Constants.UPGRADE_VQ03_QUEST_COMPLETED_WARSONG_OUTPOST;
      _rescueUnits = Regions.Warsong_Uldum_Unlock.PrepareUnitsForRescue(RescuePreparationMode.HideNonStructures,
        filterUnit => filterUnit.GetTypeId() != FourCC("ngme"));
    }

    /// <inheritdoc />
    protected override string RewardFlavour =>
      $"With the help of his southern allies, {_enterWarsongOutpostRegion.CompletingUnitName} has taken charge of the outpost in the deserts of Uldum.";

    /// <inheritdoc />
    protected override string RewardDescription => "Gain control of a small outpost in western Uldum";

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