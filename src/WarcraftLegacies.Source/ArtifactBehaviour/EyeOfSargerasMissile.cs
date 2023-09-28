using MacroTools.Extensions;
using WCSharp.Missiles;
using static War3Api.Common;

namespace WarcraftLegacies.Source.ArtifactBehaviour
{
  /// <summary>
  /// Carries the Eye of Sargeras into the target's inventory.
  /// </summary>
  public sealed class EyeOfSargerasMissile : BasicMissile
  {
    private readonly item _eyeOfSargeras;
    private bool _impacted;
    private const string ImpactEffect = @"Abilities\Spells\Undead\DarkRitual\DarkRitualTarget.mdl";

    /// <summary>
    /// Initializes a new instance of the <see cref="EyeOfSargerasMissile"/> class.
    /// </summary>
    public EyeOfSargerasMissile(unit caster, unit target, item eyeOfSargeras) : base(caster, target)
    {
      EffectString = @"Abilities\Spells\Undead\OrbOfDeath\AnnihilationMissile.mdl";
      EffectScale = 1.5f;
      Arc = 0.3f;
      Speed = 700;
      _eyeOfSargeras = eyeOfSargeras.SetPosition(20229f, 24244);
      CollisionRadius = 100;
      Active = true;
      CasterLaunchZ = 50;
      TargetImpactZ = 50;
    }

    /// <inheritdoc />
    public override void OnImpact()
    {
      if (!UnitAlive(Target)) 
        return;
      
      Target
        .AddAbility(Constants.ABILITY_A01Y_INVENTORY_DUMMY_DROP_ARTIFACT)
        .AddItemSafe(_eyeOfSargeras);
      _impacted = true;
        
      AddSpecialEffect(ImpactEffect, GetUnitX(Target), GetUnitY(Target))
        .SetScale(2)
        .SetLifespan(1);
      
      var eyeEffect = AddSpecialEffectTarget(@"Doodads\Cinematic\EyeOfSargeras\EyeOfSargeras.mdl", Target, "overhead");
      CreateTrigger()
        .RegisterUnitEvent(Target, EVENT_UNIT_DEATH)
        .AddAction(() =>
        {
          eyeEffect.Destroy();
          DestroyTrigger(GetTriggeringTrigger());
        });
    }

    /// <inheritdoc />
    public override void OnDispose()
    {
      if (_impacted) 
        return;
      
      _eyeOfSargeras.SetPosition(MissileX, MissileY);
      AddSpecialEffect(ImpactEffect, MissileX, MissileY)
        .SetScale(2)
        .SetLifespan(1);
    }
  }
}