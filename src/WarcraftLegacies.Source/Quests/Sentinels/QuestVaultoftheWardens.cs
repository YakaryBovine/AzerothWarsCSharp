using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.FactionBased;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.QuestSystem;
using static MacroTools.Libraries.GeneralHelpers;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Quests.Sentinels
{
  /// <summary>
  /// Capture the Vault of the Wardens to learn to train Wardens.
  /// </summary>
  public sealed class QuestVaultoftheWardens : QuestData
  {
    private readonly Capital _vaultOfTheWardens;
    private const int WardenId = Constants.UNIT_H045_WARDEN_SENTINELS;

    /// <inheritdoc />
    public QuestVaultoftheWardens(LegendaryHero maiev, Capital vaultOfTheWardens) : base("Vault of the Wardens",
      "In millenia past, the most vile entities of Azeroth were imprisoned in a facility near Zin-Ashari, but it was abandoned when the Broken Isles were shattered. In troubling times such as these, the Wardens could make great use of such a facility.",
      @"ReplaceableTextures\CommandButtons\BTNReincarnationWarden.blp")
    {
      _vaultOfTheWardens = vaultOfTheWardens;
      AddObjective(new ObjectiveChannelRect(Regions.VaultoftheWardens, "Vault of the Wardens", maiev,
        120, 90));
      AddObjective(new ObjectiveSelfExists());
      ResearchId = Constants.UPGRADE_R06H_QUEST_COMPLETED_VAULT_OF_THE_WARDENS_SENTINELS_JAROD;
      
    }

    /// <inheritdoc />
    public override string RewardFlavour =>
      "The ancient Vault of the Wardens has been secured. Maiev and her Wardens take up residence within its ancient halls.";

    /// <inheritdoc />
    protected override string RewardDescription =>
      $"4 free {GetObjectName(Constants.UNIT_H045_WARDEN_SENTINELS)}s appear at the Broken Isles, and you learn to train {GetObjectName(Constants.UNIT_H045_WARDEN_SENTINELS)}s from the {GetObjectName(Constants.UNIT_N04G_VAULT_OF_THE_WARDENS_SENTINELS)} and from {GetObjectName(Constants.UNIT_E00T_WATCHER_S_BASTION_SENTINEL_SIEGE)}s";

    /// <inheritdoc />
    protected override void OnComplete(Faction completingFaction)
    {
      CreateUnits(completingFaction.Player, WardenId, Regions.VaultoftheWardens.Center.X,
        Regions.VaultoftheWardens.Center.Y, 270, 4);
      completingFaction.Player.DisplayUnitTypeAcquired(WardenId,
        $"You can now train Wardens from the {GetObjectName(Constants.UNIT_N04G_VAULT_OF_THE_WARDENS_SENTINELS)} and from {GetObjectName(Constants.UNIT_E00T_WATCHER_S_BASTION_SENTINEL_SIEGE)}s.");
      _vaultOfTheWardens.Unit?.Rescue(completingFaction.Player);
    }

    /// <inheritdoc />
    protected override void OnFail(Faction completingFaction)
    {
      _vaultOfTheWardens.Unit?.Kill();
    }

    /// <inheritdoc />
    protected override void OnAdd(Faction whichFaction)
    {
      _vaultOfTheWardens.Unit?.SetInvulnerable(true);
    }
  }
}