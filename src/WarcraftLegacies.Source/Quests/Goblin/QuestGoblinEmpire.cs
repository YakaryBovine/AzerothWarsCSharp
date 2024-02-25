using MacroTools.ControlPointSystem;
using MacroTools.ObjectiveSystem.Objectives.ControlPointBased;
using MacroTools.QuestSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Goblin
{
  public sealed class QuestGoblinEmpire : QuestData
  {
    public QuestGoblinEmpire() : base("Goblin Empire",
      "All the Goblin syndicate's towns must be reunited under one banner.",
      @"ReplaceableTextures\CommandButtons\BTNGoblinWarZeppelin.blp")
    {
      AddObjective(new ObjectiveControlPoint(ControlPointManager.Instance.GetFromUnitType(FourCC("n01X"))));
      AddObjective(new ObjectiveControlPoint(ControlPointManager.Instance.GetFromUnitType(FourCC("n00L"))));
      AddObjective(new ObjectiveControlPoint(ControlPointManager.Instance.GetFromUnitType(FourCC("n07Y"))));
      AddObjective(new ObjectiveControlPoint(ControlPointManager.Instance.GetFromUnitType(FourCC("n01E"))));
      AddObjective(new ObjectiveControlPoint(ControlPointManager.Instance.GetFromUnitType(FourCC("n04Z"))));
      AddObjective(new ObjectiveControlPoint(ControlPointManager.Instance.GetFromUnitType(FourCC("n05C"))));
      AddObjective(new ObjectiveControlPoint(ControlPointManager.Instance.GetFromUnitType(FourCC("n0A6"))));
      ResearchId = FourCC("R07F");
    }

    /// <inheritdoc/>
    public override string RewardFlavour => "With all the Goblin towns united, a new empire rises!";

    /// <inheritdoc/>
    protected override string RewardDescription => "Unlock the Intercontinental Artillery";
  }
}