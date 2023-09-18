using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.QuestSystem;
using WarcraftLegacies.Source.Setup;
using WarcraftLegacies.Source.Setup.FactionSetup;
using WarcraftLegacies.Source.Setup.Legends;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Zandalar
{
  /// <summary>
  /// Destroy <see cref="LegendKultiras.LegendBoralus"/> without losing <see cref="LegendNeutral.Dazaralor"/> 
  /// </summary>
  public sealed class QuestConquerKul : QuestData
  {
    private readonly QuestData _completeOnFailQuest;
    private readonly QuestData _failOnFailQuest;
    private readonly Rectangle _onFailSpawnRect;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestConquerKul"/> class
    /// </summary>
    /// <param name="onFailSpawnRect"></param>
    /// <param name="completeOnFailQuest">Quest that gets completed upon failing <see cref="QuestConquerKul"/>.</param>
    /// <param name="failOnFailQuest">Quest that is failed upon failing <see cref="QuestConquerKul"/>. </param>
    /// <param name="legendSetup"></param>
    public QuestConquerKul(Rectangle onFailSpawnRect, QuestData completeOnFailQuest, QuestData failOnFailQuest, AllLegendSetup legendSetup) : base(
      "Conquer Boralus",
      "The Kul'tiran people and their fleet have been a threat to the Zandalari Empire for ages, it is time to put them to rest.",
      @"ReplaceableTextures\CommandButtons\BTNGalleonIcon.blp")
    {
      AddObjective(new ObjectiveControlCapital(legendSetup.Neutral.Dazaralor, true));
      AddObjective(new ObjectiveCapitalDead(legendSetup.Kultiras.LegendBoralus));
      ResearchId = Constants.UPGRADE_R08D_QUEST_COMPLETED_CONQUER_BORALUS;
      _onFailSpawnRect = onFailSpawnRect;
      _completeOnFailQuest = completeOnFailQuest;
      _failOnFailQuest = failOnFailQuest;
      Required = true;
    }

    /// <inheritdoc/>
    protected override string RewardFlavour => "Before setting sails we need to conquer Kul'tiras";

    /// <inheritdoc/>>
    protected override string RewardDescription => "Unlock shipyards";

    /// <inheritdoc/>
    protected override string PenaltyFlavour => "Zandalar has fallen.";

    /// <inheritdoc/>
    protected override string PenaltyDescription =>
      "You lose everything you control and can no longer build shipyards, but you unlock Zul'Farrak";

    /// <inheritdoc/>
    protected override void OnComplete(Faction whichFaction)
    {
      whichFaction.Player?.AddGold(750);
      ZandalarSetup.Zandalar?.Player?.SetTeam(TeamSetup.Horde);
    }

    /// <inheritdoc/>
    protected override void OnFail(Faction completingFaction)
    {
      completingFaction.Obliterate();
      if (completingFaction.Player != null)
      {
        _completeOnFailQuest.Progress = QuestProgress.Complete;
        _failOnFailQuest.Progress = QuestProgress.Failed;

        if (GetLocalPlayer() == completingFaction.Player)
          SetCameraPosition(_onFailSpawnRect.Center.X, _onFailSpawnRect.Center.Y);
        completingFaction.Player.AddGold(1500);
        completingFaction.Player.AddLumber(2000);
      }
      ZandalarSetup.Zandalar?.Player?.SetTeam(TeamSetup.Horde);
    }
  }
}