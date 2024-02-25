using System.Collections.Generic;
using MacroTools;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.FactionBased;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.ObjectiveSystem.Objectives.TimeBased;
using MacroTools.QuestSystem;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Stormwind
{
  public sealed class QuestNethergarde : QuestData
  {
    private readonly List<unit> _rescueUnits;
    private readonly unit _gate;

    public QuestNethergarde(PreplacedUnitSystem preplacedUnitSystem, LegendaryHero varian) : base("Nethergarde Relief",
      "Nethergarde Keep fort is holding down the Dark Portal, they will need to be reinforced soon!",
      @"ReplaceableTextures\CommandButtons\BTNStormwindGuardTower.blp")
    {
      AddObjective(new ObjectiveLegendInRect(varian, Regions.NethergardeUnlock, "Nethergarde"));
      AddObjective(new ObjectiveExpire(600, Title));
      AddObjective(new ObjectiveSelfExists());
      _rescueUnits = Regions.NethergardeUnlock.PrepareUnitsForRescue(RescuePreparationMode.HideNonStructures);
      _gate = preplacedUnitSystem.GetUnit(Constants.UNIT_H00L_HORIZONTAL_WOODEN_GATE_OPEN, new Point(17140, -18000));
    }

    /// <inheritdoc />
    public override string RewardFlavour => "Varian has come to relieve the Nethergarde garrison.";

    /// <inheritdoc />
    protected override string RewardDescription => "You gain control of Nethergarde";

    /// <inheritdoc />
    protected override void OnFail(Faction completingFaction)
    {
      var rescuer = completingFaction.ScoreStatus == ScoreStatus.Defeated
        ? Player(PLAYER_NEUTRAL_AGGRESSIVE)
        : completingFaction.Player;

      rescuer.RescueGroup(_rescueUnits);
    }

    /// <inheritdoc />
    protected override void OnComplete(Faction completingFaction)
    {
      completingFaction.Player.RescueGroup(_rescueUnits);
      SetUnitOwner(_gate, completingFaction.Player, true);
    }
  }
}