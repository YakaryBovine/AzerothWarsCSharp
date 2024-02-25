using MacroTools.ChannelSystem;
using static War3Api.Common;

namespace MacroTools.Channels
{
  public sealed class SummonLegionChannel : Channel
  {
    private readonly int _spellImmunityId;
    private readonly timer _timer;
    private readonly timerdialog _timerDialog;

    public SummonLegionChannel(unit caster, int spellId, int spellImmunityId) : base(caster, spellId)
    {
      _spellImmunityId = spellImmunityId;
      _timer = CreateTimer();
      _timerDialog = CreateTimerDialog(_timer);
    }

    public override void OnCreate()
    {
      UnitAddAbility(Caster, _spellImmunityId);
      TimerStart(_timer, Duration, false, null);
      TimerDialogDisplay(_timerDialog, true);
      TimerDialogSetTitle(_timerDialog, "Legion Summon");
      PingMinimap(GetUnitX(Caster), GetUnitY(Caster), 8);
      foreach (var player in WCSharp.Shared.Util.EnumeratePlayers())
        DisplayTextToPlayer(player, 0, 0, "The Burning Legion is being summoned!");
    }

    /// <inheritdoc />
    protected override void OnDispose()
    {
      UnitRemoveAbility(Caster, _spellImmunityId);
      DestroyTimer(_timer);
      DestroyTimerDialog(_timerDialog);
    }
  }
}