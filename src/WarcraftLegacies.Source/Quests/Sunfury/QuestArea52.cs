using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.QuestSystem;
using WCSharp.Shared.Data;
using MacroTools.ControlPointSystem;
using MacroTools.ObjectiveSystem.Objectives.ControlPointBased;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using static War3Api.Common;
using MacroTools.ObjectiveSystem.Objectives.TimeBased;
using System.Collections.Generic;
using MacroTools.ObjectiveSystem.Objectives.FactionBased;

namespace WarcraftLegacies.Source.Quests.Sunfury
{
  /// <summary>
  /// Build various structures inside <see cref="Regions.Area52Unlock"/>
  /// </summary>
  public sealed class QuestArea52 : QuestData
  {
    private readonly List<unit> _rescueUnits;
    private const int GoldReward = 200;
    private const int LumberReward = 200;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="QuestArea52"/> class.
    /// </summary>
    public QuestArea52(Rectangle rescueRect) : base("Area 52",
      "The goblins of Area 52 have lived in Netherstorm long before our arrival. In other circumstances, they may have been potential allies - but desperate times call for desperate conquests.",
      @"ReplaceableTextures\CommandButtons\BTNLordaeronPalace.blp")
    {
      
      AddObjective(new ObjectiveHostilesInAreaAreDead(new List<Rectangle> { Regions.Area52Unlock }, "in Area 52"));
      AddObjective(
        new ObjectiveControlPoint(
          ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N07Q_AREA_52)));
      AddObjective(new ObjectiveExpire(600, Title));
      AddObjective(new ObjectiveSelfExists());
      _rescueUnits = rescueRect.PrepareUnitsForRescue(RescuePreparationMode.HideAll,
        filterUnit => filterUnit.GetTypeId() != FourCC("ngme"));
    }

    /// <inheritdoc/>
    protected override void OnComplete(Faction whichFaction)
    {
      whichFaction.Player?.AddGold(GoldReward);
      whichFaction.Player?.AddLumber(LumberReward);
      if (whichFaction.Player != null)
        whichFaction.Player.RescueGroup(_rescueUnits);
      else
        Player(PLAYER_NEUTRAL_AGGRESSIVE).RescueGroup(_rescueUnits);
    }

    /// <inheritdoc/>
    public override string RewardFlavour =>
      "The goblins of Area 52 once aspired to travel amongst the stars. Now they aspire to nothing, buried one layer of dirt beneath our new settlement.";

    /// <inheritdoc/>
    protected override string RewardDescription =>
      $"Gain {GoldReward} gold, {LumberReward} lumber, and a base in Area 52";
  }
}