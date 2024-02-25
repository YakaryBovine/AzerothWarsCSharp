using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.ObjectiveSystem.Objectives.MetaBased;
using MacroTools.ObjectiveSystem.Objectives.QuestBased;
using MacroTools.QuestSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Scourge
{
  /// <summary>
  /// When Kel'thuzad dies, he becomes a ghost.
  /// </summary>
  public sealed class QuestKelthuzadDies : QuestData
  {
    private readonly LegendaryHero _kelthuzad;
    private readonly ObjectiveEitherOf _objectiveEitherOf;

    /// <inheritdoc />
    public override string RewardFlavour => 
      _objectiveEitherOf.ObjectiveA.Progress == QuestProgress.Complete
        ? "As foretold by the Lich King, Kel'thuzad has been slain. Unseen to his assailants, his spirit is carried away by the howling winds of Northrend and reconstituted at the base of Icecrown Citadel."
        : "In a rare twist of fate, the Lich King's prophecy did not come to pass: Kel'thuzad survived long enough to reach the Sunwell under his own power.";

    /// <inheritdoc />
    protected override string RewardDescription => "If Kel'thuzad dies, he revives in spectral form at Icecrown Citadel. Otherwise, he gains 4000 experience";

    /// <inheritdoc />
    public QuestKelthuzadDies(QuestData questKelthuzadLich, LegendaryHero kelthuzad) : base("Life Beyond Death",
      "The Lich King has foretold that the human necromancer Kel'thuzad will be slain by the enemies of the Scourge. Fortunately for him, he will live on in an ethereal form.",
      @"ReplaceableTextures\CommandButtons\BTNGhostOfKelThuzad.blp")
    {
      _kelthuzad = kelthuzad;
      _objectiveEitherOf = new ObjectiveEitherOf(
        new ObjectiveLegendDead(_kelthuzad),
        new ObjectiveQuestComplete(questKelthuzadLich));
      AddObjective(_objectiveEitherOf);
    }
    
    /// <inheritdoc />
    protected override void OnComplete(Faction whichFaction)
    {
      
      if (_objectiveEitherOf.ObjectiveA.Progress == QuestProgress.Complete)
      {
        _kelthuzad.UnitType = FourCC("U00M");
        _kelthuzad.PermaDies = false;
        _kelthuzad.ForceCreate(whichFaction.Player, Regions.FTSummon.Center,
          270);
      }
      else
      {
        _kelthuzad.Unit?.AddExperience(4000);
      }
    }
  }
}