﻿using MacroTools.QuestSystem;
using static War3Api.Common;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.LegendSystem;
using MacroTools.Extensions;
using System.Collections.Generic;
using WCSharp.Shared.Data;
using MacroTools.FactionSystem;

namespace WarcraftLegacies.Source.Quests.Frostwolf
{
  /// <summary>
  /// Bring any hero to the Echo Isles to unlock the base.
  /// </summary>
  public sealed class QuestHighmountain : QuestData
  {
    private readonly LegendaryHero _cairne;
    private readonly List<unit> _rescueUnits = new();

    /// <inheritdoc />
    public QuestHighmountain(LegendaryHero cairne, Rectangle rescueRect) : base("A Feast for Our Kin",
      "Scouts report sighting of the Highmountain totem, thought lost long ago when the Broken Isles were shattered. As a gesture of renewed welcome, Cairne might offer them an invitation to a feast in Thunderbluff.",
      @"ReplaceableTextures/CommandButtons/BTNPigHead.blp")
    {
      _cairne = cairne;
      AddObjective(new ObjectiveLegendInRect(cairne, rescueRect, "Highmountain, north of Stormheim"));
      ResearchId = Constants.UPGRADE_R0A9_QUEST_COMPLETED_INVITATION_TO_A_FEAST;
      _rescueUnits = rescueRect.PrepareUnitsForRescue(RescuePreparationMode.Invulnerable);
    }

    /// <inheritdoc />
    protected override string RewardFlavour =>
      "Cairne is welcomed in Highmountain like a lost-long friend. Eager to explore the world and fight alongside their long-lost brethren, the Highmountain send their best hunters to support the Horde, and offer their home as a traveler's respite.";

    /// <inheritdoc />
    protected override string RewardDescription =>
      $"Learn to train {GetObjectName(Constants.UNIT_N049_WANDERER_FROSTWOLF)}s from the {GetObjectName(Constants.UNIT_OTTO_TAUREN_TOTEM_FROSTWOLF_SPECIALIST)}";

    protected override void OnFail(Faction completingFaction)
    {
      var rescuer = completingFaction.ScoreStatus == ScoreStatus.Defeated
        ? Player(PLAYER_NEUTRAL_AGGRESSIVE)
        : completingFaction.Player;

      rescuer.RescueGroup(_rescueUnits);
    }

    protected override void OnComplete(Faction completingFaction)
    {
      completingFaction.Player.RescueGroup(_rescueUnits);
    }

  }
}