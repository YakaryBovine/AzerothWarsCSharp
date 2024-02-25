using MacroTools;
using WCSharp.Shared.Data;

namespace WarcraftLegacies.Source.FactionMechanics.Scourge.Blight
{
  public static class BlightSetup
  {
    public static void Setup(PreplacedUnitSystem preplacedUnitSystem)
    {
      SetupA(preplacedUnitSystem);
      SetupB(preplacedUnitSystem);
      SetupC(preplacedUnitSystem);
    }

    private static void SetupA(PreplacedUnitSystem preplacedUnitSystem)
    {
      BlightSystem.Register(
        preplacedUnitSystem.GetUnit(Constants.UNIT_HTOW_TOWN_HALL_LORDAERON_T1, Regions.Andorhal.Center),
        new BlightParameters
        {
          PrimaryBlightRadius = 512,
          RandomBlightCount = 6,
          RandomBlightRadius = 200,
          RandomBlightRectangle = Regions.Andorhal
        });

      BlightSystem.Register(preplacedUnitSystem.GetUnit(Constants.UNIT_H030_TYR_S_HAND_CITADEL_LORDAERON_OTHER),
        new BlightParameters
        {
          PrimaryBlightRadius = 512,
          RandomBlightCount = 6,
          RandomBlightRadius = 200,
          RandomBlightRectangle = Regions.Havenshire
        });

      BlightSystem.Register(
        preplacedUnitSystem.GetUnit(Constants.UNIT_H01T_INN_LORDAERON_OTHER, Regions.Darrowshire.Center),
        new BlightParameters
        {
          PrimaryBlightRadius = 512,
          RandomBlightCount = 2,
          RandomBlightRadius = 200,
          RandomBlightRectangle = Regions.Darrowshire
        });

      BlightSystem.Register(
        preplacedUnitSystem.GetUnit(Constants.UNIT_HHOU_FARM_LORDAERON_FARM, Regions.Terrordale.Center),
        new BlightParameters
        {
          PrimaryBlightRadius = 512,
          RandomBlightCount = 3,
          RandomBlightRadius = 200,
          RandomBlightRectangle = Regions.Terrordale
        });

      BlightSystem.Register(
        preplacedUnitSystem.GetUnit(Constants.UNIT_H01V_BARN_LORDAERON_OTHER, Regions.Corins_Crossing.Center),
        new BlightParameters
        {
          PrimaryBlightRadius = 512,
          RandomBlightCount = 6,
          RandomBlightRadius = 200,
          RandomBlightRectangle = Regions.Corins_Crossing
        });

      BlightSystem.Register(preplacedUnitSystem.GetUnit(Constants.UNIT_H01T_INN_LORDAERON_OTHER, Regions.Brill.Center),
        new BlightParameters
        {
          PrimaryBlightRadius = 512,
          RandomBlightCount = 4,
          RandomBlightRadius = 200,
          RandomBlightRectangle = Regions.Brill
        });
    }

    private static void SetupB(PreplacedUnitSystem preplacedUnitSystem)
    {
      BlightSystem.Register(
        preplacedUnitSystem.GetUnit(Constants.UNIT_H00I_WINDMILL_LORDAERON_OTHER, Regions.Vandermar_Village.Center),
        new BlightParameters
        {
          PrimaryBlightRadius = 512,
          RandomBlightCount = 4,
          RandomBlightRadius = 200,
          RandomBlightRectangle = Regions.Vandermar_Village
        });

      BlightSystem.Register(
        preplacedUnitSystem.GetUnit(Constants.UNIT_H01T_INN_LORDAERON_OTHER, Regions.Solliden_Farmstead.Center),
        new BlightParameters
        {
          PrimaryBlightRadius = 512,
          RandomBlightCount = 4,
          RandomBlightRadius = 200,
          RandomBlightRectangle = Regions.Solliden_Farmstead
        });

      BlightSystem.Register(
        preplacedUnitSystem.GetUnit(Constants.UNIT_HHOU_FARM_LORDAERON_FARM, new Point(11323, 9032)),
        new BlightParameters
        {
          PrimaryBlightRadius = 250
        });

      BlightSystem.Register(
        preplacedUnitSystem.GetUnit(Constants.UNIT_HHOU_FARM_LORDAERON_FARM, new Point(11323, 9032)),
        new BlightParameters
        {
          PrimaryBlightRadius = 250
        });

      BlightSystem.Register(
        preplacedUnitSystem.GetUnit(Constants.UNIT_HHOU_FARM_LORDAERON_FARM, new Point(11911, 9620)),
        new BlightParameters
        {
          PrimaryBlightRadius = 250
        });

      BlightSystem.Register(
        preplacedUnitSystem.GetUnit(Constants.UNIT_H00I_WINDMILL_LORDAERON_OTHER, new Point(7536, 11626)),
        new BlightParameters
        {
          PrimaryBlightRadius = 300
        });

      BlightSystem.Register(
        preplacedUnitSystem.GetUnit(Constants.UNIT_HHOU_FARM_LORDAERON_FARM, new Point(10417, 10194)),
        new BlightParameters
        {
          PrimaryBlightRadius = 250
        });

      BlightSystem.Register(preplacedUnitSystem.GetUnit(Constants.UNIT_H000_CAPITAL_PALACE_LORDAERON),
        new BlightParameters
        {
          PrimaryBlightRadius = 400
        });
    }

    private static void SetupC(PreplacedUnitSystem preplacedUnitSystem)
    {
      BlightSystem.Register(
        preplacedUnitSystem.GetUnit(Constants.UNIT_HHOU_FARM_LORDAERON_FARM, new Point(11307, 9735)),
        new BlightParameters
        {
          PrimaryBlightRadius = 250
        });

      BlightSystem.Register(
        preplacedUnitSystem.GetUnit(Constants.UNIT_H016_GRANARY_LORDAERON_OTHER, new Point(13385, 9152)),
        new BlightParameters
        {
          PrimaryBlightRadius = 300
        });

      BlightSystem.Register(
        preplacedUnitSystem.GetUnit(Constants.UNIT_HHOU_FARM_LORDAERON_FARM, new Point(15485, 9804)),
        new BlightParameters
        {
          PrimaryBlightRadius = 250
        });

      BlightSystem.Register(
        preplacedUnitSystem.GetUnit(Constants.UNIT_H016_GRANARY_LORDAERON_OTHER, new Point(17474, 8825)),
        new BlightParameters
        {
          PrimaryBlightRadius = 300
        });

      BlightSystem.Register(
        preplacedUnitSystem.GetUnit(Constants.UNIT_HHOU_FARM_LORDAERON_FARM, new Point(17188, 10583)),
        new BlightParameters
        {
          PrimaryBlightRadius = 250
        });

      BlightSystem.Register(preplacedUnitSystem.GetUnit(Constants.UNIT_NEGT_SUN_TOWER, Regions.Dreadscar_1.Center),
        new BlightParameters
        {
          PrimaryBlightRadius = 200,
          RandomBlightCount = 8,
          RandomBlightRadius = 200,
          RandomBlightRectangle = Regions.Dreadscar_1
        });

      BlightSystem.Register(
        preplacedUnitSystem.GetUnit(Constants.UNIT_N003_IMPROVED_SUN_TOWER_QUEL_THALAS_TOWER, new Point(20679, 17143)),
        new BlightParameters
        {
          PrimaryBlightRadius = 200,
          RandomBlightCount = 9,
          RandomBlightRadius = 200,
          RandomBlightRectangle = Regions.Dreadscar_2
        });

      BlightSystem.Register(
        preplacedUnitSystem.GetUnit(Constants.UNIT_H074_ARCANE_TOWER_QUEL_THALAS_TOWER, Regions.Dreadscar_3.Center),
        new BlightParameters
        {
          PrimaryBlightRadius = 200,
          RandomBlightCount = 13,
          RandomBlightRadius = 200,
          RandomBlightRectangle = Regions.Dreadscar_3
        });
    }
  }
}