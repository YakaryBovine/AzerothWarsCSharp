using MacroTools;
using MacroTools.ControlPointSystem;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.ObjectiveSystem.Objectives.ControlPointBased;
using MacroTools.ObjectiveSystem.Objectives.FactionBased;
using MacroTools.ObjectiveSystem.Objectives.TimeBased;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using MacroTools.QuestSystem;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Ironforge
{
  public sealed class QuestDunMorogh : QuestData
  {
    public QuestDunMorogh(PreplacedUnitSystem preplacedUnitSystem) : base("Mountain Village",
      "A small troll skirmish is attacking Dun Morogh. Push them back!",
      @"ReplaceableTextures\CommandButtons\BTNIceTrollShadowPriest.blp")
    {
      AddObjective(new ObjectiveUnitIsDead(preplacedUnitSystem.GetUnit(FourCC("nith"), new Point(10673, -7188)))); //Troll High Priest
      AddObjective(new ObjectiveControlPoint(ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N014_DUN_MOROGH)));
      AddObjective(new ObjectiveExpire(660, Title));
      AddObjective(new ObjectiveSelfExists());
      
    }

    /// <inheritdoc/>
    public override string RewardFlavour => "The Trolls have been defeated, Dun Morogh will join your cause.";

    /// <inheritdoc/>
    protected override string RewardDescription => "Control of all units in Dun Morogh";

    private static void GrantDunMorogh(player whichPlayer)
    {
      var tempGroup = CreateGroup();

      //Transfer all Neutral Passive units in DunMorogh
      GroupEnumUnitsInRect(tempGroup, Regions.DunmoroghAmbient2.Rect, null);
      var u = FirstOfGroup(tempGroup);
      while (true)
      {
        if (u == null) break;
        if (GetOwningPlayer(u) == Player(PLAYER_NEUTRAL_PASSIVE)) u.Rescue(whichPlayer);
        GroupRemoveUnit(tempGroup, u);
        u = FirstOfGroup(tempGroup);
      }

      DestroyGroup(tempGroup);
    }

    /// <inheritdoc/>
    protected override void OnFail(Faction completingFaction)
    {
      var rescuer = completingFaction.ScoreStatus == ScoreStatus.Defeated
        ? Player(PLAYER_NEUTRAL_AGGRESSIVE)
        : completingFaction.Player;

      GrantDunMorogh(rescuer);
    }

    /// <inheritdoc/>
    protected override void OnComplete(Faction completingFaction)
    {
      GrantDunMorogh(completingFaction.Player);
    }
  }
}