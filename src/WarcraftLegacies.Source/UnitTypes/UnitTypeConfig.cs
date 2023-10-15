using MacroTools;

namespace WarcraftLegacies.Source.UnitTypes
{
  public static class UnitTypeConfig
  {
    public static void Setup()
    {
      SubSetupA();
      SubSetupB();
      SubSetupPortals();
      SubSetupTrader();
      SubSetupGatesA();
      SubSetupGatesB();
    }

    private static void SubSetupA()
    {
      UnitType.Register(new UnitType(Constants.UNIT_HSHY_SHIPYARD_LORDAERON_SHIPYARD)
      {
        Category = UnitCategory.Shipyard
      });

      UnitType.Register(new UnitType(Constants.UNIT_H076_SHIPYARD_DALARAN_SHIPYARD)
      {
        Category = UnitCategory.Shipyard
      });

      UnitType.Register(new UnitType(Constants.UNIT_H07W_SHIPYARD_KUL_TIRAS_SHIPYARD)
      {
        Category = UnitCategory.Shipyard
      });

      UnitType.Register(new UnitType(Constants.UNIT_H077_SHIPYARD_QUEL_THALAS_SHIPYARD)
      {
        Category = UnitCategory.Shipyard
      });

      UnitType.Register(new UnitType(Constants.UNIT_H03H_SHIPYARD_GILNEAS_SHIPYARD)
      {
        Category = UnitCategory.Shipyard
      });

      UnitType.Register(new UnitType(Constants.UNIT_H06D_ROYAL_HARBOUR_STORMWIND_SHIPYARD)
      {
        Category = UnitCategory.Shipyard
      });

      UnitType.Register(new UnitType(Constants.UNIT_H07D_SHIPYARD_IRONFORGE_SHIPYARD)
      {
        Category = UnitCategory.Shipyard
      });

      UnitType.Register(new UnitType(Constants.UNIT_OSHY_HORDE_PIER_FROSTWOLF_SHIPYARD)
      {
        Category = UnitCategory.Shipyard
      });

      UnitType.Register(new UnitType(Constants.UNIT_O02T_SHIPYARD_WARSONG_SHIPYARD)
      {
        Category = UnitCategory.Shipyard
      });

      UnitType.Register(new UnitType(Constants.UNIT_O032_SHIPYARD_FEL_HORDE_SHIPYARD)
      {
        Category = UnitCategory.Shipyard
      });
    }

    private static void SubSetupB()
    {
      UnitType.Register(new UnitType(Constants.UNIT_O03I_SHIPYARD_TWILIGHT_DOCK)
      {
        Category = UnitCategory.Shipyard
      });

      UnitType.Register(new UnitType(Constants.UNIT_O049_GOLDEN_DOCK_ZANDALARI_SHIPYARD)
      {
        Category = UnitCategory.Shipyard
      });

      UnitType.Register(new UnitType(Constants.UNIT_O03V_BILGEWATER_HARBOR_GOBLIN_SHIPYARD)
      {
        Category = UnitCategory.Shipyard
      });

      UnitType.Register(new UnitType(Constants.UNIT_USHP_HAUNTED_HARBOR_SCOURGE)
      {
        Category = UnitCategory.Shipyard
      });

      UnitType.Register(new UnitType(Constants.UNIT_U009_SHIPYARD_LEGION_SHIPYARD)
      {
        Category = UnitCategory.Shipyard
      });

      UnitType.Register(new UnitType(Constants.UNIT_ESHY_KALDOREI_DOCKS)
      {
        Category = UnitCategory.Shipyard
      });

      UnitType.Register(new UnitType(Constants.UNIT_E011_KALDOREI_DOCKS_SENTINEL_SHIPYARD)
      {
        Category = UnitCategory.Shipyard
      });
    }

    private static void SubSetupPortals()
    {
      UnitType.Register(new UnitType(Constants.UNIT_N03H_DEATH_GATE_WAYGATE)
      {
        NeverDelete = true
      });
      
      UnitType.Register(new UnitType(Constants.UNIT_N036_DARK_PORTAL_WAYGATE)
      {
        NeverDelete = true
      });
      
      UnitType.Register(new UnitType(Constants.UNIT_N05J_DARK_PORTAL_AURA_CONTROL_NEXUS)
      {
        NeverDelete = true
      });
      
      UnitType.Register(new UnitType(Constants.UNIT_N03J_BLACK_PORTAL_AURA_CONTROL_NEXUS)
      {
        NeverDelete = true
      });
    }
    
    private static void SubSetupTrader()
    {
      UnitType.Register(new UnitType(Constants.UNIT_H014_TRADING_POST_SEA)
      {
        NeverDelete = true
      });
      
      UnitType.Register(new UnitType(Constants.UNIT_H05W_TRADE_LUMBER_FOR_GOLD)
      {
        NeverDelete = true
      });
    }

    private static void SubSetupGatesA()
    {
      UnitType.Register(new UnitType(Constants.UNIT_H00L_HORIZONTAL_WOODEN_GATE_OPEN)
      {
        NeverDelete = true
      });
      
      UnitType.Register(new UnitType(Constants.UNIT_H00K_HORIZONTAL_WOODEN_GATE_CLOSED)
      {
        NeverDelete = true
      });
      
      UnitType.Register(new UnitType(Constants.UNIT_H00M_HORIZONTAL_WOODEN_GATE_DEAD)
      {
        NeverDelete = true
      });
      
      UnitType.Register(new UnitType(Constants.UNIT_H01X_ELVEN_GATE_OPEN)
      {
        NeverDelete = true
      });
      
      UnitType.Register(new UnitType(Constants.UNIT_H01W_ELVEN_GATE_CLOSED)
      {
        NeverDelete = true
      });
      
      UnitType.Register(new UnitType(Constants.UNIT_H01Y_ELVEN_GATE_DEAD)
      {
        NeverDelete = true
      });
      
      UnitType.Register(new UnitType(Constants.UNIT_H02J_STORMWIND_HARBOUR_GATE_OPEN)
      {
        NeverDelete = true
      });
      
      UnitType.Register(new UnitType(Constants.UNIT_H02H_STORMWIND_HARBOUR_GATE_CLOSED)
      {
        NeverDelete = true
      });
    }

    private static void SubSetupGatesB()
    {
      UnitType.Register(new UnitType(Constants.UNIT_H02I_STORMWIND_HARBOUR_GATE_DEAD)
      {
        NeverDelete = true
      });
      
      UnitType.Register(new UnitType(Constants.UNIT_H02S_GATES_OF_AHN_QIRAJ_OPEN)
      {
        NeverDelete = true
      });
      
      UnitType.Register(new UnitType(Constants.UNIT_H02U_GATES_OF_AHN_QIRAJ_CLOSED)
      {
        NeverDelete = true
      });
      
      UnitType.Register(new UnitType(Constants.UNIT_H02T_GATES_OF_AHN_QIRAJ_DEAD)
      {
        NeverDelete = true
      });
      
      UnitType.Register(new UnitType(Constants.UNIT_H02M_GREYMANE_S_GATE_OPEN)
      {
        NeverDelete = true
      });
      
      UnitType.Register(new UnitType(Constants.UNIT_H02K_GREYMANE_S_GATE_CLOSED)
      {
        NeverDelete = true
      });
      
      UnitType.Register(new UnitType(Constants.UNIT_H02L_GREYMANE_S_GATE_DEAD)
      {
        NeverDelete = true
      });
    }
  }
}