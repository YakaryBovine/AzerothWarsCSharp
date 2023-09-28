using System;
using System.Collections.Generic;
using System.Linq;
using MacroTools.ControlPointSystem;
using MacroTools.DialogueSystem;
using MacroTools.FactionSystem;
using MacroTools.Save;
using static War3Api.Common;

namespace MacroTools.Extensions
{
  /// <summary>
  /// Provides extra information about players that is not already tracked by the Warcraft 3 engine.
  /// </summary>
  internal sealed class PlayerData
  {
    /// <summary>
    /// Fired when the player leaves a team.
    /// </summary>
    public event EventHandler<PlayerChangeTeamEventArgs>? PlayerLeftTeam;
    
    /// <summary>
    /// Fired when the player joins a team.
    /// </summary>
    public event EventHandler<PlayerChangeTeamEventArgs>? PlayerJoinedTeam;

    /// <summary>
    /// Fired when the player changes their <see cref="Faction"/>.
    /// </summary>
    public event EventHandler<PlayerFactionChangeEventArgs>? ChangedFaction;
    
    private static readonly Dictionary<int, PlayerData> ById = new();
    private readonly Dictionary<int, int> _objectLevels = new();

    private readonly Dictionary<int, int> _objectLimits = new();
    private float _goldPerMinute;
    private float _bonusGoldPerMinute;

    private readonly player _player;
    private Team? _team;
    private Faction? _faction;

    private float _partialGold;
    private float _partialLumber;
    
    private readonly Queue<IHasPlayableDialogue> _dialogueQueue = new();
    private bool _dialoguePlaying;

    /// <summary>
    /// Fired when the player's income changes.
    /// </summary>
    public event EventHandler<PlayerData>? IncomeChanged;
    
    /// <summary>
    /// Fired when the <see cref="player" />'s <see cref="ControlPoint" />s change
    /// </summary>
    public event EventHandler<PlayerData>? ControlPointsChanged;

    /// <summary>
    /// Fired when any player changes <see cref="Faction"/>.
    /// Todo: remove this and use the instance version instead.
    /// </summary>
    public static event EventHandler<PlayerFactionChangeEventArgs>? FactionChange;
    
    /// <summary>
    /// Controls who the player is allied to.
    /// </summary>
    public Team? Team
    {
      get => _team;
      set
      {
        if (_team != null)
        {
          _team?.RemovePlayer(_player);
          PlayerLeftTeam?.Invoke(this, new PlayerChangeTeamEventArgs(_player, _team));
        }

        if (value == null) return;
        var prevTeam = _team;
        _team = value;
        value.AddPlayer(_player);
        PlayerJoinedTeam?.Invoke(this, new PlayerChangeTeamEventArgs(_player, prevTeam));
      }
    }
    
    /// <summary>
    ///   Controls name, available objects, color, and icon.
    /// </summary>
    public Faction? Faction
    {
      get => _faction;
      set
      {
        var prevFaction = Faction;

        //Unapply old faction
        if (_faction != null)
        {
          _faction = null;
          if (prevFaction != null)
            prevFaction.Player = null; //Referential integrity
        }

        //Apply new faction
        if (value != null)
        {
          if (value.Player == null)
          {
            _player.SetColor(value.PlayerColor, true);
            _faction = value;
            //Enforce referential integrity
            if (value.Player != _player)
              value.Player = _player;
          }
          else
          {
            throw new Exception("Attempted to Person " + GetPlayerName(_player) +
                                " to already occupied faction with name " + value.Name);
          }
        }

        FactionChange?.Invoke(this, new PlayerFactionChangeEventArgs(_player, prevFaction));
        ChangedFaction?.Invoke(this, new PlayerFactionChangeEventArgs(_player, prevFaction));
      }
    }

    public int EliminationTurns { get; set; }
    
    public void UpdatePlayerSetting(string setting, int value)
    {
      switch (setting)
      {
        case "CamDistance":
          PlayerSettings.CamDistance = value;
          _player.ApplyCameraField(CAMERA_FIELD_TARGET_DISTANCE, PlayerSettings.CamDistance, 1);
          break;
      }
      SaveManager.Save(PlayerSettings);
    }

    public void UpdatePlayerSetting(string setting, bool value)
    {
      switch (setting)
      {
        case "PlayDialogue":
          PlayerSettings.PlayDialogue = value;
          break;
        case "ShowQuestText":
          PlayerSettings.ShowQuestText = value;
          break;
        case "ShowCaptions":
          PlayerSettings.ShowCaptions = value;
          break;
      }
      SaveManager.Save(PlayerSettings);
    }
    
    public float LumberIncome { get; set; }

    /// <summary>
    ///   Gold per second gained from all sources.
    /// </summary>
    public float TotalIncome => BaseIncome + BonusIncome;

    /// <summary>
    ///   Gold per second gained from secondary sources like Forsaken's plagued buildings.
    /// </summary>
    public float BonusIncome
    {
      get => _bonusGoldPerMinute;
      set
      {
        _bonusGoldPerMinute = value;
        IncomeChanged?.Invoke(this, this);
      }
    }

    /// <summary>
    ///   Gold per second gained from primary sources like Control Points.
    /// </summary>
    public float BaseIncome
    {
      get => _goldPerMinute;
      set
      {
        if (value < 0)
          throw new ArgumentOutOfRangeException(
            $"Tried to assign a negative {nameof(BaseIncome)} value to {GetPlayerName(_player)}.");

        _goldPerMinute = value;
        IncomeChanged?.Invoke(this, this);
      }
    }

    public PlayerSettings PlayerSettings => SaveManager.SavesByPlayer.ContainsKey(_player)? SaveManager.SavesByPlayer[_player]: CreateNewPlayerSettings();

    private PlayerSettings CreateNewPlayerSettings()
    {
      var newPlayerSettings = new PlayerSettings();
      newPlayerSettings.SetPlayer(_player);
      SaveManager.SavesByPlayer[_player] = newPlayerSettings;
      return newPlayerSettings;
    }

    /// <summary>
    /// Control points the player owns
    /// </summary>
    public List<ControlPoint> ControlPoints { get; } = new();
    
    /// <summary>The number of extra <see cref="ControlPoint.ControlLevel"/>s the player gets each turn.</summary>
    public float ControlLevelPerTurnBonus { get; set; }

    private PlayerData(player player)
    {
      _player = player;
      EliminationTurns = 0;
    }
    
    /// <summary>
    /// Queues the <see cref="IHasPlayableDialogue"/> for this player, playing it the next time there is nothing else playing.
    /// </summary>
    internal void QueueDialogue(IHasPlayableDialogue dialogue)
    {
      _dialogueQueue.Enqueue(dialogue);

      if (_dialoguePlaying)
        return;
      
      _dialoguePlaying = true;
      CreateTrigger().AddAction(() =>
      {
        while (_dialogueQueue.Any())
        {
          var nextDialogue = _dialogueQueue.Dequeue();
          nextDialogue.Play(_player);
          TriggerSleepAction(nextDialogue.Length);
          TriggerSleepAction(5f);
        }
        _dialoguePlaying = false;
        GetTriggeringTrigger().Destroy();
      }).Execute();
    }
    
    /// <summary>
    /// Adds <see cref="ControlPoint" /> to list of this <see cref="player" />'s controlpoints, updates the <see cref="Team" /> total and fires any events subscribed to ControlPointsChanged
    /// </summary>
    public void AddControlPoint(ControlPoint controlPoint)
    {
      ControlPoints.Add(controlPoint);
      ControlPointsChanged?.Invoke(this, this);
    }
    
    /// <summary>
    /// Removes <see cref="ControlPoint" /> from list of this <see cref="player" />'s controlpoints, updates the <see cref="Team" /> total and fires any events subscribed to ControlPointsChanged
    /// </summary>
    public void RemoveControlPoint(ControlPoint controlPoint)
    {
      ControlPoints.Remove(controlPoint);
      ControlPointsChanged?.Invoke(this, this);
    }

    public int GetObjectLevel(int obj) => _objectLevels.ContainsKey(obj) ? _objectLevels[obj] : 0;

    public void SetObjectLevel(int obj, int level)
    {
      var objectLimit = _player.GetObjectLimit(obj);
      
      if (level > objectLimit)
        throw new ArgumentException(
          $"{nameof(level)} ({level}) cannot be higher than the object limit for {GetObjectName(obj)} ({objectLimit}).",
          $"{nameof(level)}");
      
      //Object levels cannot be changed for objects with a limit of 0.
      //This works around it by increasing the limit to 1 before making the change, then reverting it back.
      var revertAfter = false;

      if (objectLimit < 1)
      {
        SetPlayerTechMaxAllowed(_player, obj, 100);
        revertAfter = true;
      }

      SetPlayerTechResearched(_player, obj, Math.Max(level, 0));

      _objectLevels[obj] = level;
      if (revertAfter)
        SetPlayerTechMaxAllowed(_player, obj, 0);
      
      if (GetPlayerTechCount(_player, obj, false) != Math.Max(level, 0))
        throw new InvalidOperationException(
          $"Failed to set the object level of {GetObjectName(obj)} to {level}; it is {GetPlayerTechCount(_player, obj, false)} instead.");
    }

    public int GetObjectLimit(int id) => _objectLimits.TryGetValue(id, out var limit) ? limit : 0;

    public void SetObjectLimit(int id, int limit)
    {
      _objectLimits[id] = limit;

      if (limit >= Faction.UNLIMITED)
        SetPlayerTechMaxAllowed(_player, id, -1);
      else if (limit <= 0)
        SetPlayerTechMaxAllowed(_player, id, 0);
      else
        SetPlayerTechMaxAllowed(_player, id, limit);
    }

    public void ModObjectLimit(int id, int limit) =>
      SetObjectLimit(id, GetObjectLimit(id) + limit);

    public void AddGold(float x)
    {
      var fullGold = (float) Math.Floor(x);
      var remainderGold = x - fullGold;

      SetPlayerState(_player, PLAYER_STATE_RESOURCE_GOLD,
        GetPlayerState(_player, PLAYER_STATE_RESOURCE_GOLD) + R2I(fullGold));
      _partialGold += remainderGold;

      while (true)
      {
        if (_partialGold < 1) break;

        _partialGold -= 1;
        SetPlayerState(_player, PLAYER_STATE_RESOURCE_GOLD, GetPlayerState(_player, PLAYER_STATE_RESOURCE_GOLD) + 1);
      }
    }

    public void AddLumber(float x)
    {
      var fullLumber = (float) Math.Floor(x);
      var remainderLumber = x - fullLumber;

      SetPlayerState(_player, PLAYER_STATE_RESOURCE_LUMBER,
        GetPlayerState(_player, PLAYER_STATE_RESOURCE_LUMBER) + R2I(fullLumber));
      _partialLumber += remainderLumber;

      while (true)
      {
        if (_partialLumber < 1) break;

        _partialLumber -= 1;
        SetPlayerState(_player, PLAYER_STATE_RESOURCE_LUMBER, GetPlayerState(_player, PLAYER_STATE_RESOURCE_LUMBER) + 1);
      }
    }

    /// <summary>
    ///   Retrieves the <see cref="PlayerData" /> object which contains information about the given <see cref="player" />.
    /// </summary>
    public static PlayerData ByHandle(player whichPlayer)
    {
      if (ById.TryGetValue(GetPlayerId(whichPlayer), out var person)) return person;

      var newPerson = new PlayerData(whichPlayer);
      Register(newPerson);
      return newPerson;
    }

    /// <summary>
    ///   Register a <see cref="PlayerData" /> to the Person system.
    /// </summary>
    private static void Register(PlayerData playerData)
    {
      ById.Add(GetPlayerId(playerData._player), playerData);
    }
  }
}