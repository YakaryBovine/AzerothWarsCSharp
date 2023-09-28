using MacroTools.Extensions;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace MacroTools.SpellSystem
{
  /// <summary>
  /// Provides methods for casting abilities through a dummy unit.
  /// </summary>
  public static class DummyCast
  {
    /// <summary>
    /// A filter that can be applied to dummy casts so that the casts are only performed on particular targets.
    /// </summary>
    public delegate bool CastFilter(unit caster, unit target);

    /// <summary>
    /// Causes the specified spell to be channeled at a particular point.
    /// </summary>
    public static void ChannelOnPoint(unit caster, int abilityId, string orderId, int level, Point targetPoint, float duration)
    {
      CreateUnit(GetOwningPlayer(caster), DummyCaster.UnitTypeId, targetPoint.X, targetPoint.Y, 0)
        .AddAbility(abilityId)
        .SetAbilityLevel(abilityId, level)
        .IssueOrder(orderId)
        .SetTimedLife(duration);
    }

    /// <summary>
    /// Causes the specified spell to be channeled at the caster's point.
    /// </summary>
    public static void ChannelAtCaster(unit caster, int abilityId, string orderId, int level, float duration)
    {
      CreateUnit(GetOwningPlayer(caster), DummyCaster.UnitTypeId, caster.GetPosition().X, caster.GetPosition().Y, 0)
        .AddAbility(abilityId)
        .SetAbilityLevel(abilityId, level)
        .IssueOrder(orderId)
        .SetTimedLife(duration);
    }

    /// <summary>
    /// Causes the specified ability to be cast from the specified object at the specified target.
    /// </summary>
    public static void DummyCastUnit(unit caster, int abilId, string orderId, int level, unit target, DummyCastOriginType originType)
    {
      var originPoint = originType == DummyCastOriginType.Caster ? caster.GetPosition() : target.GetPosition();
      DummyCaster.DummyUnit
        .SetOwner(caster.OwningPlayer())
        .SetPosition(originPoint)
        .AddAbility(abilId)
        .SetAbilityLevel(abilId, level);

      if (originType == DummyCastOriginType.Caster)
        DummyCaster.DummyUnit.FacePosition(target.GetPosition());

      DummyCaster.DummyUnit
        .IssueOrder(orderId, target)
        .RemoveAbility(abilId);
    }

    public static void DummyCastNoTarget(unit caster, int abilId, int orderId, int level)
    {
      DummyCaster.DummyUnit
        .SetOwner(caster.OwningPlayer())
        .SetPosition(caster.GetPosition())
        .AddAbility(abilId)
        .SetAbilityLevel(abilId, level);

      DummyCaster.DummyUnit
        .IssueOrder(orderId)
        .RemoveAbility(abilId);
    }

    /// <summary>
    /// Causes the specified spell to be cast on a particular point.
    /// </summary>
    public static void DummyCastNoTargetOnUnit(unit caster, int abilId, string orderId, int level, unit target)
    {

      DummyCaster.DummyUnit
        .SetOwner(caster.OwningPlayer())
        .SetPosition(target.GetPosition())
        .AddAbility(abilId)
        .SetAbilityLevel(abilId, level);

      DummyCaster.DummyUnit
        .IssueOrder(orderId)
        .RemoveAbility(abilId);

    }

    /// <summary>
    /// Causes the specified spell to be cast at a particular point.
    /// </summary>
    public static void DummyCastPoint(player whichPlayer, int abilId, string orderId, int level, Point target)
    {
      DummyCaster.DummyUnit
        .SetOwner(whichPlayer)
        .SetPosition(target)
        .AddAbility(abilId)
        .SetAbilityLevel(abilId, level)
        .IssueOrder(orderId, target)
        .RemoveAbility(abilId);
    }

    /// <summary>
    /// Causes the specified spell to be cast on all units in a circle.
    /// </summary>
    public static void DummyCastOnUnitsInCircle(unit caster, int abilId, string orderId, int level, Point center,
      float radius, CastFilter castFilter, DummyCastOriginType originType)
    {
      foreach (var target in CreateGroup()
                 .EnumUnitsInRange(center, radius).EmptyToList()
                 .FindAll(unit => castFilter(caster, unit)))
      {
        DummyCastUnit(caster, abilId, orderId, level, target, originType);
      }
    }
  }
}