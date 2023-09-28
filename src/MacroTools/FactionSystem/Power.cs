using System;
using static War3Api.Common;

namespace MacroTools.FactionSystem
{
  /// <summary>
  ///   An ability that can be bestowed on a Faction.
  /// </summary>
  public abstract class Power
  {
    private string _description = "";

    public string IconName { get; init; } = "";
    
    public string IconPath => $@"ReplaceableTextures\CommandButtons\BTN{IconName}.blp";

    public string Name { get; init; } = "";

    public string Description
    {
      get => _description;
      protected set
      {
        _description = value;
        DescriptionChanged?.Invoke(this, this);
      }
    }

    public bool Usable { get; set; }

    public bool OnCooldown { get; set; }
    
    public event EventHandler<Power>? DescriptionChanged;

    /// <summary>
    ///   Fired when the <see cref="Power" /> is added to a <see cref="player" />.
    /// </summary>
    public virtual void OnAdd(player whichPlayer)
    {
    }

    /// <summary>
    ///   Fired when the <see cref="Power" /> is added to a <see cref="player" />.
    /// </summary>
    public virtual void OnRemove(player whichPlayer)
    {
    }

    /// <summary>
    ///   Fired when the <see cref="Power" /> is added to a <see cref="Faction" />.
    /// </summary>
    public virtual void OnAdd(Faction whichFaction)
    {
    }

    /// <summary>
    ///   Fired when the <see cref="Power" /> is added to a <see cref="Faction" />.
    /// </summary>
    public virtual void OnRemove(Faction whichFaction)
    {
    }
    
    public virtual void OnUse(player whichPlayer)
    {
    }
  }
}