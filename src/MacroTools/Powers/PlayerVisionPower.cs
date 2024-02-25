using System.Collections.Generic;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using static War3Api.Common;

namespace MacroTools.Powers
{
  public sealed class PlayerVisionPower : Power
  {
    private readonly IEnumerable<player?> _playersToGiveVisionOf;

    public PlayerVisionPower(string name, string description, IEnumerable<player?> playersToGiveVisionOf)
    {
      Name = name;
      Description = description;
      _playersToGiveVisionOf = playersToGiveVisionOf;
    }

    public override void OnAdd(player sourcePlayer)
    {
      foreach (var otherPlayer in _playersToGiveVisionOf)
      {
        otherPlayer?.SetAllianceState(sourcePlayer, AllianceState.UnalliedVision);
      }
    }

    public override void OnRemove(player sourcePlayer)
    {
      foreach (var otherPlayer in _playersToGiveVisionOf)
      {
        otherPlayer?.SetAllianceState(sourcePlayer, AllianceState.Unallied);
      }
    }
  }
}