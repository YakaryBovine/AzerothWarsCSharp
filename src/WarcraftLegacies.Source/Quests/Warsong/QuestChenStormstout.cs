using MacroTools.FactionSystem;
using MacroTools.ObjectiveSystem.Objectives.FactionBased;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using MacroTools.QuestSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Warsong
{
  /// <summary>
  /// Chen Stormstout becomes available for training at the Warsong Altar.
  /// </summary>
  public sealed class QuestChenStormstout : QuestData
  {
    private readonly int _chenResearch = FourCC("R037");
    private readonly unit _chen;
    
    public QuestChenStormstout(unit chen) : base("The Wandering Brewmaster",
      "Rumours tell of a strange white-furred creature from a foreign land. Perhaps it could be convinced to join the Horde.",
      @"ReplaceableTextures\CommandButtons\BTNPandarenBrewmaster.blp")
    {
      AddObjective(new ObjectiveAnyUnitInRect(Regions.Chen, "Chen Stormstout", false));
      AddObjective(new ObjectiveSelfExists());
      _chen = chen;
      
    }

    /// <inheritdoc/>
    public override string RewardFlavour => "Chen Stormstout has joined the Warsong as a mercenary!";

    /// <inheritdoc/>
    protected override string RewardDescription => "The hero Chen Stormstout is now trainable at the Altar";

    /// <inheritdoc/>
    protected override void OnFail(Faction completingFaction)
    {
      RemoveUnit(_chen);
    }

    /// <inheritdoc/>
    protected override void OnComplete(Faction completingFaction)
    {
      RemoveUnit(_chen);
      SetPlayerTechResearched(completingFaction.Player, _chenResearch, 1);
    }

    /// <inheritdoc/>
    protected override void OnAdd(Faction whichFaction)
    {
      whichFaction.ModObjectLimit(_chenResearch, Faction.UNLIMITED);
      SetUnitInvulnerable(_chen, true);
    }
  }
}