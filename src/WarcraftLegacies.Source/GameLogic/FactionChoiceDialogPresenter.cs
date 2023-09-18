using System;
using System.Collections.Generic;
using System.Linq;
using MacroTools.ControlPointSystem;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.GameLogic
{
  /// <summary>Allows a player to choose between one of two factions at the start of the game.</summary>
  public sealed class FactionChoiceDialogPresenter
  {
    //Ensures choice dialogs are kept in memory until they're done.
    private readonly List<FactionChoiceDialogPresenter> _activeChoices = new ();
    
    private readonly dialog? _pickDialogue = DialogCreate();
    private readonly Dictionary<button, Faction> _factionPicksByButton = new();
    private readonly List<Faction> _factionChoices;
    private readonly List<trigger> _triggers = new();

    /// <summary>Initializes a new instance of the <see cref="FactionChoiceDialogPresenter"/> class.</summary>
    public FactionChoiceDialogPresenter(params Faction[] factionChoices)
    {
      foreach (var faction in factionChoices)
      {
        var factionButton = DialogAddButton(_pickDialogue, faction.Name, 0);
        _factionPicksByButton[factionButton] = faction;
      }
      _factionChoices = factionChoices.ToList();
    }

    /// <summary>Displays the faction choice to a player.</summary>
    public void Run(player whichPlayer)
    {
      _activeChoices.Add(this);
      DialogSetMessage(_pickDialogue, "Pick your Faction");
      
      var timer = CreateTimer();
      TimerStart(timer, 4, false, () =>
      {
        StartFactionPick(whichPlayer);
        DestroyTimer(GetExpiredTimer());
      });
      
      var concludeTimer = CreateTimer();
      TimerStart(concludeTimer, 24, false, () =>
      {
        ExpireFactionPick(whichPlayer);
        DestroyTimer(GetExpiredTimer());
      });
    }

    private void Dispose()
    {
      if (!_activeChoices.Contains(this)) 
        return;
      
      DialogClear(_pickDialogue);
      DialogDestroy(_pickDialogue);
      
      _activeChoices.Remove(this);
      foreach (var trigger in _triggers)
        trigger.Destroy();
    }
    
    private void StartFactionPick(player whichPlayer)
    {
      if (GetLocalPlayer() == whichPlayer)
        DialogDisplay(GetLocalPlayer(), _pickDialogue, true);
      
      foreach (var (button, faction) in _factionPicksByButton)
      {
        var pickTrigger = CreateTrigger();
        TriggerRegisterDialogButtonEvent(pickTrigger, button);
        TriggerAddAction(pickTrigger, () =>
        {
          try
          {
            PickFaction(whichPlayer, faction);
          }
          catch (Exception ex)
          {
            Console.WriteLine(ex);
          }
        });
        _triggers.Add(pickTrigger);
      }
    }
    
    private void ExpireFactionPick(player whichPlayer)
    {
      if (GetLocalPlayer() == whichPlayer)
        DialogDisplay(GetLocalPlayer(), _pickDialogue, false);
      
      if (whichPlayer.GetFaction() == null)
        PickFaction(whichPlayer, _factionChoices.First());
      
      Dispose();
    }
    
    private void PickFaction(player whichPlayer, Faction whichFaction)
    {
      if (GetLocalPlayer() == whichPlayer && whichFaction.StartingCameraPosition != null)
        SetCameraPosition(whichFaction.StartingCameraPosition.X, whichFaction.StartingCameraPosition.Y);
      
      whichPlayer.SetFaction(whichFaction);
      whichPlayer.RescueGroup(whichFaction.StartingUnits);
      
      foreach (var unpickedFaction in _factionChoices.Where(x => x != whichFaction))
        RemoveFaction(unpickedFaction);
    }

    private static void RemoveFaction(Faction whichFaction)
    {
      foreach (var unit in whichFaction.StartingUnits)
      {
        if (ControlPointManager.Instance.UnitIsControlPoint(unit))
        {
          unit.SetInvulnerable(false);
          unit.SetOwner(Player(PLAYER_NEUTRAL_AGGRESSIVE));
        }
        else
          unit.Remove();
      }
      
      whichFaction.RemoveGoldMines();
      whichFaction.Defeat();
    }
  }
}