using System;
using MacroTools;
using MacroTools.Extensions;
using MacroTools.Libraries;
using MacroTools.SpellSystem;
using WarcraftLegacies.Shared;
using WarcraftLegacies.Shared.Models.Abilities;
using WCSharp.Missiles;
using WCSharp.Shared.Data;

namespace WarcraftLegacies.Source.Spells
{
  /// <summary>
  /// Creates a line of projectiles behind the caster and sends them forward. The projectiles deal instant damage and
  /// apply a dummy spell.
  /// </summary>
  public sealed class Apocalypse : Spell
  {
    /// <summary>How far the spell travels.</summary>
    public float Range { get; init; }
    
    /// <summary>All projectiles are spaced out in a line this wide.</summary>
    public float Width { get; init; }
    
    /// <summary>When upgraded, all projectiles are spaced out in a line this wide.</summary>
    public float WidthUpgraded { get; init; }
    
    /// <summary>How far the projectiles travel.</summary>
    public float ProjectileVelocity { get; init; }
    
    /// <summary>The radius in which the projectiles deal damage.</summary>
    public float ProjectileRadius { get; init; }
    
    /// <summary>How many projectiles to spawn.</summary>
    public int ProjectileCount { get; init; }
    
    /// <summary>When upgraded, how many projectiles to spawn.</summary>
    public int ProjectileCountUpgraded { get; init; }

    /// <summary>How much instant damage the projectiles deal.</summary>
    public LeveledAbilityField<int> Damage { get; init; } = new();

    /// <summary>What the projectile looks like.</summary>
    public string ProjectileModel { get; init; } = "";
    
    /// <summary>The size of the projectile.</summary>
    public float ProjectileScale { get; init; }

    /// <summary>The visual effect that appears when a unit is struck.</summary>
    public string EffectOnHitModel { get; init; } = "";

    /// <summary>The size of <see cref="EffectOnHitModel"/>.</summary>
    public float EffectOnHitScale { get; init; }

    /// <summary>The effect that appears where each projectile is spawned.</summary>
    public string EffectOnProjectileSpawn { get; init; } = "";

    /// <summary>The size of <see cref="EffectOnProjectileSpawn"/>.</summary>
    public float EffectOnProjectileSpawnScale { get; init; }
    
    /// <summary>This ability is cast on all units struck.</summary>
    public int DummyAbilityId { get; init; }

    /// <summary>The order ID for <see cref="DummyAbilityId"/>.</summary>
    public int DummyAbilityOrderId { get; init; }

    /// <summary>A caster matching this condition is considered to have the upgraded version of the spell.</summary>
    public Func<unit, bool> UpgradeCondition { get; init; } = _ => false;
    
    /// <inheritdoc />
    public Apocalypse(int id) : base(id)
    {
      
    }

    /// <inheritdoc />
    public override void OnCast(unit caster, unit target, Point targetPoint)
    {
      var casterFacing = MathEx.GetAngleBetweenPoints(GetUnitX(caster), GetUnitY(caster), targetPoint.X, targetPoint.Y);
      var casterX = GetUnitX(caster);
      var casterY = GetUnitY(caster);
      var level = GetAbilityLevel(caster);
      var isUpgraded = UpgradeCondition(caster);
      var width = isUpgraded ? WidthUpgraded : Width;
      var projectileCount = isUpgraded ? ProjectileCountUpgraded : ProjectileCount;
      var middle = (projectileCount - 1) / 2;
      for (var i = 0; i < projectileCount; i++)
      {
        var projectileOrigin = GetProjectileOrigin(i, middle, casterFacing, casterX, casterY, width, projectileCount);
        AddSpecialEffect(EffectOnProjectileSpawn, projectileOrigin.X, projectileOrigin.Y)
          .SetScale(EffectOnProjectileSpawnScale)
          .SetLifespan();

        var projectileDestination = GetProjectileDestination(projectileOrigin, casterFacing);
        
        var missile = new ApocalypseProjectile(caster.OwningPlayer(), projectileOrigin.X, projectileOrigin.Y,
          projectileDestination.X, projectileDestination.Y)
        {
          CollisionRadius = ProjectileRadius,
          EffectString = ProjectileModel,
          EffectScale = ProjectileScale,
          Active = true,
          Caster = caster,
          CasterLaunchZ = 50f,
          TargetImpactZ = 50f,
          Speed = ProjectileVelocity,
          Damage = Damage.Base + Damage.PerLevel * level,
          EffectOnHitModel = EffectOnHitModel,
          EffectOnHitScale = EffectOnHitScale,
          EffectOnProjectileDespawnModel = EffectOnProjectileSpawn,
          EffectOnProjectileDespawnScale = EffectOnProjectileSpawnScale,
          DummyAbilityId = DummyAbilityId,
          DummyAbilityOrderId = DummyAbilityOrderId,
          DummyAbilityLevel = 1
        };
        MissileSystem.Add(missile);
      }
    }

    private static Point GetProjectileOrigin(int projectileIndex, int middleProjectileIndex, float casterFacing,
      float casterX, float casterY, float width, int projectileCount)
    {
      float offsetAngle = 0;
      float offsetDistance = 0;
      if (projectileIndex < middleProjectileIndex)
      {
        offsetAngle = casterFacing - 90 - 15 * (middleProjectileIndex - projectileIndex);
        offsetDistance = (middleProjectileIndex - projectileIndex) * (width / projectileCount);
      }
      else if (projectileIndex > middleProjectileIndex)
      {
        offsetAngle = casterFacing + 90 + 15 * (projectileIndex - middleProjectileIndex);
        offsetDistance = (projectileIndex - middleProjectileIndex) * (width / projectileCount);
      }

      var projectileOrigin = new Point(MathEx.GetPolarOffsetX(casterX, offsetDistance, offsetAngle),
        MathEx.GetPolarOffsetY(casterY, offsetDistance, offsetAngle));
      return projectileOrigin;
    }
    
    private Point GetProjectileDestination(Point projectileOrigin, float casterFacing)
    {
      var x = MathEx.GetPolarOffsetX(projectileOrigin.X, Range, casterFacing);
      var y = MathEx.GetPolarOffsetY(projectileOrigin.Y, Range, casterFacing);

      var projectileDestination = new Point(x, y);
      return projectileDestination;
    }
  }
}