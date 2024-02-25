using System.Collections.Generic;
using MacroTools;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.ObjectiveSystem.Objectives.FactionBased;
using MacroTools.ObjectiveSystem.Objectives.TimeBased;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using MacroTools.QuestSystem;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Ironforge
{
  /// <summary>
  /// Kill a specific to unlock Gnomeregan
  /// </summary>
  public sealed class QuestGnomeregan : QuestData
  {
    private static readonly int QuestResearchId = FourCC("R05Q");
    private readonly List<unit> _rescueUnits;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestGnomeregan"/> class.
    /// </summary>
    /// <param name="rescueRect"></param>
    /// <param name="preplacedUnitSystem"></param>
    public QuestGnomeregan(Rectangle rescueRect, PreplacedUnitSystem preplacedUnitSystem) : base("The City of Invention",
      "The people of Gnomeregan have long been unable to assist the Alliance in its wars due an infestation of troggs and Ice Trolls. Resolve their conflicts for them to gain their services.",
      @"ReplaceableTextures\CommandButtons\BTNFlyingMachine.blp")
    {
      AddObjective(new ObjectiveUnitIsDead(preplacedUnitSystem.GetUnit(FourCC("nitw"), Regions.Gnomergan.Center))); //Ice Troll Warlord
      AddObjective(new ObjectiveExpire(480, Title));
      AddObjective(new ObjectiveSelfExists());
      _rescueUnits = rescueRect.PrepareUnitsForRescue(RescuePreparationMode.HideNonStructures);
      
    }

    /// <inheritdoc/>
    public override string RewardFlavour =>
      "Gnomeregan has been literated, and its military is now free to assist Ironforge.";

    /// <inheritdoc/>
    protected override string RewardDescription => "Control of all units in Gnomeregan";

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
      SetPlayerTechResearched(completingFaction.Player, FourCC("R05Q"), 1);
      completingFaction.Player.RescueGroup(_rescueUnits);
    }

    protected override void OnAdd(Faction whichFaction)
    {
      whichFaction.ModObjectLimit(QuestResearchId, 1);
    }
  }
}