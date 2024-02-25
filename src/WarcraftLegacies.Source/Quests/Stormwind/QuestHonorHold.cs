using System.Collections.Generic;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.QuestSystem;
using WCSharp.Shared.Data;
using static War3Api.Common;


namespace WarcraftLegacies.Source.Quests.Stormwind
{
  public sealed class QuestHonorHold : QuestData
  {
    private readonly List<unit> _rescueUnits = new();

    public QuestHonorHold(Rectangle rescueRect, Capital hellfireCitadel) : base("Honor Hold",
      "Despite Outland's incredibly harsh climate, some Alliance forces have managed to make a home there - a town called Honor Hold",
      @"ReplaceableTextures\CommandButtons\BTNHumanBarracks.blp")
    {
      foreach (var unit in CreateGroup().EnumUnitsInRect(rescueRect).EmptyToList())
        if (GetOwningPlayer(unit) == Player(PLAYER_NEUTRAL_PASSIVE))
        {
          SetUnitInvulnerable(unit, true);
          _rescueUnits.Add(unit);
        }
      AddObjective(new ObjectiveCapitalDead(hellfireCitadel));
      ResearchId = Constants.UPGRADE_R039_QUEST_COMPLETED_HONOR_HOLD;
    }

    /// <inheritdoc/>
    public override string RewardFlavour =>
      "Honor Hold is now free from the constant looming threat of Hellfire Citadel, and have finally been reconnected with their Alliance from Azeroth.";

    /// <inheritdoc/>
    protected override string RewardDescription =>
      $"Control of all units at Honor Hold and {GetObjectName(Constants.UNIT_O06K_SIEGE_TOWER_STORMWIND)} gain the {GetObjectName(Constants.ABILITY_A108_ARTILLERY_BOMBARDMENT_STORMWIND)} ability."; 

    /// <inheritdoc/>
    protected override void OnFail(Faction completingFaction)
    {
      foreach (var unit in _rescueUnits) unit.Rescue(Player(PLAYER_NEUTRAL_AGGRESSIVE));
    }

    /// <inheritdoc/>
    protected override void OnComplete(Faction completingFaction)
    {
      foreach (var unit in _rescueUnits) unit.Rescue(completingFaction.Player);
      //Set animations of doodads within Honor Hold
      SetDoodadAnimationRect(Regions.HonorHold.Rect, FourCC("ISrb"), "hide", false);
      SetDoodadAnimationRect(Regions.HonorHold.Rect, FourCC("LSst"), "hide", false);
      SetDoodadAnimationRect(Regions.HonorHold.Rect, FourCC("CSra"), "unhide", false);
    }
  }
}