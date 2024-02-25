using System.Collections.Generic;
using MacroTools;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.ObjectiveSystem.Objectives.FactionBased;
using MacroTools.ObjectiveSystem.Objectives.TimeBased;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using MacroTools.QuestSystem;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Ironforge
{
  public sealed class QuestThelsamar : QuestData
  {
    private readonly List<unit> _rescueUnits;

    public QuestThelsamar(PreplacedUnitSystem preplacedUnitSystem, Rectangle rescueRect) : base("Murloc Menace",
      "A vile group of Murloc is terrorizing Thelsamar. Destroy them!",
      @"ReplaceableTextures\CommandButtons\BTNMurlocNightCrawler.blp")
    {
      AddObjective(new ObjectiveUnitIsDead(preplacedUnitSystem.GetUnit(Constants.UNIT_N0D1_MURLOC_SORCERER_CREEP_LOCH_MODAN)));
      AddObjective(new ObjectiveExpire(600, Title));
      AddObjective(new ObjectiveSelfExists());
      _rescueUnits = rescueRect.PrepareUnitsForRescue(RescuePreparationMode.Invulnerable);
      
    }

    /// <inheritdoc/>
    public override string RewardFlavour => "The Murlocs have been defeated, Thelsamar will join your cause.";

    /// <inheritdoc/>
    protected override string RewardDescription => "Control of all units in Thelsamar";

    /// <inheritdoc/>
    protected override void OnFail(Faction completingFaction)
    {
      var rescuer = completingFaction.ScoreStatus == ScoreStatus.Defeated
        ? Player(PLAYER_NEUTRAL_AGGRESSIVE)
        : completingFaction.Player;

      rescuer.RescueGroup(_rescueUnits);
    }

    /// <inheritdoc/>
    protected override void OnComplete(Faction completingFaction)
    {
      completingFaction.Player.RescueGroup(_rescueUnits);
    }
  }
}