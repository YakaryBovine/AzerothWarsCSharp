using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.QuestSystem;
using static War3Api.Common;
using WarcraftLegacies.Source.Powers;
using WarcraftLegacies.Source.Setup.FactionSetup;
using WCSharp.Shared.Data;

namespace WarcraftLegacies.Source.Quests.Scourge
{
  public sealed class QuestDestroyStratholme : QuestData
  {
    private readonly LegendaryHero _arthas;
    
    public QuestDestroyStratholme(Capital stratholme, LegendaryHero arthas) : base("The Culling", "When the city of Stratholme falls, Prince Arthas' despair will make him more susceptible to the power of the Lich King.", @"ReplaceableTextures\CommandButtons\BTNRuneblade.blp")
    {
      _arthas = arthas;
      AddObjective(new ObjectiveCapitalDead(stratholme));
      Required = true;
    }

    private static bool IsPointValidForArthas(Point whichPoint) =>
      !whichPoint.IsPathable(PATHING_TYPE_FLOATABILITY) && whichPoint.IsPathable(PATHING_TYPE_WALKABILITY);

    /// <inheritdoc />
    protected override string RewardFlavour => "Prince Arthas could not protect the people of Stratholme. The Lich King's hold over him grows stronger.";

    /// <inheritdoc />
    protected override string RewardDescription => "Gain the power Eye of the Lich King, which allows you to identify where Arthas is at any time";

    /// <inheritdoc />
    protected override void OnComplete(Faction completingFaction)
    {
      Point randomPoint;
      do
      {
        randomPoint = Regions.ArthasRandomPoint.GetRandomPoint();
      } while (!IsPointValidForArthas(randomPoint));

      _arthas.ForceCreate(LordaeronSetup.Lordaeron?.Player ?? Player(PLAYER_NEUTRAL_AGGRESSIVE), randomPoint, 270);
      
      completingFaction.AddPower(new PingPower(_arthas, "Eye of the Lich King", 5, 5));
    }
  }
}