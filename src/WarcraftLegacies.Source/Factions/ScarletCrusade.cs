using MacroTools.FactionSystem;
using WarcraftLegacies.Source.Quests.Scarlet;
using WarcraftLegacies.Source.Setup;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Factions
{
  public sealed class ScarletCrusade : Faction
  {
    private readonly AllLegendSetup _allLegendSetup;

    /// <inheritdoc />
    public ScarletCrusade(AllLegendSetup allLegendSetup) : base("Scarlet Crusade", PLAYER_COLOR_MAROON, "|cff800000",
      "ReplaceableTextures/CommandButtons/BTNScarletKnight.blp")
    {
      _allLegendSetup = allLegendSetup;
      StartingGold = 200;
      StartingLumber = 500;
      ControlPointDefenderUnitTypeId = Constants.UNIT_H09O_CONTROL_POINT_DEFENDER_CRUSADE;
      IntroText = @"You are playing as the zealous |cff940000Scarlet Crusade|r.

The Cult of the Damned has mobilized and is quietly spreading corruption throughout Lordaeron.

Build towers to detect the hidden cultists moving through the Kingdom and burn any buildings they have corrupted.

Your soldiers are weaker when alone, but gain substantial bonuses when paired with a variety of unit-types. 

Fortify your strongholds against the storm to come and make ready to unleash the Crusade on those who would defile your lands.";
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
      ModObjectLimit(Constants.UNIT_H0BM_TOWN_HALL_CRUSADE_T1, UNLIMITED);
      ModObjectLimit(Constants.UNIT_H0BN_KEEP_CRUSADE_T2, UNLIMITED);
      ModObjectLimit(Constants.UNIT_H0BO_CASTLE_CRUSADE_T3, UNLIMITED);
      ModObjectLimit(Constants.UNIT_H0BP_FARMSTEAD_CRUSADE_FARM, UNLIMITED);
      ModObjectLimit(Constants.UNIT_H0A3_BLACKSMITH_CRUSADE_RESEARCH, UNLIMITED);
      ModObjectLimit(Constants.UNIT_H09X_SHIPYARD_CRUSADE_SHIPYARD, UNLIMITED);
      ModObjectLimit(Constants.UNIT_H0AG_HALL_OF_SWORDS_CRUSADE_BARRACKS, UNLIMITED);
      ModObjectLimit(Constants.UNIT_H0BE_STUDIUM_CRUSADE_MAGIC, UNLIMITED);
      ModObjectLimit(Constants.UNIT_H0BL_ROOKERY_CRUSADE_BEAST, UNLIMITED);
      ModObjectLimit(Constants.UNIT_N035_DIVINE_BASTION_CRUSADE_SPECIAL, 1);
      ModObjectLimit(Constants.UNIT_H0BI_BOMBARD_TOWER_CRUSADE_TOWER, UNLIMITED);
      ModObjectLimit(Constants.UNIT_H0BJ_IMPROVED_BOMBARD_TOWER_CRUSADE_TOWER, UNLIMITED);
      ModObjectLimit(Constants.UNIT_H0BQ_ALTAR_OF_CRUSADERS_CRUSADE_ALTAR, UNLIMITED);
      ModObjectLimit(Constants.UNIT_N0D8_VENDOR_HALL_CRUSADE_SHOP, UNLIMITED);

      //Units
      ModObjectLimit(Constants.UNIT_H022_FARMER_DALARAN_WORKER, UNLIMITED);
      ModObjectLimit(Constants.UNIT_H06B_TEMPLAR_LORDAERON, 6);
      ModObjectLimit(Constants.UNIT_H08I_CRUSADER_SCARLET, UNLIMITED);
      ModObjectLimit(Constants.UNIT_H09P_LONGBOWMAN_SCARLET, UNLIMITED);
      ModObjectLimit(Constants.UNIT_H08L_CAVALIER_SCARLET, UNLIMITED);
      ModObjectLimit(Constants.UNIT_N068_INQUISITOR_SCARLET, UNLIMITED);
      ModObjectLimit(Constants.UNIT_H08K_MONK_SCARLET, UNLIMITED);
      ModObjectLimit(Constants.UNIT_N09N_BISHOP_OF_THE_LIGHT_SCARLET, 6);
      ModObjectLimit(Constants.UNIT_H09Q_CRIMSON_COMMANDER_SCARLET, 8);
      ModObjectLimit(Constants.UNIT_O06V_EAGLE_RIDER_SCARLET, 6);
      ModObjectLimit(Constants.UNIT_E01L_GRYPHON_MARKSMAN_SCARLET, 6);
      ModObjectLimit(Constants.UNIT_O04C_SERAPHIM_SCARLET, 6);
      ModObjectLimit(Constants.UNIT_O00X_ARCHANGEL_SCARLET, 3);

      //Heroes
      ModObjectLimit(Constants.UNIT_H08G_GRAND_CRUSADER_SCARLET, 1);
      ModObjectLimit(Constants.UNIT_H0A2_SCARLET_COMMANDER_SCARLET, 1);
      ModObjectLimit(Constants.UNIT_H08H_HIGH_INQUISITOR_SCARLET, 1);
      ModObjectLimit(Constants.UNIT_H00Y_HIGH_GENERAL_SCARLET, 1);

      //Ships
      ModObjectLimit(Constants.UNIT_HBOT_TRANSPORT_SHIP_ALLIANCE, UNLIMITED);
      ModObjectLimit(Constants.UNIT_H0AR_SCOUT_SHIP_ALLIANCE, UNLIMITED);
      ModObjectLimit(Constants.UNIT_H0AX_FRIGATE_ALLIANCE, UNLIMITED);
      ModObjectLimit(Constants.UNIT_H0B3_FIRESHIP_ALLIANCE, UNLIMITED);
      ModObjectLimit(Constants.UNIT_H0B0_GALLEY_ALLIANCE, UNLIMITED);
      ModObjectLimit(Constants.UNIT_H0B6_BOARDING_VESSEL_ALLIANCE, UNLIMITED);
      ModObjectLimit(Constants.UNIT_H0AN_JUGGERNAUT_ALLIANCE, UNLIMITED);
      ModObjectLimit(Constants.UNIT_H0B7_BOMBARD_ALLIANCE, 6);

      //Upgrades
      ModObjectLimit(Constants.UPGRADE_R05D_MONK_MASTER_TRAINING_SCARLET, UNLIMITED);
      ModObjectLimit(Constants.UPGRADE_R04F_INQUISITOR_MASTER_TRAINING_SCARLET_CRUSADE, UNLIMITED);
      ModObjectLimit(Constants.UPGRADE_RHSE_MAGIC_SENTRY, UNLIMITED);
      ModObjectLimit(Constants.UPGRADE_RHLH_IMPROVED_LUMBER_HARVESTING_ADVANCED_LUMBER_HARVESTING_YELLOW_PURPLE_ORANGE_GREEN_DARK_GREEN_RESEARCH, UNLIMITED);
      ModObjectLimit(Constants.UPGRADE_RHAC_IMPROVED_MASONRY_ADVANCED_MASONRY_IMBUED_MASONRY_YELLOW_PURPLE_ORANGE_GREEN_DARK_GREEN_RESEARCH, UNLIMITED);
    }

    private void RegisterQuests()
    {
      var questStratholme = new QuestRebuildStratholme(Regions.StratholmeUnlock, _allLegendSetup.Scarlet.Saiden);
      AddQuest(questStratholme);

      var questCapital = new QuestReconquerCapital(Regions.Terenas, _allLegendSetup.Lordaeron.CapitalPalace,
        _allLegendSetup.Scarlet.Saiden, _allLegendSetup.Scarlet.Renault, _allLegendSetup.Scarlet.Sally,
        _allLegendSetup.Scarlet.Brigitte);
      AddQuest(questCapital);

      var questHearthglen = new QuestRebuildHearthglen(Regions.Hearthglen, _allLegendSetup.Lordaeron.Monastery);
      AddQuest(questHearthglen);

      var questBrill = new QuestRebuildBrill(Regions.Brill, _allLegendSetup.Scarlet.Renault);
      AddQuest(questBrill);

      var questAndorhal = new QuestRebuildAndorhal(Regions.Andorhal);
      AddQuest(questAndorhal);

      var questReconquerLordaeron =
        new QuestReconquerLordaeron(questStratholme, questCapital, questHearthglen, questBrill, questAndorhal);
      AddQuest(questReconquerLordaeron);
      StartingQuest = questReconquerLordaeron;

      var questOnslaught = new QuestOnslaught(Regions.Central_Northrend);
      AddQuest(questOnslaught);

      AddQuest(new QuestCrimsonCathedral(questOnslaught, _allLegendSetup.Scarlet.CrimsonCathedral));
    }
  }
}