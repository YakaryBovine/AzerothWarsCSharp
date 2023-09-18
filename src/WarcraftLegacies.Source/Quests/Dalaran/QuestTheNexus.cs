using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.QuestSystem;
using static War3Api.Common; 

namespace WarcraftLegacies.Source.Quests.Dalaran
{
  public sealed class QuestTheNexus : QuestData
  {
    private readonly LegendaryHero _jaina;

    public QuestTheNexus(LegendaryHero jaina, Capital lichKing, Capital theNexus, Capital dalaran) : base("The Nexus",
      "The Nexus is a tower of powerful arcane energy, Jaina could absord it to gain it's power",
      @"ReplaceableTextures\CommandButtons\BTNBlueDragonNexus.blp")
    {
      _jaina = jaina;
      AddObjective(new ObjectiveChannelRect(Regions.JainaChannel, "The Nexus", jaina, 60, 270, Title));
      AddObjective(new ObjectiveControlLegend(jaina, true));
      AddObjective(new ObjectiveCapitalDead(lichKing));
      AddObjective(new ObjectiveCapitalDead(dalaran));
      AddObjective(new ObjectiveControlCapital(theNexus, false));
      ResearchId = Constants.UPGRADE_R03Y_QUEST_COMPLETED_THE_NEXUS;
      Global = true;
    }

    /// <inheritdoc/>
    protected override string RewardFlavour =>
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
      completingFaction.ModObjectLimit(FourCC("H04A"), 1); //Nexus Jaina

      completingFaction.ModObjectLimit(FourCC("n0A1"), 6); //Elite
      completingFaction.ModObjectLimit(FourCC("h09C"), Faction.UNLIMITED); //Worker
      completingFaction.ModObjectLimit(FourCC("h099"), Faction.UNLIMITED); //Infantry
      completingFaction.ModObjectLimit(FourCC("n0A4"), Faction.UNLIMITED); //Dragonspawn
      completingFaction.ModObjectLimit(FourCC("u025"), 12); //Elementalist
      completingFaction.ModObjectLimit(FourCC("n09T"), 6); //Judicator
      completingFaction.ModObjectLimit(FourCC("h09A"), Faction.UNLIMITED); //Nexus
      completingFaction.ModObjectLimit(FourCC("h09B"), Faction.UNLIMITED); //Roost

      _jaina.UnitType = Constants.UNIT_H04A_LORD_OF_THE_NEXUS_NEXUS;
      
      completingFaction.Name = "The Nexus";
      completingFaction.Icon = @"ReplaceableTextures\CommandButtons\BTNJaina_Archmage.blp";
      SetPlayerState(completingFaction.Player, PLAYER_STATE_FOOD_CAP_CEILING, 250);
    }
  }
}