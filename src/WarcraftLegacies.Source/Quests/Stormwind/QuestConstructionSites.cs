using System.Collections.Generic;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.ObjectiveSystem.Objectives.TimeBased;
using MacroTools.QuestSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Stormwind
{
  /// <summary>
  /// After some time, Stormwind can upgrade their Construction Sites.
  /// </summary>
  public sealed class QuestConstructionSites : QuestData
  {
    private readonly IEnumerable<unit> _constructionSites;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestConstructionSites"/> class.
    /// </summary>
    /// <param name="constructionSites">The construction sites that should get pinged when the quest is completed.</param>
    public QuestConstructionSites(IEnumerable<unit> constructionSites) : base("Inevitable Progress",
      "Stormwind has not yet fully recovered from the ravaging it experienced during the Second War. Await reconstruction.",
      @"ReplaceableTextures\CommandButtons\BTNGenericHumanBuilding.blp")
    {
      _constructionSites = constructionSites;
      ResearchId = Constants.UPGRADE_R022_QUEST_COMPLETED_INEVITABLE_PROGRESS_STORMWIND;
      AddObjective(new ObjectiveTime(360));
    }

    /// <inheritdoc />
    public override string RewardFlavour => "Stormwind's Construction Sites are now ready to be upgraded.";

    /// <inheritdoc />
    protected override string RewardDescription => "Your Construction Sites can be upgraded";
    
    /// <inheritdoc />
    protected override void OnComplete(Faction completingFaction)
    {
      if (completingFaction.Player == null) 
        return;
      
      foreach (var constructionSite in _constructionSites)
      {
        var position = constructionSite.GetPosition();
        completingFaction.Player.PingMinimapSimple(position.X, position.Y, 5);
      }
    }
  }
}