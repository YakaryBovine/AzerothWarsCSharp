using System;
using System.Collections.Generic;
using System.Linq;
using MacroTools.Extensions;
using MacroTools.LegendSystem;
using static War3Api.Common;

namespace MacroTools.FactionSystem
{
  /// <summary>
  /// A collection of players who are all allied to each other.
  /// </summary>
  public sealed class Team
  {
    private readonly List<player> _members = new();
    private readonly List<player> _invitees = new();

    public Team(string name)
    {
      Name = name;
    }

    public string Name { get; }

    /// <summary>
    ///   Returns the number of real <see cref="player" />s within this <see cref="Team" />.
    /// </summary>
    public int Size => _members.Count;

    /// <summary>
    /// The number of players in the <see cref="Team"/> who have not been defeated.
    /// </summary>
    public int UndefeatedPlayerCount => _members.Count(x => x.GetFaction()?.ScoreStatus != ScoreStatus.Defeated);

    /// <summary>
    ///   Music that plays when this <see cref="Team" /> wins the game.
    /// </summary>
    public string? VictoryMusic { get; init; }

    public IEnumerable<Faction> GetAllFactions()
    {
      foreach (var member in _members)
      {
        var memberFaction = member.GetFaction();
        if (memberFaction != null)
        {
          yield return memberFaction;
        }
      }
    }

    internal void RemovePlayer(player whichPlayer)
    {
      if (!_members.Contains(whichPlayer))
      {
        throw new Exception($"Attempted to remove non-present player {GetPlayerName(whichPlayer)} from team {Name})");
      }
      _members.Remove(whichPlayer);
      UnallyPlayer(whichPlayer);
    }

    internal void AddPlayer(player whichPlayer)
    {
      if (_members.Contains(whichPlayer))
        throw new Exception($"Attempted to add already present player {GetPlayerName(whichPlayer)} to team {Name})");
      _members.Add(whichPlayer);
      AllyPlayer(whichPlayer);
    }

    /// <summary>
    ///   Causes every <see cref="player" /> in the <see cref="Team" /> to ally the given player, and vise-versa.
    /// </summary>
    /// <param name="whichPlayer"></param>
    internal void AllyPlayer(player whichPlayer)
    {
      foreach (var player in _members)
      {
        whichPlayer.SetAllianceState(player, AllianceState.AlliedVision);
        player.SetAllianceState(whichPlayer, AllianceState.AlliedVision);
      }
    }

    /// <summary>
    ///   Causes every <see cref="player" /> in the <see cref="Team" /> to unally the given player, and vise-versa.
    /// </summary>
    /// <param name="whichPlayer"></param>
    internal void UnallyPlayer(player whichPlayer)
    {
      foreach (var player in _members)
      {
        whichPlayer.SetAllianceState(player, AllianceState.Unallied);
        player.SetAllianceState(whichPlayer, AllianceState.Unallied);
      }
    }

    /// <summary>
    ///   Revokes an invite sent to a player.
    /// </summary>
    public void Uninvite(player whichPlayer)
    {
      if (!_invitees.Contains(whichPlayer)) 
        return;
      
      var faction = whichPlayer.GetFaction();
      if (faction == null)
        return;
      
      DisplayText($"{faction.ColoredName}|r is no longer invited to join the {Name}.");
      DisplayTextToPlayer(faction.Player, 0, 0, $"You are no longer invited to join the {Name}.");
      _invitees.Remove(whichPlayer);
    }

    /// <summary>
    ///   Sends an invite to this team to a player, which they can choose to accept at a later date.
    /// </summary>
    public void Invite(player whichPlayer)
    {
      if (_members.Contains(whichPlayer) || _invitees.Contains(whichPlayer)) 
        return;
      
      var faction = whichPlayer.GetFaction();
      
      if (faction == null) 
        return;
      DisplayText($"{faction.ColoredName}|r has been invited to join the {Name}.");
      DisplayTextToPlayer(faction.Player, 0, 0,
        $"You have been invited to join the {Name}. Type -join {Name} to accept.");
      _invitees.Add(whichPlayer);
    }

    /// <summary>
    ///   Displays the provided text to all <see cref="player" />s in the <see cref="Team" />.
    /// </summary>
    /// <param name="text"></param>
    public void DisplayText(string text)
    {
      foreach (var player in _members) 
        DisplayTextToPlayer(player, 0, 0, text);
    }

    /// <summary>
    ///   Checks whether or not the given <see cref="Faction" /> has been invited to this <see cref="Team" />.
    /// </summary>
    public bool IsPlayerInvited(player whichPlayer) => _invitees.Contains(whichPlayer);

    /// <summary>
    ///   Checks whether or not the given <see cref="player" /> is in this <see cref="Team" />.
    /// </summary>
    /// <param name="whichPlayer"></param>
    /// <returns></returns>
    public bool Contains(player whichPlayer) => _members.Contains(whichPlayer);

    /// <summary>
    ///   Checks whether or not any <see cref="player" />'s faction in the <see cref="Team" /> has an Essential <see cref="Legend" />.
    /// </summary>
    /// <returns></returns>
    public bool DoesTeamHaveEssentialLegend()
    {
      foreach (var player in _members)
        if (player.GetFaction()!.HasEssentialLegend)
          return true;
      
      return false;
    }
  }
}