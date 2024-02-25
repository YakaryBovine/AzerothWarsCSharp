using System.Collections.Generic;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.QuestSystem;
using WCSharp.Shared.Data;
using static War3Api.Common;


namespace WarcraftLegacies.Source.Quests.Zandalar
{
  /// <summary>
  /// Capture Zul'Farrak to unlock Gahz'rilla as a hero/>
  /// </summary>
  public sealed class QuestZulfarrak : QuestData
  {
    private readonly List<unit> _rescueUnits;
    private readonly Capital _zulfarrak;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestZulfarrak"/> class.
    /// </summary>
    /// <param name="rescueRect"></param>
    /// <param name="zulfarrak"></param>
    /// <param name="zul"></param>
    public QuestZulfarrak(Rectangle rescueRect, Capital zulfarrak, LegendaryHero zul) : base("Fury of the Sands",
      "The Sandfury Trolls of Zul'farrak are openly hostile to visitors, but they share a common heritage with the Zandalari Trolls. An adequate display of force could bring them around.",
      @"ReplaceableTextures\CommandButtons\BTNDarkTroll.blp")
    {
      _zulfarrak = zulfarrak;
      ResearchId = Constants.UPGRADE_R02F_QUEST_COMPLETED_FURY_OF_THE_SANDS_WARSONG;
      AddObjective(new ObjectiveControlCapital(zulfarrak, false));
      AddObjective(new ObjectiveLegendReachRect(zul, rescueRect, "Zul'Farrak"));
      _rescueUnits = rescueRect.PrepareUnitsForRescue(RescuePreparationMode.HideNonStructures);
      
    }

    /// <inheritdoc/>
    public override string RewardFlavour =>
      $"Zul'farrak has fallen. The Sandfury trolls lend their might to the Zandalari.";

    /// <inheritdoc/>
    protected override string RewardDescription =>
      "Control of Zul'farrak, 150 gold tribute, enable to train Storm Wyrm and you can summon the hero Gahz'rilla from the Altar of Conquerors";

    /// <inheritdoc/>>
    protected override void OnComplete(Faction completingFaction)
    {
      if (completingFaction.Player != null)
      {
        SetUnitOwner(_zulfarrak.Unit, completingFaction.Player, true);
        completingFaction.Player.AdjustPlayerState(PLAYER_STATE_RESOURCE_GOLD, 150);
        completingFaction.Player.RescueGroup(_rescueUnits);
      }
    }
  }
}