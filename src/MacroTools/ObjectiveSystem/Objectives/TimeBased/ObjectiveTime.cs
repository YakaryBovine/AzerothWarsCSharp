using MacroTools.QuestSystem;
using static War3Api.Common;

namespace MacroTools.ObjectiveSystem.Objectives.TimeBased
{
  public sealed class ObjectiveTime : Objective
  {
    private readonly timer _timer;

    private void OnExpire()
    {
      DestroyTimer(_timer);
      Progress = QuestProgress.Complete;
    }

    public ObjectiveTime(int duration)
    {
      var turn = GameTime.ConvertGameTimeToTurn(duration);
      Description = $"Turn {turn} has started";
      _timer = CreateTimer();
      TimerStart(_timer, duration, false, OnExpire);
    }
  }
}