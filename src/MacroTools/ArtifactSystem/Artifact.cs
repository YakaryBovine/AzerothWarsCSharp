using System;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using WCSharp.Events;
using static War3Api.Common;

namespace MacroTools.ArtifactSystem
{
  /// <summary>
  ///   Represents a unique item with some significance to the game's lore.
  /// </summary>
  public sealed class Artifact
  {
    private unit? _owningUnit;
    private int _titanforgedAbility = FourCC("A0VJ");

    /// <summary>
    ///   Initializes a new instance of the <see cref="Artifact" /> class.
    /// </summary>
    /// <param name="whichItem">The real item that the Artifact is representing.</param>
    public Artifact(item whichItem)
    {
      Item = whichItem;
      LocationType = ArtifactLocationType.Ground;
      SetOwningPlayer(null);
      PlayerUnitEvents.Register(ItemTypeEvent.IsPickedUp, OnPickedUp, GetItemTypeId(whichItem));
      PlayerUnitEvents.Register(ItemTypeEvent.IsDropped, OnDropped, GetItemTypeId(whichItem));
      PlayerUnitEvents.Register(UnitTypeEvent.ChangesOwner, OnUnitChangesOwner);
      PlayerData.FactionChange += OnPlayerFactionChange;
    }

    /// <summary>
    ///   This ability is granted to any creep unit which holds an Artifact at the start of the game.
    ///   It should be an inventory ability so that the creep can hold the Artifact.
    /// </summary>
    public static int ArtifactHolderAbilId { get; } = FourCC("A01Y");

    /// <summary>
    ///   The real <see cref="item" /> that the <see cref="Artifact" /> is representing.
    /// </summary>
    public item Item { get; }

    /// <summary>
    ///   The extra ability the Artifact gains when it's Titanforged.
    /// </summary>
    public int TitanforgedAbility
    {
      set => _titanforgedAbility = value;
    }

    /// <summary>
    ///   Whether or not the Artifact has gained its bonus ability.
    /// </summary>
    public bool Titanforged { get; private set; }

    /// <summary>
    ///   Describes the kind of location that the <see cref="Artifact" /> is in.
    /// </summary>
    public ArtifactLocationType LocationType { private set; get; }

    /// <summary>
    ///   The <see cref="unit" /> carrying this <see cref="Artifact" />, if any.
    /// </summary>
    public unit? OwningUnit
    {
      get => _owningUnit;
      private set
      {
        _owningUnit = value;
        if (OwningPlayer != GetOwningPlayer(value)) 
          SetOwningPlayer(value != null ? GetOwningPlayer(value) : null);
      }
    }

    /// <summary>
    ///   The <see cref="player" /> owning the <see cref="unit" /> carrying this <see cref="Artifact" />.
    ///   Returs null if the Artifact is not being carried.
    /// </summary>
    public player? OwningPlayer { get; private set; }

    /// <summary>
    ///   Fired when the <see cref="player" /> owning the <see cref="unit" /> carrying the <see cref="Artifact" /> changes
    ///   <see cref="Faction" />.
    /// </summary>
    public event EventHandler<Artifact>? FactionChanged;

    /// <summary>
    ///   Fired when the <see cref="Artifact" /> is picked up by a unit.
    /// </summary>
    public event EventHandler<Artifact>? PickedUp;

    /// <summary>
    ///   Fired when the <see cref="Artifact" /> is dropped.
    /// </summary>
    public event EventHandler<Artifact>? Dropped;

    /// <summary>
    ///   Fired when the <see cref="Artifact" /> is permanently destroyed.
    /// </summary>
    public event EventHandler<Artifact>? Disposed;

    /// <summary>
    ///   The owner of this <see cref="Artifact" /> changes.
    /// </summary>
    public event EventHandler<Artifact>? OwnerChanged;

    /// <summary>
    ///   Grant the Artifact a predefined bonus ability.
    /// </summary>
    public void Titanforge()
    {
      if (Titanforged) 
        return;
      Titanforged = true;
      BlzItemAddAbility(Item, _titanforgedAbility);
      BlzSetItemExtendedTooltip(Item,
        $"{BlzGetItemExtendedTooltip(Item)}|n|n|cff800000Titanforged|r|n{BlzGetAbilityExtendedTooltip(_titanforgedAbility, 0)}");
      BlzSetItemDescription(Item, $"{BlzGetItemDescription(Item)}|n|cff800000Titanforged|r");
    }

    /// <summary>
    ///   Pings the <see cref="Artifact" /> on the minimap for the given player.
    /// </summary>
    public void Ping(player whichPlayer)
    {
      if (_owningUnit != null)
        whichPlayer.PingMinimapSimple(GetUnitX(_owningUnit), GetUnitY(_owningUnit), 3);
      else
        whichPlayer.PingMinimapSimple(GetItemX(Item), GetItemY(Item), 3);
    }

    private void SetOwningPlayer(player? value)
    {
      OwningPlayer = value;
      OwnerChanged?.Invoke(this, this);

      LocationType = OwningPlayer != null ? ArtifactLocationType.Unit : ArtifactLocationType.Ground;
    }

    private void OnPickedUp()
    {
      OwningUnit = GetTriggerUnit();
      PickedUp?.Invoke(this, this);
    }

    private void OnDropped()
    {
      //Remove dummy Artifact holding ability if the dropping unit had one
      if (GetUnitAbilityLevel(_owningUnit, ArtifactHolderAbilId) > 0)
        UnitRemoveAbility(_owningUnit, ArtifactHolderAbilId);

      SetOwningPlayer(null);
      OwningUnit = null;
      Dropped?.Invoke(this, this);
    }

    private void OnPlayerFactionChange(object? sender, PlayerFactionChangeEventArgs e)
    {
      if (OwningPlayer?.GetFaction() == e.Player.GetFaction()) FactionChanged?.Invoke(this, this);
    }

    private void OnUnitChangesOwner()
    {
      if (OwningUnit == GetTriggerUnit())
        SetOwningPlayer(GetOwningPlayer(GetTriggerUnit()));
    }

    /// <summary>
    /// Cleans up all managed resources used by the <see cref="Artifact"/>.
    /// </summary>
    internal void Dispose()
    {
      Disposed?.Invoke(this, this);
      RemoveItem(Item);
    }
  }
}