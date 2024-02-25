using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.QuestSystem;
using WarcraftLegacies.Source.Powers;

namespace WarcraftLegacies.Source.Quests.Druids
{
  /// <summary>
  /// Capture Shaladrassil to get a Power related to it.
  /// </summary>
  public sealed class QuestShaladrassil : QuestData
  {
    private readonly Capital _shaladrassil;

    /// <inheritdoc />
    public QuestShaladrassil(Capital shaladrassil) : base("Crown of Shadow",
      "The World Tree Shaladrassil was planted in the land once known as Val'sharah, the cradle of Druidic culture. Val'sharah was shattered by the Great Sundering along with the rest of the Broken Isles. The tree still remains, but without a Druidic presence it will wither in time.",
      @"ReplaceableTextures\CommandButtons\BTNTreeOfEternity.blp")
    {
      _shaladrassil = shaladrassil;
      AddObjective(new ObjectiveControlCapital(_shaladrassil, false));
    }

    /// <inheritdoc />
    public override string RewardFlavour => "With Shaladrassil back under Druidic control, its roots begin to swell and its branches bloom flowers anew, as if welcoming the Night elves home.";
    
    /// <inheritdoc />
    protected override string RewardDescription => "You gain the Shaladrassil's Blessing Power";

    /// <inheritdoc />
    protected override void OnComplete(Faction completingFaction)
    {
      var power = new ShaladrassilsBlessing(_shaladrassil.Unit,
        Constants.UNIT_EFON_TREANT_DRUIDS_SUMMONED, 60, 12, 50)
      {
      };
      completingFaction.AddPower(power);
      completingFaction.Player?.DisplayPowerAcquired(power);
    }
  }
}