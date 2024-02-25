using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.QuestSystem;
using System.Collections.Generic;
using MacroTools.ArtifactSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.ArtifactBased;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Frostwolf
{
  /// <summary>
  /// Warsong acquires the Scepter of the Queen and kills some Highborne.
  /// </summary>
  public sealed class QuestScepterOfTheQueenWarsong : QuestData
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="QuestScepterOfTheQueenWarsong"/> class.
    /// </summary>
    /// <param name="area">Units in this area will be made invulnerable, then made hostile when the quest is completed.</param>
    /// <param name="scepterOfTheQueen">Reward for completing the quest.</param>
    /// <param name="auberdine">Must be destroyed to complete the quest.</param>
    public QuestScepterOfTheQueenWarsong(Rectangle area, Artifact scepterOfTheQueen, Capital auberdine) : base("Royal Plunder",
      "Remnants of the ancient Highborne survive within the ruins of the Athenaeum. If Feathermoon Stronghold falls, it would become a simple matter to slaughter the Highborne and plunder their artifacts.",
      @"ReplaceableTextures\CommandButtons\BTNNagaWeaponUp2.blp")
    {
      _highBourneArea = area;
      _scepterOfTheQueen = scepterOfTheQueen;
      _highBourneAreaUnits = _highBourneArea.PrepareUnitsForRescue(RescuePreparationMode.HideNonStructures);
      AddObjective(new ObjectiveCapitalDead(auberdine));
      AddObjective(new ObjectiveHostilesInAreaAreDead(new[]{area}, "outside the Athenaeum"));
      _anyUnitInRect = new ObjectiveAnyUnitInRect(_highBourneArea, "Dire Maul", false);
      AddObjective(_anyUnitInRect);
      AddObjective(new ObjectiveNoOtherPlayerGetsArtifact(scepterOfTheQueen));
    }

    private readonly List<unit> _highBourneAreaUnits;
    private readonly Rectangle _highBourneArea;
    private readonly Artifact _scepterOfTheQueen;
    private readonly ObjectiveAnyUnitInRect _anyUnitInRect;

    /// <inheritdoc/>
    public override string RewardFlavour =>
      "The Highborne are no longer implicitly defended by the Night Elven presence at Feathermoon Stronghold. The Horde unleashes their full might against these Night Elven arcanists.";

    /// <inheritdoc/>
    protected override string RewardDescription =>
      $"Gain the Scepter of the Queen and reveal 4 hostile {GetObjectName(Constants.UNIT_NNMG_REDEEMED_HIGHBORNE_SENTINELS)} outside the Athenaeum";

    /// <inheritdoc/>
    protected override void OnComplete(Faction whichFaction)
    {
      _anyUnitInRect.CompletingUnit?.AddItemSafe(_scepterOfTheQueen.Item);
      Player(GetPlayerNeutralAggressive()).RescueGroup(_highBourneAreaUnits);
    }
  }
}