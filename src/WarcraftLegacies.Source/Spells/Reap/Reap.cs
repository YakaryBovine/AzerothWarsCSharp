using System;
using System.Linq;
using MacroTools;
using MacroTools.Extensions;
using MacroTools.Libraries;
using MacroTools.SpellSystem;
using WarcraftLegacies.Shared;
using WarcraftLegacies.Shared.Models.Abilities;
using WCSharp.Buffs;
using WCSharp.Shared.Data;

namespace WarcraftLegacies.Source.Spells.Reap
{
  /// <summary>Kills nearby enemy units to grant the caster Strength for a limited time.</summary>
  public sealed class Reap : Spell
  {
    /// <summary>Number of units slain by the ability.</summary>
    public LeveledAbilityField<int> UnitsSlain { get; init; } = new();

    /// <summary>Strength gained per unit slain.</summary>
    public LeveledAbilityField<int> StrengthPerUnit { get; init; } = new();
    
    /// <summary>Strength gained per unit slain.</summary>
    public LeveledAbilityField<int> StrengthPerUnitUpgraded { get; init; } = new();

    /// <summary>How far away units can be slain.</summary>
    public LeveledAbilityField<float> Radius { get; init; } = new();

    /// <summary>An effect that briefly appears at each enemy slain.</summary>
    public string KillEffect { get; init; } = string.Empty;

    /// <summary>An effect that gets applied to the caster while they are buffed.</summary>
    public string BuffEffect { get; init; } = string.Empty;
    
    /// <summary>How long the Strength buff lasts.</summary>
    public float Duration { get; init; }

    /// <summary>A caster matching this condition is considered to have the upgraded version of the spell.</summary>
    public Func<unit, bool> UpgradeCondition { get; init; } = _ => false;
    
    /// <inheritdoc />
    public Reap(int id) : base(id)
    {
    }

    /// <inheritdoc />
    public override void OnCast(unit caster, unit target, Point targetPoint)
    {
      try
      {
        var casterPosition = caster.GetPosition();
        var abilityLevel = GetAbilityLevel(caster);
        var radius = Radius.Base + Radius.PerLevel * abilityLevel;
        var unitsSlain = UnitsSlain.Base + UnitsSlain.PerLevel * abilityLevel;
        var killTargets = CreateGroup()
          .EnumUnitsInRange(casterPosition, radius)
          .EmptyToList()
          .Where(x => IsValidTarget(x, caster))
          .OrderBy(x => GetUnitLevel(x))
          .ThenBy(x => MathEx.GetDistanceBetweenPoints(caster.GetPosition(), x.GetPosition()))
          .Take(unitsSlain)
          .ToList();

        if (!killTargets.Any())
          return;

        foreach (var killTarget in killTargets)
        {
          killTarget.TakeDamage(caster, killTarget.GetHitPoints(), damageType: DAMAGE_TYPE_UNIVERSAL,
            attackType: ATTACK_TYPE_CHAOS);
          AddSpecialEffect(KillEffect, GetUnitX(killTarget), GetUnitY(killTarget))
            .SetLifespan();
        }

        var strengthGainPerTarget = UpgradeCondition(caster)
          ? StrengthPerUnitUpgraded.Base + StrengthPerUnitUpgraded.PerLevel * abilityLevel
          : StrengthPerUnit.Base + StrengthPerUnit.PerLevel * abilityLevel;

        BuffSystem.Add(new ReapBuff(caster, BuffEffect)
        {
          Active = true,
          Duration = Duration,
          IsBeneficial = true,
          StrengthGain = strengthGainPerTarget * killTargets.Count
        });
      }
      catch (Exception ex)
      {
        Logger.LogError($"Failed to cast {nameof(Reap)}: {ex}");
      }
    }

    private static bool IsValidTarget(unit target, unit caster) =>
      UnitAlive(target) && 
      !target.IsResistant() && 
      !IsUnitType(target, UNIT_TYPE_STRUCTURE) &&
      !IsUnitType(target, UNIT_TYPE_ANCIENT) && 
      !IsUnitType(target, UNIT_TYPE_MECHANICAL) &&
      !IsUnitType(target, UNIT_TYPE_MAGIC_IMMUNE) && 
      !IsPlayerAlly(caster.OwningPlayer(), target.OwningPlayer());
  }
}