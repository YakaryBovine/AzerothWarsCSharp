using System.Collections.Generic;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.Libraries;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.ObjectiveSystem.Objectives.TimeBased;
using MacroTools.QuestSystem;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Quelthalas
{
  public sealed class QuestQueldanil : QuestData
  {
    private readonly LegendaryHero _rommath;
    private readonly Rectangle _secondChanceRect;
    private readonly Rectangle _rescueRect;
    private readonly List<unit> _rescueUnits;
    private const int GoldOnFail = 400;
    private const int LumberOnFail = 750;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestQueldanil"/> class.
    /// </summary>
    public QuestQueldanil(Rectangle rescueRect, Rectangle secondChanceRect, Capital sunwell, LegendaryHero rommath) : base("Quel'danil Lodge",
      "Quel'danil Lodge is a High Elven outpost situated in the Hinterlands. It's been some time since the rangers there have been in contact with Quel'thalas.",
      @"ReplaceableTextures\CommandButtons\BTNBearDen.blp")
    {
      _secondChanceRect = secondChanceRect;
      _rommath = rommath;
      _rescueRect = rescueRect;
      AddObjective(new ObjectiveTime(1200));
      ResearchId = Constants.UPGRADE_R074_QUEST_COMPLETED_QUEL_DANIL_LODGE;
      _rescueUnits = rescueRect.PrepareUnitsForRescue(RescuePreparationMode.HideNonStructures);
      AddObjective(new ObjectiveControlCapital(sunwell, true));
      

    }

    /// <inheritdoc />
    public override string PenaltyFlavour =>
      "The Sunwell has fallen. The survivors escape into the Hinterlands and find refuge at Quel'Danil Lodge.";

    /// <inheritdoc/>
    public override string RewardFlavour =>
      "The rangers of Quel'danil have been reunited with the forces of Quel'thalas.";

    /// <inheritdoc/>
    protected override string RewardDescription => "Grants control of Quel'danil Lodge and it's rangers";

    /// <inheritdoc />
    protected override string PenaltyDescription =>
      $"You lose everything you control, but you gain control of Quel'Danil Lodge in the Hinterlands and you receive {GoldOnFail} gold and {LumberOnFail} lumber";

    /// <inheritdoc/>
    protected override void OnComplete(Faction completingFaction)
    {
      completingFaction.Player.RescueGroup(_rescueUnits);
    }

    /// <inheritdoc />
    protected override void OnFail(Faction completingFaction)
    {
      completingFaction.Player?
        .RemoveAllResources()
        .RemoveAllUnits();
      
      if (completingFaction.ScoreStatus == ScoreStatus.Defeated)
        return;

      completingFaction.Player?.RepositionCamera(_rescueRect.Center.X, _rescueRect.Center.Y);
      CreateSecondChanceUnits(completingFaction);
      completingFaction.Player?.AddGold(GoldOnFail);
      completingFaction.Player?.AddLumber(LumberOnFail);
      completingFaction.Player?.RescueGroup(_rescueUnits);
    }
    /// <inheritdoc />
    protected override void OnAdd(Faction whichFaction)
    { }
    private void CreateSecondChanceUnits(Faction whichFaction)
    {
      var whichPlayer = whichFaction.Player;
      if (whichPlayer == null)
        return;
      var spawn = _secondChanceRect.Center;
      GeneralHelpers.CreateUnits(whichPlayer, Constants.UNIT_U00J_ARCANE_WAGON_QUEL_THALAS, spawn.X, spawn.Y, 270, 2);
      GeneralHelpers.CreateUnits(whichPlayer, Constants.UNIT_HHES_SWORDSMAN_QUEL_THALAS, spawn.X, spawn.Y, 270, 12);
      GeneralHelpers.CreateUnits(whichPlayer, Constants.UNIT_NHEA_RANGER_QUEL_THALAS, spawn.X, spawn.Y, 270, 12);
      GeneralHelpers.CreateUnits(whichPlayer, Constants.UNIT_N063_MAGUS_QUEL_THALAS, spawn.X, spawn.Y, 270, 6); 
      _rommath.ForceCreate(whichPlayer, _secondChanceRect.Center, 270);
      UnitAddItem(_rommath.Unit,
        CreateItem(Constants.ITEM_STWP_TOWN_PORTAL_SCROLL, GetUnitX(_rommath.Unit),
          GetUnitY(_rommath.Unit)));
    }

  }
}

