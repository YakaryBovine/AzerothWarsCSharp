using System;
using System.Collections.Generic;
using MacroTools;
using MacroTools.Extensions;
using MacroTools.SpellSystem;
using WCSharp.Events;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Spells
{
  /// <summary>
  /// When a of a given type unit dies, the caster remembers it, and can later summon copies of all of the units that died.
  /// </summary>
  public sealed class AncestralLegion : Spell
  {
    private static readonly Dictionary<unit, AncestralLegionData> AncestralLegionDataByUnit = new();
    
    /// <summary>How long summoned units survive.</summary>
    public float Duration { get; init; }

    /// <summary>Summoned units have their hit points increased by this percentage.</summary>
    public LeveledAbilityField<float> HealthBonus { get; init; } = new();
    
    /// <summary>Summoned units have their damage increased by this percentage.</summary>
    public LeveledAbilityField<float> DamageBonus { get; init; } = new();
    
    /// <summary>The maximum number of Tauren that can be summoned.</summary>
    public LeveledAbilityField<int> SummonCap { get; init; } = new();

    /// <summary>The chance for a unit to be remembered on death.</summary>
    public float RememberChance { get; init; }

    /// <summary>Only units of this unit type ID will be remembered.</summary>
    public int RememberableUnitTypeId { get; init; }

    /// <summary>The effect to show when the spell is cast.</summary>
    public string SummonEffect { get; init; } = "";

    /// <summary>Summoned units will create this effect when they die.</summary>
    public string DeathEffect { get; init; } = "";

    /// <inheritdoc />
    public AncestralLegion(int id) : base(id)
    {
    }
    
    /// <inheritdoc />
    public override void OnCast(unit caster, unit target, Point targetPoint)
    {
      var level = GetAbilityLevel(caster);
      var summonCap = SummonCap.Base + SummonCap.PerLevel * level;
      var ancestralLegionData = AncestralLegionDataByUnit[caster];
      var availableTauren = ancestralLegionData.RememberedUnits;
      var taurenToSummon = Math.Min(summonCap, availableTauren);
      for (var i = 0; i < taurenToSummon; i++)
      {
        var summonedTauren = CreateUnit(caster.OwningPlayer(), RememberableUnitTypeId, targetPoint.X, targetPoint.Y, caster.GetFacing())
          .SetColor(200, 165, 50, 150)
          .MultiplyBaseDamage(1 + DamageBonus.Base + DamageBonus.PerLevel * level, 0)
          .MultiplyMaxHitpoints(1 + HealthBonus.Base + HealthBonus.PerLevel * level)
          .SetTimedLife(Duration)
          .SetName("Ancestor")
          .AddType(UNIT_TYPE_SUMMONED);

        CreateTrigger()
          .RegisterUnitEvent(summonedTauren, EVENT_UNIT_DEATH)
          .AddAction(() =>
          {
            AddSpecialEffect(DeathEffect, GetUnitX(summonedTauren), GetUnitY(summonedTauren))
              .SetLifespan(1);
            
            summonedTauren.Remove();
            
            GetTriggeringTrigger()
              .Destroy();
          });
        
        AddSpecialEffect(SummonEffect, targetPoint.X, targetPoint.Y)
          .SetLifespan();
      }
      ancestralLegionData.RememberedUnits -= taurenToSummon;
      RegenerateTooltip(caster);
    }

    /// <inheritdoc />
    public override void OnLearn(unit learner)
    {
      if (AncestralLegionDataByUnit.ContainsKey(learner)) 
        return;
      
      var newAncestralLegionData = new AncestralLegionData
      {
        BaseTooltips = new string[]
        {
          BlzGetAbilityExtendedTooltip(Id, 0),
          BlzGetAbilityExtendedTooltip(Id, 1),
          BlzGetAbilityExtendedTooltip(Id, 2)
        }
      };
      AncestralLegionDataByUnit.Add(learner, newAncestralLegionData);
        
      RegisterRememberUnitsOnDeathTrigger(learner, newAncestralLegionData);
    }

    private void RegisterRememberUnitsOnDeathTrigger(unit learner, AncestralLegionData ancestralLegionData)
    {
      PlayerUnitEvents.Register(UnitTypeEvent.Dies, () =>
      {
        if (!UnitIsRememberable(GetTriggerUnit()))
          return;
        
        ancestralLegionData.RememberedUnits++;
        RegenerateTooltip(learner);
      }, RememberableUnitTypeId);
    }

    private bool UnitIsRememberable(unit getTriggerUnit) =>
      !IsUnitType(getTriggerUnit, UNIT_TYPE_SUMMONED) && GetRandomReal(0, 1) < RememberChance;

    private void RegenerateTooltip(unit caster)
    {
      var ancestralLegionData = AncestralLegionDataByUnit[caster];
      for (var i = 0; i < 3; i++)
        BlzSetAbilityExtendedTooltip(Id, $"{ancestralLegionData.BaseTooltips[i]}|n|n|cffDA9531Remembered Tauren:|r {ancestralLegionData.RememberedUnits}", i);
    }

    private sealed class AncestralLegionData
    {
      /// <summary>
      /// The tooltips for Ancestral Legion at the start of the game.
      /// </summary>
      public string[] BaseTooltips { get; init; } = Array.Empty<string>();
      
      /// <summary>How many units the caster has remembered.</summary>
      public int RememberedUnits { get; set; }
    }
  }
}