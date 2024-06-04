using System.Collections.Generic;
using MacroTools;
using MacroTools.DialogueSystem;
using MacroTools.FactionSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using WarcraftLegacies.Shared.Config.Factions;
using WarcraftLegacies.Source.Quests.KulTiras;
using WarcraftLegacies.Source.Setup;
using WCSharp.Shared.Data;

namespace WarcraftLegacies.Source.Factions
{
  public sealed class Kultiras : Faction
  {
    private readonly AllLegendSetup _allLegendSetup;
    private readonly unit _proudmooreCapitalShip;

    /// <inheritdoc />
    
    public Kultiras(PreplacedUnitSystem preplacedUnitSystem, AllLegendSetup allLegendSetup) : base("Kul'tiras",
      PLAYER_COLOR_EMERALD, "|cff00781e", @"ReplaceableTextures\CommandButtons\BTNProudmoore.blp")
    {
      TraditionalTeam = TeamSetup.SouthAlliance;
      _allLegendSetup = allLegendSetup;
      _proudmooreCapitalShip = preplacedUnitSystem.GetUnit(UNIT_H05V_PROUDMOORE_FLAGSHIP_KUL_TIRAS);
      StartingGold = 200;
      ControlPointDefenderUnitTypeId = UNIT_H09W_CONTROL_POINT_DEFENDER_KUL_TIRAS;
      IntroText = @"You are playing as the maritime |cff008000Kingdom of Kul'tiras|r.

You begin on Balor island, separated from your main forces in Kul Tiras. Unite your forces by eliminating your enemies in Tiragarde, Drustvar and Stormsong Valley.

Stormwind is preparing for an invasion through the Dark Portal in the South. Muster the Admiralty and help them, or you may lose your strongest ally.";
      GoldMines = new List<unit>
      {
        preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(4585, -13038))
      };
      Nicknames = new List<string>
      {
        "kt",
        "kul",
        "kultiras"
      };

      RegisterFactionDependentInitializer<Frostwolf>(RegisterDialogue);
    }

    /// <inheritdoc />
    public override void OnRegistered()
    {
      RegisterObjectLimits();
      RegisterQuests();
      SharedFactionConfigSetup.AddSharedFactionConfig(this);
    }

    private void RegisterObjectLimits()
    {
      foreach (var (objectTypeId, objectLimit) in KultirasObjectLimitData.GetAllObjectLimits())
        ModObjectLimit(FourCC(objectTypeId), objectLimit);
    }

    private void RegisterQuests()
    {
      var questBoralus = new QuestBoralus(Regions.Kultiras);
      StartingQuest = AddQuest(questBoralus);
      AddQuest(new QuestUnlockShip(Regions.ShipAmbient, _proudmooreCapitalShip, _allLegendSetup.Kultiras.LegendAdmiral,
        questBoralus));
      AddQuest(new QuestOldHatreds(_allLegendSetup.Kultiras.LegendAdmiral));
      AddQuest(new QuestWestfallOutpost(Regions.StranglethornBaseBuild));
      AddQuest(new QuestHighBank(Regions.HighbankUnlock, _allLegendSetup.Kultiras.LegendKatherine));
      AddQuest(new QuestBeyondPortal(_allLegendSetup.FelHorde.HellfireCitadel,
        _allLegendSetup.FelHorde.KilsorrowFortress));
    }

    private void RegisterDialogue(Frostwolf frostwolf)
    {
      TriggeredDialogueManager.Add(
        new TriggeredDialogue(new DialogueSequence(new Dialogue(
              @"Sound\Dialogue\OrcExpCamp\OrcQuest20x\D20Proudmoore05",
              "I must admit, you orcs are more tenacious than I remembered. I thought you savages would have turned on each other by now.",
              "Daelin Proudmoore"),
            new Dialogue(
              @"Sound\Dialogue\OrcExpCamp\OrcQuest20x\D20Thrall06",
              "This is not the Horde you remember, old man. We have no interest in conquest or murder. We have paid for our sins of our forebears in blood.",
              "Thrall"),
            new Dialogue(
              @"Sound\Dialogue\OrcExpCamp\OrcQuest20x\D20Proudmoore07",
              "Can your blood atone for genocide, orc? Your Horde killed countless innocents with its rampage across Stormwind and Lordaeron. Do you really think you can just sweep all that away and cast aside your guilt so easily? No, your kind will never change, and I will never stop fighting you.",
              "Daelin Proudmoore"))
          , new Faction[]
          {
            this,
            frostwolf
          }, new[]
          {
            new ObjectiveLegendMeetsLegend(_allLegendSetup.Kultiras.LegendAdmiral, _allLegendSetup.Frostwolf.Thrall)
          }));
    }
  }
}