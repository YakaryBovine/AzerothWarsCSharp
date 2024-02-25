using MacroTools;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.QuestSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Sunfury
{
  public sealed class QuestWellOfEternity : QuestData
  {
    private readonly unit _well;

    public QuestWellOfEternity(PreplacedUnitSystem preplacedUnitSystem, LegendaryHero kiljaeden) : base(
      "The Well of Eternity",
      "The Maelstrom still hides the shattered Well of Eternity. With his immense power, Kil'jaeden can summon a new well that will bring forth the destruction of the world.",
      @"ReplaceableTextures\CommandButtons\BTNFountainOfLife.blp")
    {
      _well = preplacedUnitSystem.GetUnit(Constants.UNIT_N0DZ_THE_WELL_OF_ETERNITY_SUNFURY_OTHER).Show(false);
      AddObjective(new ObjectiveChannelRect(Regions.MaelstromChannel, "The Maelstrom", kiljaeden, 420, 90, Title));
      Global = true;
    }

    /// <inheritdoc/>
    public override string RewardFlavour =>
      "Kil'jaeden has reformed the ancient Well of Eternity. From its wellsprings, unlimited arcane energies spring forth. For the first time in their miserable existences, the Sunfury are truly sated.";

    /// <inheritdoc/>
    protected override string RewardDescription =>
      "Gain control of the Well of Eternity, which will grant every Sunfury unit unlimited mana";
    
    /// <inheritdoc/>
    protected override void OnComplete(Faction completingFaction)
    {
      _well.Show(true);
      SetUnitOwner(_well, completingFaction.Player, true);
    }
  }
}