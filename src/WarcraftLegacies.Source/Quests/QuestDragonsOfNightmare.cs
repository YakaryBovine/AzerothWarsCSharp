using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.ObjectiveSystem.Objectives.TimeBased;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using MacroTools.QuestSystem;
using MacroTools.Sound;
using WCSharp.Shared;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests
{
  /// <summary>
  /// Spawns 2 dragons in different locations. Activates portals connecting the two locations after the dragons have been killed.
  /// </summary>
  public sealed class QuestDragonsOfNightmare : QuestData
  {
    private readonly unit _nightmareDragonKalimdor;
    private readonly unit _nightmareDragonEk;
    private readonly string _portalOneLocation;
    private readonly string _portalTwoLocation;

    private readonly unit _waygateOne;
    private readonly unit _waygateTwo;
    private readonly Rectangle _wayGateOneDestination;
    private readonly Rectangle _wayGateTwoDestination;
    private readonly timer _timer;
    
    /// <summary>
    /// Initilaizes the quest <see cref="QuestDragonsOfNightmare"/>
    /// </summary>
    /// <param name="nightmareDragonKalimdor">The unit that has to be killed to complete the objective</param>
    /// <param name="nightmareDragonEk">The unit that has to be killed to complete the objective</param>
    /// <param name="portalOneLocation">The name of the first portal's location</param>
    /// <param name="portalTwoLocation">The name of the second portal's location</param>
    /// <param name="waygateOne">the waygate at the first location</param>
    /// <param name="waygateTwo">the waygate at the second location</param>
    /// <param name="wayGateOneDestination"></param>
    /// <param name="wayGateTwoDestination"></param>
    /// <param name="icon">the icon shown in the quest menu</param>
    public QuestDragonsOfNightmare(unit nightmareDragonKalimdor, unit nightmareDragonEk, string portalOneLocation, string portalTwoLocation, unit waygateOne, unit waygateTwo, Rectangle wayGateOneDestination, Rectangle wayGateTwoDestination, string icon) : base($"{nightmareDragonKalimdor.GetProperName()} and {nightmareDragonEk.GetProperName()}",
     "Once protectors of the Emerald Dream, the now corrupted dragons came to Azeroth to spread the corruption. Stop them before the corruption begins to spread.",
      @$"ReplaceableTextures\CommandButtons\{icon}.blp")
    {
      _waygateOne = waygateOne;
      _waygateTwo = waygateTwo;
      _wayGateOneDestination = wayGateOneDestination;
      _wayGateTwoDestination = wayGateTwoDestination;
      _nightmareDragonKalimdor = nightmareDragonKalimdor.Show(false);
      _nightmareDragonEk = nightmareDragonEk.Show(false);
      _nightmareDragonKalimdor = nightmareDragonKalimdor;
      _nightmareDragonEk = nightmareDragonEk;
      _portalOneLocation = portalOneLocation;
      _portalTwoLocation = portalTwoLocation;
      AddObjective(new ObjectiveKillUnit(nightmareDragonKalimdor));
      AddObjective(new ObjectiveKillUnit(nightmareDragonEk));
      AddObjective(new ObjectiveTime(360));
      _timer = CreateTimer().Start(360, false, OnTimeElapsed);
      IsFactionQuest = false;
    }

    private void OnTimeElapsed()
    {
      DestroyTimer(_timer);
      foreach (var player in Util.EnumeratePlayers())
      {
        DisplayTextToPlayer(player, 0, 0, $"\n|cff590ff7 NIGHTMARE DRAGONS SPAWNED \n|r {_nightmareDragonKalimdor.GetProperName()} and {_nightmareDragonEk.GetProperName()} have appeared in {_portalOneLocation} and {_portalTwoLocation}.");
        StartSound(SoundLibrary.Warning);
      }
      _nightmareDragonEk.Show(true);
      _nightmareDragonKalimdor.Show(true);
    }
    
    /// <inheritdoc/>
    protected override string RewardDescription => $"A portal between {_portalOneLocation} and {_portalTwoLocation} opens";

    /// <inheritdoc/>
    public override string RewardFlavour => $"The Dragons of Nightmare {_nightmareDragonKalimdor.GetProperName()} and {_nightmareDragonEk.GetProperName()} have been defeated.";

    /// <inheritdoc/>
    protected override void OnComplete(Faction completingFaction)
    {
      _waygateOne
     .Show(true)
     .SetWaygateDestination(_wayGateOneDestination.Center);
      _waygateTwo
        .Show(true)
        .SetWaygateDestination(_wayGateTwoDestination.Center);
    }
  }
}
