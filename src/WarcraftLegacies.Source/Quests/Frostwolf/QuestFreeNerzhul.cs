using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using MacroTools.QuestSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Frostwolf
{
  /// <summary>
  /// Frostwolf kills the Frozen Throne to get cool stuff for Thrall.
  /// </summary>
  public sealed class QuestFreeNerzhul : QuestData
  {
    private readonly LegendaryHero _thrall;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestFreeNerzhul"/> class.
    /// </summary>
    public QuestFreeNerzhul(Legend lichKing, LegendaryHero thrall) : base("Jailor of the Damned",
      "Before he became the Lich King, Ner'zhul was the chieftain and elder shaman of the Shadowmoon Clan. Perhaps something of his former self still survives within the Frozen Throne.",
      @"ReplaceableTextures\CommandButtons\BTNShaman.blp")
    {
      _thrall = thrall;
      AddObjective(new ObjectiveKillUnit(lichKing.Unit));
    }
    
    /// <inheritdoc/>
    public override string RewardFlavour =>
      "Ner'zhul is finally free from his tortured existence as the bearer of the Helm of Domination. With his dying breath, he shares with Thrall the latent energies of the Spell of Conjuration that once shattered the Orc homeworld of Draenor.";

    /// <inheritdoc/>
    protected override string RewardDescription => "Thrall gains 10 Strength, 10 Dexterity, 10 Intelligence, and an item that can channel a one-time portal to Nagrand in Outland";

    /// <inheritdoc/>
    protected override void OnComplete(Faction completingFaction)
    {
      _thrall.Unit?
        .AddHeroAttributes(10, 10, 10)
        .AddItemSafe(CreateItem(Constants.ITEM_I017_PORTAL_TO_NAGRAND, 0, 0));
    }
  }
}