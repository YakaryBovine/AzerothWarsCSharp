using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.FactionBased;
using MacroTools.ObjectiveSystem.Objectives.TimeBased;
using MacroTools.QuestSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Druids
{
  public sealed class QuestTortolla : QuestData
  {
    private const int HeroId = Constants.UNIT_H04U_DEMIGOD_DRUIDS;
    private readonly unit _sleepingTortolla;

    public QuestTortolla(LegendaryHero tortolla) : base("The Turtle Demigod",
      "Tortolla was badly wounded during the War of the Ancients, and has been resting ever since.",
      @"ReplaceableTextures\CommandButtons\BTNSeaTurtleGreen.blp")
    {
      AddObjective(new ObjectiveTime(840));
      AddObjective(new ObjectiveSelfExists());
      ResearchId = Constants.UPGRADE_R049_QUEST_COMPLETED_THE_TURTLE_DEMIGOD;

      _sleepingTortolla = CreateUnit(Player(PLAYER_NEUTRAL_PASSIVE), HeroId, -11315, 9389, 333)
        .SetInvulnerable(true)
        .AddExperience(tortolla.StartingXp);
      AddSpecialEffectTarget(@"Abilities\Spells\Undead\Sleep\SleepTarget.mdl", _sleepingTortolla,
        "overhead");
    }

    /// <inheritdoc/>
    public override string RewardFlavour => "Tortolla has finally awoken from his ancient slumber.";

    /// <inheritdoc/>
    protected override string RewardDescription => "You can summon Tortolla from the Altar of Elders";

    /// <inheritdoc/>
    protected override void OnComplete(Faction completingFaction)
    {
      RemoveUnit(_sleepingTortolla);
    }

    /// <inheritdoc/>
    protected override void OnFail(Faction completingFaction)
    {
      RemoveUnit(_sleepingTortolla);
    }
  }
}