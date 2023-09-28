using MacroTools;
using MacroTools.LegendSystem;
using static War3Api.Common;
#pragma warning disable CS1591

namespace WarcraftLegacies.Source.Setup.Legends
{
  /// <summary>
  /// Responsible for setting up and storing all Sentinel <see cref="Legend"/>s.
  /// </summary>
  public sealed class LegendSentinels
  {
    public LegendaryHero Maiev { get; }
    public LegendaryHero Tyrande { get; }
    public LegendaryHero Shandris { get; }
    public LegendaryHero Naisha { get; }
    public Capital Auberdine { get; }
    public Capital Feathermoon { get; }
    public Capital VaultOfTheWardens { get; }
    public Capital BlackrookHold { get; }
    
    public LegendSentinels(PreplacedUnitSystem preplacedUnitSystem)
    {
      Maiev = new LegendaryHero("Maiev Shadowsong")
      {
        UnitType = FourCC("Ewrd"),
        StartingXp = 1800
      };

      Auberdine = new Capital
      {
        Unit = preplacedUnitSystem.GetUnit(FourCC("e00J")),
        Essential = true
      };

      Feathermoon = new Capital
      {
        Unit = preplacedUnitSystem.GetUnit(FourCC("e00M")),
        Essential = true
      };

      Tyrande = new LegendaryHero("Tyrande Whisperwind")
      {
        UnitType = FourCC("Etyr"),
        PlayerColor = PLAYER_COLOR_CYAN,
        StartingXp = 400
      };

      Naisha = new LegendaryHero("Naisha")
      {
        UnitType = FourCC("E025"),
        PlayerColor = PLAYER_COLOR_PINK,
        StartingXp = 1800
      };

      Shandris = new LegendaryHero("Shandris Feathermoon")
      {
        UnitType = FourCC("E002"),
      };

      VaultOfTheWardens = new Capital
      {
        Unit = preplacedUnitSystem.GetUnit(Constants.UNIT_N04G_VAULT_OF_THE_WARDENS_SENTINELS)
      };
      
      BlackrookHold = new Capital
      {
        Unit = preplacedUnitSystem.GetUnit(Constants.UNIT_H03C_BLACK_ROOK_HOLD_NEUTRAL_HOSTILE)
      };
    }
    
    public void RegisterLegends()
    {
      LegendaryHeroManager.Register(Maiev);
      LegendaryHeroManager.Register(Tyrande);
      LegendaryHeroManager.Register(Shandris);
      LegendaryHeroManager.Register(Naisha);
      CapitalManager.Register(Auberdine);
      CapitalManager.Register(Feathermoon);
      CapitalManager.Register(BlackrookHold);
    }
  }
}