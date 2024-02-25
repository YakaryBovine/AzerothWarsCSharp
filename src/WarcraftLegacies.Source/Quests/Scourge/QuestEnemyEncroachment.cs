using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using MacroTools.QuestSystem;

namespace WarcraftLegacies.Source.Quests.Scourge
{
  public sealed class QuestEnemyEncroachment : QuestData
  {
    
    /// <inheritdoc />
    public QuestEnemyEncroachment() : base("The Slumbering King", "Ner'zhul commands the undead hordes from his throne atop Icecrown, waiting patiently for the inevitable day that interlopers will come to invade his frozen lands.", @"ReplaceableTextures\CommandButtons\BTNAnimateDead.blp")
    {
      AddObjective(new ObjectiveAnyEnemyUnitInRects(new[]
      {
        Regions.Storm_Peaks,
        Regions.Central_Northrend,
        Regions.The_Basin,
        Regions.Ice_Crown,
        Regions.Fjord,
        Regions.Eastern_Northrend,
        Regions.Far_Eastern_Northrend,
        Regions.Coldarra,
        Regions.Borean_Tundra,
        Regions.IcecrownShipyard
      }, "Northrend"));
      ResearchId = Constants.UPGRADE_R04V_QUEST_COMPLETED_THE_SLUMBERING_KING;
    }
    
    /// <inheritdoc/>
    public override string RewardFlavour => "For the first time in years, outlanders have set foot on the shores of Northrend. Soon they will feel the biting chill of death.";

    /// <inheritdoc/>
    protected override string RewardDescription => "Learn to cast Frost Nova and Animate Dead from the Frozen Throne";
  }
}