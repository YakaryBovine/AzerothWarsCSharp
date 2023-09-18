using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.QuestSystem;

namespace WarcraftLegacies.Source.Quests.Legion
{
  public sealed class QuestKillCapitalPalace : QuestData
  {
   
    public QuestKillCapitalPalace(Capital CapitalPalace) : base("Unleash the Swarm",
      "Unlock your Dreadlords' full potential. Weaken the pitiful human resistance by destroying their beloved capital!",
      @"ReplaceableTextures\CommandButtons\BTNCarrionSwarm.blp")
    {
      AddObjective(new ObjectiveCapitalDead(CapitalPalace));
      ResearchId = Constants.UPGRADE_R09T_QUEST_COMPLETED_UNLEASH_THE_SWARM;
    }
    
    /// <inheritdoc/>
    protected override string RewardFlavour =>
      "The precious human capital has crumbled, and your Dreadlords feel their strength growing as the fate of the humanity is all but sealed.";

    /// <inheritdoc/>
    protected override string RewardDescription => "Gain the ability to research Greater Carrion Swarm from Black Citadels and capitals.";
    /// <inheritdoc/>
    protected override void OnComplete(Faction whichFaction)
    {


    }
  }
}