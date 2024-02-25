using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.QuestSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Sunfury
{
  /// <summary>
  /// Kael summons Kiljaeden
  /// </summary>
  public sealed class QuestSummonKil : QuestData
  {
    /// <inheritdoc />
    public QuestSummonKil(Capital stormwindKeep, Capital karazhan, LegendaryHero kael) : base("The Deceiver",
      "Our hidden master, Kil'jaeden, calls to us from the depths of the Twisting Nether. The bounty of fel energy residing within Karazhan could be used to bring him forth - but not while the Kingdom of Stormwind is still strong enough to interfere.",
      @"ReplaceableTextures\CommandButtons\BTNKiljaedin.blp")
    {
      AddObjective(new ObjectiveLegendLevel(kael, 8));
      AddObjective(new ObjectiveCapitalDead(stormwindKeep));
      AddObjective(new ObjectiveControlCapital(karazhan, false));
      AddObjective(new ObjectiveChannelRect(Regions.KilSummon, "Karazhan", kael, 180, 90, "Summoning Kil'jaeden"));
      ResearchId = Constants.UPGRADE_R09J_QUEST_COMPLETED_THE_DECEIVER;
    }

    /// <inheritdoc />
    public override string RewardFlavour =>
      "Kael'thas' profane ritual has paved the way for Kil'jaeden, supreme commander of the Burning Legion, to bridge the gap from the Twisting Nether to our world. Our people embrace fel magic wholeheartedly, training in preparation for their coming lord.";
    
    /// <inheritdoc />
    protected override string RewardDescription =>
      $"Learn to train Kil'jaeden from the {GetObjectName(Constants.UNIT_H0C6_ALTAR_OF_BLOOD_SUNFURY_ALTAR)}, and {GetObjectName(Constants.UNIT_N0E3_WARLOCK_SUNFURY)}s from the {GetObjectName(Constants.UNIT_H0CB_LYCEUM_ARCANUM_SUNFURY_MAGIC)}";
  }
}