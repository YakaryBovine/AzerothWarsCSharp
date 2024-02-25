using System.Collections.Generic;
using MacroTools;
using MacroTools.DialogueSystem;
using MacroTools.Extensions;
using MacroTools.FactionChoices;
using MacroTools.FactionSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.Powers;
using WarcraftLegacies.Source.Quests.Goblin;
using WarcraftLegacies.Source.Setup;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Factions
{
  public sealed class Bilgewater : Faction
  {
    private readonly AllLegendSetup _allLegendSetup;

    /// <inheritdoc />
    public Bilgewater(PreplacedUnitSystem preplacedUnitSystem, AllLegendSetup allLegendSetup) : base("Bilgewater", PLAYER_COLOR_LIGHT_GRAY, "|cff808080",
      @"ReplaceableTextures\CommandButtons\BTNHeroTinker.blp")
    {
      _allLegendSetup = allLegendSetup;
      StartingGold = 200;
      StartingLumber = 700;
      ControlPointDefenderUnitTypeId = Constants.UNIT_O01C_CONTROL_POINT_DEFENDER_GOBLIN;
      StartingUnits = Regions.GoblinStartPos.PrepareUnitsForRescue(RescuePreparationMode.Invulnerable);
      LearningDifficulty = FactionLearningDifficulty.Advanced;
      IntroText = @"You are playing as the industrious |cff808080Bilgewater Cartel|r.

You begin in Tanaris with a very small business venture. Expand onto Kalimdor to grow your trade empire.

Your advanced units require Oil to function. Use oil ships to find oil deposits in the Great Sea and build platforms on them.

The Trading Center in Kezan will unlock the ability to train Traders. Be sure to protect the Trading Center once you unlock it, as it will form the backbone of your Goblin Empire.";
      GoldMines = new List<unit>
      {
        preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(-8615, -12869)), //Starting
        preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(-730, -6777))    //Kezan
      };
    }
        
    /// <inheritdoc />
    public override void OnRegistered()
    {
      RegisterObjectLimits();
      RegisterQuests();
      RegisterDialogue();
      RegisterPowers();
      SharedFactionConfigSetup.AddSharedFactionConfig(this);
    }

    /// <inheritdoc />
    public override void OnNotPicked()
    {
      Regions.KezanUnlock.CleanupNeutralPassiveUnits();
      base.OnNotPicked();
    }
    
    private void RegisterObjectLimits()
    {
      ModObjectLimit(FourCC("o03L"), UNLIMITED); //Great Hall
      ModObjectLimit(FourCC("o03M"), UNLIMITED); //Stronghold
      ModObjectLimit(FourCC("o03N"), UNLIMITED); //Fortress
      ModObjectLimit(FourCC("o03O"), UNLIMITED); //Altar of Storms
      ModObjectLimit(FourCC("o03P"), UNLIMITED); //Barracks
      ModObjectLimit(FourCC("o05T"), UNLIMITED); //Boot Camp
      ModObjectLimit(FourCC("o03Q"), UNLIMITED); //War Mill
      ModObjectLimit(FourCC("o03S"), UNLIMITED); //Tauren Totem
      ModObjectLimit(FourCC("o01M"), UNLIMITED); //Spirit Lodge
      ModObjectLimit(FourCC("o03T"), UNLIMITED); //Orc Burrow
      ModObjectLimit(FourCC("o03U"), UNLIMITED); //Watch Tower
      ModObjectLimit(FourCC("o03W"), UNLIMITED); //Improved Watch Tower
      ModObjectLimit(FourCC("o03X"), UNLIMITED); //Voodoo Lounge
      ModObjectLimit(FourCC("o03V"), UNLIMITED); //Shipyard
      ModObjectLimit(FourCC("h011"), 1); //Artillery

      //Ship
      ModObjectLimit(Constants.UNIT_O06G_OIL_RIG_CONSTRUCTOR_GOBLIN, UNLIMITED);
      ModObjectLimit(FourCC("h0AS"), UNLIMITED); //Scout
      ModObjectLimit(FourCC("h0AP"), UNLIMITED); //Frigate
      ModObjectLimit(FourCC("h0B2"), UNLIMITED); //Fireship
      ModObjectLimit(FourCC("h0AY"), UNLIMITED); //Galley
      ModObjectLimit(FourCC("h0B5"), UNLIMITED); //Boarding
      ModObjectLimit(FourCC("h0BC"), UNLIMITED); //Juggernaut
      ModObjectLimit(FourCC("h0AO"), 6); //Bombard

      ModObjectLimit(Constants.UNIT_O02I_BUILDER_GOBLIN_WORKER, UNLIMITED);
      ModObjectLimit(Constants.UNIT_N099_OGRE_MERCENARY_GOBLIN, UNLIMITED);
      ModObjectLimit(Constants.UNIT_H08X_SAPPERS_GOBLIN, 8);
      ModObjectLimit(Constants.UNIT_H08Y_GUNNER_GOBLIN, UNLIMITED);
      ModObjectLimit(Constants.UNIT_U02R_HOBGOBLIN_GOBLIN, UNLIMITED);
      ModObjectLimit(Constants.UNIT_H09I_PERSONAL_TANK_GOBLIN, 12);
      ModObjectLimit(Constants.UNIT_H09J_GRENADIER_GOBLIN, 12);
      ModObjectLimit(Constants.UNIT_ODOC_WITCH_DOCTOR_FROSTWOLF, UNLIMITED);
      ModObjectLimit(Constants.UNIT_O04O_ALCHEMIST_GOBLIN, UNLIMITED);
      ModObjectLimit(Constants.UNIT_O04Q_TINKER_GOBLIN, 6);
      ModObjectLimit(Constants.UNIT_ODES_FRIGATE_WARSONG_FROSTWOLF_FEL_HORDE, UNLIMITED);
      ModObjectLimit(Constants.UNIT_OJGN_JUGGERNAUT_WARSONG_FROSTWOLF_FEL_HORDE, 6);
      ModObjectLimit(Constants.UNIT_N062_SHREDDER_GOBLIN, 12);
      ModObjectLimit(Constants.UNIT_H08Z_ASSAULT_TANK_GOBLIN, 5);
      ModObjectLimit(Constants.UNIT_H091_WAR_ZEPPELIN_GOBLIN, 6);
      ModObjectLimit(Constants.UNIT_H09H_SIEGE_WALKER_GOBLIN, 5);
      ModObjectLimit(Constants.UNIT_NZEP_TRADING_ZEPPELIN_WARSONG, 16);
      ModObjectLimit(Constants.UNIT_O04S_TRADER_GOBLIN, 10);

      ModObjectLimit(Constants.UNIT_O04N_TRADE_PRINCE_OF_THE_BILGEWATER_CARTEL_GOBLIN, 1);
      ModObjectLimit(Constants.UNIT_NTIN_CHIEF_ENGINEER_GOBLIN, 1);
      ModObjectLimit(Constants.UNIT_VH01_BARON_OF_GADGETZAN_GOBLIN, 1);

      ModObjectLimit(Constants.UPGRADE_R07M_ALCHEMIST_GRANDMASTER_TRAINING_GOBLIN, UNLIMITED);
      ModObjectLimit(Constants.UPGRADE_R097_FORTIFIED_HULLS_GOBLIN, UNLIMITED);
    }

    private void RegisterQuests()
    {
      StartingQuest = AddQuest(new QuestKezan());
      AddQuest(new QuestExplosiveEngineering());
      AddQuest(new QuestRatchet());
      AddQuest(new QuestWesternExpansion(new [] { _allLegendSetup.Sentinels.Auberdine, _allLegendSetup.Sentinels.Feathermoon }));
      AddQuest(new QuestLumberMarket(_allLegendSetup.Druids.Nordrassil));
      AddQuest(new QuestBusinessExpansion());
      AddQuest(new QuestGoblinEmpire());
    }

    private void RegisterDialogue()
    {
      TriggeredDialogueManager.Add(
        new TriggeredDialogue(new Dialogue(
            @"Sound\Dialogue\OrcExpCamp\OrcQuest03x\D03Gazlowe01",
            "Ah, new guy, huh? I'm Gazlowe, chief engineer around these parts. But enough about me. We got work to do, buddy!",
            "Gazlowe")
          , new[]
          {
            this
          }, new[]
          {
            new ObjectiveControlLegend(_allLegendSetup.Goblin.Gazlowe, false)
            {
              EligibleFactions = new List<Faction>{ this }
            }
          }));
    }
    
    private void RegisterPowers()
    {
      var oilPower = new OilPower
      {
        Name = "Oil Tycoon",
        StartingRandomOilPoolCount = 3,
        MaximumOilPoolCount = 15,
        OilPoolMinimumValue = 1500,
        OilPoolMaximumValue = 9000,
        OilPoolBorderDistance = 600,
        ForcedStartingOilPoolSpawnLocations = new [] { new Point(-4825f, -282f) }
      };
      AddPower(oilPower);
    }
  }
}