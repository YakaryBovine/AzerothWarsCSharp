using System.Collections.Generic;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.ObjectiveSystem.Objectives.FactionBased;
using MacroTools.ObjectiveSystem.Objectives.TimeBased;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using MacroTools.QuestSystem;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Stormwind
{
  public sealed class QuestStromgarde : QuestData
  {
    private readonly List<unit> _rescueUnits = new();

    public QuestStromgarde(Rectangle rescueRect) : base("Stromgarde",
      "Although Stromgarde's strength has dwindled since the days of the Arathorian Empire, it remains a significant bastion of humanity. They could be convinced to mobilize their forces for Stormwind.",
      @"ReplaceableTextures\CommandButtons\BTNTheCaptain.blp")
    {
      var objectiveAnyUnitInRect = new ObjectiveAnyUnitInRect(Regions.Stromgarde, "Stromgarde", true);
      AddObjective(objectiveAnyUnitInRect);
      AddObjective(new ObjectiveTime(900));
      AddObjective(new ObjectiveSelfExists());
      ResearchId = Constants.UPGRADE_R01M_QUEST_COMPLETED_STROMGARDE_STORMWIND;
      foreach (var unit in CreateGroup().EnumUnitsInRect(rescueRect).EmptyToList())
        if (GetOwningPlayer(unit) == Player(PLAYER_NEUTRAL_PASSIVE))
        {
          SetUnitInvulnerable(unit, true);
          _rescueUnits.Add(unit);
        }
    }

    /// <inheritdoc />
    public override string RewardFlavour => "Galen Trollbane has pledged his forces to Stormwind's cause.";

    /// <inheritdoc />
    protected override string RewardDescription =>
      "Control of all units at Stromgarde, the artifact Trol'kalar, and you can summon the hero Galen Trollbane from the Altar of Kings";

    /// <inheritdoc />
    protected override void OnFail(Faction completingFaction)
    {
      foreach (var unit in _rescueUnits) 
        unit.Rescue(Player(PLAYER_NEUTRAL_AGGRESSIVE));
    }

    /// <inheritdoc />
    protected override void OnComplete(Faction completingFaction)
    {
      SetPlayerTechResearched(completingFaction.Player, ResearchId, 1);
      foreach (var unit in _rescueUnits) unit.Rescue(completingFaction.Player);
    }

    /// <inheritdoc />
    protected override void OnAdd(Faction whichFaction)
    {
      whichFaction.ModObjectLimit(ResearchId, Faction.UNLIMITED);
    }
  }
}