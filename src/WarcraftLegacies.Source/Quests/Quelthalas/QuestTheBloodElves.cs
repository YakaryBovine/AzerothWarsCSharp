using MacroTools.ControlPointSystem;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.ControlPointBased;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.Powers;
using MacroTools.QuestSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Quelthalas
{
  /// <summary>
  /// Quel'thalas either wins or loses their duel to get Blood Mages and some other stuff.
  /// </summary>
  public sealed class QuestTheBloodElves : QuestData
  {
    private const int QuestResearchId = Constants.UPGRADE_R04Q_QUEST_COMPLETED_THE_BLOOD_ELVES_QUEL_THALAS;
    private const int UnittypeId = Constants.UNIT_N048_BLOOD_MAGE_QUEL_THALAS;
    private const int BuildingId = Constants.UNIT_N0A2_CONSORTIUM_QUEL_THALAS_SIEGE;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestTheBloodElves"/> class.
    /// </summary>
    public QuestTheBloodElves(Capital draktharonKeep) : base("The Blood Elves",
      "The Elves of Quel'thalas have a deep reliance on the Sunwell's magic. But perhaps, in these dark times they would have to turn to darker magicks to sate themselves.",
      @"ReplaceableTextures\CommandButtons\BTNHeroBloodElfPrince.blp")

    { 
      AddObjective(new ObjectiveControlCapital(draktharonKeep, false));
      AddObjective(new ObjectiveControlLevel(ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N030_DRAK_THARON_KEEP), 10));
      
    }


  /// <inheritdoc />
  public override string RewardFlavour =>
    "The Legion Nexus has been obliterated. A group of ambitious mages seize the opportunity to study the demons' magic, becoming the first Blood Mages.";

  /// <inheritdoc />
  protected override string RewardDescription =>
    $"Learn to train {GetObjectName(UnittypeId)}s from the Consortium, and you can summon Magister Rommath from the Altar of Prowess";

  /// <inheritdoc />
  protected override void OnComplete(Faction completingFaction)
  {
    SetPlayerTechResearched(completingFaction.Player, QuestResearchId, 1);
    completingFaction.Player?.DisplayUnitTypeAcquired(UnittypeId,
      $"You can now train {GetObjectName(UnittypeId)}s from the {GetObjectName(BuildingId)}.");
  }


  /// <inheritdoc />
  protected override void OnAdd(Faction whichFaction)
  {
    whichFaction.ModObjectLimit(QuestResearchId, Faction.UNLIMITED);
  }


  private static void GrantPower(Faction whichFaction)
  {
    var manaAddiction = new UnitsStealMana(0.35f)
    {
      Name = "Mana Addiction"
    };
    whichFaction.AddPower(manaAddiction);
    whichFaction.Player?.DisplayPowerAcquired(manaAddiction);

  }
 }
}