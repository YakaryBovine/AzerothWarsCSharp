using System.Collections.Generic;
using System.Linq;
using MacroTools.ArtifactSystem;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.ObjectiveSystem.Objectives.ArtifactBased;
using MacroTools.QuestSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests
{
  /// <summary>
  /// Acquire Fragments to assemble Zin'rokh.
  /// </summary>
  public sealed class QuestZinrokhAssembly : QuestData
  {
    private readonly List<Artifact> _fragments;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestZinrokhAssembly"/> class.
    /// </summary>
    public QuestZinrokhAssembly(List<Artifact> fragments) : base("Destroyer of Worlds", "When Hakkar the Soulflayer was defeated long ago, Zin'rokh was shattered and spread throughout the Troll tribes. The legendary blade could be reforged if its pieces could be unified once more.", @"ReplaceableTextures\CommandButtons\BTNHSZin'Rokhv1.blp")
    {
      _fragments = fragments;
      foreach (var artifact in fragments) 
        AddObjective(new ObjectiveAcquireArtifact(artifact));
      IsFactionQuest = false;
    }

    /// <inheritdoc/>
    public override string RewardFlavour =>
      $"{_fragments.First().OwningUnit?.GetProperName() ?? ""} has assembled Zin'rokh, Destroyer of Worlds!";
    
    /// <inheritdoc/>
    public override string PenaltyFlavour =>
      $"{_fragments.First().OwningPlayer?.GetFaction()?.ColoredName ?? ""} has assembled Zin'rokh, Destroyer of Worlds. The only way we will acquire it now is if we take it from them.";
    
    /// <inheritdoc/>
    protected override string RewardDescription => "Reforge Zin'rokh, Destroyer of Worlds from its Shards";
    
    /// <inheritdoc/>
    protected override void OnComplete(Faction completingFaction)
    {
      var azureFragmentHolder = _fragments.First().OwningUnit;
      foreach (var artifact in _fragments)
        ArtifactManager.Destroy(artifact);
      var zinrokh = new Artifact(CreateItem(Constants.ITEM_I016_ZIN_ROKH_DESTROYER_OF_WORLDS, 0, 0))
      {
        TitanforgedAbility = Constants.ABILITY_A0VM_TITANFORGED_9_STRENGTH
      };
      ArtifactManager.Register(zinrokh);
      azureFragmentHolder?.AddItemSafe(zinrokh.Item);
    }
  }
}