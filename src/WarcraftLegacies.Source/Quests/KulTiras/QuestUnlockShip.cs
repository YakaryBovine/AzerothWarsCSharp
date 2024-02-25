using System.Collections.Generic;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.QuestSystem;
using WCSharp.Shared.Data;
using static War3Api.Common;
using static War3Api.Blizzard;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.FactionBased;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using System.Linq;

namespace WarcraftLegacies.Source.Quests.KulTiras
{
  /// <summary>
  /// Proudmoore captial ship starts locked. Take control of Kul Tiras to unlock it.
  /// </summary>
  public sealed class QuestUnlockShip : QuestData
  {
    private readonly unit _proudmooreCapitalShip;
    private readonly List<unit> _rescueUnits;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestUnlockShip"/> class.
    /// </summary>
    /// <param name="rescueRect">All units in this area will be made neutral, then rescued when the quest is completed.</param>
    /// <param name="proudmooreCapitalShip">starts invulnerable and unusable. Made usuable and vulnerable when the quest is completed.</param>
    /// <param name="legendBoralus">Must be controlled to complete the quest.</param>
    /// <param name="daelinProudmoore">Must be controlled to complete the quest.</param>
    public QuestUnlockShip(Rectangle rescueRect, unit proudmooreCapitalShip, Capital legendBoralus,
      LegendaryHero daelinProudmoore) : base("Stranglethorn Expedition",
      "The Stranglethorn vale is still infested with trolls and pirates. If peace is to be brought back to the South Alliance, it needs to be purged",
      @"ReplaceableTextures\CommandButtons\BTNGalleonIcon.blp")
    {
      AddObjective(new ObjectiveControlCapital(legendBoralus, false));
      AddObjective(new ObjectiveControlLegend(daelinProudmoore, false));
      AddObjective(new ObjectiveResearch(Constants.UPGRADE_R05J_STRANGLETHORN_EXPEDITION_KULTIRAS, Constants.UNIT_H046_BORALUS_KEEP_KUL_TIRAS));
      AddObjective(new ObjectiveSelfExists());
      _proudmooreCapitalShip = proudmooreCapitalShip;
      _rescueUnits = rescueRect.PrepareUnitsForRescue(RescuePreparationMode.HideNonStructures);
    }

    /// <inheritdoc/>
    public override string RewardFlavour => "The capital ship will set sail with the Kul'tiran navy army to Stranglethorn Vale.";

    /// <inheritdoc/>
    protected override string RewardDescription =>
      "Unlock the Proudmoore capital ship and the buildings inside. Move all your non-worker units to Stranglethorn Vale";

    /// <inheritdoc/>
    protected override void OnComplete(Faction completingFaction)
    {
      if (completingFaction.Player != null)
      {
        MoveStranglethorn(completingFaction.Player);
        completingFaction.Player.RescueGroup(_rescueUnits);
        _proudmooreCapitalShip.Rescue(completingFaction.Player);
      }
      else
      {
        Player(bj_PLAYER_NEUTRAL_VICTIM).RescueGroup(_rescueUnits);
        _proudmooreCapitalShip.Rescue(Player(PLAYER_NEUTRAL_AGGRESSIVE));
      }
      _proudmooreCapitalShip.PauseEx(false);


    }

    private static void MoveStranglethorn(player whichPlayer)
    {
      foreach (var unit in CreateGroup().EnumUnitsInRect(Rectangle.WorldBounds).EmptyToList().Where(x => x.OwningPlayer() == whichPlayer))
      {
        if (!IsUnitType(unit, UNIT_TYPE_STRUCTURE) && !IsUnitType(unit, UNIT_TYPE_ANCIENT) && !IsUnitType(unit, UNIT_TYPE_PEON))
          SetUnitPosition(unit, 6864, -17176);
      }

      whichPlayer.RepositionCamera(6864, -17176);
    }

    /// <inheritdoc/>
    protected override void OnFail(Faction completingFaction)
    {
      _proudmooreCapitalShip.Remove();
    }
  }
}