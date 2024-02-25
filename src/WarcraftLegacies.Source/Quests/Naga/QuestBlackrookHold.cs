using MacroTools.ControlPointSystem;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.ControlPointBased;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using MacroTools.QuestSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Naga
{
  /// <summary>
  /// Capture the entire Broken Isles to get Blackrook Hold.
  /// </summary>
  public sealed class QuestBlackrookHold : QuestData
  {
    private readonly Capital _blackrookHold;

    /// <inheritdoc />
    public QuestBlackrookHold(Capital blackrookHold) : base("Blackrook Hold",
      "Blackrook Hold once stood as a bulwark against the Burning Legion during the War of the Ancients. That it survived the Great Sundering is an extraordinary testatement to its construction; if it were to be secured, it would offer dominion over the entire Broken Isles.",
      BlzGetAbilityIcon(blackrookHold.UnitType))
    {
      _blackrookHold = blackrookHold;
      AddObjective(new ObjectiveHostilesInAreaAreDead(new[]
      {
        Regions.BrokenIslesA,
        Regions.BrokenIslesB
      }, "on the Broken Isles"));
      AddObjective(new NoOtherPlayerGetsCapital(blackrookHold));
      AddObjective(new ObjectiveControlPoint(ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N053_VAL_SHARAH)));
    }

    /// <inheritdoc />
    public override string RewardFlavour =>
      "The Broken Isles have been secured, and Black Rook Hold's garrison has been re-established.";

    /// <inheritdoc />
    protected override string RewardDescription =>
      "You gain control of Blackrook Hold";

    /// <inheritdoc />
    protected override void OnComplete(Faction whichFaction)
    {
      _blackrookHold.Unit?.Rescue(whichFaction.Player);
    }

    /// <inheritdoc />
    protected override void OnAdd(Faction whichFaction)
    {
      _blackrookHold.Unit?.SetInvulnerable(true);
    }
  }
}