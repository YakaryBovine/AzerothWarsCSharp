using System.Collections.Generic;
using MacroTools;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.FactionBased;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.ObjectiveSystem.Objectives.QuestBased;
using MacroTools.ObjectiveSystem.Objectives.TimeBased;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using MacroTools.Powers;
using MacroTools.QuestSystem;
using WarcraftLegacies.Source.Setup.Legends;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Lordaeron
{
  /// <summary>
  /// Capture <see cref="LegendNeutral.Caerdarrow"/> to unlock the capital city of Lordaeron.
  /// </summary>
  public sealed class QuestCapitalCity : QuestData
  {
    private readonly List<unit> _rescueUnits;
    private readonly unit _terenas;
    private readonly LegendaryHero _uther;
    private readonly Capital _capitalPalace;
    private const string RewardPowerName = "Dominion";

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestCapitalCity"/> class.
    /// </summary>
    public QuestCapitalCity(PreplacedUnitSystem preplacedUnitSystem, Rectangle rescueRect, unit terenas, LegendaryHero uther,
      LegendaryHero arthas, Capital caerDarrow, Capital capitalPalace, IEnumerable<QuestData> prequisites) :
      base("Hearthlands",
        "The territories of Lordaeron are fragmented. Regain control of the old Alliance's hold to secure the kingdom.",
        @"ReplaceableTextures\CommandButtons\BTNCastle.blp")
    {
      AddObjective(new ObjectiveUnitIsDead(preplacedUnitSystem.GetUnit(Constants.UNIT_N0AG_LORD_BAROV)));
      foreach (var prequisite in prequisites)
        AddObjective(new ObjectiveQuestComplete(prequisite));
      AddObjective(new ObjectiveControlLegend(arthas, false));
      AddObjective(new ObjectiveExpire(660, Title));
      AddObjective(new ObjectiveSelfExists());
      ResearchId = Constants.UPGRADE_R04Y_QUEST_COMPLETED_HEARTHLANDS;
      _terenas = terenas;
      _uther = uther;
      _capitalPalace = capitalPalace;
      _rescueUnits = rescueRect.PrepareUnitsForRescue(RescuePreparationMode.HideNonStructures, RescueUnitFilter);
      
    }

    private static bool RescueUnitFilter(unit whichUnit) => GetUnitTypeId(whichUnit) != Constants.UNIT_N08F_UNDERCITY_ENTRANCE;

    /// <inheritdoc/>
    public override string RewardFlavour =>
      "The Capital City of Lordaeron has joined Arthas.";

    /// <inheritdoc/>
    protected override string RewardDescription => $"Gain control of all units in the Capital City, gain Uther, and acquire the {RewardPowerName} Power";

    /// <inheritdoc/>
    protected override void OnFail(Faction completingFaction)
    {
      var rescuer = completingFaction.ScoreStatus == ScoreStatus.Defeated
        ? Player(PLAYER_NEUTRAL_AGGRESSIVE)
        : completingFaction.Player;

      rescuer.RescueGroup(_rescueUnits);
      SetUnitInvulnerable(_terenas, true);
    }

    /// <inheritdoc/>
    protected override void OnComplete(Faction completingFaction)
    {
      var rewardPower = new ControlLevelPerTurnBonus(0.5f)
      {
        Name = RewardPowerName
      };
      
      completingFaction.AddPower(rewardPower);
      completingFaction.Player?.DisplayPowerAcquired(rewardPower);

      if (_uther.Unit == null)
      {
        _uther.ForceCreate(completingFaction.Player, Regions.King_Arthas_crown.Center,90);
        _uther.Unit?.SetLevel(5, false);
      }

      completingFaction.Player?
        .RescueGroup(_rescueUnits)
        .PlayMusicThematic("war3mapImported\\CapitalCity.mp3");
      
      SetUnitInvulnerable(_terenas, true);
      _uther.AddUnitDependency(_capitalPalace.Unit);
    }
  }
}