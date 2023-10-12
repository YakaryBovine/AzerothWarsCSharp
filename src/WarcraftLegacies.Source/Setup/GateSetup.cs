using MacroTools.PassiveAbilities;
using MacroTools.PassiveAbilitySystem;

namespace WarcraftLegacies.Source.Setup
{
  public static class GateSetup
  {
    public static void Setup()
    {
      PassiveAbilityManager.Register(new Gate(Constants.UNIT_H00L_HORIZONTAL_WOODEN_GATE_OPEN,
        Constants.UNIT_H00K_HORIZONTAL_WOODEN_GATE_CLOSED, Constants.UNIT_H00M_HORIZONTAL_WOODEN_GATE_DEAD));
      PassiveAbilityManager.Register(new Gate(Constants.UNIT_H01X_ELVEN_GATE_OPEN,
        Constants.UNIT_H01W_ELVEN_GATE_CLOSED, Constants.UNIT_H01Y_ELVEN_GATE_DEAD));
      PassiveAbilityManager.Register(new Gate(Constants.UNIT_H02J_STORMWIND_HARBOUR_GATE_OPEN,
        Constants.UNIT_H02H_STORMWIND_HARBOUR_GATE_CLOSED, Constants.UNIT_H02I_STORMWIND_HARBOUR_GATE_DEAD));
      PassiveAbilityManager.Register(new Gate(Constants.UNIT_H02S_GATES_OF_AHN_QIRAJ_OPEN,
        Constants.UNIT_H02U_GATES_OF_AHN_QIRAJ_CLOSED, Constants.UNIT_H02T_GATES_OF_AHN_QIRAJ_DEAD));
      PassiveAbilityManager.Register(new Gate(Constants.UNIT_H02M_GREYMANE_S_GATE_OPEN,
        Constants.UNIT_H02K_GREYMANE_S_GATE_CLOSED, Constants.UNIT_H02L_GREYMANE_S_GATE_DEAD));
    }
  }
}