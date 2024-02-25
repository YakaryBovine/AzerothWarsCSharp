using System.Collections.Generic;
using MacroTools.ArtifactSystem;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.ArtifactBased;
using MacroTools.ObjectiveSystem.Objectives.FactionBased;
using MacroTools.ObjectiveSystem.Objectives.TimeBased;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using MacroTools.QuestSystem;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Druids
{
  /// <summary>
  /// The Druids acquire Malfurion, Moonglade and the World Tree.
  /// </summary>
  public sealed class QuestMalfurionAwakens : QuestData
  {
    private readonly unit _worldTree;
    private readonly Artifact _hornofCenarius;
    private readonly LegendaryHero _malfurion;
    private readonly List<unit> _moongladeUnits;
    private readonly List<unit> _darnassusUnits;
    private readonly List<unit> _cenarionHoldUnits;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestMalfurionAwakens"/> class.
    /// </summary>
    /// <param name="moonglade">All units here start invulnerable and are rescued when the quest is completed.</param>
    /// <param name="darnassus"></param>
    /// <param name="cenarionHold"></param>
    /// <param name="worldTree">Starts invulnerable and is recued when the quest is completed.</param>
    /// <param name="hornofCenarius">Required to complete the quest.</param>
    /// <param name="malfurion">Awakened when the quest is completed.</param>
    public QuestMalfurionAwakens(Rectangle moonglade, Rectangle darnassus, Rectangle cenarionHold, unit worldTree, Artifact hornofCenarius, LegendaryHero malfurion) : base("Awakening of Stormrage",
      "Ever since the War of the Ancients ten thousand years ago, Malfurion Stormrage and his druids have slumbered within the Barrow Den. Now, their help is required once again.",
      @"ReplaceableTextures\CommandButtons\BTNFurion.blp")
    {
      _worldTree = worldTree;
      _hornofCenarius = hornofCenarius;
      _malfurion = malfurion;
      AddObjective(new ObjectiveAcquireArtifact(hornofCenarius));
      AddObjective(new ObjectiveArtifactInRect(hornofCenarius, Regions.Moonglade,
        "The Barrow Den"));
      AddObjective(new ObjectiveExpire(480, Title));
      AddObjective(new ObjectiveUpgrade(Constants.UNIT_ETOE_TREE_OF_ETERNITY_DRUID_T3, Constants.UNIT_ETOL_TREE_OF_LIFE_DRUID_T1));
      AddObjective(new ObjectiveSelfExists());
      _moongladeUnits = moonglade.PrepareUnitsForRescue(RescuePreparationMode.HideNonStructures);
      _darnassusUnits = darnassus.PrepareUnitsForRescue(RescuePreparationMode.HideNonStructures);
      _cenarionHoldUnits = cenarionHold.PrepareUnitsForRescue(RescuePreparationMode.HideNonStructures);
      worldTree.SetInvulnerable(true);
      
    }

    /// <inheritdoc />
    public override string RewardFlavour => "Malfurion has emerged from his deep slumber in the Barrow Den. Darnassus and the Moonglade ancients have been awakened.";

    /// <inheritdoc />
    protected override string RewardDescription => "Gain Nordrassil, the Darnassus base, the Cenarion Hold base, the Moonglade base, the hero Malfurion, and the artifact G'hanir";

    /// <inheritdoc />
    protected override void OnFail(Faction completingFaction)
    {
      _worldTree.Rescue(completingFaction.Player);
      var rescuer = completingFaction.ScoreStatus == ScoreStatus.Defeated
        ? Player(PLAYER_NEUTRAL_AGGRESSIVE)
        : completingFaction.Player;

      rescuer.RescueGroup(_moongladeUnits);
      rescuer.RescueGroup(_darnassusUnits);
      rescuer.RescueGroup(_cenarionHoldUnits);
    }

    /// <inheritdoc />
    protected override void OnComplete(Faction completingFaction)
    {
      completingFaction.Player.RescueGroup(_moongladeUnits);
      completingFaction.Player.RescueGroup(_darnassusUnits);
      completingFaction.Player.RescueGroup(_cenarionHoldUnits);
      _worldTree.Rescue(completingFaction.Player);
      if (_malfurion.Unit == null)
      {
        _malfurion.ForceCreate(completingFaction.Player, Regions.Moonglade.Center,
          270);
        _malfurion.Unit?
          .SetLevel(3, false)
          .AddItemSafe(_hornofCenarius.Item);
      }
      else
      {
        _hornofCenarius.Item.SetPositionSafe(GetTriggerUnit().GetPosition());
      }
    }
  }
}