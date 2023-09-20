using System;
using System.Collections.Generic;
using System.Linq;
using MacroTools.Augments;
using MacroTools.ControlPointSystem;
using MacroTools.Extensions;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives;
using MacroTools.QuestSystem;
using MacroTools.ResearchSystems;
using WCSharp.Events;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace MacroTools.FactionSystem
{
  /// <summary>
  ///   Represents a faction in the Azeroth Wars universe, such as Lordaeron, Stormwind, or the Frostwolf Clan.
  ///   Governs techtrees and quests.
  /// </summary>
  public sealed class Faction
  {
    /// <summary>
    ///   Signifies unlimited unit production.
    /// </summary>
    public const int UNLIMITED = 200;

    /// <summary>
    ///   The gold cost value of a hero.
    /// </summary>
    public const int HeroCost = 100;

    /// <summary>
    ///   How much gold and lumber is refunded from units that get refunded when a player leaves.
    /// </summary>
    private const float RefundMultiplier = 1;

    /// <summary>
    ///   How much experience is transferred from heroes that leave the game.
    /// </summary>
    private const float ExperienceTransferMultiplier = 1;

    /// <summary>
    ///   The amount of food <see cref="Faction" />s can have by default.
    /// </summary>
    private const int FoodMaximumDefault = 150;

    private readonly Dictionary<int, int> _abilityAvailabilities = new();
    private readonly List<Augment> _augments = new();

    private readonly int _defeatedResearch;
    private readonly List<unit> _goldMines = new();

    private readonly Dictionary<int, int> _objectLevels = new();
    private readonly Dictionary<int, int> _objectLimits = new();
    private readonly List<Power> _powers = new();
    private readonly Dictionary<string, QuestData> _questsByName = new();
    private int _foodMaximum;
    private string _icon;
    private string _name;
    private player? _player;
    private int _undefeatedResearch;
    private int _xp; //Stored by DistributeUnits and given out again by DistributeResources

    /// <summary>
    ///   Fired when the <see cref="Faction" /> gains a <see cref="Power" />.
    /// </summary>
    public EventHandler<FactionPowerEventArgs>? PowerAdded;

    /// <summary>
    ///   Fired when the <see cref="Faction" /> loses a <see cref="Power" />.
    /// </summary>
    public EventHandler<FactionPowerEventArgs>? PowerRemoved;

    /// <summary>
    /// Invoked when <see cref="ScoreStatus"/> changes.
    /// </summary>
    public EventHandler<Faction>? ScoreStatusChanged;

    static Faction()
    {
      PlayerUnitEvents.Register(ResearchEvent.IsFinished, () =>
      {
        try
        {
          var faction = FactionManager.GetFromPlayer(GetTriggerPlayer());
          if (faction == null)
            return;
          var researchId = GetResearched();
          var research = ResearchManager.GetFromTypeId(researchId);
          if (research == null || !research.IncompatibleWith.Any(x => faction.GetObjectLevel(x.ResearchTypeId) > 0))
          {
            faction.SetObjectLevel(researchId, GetPlayerTechCount(GetTriggerPlayer(), researchId, true));
            if (research == null)
              return;
            research.OnResearch(GetTriggerPlayer());
            foreach (var otherResearch in research.IncompatibleWith)
              faction.SetObjectLimit(otherResearch.ResearchTypeId, -UNLIMITED, true);
          }
          else
          {
            faction.SetObjectLimit(researchId, -UNLIMITED, true);
            research.Refund(GetTriggerPlayer());
          }
        }
        catch (Exception ex)
        {
          Logger.LogError(ex.ToString());
        }
      });
    }

    /// <summary>
    ///   Displayed to the <see cref="Faction" /> when the game starts.
    /// </summary>
    public string? IntroText { get; init; }

    /// <summary>
    /// All of the <see cref="Faction"/>'s <see cref="ControlPoint"/> <see cref="ControlPoint.Defender"/>s
    /// will be represented by this unit type.
    /// </summary>
    public int? ControlPointDefenderUnitTypeId { get; init; }

    /// <summary>
    /// Check to see if <see cref="Faction"/> has any living essential legends
    /// </summary>
    public bool HasEssentialLegend => GetEssentialLegends().Count > 0;

    /// <summary>How much gold the faction starts with.</summary>
    public int StartingGold { get; set; }

    /// <summary>How much lumber the faction starts with.</summary>
    public int StartingLumber { get; set; }
    
    /// <summary>The units this faction should start the game with.</summary>
    public List<unit> StartingUnits { get; init; }
    
    /// <summary>Where any player occupying this faction should have their camera set to on game start.</summary>
    public Point? StartingCameraPosition { get; init; }

    /// <summary>Players with this faction will become this color.</summary>
    public playercolor PlayerColor { get; }

    public float Gold
    {
      get => I2R(GetPlayerState(Player, PLAYER_STATE_RESOURCE_GOLD));
      set => SetPlayerState(Player, PLAYER_STATE_RESOURCE_GOLD, R2I(value));
    }

    public float Lumber
    {
      get => I2R(GetPlayerState(Player, PLAYER_STATE_RESOURCE_LUMBER));
      set => SetPlayerState(Player, PLAYER_STATE_RESOURCE_LUMBER, R2I(value));
    }

    /// <summary>
    ///   The <see cref="Faction" />'s food limit.
    ///   A <see cref="player" /> with this Faction can never exceed this amount of food.
    /// </summary>
    public int FoodMaximum
    {
      get => _foodMaximum;
      set
      {
        _foodMaximum = value;
        if (Player != null) SetPlayerState(Player, PLAYER_STATE_FOOD_CAP_CEILING, value);
      }
    }

    /// <summary>
    ///   Music that will play for the Faction at the start of the game.
    /// </summary>
    public string CinematicMusic { get; init; }

    /// <summary>
    /// Whether or not the <see cref="Faction"/> has been defeated.
    /// </summary>
    public ScoreStatus ScoreStatus { get; private set; } = ScoreStatus.Undefeated;

    public string ColoredName => PrefixCol + _name + "|r";

    public string PrefixCol { get; }

    public string Name
    {
      get => _name;
      set
      {
        var formerName = _name;
        _name = value;
        NameChanged?.Invoke(this, new FactionNameChangeEventArgs(this, formerName));
      }
    }

    public string Icon
    {
      get => _icon;
      set
      {
        _icon = value;
        IconChanged?.Invoke(this, this);
      }
    }

    /// <summary>
    ///   The <see cref="player" /> currently occupying this <see cref="Faction" />.
    /// </summary>
    public player? Player
    {
      get => _player;
      set
      {
        if (Player != null)
        {
          Player.GetTeam()?.UnallyPlayer(Player);
          HideAllQuests();
          UnapplyObjects();
          UnapplyPowers();
        }

        _player = value;
        //Maintain referential integrity
        //Todo: this seems a bit silly
        if (value == null) return;

        if (value.GetFaction() != this)
          value.SetFaction(this);

        Player?.GetTeam()?.AllyPlayer(value);
        ApplyObjects();
        ApplyPowers();
        ShowAllQuests();
        SetPlayerState(Player, PLAYER_STATE_FOOD_CAP_CEILING, _foodMaximum);
      }
    }

    public QuestData? StartingQuest { get; set; }

    /// <summary>
    ///   This research is enabled for every player while this <see cref="Faction" /> is not defeated.
    /// </summary>
    public int UndefeatedResearch
    {
      set
      {
        if (_undefeatedResearch != 0) return;
        _undefeatedResearch = value;
        foreach (var player in WCSharp.Shared.Util.EnumeratePlayers())
          SetPlayerTechResearched(player, _undefeatedResearch, 1);
      }
      get => _undefeatedResearch;
    }

    /// <summary>
    /// Gets a list of legends that are flagged as essential and alive that the faction currently has
    /// </summary>
    private List<Legend> GetEssentialLegends()
    {
      var essentialLegends = new List<Legend>();
      foreach (var legend in LegendaryHeroManager.GetAll())
      {
        if (legend.Essential && legend.OwningPlayer == Player && legend.Unit.IsAlive())
        {
          essentialLegends.Add(legend);
        }
      }
      foreach (var capital in CapitalManager.GetAll())
      {
        if (capital.Essential && capital.OwningPlayer == Player && capital.Unit.IsAlive())
        {
          essentialLegends.Add(capital);
        }
      }
      
      return essentialLegends;
    }

    /// <summary>
    ///   This research is enabled for every player while this <see cref="Faction" /> is defeated.
    /// </summary>
    public int DefeatedResearch
    {
      init
      {
        if (_defeatedResearch != 0) return;
        _defeatedResearch = value;
        foreach (var player in WCSharp.Shared.Util.EnumeratePlayers())
          SetPlayerTechResearched(player, _defeatedResearch, 0);
      }
    }

    public Faction(string name, playercolor playerColor, string prefixCol, string icon)
    {
      _name = name;
      PlayerColor = playerColor;
      PrefixCol = prefixCol;
      _icon = icon;
      FoodMaximum = FoodMaximumDefault;
    }

    /// <summary>
    ///   Fires when the <see cref="Faction" /> joins a new <see cref="Team" />.
    /// </summary>
    public event EventHandler<Faction>? JoinedTeam;

    /// <summary>
    ///   Fires when the <see cref="Faction" /> changes its name.
    /// </summary>
    public event EventHandler<FactionNameChangeEventArgs>? NameChanged;

    /// <summary>
    /// Fired after the <see cref="Faction"/> leaves the game.
    /// </summary>
    public event EventHandler<Faction>? LeftGame;

    /// <summary>
    /// Fired when the <see cref="Faction"/>'s has changed.
    /// </summary>
    public event EventHandler<Faction>? IconChanged;

    /// <summary>
    /// Fired after the <see cref="Faction"/>'s status has changed.
    /// </summary>
    public event EventHandler<Faction>? StatusChanged;

    /// <summary>
    /// Defeats the player, making them an observer, and distributing their units and resources to allies if possible.
    /// </summary>
    public void Defeat()
    {
      foreach (var player in WCSharp.Shared.Util.EnumeratePlayers())
      {
        SetPlayerTechResearched(player, _defeatedResearch, 1);
        SetPlayerTechResearched(player, _undefeatedResearch, 0);
      }
      
      if (Player != null)
      {
        FogModifierStart(CreateFogModifierRect(Player, FOG_OF_WAR_VISIBLE,
          WCSharp.Shared.Data.Rectangle.WorldBounds.Rect, false, false));
        RemovePlayer(Player, PLAYER_GAME_RESULT_DEFEAT);
        SetPlayerState(Player, PLAYER_STATE_OBSERVER, 1);
        Leave();
      }

      ScoreStatus = ScoreStatus.Defeated;
      StatusChanged?.Invoke(this, this);
      ScoreStatusChanged?.Invoke(this, this);
    }
    
    /// <summary>
    ///   Returns all unit types which this <see cref="Faction" /> can only train a limited number of.
    /// </summary>
    public IEnumerable<int> GetLimitedObjects()
    {
      return _objectLimits.Keys;
    }

    /// <summary>
    ///   Returns the maximum number of times the Faction can train a unit, build a building, or research a research.
    /// </summary>
    /// <param name="whichObject">The object ID of a unit, building, or research.</param>
    public int GetObjectLimit(int whichObject)
    {
      return _objectLimits[whichObject];
    }

    /// <summary>
    ///   Registers a gold mine as belonging to this <see cref="Faction" />.
    ///   When the Faction leaves the game, all of their goldmines are removed.
    /// </summary>
    public void AddGoldMine(unit whichUnit)
    {
      _goldMines.Add(whichUnit);
    }

    /// <summary>
    ///   Adds a <see cref="Augment" /> to this <see cref="Faction" />.
    /// </summary>
    /// <param name="augment"></param>
    public void AddAugment(Augment augment)
    {
      _augments.Add(augment);
      augment.OnAdd(this);
    }

    /// <summary>
    ///   Adds a <see cref="Power" /> to this <see cref="Faction" />.
    /// </summary>
    public void AddPower(Power power)
    {
      _powers.Add(power);
      if (Player != null) power.OnAdd(Player);
      PowerAdded?.Invoke(this, new FactionPowerEventArgs(this, power));
    }

    /// <summary>
    ///   Removes a <see cref="Power" /> from this <see cref="Faction" />.
    /// </summary>
    public void RemovePower(Power power)
    {
      _powers.Remove(power);
      if (Player != null) power.OnRemove(Player);
      PowerRemoved?.Invoke(this, new FactionPowerEventArgs(this, power));
    }

    /// <summary>
    ///   Removes a <see cref="Power" /> from this <see cref="Faction" />.
    /// </summary>
    public void RemovePowerByName(string name)
    {
      var power = _powers.Where(x => x.Name == name).FirstOrDefault();
      if (power == null)
        throw new ArgumentException($"Unable to find power with name {name}");
      _powers.Remove(power);
      if (Player != null) power.OnRemove(Player);
      PowerRemoved?.Invoke(this, new FactionPowerEventArgs(this, power));
    }
    /// <summary>
    ///   Gets the first <see cref="Power" /> this <see cref="Faction" /> has with the provided type.
    /// </summary>
    public T? GetPowerByType<T>() where T : Power
    {
      foreach (var power in _powers)
        if (power.GetType() == typeof(T))
          return (T)power;

      return null;
    }

    /// <summary>
    ///   Unallies the <see cref="Faction" /> from all of its allies, creating a new <see cref="Team" />
    ///   based on its name.
    /// </summary>
    public void Unally()
    {
      if (!(Player?.GetTeam()?.Size > 1)) return;
      var newTeamName = Name + " Pact";
      if (FactionManager.TeamWithNameExists(newTeamName))
      {
        Player.SetTeam(FactionManager.GetTeamByName(newTeamName));
        return;
      }

      var newTeam = new Team(newTeamName);
      FactionManager.Register(newTeam);
      Player.SetTeam(newTeam);
    }

    public QuestData AddQuest(QuestData questData)
    {
      questData.Add(this);
      _questsByName.Add(questData.Title, questData);
      if (GetLocalPlayer() == Player)
        questData.ShowLocal();
      questData.ShowSync();
      questData.ProgressChanged += OnQuestProgressChanged;
      return questData;
    }

    public int GetObjectLevel(int obj) => _objectLevels.ContainsKey(obj) ? _objectLevels[obj] : 0;

    /// <summary>
    /// Sets the current level of a particular research for the <see cref="Faction"/>.
    /// </summary>
    public void SetObjectLevel(int obj, int level)
    {
      _objectLevels[obj] = level;
      Player?.SetObjectLevel(obj, level);
    }

    /// <summary>
    ///   Changes the ability's availability by the provided amount.
    ///   If the availability is higher than 0, the <see cref="player" /> with this <see cref="Faction" />
    ///   can see and use it.
    /// </summary>
    public void ModAbilityAvailability(int ability, int value)
    {
      if (_abilityAvailabilities.ContainsKey(ability))
        _abilityAvailabilities[ability] += value;
      else
        _abilityAvailabilities[ability] = value;
      Player?.SetAbilityAvailability(ability, value > 0);
    }

    /// <summary>
    ///   Changes the limit of the given object that the <see cref="Faction" /> can train, build, or research.
    /// </summary>
    /// <param name="objectId">The object ID to modify the limit of.</param>
    /// <param name="limit">The amount to adjust the limit by.</param>
    /// <param name="isResearch">Should be true if the input ID is a research.</param>
    public void ModObjectLimit(int objectId, int limit, bool isResearch = false)
    {
      if (_objectLimits.ContainsKey(objectId))
        _objectLimits[objectId] += limit;
      else
        _objectLimits.Add(objectId, limit);

      //If a player has this Faction, adjust their techtree as well
      Player?.ModObjectLimit(objectId, limit);

      if (_objectLimits[objectId] == 0)
        _objectLimits.Remove(objectId);
    }

    /// <summary>
    ///   Sets the limit of the given object that the <see cref="Faction" /> can train, build, or research.
    /// </summary>
    /// <param name="objectId">The object ID to modify the limit of.</param>
    /// <param name="limit">The amount to set the limit to.</param>
    /// <param name="isResearch">Should be true if the input ID is a research.</param>
    public void SetObjectLimit(int objectId, int limit, bool isResearch = false)
    {
      if (_objectLimits.ContainsKey(objectId))
        _objectLimits[objectId] = limit;
      else
        _objectLimits.Add(objectId, limit);

      //If a player has this Faction, adjust their techtree as well
      Player?.SetObjectLimit(objectId, limit);

      if (_objectLimits[objectId] == 0)
        _objectLimits.Remove(objectId);
    }

    /// <summary>
    ///   Causes the <see cref="Faction" />'s <see cref="player" /> to lose everything they control,
    ///   without distributing it to members of their <see cref="Team" />.
    /// </summary>
    public void Obliterate()
    {
      //Take away resources
      SetPlayerState(Player, PLAYER_STATE_RESOURCE_GOLD, 0);
      SetPlayerState(Player, PLAYER_STATE_RESOURCE_LUMBER, 0);

      //Give all units to Neutral Victim
      foreach (var unit in CreateGroup().EnumUnitsOfPlayer(Player).EmptyToList())
      {
        var tempUnitType = UnitType.GetFromHandle(unit);
        if (!UnitAlive(unit))
          unit.Remove();

        if (!tempUnitType.NeverDelete)
          SetUnitOwner(unit, Player(GetBJPlayerNeutralVictim()), false);
      }
    }

    /// <summary>
    ///   Returns all <see cref="Power" />s this <see cref="Faction" /> has.
    /// </summary>
    public IEnumerable<Power> GetAllPowers()
    {
      foreach (var power in _powers) yield return power;
    }

    /// <summary>
    ///   Attempts to retrieve a <see cref="QuestData" /> belonging to this <see cref="Faction" /> with the given title.
    /// </summary>
    public QuestData? GetQuestByTitle(string parameter) =>
      _questsByName.ContainsKey(parameter) ? _questsByName[parameter] : null;

    /// <summary>
    /// Returns the first <see cref="QuestData"/> the <see cref="Faction"/> has with the given <see cref="Type"/>.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public QuestData GetQuestByType(Type type)
    {
      var quest = _questsByName.Values.FirstOrDefault(x => x.GetType() == type);
      if (quest == null)
        throw new Exception($"{Name} does not have a {nameof(QuestData)} of type {type.Name}");
      return quest;
    }

    /// <summary>
    /// Returns all <see cref="QuestData"/>s the <see cref="Faction"/> can complete.
    /// </summary>
    /// <returns></returns>
    public List<QuestData> GetAllQuests() => _questsByName.Values.ToList();

    private void ApplyPowers()
    {
      if (Player == null) return;
      foreach (var power in _powers)
        power.OnAdd(Player);
    }

    private void UnapplyPowers()
    {
      if (Player == null) return;
      foreach (var power in _powers)
        power.OnRemove(Player);
    }

    //Adds this Faction's object limits and levels to its active Person
    private void ApplyObjects()
    {
      foreach (var (key, value) in _objectLimits)
        Player?.ModObjectLimit(key, value);

      foreach (var (key, value) in _objectLevels)
        Player?.SetObjectLevel(key, value);

      foreach (var (key, value) in _abilityAvailabilities)
        Player?.SetAbilityAvailability(key, value > 0);
    }

    //Removes this Faction's object limits and levels from its active Person
    private void UnapplyObjects()
    {
      foreach (var (key, value) in _objectLimits)
        Player?.ModObjectLimit(key, -value);

      foreach (var (key, _) in _objectLevels)
        Player?.SetObjectLevel(key, 0);

      foreach (var (key, value) in _abilityAvailabilities)
        Player?.SetAbilityAvailability(key, true);
    }

    /// <summary>
    ///   Shows all of the Faction's quest, rendering them in the quest log,
    ///   showing them on the minimap, and showing them on the map.
    /// </summary>
    private void ShowAllQuests()
    {
      foreach (var quest in _questsByName.Values)
      {
        if (GetLocalPlayer() == Player) quest.ShowLocal();

        quest.ShowSync();
      }
    }

    //Hides all of the Faction)s quests.
    private void HideAllQuests()
    {
      foreach (var quest in _questsByName.Values)
      {
        if (GetLocalPlayer() == Player) quest.HideLocal();

        quest.HideSync();
      }
    }

    private void OnQuestProgressChanged(object? sender, QuestProgressChangedEventArgs args)
    {
      try
      {
        args.Quest.ApplyFactionProgress(this, args.Quest.Progress, args.FormerProgress);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
      }
    }

    private void DistributeExperience(List<player?> playersToDistributeTo)
    {
      if (Player?.GetTeam() == null) return;
      foreach (var ally in playersToDistributeTo)
      {
        var allyHeroes = CreateGroup().EnumUnitsOfPlayer(ally).EmptyToList()
          .FindAll(unit => IsUnitType(unit, UNIT_TYPE_HERO));
        foreach (var hero in allyHeroes)
          AddHeroXP(hero, R2I(_xp / (Player.GetTeam()!.Size - 1) / allyHeroes.Count * ExperienceTransferMultiplier),
            true);
      }

      _xp = 0;
    }

    private void DistributeResources(List<player?> playersToDistributeTo)
    {
      foreach (var player in playersToDistributeTo)
      {
        if (player == null) continue;
        player.AdjustPlayerState(PLAYER_STATE_RESOURCE_GOLD, (int)(Gold / playersToDistributeTo.Count));
        player.AdjustPlayerState(PLAYER_STATE_RESOURCE_LUMBER, (int)(Lumber / playersToDistributeTo.Count));
      }

      Gold = 0;
      Lumber = 0;
    }

    private void DistributeUnits(IReadOnlyList<player?> playersToDistributeTo)
    {
      if (Player?.GetTeam() == null) return;
      var playerUnits = CreateGroup().EnumUnitsOfPlayer(Player).EmptyToList();

      foreach (var unit in playerUnits)
      {
        var loopUnitType = UnitType.GetFromHandle(unit);
        if (IsUnitType(unit, UNIT_TYPE_SUMMONED))
        {
          unit
            .Kill()
            .Remove();
          continue;
        }

        if (IsUnitType(unit, UNIT_TYPE_HERO))
        {
          Player?.AddGold(HeroCost);
          _xp += GetHeroXP(unit);
          if (LegendaryHeroManager.GetFromUnit(unit) != null)
            _xp -= LegendaryHeroManager.GetFromUnit(unit)!.StartingXp;
          unit
            .DropAllItems()
            .Kill()
            .Remove();
          continue;
        }

        if (!CapitalManager.UnitIsCapital(unit) && !CapitalManager.UnitIsProtector(unit) && !ControlPointManager.Instance.UnitIsControlPoint(unit) && !loopUnitType.NeverDelete)
        {
          if (!IsUnitType(unit, UNIT_TYPE_STRUCTURE))
          {
            Gold += loopUnitType.GoldCost * RefundMultiplier;
            Lumber += loopUnitType.LumberCost * RefundMultiplier;
          }
          unit
            .DropAllItems()
            .Kill()
            .Remove();
          continue;
        }

        unit.SetOwner(
          Player?.GetTeam()?.Size > 1
            ? playersToDistributeTo[GetRandomInt(0, playersToDistributeTo.Count - 1)]
            : Player(GetBJPlayerNeutralVictim()), false);
      }
    }

    /// <summary>
    ///   This should get used any time a player exits the game without being defeated;
    ///   IE they left, went afk, became an observer, or triggered an event that causes this.
    /// </summary>
    public void Leave()
    {
      Player?.GetTeam()?.PlayersToDistribute.Enqueue(Player);
      while (Player?.GetTeam()?.PlayersToDistribute.Count > 0 && !(bool)Player?.GetTeam()?.PrcessingDistributeQueue)
      {
        if (Player != null) Player.GetTeam()!.PrcessingDistributeQueue = true;
        var queueValue = Player?.GetTeam()?.PlayersToDistribute.Dequeue();
        var eligiblePlayers = queueValue?
          .GetTeam()?
          .GetAllFactions()
          .Where(x => x.ScoreStatus == ScoreStatus.Undefeated && x.Player != queueValue)
          .Select(x => x.Player)
          .ToList();
        if (eligiblePlayers != null && eligiblePlayers.Any() && GameTime.GetGameTime() > 60)
        {
          queueValue?.GetFaction()?.DistributeUnits(eligiblePlayers);
          queueValue?.GetFaction()?.DistributeResources(eligiblePlayers);
          queueValue?.GetFaction()?.DistributeExperience(eligiblePlayers);
          queueValue?.GetFaction()?.RemoveGoldMines();
        }
        else
        {
          queueValue?.GetFaction()?.RemoveGoldMines();
          queueValue?.GetFaction()?.Obliterate();
        }
      }
      if (Player != null) Player.GetTeam()!.PrcessingDistributeQueue = false;
      LeftGame?.Invoke(this, this);
    }

    /// <summary>
    /// Removes all gold mines assigned to the faction
    /// </summary>
    public void RemoveGoldMines()
    {
      foreach (var unit in _goldMines) KillUnit(unit);
      _goldMines.Clear();
    }
  }
}