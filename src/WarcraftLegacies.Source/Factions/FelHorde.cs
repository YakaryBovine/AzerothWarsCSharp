using System.Collections.Generic;
using MacroTools;
using MacroTools.FactionSystem;
using WarcraftLegacies.Source.FactionMechanics.Fel_Horde;
using WarcraftLegacies.Source.Quests.Fel_Horde;
using WarcraftLegacies.Source.Setup;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Factions
{
  public sealed class FelHorde : Faction
  {
    private readonly PreplacedUnitSystem _preplacedUnitSystem;
    private readonly AllLegendSetup _allLegendSetup;

    /// <inheritdoc />
    public FelHorde(PreplacedUnitSystem preplacedUnitSystem, AllLegendSetup allLegendSetup) : base("Fel Horde",
      PLAYER_COLOR_GREEN, "|c0020c000", @"ReplaceableTextures\CommandButtons\BTNPitLord.blp")
    {
      _preplacedUnitSystem = preplacedUnitSystem;
      _allLegendSetup = allLegendSetup;
      UndefeatedResearch = FourCC("R05L");
      StartingGold = 200;
      StartingLumber = 700;
      CinematicMusic = "Doom";
      ControlPointDefenderUnitTypeId = Constants.UNIT_N0AA_CONTROL_POINT_DEFENDER_FEL_HORDE;
      IntroText = @"You are playing as the bloodthirsty Fel Horde.

You begin in Nagrand, cut off from your forces in Hellfire Citadel. You must raise an army and quickly conquer Outland.

Once Outland is under your control, gather your hordes and prepare to invade Azeroth through the Dark Portal.

The Alliance is gathering outside the Dark Portal to stop you, so prepare to for a very hard breakout.";
      FoodMaximum = 250;
      GoldMines = new List<unit>
      {
        _preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(-2735, -30242))
      };
      RegisterFactionDependentInitializer<Stormwind, Illidari>(RegisterStormwindIllidariQuests);
    }

    /// <inheritdoc />
    public override void OnRegistered()
    {
      RegisterObjectLimits();
      RegisterQuests();
      JuggernautDeath.Setup(_preplacedUnitSystem);
      SharedFactionConfigSetup.AddSharedFactionConfig(this);
    }

    private void RegisterObjectLimits()
    {
      ModObjectLimit(FourCC("o02Y"), UNLIMITED); //Great Hall
      ModObjectLimit(FourCC("o02Z"), UNLIMITED); //Stronghold
      ModObjectLimit(FourCC("o030"), UNLIMITED); //Fortress
      ModObjectLimit(FourCC("o02V"), UNLIMITED); //Altar of Storms
      ModObjectLimit(FourCC("o02W"), UNLIMITED); //Barracks
      ModObjectLimit(FourCC("o031"), UNLIMITED); //War Mill
      ModObjectLimit(FourCC("o033"), UNLIMITED); //Spirit Lodge
      ModObjectLimit(FourCC("o02X"), UNLIMITED); //Bestiary
      ModObjectLimit(FourCC("o032"), UNLIMITED); //Shipyard
      ModObjectLimit(FourCC("o034"), UNLIMITED); //Watch Tower
      ModObjectLimit(FourCC("o035"), UNLIMITED); //Improved Watch Tower
      ModObjectLimit(FourCC("u00Q"), UNLIMITED); //Hellforge
      ModObjectLimit(FourCC("n0AM"), UNLIMITED); //Boulder Tower
      ModObjectLimit(FourCC("n0AN"), UNLIMITED); //Advanced Boulder Tower
      ModObjectLimit(FourCC("ocbw"), UNLIMITED); //Burrow
      ModObjectLimit(FourCC("n0AP"), UNLIMITED); //Focal Demon Gate

      ModObjectLimit(FourCC("nbdk"), 6); //Black Drake
      ModObjectLimit(FourCC("odkt"), 6); //Eredar Warlock
      ModObjectLimit(FourCC("nchw"), UNLIMITED); //Fel Orc Warlock
      ModObjectLimit(FourCC("nchg"), UNLIMITED); //Fel Orc Grunt
      ModObjectLimit(FourCC("nchr"), UNLIMITED); //Fel Orc Raider
      ModObjectLimit(FourCC("ncpn"), UNLIMITED); //Fel Orc Peon
      ModObjectLimit(FourCC("owar"), 12); //Horde Cavarly
      ModObjectLimit(FourCC("o01L"), 6); //Executioner
      ModObjectLimit(FourCC("o01O"), 8); //Demolisher
      ModObjectLimit(FourCC("u018"), 10); //Eye of Grillok
      ModObjectLimit(FourCC("u00V"), UNLIMITED); //Necrolyte
      ModObjectLimit(FourCC("n058"), UNLIMITED); //Troll Axethrowers
      ModObjectLimit(Constants.UNIT_NINA_INFERNAL_JUGGERNAUT_FEL_HORDE, 4);
      ModObjectLimit(Constants.UNIT_N086_FEL_DEATH_KNIGHT_FEL_HORDE_ELITE_TIER, 6);

      //Ship
      ModObjectLimit(FourCC("obot"), UNLIMITED); //Transport Ship
      ModObjectLimit(FourCC("h0AS"), UNLIMITED); //Scout
      ModObjectLimit(FourCC("h0AP"), UNLIMITED); //Frigate
      ModObjectLimit(FourCC("h0B2"), UNLIMITED); //Fireship
      ModObjectLimit(FourCC("h0AY"), UNLIMITED); //Galley
      ModObjectLimit(FourCC("h0B5"), UNLIMITED); //Boarding
      ModObjectLimit(FourCC("h0BC"), UNLIMITED); //Juggernaut
      ModObjectLimit(FourCC("h0AO"), 6); //Bombard

      ModObjectLimit(FourCC("n05T"), 1); //Kazzak
      ModObjectLimit(FourCC("n08A"), 1); //neltharaktu
      ModObjectLimit(FourCC("N03D"), 1); //Kargath
      ModObjectLimit(FourCC("Nbbc"), 1); //Rend
      ModObjectLimit(FourCC("U02D"), 1); //Teron
      ModObjectLimit(FourCC("Nmag"), 1); //Magtheridon

      ModObjectLimit(FourCC("Robf"), UNLIMITED); //Demonic Flux
      ModObjectLimit(FourCC("R066"), UNLIMITED); //Burning Oil
      ModObjectLimit(FourCC("R00O"), UNLIMITED); //Ensnare
      ModObjectLimit(FourCC("Rorb"), UNLIMITED); //Reinforced Defenses
      ModObjectLimit(FourCC("Rosp"), UNLIMITED); //Spiked Barricades
      ModObjectLimit(FourCC("R000"), -UNLIMITED); //Skeletal Perserverance
      ModObjectLimit(FourCC("R024"), UNLIMITED); //Necrolyte adept Training
      ModObjectLimit(FourCC("R00M"), UNLIMITED); //Warlock Adept Training
      ModObjectLimit(FourCC("R03I"), UNLIMITED); //Eredar Warlock Adept Training
      ModObjectLimit(FourCC("R00Y"), UNLIMITED); //Improved Self-Bloodlust
      ModObjectLimit(FourCC("R03L"), UNLIMITED); //Improved Shadow Infusion
      ModObjectLimit(FourCC("R036"), UNLIMITED); //Incinerate
      ModObjectLimit(FourCC("R02L"), UNLIMITED); //Bloodcraze
      ModObjectLimit(FourCC("R03O"), UNLIMITED); //Bloodcraze
      ModObjectLimit(FourCC("R034"), UNLIMITED); //Enhanced Breath
      ModObjectLimit(FourCC("R035"), UNLIMITED); //Improved Firebolt
      ModObjectLimit(FourCC("R01Z"), UNLIMITED); //Battle Stations
      ModObjectLimit(Constants.UPGRADE_R098_FEL_INFUSED_SKELETON_FEL_HORDE, UNLIMITED);
      ModObjectLimit(Constants.UPGRADE_R09W_IMPROVED_GREATER_CARRION_SWARM_LEGION, UNLIMITED); 
      SetObjectLevel(FourCC("R01Z"), 1); //Battle Stations
      
      ModObjectLimit(FourCC("n05R"), UNLIMITED); //Felguard
      ModObjectLimit(FourCC("n06H"), UNLIMITED); //Pit Fiend
      ModObjectLimit(FourCC("n07B"), UNLIMITED); //Queen
      ModObjectLimit(FourCC("n07D"), UNLIMITED); //Maiden
      ModObjectLimit(FourCC("n07o"), UNLIMITED); //Terror
      ModObjectLimit(FourCC("n07N"), UNLIMITED); //Lord
      
      ModAbilityAvailability(Constants.ABILITY_A0MZ_DEMONIC_CONSTRUCTION_TEAL_DEMOLISHERS, -1);
      ModAbilityAvailability(Constants.ABILITY_A0GM_FOR_THE_HORDE_PINK_GREY_MAIN_BUILDINGS, -1);

      ModObjectLimit(FourCC("R090"), UNLIMITED); //Blackrock
    }

    private void RegisterQuests()
    {
      var questHellfireCitadel = AddQuest(new QuestHellfireCitadel(Regions.HellfireUnlock));
      AddQuest(new QuestRuinsofShadowmoon(Regions.ShadowmoonBaseUnlock));
      AddQuest(new QuestBlackrock(Regions.BlackrockUnlock, Regions.DarkPortalUnlock, new[] { questHellfireCitadel }));
      AddQuest(new QuestFelHordeKillIronforge(_allLegendSetup.Ironforge.GreatForge));
      AddQuest(new QuestFelHordeKillStormwind(_allLegendSetup.Stormwind.StormwindKeep));
      AddQuest(new QuestGuldansLegacy());
    }
    
    private void RegisterStormwindIllidariQuests(Stormwind stormwind, Illidari illidari)
    {
      AddQuest(new QuestDarkPortal(
        _preplacedUnitSystem.GetUnit(Constants.UNIT_N036_DARK_PORTAL_WAYGATE, Regions.Dark_Portal_Entrance_1.Center),
        _preplacedUnitSystem.GetUnit(Constants.UNIT_N036_DARK_PORTAL_WAYGATE, Regions.Dark_Portal_Entrance_2.Center),
        _preplacedUnitSystem.GetUnit(Constants.UNIT_N036_DARK_PORTAL_WAYGATE, Regions.Dark_Portal_Entrance_3.Center),
        _preplacedUnitSystem.GetUnit(Constants.UNIT_N036_DARK_PORTAL_WAYGATE, Regions.Dark_Portal_Exit_1.Center),
        _preplacedUnitSystem.GetUnit(Constants.UNIT_N036_DARK_PORTAL_WAYGATE, Regions.Dark_Portal_Exit_2.Center),
        _preplacedUnitSystem.GetUnit(Constants.UNIT_N036_DARK_PORTAL_WAYGATE, Regions.Dark_Portal_Exit_3.Center), stormwind, illidari));
    }
  }
}