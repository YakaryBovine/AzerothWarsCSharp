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
    private readonly List<unit> _rescueUnits = new();

    public QuestThelsamar(PreplacedUnitSystem preplacedUnitSystem, Rectangle rescueRect) : base("Murloc Menace",
      "A vile group of Murloc is terrorizing Thelsamar. Destroy them!",
      @"ReplaceableTextures\CommandButtons\BTNMurlocNightCrawler.blp")
    {
      AddObjective(new ObjectiveUnitIsDead(preplacedUnitSystem.GetUnit(FourCC("N089"))));
      AddObjective(new ObjectiveExpire(1020, Title));
      AddObjective(new ObjectiveSelfExists());
      _rescueUnits = rescueRect.PrepareUnitsForRescue(RescuePreparationMode.Invulnerable);
      Required = true;
    }

    /// <inheritdoc/>
    protected override string RewardFlavour => "The Murlocs have been defeated, Thelsamar will join your cause.";

    /// <inheritdoc/>
    protected override string RewardDescription => "Control of all units in Thelsamar";

    /// <inheritdoc/>
    protected override void OnFail(Faction completingFaction)
    {
      foreach (var unit in _rescueUnits) unit.Rescue(Player(PLAYER_NEUTRAL_AGGRESSIVE));
    }

    /// <inheritdoc/>
    protected override void OnComplete(Faction completingFaction)
    {
      foreach (var unit in _rescueUnits) unit.Rescue(completingFaction.Player);
    }
  }
}