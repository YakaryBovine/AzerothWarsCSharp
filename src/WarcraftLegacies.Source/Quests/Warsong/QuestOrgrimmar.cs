using System.Collections.Generic;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.ObjectiveSystem.Objectives.FactionBased;
using MacroTools.ObjectiveSystem.Objectives.TimeBased;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using MacroTools.QuestSystem;
using WCSharp.Shared.Data;
using static War3Api.Common;


namespace WarcraftLegacies.Source.Quests.Warsong
{

  public sealed class QuestOrgrimmar : QuestData
  {
    private readonly List<unit> _rescueUnits;
    private const int RequiredResearchId = Constants.UPGRADE_R05O_FORTIFIED_HULLS_WARSONG;

    public QuestOrgrimmar(Rectangle rescueRect) : base("To Tame a Land",
      "This new continent is ripe for the taking. If the Horde is to survive, a new city needs to be built.",
      @"ReplaceableTextures\CommandButtons\BTNFortress.blp")
    {
      AddObjective(new ObjectiveResearch(RequiredResearchId, Constants.UNIT_O02S_FORTRESS_WARSONG_T3));
      AddObjective(new ObjectiveExpire(800, Title));
      AddObjective(new ObjectiveSelfExists());
      ResearchId = Constants.UPGRADE_R05R_QUEST_COMPLETED_TO_TAME_A_LAND;
      
      _rescueUnits = rescueRect.PrepareUnitsForRescue(RescuePreparationMode.HideAll);
    }

    /// <inheritdoc/>
    public override string RewardFlavour => "The city of Orgrimmar was finally constructed by the Warsong engineers, it is now a home for the new Horde and a symbol of power and innovation";

    /// <inheritdoc/>
    protected override string RewardDescription => "Control of all units in Orgrimmar, able to train Varok and Azerite Siege Engines";

    /// <inheritdoc/>
    protected override void OnComplete(Faction completingFaction)
    {
      foreach (var unit in _rescueUnits) 
        unit.Rescue(completingFaction.Player);

      completingFaction.Player?.PlayMusicThematic("war3mapImported\\OrgrimmarTheme.mp3");
    }

    /// <inheritdoc/>
    protected override void OnFail(Faction completingFaction)
    {
      var rescuer = completingFaction.ScoreStatus == ScoreStatus.Defeated
        ? Player(PLAYER_NEUTRAL_AGGRESSIVE)
        : completingFaction.Player;

      rescuer.RescueGroup(_rescueUnits);
    }

    /// <inheritdoc/>
    protected override void OnAdd(Faction whichFaction)
    {
      whichFaction.ModObjectLimit(RequiredResearchId, 1);
    }
  }
}