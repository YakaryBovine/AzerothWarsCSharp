using System.Collections.Generic;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.ObjectiveSystem.Objectives.QuestBased;
using MacroTools.QuestSystem;
using WarcraftLegacies.Source.Quests.Scourge;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Legion
{
  public sealed class QuestCunningPlan : QuestData
  {
    private readonly List<unit> _rescueUnits;
    
    public QuestCunningPlan(Rectangle rescueRect, Faction scourge) : base("A Cunning Plan",
      "The Dreadlords have played a subtle hand in preparing Lordaeron for the coming of the Scourge. Once the Plague is unleashed, the Dreadlords will activate their own assets.",
      @"ReplaceableTextures\CommandButtons\BTNHeroDreadlord.blp")
    {
      AddObjective(new ObjectiveFactionQuestComplete(scourge.GetQuestByType<QuestPlague>(), scourge));

      _rescueUnits = rescueRect.PrepareUnitsForRescue(RescuePreparationMode.HideAll);
      
      ResearchId = Constants.UPGRADE_R01L_QUEST_COMPLETED_A_CUNNING_PLAN;
    }

    /// <inheritdoc/>
    protected override string RewardDescription =>
      $"Gain control of a small base in Alterac, learn to generate a portal to Alterac using the Argus Teleporter, and gain a {GetObjectName(Constants.UNIT_U005_DREAD_SHRINE_LEGION_SPECIAL)} in each of the following Scourge bases: Deathknell, Stratholme Coast, and Scholomance";

    /// <inheritdoc/>
    public override string RewardFlavour =>
      "With the Plague now spreading amongst Lordaeron's populace, the Dreadlords set the second half of their plan in motion: a direct demonic incursion into the Eastern Kingdoms.";

    /// <inheritdoc/>
    protected override void OnComplete(Faction completingFaction)
    {
      completingFaction.Player.RescueGroup(_rescueUnits);
      CreateUnit(completingFaction.Player, Constants.UNIT_U005_DREAD_SHRINE_LEGION_SPECIAL, 11138, 12802, 0);
      CreateUnit(completingFaction.Player, Constants.UNIT_U005_DREAD_SHRINE_LEGION_SPECIAL, 4860, 9277, 0);
      CreateUnit(completingFaction.Player, Constants.UNIT_U005_DREAD_SHRINE_LEGION_SPECIAL, 14725, 7356, 0);
    }
  }
}