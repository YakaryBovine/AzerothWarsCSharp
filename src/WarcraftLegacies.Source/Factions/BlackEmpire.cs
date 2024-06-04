using MacroTools.FactionSystem;
using WarcraftLegacies.Shared.Config.Factions;
using WarcraftLegacies.Source.Setup;

namespace WarcraftLegacies.Source.Factions
{
  public class BlackEmpire : Faction
  {
    /// <inheritdoc />
    public BlackEmpire() : base("BlackEmpire", PLAYER_COLOR_MAROON, "|cff800000",
      @"ReplaceableTextures\CommandButtons\BTNNzothIcon.blp")
    {
      ControlPointDefenderUnitTypeId = UNIT_N0DV_CONTROL_POINT_DEFENDER_BLACK_EMPIRE_TOWER;
      TraditionalTeam = TeamSetup.OldGods;
    }
    
    /// <inheritdoc />
    public override void OnRegistered()
    {
      RegisterObjectLimits();
      SharedFactionConfigSetup.AddSharedFactionConfig(this);
    }

    private void RegisterObjectLimits()
    {
      foreach (var (objectTypeId, objectLimit) in BlackEmpireObjectLimitData.GetAllObjectLimits())
        ModObjectLimit(FourCC(objectTypeId), objectLimit);
    }
  }
}