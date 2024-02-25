using MacroTools.ArtifactSystem;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem;
using MacroTools.ObjectiveSystem.Objectives.ArtifactBased;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using MacroTools.QuestSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests
{
  /// <summary>
  /// Any player approaches the Book of Medivh to acquire it.
  /// </summary>
  public sealed class QuestBookOfMedivh : QuestData
  {
    private static unit? _bookOfMedivhPedestal;
    
    private readonly IHasCompletingUnit _objectiveWithCompletingUnit;
    private readonly bool _bypassLevelRequirement;
    private readonly Artifact _bookOfMedivh;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestBookOfMedivh"/> class.
    /// </summary>
    /// <param name="protectingCapital">Must be destroyed for the quest to be completed.</param>
    /// <param name="bookLocation">Where to place the Book of Medivh pedestal.</param>
    /// <param name="bookOfMedivh">Reward for completing the quest.</param>
    /// <param name="bypassLevelRequirement">If set to true, any hero of any level can complete the objective.</param>
    /// <param name="bypassDestructionRequirement">If true, <paramref name="protectingCapital"/> does not need to be destroyed to complete the quest.</param>
    public QuestBookOfMedivh(Capital protectingCapital, NamedRectangle bookLocation, Artifact bookOfMedivh,
      bool bypassLevelRequirement, bool bypassDestructionRequirement) : base("Book of Medivh",
      $"The last remaining spellbook written by Medivh, the Last Guardian, is held securely within {bookLocation.Name}. The spells within its pages could bring us great power.",
      @"ReplaceableTextures\CommandButtons\BTNBookOfTheDead.blp")
    {
      _bypassLevelRequirement = bypassLevelRequirement;
      var bookLocationFullName = $"the Book of Medivh's pedestal at {bookLocation.Name}";
      _bookOfMedivh = bookOfMedivh;
       _objectiveWithCompletingUnit = bypassLevelRequirement
         ? new ObjectiveAnyUnitInRect(bookLocation.Rectangle, bookLocationFullName, true)
         : new ObjectiveHeroWithLevelInRect(12, bookLocation.Rectangle, bookLocationFullName);
      if (_objectiveWithCompletingUnit is Objective objective) 
        AddObjective(objective);
      
      AddObjective(new ObjectiveNoOtherPlayerGetsArtifact(bookOfMedivh));
      if (!bypassDestructionRequirement)
        AddObjective(new ObjectiveCapitalDead(protectingCapital));

      if (_bookOfMedivhPedestal == null)
      {
        _bookOfMedivhPedestal = CreateUnit(Player(PLAYER_NEUTRAL_PASSIVE), Constants.UNIT_NBSM_BOOK_OF_MEDIVH,
          bookLocation.Rectangle.Center.X, bookLocation.Rectangle.Center.Y, 270);
        _bookOfMedivhPedestal.SetInvulnerable(true)
          .AddAbility(Constants.ABILITY_A01Y_INVENTORY_DUMMY_DROP_ARTIFACT)
          .AddItemSafe(bookOfMedivh.Item);
      }
      
      IsFactionQuest = bypassLevelRequirement;
    }

    /// <inheritdoc/>
    protected override string RewardDescription => _bypassLevelRequirement
      ? "The Book of Medivh, which can be used to summon the full might of the Burning Legion"
      : "The Book of Medivh";

    /// <inheritdoc/>
    public override string RewardFlavour => _bypassLevelRequirement
      ? $"{_objectiveWithCompletingUnit.CompletingUnitName} has retrieved the Book of Medivh from its pedestal. With its power, we can summon the full might of the Burning Legion from the depths of the Twisting Nether."
      : $"{_objectiveWithCompletingUnit.CompletingUnitName} has retrieved the Book of Medivh from its pedestal, and now prepares to harness its untold power.";

    /// <inheritdoc/>
    public override string PenaltyFlavour => 
      "Another faction has retrieved the Book of Medivh from its pedestal. Hopefully they do not turn its nefarious power against us.";

    /// <inheritdoc/>
    protected override void OnComplete(Faction completingFaction)
    {
      _objectiveWithCompletingUnit.CompletingUnit?.AddItemSafe(_bookOfMedivh.Item);
      _bookOfMedivhPedestal?.Kill();
    }
  }
}