using System.Collections.Generic;
using MacroTools;
using MacroTools.ControlPointSystem;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.ControlPointBased;
using MacroTools.ObjectiveSystem.Objectives.FactionBased;
using MacroTools.ObjectiveSystem.Objectives.TimeBased;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using MacroTools.QuestSystem;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Quelthalas
{
  /// <summary>
  /// Kill the Trolls attacking Silvermoon.
  /// </summary>
  public sealed class QuestSilvermoon : QuestData
  {
    private readonly unit _elvenRunestone;
    private readonly Capital _silvermoon;
    private readonly Capital _sunwell;
    private readonly List<unit> _rescueUnits;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestSilvermoon"/> class.
    /// </summary>
    public QuestSilvermoon(Rectangle rescueRect, unit elvenRunestone, PreplacedUnitSystem preplacedUnitSystem,
      Capital silvermoon, Capital sunwell) : base("The Siege of Silvermoon",
      "The Amani Trolls have been harassing Silvermoon since its founding, but their defensive position within their jungle has made the prospect of an all-out assault too costly. Today, however, the Amani begins their largest siege yet. They leave us no choice; we must scour Zul'aman if the High Elves are to prosper.",
      @"ReplaceableTextures\CommandButtons\BTNForestTrollTrapper.blp")
    {
      _elvenRunestone = elvenRunestone;
      _silvermoon = silvermoon;
      _sunwell = sunwell;
      AddObjective(new ObjectiveUnitIsDead(preplacedUnitSystem.GetUnit(Constants.UNIT_O06Z_ZUL_JIN_CREEP_ZUL_AMAN)));
      AddObjective(new ObjectiveControlPoint(ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N01V_ZUL_AMAN)));
      AddObjective(new ObjectiveControlPoint(ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N01L_EVERSONG_WOODS)));
      AddObjective(new ObjectiveUpgrade(Constants.UNIT_H03T_PALACE_QUEL_THALAS_T3, Constants.UNIT_H033_STEADING_QUEL_THALAS_T1));
      AddObjective(new ObjectiveExpire(660, Title));
      AddObjective(new ObjectiveSelfExists());
      ResearchId = Constants.UPGRADE_R02U_QUEST_COMPLETED_THE_SIEGE_OF_SILVERMOON;
      _rescueUnits = rescueRect.PrepareUnitsForRescue(RescuePreparationMode.HideNonStructures);
      
      ResearchId = Constants.UPGRADE_R02U_QUEST_COMPLETED_THE_SIEGE_OF_SILVERMOON;
    }

    /// <inheritdoc />
    public override string RewardFlavour =>
      "The Amani trolls have been eliminated, settling a racial feud that had persisted for millenia.";

    /// <inheritdoc />
    protected override string RewardDescription =>
      "Control of all units in Silvermoon, unlock the Summon Mystic Defenders ability from Elven Runestones and enable Anasterian to be trained at the Altar";

    /// <inheritdoc />

    protected override void OnFail(Faction completingFaction)
    {
      var rescuer = completingFaction.ScoreStatus == ScoreStatus.Defeated
        ? Player(PLAYER_NEUTRAL_AGGRESSIVE)
        : completingFaction.Player;

      rescuer.RescueGroup(_rescueUnits);
      if (UnitAlive(_elvenRunestone))
        _silvermoon.Unit?.SetInvulnerable(true);
      _sunwell.Unit?.SetInvulnerable(true);
    }

    /// <inheritdoc />
    protected override void OnComplete(Faction completingFaction)
    {
      completingFaction.Player
        .RescueGroup(_rescueUnits)
        .PlayMusicThematic("war3mapImported\\SilvermoonTheme.mp3");
      
      if (UnitAlive(_elvenRunestone))
        _silvermoon.Unit?.SetInvulnerable(true);
      _sunwell.Unit?.SetInvulnerable(true);
    }
  }
}
