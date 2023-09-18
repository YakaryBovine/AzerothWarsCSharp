using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.QuestSystem;

namespace WarcraftLegacies.Source.Quests.Legion
{
  public sealed class QuestUnlockAstral : QuestData
  {
   
    public QuestUnlockAstral(Capital caerDarrow) : base("Secrets of the Ethereal Realm",
      "Once we have established a foothold in this puny mortal plane we can reveal our true power over the dimensions.",
      @"ReplaceableTextures\CommandButtons\BTNSpell_Nature_AstralRecal.blp")

    {
      AddObjective(new ObjectiveControlCapital(caerDarrow, false));
      ResearchId = Constants.UPGRADE_R09S_QUEST_COMPLETE_SECRETS_OF_THE_ETHEREAL_REALM;
     
    }
    
    /// <inheritdoc/>
    protected override string RewardFlavour => "Resistance is futile, this land will fall to the might of the Burning Legion. ";

    /// <inheritdoc/>
    protected override string RewardDescription => "Your power grows. Dreadlords and Infiltrators can now unlock the ability to teleport.";
      
    /// <inheritdoc/>
    protected override void OnComplete(Faction whichFaction)
    {
      

    }
  }
}