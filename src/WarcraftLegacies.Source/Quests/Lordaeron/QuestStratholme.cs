using System.Collections.Generic;
using MacroTools;
using MacroTools.ControlPointSystem;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.ControlPointBased;
using MacroTools.ObjectiveSystem.Objectives.FactionBased;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.ObjectiveSystem.Objectives.TimeBased;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using MacroTools.QuestSystem;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Lordaeron
{
  /// <summary>
  /// Free Alterac Mountains of the Blackrock orcs and upgrgade the main building to a castle in order to gain control of Stratholme.
  /// </summary>
  public sealed class QuestStratholme : QuestData
  {
    private readonly LegendaryHero _arthas;
    private readonly LegendaryHero _uther;
    private readonly Capital _stratholme;
    private readonly List<unit> _rescueUnits;
    private readonly unit _goldmine1;
    private readonly unit _goldmine2;
    private readonly unit _goldmine3;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestStratholme"/> class.
    /// </summary>
    public QuestStratholme(Rectangle rescueRect, PreplacedUnitSystem preplacedUnitSystem, LegendaryHero arthas, LegendaryHero uther, Capital stratholme) : base("Blackrock and Roll",
      "The Blackrock clan has taken over Alterac, they must be eliminated for the safety of Lordaeron",
      @"ReplaceableTextures\CommandButtons\BTNChaosBlademaster.blp")
    {
      _arthas = arthas;
      _uther = uther;
      _stratholme = stratholme;
      AddObjective(new ObjectiveUnitIsDead(preplacedUnitSystem.GetUnit(Constants.UNIT_O00B_JUBEI_THOS_LEGION_DEMI)));
      AddObjective(new ObjectiveControlPoint(ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N019_ALTERAC_MOUNTAINS)));
      AddObjective(new ObjectiveUpgrade(Constants.UNIT_HCAS_CASTLE_LORDAERON_T3, Constants.UNIT_HTOW_TOWN_HALL_LORDAERON_T1));
      AddObjective(new ObjectiveControlLegend(arthas, false));
      AddObjective(new ObjectiveExpire(660, Title));
      AddObjective(new ObjectiveSelfExists());

      _goldmine1 = preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(17204, 8197));
      _goldmine2 = preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(7704, 11660));
      _goldmine3 = preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(7403, 6983));

      //_goldmine1.Show(false);
      //_goldmine2.Show(false);
      //_goldmine3.Show(false);

      _rescueUnits = rescueRect.PrepareUnitsForRescue(RescuePreparationMode.HideNonStructures);
      ResearchId = Constants.UPGRADE_R09E_QUEST_COMPLETED_BLACKROCK_AND_ROLL;
      
    }

    /// <inheritdoc/>
    public override string RewardFlavour =>
      "Stratholme has been liberated, and its military is now free to assist the Kingdom of Lordaeron.";
    /// <inheritdoc/>
    protected override string RewardDescription => "Control of all units in Stratholme and you can now build Town Halls";

    /// <inheritdoc/>
    protected override void OnFail(Faction completingFaction)
    {
      var rescuer = completingFaction.ScoreStatus == ScoreStatus.Defeated
        ? Player(PLAYER_NEUTRAL_AGGRESSIVE)
        : completingFaction.Player;

      rescuer.RescueGroup(_rescueUnits);
      _arthas.AddUnitDependency(_stratholme.Unit);
    }

    /// <inheritdoc/>
    protected override void OnComplete(Faction completingFaction)
    {
      completingFaction.Player.RescueGroup(_rescueUnits);
      _arthas.AddUnitDependency(_stratholme.Unit);
      _uther.AddUnitDependency(_stratholme.Unit);
      //_goldmine1.Show(true);
      //_goldmine2.Show(true);
      //_goldmine3.Show(true);
    }
  }
}