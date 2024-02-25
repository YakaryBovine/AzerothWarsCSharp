using System.Collections.Generic;
using MacroTools;
using MacroTools.ControlPointSystem;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.ObjectiveSystem.Objectives.ControlPointBased;
using MacroTools.ObjectiveSystem.Objectives.FactionBased;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using MacroTools.QuestSystem;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Zandalar
{
  /// <summary>
  /// Fully upgrade your main to unlock Zan
  /// </summary>
  public sealed class QuestZandalar : QuestData
  {
    private readonly List<unit> _rescueUnits;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestZandalar"/> class
    /// </summary>
    /// <param name="rescueRect"></param>
    /// <param name="preplacedUnitSystem"></param>
    public QuestZandalar(Rectangle rescueRect, PreplacedUnitSystem preplacedUnitSystem) : base("City of Gold", "We need to regain control of our land.",
      @"ReplaceableTextures\CommandButtons\BTNBloodTrollMage.blp")
    {
      AddObjective(new ObjectiveControlPoint(ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N092_ZUL_FARRAK)));
      AddObjective(new ObjectiveControlPoint(ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N0BK_LOST_CITY_OF_THE_TOL_VIR)));
      AddObjective(new ObjectiveControlPoint(ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N025_UN_GORO_CRATER)));
      AddObjective(new ObjectiveUpgrade(Constants.UNIT_O03Z_FORTRESS_ZANDALARI_T3, Constants.UNIT_O03Y_STRONGHOLD_ZANDALARI_T2));
      AddObjective(new ObjectiveSelfExists());
      ResearchId = Constants.UPGRADE_R04W_QUEST_COMPLETED_CITY_OF_GOLD;
      _rescueUnits = rescueRect.PrepareUnitsForRescue(RescuePreparationMode.Invulnerable);
      //For some reason that one dock just does not get rescured even though it is inside the rect so we add it manually here.
      _rescueUnits.Add(preplacedUnitSystem.GetUnit(Constants.UNIT_O049_GOLDEN_DOCK_ZANDALARI_SHIPYARD, new Point(-3738, -16911)));
      
    }

    /// <inheritdoc/>
    public override string RewardFlavour =>
      "The City of Gold is now yours to command and has joined the Zandalari";

    /// <inheritdoc/>
    protected override string RewardDescription =>
      "Control of all units in Dazar'alor and enables the Rasthakan to be trained";

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
      completingFaction.Player?
        .PlayMusicThematic("war3mapImported\\ZandalarTheme.mp3")
        .RescueGroup(_rescueUnits);
    }
  }
}