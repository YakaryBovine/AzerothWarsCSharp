using System.Collections.Generic;
using MacroTools.ControlPointSystem;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.ObjectiveSystem.Objectives.ControlPointBased;
using MacroTools.ObjectiveSystem.Objectives.FactionBased;
using MacroTools.ObjectiveSystem.Objectives.TimeBased;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using MacroTools.Powers;
using MacroTools.QuestSystem;
using WCSharp.Shared.Data;
using static War3Api.Common;


namespace WarcraftLegacies.Source.Quests.Stormwind
{
  /// <summary>
  /// Capture various control points and upgrade the main building to Tier 3 in order to gain control of Stormwind.
  /// </summary>
  public sealed class QuestStormwindCity : QuestData
  {
    private readonly List<unit> _rescueUnits;
    private const string RewardPowerName = "City of Heroes";

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestStormwindCity"/> class.
    /// </summary>
    /// <param name="rescueRect">Units in this area will be made invulnerable, then rescued when the quest is completed.</param>
    public QuestStormwindCity(Rectangle rescueRect) : base("Clear the Outskirts",
      "The outskirts of Stormwind are infested by rebels and foul creatures. Defeat them to regain control of your lands.",
      @"ReplaceableTextures\CommandButtons\BTNStormwindCastle.blp")
    {
      AddObjective(
        new ObjectiveControlPoint(
          ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N00V_DUSKWOOD)));
      AddObjective(
        new ObjectiveControlPoint(
          ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N00Z_ELWYNN_FOREST)));
      AddObjective(new ObjectiveControlPoint(
        ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N011_REDRIDGE_MOUNTAINS)));
      AddObjective(new ObjectiveUpgrade(Constants.UNIT_H06N_CASTLE_STORMWIND_T3,
        Constants.UNIT_H06K_TOWN_HALL_STORMWIND_T1));
      AddObjective(new ObjectiveExpire(600, Title));
      AddObjective(new ObjectiveSelfExists());
      _rescueUnits = rescueRect.PrepareUnitsForRescue(RescuePreparationMode.HideNonStructures);
      ResearchId = Constants.UPGRADE_R02S_QUEST_COMPLETED_CLEAR_THE_OUTSKIRTS;
      
    }

    //Todo: bad flavour
    /// <inheritdoc />
    public override string RewardFlavour =>
      "Stormwind has been liberated, and its grand army is now free to assist the Alliance.";

    /// <inheritdoc />
    protected override string RewardDescription =>
      "Gain control of all units in Stormwind, " +
      $"learn to train Varian from the {GetObjectName(Constants.UNIT_H06T_ALTAR_OF_KINGS_STORMWIND_ALTAR)}, " +
      $"learn to cast {GetObjectName(Constants.ABILITY_A0GD_SUMMON_GARRISON_STORMWIND)} from {GetObjectName(Constants.UNIT_H06M_KEEP_STORMWIND_T2)}s and {GetObjectName(Constants.UNIT_H06N_CASTLE_STORMWIND_T3)}s, " +
      $"and acquire the {RewardPowerName} Power";

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
      var rewardPower = new CityOfHeroes(0.125f, 1.5f, "Units")
      {
        Name = RewardPowerName,
        HeroGlowAbilityTypeId = Constants.ABILITY_A0GK_HERO_GLOW_ORIGIN,
        Filter = unit => !unit.IsType(UNIT_TYPE_MECHANICAL) && unit.GetTypeId() != Constants.UNIT_H05F_STORMWIND_CHAMPION_STORMWIND_ELITE,
      };
      
      completingFaction.AddPower(rewardPower);
      
      completingFaction.Player?
        .RescueGroup(_rescueUnits)
        .DisplayPowerAcquired(rewardPower)
        .PlayMusicThematic("war3mapImported\\StormwindTheme.mp3");
    }
  }
}