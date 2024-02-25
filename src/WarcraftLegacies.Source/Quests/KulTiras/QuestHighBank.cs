using MacroTools.ControlPointSystem;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.ControlPointBased;
using MacroTools.ObjectiveSystem.Objectives.FactionBased;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using MacroTools.QuestSystem;
using System.Collections.Generic;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.KulTiras
{
  /// <inheritdoc/>
  public sealed class QuestHighBank : QuestData
  {
    private readonly List<unit> _rescueUnits;
    private readonly LegendaryHero _katherine;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestHighBank"/> class.
    /// </summary>
    public QuestHighBank(Rectangle rescueRect, LegendaryHero katherine) : base("Eliminate Piracy",
      "Kul Tiras' historical isolationism has allowed piracy to fester throughout the seas. It's high time that we do something about it; we can start with the Goblin freebooters living it up in Booty Bay.",
      @"ReplaceableTextures\CommandButtons\BTNHeroTinker.blp")
    {
      _katherine = katherine;
      AddObjective(new ObjectiveHostilesInAreaAreDead(new List<Rectangle> { Regions.BootyBayQuest }, "in Booty Bay"));
      AddObjective(new ObjectiveControlLegend(katherine, false));
      AddObjective(
        new ObjectiveControlPoint(
          ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N00L_BOOTY_BAY)));
      AddObjective(new ObjectiveSelfExists());
      _rescueUnits = rescueRect.PrepareUnitsForRescue(RescuePreparationMode.HideNonStructures);
    }

    /// <inheritdoc/>
    public override string RewardFlavour =>
      "With the south coast of the Eastern Kingdoms now secure, High Bank has been established as a base of operations on an island near the Twilight Highlands.";

    /// <inheritdoc/>
    protected override string RewardDescription =>
      $"Gain control of High Bank, earn 225 gold, and {_katherine.Name} gains 2000 experience";

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
      completingFaction.Player?.AdjustPlayerState(PLAYER_STATE_RESOURCE_GOLD, 225);
      completingFaction.Player.RescueGroup(_rescueUnits);
      _katherine.Unit?.AddExperience(2000);
    }
  }
}