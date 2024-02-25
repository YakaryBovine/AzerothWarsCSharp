using MacroTools.ControlPointSystem;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.Libraries;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using MacroTools.QuestSystem;
using static MacroTools.Libraries.GeneralHelpers;
using static War3Api.Common;


namespace WarcraftLegacies.Source.Quests.Warsong
{
  public sealed class QuestWarsongHold : QuestData
  {
    private const int RequiredResearchId = Constants.UPGRADE_R06G_NORTHREND_EXPEDITION_WARSONG;
    private const int AbilityId = Constants.ABILITY_A0DZ_WARSONG_OFFENSIVE_WARSONG;

    public QuestWarsongHold() : base("Warsong Hold",
      "The far-off land of Northrend is the new home of the traitor shaman Ner'zhul. The Warsong must land its forces on its shores in order to end the existential threat he now represents.",
      @"ReplaceableTextures\CommandButtons\BTNTuskaarBrown.blp")
    {
      AddObjective(new ObjectiveResearch(RequiredResearchId, Constants.UNIT_O02T_SHIPYARD_WARSONG_SHIPYARD));
    }

    /// <inheritdoc/>
    public override string RewardFlavour =>
      "The Warsong Clan has sail for the icy shores of Northrend and up a formidable encampment at Borean Tundra.";

    /// <inheritdoc/>
    protected override string RewardDescription => "A new base at Borean Tundra in Northrend";

    /// <inheritdoc/>
    protected override void OnComplete(Faction completingFaction)
    {
      var boreanTundra = ControlPointManager.Instance.GetFromUnitType(FourCC("n00G")).Unit;
      KillNeutralHostileUnitsInRadius(GetUnitX(boreanTundra), GetUnitY(boreanTundra), 2300);
      //Spawn the base
      SetUnitOwner(boreanTundra, completingFaction.Player, true);
      var warsongHold = CreateStructureForced(completingFaction.Player, FourCC("o02S"), -7648, 15456, 270, 192);
      BlzSetUnitName(warsongHold, "Warsong Hold");
      BlzSetUnitMaxHP(warsongHold, 4000);
      warsongHold.SetLifePercent(100);
      UnitAddAbility(warsongHold, AbilityId);
      CreateStructureForced(completingFaction.Player, FourCC("n03E"), -7296, 15680, 4.712389f * MathEx.DegToRad, 128);
      CreateStructureForced(completingFaction.Player, FourCC("o01T"), -7456, 15008, 4.712389f * MathEx.DegToRad, 128);
      CreateStructureForced(completingFaction.Player, FourCC("o028"), -7808, 16512, 4.712389f * MathEx.DegToRad, 128);
      CreateStructureForced(completingFaction.Player, FourCC("o028"), -7296, 16000, 4.712389f * MathEx.DegToRad, 128);
      CreateStructureForced(completingFaction.Player, FourCC("o028"), -7424, 16192, 4.712389f * MathEx.DegToRad, 128);
      CreateStructureForced(completingFaction.Player, FourCC("o028"), -6656, 15616, 4.712389f * MathEx.DegToRad, 128);
      CreateStructureForced(completingFaction.Player, FourCC("o028"), -6912, 15744, 4.712389f * MathEx.DegToRad, 128);
      CreateStructureForced(completingFaction.Player, FourCC("n08E"), -8299, 16110, 1.850517f * MathEx.DegToRad, 128);
      CreateStructureForced(completingFaction.Player, FourCC("o02Q"), -8512, 15936, 4.712389f * MathEx.DegToRad, 128);
      CreateStructureForced(completingFaction.Player, FourCC("n08E"), -8513, 16171, 1.126743f * MathEx.DegToRad, 128);
      CreateStructureForced(completingFaction.Player, FourCC("o01S"), -8192, 16576, 4.712389f * MathEx.DegToRad, 128);
      CreateStructureForced(completingFaction.Player, FourCC("o02M"), -8048, 16427, -0.7628738f * MathEx.DegToRad, 128);
      CreateStructureForced(completingFaction.Player, FourCC("o02M"), -8065, 15788, -0.08624744f * MathEx.DegToRad, 128);
      CreateStructureForced(completingFaction.Player, FourCC("o028"), -7936, 16768, 4.712389f * MathEx.DegToRad, 128);
      CreateStructureForced(completingFaction.Player, FourCC("o02T"), -6752, 14880, 4.712389f * MathEx.DegToRad, 128);
      CreateStructureForced(completingFaction.Player, FourCC("h0AP"), -8633, 15012, -1.101598f * MathEx.DegToRad, 128);
      CreateStructureForced(completingFaction.Player, FourCC("o020"), -6976, 15552, 4.712389f * MathEx.DegToRad, 128);
      CreateStructureForced(completingFaction.Player, FourCC("n03E"), -8064, 15360, 4.712389f * MathEx.DegToRad, 128);
      CreateStructureForced(completingFaction.Player, FourCC("n03E"), -8320, 16000, 4.712389f * MathEx.DegToRad, 128);
      CreateStructureForced(completingFaction.Player, FourCC("o02M"), -7086, 15749, 1.348478f * MathEx.DegToRad, 128);
      CreateStructureForced(completingFaction.Player, FourCC("n03E"), -7808, 16128, 4.712389f * MathEx.DegToRad, 128);
      CreateStructureForced(completingFaction.Player, FourCC("orai"), -7319, 15134, 0.467489f * MathEx.DegToRad, 128);
      CreateStructureForced(completingFaction.Player, FourCC("o02T"), -8672, 15328, 4.712389f * MathEx.DegToRad, 128);
      completingFaction.ModObjectLimit(RequiredResearchId, -Faction.UNLIMITED);
    }

    /// <inheritdoc/>
    protected override void OnAdd(Faction whichFaction)
    {
      whichFaction.ModObjectLimit(RequiredResearchId, Faction.UNLIMITED);
    }
  }
}