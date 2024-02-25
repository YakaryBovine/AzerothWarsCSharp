using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.QuestSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Legion
{
  public sealed class QuestConsumeTree : QuestData
  {
    private readonly LegendaryHero _archimonde;
    private readonly Faction _druids;
    private const int StatGain = 80;
    
    public QuestConsumeTree(LegendaryHero archimonde, Faction druids) : base("Twilight of the Gods",
      "Long ago, the Night Elves' hubris led them to forge a second Well of Eternity following the destruction of the first. Nordrassil was planted atop it as a means of protection, but this measly act of defiance shall not prevent Lord Archimonde from seizing the Well's energies for himself.",
      @"ReplaceableTextures\CommandButtons\BTNGlazeroth.blp")
    {
      _archimonde = archimonde;
      _druids = druids;
      AddObjective(new ObjectiveChannelRect(Regions.ArchimondeChannel, "The World Tree", _archimonde, 420, 90, Title));
      Global = true;
    }

    /// <inheritdoc/>
    public override string RewardFlavour =>
      "The Third War is over. Archimonde has successfully consumed the energies of the Well of Eternity resting beneath Nordrassil. The last line of defense against the Burning Legion has fallen, and with it dies the hopes and dreams of Azeroth.";

    /// <inheritdoc/>
    protected override string RewardDescription =>
      $"Archimonde gains {StatGain} Strength, Agility, and Intelligence, and the Druids are defeated";
    
    /// <inheritdoc/>
    protected override void OnComplete(Faction completingFaction)
    {
      var archimondeUnit = _archimonde.Unit;
      var druidsPlayer = _druids.Player;
      druidsPlayer?
        .RemoveAllResources()
        .RemoveAllUnits();

      archimondeUnit?.SetName("Devourer of Worlds");
      AddSpecialEffectTarget(@"Abilities\Weapons\GreenDragonMissile\GreenDragonMissile.mdl", archimondeUnit,
        "hand, right");
      AddSpecialEffectTarget(@"Abilities\Weapons\GreenDragonMissile\GreenDragonMissile.mdl", archimondeUnit, "hand, left");
      archimondeUnit?.AddHeroAttributes(StatGain, StatGain, StatGain);
    }
  }
}