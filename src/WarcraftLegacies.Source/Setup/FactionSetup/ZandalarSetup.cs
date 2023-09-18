using MacroTools;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Setup.FactionSetup
{
  public static class ZandalarSetup
  {
    public static Faction Zandalar { get; private set; }

    public static void Setup(PreplacedUnitSystem preplacedUnitSystem)
    {
      Zandalar = new Faction("Zandalar", PLAYER_COLOR_PEACH, "|cffff8c6c",
        @"ReplaceableTextures\CommandButtons\BTNHeadHunterBerserker.blp")
      {
        StartingGold = 200,
        StartingLumber = 700,
        ControlPointDefenderUnitTypeId = Constants.UNIT_H0C1_CONTROL_POINT_DEFENDER_ZANDALAR,
        StartingCameraPosition = Regions.TrollStartPos.Center,
        StartingUnits = Regions.TrollStartPos.PrepareUnitsForRescue(RescuePreparationMode.Invulnerable),
        IntroText = @"You are playing as the mighty |cffe1946cZandalari Empire|r.

You start off at the southern coast of Tanaris, seperated from your allies. Raise an army and deal with the rogue Trolls in Zul'Farrak.

The Night Elves are mounting an assault on you and your allies from the North.

Join up with your allies and brace for a tough fight and counter-attack. "
      };

      Zandalar.ModObjectLimit(FourCC("o03R"), Faction.UNLIMITED); //Great Hall
      Zandalar.ModObjectLimit(FourCC("o03Y"), Faction.UNLIMITED); //Stronghold
      Zandalar.ModObjectLimit(FourCC("o03Z"), Faction.UNLIMITED); //Fortress
      Zandalar.ModObjectLimit(FourCC("o040"), Faction.UNLIMITED); //Altar of Storms
      Zandalar.ModObjectLimit(FourCC("o041"), Faction.UNLIMITED); //Barracks
      Zandalar.ModObjectLimit(FourCC("o042"), Faction.UNLIMITED); //War Mill
      Zandalar.ModObjectLimit(FourCC("o044"), Faction.UNLIMITED); //Tauren Totem
      Zandalar.ModObjectLimit(FourCC("o043"), Faction.UNLIMITED); //Spirit Lodge
      Zandalar.ModObjectLimit(FourCC("o045"), Faction.UNLIMITED); //Orc Burrow
      Zandalar.ModObjectLimit(FourCC("o046"), Faction.UNLIMITED); //Watch Tower
      Zandalar.ModObjectLimit(FourCC("o048"), Faction.UNLIMITED); //Improved Watch Tower
      Zandalar.ModObjectLimit(FourCC("o047"), Faction.UNLIMITED); //Voodoo Lounge
      Zandalar.ModObjectLimit(FourCC("o049"), Faction.UNLIMITED); //Shipyard
      Zandalar.ModObjectLimit(FourCC("o04X"), Faction.UNLIMITED); //Loa Shrine

      Zandalar.ModObjectLimit(FourCC("o04A"), Faction.UNLIMITED); //Peon
      Zandalar.ModObjectLimit(FourCC("h021"), Faction.UNLIMITED); //Grunt
      Zandalar.ModObjectLimit(FourCC("o04D"), Faction.UNLIMITED); //Troll Headhunter
      Zandalar.ModObjectLimit(FourCC("n09E"), 2); //Storm Wyrm
      Zandalar.ModObjectLimit(FourCC("e00Z"), 8); //Direhorn
      Zandalar.ModObjectLimit(FourCC("o04F"), Faction.UNLIMITED); //Troll Witch Doctor
      Zandalar.ModObjectLimit(FourCC("o04G"), Faction.UNLIMITED); //Haruspex
      Zandalar.ModObjectLimit(FourCC("o04E"), 6); //Boneseer
      Zandalar.ModObjectLimit(FourCC("h05D"), Faction.UNLIMITED); //Raptor Rider
      Zandalar.ModObjectLimit(FourCC("o021"), 12); //Ravager
      Zandalar.ModObjectLimit(FourCC("nftk"), 12); //Warlord
      Zandalar.ModObjectLimit(FourCC("o02K"), 6); //Bear Rider
      Zandalar.ModObjectLimit(FourCC("n0DN"), 6); //Medium
      Zandalar.ModObjectLimit(FourCC("e01Z"), 3); //Throne of War

      //Ship
      Zandalar.ModObjectLimit(FourCC("obot"), Faction.UNLIMITED); //Transport Ship
      Zandalar.ModObjectLimit(FourCC("h0AS"), Faction.UNLIMITED); //Scout
      Zandalar.ModObjectLimit(FourCC("h0AP"), Faction.UNLIMITED); //Frigate
      Zandalar.ModObjectLimit(FourCC("h0B2"), Faction.UNLIMITED); //Fireship
      Zandalar.ModObjectLimit(FourCC("h0AY"), Faction.UNLIMITED); //Galley
      Zandalar.ModObjectLimit(FourCC("h0B5"), Faction.UNLIMITED); //Boarding
      Zandalar.ModObjectLimit(FourCC("h0BC"), Faction.UNLIMITED); //Juggernaut
      Zandalar.ModObjectLimit(FourCC("h0AO"), 6); //Bombard

      Zandalar.ModObjectLimit(FourCC("O026"), 1); //Rasthakan
      Zandalar.ModObjectLimit(FourCC("O01J"), 1); //Zul
      Zandalar.ModObjectLimit(FourCC("U023"), 1); //Hakkar
      Zandalar.ModObjectLimit(FourCC("H06Q"), 1); //Gazrilla

      Zandalar.ModObjectLimit(FourCC("Rers"), Faction.UNLIMITED); //Resistant Skin
      Zandalar.ModObjectLimit(FourCC("R00H"), Faction.UNLIMITED); //Animal Companion
      Zandalar.ModObjectLimit(FourCC("R070"), Faction.UNLIMITED); //Haruspex Training
      Zandalar.ModObjectLimit(FourCC("R071"), Faction.UNLIMITED); //Hex Training
      Zandalar.ModObjectLimit(FourCC("R023"), Faction.UNLIMITED); //Spiritual Infusion

      Zandalar.AddGoldMine(preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(-8900, -17000)));

      FactionManager.Register(Zandalar);
    }
  }
}