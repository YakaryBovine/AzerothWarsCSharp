using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.QuestSystem;
using WarcraftLegacies.Source.Setup.FactionSetup;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Legion
{
  public sealed class QuestConsumeTree : QuestData
  {
    private readonly LegendaryHero _archimonde;

    public QuestConsumeTree(LegendaryHero archimonde) : base("Twilight of the Gods",
      "Consuming the World Tree will grant Archimonde immeasurable power and eliminate his mortal enemies, the Druids of Kalimdor, forever.",
      @"ReplaceableTextures\CommandButtons\BTNGlazeroth.blp")
    {
      _archimonde = archimonde;
      AddObjective(new ObjectiveChannelRect(Regions.ArchimondeChannel, "The World Tree", _archimonde, 420, 90, Title));
      Global = true;
    }

    /// <inheritdoc/>
    protected override string RewardFlavour =>
      "Archimonde has now consummed the World Tree and is now nigh unstoppable";

    /// <inheritdoc/>
    protected override string RewardDescription =>
      "By consuming the World Tree, Archimonde will obtain immense power. +80 to all stats. Additionally, the Druids faction will be eliminated.";


    /// <inheritdoc/>
    protected override void OnComplete(Faction completingFaction)
    {
      var archimondeUnit = _archimonde.Unit;
      DruidsSetup.Druids?.Leave();
      BlzSetUnitName(archimondeUnit, "Devourer of Worlds");
      AddSpecialEffectTarget(@"Abilities\Weapons\GreenDragonMissile\GreenDragonMissile.mdl", archimondeUnit,
        "hand, right");
      AddSpecialEffectTarget(@"Abilities\Weapons\GreenDragonMissile\GreenDragonMissile.mdl", archimondeUnit, "hand, left");
      archimondeUnit?.AddHeroAttributes(80, 80, 80);
    }
  }
}