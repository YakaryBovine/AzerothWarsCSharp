using MacroTools;
using MacroTools.LegendSystem;
using static War3Api.Common;
#pragma warning disable CS1591

namespace WarcraftLegacies.Source.Setup.Legends
{
  public sealed class LegendNeutral
  {
    public LegendaryHero Ragnaros { get; }
    public Capital Auchindoun { get; }
    public Capital DraktharonKeep { get; }
    public Capital Oshugun { get; }
    public Capital Jinthaalor { get; }
    public Capital ShrineOfUlatek { get; }
    public Capital Seradane { get; }
    public Capital Zulgurub { get; }
    public Capital Dazaralor { get; }
    public Capital Gundrak { get; }
    public Capital DuskwoodGraveyard { get; }
    public Capital GrimBatol { get; }
    public Capital Ethelrethor { get; }
    public Capital TheNexus { get; }
    public Capital Karazhan { get; }
    public Capital Zulfarrak { get; }
    public Capital FountainOfBlood { get; }
    public LegendaryHero Vaelastrasz { get; }
    public Capital Caerdarrow { get; }
    public Capital Shaladrassil { get; }
    
    public LegendNeutral(PreplacedUnitSystem preplacedUnitSystem)
    {
      Ragnaros = new LegendaryHero("Ragnaros")
      {
        UnitType = Constants.UNIT_N00D_PRIMARY_FIRELORD_CREEP,
        DeathMessage = "Ragnaros, the King of Fire and Lord of the Firelands, has been extinguished.",
        StartingArtifactItemTypeIds = new()
        {
          Constants.ITEM_I00H_SULFURAS_HAND_OF_RAGNAROS
        },
        StartingXp = 15404
      };

      Auchindoun = new Capital
      {
        Capturable = true,
        Unit = preplacedUnitSystem.GetUnit(FourCC("h026"))
      };

      DraktharonKeep = new Capital
      {
        Capturable = true,
        Unit = preplacedUnitSystem.GetUnit(FourCC("o016"))
      };

      Oshugun = new Capital
      {
        Capturable = true,
        Unit = preplacedUnitSystem.GetUnit(FourCC("h02Z"))
      };

      Jinthaalor = new Capital
      {
        Capturable = true,
        Unit = preplacedUnitSystem.GetUnit(Constants.UNIT_O02G_JINTHA_ALOR)
      };

      ShrineOfUlatek = new Capital
      {
        Capturable = true,
        Unit = preplacedUnitSystem.GetUnit(FourCC("o00Q"))
      };

      Seradane = new Capital
      {
        Capturable = true,
        Unit = preplacedUnitSystem.GetUnit(FourCC("e014"))
      };

      Zulgurub = new Capital
      {
        Capturable = true,
        Unit = preplacedUnitSystem.GetUnit(FourCC("o018"))
      };

      Dazaralor = new Capital
      {
        Capturable = true,
        Unit = preplacedUnitSystem.GetUnit(FourCC("o00V")),
        Essential = true
      };

      Gundrak = new Capital
      {
        Capturable = true,
        Unit = preplacedUnitSystem.GetUnit(FourCC("o00N"))
      };

      DuskwoodGraveyard = new Capital
      {
        Capturable = true,
        Unit = preplacedUnitSystem.GetUnit(FourCC("h01F"))
      };

      GrimBatol = new Capital
      {
        Capturable = true,
        Unit = preplacedUnitSystem.GetUnit(FourCC("h01Z"))
      };

      Ethelrethor = new Capital
      {
        Capturable = true,
        Unit = preplacedUnitSystem.GetUnit(FourCC("h05I"))
      };

      TheNexus = new Capital
      {
        Unit = preplacedUnitSystem.GetUnit(FourCC("h04P")),
        Capturable = true
      };

      Karazhan = new Capital
      {
        Unit = preplacedUnitSystem.GetUnit(FourCC("h00G")),
        Capturable = true
      };

      Zulfarrak = new Capital
      {
        Capturable = true,
        Unit = preplacedUnitSystem.GetUnit(FourCC("o00K"))
      };

      FountainOfBlood = new Capital
      {
        Capturable = true,
        Unit = preplacedUnitSystem.GetUnit(FourCC("nbfl"))
      };

      Vaelastrasz = new LegendaryHero("Vaelastrasz")
      {
        Unit = preplacedUnitSystem.GetUnit(FourCC("nrwm"))
      };

      Caerdarrow = new Capital
      {
        Unit = preplacedUnitSystem.GetUnit(FourCC("u01M")),
        Capturable = true
      };

      Shaladrassil = new Capital
      {
        Unit = preplacedUnitSystem.GetUnit(Constants.UNIT_E01W_SHALADRASSIL_DRUID_OTHER),
        Capturable = true
      };
    }
    
    public void RegisterLegends()
    {
      LegendaryHeroManager.Register(Ragnaros);
      LegendaryHeroManager.Register(Vaelastrasz);
      CapitalManager.Register(Auchindoun);
      CapitalManager.Register(DraktharonKeep);
      CapitalManager.Register(Oshugun);
      CapitalManager.Register(Jinthaalor);
      CapitalManager.Register(ShrineOfUlatek);
      CapitalManager.Register(Seradane);
      CapitalManager.Register(Zulgurub);
      CapitalManager.Register(Dazaralor);
      CapitalManager.Register(Gundrak);
      CapitalManager.Register(DuskwoodGraveyard);
      CapitalManager.Register(GrimBatol);
      CapitalManager.Register(Ethelrethor);
      CapitalManager.Register(TheNexus);
      CapitalManager.Register(Karazhan);
      CapitalManager.Register(Zulfarrak);
      CapitalManager.Register(FountainOfBlood);
      CapitalManager.Register(Caerdarrow);
      CapitalManager.Register(Shaladrassil);
    }
  }
}