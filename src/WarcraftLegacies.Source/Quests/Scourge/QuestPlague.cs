using System.Collections.Generic;
using System.Linq;
using MacroTools;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.Libraries;
using MacroTools.ObjectiveSystem.Objectives.MetaBased;
using MacroTools.ObjectiveSystem.Objectives.TimeBased;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using MacroTools.QuestSystem;
using MacroTools.UserInterface;
using WarcraftLegacies.Source.FactionMechanics.Scourge.Plague;
using WarcraftLegacies.Source.GameLogic;
using WarcraftLegacies.Source.Rocks;
using WCSharp.Buffs;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Scourge
{
  /// <summary>
  /// When completed, the plague begins, granting either the Forsaken or the quest holder several Plague Cauldrons that periodically spawn undead units.
  /// </summary>
  public sealed class QuestPlague : QuestData
  {
    private readonly Faction _plagueVictim;
   
    private readonly Faction _secondaryPlagueFaction;
    private readonly PlagueParameters _plagueParameters;

    private readonly List<unit> _deathknellUnits;
    private readonly List<unit> _coastUnits;
    private readonly List<unit> _scholomanceUnits;

    /// <summary>
    /// When completed, the quest holder initiates the Plague, creating Plague Cauldrons around Lordaeron
    /// and converting villagers into Zombies.
    /// </summary>
    /// <param name="plagueParameters">Provides information about how the Plague should work.</param>
    /// <param name="plagueVictim">The faction that the plague will primarily affect.</param>
    /// <param name="secondaryPlagueFaction">The faction that will gain some of the fringe benefits of the plague.</param>
    /// /// <param name="coast">The base near Stratholme coast.</param>
    /// /// <param name="deathknell">The base near Capital Palace.</param>
    /// /// <param name="scholomance">The base at Caer Darrow.</param>
    public QuestPlague(PlagueParameters plagueParameters, Faction plagueVictim,
      Faction secondaryPlagueFaction, Rectangle deathknell, Rectangle coast, Rectangle scholomance) : base(
      "Plague of Undeath",
      "The Cult of the Damned is prepared to unleash a devastating zombifying plague across the lands of Lordaeron.",
      @"ReplaceableTextures\CommandButtons\BTNPlagueBarrel.blp")
    {
      _plagueVictim = plagueVictim;
      _plagueParameters = plagueParameters;
      _secondaryPlagueFaction = secondaryPlagueFaction;
      AddObjective(new ObjectiveEitherOf(
        new ObjectiveResearch(Constants.UPGRADE_R06I_PLAGUE_OF_UNDEATH_SCOURGE, FourCC("u000")),
        new ObjectiveTime(660)));
      AddObjective(new ObjectiveTime(540));
      _deathknellUnits = deathknell.PrepareUnitsForRescue(RescuePreparationMode.HideAll);
      _scholomanceUnits = scholomance.PrepareUnitsForRescue(RescuePreparationMode.HideAll);
      _coastUnits = coast.PrepareUnitsForRescue(RescuePreparationMode.HideAll);
      Global = true;
      
      ResearchId = Constants.UPGRADE_R009_QUEST_COMPLETED_PLAGUE_OF_UNDEATH;

    }

    /// <inheritdoc />
    public override string RewardFlavour =>
      "The plague has been unleashed! The citizens of Lordaeron are quickly transforming into mindless zombies";

    /// <inheritdoc />
    protected override string RewardDescription =>
      "All villagers in Lordaeron are transformed into Zombies, several Zombie-spawning Plague Cauldrons spawn throughout Lordaeron, you gain control of three bases around Lordaeron, and Lordaeron's Control Points reset to level 0. You will also be given a choice to instantly move your military units from Northrend to one of three locations in Lordaeron";

    /// <inheritdoc />
    protected override void OnComplete(Faction completingFaction)
    {
      completingFaction.ModObjectLimit(Constants.UPGRADE_R06I_PLAGUE_OF_UNDEATH_SCOURGE, -Faction.UNLIMITED);
      var plaguePower = new PlaguePower(_plagueVictim);
      if (completingFaction.Player != null)
        CreatePlagueCauldrons(completingFaction);
      completingFaction.AddPower(plaguePower);
      ResetVictimControlPointLevel();

      new ScourgeInvasionDialogPresenter(
        new Choice<Rectangle?>(null, "No invasion"),
        new Choice<Rectangle?>(Regions.CaerDarrow, "Scholomance"),
        new Choice<Rectangle?>(Regions.StratholmeScourgeBase, "Stratholme"),
        new Choice<Rectangle?>(Regions.DeathknellUnlock, "Deathknell"))
        .Run(Player(3));

      completingFaction.Player.RescueGroup(_deathknellUnits);
      completingFaction.Player.RescueGroup(_coastUnits);
      completingFaction.Player.RescueGroup(_scholomanceUnits);

      RockSystem.Register(new RockGroup(Regions.Northrend_Blocker_1, FourCC("B013"), 120));
      RockSystem.Register(new RockGroup(Regions.Northrend_Blocker_2, FourCC("B013"), 120));
      
      if (completingFaction.TryGetPowerByName("Cult Spies", out var spiesPower))
        completingFaction.RemovePower(spiesPower);
      else
        Logger.LogWarning($"Expected {completingFaction.Name} to have the Cult Spies Power.");
    }

    /// <inheritdoc />
    protected override void OnAdd(Faction whichFaction) =>
      whichFaction.ModObjectLimit(Constants.UPGRADE_R06I_PLAGUE_OF_UNDEATH_SCOURGE, Faction.UNLIMITED);
    
    private void CreatePlagueCauldrons(Faction completingFaction)
    {
      var primaryPlaguePlayer = completingFaction.ScoreStatus != ScoreStatus.Defeated && completingFaction.Player != null
        ? completingFaction.Player
        : Player(PLAYER_NEUTRAL_AGGRESSIVE);
      
      var secondaryPlaguePlayer = _secondaryPlagueFaction.ScoreStatus != ScoreStatus.Defeated && _secondaryPlagueFaction.Player != null
        ? _secondaryPlagueFaction.Player
        : Player(PLAYER_NEUTRAL_AGGRESSIVE);

      foreach (var plagueRect in _plagueParameters.PlagueRects)
      {
        var position = plagueRect.GetRandomPoint();
        var plagueCauldron = CreateUnit(primaryPlaguePlayer, _plagueParameters.PlagueCauldronUnitTypeId, position.X, position.Y, 0)
          .SetTimedLife(_plagueParameters.Duration);

        plagueCauldron.RemoveDestructablesInRadius(250f);

        CreateUnit(secondaryPlaguePlayer, Constants.UNIT_U00D_LEGION_HERALD_LEGION_WORKER, position.X, position.Y, 0);

        var attackTarget = _plagueParameters.AttackTargets.OrderBy(x => MathEx.GetDistanceBetweenPoints(position, x)).First();

        var plagueCauldronBuff = new PlagueCauldronBuff(plagueCauldron, plagueCauldron, attackTarget)
        {
          ZombieUnitTypeId = Constants.UNIT_NZOM_ZOMBIE_SCOURGE
        };
        BuffSystem.Add(plagueCauldronBuff);

        foreach (var parameter in _plagueParameters.PlagueCauldronSummonParameters)
        foreach (var unit in GeneralHelpers.CreateUnits(primaryPlaguePlayer, parameter.SummonUnitTypeId,
                   position.X, position.Y, 0, parameter.SummonCount))
        {
          if (!unit.IsType(UNIT_TYPE_PEON))
            unit.IssueOrder(OrderId("attack"), attackTarget);
        }
      }
    }
    
    private void ResetVictimControlPointLevel()
    {
      if (_plagueVictim.Player == null) 
        return;
      
      foreach (var controlPoint in _plagueVictim.Player.GetControlPoints())
        controlPoint.ControlLevel = 0;
    }
  }
}