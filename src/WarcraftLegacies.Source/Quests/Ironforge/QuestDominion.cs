using System.Collections.Generic;
using MacroTools.ControlPointSystem;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.ObjectiveSystem.Objectives.ControlPointBased;
using MacroTools.ObjectiveSystem.Objectives.FactionBased;
using MacroTools.ObjectiveSystem.Objectives.TimeBased;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using MacroTools.QuestSystem;
using WCSharp.Shared.Data;
using static War3Api.Common;


namespace WarcraftLegacies.Source.Quests.Ironforge
{
  public sealed class QuestDominion : QuestData
  {
    private readonly List<unit> _rescueUnits;

    public QuestDominion(Rectangle rescueRect) : base("Dwarven Dominion",
      "The Dwarven Dominion must be established before Ironforge can join the war.",
      @"ReplaceableTextures\CommandButtons\BTNDwarvenFortress.blp")
    {
      AddObjective(new ObjectiveControlPoint(ControlPointManager.Instance.GetFromUnitType(FourCC("n017"))));
      AddObjective(new ObjectiveControlPoint(ControlPointManager.Instance.GetFromUnitType(FourCC("n014"))));
      AddObjective(new ObjectiveControlPoint(ControlPointManager.Instance.GetFromUnitType(FourCC("n013"))));
      AddObjective(new ObjectiveUpgrade(FourCC("h07G"), FourCC("h07E")));
      AddObjective(new ObjectiveExpire(1462, Title));
      AddObjective(new ObjectiveSelfExists());
      ResearchId = FourCC("R043");
      _rescueUnits = rescueRect.PrepareUnitsForRescue(RescuePreparationMode.Invulnerable);
      
    }

    /// <inheritdoc/>
    public override string RewardFlavour =>
      "The Dwarven Empire is re-united again, Ironforge is ready for war again.";

    /// <inheritdoc/>
    protected override string RewardDescription => "Control of all units in Ironforge";

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
      foreach (var unit in _rescueUnits) unit.Rescue(completingFaction.Player ?? Player(PLAYER_NEUTRAL_AGGRESSIVE));
      completingFaction.Player?.PlayMusicThematic("war3mapImported\\DwarfTheme.mp3");
    }
  }
}