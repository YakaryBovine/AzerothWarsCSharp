using System.Collections.Generic;
using MacroTools;
using MacroTools.Extensions;
using MacroTools.FactionChoices;
using MacroTools.FactionSystem;
using MacroTools.ObjectiveSystem;
using WarcraftLegacies.Shared.Config.Factions;
using WarcraftLegacies.Source.GameLogic;
using WarcraftLegacies.Source.Quests;
using WarcraftLegacies.Source.Quests.Gilneas;
using WarcraftLegacies.Source.Setup;
using WCSharp.Shared.Data;

namespace WarcraftLegacies.Source.Factions
{
  public sealed class Gilneas : Faction
  {
    private readonly ArtifactSetup _artifactSetup;
    private readonly AllLegendSetup _allLegendSetup;
    private readonly unit _gilneasGate;

    /// <inheritdoc />
    public Gilneas(PreplacedUnitSystem preplacedUnitSystem, ArtifactSetup artifactSetup, AllLegendSetup allLegendSetup) : base("Gilneas", PLAYER_COLOR_COAL, "|cff808080",
      @"ReplaceableTextures\CommandButtons\BTNGreymane.blp")
    {
      TraditionalTeam = TeamSetup.NorthAlliance;
      _artifactSetup = artifactSetup;
      _allLegendSetup = allLegendSetup;
      _gilneasGate = preplacedUnitSystem.GetUnit(UNIT_H02K_GREYMANE_S_GATE_CLOSED);
      StartingGold = 200;
      ControlPointDefenderUnitTypeId = UNIT_H0AF_CONTROL_POINT_DEFENDER_GILNEAS;
      StartingCameraPosition = Regions.GilneasStartPos.Center;
      StartingUnits = Regions.GilneasStartPos.PrepareUnitsForRescue(RescuePreparationMode.Invulnerable);
      LearningDifficulty = FactionLearningDifficulty.Advanced;
      IntroText = @"You are playing as the accursed |cff646464Kingdom of Gilneas|r|r.

You start isolated behind the Greymane Wall, the only way for an enemy to reach you is through the Greymane Gate or via the coast.

You must raise an army and fight back against the feral wolves and worgen that have overrun  your Kingdom.

Once you have reclaimed Gilneas, open Greymane's Gate and march North to assist Lordaeron and Dalaran with the plague, if it's not too late.";
      GoldMines = new List<unit>
      {
        preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(9392, -921)),
        preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(8784, 1993)),
      };
      Nicknames = new List<string>
      {
        "gil",
        "giln",
        "worgen",
        "worg",
        "dog",
        "dogs"
      };
      RegisterFactionDependentInitializer<Legion>(RegisterBookOfMedivhQuest);
      RegisterFactionDependentInitializer<Druids>(RegisterDruidsQuests);
    }

    /// <inheritdoc />
    public override void OnRegistered()
    {
      RegisterObjectLimits();
      RegisterQuests();
      SharedFactionConfigSetup.AddSharedFactionConfig(this);
    }
    
    /// <inheritdoc />
    public override void OnNotPicked()
    {
      Regions.GilneasUnlock1.CleanupNeutralPassiveUnits();
      Regions.GilneasUnlock2.CleanupNeutralPassiveUnits();
      Regions.GilneasUnlock3.CleanupNeutralPassiveUnits();
      Regions.GilneasUnlock4.CleanupNeutralPassiveUnits();
      Regions.GilneasUnlock5.CleanupNeutralPassiveUnits();
      Regions.GilneasUnlock6.CleanupNeutralPassiveUnits();
      base.OnNotPicked();
    }

    private void RegisterObjectLimits()
    {
      foreach (var (objectTypeId, objectLimit) in GilneasObjectLimitData.GetAllObjectLimits())
        ModObjectLimit(FourCC(objectTypeId), objectLimit);
    }
    
    private void RegisterQuests()
    {
      AddQuest(new QuestDuskhaven());
      AddQuest(new QuestStormglen());
      AddQuest(new QuestKeelHarbor());
      AddQuest(new QuestTempestReach());
      AddQuest(new QuestGilneasCity(_gilneasGate));
      AddQuest(new QuestCrowley());
    }
    
    private void RegisterBookOfMedivhQuest(Legion legion)
    {
      SharedQuestRepository.RegisterQuestFactory(faction => new QuestBookOfMedivh(_allLegendSetup.Gilneas.GilneasCastle,
        new NamedRectangle("Gilneas", Regions.BookOfMedivhGilneas), _artifactSetup.BookOfMedivh,
        faction == legion, faction == this));
    }
    
    private void RegisterDruidsQuests(Druids druids)
    {
      AddQuest(new QuestGoldrinn(_artifactSetup.ScytheOfElune, _allLegendSetup.Gilneas.Goldrinn, druids));
    }
  }
}