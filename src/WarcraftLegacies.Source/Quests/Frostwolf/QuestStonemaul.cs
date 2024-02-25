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
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Frostwolf
{
  public sealed class QuestStonemaul : QuestData
  {
    private readonly List<unit> _rescueUnits;

    public QuestStonemaul(PreplacedUnitSystem preplacedUnitSystem, Rectangle rescueRect) : base("The Chieftain's Challenge",
      "Rexxar is having trouble with a beligerent Ogre Warlord, slay the Chieftain to gain the hero's allegiance.",
      @"ReplaceableTextures\CommandButtons\BTNOneHeadedOgre.blp")
    {
      AddObjective(new ObjectiveControlPoint(ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N022_STONEMAUL)));
      AddObjective(new ObjectiveUnitIsDead(preplacedUnitSystem.GetUnit(Constants.UNIT_NOGA_STONEMAUL_WARCHIEF_KOR_GALL)));
      AddObjective(new ObjectiveExpire(480,Title));
      AddObjective(new ObjectiveSelfExists());
      ResearchId = Constants.UPGRADE_R03S_QUEST_COMPLETED_THE_CHIEFTAIN_S_CHALLENGE_FROSTWOLF;
      _rescueUnits = rescueRect.PrepareUnitsForRescue(RescuePreparationMode.Invulnerable);
      
    }

    //Todo: bad flavour
    /// <inheritdoc/>
    public override string RewardFlavour =>
      "Korghal has been defeated, Rexxar has joined the Frostwolf!";

    /// <inheritdoc/>
    protected override string RewardDescription => $"Control of all buildings in Stonemaul and Rexxar is now trainable at the Altar";

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