using WarcraftLegacies.Shared.Extensions;
using WarcraftLegacies.Shared.Models.Factions;

namespace WarcraftLegacies.Shared.Config.Factions
{
  public sealed class ObjectLimitRepository
  {
    private readonly Dictionary<string, int> _objectLimits = new();

    public ObjectLimitRepository()
    {
      AddFactionObjectLimits(AhnqirajObjectLimitData.GetAllObjectLimits());
      AddFactionObjectLimits(BilgewaterObjectLimitData.GetAllObjectLimits());
      AddFactionObjectLimits(BlackEmpireObjectLimitData.GetAllObjectLimits());
      AddFactionObjectLimits(DalaranObjectLimitData.GetAllObjectLimits());
      AddFactionObjectLimits(DraeneiObjectLimitData.GetAllObjectLimits());
      AddFactionObjectLimits(DruidsObjectLimitData.GetAllObjectLimits());
      AddFactionObjectLimits(FelHordeObjectLimitData.GetAllObjectLimits());
      AddFactionObjectLimits(FrostwolfObjectLimitData.GetAllObjectLimits());
      AddFactionObjectLimits(GilneasObjectLimitData.GetAllObjectLimits());
      AddFactionObjectLimits(IllidariObjectLimitData.GetAllObjectLimits());
      AddFactionObjectLimits(IronforgeObjectLimitData.GetAllObjectLimits());
      AddFactionObjectLimits(KultirasObjectLimitData.GetAllObjectLimits());
      AddFactionObjectLimits(LegionObjectLimitData.GetAllObjectLimits());
      AddFactionObjectLimits(LordaeronObjectLimitData.GetAllObjectLimits());
      AddFactionObjectLimits(NazjatarObjectLimitData.GetAllObjectLimits());
      AddFactionObjectLimits(QuelthalasObjectLimitData.GetAllObjectLimits());
      AddFactionObjectLimits(ScarletCrusadeObjectLimitData.GetAllObjectLimits());
      AddFactionObjectLimits(ScourgeObjectLimitData.GetAllObjectLimits());
      AddFactionObjectLimits(SentinelsObjectLimitData.GetAllObjectLimits());
      AddFactionObjectLimits(SkywallObjectLimitData.GetAllObjectLimits());
      AddFactionObjectLimits(StormwindObjectLimitData.GetAllObjectLimits());
      AddFactionObjectLimits(SunfuryObjectLimitData.GetAllObjectLimits());
      AddFactionObjectLimits(WarsongObjectLimitData.GetAllObjectLimits());
      AddFactionObjectLimits(ZandalarObjectLimitData.GetAllObjectLimits());
    }

    public bool TryGetObjectLimit(int objectTypeId, out int limit)
    {
      return _objectLimits.TryGetValue(objectTypeId.IdToFourCc(), out limit);
    }

    private void AddFactionObjectLimits(IEnumerable<ObjectLimit> factionObjectLimits)
    {
      foreach (var (objectTypeId, limit) in factionObjectLimits)
        _objectLimits.TryAdd(objectTypeId, limit);
    }
  }
}