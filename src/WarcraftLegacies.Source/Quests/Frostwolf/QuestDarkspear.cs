using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.QuestSystem;
using System.Collections.Generic;
using MacroTools.ObjectiveSystem.Objectives.FactionBased;
using MacroTools.ObjectiveSystem.Objectives.TimeBased;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Frostwolf
{
  /// <summary>
  /// Bring any hero to the Echo Isles to unlock the base.
  /// </summary>
  public sealed class QuestDarkspear : QuestData
  {
    private readonly List<unit> _rescueUnits;

    /// <inheritdoc />
    public QuestDarkspear() : base("The Darkspear Trolls",
      "Mere months ago, Thrall's forces saved the Darkspear tribe from the brink of extinction at the hands of constant murloc raids. They have recently made a new home on the Echo Isles, and could prove formidable allies in the invasion of Kalimdor.",
      @"ReplaceableTextures\CommandButtons\BTNWitchDoctor.blp")
    {
      _rescueUnits = Regions.EchoUnlock.PrepareUnitsForRescue(RescuePreparationMode.HideNonStructures);
      AddObjective(new ObjectiveSelfExists());
      AddObjective(new ObjectiveExpire(480, Title));
      AddObjective(new ObjectiveAnyUnitInRect(Regions.EchoUnlock, "Echo Isles", true));
      
      ResearchId = Constants.UPGRADE_R032_QUEST_COMPLETED_THE_DARKSPEAR_TROLLS;
    }

    /// <inheritdoc />
    public override string RewardFlavour =>
      "Vol'jin, foremost Shadow Hunter of the Darkspear Tribe, welcomes Thrall to his village with open arms. The trolls of the Echo Isles unanimously agree to join the new Horde.";

    /// <inheritdoc />
    protected override string RewardDescription =>
      "You gain control of Echo Isles, and learn to train Vol'jin from the Altar of Storms";

    /// <inheritdoc />
    protected override void OnFail(Faction completingFaction)
    {
      var rescuer = completingFaction.ScoreStatus == ScoreStatus.Defeated
        ? Player(PLAYER_NEUTRAL_AGGRESSIVE)
        : completingFaction.Player;

      rescuer.RescueGroup(_rescueUnits);
    }

    /// <inheritdoc />
    protected override void OnComplete(Faction completingFaction) =>
      completingFaction.Player.RescueGroup(_rescueUnits);
  }
}