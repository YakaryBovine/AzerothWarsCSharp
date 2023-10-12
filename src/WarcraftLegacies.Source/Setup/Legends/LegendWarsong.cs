using MacroTools;
using MacroTools.LegendSystem;

namespace WarcraftLegacies.Source.Setup.Legends
{
  public sealed class LegendWarsong
  {
    public LegendaryHero GromHellscream { get; }
    public LegendaryHero ChenStormstout { get; }
    public LegendaryHero Saurfang { get; }
    public LegendaryHero Garrosh { get; }
    public LegendaryHero Mannoroth { get; }
    public Capital StonemaulKeep { get; }
    public Capital Orgrimmar { get; }
    public Capital LumberCamp { get; }
    
    public LegendWarsong(PreplacedUnitSystem preplacedUnitSystem)
    {
      GromHellscream = new LegendaryHero("Grom Hellscream")
      {
        UnitType = Constants.UNIT_OGRH_CHIEFTAIN_OF_THE_WARSONG_CLAN_WARSONG,
        StartingArtifactItemTypeIds = new()
        {
          Constants.ITEM_I01V_GOREHOWL
        }
      };
      
      ChenStormstout = new LegendaryHero("Chen Stormstout")
      {
        UnitType = Constants.UNIT_NSJS_BREWMASTER_WARSONG,
        StartingXp = 1000
      };

      Saurfang = new LegendaryHero("Varok Saurfang")
      {
        UnitType = Constants.UNIT_OBLA_HIGH_OVERLORD_OF_THE_KOR_KRON_VASSAL,
        StartingXp = 2800
      };

      Mannoroth = new LegendaryHero("Mannoroth")
      {
        UnitType = Constants.UNIT_NMAN_THE_DESTROYER_WARSONG_BLOODPACT,
        PermaDies = true,
        DeathMessage =
          "Mannoroth the Corrupter has fallen.",
        StartingXp = 41800
      };

      Garrosh = new LegendaryHero("Garrosh Hellscream")
      {
        UnitType = Constants.UNIT_O06L_WARLORD_OF_THE_WARSONG_CLAN_WARSONG,
        StartingXp = 8800
      };

      StonemaulKeep = new Capital
      {
        Unit = preplacedUnitSystem.GetUnit(Constants.UNIT_O004_STONEMAUL_KEEP),
        DeathMessage = "The fortress of the Stonemaul Clan has fallen.",
        Essential = true
      };

      Orgrimmar = new Capital
      {
        Unit = preplacedUnitSystem.GetUnit(Constants.UNIT_O01B_ORGRIMMAR_WARSONG),
        DeathMessage = "Orgrimmar has been demolished and with it die the hopes and dreams of a wartorn race seeking refuge in a new world.",
        Essential = true
      };
      
      LumberCamp = new Capital
      {
        Unit = preplacedUnitSystem.GetUnit(Constants.UNIT_O05R_WARSONG_LUMBER_CAMP_WARSONG),
        Essential = true
      };
    }
    
    public void RegisterLegends()
    {
      LegendaryHeroManager.Register(GromHellscream);
      LegendaryHeroManager.Register(ChenStormstout);
      LegendaryHeroManager.Register(Saurfang);
      LegendaryHeroManager.Register(Garrosh);
      CapitalManager.Register(StonemaulKeep);
      CapitalManager.Register(Orgrimmar);
      CapitalManager.Register(LumberCamp);
    }
  }
}