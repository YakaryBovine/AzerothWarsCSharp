using System.Collections.Generic;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.FactionBased;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.ObjectiveSystem.Objectives.TimeBased;
using MacroTools.QuestSystem;
using WarcraftLegacies.Source.Setup.Legends;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Naga
{
  /// <summary>
  /// Bring <see cref="LegendIllidan.Illidan"/> to <see cref="LegendFelHorde.BlackTemple"/> to gain control of it.
  /// </summary>
  public sealed class QuestBlackTemple : QuestData
  {
    private readonly List<unit> _rescueUnits;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestBlackTemple"/> class.
    /// </summary>
    public QuestBlackTemple(Rectangle rescueRect, LegendaryHero illidan) : base("Seat of Power",
      $"Illidan requires the aid of his servants in Outland for the upcoming war. He must travel to the Black Temple to muster them. His incredible power allows him to move between worlds with ease.",
      @"ReplaceableTextures\CommandButtons\BTNWarpPortal.blp")
    {
      AddObjective(new ObjectiveLegendInRect(illidan, Regions.IllidanBlackTempleUnlock, "Black Temple"));
      AddObjective(new ObjectiveExpire(660, Title));
      AddObjective(new ObjectiveSelfExists());
      ResearchId = Constants.UPGRADE_R09Y_QUEST_COMPLETED_SEAT_OF_POWER;
      _rescueUnits = rescueRect.PrepareUnitsForRescue(RescuePreparationMode.HideNonStructures);
      
    }

    /// <inheritdoc />
    public override string RewardFlavour => "The forces of Outland are now under Illidan's command.";

    /// <inheritdoc />
    protected override string RewardDescription => $"Gain control of the Black Temple and enable Lady Vashj to be trained at the Altar of the Betrayer";

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
    }
  }
}