using System.Collections.Generic;
using MacroTools;
using MacroTools.ControlPointSystem;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.ObjectiveSystem.Objectives.ControlPointBased;
using MacroTools.ObjectiveSystem.Objectives.FactionBased;
using MacroTools.ObjectiveSystem.Objectives.TimeBased;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using MacroTools.QuestSystem;
using WarcraftLegacies.Source.Setup.Legends;
using WCSharp.Shared.Data;
using static War3Api.Common;


namespace WarcraftLegacies.Source.Quests.Frostwolf
{
  /// <summary>
  /// Kill the centaur leader in Mulgore and bring <see cref="LegendFrostwolf.Cairne"/> to <see cref="LegendFrostwolf.ThunderBluff"/> to unlock it
  /// </summary>
  public sealed class QuestThunderBluff : QuestData
  {
    private readonly List<unit> _rescueUnits;

    /// <inheritdoc />
    public QuestThunderBluff(PreplacedUnitSystem preplacedUnitSystem, Rectangle rescueRect) : base("The Long March",
      "The Tauren have been wandering for too long. The fertile plains of Mulgore would offer respite from this endless journey.",
      @"ReplaceableTextures\CommandButtons\BTNCentaurKhan.blp")
    {
      AddObjective(new ObjectiveUnitIsDead(preplacedUnitSystem.GetUnit(FourCC("ncnk"), rescueRect.Center)));
      AddObjective(new ObjectiveHostilesInAreaAreDead(new List<Rectangle> { Regions.LongMarchCentaur }, "in the Thousand Needles"));
      AddObjective(new ObjectiveControlPoint(ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N026_THOUSAND_NEEDLES )));
      AddObjective(new ObjectiveControlPoint(ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N09G_MULGORE)));
      AddObjective(new ObjectiveExpire(480, Title));
      AddObjective(new ObjectiveSelfExists());
      ResearchId = Constants.UPGRADE_R05I_QUEST_COMPLETED_THE_LONG_MARCH; 
      _rescueUnits = rescueRect.PrepareUnitsForRescue(RescuePreparationMode.HideNonStructures);
      
    }

    //todo: bad flavour
    /// <inheritdoc />
    public override string RewardFlavour => "The long march of the Tauren clans has ended, and they have joined forces with the Horde.";

    /// <inheritdoc />
    protected override string RewardDescription => $"Control of Thunder Bluff and enable Cairne to be trained at the Altar of Storms";

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
      completingFaction.Player?
        .RescueGroup(_rescueUnits)
        .PlayMusicThematic("war3mapImported\\TaurenTheme.mp3");
    }
  }
}