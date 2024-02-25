using System.Collections.Generic;
using System.Linq;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.FactionBased;
using MacroTools.QuestSystem;
using WCSharp.Shared.Data;
using static War3Api.Common;
using static War3Api.Blizzard;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;

namespace WarcraftLegacies.Source.Quests.Dalaran
{
  /// <summary>
  /// Theramore starts neutral and must be discovered.
  /// </summary>
  public sealed class QuestTheramore : QuestData
  {
    private readonly List<unit> _rescueUnits;
    private const int RequiredResearchId = Constants.UPGRADE_R0A7_ESCAPE_TO_THERAMORE_DALARAN;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestTheramore"/> class.
    /// </summary>
    public QuestTheramore(LegendaryHero jaina, Capital violetCitadel, Rectangle theramoreRect) : base("Theramore",
      "The distant lands of Kalimdor remain untouched by human civilization. If the Third War proceeds poorly, it may become necessary to abandon Dalaran and establish a refuge there.",
      @"ReplaceableTextures\CommandButtons\BTNOldRock.blp")
    {
      AddObjective(new ObjectiveControlLegend(jaina, false)
      {
        ResearchId = Constants.UPGRADE_R0A8_YOUR_TEAM_CONTROLS_JAINA_PROUDMOORE
      });
      AddObjective(new ObjectiveResearch(RequiredResearchId, Constants.UNIT_H002_THE_VIOLET_CITADEL_DALARAN_OTHER));
      AddObjective(new ObjectiveUnitAlive(violetCitadel.Unit));
      AddObjective(new ObjectiveSelfExists());
      _rescueUnits = theramoreRect.PrepareUnitsForRescue(RescuePreparationMode.HideNonStructures);
    }

    /// <inheritdoc />
    public override string RewardFlavour =>
      "Jaina Proudmoore abandons the once mighty city of Dalaran and leads her people across the sea, arriving in the untamed lands of Kalimdor.";

    /// <inheritdoc />
    protected override string RewardDescription =>
      "Gain control of all units at Theramore and teleport all of your units within Dalaran to Theramore. Dalaran becomes hostile";

    /// <inheritdoc />
    public override string PenaltyFlavour =>
      "Dalaran has fallen. Those who managed to survive its destruction travel west, to the distant lands of Kalimdor. They hope that this new world will treat them more kindly than the one they left behind.";

    /// <inheritdoc />
    protected override string PenaltyDescription =>
      $"Gain control of all units at Theramore";

    /// <inheritdoc />
    protected override void OnComplete(Faction completingFaction)
    {
      if (completingFaction.Player != null)
        completingFaction.Player.RescueGroup(_rescueUnits);
      else
        Player(bj_PLAYER_NEUTRAL_VICTIM).RescueGroup(_rescueUnits);

      foreach (var unit in CreateGroup().EnumUnitsInRect(Regions.Dalaran).EmptyToList().Where(x =>
                 x.OwningPlayer() == completingFaction.Player && !IsUnitType(x, UNIT_TYPE_STRUCTURE)).ToList())
        unit.SetPosition(Regions.Theramore.Center);

      foreach (var unit in CreateGroup().EnumUnitsInRect(Regions.Dalaran).EmptyToList().Where(x =>
                 x.OwningPlayer() == completingFaction.Player && IsUnitType(x, UNIT_TYPE_STRUCTURE)).ToList())
        unit.SetOwner(Player(PLAYER_NEUTRAL_AGGRESSIVE));
    }

    /// <inheritdoc />
    protected override void OnFail(Faction completingFaction)
    {
      var rescuer = completingFaction.ScoreStatus == ScoreStatus.Defeated
        ? Player(PLAYER_NEUTRAL_AGGRESSIVE)
        : completingFaction.Player;

      rescuer.RescueGroup(_rescueUnits);
    }

    /// <inheritdoc/>
    protected override void OnAdd(Faction whichFaction) => whichFaction.ModObjectLimit(RequiredResearchId, 1);
  }
}