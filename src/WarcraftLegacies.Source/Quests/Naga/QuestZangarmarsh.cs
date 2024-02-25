using System.Collections.Generic;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.FactionBased;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.ObjectiveSystem.Objectives.TimeBased;
using MacroTools.QuestSystem;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Naga
{
  /// <summary>
  /// </summary>
  public sealed class QuestZangarmarsh : QuestData
  {
    private readonly List<unit> _rescueUnits;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestZangarmarsh"/> class.
    /// </summary>
    public QuestZangarmarsh(Rectangle rescueRect, LegendaryHero vashj) : base("The Coilfang Reservoir",
      $"The bassins of Zangarmarsh will be the perfect breeding ground for Illidan's Naga",
      @"ReplaceableTextures\CommandButtons\BTNIllidariDemonGate.blp")
    {
      AddObjective(new ObjectiveLegendInRect(vashj, rescueRect, "Zangarmarsh"));
      AddObjective(new ObjectiveExpire(660, Title));
      AddObjective(new ObjectiveSelfExists());
      ResearchId = Constants.UPGRADE_R03B_QUEST_COMPLETED_THE_COILFANG_RESERVOIR;
      _rescueUnits = rescueRect.PrepareUnitsForRescue(RescuePreparationMode.HideAll);
      
    }

    /// <inheritdoc />
    public override string RewardFlavour => "The Clutcheries of Zangarmarsh are now built";

    /// <inheritdoc />
    protected override string RewardDescription => "Gain control of the Zangarmarsh Outpost and the ability to build the Clutchery";

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