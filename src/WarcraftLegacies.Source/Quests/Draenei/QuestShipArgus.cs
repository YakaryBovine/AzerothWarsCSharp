using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.QuestSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Draenei
{
  /// <summary>
  /// Create a portal between Tempest Keep and Argus.
  /// </summary>
  public sealed class QuestShipArgus : QuestData
  {
    private readonly unit _outlandToArgusWaygate;
    private readonly unit _argusToOutlandWaygate;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestShipArgus"/> class.
    /// </summary>
    /// <param name="outlandToArgusWaygate">Starts hidden, and gets enabled as a waygate when the quest is complete.</param>
    /// <param name="argusToOutlandWaygate">Starts hidden, and gets enabled as a waygate when the quest is complete.</param>
    /// <param name="velen">Needs to be brought somewhere to complete the quest.</param>
    public QuestShipArgus(unit outlandToArgusWaygate, unit argusToOutlandWaygate, LegendaryHero velen) : base("Reconquering Tempest Keep",
      "Tempest Keep still has the power to open a portal Argus, but Velen needs to channel it",
      @"ReplaceableTextures\CommandButtons\BTNArcaneCastle.blp")
    {
      _outlandToArgusWaygate = outlandToArgusWaygate.Show(false);
      _argusToOutlandWaygate = argusToOutlandWaygate.Show(false);
      AddObjective(new ObjectiveChannelRect(Regions.TempestKeepSpawn, "Tempest Keep", velen, 180, 0));
      Global = true;
      Progress = QuestProgress.Undiscovered;
    }

    /// <inheritdoc />
    public override string RewardFlavour => "Velen has opened the portal to Argus";

    /// <inheritdoc />
    protected override string RewardDescription => "Open a Portal between Tempest Keep and Argus";
    
    /// <inheritdoc />
    protected override void OnComplete(Faction completingFaction)
    {
      _outlandToArgusWaygate
        .Show(true)
        .SetWaygateDestination(Regions.TempestKeepSpawn.Center);
      _argusToOutlandWaygate
        .Show(true)
        .SetWaygateDestination(Regions.OutlandToArgus.Center);
    }
  }
}