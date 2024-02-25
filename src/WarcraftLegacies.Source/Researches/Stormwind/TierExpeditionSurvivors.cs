using MacroTools.Extensions;
using MacroTools.FactionSystem;
using WCSharp.Events;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Researches.Stormwind
{
  public static class TierExpeditionSurvivors
  {
    private static void Research()
    {
      GetTriggerPlayer().GetFaction()?.ModObjectLimit(Constants.UNIT_H00A_SPEARMAN_STORMWIND, -Faction.UNLIMITED);
      GetTriggerPlayer().GetFaction()?.ModObjectLimit(Constants.UNIT_H05N_MARKSMAN_STORMWIND, Faction.UNLIMITED);
    }

    public static void Setup()
    {
      PlayerUnitEvents.Register(ResearchEvent.IsFinished, Research, Constants.UPGRADE_R031_EXPEDITION_SURVIVORS_ARATHOR_T3);
    }
  }
}