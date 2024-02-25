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

namespace WarcraftLegacies.Source.Quests.Sentinels
{
  /// <summary>
  /// Unlocks Abuderine 
  /// </summary>
  public sealed class QuestAstranaar : QuestData
  {
    private readonly List<unit> _rescueUnits = new();

    /// <summary>
    /// Initializes a new instance of <see cref="QuestAstranaar"/>.
    /// </summary>
    /// <param name="rescueRects"></param>
    public QuestAstranaar(List<Rectangle> rescueRects) : base("Daughters of the Moon",
      "Shandris need to warn the Sentinels in Auberdine of the Horde invadors by sending a messenger.",
      @"ReplaceableTextures\CommandButtons\BTNShandris.blp")
    {
      AddObjective(new ObjectiveAnyUnitInRect(Regions.AuberdineUnlock,
        "Auberdine", false));
      AddObjective(new ObjectiveControlPoint(ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N05U_FEATHERMOON_STRONGHOLD)));
      AddObjective(new ObjectiveUpgrade(Constants.UNIT_N06P_SENTINEL_ENCLAVE_SENTINEL_T3, Constants.UNIT_N06J_SENTINEL_OUTPOST_SENTINEL_T1));
      AddObjective(new ObjectiveExpire(480, Title));
      AddObjective(new ObjectiveSelfExists());
      ResearchId = Constants.UPGRADE_R03N_QUEST_COMPLETED_DAUGHTERS_OF_THE_MOON;

      foreach (var rectangle in rescueRects)
        _rescueUnits.AddRange(rectangle.PrepareUnitsForRescue(RescuePreparationMode.Invulnerable));
     
      
    }

    /// <inheritdoc />
    public override string RewardFlavour =>
      "Auberdine has been reached and has joined the Sentinels in their war effort";

    /// <inheritdoc />
    protected override string RewardDescription => "Control of all units in Astranaar Outpost and Auberdine. Maiev and Naisha will be trainable at Altar";

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