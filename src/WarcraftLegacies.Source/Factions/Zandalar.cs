using System.Collections.Generic;
using MacroTools;
using MacroTools.Extensions;
using MacroTools.FactionChoices;
using MacroTools.FactionSystem;
using WarcraftLegacies.Source.Quests.Zandalar;
using WarcraftLegacies.Source.Setup;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Factions
{
  public sealed class Zandalar : Faction
  {
    private readonly PreplacedUnitSystem _preplacedUnitSystem;
    private readonly AllLegendSetup _allLegendSetup;
    private readonly ArtifactSetup _artifactSetup;

    /// <inheritdoc />
    public Zandalar(PreplacedUnitSystem preplacedUnitSystem, AllLegendSetup allLegendSetup, ArtifactSetup artifactSetup)
      : base("Zandalar", PLAYER_COLOR_PEACH, "|cffff8c6c",
        @"ReplaceableTextures\CommandButtons\BTNHeadHunterBerserker.blp")
    {
      _preplacedUnitSystem = preplacedUnitSystem;
      _allLegendSetup = allLegendSetup;
      _artifactSetup = artifactSetup;
      StartingGold = 200;
      StartingLumber = 700;
      ControlPointDefenderUnitTypeId = Constants.UNIT_H0C1_CONTROL_POINT_DEFENDER_ZANDALAR;
      StartingCameraPosition = Regions.TrollStartPos.Center;
      StartingUnits = Regions.TrollStartPos.PrepareUnitsForRescue(RescuePreparationMode.Invulnerable);
      LearningDifficulty = FactionLearningDifficulty.Basic;
      IntroText = @"You are playing as the mighty |cffe1946cZandalari Empire|r.

You start off at the southern coast of Tanaris, seperated from your allies. Raise an army and deal with the rogue Trolls in Zul'Farrak.

The Night Elves are mounting an assault on you and your allies from the North.

Join up with your allies and brace for a tough fight and counter-attack. ";

      GoldMines = new List<unit>
      {
        _preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(-8900, -17000)), //Starting
        _preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(-3500, -15000))  //Zandalar
      };
    }
        
    /// <inheritdoc />
    public override void OnRegistered()
    {
      RegisterObjectLimits();
      RegisterQuests();
      SharedFactionConfigSetup.AddSharedFactionConfig(this);
    }
    
    /// <inheritdoc />
    public override void OnNotPicked()
    {
      Regions.ZandalarUnlock.CleanupNeutralPassiveUnits();
      base.OnNotPicked();
    }

    private void RegisterObjectLimits()
    {
      ModObjectLimit(FourCC("o03R"), UNLIMITED); //Great Hall
      ModObjectLimit(FourCC("o03Y"), UNLIMITED); //Stronghold
      ModObjectLimit(FourCC("o03Z"), UNLIMITED); //Fortress
      ModObjectLimit(FourCC("o040"), UNLIMITED); //Altar of Storms
      ModObjectLimit(FourCC("o041"), UNLIMITED); //Barracks
      ModObjectLimit(FourCC("o042"), UNLIMITED); //War Mill
      ModObjectLimit(FourCC("o044"), UNLIMITED); //Tauren Totem
      ModObjectLimit(FourCC("o043"), UNLIMITED); //Spirit Lodge
      ModObjectLimit(FourCC("o045"), UNLIMITED); //Orc Burrow
      ModObjectLimit(FourCC("o046"), UNLIMITED); //Watch Tower
      ModObjectLimit(FourCC("o048"), UNLIMITED); //Improved Watch Tower
      ModObjectLimit(FourCC("o047"), UNLIMITED); //Voodoo Lounge
      ModObjectLimit(FourCC("o049"), UNLIMITED); //Shipyard
      ModObjectLimit(FourCC("o04X"), UNLIMITED); //Loa Shrine

      ModObjectLimit(FourCC("o04A"), UNLIMITED); //Peon
      ModObjectLimit(FourCC("h021"), UNLIMITED); //Grunt
      ModObjectLimit(FourCC("o04D"), UNLIMITED); //Troll Headhunter
      ModObjectLimit(FourCC("n09E"), 2); //Storm Wyrm
      ModObjectLimit(FourCC("e00Z"), 8); //Direhorn
      ModObjectLimit(FourCC("o04F"), UNLIMITED); //Troll Witch Doctor
      ModObjectLimit(FourCC("o04G"), UNLIMITED); //Haruspex
      ModObjectLimit(FourCC("o04E"), 6); //Boneseer
      ModObjectLimit(FourCC("h05D"), UNLIMITED); //Raptor Rider
      ModObjectLimit(FourCC("o021"), 12); //Ravager
      ModObjectLimit(FourCC("nftk"), 12); //Warlord
      ModObjectLimit(FourCC("o02K"), 6); //Bear Rider
      ModObjectLimit(FourCC("n0DN"), 6); //Medium
      ModObjectLimit(FourCC("e01Z"), 3); //Throne of War

      //Ship
      ModObjectLimit(FourCC("obot"), UNLIMITED); //Transport Ship
      ModObjectLimit(FourCC("h0AS"), UNLIMITED); //Scout
      ModObjectLimit(FourCC("h0AP"), UNLIMITED); //Frigate
      ModObjectLimit(FourCC("h0B2"), UNLIMITED); //Fireship
      ModObjectLimit(FourCC("h0AY"), UNLIMITED); //Galley
      ModObjectLimit(FourCC("h0B5"), UNLIMITED); //Boarding
      ModObjectLimit(FourCC("h0BC"), UNLIMITED); //Juggernaut
      ModObjectLimit(FourCC("h0AO"), 6); //Bombard

      ModObjectLimit(FourCC("O026"), 1); //Rasthakan
      ModObjectLimit(FourCC("O01J"), 1); //Zul
      ModObjectLimit(FourCC("U023"), 1); //Hakkar
      ModObjectLimit(FourCC("H06Q"), 1); //Gazrilla

      ModObjectLimit(FourCC("Rers"), UNLIMITED); //Resistant Skin
      ModObjectLimit(FourCC("R00H"), UNLIMITED); //Animal Companion
      ModObjectLimit(FourCC("R070"), UNLIMITED); //Haruspex Training
      ModObjectLimit(FourCC("R071"), UNLIMITED); //Hex Training
    }

    private void RegisterQuests()
    {
      var questZulFarrak = new QuestZulfarrak(Regions.Zulfarrak, _allLegendSetup.Neutral.Zulfarrak, _allLegendSetup.Troll.Zul);
      StartingQuest = questZulFarrak;
      AddQuest(questZulFarrak);
      AddQuest(new QuestZandalar(Regions.ZandalarUnlock, _preplacedUnitSystem));
      AddQuest(new QuestGundrak(_allLegendSetup));
      AddQuest(new QuestJinthaAlor(_allLegendSetup));
      AddQuest(new QuestZulgurub(_allLegendSetup));
      AddQuest(new QuestHakkar(_artifactSetup.ZinRokh));
      AddQuest(new QuestZandalarOutpost());
    }
  }
}