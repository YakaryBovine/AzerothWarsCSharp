using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using MacroTools.ControlPointSystem;
using MacroTools.Extensions;
using MacroTools.FactionChoices;
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
  public abstract class Faction
  {
    /// <summary>Signifies unlimited unit production.</summary>
    public const int UNLIMITED = 200;

    /// <summary>The amount of food <see cref="Faction"/>s can have by default.</summary>
    private const int FoodMaximumDefault = 200;

    private static int _highestId;

    private readonly Dictionary<int, int> _abilityAvailabilities = new();
    private readonly List<unit> _goldMines = new();
    private readonly Dictionary<int, int> _objectLevels = new();
    private readonly Dictionary<int, int> _objectLimits = new();
    private readonly List<Power> _powers = new();
    private readonly Dictionary<string, QuestData> _questsByName = new();
    private readonly int _undefeatedResearch;
    
    private int _foodMaximum;
    private string _icon;
    private string _name;
    private player? _player;

    static Faction()
    {
      PlayerUnitEvents.Register(ResearchEvent.IsFinished, () =>
      {
        try
        {
          var faction = GetTriggerPlayer().GetFaction();
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
              faction.SetObjectLimit(otherResearch.ResearchTypeId, -UNLIMITED);
          }
          else
          {
            faction.SetObjectLimit(researchId, -UNLIMITED);
            research.Refund(GetTriggerPlayer());
          }
        }
        catch (Exception ex)
        {
          Logger.LogError(ex.ToString());
        }
      });
    }

    protected Faction(string name, playercolor playerColor, string prefixCol, string icon)
    {
      _name = name;
      PlayerColor = playerColor;
      PrefixCol = prefixCol;
      _icon = icon;
      FoodMaximum = FoodMaximumDefault;
      Id = _highestId + 1;
      _highestId = Id;
    }

    internal List<FactionDependentInitializer> FactionDependentInitializers { get; } = new();

    /// <summary>A unique numerical identifier.</summary>
    public int Id { get; }

    protected internal IReadOnlyList<unit> GoldMines
    {
      get => _goldMines;
      protected init => _goldMines = value as List<unit> ?? throw new InvalidOperationException();
    }

    /// <summary>Displayed to the <see cref="Faction" /> when the game starts.</summary>
    public string? IntroText { get; protected init; }

    /// <summary>
    /// All of the <see cref="Faction"/>'s <see cref="ControlPoint"/> <see cref="ControlPoint.Defender"/>s
    /// will be represented by this unit type.
    /// </summary>
    public int? ControlPointDefenderUnitTypeId { get; protected init; }

    /// <summary>
    /// Check to see if <see cref="Faction"/> has any living essential legends
    /// </summary>
    public bool HasEssentialLegend => GetEssentialLegends().Count > 0;

    /// <summary>How much gold the faction starts with.</summary>
    public int StartingGold { get; protected init; }

    /// <summary>The units this faction should start the game with.</summary>
    public List<unit> StartingUnits { get; protected init; } = new();

    /// <summary>Where any player occupying this faction should have their camera set to on game start.</summary>
    public Point? StartingCameraPosition { get; protected init; }

    /// <summary>Players with this faction will become this color.</summary>
    public playercolor PlayerColor { get; }

    /// <summary>
    /// A list of additional names that this Faction can be referred to by in commands.
    /// </summary>
    public IReadOnlyList<string> Nicknames { get; protected init; } = new List<string>();

    /// <summary>
    /// The <see cref="Team"/> that the <see cref="Faction"/> would traditionally be on.
    /// <para>May or may not be used depending on actual game mode selections.</para>
    /// </summary>
    public Team? TraditionalTeam { get; protected init; }

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
        if (Player != null) 
          SetPlayerState(Player, PLAYER_STATE_FOOD_CAP_CEILING, value);
      }
    }

    /// <summary>Music that will play for the Faction at the start of the game.</summary>
    public string CinematicMusic { get; protected init; } = "";

    /// <summary>Whether or not the <see cref="Faction"/> has been defeated.</summary>
    public ScoreStatus ScoreStatus { get; private set; } = ScoreStatus.Undefeated;

    /// <summary>
    /// Indicates how difficult it is to learn the basic mechanics of this <see cref="Faction"/>.
    /// <para>This isn't about how difficult the Faction is to play optimally, but rather how difficult it is to
    /// play at a very basic level. For instance, a Faction with a very complex starting quest would be very hard
    /// even if it doesn't have to perform a lot of micro in fights.</para>
    /// </summary>
    public FactionLearningDifficulty LearningDifficulty { get; protected init; }

    public string ColoredName => $"{PrefixCol}{_name}|r";

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
          UnapplyFactionFromPlayer(Player);

        _player = value;
        if (value == null) 
          return;

        if (value.GetFaction() != this)
          value.SetFaction(this);

        ApplyFactionToPlayer(value);
      }
    }

    public QuestData? StartingQuest { get; protected set; }

    /// <summary>
    ///   This research is enabled for every player while this <see cref="Faction" /> is not defeated.
    /// </summary>
    public int UndefeatedResearch
    {
      get => _undefeatedResearch;
      init
      {
        if (_undefeatedResearch != 0) return;
        _undefeatedResearch = value;
        foreach (var player in WCSharp.Shared.Util.EnumeratePlayers())
          SetPlayerTechResearched(player, _undefeatedResearch, 1);
      }
    }

    /// <summary>
    /// Fired when the faction gains a GoldMine that was previously registered to another Faction.
    /// </summary>
    private readonly SharedGoldMineManager _sharedGoldMineManager;

    /// <summary>Fired when the <see cref="Faction" /> gains a <see cref="Power" />.</summary>
    public event EventHandler<FactionPowerEventArgs>? PowerAdded;

    /// <summary>Fired when the <see cref="Faction" /> loses a <see cref="Power" />.</summary>
    public event EventHandler<FactionPowerEventArgs>? PowerRemoved;

    /// <summary>Invoked when <see cref="ScoreStatus"/> changes.</summary>
    public event EventHandler<Faction>? ScoreStatusChanged;

    /// <summary>Invoked when one of the <see cref="Faction"/>'s <see cref="QuestData"/>s changes progress.</summary>
    public event EventHandler<FactionQuestProgressChangedEventArgs>? QuestProgressChanged;

    /// <summary>Fires when the <see cref="Faction" /> changes its name.</summary>
    public event EventHandler<FactionNameChangeEventArgs>? NameChanged;

    /// <summary>Fired when the <see cref="Faction"/>'s has changed.</summary>
    public event EventHandler<Faction>? IconChanged;

    /// <summary>Fired after the <see cref="Faction"/>'s status has changed.</summary>
    public event EventHandler<Faction>? StatusChanged;

    /// <summary>
    /// Invoked after the <see cref="Faction"/> is registered to a <see cref="FactionManager"/>.
    /// <para>Override this for faction-specific initialization.</para>
    /// </summary>
    public virtual void OnRegistered()
    {
    }

    /// <summary>
    /// Invoked when the <see cref="Faction"/> has not been picked by any player by the time the game starts.
    /// <para>Override this to cleanup anything that would have been used by the <see cref="Faction"/> if it had
    /// been picked.</para>
    /// </summary>
    public virtual void OnNotPicked()
    {
      if (GoldMines != null)
      {
        foreach (var unit in GoldMines)
        {
          if (_sharedGoldMineManager.IsSharedGoldMine(unit))
          {
            _sharedGoldMineManager.TransferGoldMineOwnership(unit, this);
          }
          else
          {
            RemoveUnit(unit);
          }
        }
      }
    }

    public Faction(SharedGoldMineManager sharedGoldMineManager)
    {
      _sharedGoldMineManager = sharedGoldMineManager;

    }
    /// <summary>
    /// Defeats the player, making them an observer, and distributing their units and resources to allies if possible.
    /// </summary>
    public void Defeat()
    {
      foreach (var player in WCSharp.Shared.Util.EnumeratePlayers())
        SetPlayerTechResearched(player, _undefeatedResearch, 0);

      if (Player != null)
      {
        FogModifierStart(CreateFogModifierRect(Player, FOG_OF_WAR_VISIBLE,
          Rectangle.WorldBounds.Rect, false, false));
        RemovePlayer(Player, PLAYER_GAME_RESULT_DEFEAT);
        SetPlayerState(Player, PLAYER_STATE_OBSERVER, 1);
        PlayerDistributor.DistributePlayer(Player);
        RemoveGoldMines();
      }

      ScoreStatus = ScoreStatus.Defeated;
      StatusChanged?.Invoke(this, this);
      ScoreStatusChanged?.Invoke(this, this);
    }

    /// <summary>
    ///   Returns the maximum number of times the Faction can train a unit, build a building, or research a research.
    /// </summary>
    /// <param name="whichObject">The object ID of a unit, building, or research.</param>
    public int GetObjectLimit(int whichObject) => _objectLimits.TryGetValue(whichObject, out var limit) ? limit : 0;

    /// <summary>Adds a <see cref="Power" /> to this <see cref="Faction" />.</summary>
    public void AddPower(Power power)
    {
      _powers.Add(power);
      power.OnAdd(this);
      if (Player != null) 
        power.OnAdd(Player);
      
      PowerAdded?.Invoke(this, new FactionPowerEventArgs(this, power));
    }

    /// <summary>Removes a <see cref="Power" /> from this <see cref="Faction" />.</summary>
    public void RemovePower(Power power)
    {
      _powers.Remove(power);
      power.OnRemove(this);
      if (Player != null) 
        power.OnRemove(Player);
      
      PowerRemoved?.Invoke(this, new FactionPowerEventArgs(this, power));
    }

    /// <summary>
    ///   Removes a <see cref="Power" /> from this <see cref="Faction" />.
    /// </summary>
    public bool TryGetPowerByName(string name, [MaybeNullWhen(false)] out Power power)
    {
      power = _powers.FirstOrDefault(x => x.Name == name);
      return power != null;
    }

    /// <summary>Gets the first <see cref="Power" /> this <see cref="Faction" /> has with the provided type.</summary>
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
      var newTeamName = $"{Name} Pact";

      if (FactionManager.TryGetTeamByName(newTeamName, out var existingTeam))
      {
        Player.SetTeam(existingTeam);
        return;
      }

      var newTeam = new Team(newTeamName);
      FactionManager.Register(newTeam);
      Player.SetTeam(newTeam);
    }

    public QuestData AddQuest(QuestData questData)
    {
      var loweredQuestTitle = questData.Title.ToLower();
      if (_questsByName.ContainsKey(loweredQuestTitle))
        throw new InvalidOperationException($"Tried to add a quest named {loweredQuestTitle} to {Name} but they already have one with that name.");

      questData.Add(this);
      _questsByName.Add(questData.Title.ToLower(), questData);
      if (GetLocalPlayer() == Player)
        questData.ShowLocal();
      questData.ShowSync();
      questData.ProgressChanged += OnQuestProgressChanged;
      return questData;
    }

    public int GetObjectLevel(int obj) => _objectLevels.ContainsKey(obj) ? _objectLevels[obj] : 0;

    /// <summary>Sets the current level of a particular research for the <see cref="Faction"/>.</summary>
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
    public void ModObjectLimit(int objectId, int limit)
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
    /// Sets the limit of the given object that the <see cref="Faction"/> can train, build, or research.
    /// </summary>
    /// <param name="objectId">The object ID to modify the limit of.</param>
    /// <param name="limit">The amount to set the limit to.</param>
    public void SetObjectLimit(int objectId, int limit)
    {
      _objectLimits[objectId] = limit;
      Player?.SetObjectLimit(objectId, limit);

      if (_objectLimits[objectId] == 0)
        _objectLimits.Remove(objectId);
    }

    /// <summary>
    /// Copies object levels from another <see cref="Faction"/> to this one.
    /// <para>Only copies levels that this Faction has the object limit to handle.</para>
    /// </summary>
    public void CopyObjectLevelsFrom(Faction otherFaction)
    {
      var objectLevels = otherFaction._objectLevels;
      foreach (var (objectId, level) in objectLevels)
      {
        var objectLimit = GetObjectLimit(objectId);
        if (objectLimit > 0) 
          SetObjectLevel(objectId, Math.Min(objectLimit, level));
      }
    }

    /// <summary>Returns all <see cref="Power" />s this <see cref="Faction" /> has.</summary>
    public IEnumerable<Power> GetAllPowers()
    {
      foreach (var power in _powers) 
        yield return power;
    }

    /// <summary>
    ///   Attempts to retrieve a <see cref="QuestData" /> belonging to this <see cref="Faction" /> with the given title.
    /// </summary>
    public QuestData? GetQuestByTitle(string parameter) =>
      _questsByName.TryGetValue(parameter.ToLower(), out var value) ? value : null;

    /// <summary>
    /// Returns the first <see cref="QuestData"/> the <see cref="Faction"/> has with the given <see cref="Type"/>.
    /// </summary>
    public T GetQuestByType<T>() where T : QuestData
    {
      return _questsByName.Values.FirstOrDefault(x => x.GetType() == typeof(T)) as T ??
             throw new Exception($"{Name} does not have a {nameof(QuestData)} of type {typeof(T)}");
    }

    /// <summary>Returns all <see cref="QuestData"/>s the <see cref="Faction"/> can complete.</summary>
    public List<QuestData> GetAllQuests() => _questsByName.Values.ToList();

    /// <summary>
    /// Registers an initializer function that will only fire once a <see cref="Faction"/>s of the specified type has
    /// been registered.
    /// </summary>
    /// <param name="initializer">The initializer function itself, which receives the registered instance of the
    /// <see cref="Faction"/> type it depends on.</param>
    /// <typeparam name="TFaction">The type of <see cref="Faction"/> to depend on.</typeparam>
    protected void RegisterFactionDependentInitializer<TFaction>(Action<TFaction> initializer) where TFaction : Faction
    {
      var factionTypes = new List<Type> { typeof(TFaction) };
      var factionDependentInitializer = new FactionDependentInitializer(factionTypes, () =>
      {
        if (FactionManager.TryGetFactionByType<TFaction>(out var factionDependency))
          initializer(factionDependency);
      });
      FactionDependentInitializers.Add(factionDependentInitializer);
    }

    /// <summary>
    /// Registers an initializer function that will only fire once all <see cref="Faction"/>s of the specified types have
    /// been registered.
    /// </summary>
    /// <param name="initializer">The initializer function itself, which receives the registered instance of the
    /// <see cref="Faction"/> type it depends on.</param>
    /// <typeparam name="TFactionA">One of the type of <see cref="Faction"/> to depend on.</typeparam>
    /// <typeparam name="TFactionB">One of the type of <see cref="Faction"/> to depend on.</typeparam>
    protected void RegisterFactionDependentInitializer<TFactionA, TFactionB>(Action<TFactionA, TFactionB> initializer) where TFactionA : Faction where TFactionB : Faction
    {
      var factionTypes = new List<Type> { typeof(TFactionA), typeof(TFactionB) };
      var factionDependentInitializer = new FactionDependentInitializer(factionTypes, () =>
      {
        if (FactionManager.TryGetFactionByType<TFactionA>(out var factionDependencyA) &&
            FactionManager.TryGetFactionByType<TFactionB>(out var factionDependencyB))
          initializer(factionDependencyA, factionDependencyB);
      });
      FactionDependentInitializers.Add(factionDependentInitializer);
    }

    /// <summary>
    /// Registers an initializer function that will only fire once all <see cref="Faction"/>s of the specified types have
    /// been registered.
    /// </summary>
    /// <param name="initializer">The initializer function itself, which receives the registered instance of the
    /// <see cref="Faction"/> type it depends on.</param>
    /// <typeparam name="TFactionA">One of the type of <see cref="Faction"/> to depend on.</typeparam>
    /// <typeparam name="TFactionB">One of the type of <see cref="Faction"/> to depend on.</typeparam>
    /// <typeparam name="TFactionC">One of the type of <see cref="Faction"/> to depend on.</typeparam>
    protected void RegisterFactionDependentInitializer<TFactionA, TFactionB, TFactionC>(Action<TFactionA, TFactionB, TFactionC> initializer) 
      where TFactionA : Faction 
      where TFactionB : Faction
      where TFactionC : Faction
    {
      var factionTypes = new List<Type> { typeof(TFactionA), typeof(TFactionB) };
      var factionDependentInitializer = new FactionDependentInitializer(factionTypes, () =>
      {
        if (FactionManager.TryGetFactionByType<TFactionA>(out var factionDependencyA) &&
            FactionManager.TryGetFactionByType<TFactionB>(out var factionDependencyB) &&
            FactionManager.TryGetFactionByType<TFactionC>(out var factionDependencyC))
          initializer(factionDependencyA, factionDependencyB, factionDependencyC);
      });
      FactionDependentInitializers.Add(factionDependentInitializer);
    }

    /// <summary>
    /// Registers an initializer function that will only fire once all <see cref="Faction"/>s of the specified types have
    /// been registered.
    /// </summary>
    /// <param name="initializer">The initializer function itself, which receives the registered instance of the
    /// <see cref="Faction"/> type it depends on.</param>
    /// <typeparam name="TFactionA">One of the type of <see cref="Faction"/> to depend on.</typeparam>
    /// <typeparam name="TFactionB">One of the type of <see cref="Faction"/> to depend on.</typeparam>
    /// <typeparam name="TFactionC">One of the type of <see cref="Faction"/> to depend on.</typeparam>
    /// <typeparam name="TFactionD">One of the type of <see cref="Faction"/> to depend on.</typeparam>
    protected void RegisterFactionDependentInitializer<TFactionA, TFactionB, TFactionC, TFactionD>(
      Action<TFactionA, TFactionB, TFactionC, TFactionD> initializer) 
      where TFactionA : Faction 
      where TFactionB : Faction
      where TFactionC : Faction
      where TFactionD : Faction
    {
      var factionTypes = new List<Type> { typeof(TFactionA), typeof(TFactionB) };
      var factionDependentInitializer = new FactionDependentInitializer(factionTypes, () =>
      {
        if (FactionManager.TryGetFactionByType<TFactionA>(out var factionDependencyA) &&
            FactionManager.TryGetFactionByType<TFactionB>(out var factionDependencyB) &&
            FactionManager.TryGetFactionByType<TFactionC>(out var factionDependencyC) &&
            FactionManager.TryGetFactionByType<TFactionD>(out var factionDependencyD)) 
          initializer(factionDependencyA, factionDependencyB, factionDependencyC, factionDependencyD);
      });
      FactionDependentInitializers.Add(factionDependentInitializer);
    }

    /// <summary>Gets a list of legends that are flagged as essential and alive that the faction currently has.</summary>
    private List<Legend> GetEssentialLegends()
    {
      var essentialLegends = new List<Legend>();
      
      foreach (var legend in LegendaryHeroManager.GetAll())
        if (legend.Essential && legend.OwningPlayer == Player && legend.Unit?.IsAlive() == true)
          essentialLegends.Add(legend);
      
      foreach (var capital in CapitalManager.GetAll())
        if (capital.Essential && capital.OwningPlayer == Player && capital.Unit?.IsAlive() == true)
          essentialLegends.Add(capital);

      return essentialLegends;
    }

    /// <summary>Removes all gold mines assigned to the faction</summary>
    private void RemoveGoldMines()
    {
      foreach (var unit in GoldMines) 
        RemoveUnit(unit);
      
      _goldMines.Clear();
    }

    /// <summary>
    /// Modifies the player to have the <see cref="Faction"/>'s attributes.
    /// </summary>
    private void ApplyFactionToPlayer(player whichPlayer)
    {
      whichPlayer.GetTeam()?.AllyPlayer(whichPlayer);
      ApplyObjects();
      ApplyPowers();
      ShowAllQuests();
      SetPlayerState(Player, PLAYER_STATE_FOOD_CAP_CEILING, _foodMaximum);
    }

    /// <summary>
    /// Modifies the player to no longer have the <see cref="Faction"/>'s attributes.
    /// </summary>
    private void UnapplyFactionFromPlayer(player whichPlayer)
    {
      whichPlayer.GetTeam()?.UnallyPlayer(whichPlayer);
      HideAllQuests();
      UnapplyObjects();
      UnapplyPowers();
    }

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

      foreach (var (key, _) in _abilityAvailabilities)
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
        if (GetLocalPlayer() == Player) 
          quest.ShowLocal();

        quest.ShowSync();
      }
    }

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
        QuestProgressChanged?.Invoke(this, new FactionQuestProgressChangedEventArgs(this, args.Quest, args.FormerProgress));
      }
      catch (Exception ex)
      {
        Logger.LogError($"{Name} failed to execute {nameof(OnQuestProgressChanged)}: {ex.Message}");
      }
    }
  }
}