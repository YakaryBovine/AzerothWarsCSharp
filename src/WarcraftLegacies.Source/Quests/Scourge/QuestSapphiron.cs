using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using MacroTools.QuestSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Scourge
{
  /// <inheritdoc/>
  public sealed class QuestSapphiron : QuestData
  {
    private readonly ObjectiveUnitIsDead _unitIsDeadObjective;
    private const int SapphironId = Constants.UNIT_UBDD_SAPPHIRON_SCOURGE_DEMI;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestSapphiron"/> class.
    /// </summary>
    /// <param name="sapphiron"></param>
    /// <param name="kelthuzad"></param>
    public QuestSapphiron(unit sapphiron, LegendaryHero kelthuzad) : base("Sapphiron",
      "Kill Sapphiron the Blue Dragon to have Kel'Tuzad reanimate her as a Frost Wyrm. Sapphiron can be found in Northrend.",
      @"ReplaceableTextures\CommandButtons\BTNFrostWyrm.blp")
    {
      _unitIsDeadObjective = new ObjectiveUnitIsDead(sapphiron);
      AddObjective(_unitIsDeadObjective);
      AddObjective(new ObjectiveControlLegend(kelthuzad, false));
      ResearchId = Constants.UPGRADE_R025_QUEST_COMPLETED_SAPPHIRON_SCOURGE;
    }

    /// <inheritdoc/>
    public override string RewardFlavour =>
      "Sapphiron has been slain, and has been reanimated as a mighty Frost Wyrm under the command of the Scourge.";

    /// <inheritdoc/>
    protected override string RewardDescription => "Unlock one of the requirements to train Frost Wyrms. " +
                                                   "If your team killed Sapphiron, gain him in an undead form; " +
                                                   $"otherwise, learn to train him from the {GetObjectName(Constants.UNIT_UAOD_ALTAR_OF_DARKNESS_SCOURGE_ALTAR)}";

    /// <inheritdoc/>
    protected override void OnComplete(Faction completingFaction)
    {
      var killingPlayer = _unitIsDeadObjective.KillingUnit?.OwningPlayer();
      if (killingPlayer == null)
        return;
      if (completingFaction.Player?.GetTeam()?.Contains(killingPlayer) == true)
        CreateUnit(completingFaction.Player, SapphironId, GetUnitX(GetTriggerUnit()), GetUnitY(GetTriggerUnit()),
          GetUnitFacing(GetTriggerUnit()));
    }
  }
}