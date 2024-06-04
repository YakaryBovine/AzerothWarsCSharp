using War3Api.Object;
using War3Net.Build;
using War3Net.CodeAnalysis.Jass.Extensions;
using WarcraftLegacies.Shared.Models.Abilities;

namespace Launcher.MapMigrations.AbilityMigrations
{
  public sealed class SpawnTentacleMigration : IMapMigration
  {
    private readonly int _abilityTypeId;
    private readonly SpawnTentacleModel _data;

    public SpawnTentacleMigration(int abilityTypeId, SpawnTentacleModel data)
    {
      _abilityTypeId = abilityTypeId;
      _data = data;
    }
    
    /// <inheritdoc />
    public void Migrate(Map map, ObjectDatabase objectDatabase)
    {
      var spawnTentacle = objectDatabase.GetAbility(_abilityTypeId.InvertEndianness());
      
      spawnTentacle.TextTooltipLearn = $"Learn {_data.Name} - [|cffffcc00Level %d|r]";
      for (var i = 0; i < spawnTentacle.StatsRequiredLevel; i++)
      {
        var level = i + 1;
        spawnTentacle.StatsManaCost[i] = _data.ManaCost;
        spawnTentacle.StatsCooldown[i] = _data.Cooldown;
        spawnTentacle.TextTooltipNormal[i] = $"{_data.Name} - [|cffffcc00Level {level}|r]";
        var hitPoints = _data.HitPoints.Base + _data.HitPoints.PerLevel * level;
        var damage = _data.HitPoints.Base + _data.HitPoints.PerLevel * level;
        spawnTentacle.TextTooltipNormalExtended[i] = $"Spawns a Tentacle with {hitPoints} hit points and {damage} damage at the target location. The Tentacle's attacks deal damage to all units in a straight line in front of it.";
      }
    }
  }
}