using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.QuestSystem;
using static War3Api.Common;

namespace MacroTools.ObjectiveSystem.Objectives.LegendBased
{
  public sealed class ObjectiveControlLegend : Objective
  {
    private readonly bool _canFail;
    private readonly Legend _target;

    public ObjectiveControlLegend(LegendaryHero target, bool canFail)
    {
      _target = target;
      Description = $"You control {target.Name}";
      _canFail = canFail;
      if (target.Unit != null) TargetWidget = target.Unit;

      DisplaysPosition = GetOwningPlayer(target.Unit) == Player(PLAYER_NEUTRAL_AGGRESSIVE);
      target.ChangedOwner += OnTargetChangeOwner;
      target.PermanentlyDied += OnTargetDeath;
      target.OwningPlayer.GetPlayerData().PlayerJoinedTeam += OnFactionTeamJoin;
      Position = new(GetUnitX(_target.Unit), GetUnitY(_target.Unit));
    }

    internal override void OnAdd(Faction whichFaction)
    {
      if (_target.Unit != null && IsPlayerOnSameTeamAsAnyEligibleFaction(_target.Unit.OwningPlayer()))
        Progress = QuestProgress.Complete;
    }

    private void OnTargetChangeOwner(object? sender, LegendChangeOwnerEventArgs legendChangeOwnerEventArgs)
    {
      if (_target.Unit != null && IsPlayerOnSameTeamAsAnyEligibleFaction(_target.Unit.OwningPlayer()))
        Progress = QuestProgress.Complete;
      else
        Progress = _canFail ? QuestProgress.Failed : QuestProgress.Incomplete;
    }
    
    private void OnFactionTeamJoin(object? sender, PlayerChangeTeamEventArgs playerChangeTeamEventArgs)
    {
      var isOnSameTeam = IsPlayerOnSameTeamAsAnyEligibleFaction(_target.OwningPlayer);
      if (_target.Unit != null && isOnSameTeam)
        Progress = QuestProgress.Complete;
      else
        Progress = _canFail ? QuestProgress.Failed : QuestProgress.Incomplete;
    }

    private void OnTargetDeath(object? sender, Legend legend)
    {
      if (_canFail) Progress = QuestProgress.Failed;
    }
  }
}