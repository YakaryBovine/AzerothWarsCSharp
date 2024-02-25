using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.QuestSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Dalaran
{
  public sealed class QuestBlueDragons : QuestData
  {
    private static readonly int RESEARCH_ID = FourCC("R00U");
    private static readonly int DRAGON_ID = FourCC("n0AC");
    private static readonly int MANADAM_ID = FourCC("R00N");

    public QuestBlueDragons(Capital theNexus) : base("The Blue Dragonflight",
      "The Blue Dragons of Northrend are the wardens of magic on Azeroth. They might be convinced to willingly join the mages of Dalaran.",
      @"ReplaceableTextures\CommandButtons\BTNAzureDragon.blp")
    {
      AddObjective(new ObjectiveControlCapital(theNexus, false));
    }

    /// <inheritdoc/>
    public override string RewardFlavour =>
      "The Nexus has been captured. The Blue Dragonflight fights for Dalaran.";

    /// <inheritdoc/>
    protected override string RewardDescription => "Learn to train Blue Dragons";

    /// <inheritdoc/>
    protected override void OnComplete(Faction completingFaction)
    {
      SetPlayerTechResearched(completingFaction.Player, RESEARCH_ID, 1);
      completingFaction.Player?.DisplayUnitTypeAcquired(DRAGON_ID,
        "You can now train Blue Dragons from Military Quarters and the Nexus.");
    }

    /// <inheritdoc/>
    protected override void OnAdd(Faction whichFaction)
    {
      whichFaction.ModObjectLimit(MANADAM_ID, Faction.UNLIMITED);
    }
  }
}