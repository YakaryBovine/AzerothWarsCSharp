using System;
using System.Collections.Generic;
using System.Linq;

namespace MacroTools.FactionSystem
{
  /// <summary>
  /// A <see cref="Faction"/> initializer that is dependent on the registration of another <see cref="Faction"/> type
  /// to work.
  /// </summary>
  public sealed class FactionDependentInitializer
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="FactionDependentInitializer"/> class.
    /// </summary>
    /// <param name="factionDependencieses">The <see cref="Type"/>s of the <see cref="Faction"/> that this initializer is dependent on.</param>
    /// <param name="initializerAction">The <see cref="Action"/> to run when the depended on <see cref="Faction"/> <see cref="Type"/> exists.</param>
    public FactionDependentInitializer(List<Type> factionDependencieses, Action initializerAction)
    {
      FactionDependencies = factionDependencieses;
      _initializerAction = initializerAction;
    }
    
    /// <summary>
    /// The <see cref="Type"/> of the <see cref="Faction"/> that this initializer is dependent on.
    /// </summary>
    public List<Type> FactionDependencies { get; }

    /// <summary>
    /// The <see cref="Action"/> to run when the depended on <see cref="Faction"/> <see cref="Type"/> exists.
    /// </summary>
    private readonly Action _initializerAction;
    
    /// <summary>
    /// Whether or not the initializer has already been run.
    /// </summary>
    public bool Executed { get; private set; }

    /// <summary>
    /// Executes the initializer.
    /// </summary>
    public void Execute()
    {
      if (Executed)
        throw new Exception($"Tried to execute {nameof(FactionDependentInitializer)} with {nameof(FactionDependencies)} {FactionDependencies.First().Name} but it has already been executed.");
      
      _initializerAction();
      Executed = true;
    }
  }
}