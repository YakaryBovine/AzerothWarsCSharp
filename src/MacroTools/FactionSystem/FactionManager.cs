using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
namespace MacroTools.FactionSystem
{
  /// <summary>
  ///   Responsible for the management of all <see cref="Faction" />s and <see cref="Team" />s in the game.
  /// </summary>
  public static class FactionManager
  {
    private static readonly Dictionary<string, Team> TeamsByName = new();
    private static readonly List<Team> AllTeams = new();
    private static readonly Dictionary<string, Faction> FactionsByName = new();
    private static readonly List<Faction> AllFactions = new();

    /// <summary>
    ///   Fired when a <see cref="Faction" /> is registered to the <see cref="FactionManager" />.
    /// </summary>
    public static event EventHandler<Faction>? FactionRegistered;

    /// <summary>
    /// Fired when any <see cref="Faction"/> changes its name.
    /// </summary>
    public static event EventHandler<Faction>? AnyFactionNameChanged; //todo: remove this; shouldn't need static events of this nature

    private static void OnFactionNameChange(object? sender, FactionNameChangeEventArgs args)
    {
      try
      {
        FactionsByName.Remove(args.PreviousName);
        FactionsByName.Add(args.Faction.Name, args.Faction);
        AnyFactionNameChanged?.Invoke(args.Faction, args.Faction);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
      }
    }

    /// <summary>
    /// Returns all registered <see cref="Faction"/>s.
    /// </summary>
    /// <returns></returns>
    public static ReadOnlyCollection<Faction> GetAllFactions() => AllFactions.AsReadOnly();

    /// <summary>
    /// Returns all registered <see cref="Team"/>s.
    /// </summary>
    /// <returns></returns>
    public static ReadOnlyCollection<Team> GetAllTeams() => AllTeams.AsReadOnly();

    /// <summary>
    /// Gets the registered <see cref="Team"/> with the specified name.
    /// </summary>
    /// <param name="teamName">The name of the team.</param>
    /// <param name="team">The team with the specified name.</param>
    /// <returns>Returns true if a team with the specified name exists.</returns>
    public static bool TryGetTeamByName(string teamName, [NotNullWhen(true)] out Team? team) =>
      TeamsByName.TryGetValue(teamName.ToLower(), out team);

    public static bool FactionWithNameExists(string name)
    {
      return FactionsByName.ContainsKey(name.ToLower());
    }

    /// <summary>
    /// Returns the <see cref="Faction"/> with the specified name if one exists.
    /// Returns null otherwise.
    /// </summary>
    public static Faction? GetFactionByName(string name) => 
      FactionsByName.TryGetValue(name.ToLower(), out var faction) ? faction : null;
    
    /// <summary>
    /// Returns true if a <see cref="Faction"/> with the specified type exists.
    /// </summary>
    /// <param name="faction">Outputs the <see cref="Faction"/> with the specified type.</param>
    public static bool TryGetFactionByType<T>([NotNullWhen(true)] out T? faction) where T : Faction
    {
      faction = FactionsByName.Values.FirstOrDefault(x => x.GetType() == typeof(T)) as T;
      return faction != null;
    }

    /// <summary>
    /// Returns true if a <see cref="Faction"/> with the specified type exists.
    /// </summary>
    public static bool FactionOfTypeExists(Type factionType) => AllFactions.Any(x => x.GetType() == factionType);

    /// <summary>
    ///   Registers a <see cref="Faction" /> to the <see cref="FactionManager" />,
    ///   allowing it to be retrieved globally and fire global events.
    /// </summary>
    public static Faction Register(Faction faction)
    {
      if (!FactionsByName.ContainsKey(faction.Name.ToLower()))
      {
        FactionsByName[faction.Name.ToLower()] = faction;
        AllFactions.Add(faction);
        FactionRegistered?.Invoke(faction, faction);
        faction.OnRegistered();
        faction.NameChanged += OnFactionNameChange;

        ExecuteFactionDependentInitializers();
        return faction;
      }

      throw new Exception($"Attempted to register faction that already exists with name {faction}.");
    }

    /// <summary>
    ///   Registers a <see cref="Team" /> to the <see cref="FactionManager" />,
    ///   allowing it to be retrieved globally and fire global events.
    /// </summary>
    public static void Register(Team team)
    {
      if (!TeamsByName.ContainsKey(team.Name.ToLower()))
      {
        TeamsByName[team.Name.ToLower()] = team;
        AllTeams.Add(team);
      }
      else
      {
        throw new Exception(
          $"Attempted to register a {nameof(Team)} named {team.Name}, but there is already a registered {nameof(Team)} with that name.");
      }
    }

    /// <summary>
    /// Executes all <see cref="FactionDependentInitializer"/>s related to the provided <see cref="Faction"/>, which have
    /// satisfied their dependencies and which have not already been executed.
    /// </summary>
    private static void ExecuteFactionDependentInitializers()
    {
      //Try execute initializers that depend on the provided Faction.
      var dependentInitializers = AllFactions
        .SelectMany(x => x.FactionDependentInitializers)
        .Where(x => !x.Executed && x.FactionDependencies.All(FactionOfTypeExists));
      foreach (var initializer in dependentInitializers)
        initializer.Execute();
    }
  }
}