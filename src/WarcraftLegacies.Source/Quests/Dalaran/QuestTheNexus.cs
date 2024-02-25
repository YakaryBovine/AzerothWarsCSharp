using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.QuestSystem;
using WarcraftLegacies.Source.Setup.Legends;
using static War3Api.Common; 

namespace WarcraftLegacies.Source.Quests.Dalaran
{
  public sealed class QuestTheNexus : QuestData
  {
    private readonly LegendaryHero _jaina;

    public QuestTheNexus(LegendDalaran legendDalaran, Capital lichKing, Capital theNexus) : base("The Nexus",
      "The Nexus is a tower of powerful arcane energy, Jaina could absord it to gain it's power",
      @"ReplaceableTextures\CommandButtons\BTNBlueDragonNexus.blp")
    {
      _jaina = legendDalaran.Jaina;
      AddObjective(new ObjectiveChannelRect(Regions.JainaChannel, "The Nexus", legendDalaran.Jaina, 60, 270, Title));
      AddObjective(new ObjectiveControlLegend(legendDalaran.Jaina, true));
      AddObjective(new ObjectiveCapitalDead(lichKing));
      AddObjective(new ObjectiveCapitalDead(legendDalaran.Dalaran));
      AddObjective(new ObjectiveDontControlLegend(legendDalaran.Aegwynn));
      AddObjective(new ObjectiveControlCapital(theNexus, false));
      ResearchId = Constants.UPGRADE_R03Y_QUEST_COMPLETED_THE_NEXUS;
      Global = true;
    }

    /// <inheritdoc/>
    public override string RewardFlavour =>
      "The Nexus powers have been absorbed by Jaina";

    /// <inheritdoc/>
    protected override string RewardDescription =>
      "You become the Nexus faction. You gain multiple new Dragonkin units.";
    
    /// <inheritdoc/>
    protected override void OnComplete(Faction completingFaction)
    {
      completingFaction.ModObjectLimit(FourCC("h069"), -Faction.UNLIMITED); //Military Quarters
      completingFaction.ModObjectLimit(FourCC("h067"), -Faction.UNLIMITED); //Laboratory
      completingFaction.ModObjectLimit(FourCC("n096"), -Faction.UNLIMITED); //Golem
      completingFaction.ModObjectLimit(FourCC("o02U"), -Faction.UNLIMITED); //Crystal artillery
      completingFaction.ModObjectLimit(FourCC("n0AD"), -Faction.UNLIMITED); //Crystal Golem
      completingFaction.ModObjectLimit(FourCC("h032"), -Faction.UNLIMITED); //Battlemage
      completingFaction.ModObjectLimit(FourCC("n007"), -Faction.UNLIMITED); //Kirintor
      completingFaction.ModObjectLimit(FourCC("h022"), -Faction.UNLIMITED); //Peasant
      completingFaction.ModObjectLimit(FourCC("R06O"), -Faction.UNLIMITED); //Phase Blade
      completingFaction.ModObjectLimit(FourCC("R061"), -Faction.UNLIMITED); //Forked Lightning

      completingFaction.ModObjectLimit(FourCC("U027"), 1); //Kalecgos
      completingFaction.ModObjectLimit(Constants.UNIT_H04A_LORD_OF_THE_NEXUS_NEXUS, 1);
      completingFaction.ModObjectLimit(Constants.UNIT_H09N_MATRIARCH_OF_TIRISFAL_DALARAN, -1);

      completingFaction.ModObjectLimit(Constants.UNIT_N0A1_CRYSTAL_LORD_NEXUS, 6);
      completingFaction.ModObjectLimit(Constants.UNIT_H09C_WHELP_DALARAN, Faction.UNLIMITED);
      completingFaction.ModObjectLimit(Constants.UNIT_H099_ZEALOT_NEXUS, Faction.UNLIMITED);
      completingFaction.ModObjectLimit(Constants.UNIT_N0A4_BLUE_DRAGONSPAWN_NEXUS, Faction.UNLIMITED);
      completingFaction.ModObjectLimit(FourCC("u025"), 12); //Elementalist
      completingFaction.ModObjectLimit(Constants.UNIT_N09T_JUDICATOR_NEXUS, 6);
      completingFaction.ModObjectLimit(Constants.UNIT_H09A_DRACONIC_SPIRE_DALARAN_BARRACKS_ALTERNATE, Faction.UNLIMITED);
      completingFaction.ModObjectLimit(Constants.UNIT_H09B_BLUE_DRAGON_ROOST_DALARAN_SIEGE, Faction.UNLIMITED);

      _jaina.UnitType = Constants.UNIT_H04A_LORD_OF_THE_NEXUS_NEXUS;
      
      completingFaction.Name = "The Nexus";
      completingFaction.Icon = @"ReplaceableTextures\CommandButtons\BTNJaina_Archmage.blp";
      SetPlayerState(completingFaction.Player, PLAYER_STATE_FOOD_CAP_CEILING, 250);
    }
  }
}