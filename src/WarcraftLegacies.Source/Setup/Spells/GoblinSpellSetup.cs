using MacroTools.PassiveAbilities;
using MacroTools.PassiveAbilitySystem;
using MacroTools.SpellSystem;
using WarcraftLegacies.Source.FactionMechanics.Goblins;
using WarcraftLegacies.Source.Spells;

namespace WarcraftLegacies.Source.Setup.Spells
{
  public static class GoblinSpellSetup
  {
    public static void Setup()
    {
      var zeppelinTradeTargets = new[]
      {
        Regions.Trade1.Center,
        Regions.Trade2.Center,
        Regions.Trade3.Center,
        Regions.Trade4.Center
      };
      PassiveAbilityManager.Register(new Trader(Constants.UNIT_NZEP_TRADING_ZEPPELIN_WARSONG, 15, 0, zeppelinTradeTargets));

      var traderTradeTargets = new[]
      {
        Regions.Trader1.Center,
        Regions.Trader2.Center,
        Regions.Trader3.Center
      };
      PassiveAbilityManager.Register(new Trader(Constants.UNIT_O04S_TRADER_GOBLIN, 25, 0, traderTradeTargets));

      SpellSystem.Register(new NuclearLaunch(Constants.ABILITY_A0RH_INTERCONTINENTAL_BOMBARDMENT_GOBLIN_ARTILLERY, 25));

      PassiveAbilityManager.Register(new NuclearLaunchWarning(Constants.UNIT_H011_INTERCONTINENTAL_ARTILLERY_GOBLIN_TOWER,
        Constants.UNIT_H06L_DUMMY_NUKE_WARNING, @"war3mapImported/NuclearLaunchDetected.mp3", 25));
      
      PassiveAbilityManager.Register(new AnimationSpeedMultiplier(Constants.UNIT_H011_INTERCONTINENTAL_ARTILLERY_GOBLIN_TOWER, 0.4f));
      
      PassiveAbilityManager.Register(new OilUser(Constants.UNIT_H011_INTERCONTINENTAL_ARTILLERY_GOBLIN_TOWER));
      PassiveAbilityManager.Register(new OilUser(Constants.UNIT_NTIN_CHIEF_ENGINEER_GOBLIN));
      PassiveAbilityManager.Register(new OilUser(Constants.UNIT_N062_SHREDDER_GOBLIN));
      PassiveAbilityManager.Register(new OilUser(Constants.UNIT_H08Z_ASSAULT_TANK_GOBLIN));
      PassiveAbilityManager.Register(new OilUser(Constants.UNIT_H09H_SIEGE_WALKER_GOBLIN));
      PassiveAbilityManager.Register(new OilUser(Constants.UNIT_O01M_ENGINEER_S_GUILD_GOBLIN_SPECIALIST));
      PassiveAbilityManager.Register(new OilUser(Constants.UNIT_NTIN_CHIEF_ENGINEER_GOBLIN));
      PassiveAbilityManager.Register(new OilUser(Constants.UNIT_H09I_PERSONAL_TANK_GOBLIN));
      PassiveAbilityManager.Register(new OilProducer(Constants.UNIT_H04Z_KEZAN_OIL_SUPPLY_GOBLIN_OTHER)
      {
        OilProducedPerSecond = 20
      });
      PassiveAbilityManager.Register(new OilHarvester(Constants.UNIT_O04R_OIL_RIG_GOBLIN_OTHER)
      {
        OilHarvestedPerSecond = 5,
        Radius = 400
      });

      SpellSystem.Register(new PingOilDeposits(Constants.ABILITY_A0PJ_LOCATE_OIL_GOBLIN_OIL_RIG_CONSTRUCTOR)
      {
        Duration = 10
      });

      PassiveAbilityManager.Register(new ProvidesIncome(Constants.UNIT_NBOT_GOBLIN_PRIVATEER_NEUTRAL_GOBLIN, -2));
      PassiveAbilityManager.Register(new ProvidesIncome(Constants.UNIT_NGIR_SHREDDER_GOBLIN_SHOP, -2));
    }
  }
}