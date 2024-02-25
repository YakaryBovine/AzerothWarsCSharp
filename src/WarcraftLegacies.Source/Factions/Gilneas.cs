using System.Collections.Generic;
using MacroTools;
using MacroTools.Extensions;
using MacroTools.FactionChoices;
using MacroTools.FactionSystem;
using MacroTools.ObjectiveSystem;
using WarcraftLegacies.Source.GameLogic;
using WarcraftLegacies.Source.Quests;
using WarcraftLegacies.Source.Quests.Gilneas;
using WarcraftLegacies.Source.Setup;
using WCSharp.Shared.Data;
using static War3Api.Common;

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
      _artifactSetup = artifactSetup;
      _allLegendSetup = allLegendSetup;
      _gilneasGate = preplacedUnitSystem.GetUnit(Constants.UNIT_H02K_GREYMANE_S_GATE_CLOSED);
      StartingGold = 200;
      StartingLumber = 700;
      ControlPointDefenderUnitTypeId = Constants.UNIT_H0AF_CONTROL_POINT_DEFENDER_GILNEAS;
      StartingCameraPosition = Regions.GilneasStartPos.Center;
      StartingUnits = Regions.GilneasStartPos.PrepareUnitsForRescue(RescuePreparationMode.Invulnerable);
      LearningDifficulty = FactionLearningDifficulty.Advanced;
      IntroText = @"You are playing as the accursed |cff646464Kingdom of Gilneas|r|r.

You start isolated behind the Greymane Wall, the only way for an enemy to reach you is through the Greymane Gate or via the coast.

You must raise an army and fight back against the feral wolves and worgen that have overrun  your Kingdom.

Once you have reclaimed Gilneas, open Greymane's Gate and march North to assist Lordaeron and Dalaran with the plague, if it's not too late.";
      GoldMines = new List<unit>
      {
        preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(4236, 1321)),
        preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(4477, -1449)),
        preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(7709, -2853)),
        preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(9392, -921)),
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

    private void RegisterObjectLimits()
    {
      SharedFactionConfigSetup.AddSharedFactionConfig(this);
      
      //Structures
      ModObjectLimit(FourCC("h01R"), UNLIMITED); //Town Hall
      ModObjectLimit(FourCC("h023"), UNLIMITED); //Keep
      ModObjectLimit(FourCC("h02C"), UNLIMITED); //Castle
      ModObjectLimit(FourCC("h02F"), UNLIMITED); //Farm
      ModObjectLimit(FourCC("h02X"), UNLIMITED); //Altar
      ModObjectLimit(FourCC("h039"), UNLIMITED); //Scout Tower
      ModObjectLimit(FourCC("h03A"), UNLIMITED); //Guard Tower
      ModObjectLimit(FourCC("h03B"), UNLIMITED); //Cannon Tower
      ModObjectLimit(FourCC("h03D"), UNLIMITED); //Temple of the cursed
      ModObjectLimit(FourCC("h03E"), UNLIMITED); //Training Hall
      ModObjectLimit(FourCC("n008"), UNLIMITED); //Marketplace
      ModObjectLimit(FourCC("h03H"), UNLIMITED); //Shipyard
      ModObjectLimit(FourCC("h03O"), UNLIMITED); //Blacksmith
      ModObjectLimit(FourCC("h03Q"), UNLIMITED); //Garrison
      ModObjectLimit(FourCC("h052"), UNLIMITED); //Improved Guard Tower
      ModObjectLimit(FourCC("h04N"), UNLIMITED); //Improved Cannon Tower

      //Units
      ModObjectLimit(FourCC("hpea"), UNLIMITED); //Peasant
      ModObjectLimit(FourCC("n06K"), UNLIMITED); //Druid of the Scythe
      ModObjectLimit(FourCC("h04M"), UNLIMITED); //Cleric
      ModObjectLimit(FourCC("h04E"), UNLIMITED); //Protector
      ModObjectLimit(FourCC("n06L"), UNLIMITED); //Armored Wolf
      ModObjectLimit(FourCC("o01V"), 6); //Greyguard
      ModObjectLimit(FourCC("o02J"), 12); //Worgen
      ModObjectLimit(FourCC("h03L"), UNLIMITED); //Shotgunner
      ModObjectLimit(FourCC("n067"), UNLIMITED); //Spider summon
      ModObjectLimit(FourCC("o04U"), 6); //Cyclone Cannon
      ModObjectLimit(FourCC("o06P"), 6); //Worgen Shaman
      ModObjectLimit(FourCC("h04X"), 6); //HarvestWitch

      ModObjectLimit(FourCC("E01E"), 1); //Goldrinn
      ModObjectLimit(FourCC("Ewar"), 1); //Tess
      ModObjectLimit(FourCC("Hhkl"), 1); //Genn
      ModObjectLimit(FourCC("Hpb2"), 1); //Darius

      //Ships
      ModObjectLimit(FourCC("hbot"), UNLIMITED); //Alliance Transport Ship
      ModObjectLimit(FourCC("h0AR"), UNLIMITED); //Alliance Scout
      ModObjectLimit(FourCC("h0AX"), UNLIMITED); //Alliance Frigate
      ModObjectLimit(FourCC("h0B3"), UNLIMITED); //Alliance Fireship
      ModObjectLimit(FourCC("h0B0"), UNLIMITED); //Alliance Galley
      ModObjectLimit(FourCC("h0B6"), UNLIMITED); //Alliance Boarding
      ModObjectLimit(FourCC("h0AN"), UNLIMITED); //Alliance Juggernaut
      ModObjectLimit(FourCC("h0B7"), 6); //Alliance Bombard

      //Upgrades
      ModObjectLimit(Constants.UPGRADE_R04O_CLERIC_MASTER_TRAINING_GILNEAS, UNLIMITED);
      ModObjectLimit(Constants.UPGRADE_R04P_DRUID_OF_THE_SCYTHE_MASTER_TRAINING_GILNEAS, UNLIMITED);
      ModObjectLimit(Constants.UPGRADE_RHLH_IMPROVED_LUMBER_HARVESTING_ADVANCED_LUMBER_HARVESTING_YELLOW_PURPLE_ORANGE_GREEN_DARK_GREEN_RESEARCH, UNLIMITED);
      ModObjectLimit(Constants.UPGRADE_RHAC_IMPROVED_MASONRY_ADVANCED_MASONRY_IMBUED_MASONRY_YELLOW_PURPLE_ORANGE_GREEN_DARK_GREEN_RESEARCH, UNLIMITED);
      ModObjectLimit(Constants.UPGRADE_R09L_WORGEN_SHAMAN_MASTER_TRAINING_GILNEAS, UNLIMITED);
      ModObjectLimit(Constants.UPGRADE_R09M_HARVEST_WITCH_MASTER_TRAINING_GILNEAS, UNLIMITED);
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