using MacroTools.Extensions;
using static WarcraftLegacies.Source.AmbianceLibrary;
using static War3Api.Blizzard;

namespace WarcraftLegacies.Source.Setup
{
  /// <summary>
  /// Responsible for adding sound ambience to regions across the map.
  /// </summary>
  public static class AmbianceSetup
  {
    /// <summary>
    /// Adds ambient sounds to regions across the map.
    /// </summary>
    public static void Setup()
    {
      StopDefaultAmbientSounds();
      SetupEasternKingdoms();
      SetupKalimdor();
    }


    private static void StopDefaultAmbientSounds()
    {
      StopSound(bj_dayAmbientSound, true, true);
      StopSound(bj_nightAmbientSound, true, true);
    }

    private static void SetupEasternKingdoms()
    {
      Regions.QuelthalasAmbient.AddSound(LordaeronSummerDay);
      Regions.LordaeronAmbient1.AddSound(LordaeronSummerDay);
      Regions.LordaeronAmbient2.AddSound(LordaeronSummerDay);
      Regions.LordaeronAmbient3.AddSound(LordaeronSummerDay);
      Regions.LordaeronAmbient4.AddSound(LordaeronSummerDay);
      Regions.LordaeronAmbient5.AddSound(LordaeronSummerDay);
      Regions.ElwinForestAmbient.AddSound(LordaeronSummerDay);
      Regions.SunwellAmbient.AddSound(CityScapeDay);
      Regions.StratholmeAmbient.AddSound(CityScapeDay);
      Regions.Terenas.AddSound(CityScapeDay);
      Regions.TyrHandAmbient.AddSound(CityScapeDay);
      Regions.Dalaran.AddSound(CityScapeDay);
      Regions.Stromgarde.AddSound(CityScapeDay);
      Regions.StormwindAmbient2.AddSound(CityScapeDay);
      Regions.Stromwind_antiship.AddSound(CityScapeDay);
      Regions.ShipAmbient.AddSound(CityScapeDay);
      Regions.Gilneas.AddSound(DalaranRuinsDay);
      Regions.ShadowfangAmbient.AddSound(LordaeronSummerNight);
      Regions.LordamereLakeAmbient.AddSound(IceCrownDay);
      Regions.BridgeAmbient.AddSound(IceCrownDay);
      Regions.Aerie_Peak.AddSound(IceCrownDay);
      Regions.DunmoroghAmbient1.AddSound(IceCrownDay);
      Regions.DunmoroghAmbient2.AddSound(IceCrownDay);
      Regions.DunmoroghAmbient3.AddSound(IceCrownDay);
      Regions.IronforgeAmbient.AddSound(IceCrownNight);
      Regions.SouthshoreAmbient.AddSound(LordaeronFallDay);
      Regions.SouthshoreAmbient2.AddSound(LordaeronFallDay);
      Regions.SouthshoreAmbient3.AddSound(LordaeronFallDay);
      Regions.SouthshoreAmbient4.AddSound(LordaeronFallDay);
      Regions.SouthshoreAmbient5.AddSound(LordaeronFallDay);
      Regions.ScholomanceAmbient1.AddSound(LordaeronFallDay);
      Regions.ScholomanceAmbient2.AddSound(LordaeronFallDay);
      Regions.ScholomanceAmbient3.AddSound(LordaeronFallDay);
      Regions.Kultiras.AddSound(LordaeronFallDay);
      Regions.SentinelTowerAmbient.AddSound(LordaeronFallDay);
      Regions.GrimBatolAmbient1.AddSound(LordaeronFallDay);
      Regions.GrimBatolAmbient2.AddSound(LordaeronFallDay);
      Regions.ZulAman_trolls.AddSound(LordaeronFallNight);
      Regions.AlteracAmbient.AddSound(NorthrendDay);
      Regions.HinterlandAmbient1.AddSound(LordaeronWinterNight);
      Regions.HinterlandAmbient2.AddSound(LordaeronWinterNight);
      Regions.TolbaradAmbient.AddSound(DalaranRuinsDay);
      Regions.DarkshireAmbient1.AddSound(DalaranRuinsDay);
      Regions.DarkshireAmbient2.AddSound(DalaranRuinsDay);
      Regions.DarkshireAmbient3.AddSound(DalaranRuinsDay);
      Regions.BalorAmbient.AddSound(WetlandsNight);
      Regions.StranglethornAmbient1.AddSound(WetlandsNight);
      Regions.StranglethornAmbient2.AddSound(WetlandsNight);
      Regions.StranglethornAmbient3.AddSound(WetlandsNight);
      Regions.BlastedlandAmbient.AddSound(BlackCitadelOutlandNight);
      Regions.SwampofSorrowsAmbient.AddSound(AshenvaleNight);
      Regions.WetlandAmbient1.AddSound(AshenvaleNight);
      Regions.WetlandAmbient2.AddSound(AshenvaleNight);
      Regions.BurningSteppesAmbient.AddSound(BarrensDay);
      Regions.BurningSteppeAmbient2.AddSound(BarrensDay);
      Regions.LochModanAmbient.AddSound(LordaeronWinterDay);
    }

    private static void SetupKalimdor()
    {
      Regions.Northrend_Ambiance.AddSound(NorthrendNight);
      Regions.Coldarra.AddSound(NorthrendNight);
      Regions.InstanceOutland.AddSound(BlackCitadelOutlandDay);
      Regions.MonolithNoBuild.AddSound(BlackCitadelOutlandDay);
      Regions.TeldrassilAmbient.AddSound(AshenvaleNight);
      Regions.FeralasAmbient1.AddSound(AshenvaleNight);
      Regions.FeralasAmbient2.AddSound(AshenvaleNight);
      Regions.RanazjarAmbient.AddSound(AshenvaleNight);
      Regions.DusthallowAmbient.AddSound(AshenvaleNight);
      Regions.AzuremystAmbient.AddSound(LordaeronFallDay);
      Regions.AszharaAmbient1.AddSound(LordaeronFallDay);
      Regions.AzsharaAmbient2.AddSound(LordaeronFallDay);
      Regions.EchoIsleAmbient.AddSound(LordaeronFallDay);
      Regions.MaelstromAmbient.AddSound(LordaeronFallDay);
      Regions.AshenvaleAmbient.AddSound(AshenvaleDay);
      Regions.AshenvaleAmbient2.AddSound(AshenvaleDay);
      Regions.BarrenAmbient1.AddSound(BarrensDay);
      Regions.BarrenAmbient2.AddSound(BarrensDay);
      Regions.BarrenAmbient3.AddSound(BarrensDay);
      Regions.BarrenAmbient4.AddSound(BarrensDay);
      Regions.BarrenAmbient5.AddSound(BarrensDay);
      Regions.UldumAmbiance.AddSound(WetlandsNight);
      Regions.SilithusAmbient.AddSound(DalaranRuinsDay);
      Regions.UngoroAmbient.AddSound(WetlandsNight);
      Regions.ZulfarrakAmbient.AddSound(Wetlandsday);
      Regions.KezanAmbient.AddSound(Wetlandsday);
      Regions.BrokenIslesA.AddSound(Wetlandsday);
      Regions.BrokenIslesB.AddSound(Wetlandsday);
      Regions.WinterspringAmbient1.AddSound(IceCrownDay);
      Regions.WinterspringAmbient2.AddSound(IceCrownDay);
      Regions.Nyalotha.AddSound(BlackCitadelOutlandDay);
      Regions.ElementalRealm.AddSound(BlackCitadelOutlandNight);

    }
  }
}