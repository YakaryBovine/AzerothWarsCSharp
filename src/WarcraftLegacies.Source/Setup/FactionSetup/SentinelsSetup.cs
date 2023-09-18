using MacroTools;
using MacroTools.FactionSystem;
using MacroTools.Powers;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Setup.FactionSetup
{
  public static class SentinelsSetup
  {
    public static Faction? Sentinels { get; private set; }

    public static void Setup(PreplacedUnitSystem preplacedUnitSystem)
    {
      Sentinels = new Faction("Sentinels", PLAYER_COLOR_MINT, "|CFFBFFF80",
        @"ReplaceableTextures\CommandButtons\BTNPriestessOfTheMoon.blp")
      {
        UndefeatedResearch = FourCC("R05Y"),
        StartingGold = 200,
        StartingLumber = 700,
        CinematicMusic = "Comradeship",
        ControlPointDefenderUnitTypeId = Constants.UNIT_H03F_CONTROL_POINT_DEFENDER_SENTINELS,
        IntroText = @"You are playing as the ever-watchful Sentinels.

The Druids are slowly waking from their slumber, and it falls to you to drive back the Orcish invaders from Kalimdor until then.

Your first mission is to race down the coast to Feathermoon Stronghold, a powerful Sentinel stronghold on the southern half of the continent. 

Once you have secured your holdings, gather your army and destroy the Orcish Horde. Be careful, they will outnumber you if given time to unite the clans."
      };

      Sentinels.ModObjectLimit(FourCC("e00V"), Faction.UNLIMITED); //Temple of Elune
      Sentinels.ModObjectLimit(FourCC("e00R"), Faction.UNLIMITED); //Altar of Watchers
      Sentinels.ModObjectLimit(FourCC("e00L"), Faction.UNLIMITED); //War Academy
      Sentinels.ModObjectLimit(FourCC("edob"), Faction.UNLIMITED); //Hunter)s Hall
      Sentinels.ModObjectLimit(FourCC("eden"), Faction.UNLIMITED); //Ancient of Wonders
      Sentinels.ModObjectLimit(FourCC("e011"), Faction.UNLIMITED); //Night Elf Shipyard
      Sentinels.ModObjectLimit(FourCC("h03N"), Faction.UNLIMITED); //Enchanged Runestone
      Sentinels.ModObjectLimit(FourCC("h03M"), Faction.UNLIMITED); //Runestone
      Sentinels.ModObjectLimit(FourCC("n06O"), Faction.UNLIMITED); //Sentinel Embassy
      Sentinels.ModObjectLimit(FourCC("n06P"), Faction.UNLIMITED); //Sentinel Enclave
      Sentinels.ModObjectLimit(FourCC("n06J"), Faction.UNLIMITED); //Sentinel Outpost
      Sentinels.ModObjectLimit(FourCC("n06M"), Faction.UNLIMITED); //Residence
      Sentinels.ModObjectLimit(FourCC("edos"), Faction.UNLIMITED); //Roost
      Sentinels.ModObjectLimit(FourCC("e00T"), Faction.UNLIMITED); //Bastion

      Sentinels.ModObjectLimit(FourCC("ewsp"), Faction.UNLIMITED); //Wisp
      Sentinels.ModObjectLimit(FourCC("e006"), Faction.UNLIMITED); //Priestess
      Sentinels.ModObjectLimit(FourCC("n06C"), Faction.UNLIMITED); //Trapper
      Sentinels.ModObjectLimit(FourCC("h04L"), 6); //Priestess of the Moon
      Sentinels.ModObjectLimit(FourCC("earc"), Faction.UNLIMITED); //Archer
      Sentinels.ModObjectLimit(FourCC("esen"), Faction.UNLIMITED); //Huntress
      Sentinels.ModObjectLimit(FourCC("h08V"), Faction.UNLIMITED); //Nightsaber Knight
      Sentinels.ModObjectLimit(FourCC("ebal"), 8); //Glaive Thrower
      Sentinels.ModObjectLimit(FourCC("ehpr"), 6); //Hippogryph Rider
      Sentinels.ModObjectLimit(FourCC("n034"), 12); //Guild Ranger
      Sentinels.ModObjectLimit(FourCC("nwat"), Faction.UNLIMITED); //Nightblade
      Sentinels.ModObjectLimit(FourCC("nnmg"), 12); //Redeemed Highborne
      Sentinels.ModObjectLimit(FourCC("e022"), 2); //Moon Rider
      Sentinels.ModObjectLimit(Constants.UNIT_ECHM_CHIMAERA_SENTINELS, 6);

      //Ships
      Sentinels.ModObjectLimit(FourCC("etrs"), Faction.UNLIMITED); //Night Elf Transport Ship
      Sentinels.ModObjectLimit(FourCC("h0AU"), Faction.UNLIMITED); // Scout
      Sentinels.ModObjectLimit(FourCC("h0AV"), Faction.UNLIMITED); // Frigate
      Sentinels.ModObjectLimit(FourCC("h0B1"), Faction.UNLIMITED); // Fireship
      Sentinels.ModObjectLimit(FourCC("h057"), Faction.UNLIMITED); // Galley
      Sentinels.ModObjectLimit(FourCC("h0B4"), Faction.UNLIMITED); // Boarding
      Sentinels.ModObjectLimit(FourCC("h0BA"), Faction.UNLIMITED); // Juggernaut
      Sentinels.ModObjectLimit(FourCC("h0B8"), 6); // Bombard

      Sentinels.ModObjectLimit(FourCC("E025"), 1); //Naisha
      Sentinels.ModObjectLimit(FourCC("Etyr"), 1); //Tyrande
      Sentinels.ModObjectLimit(FourCC("E002"), 1); //Shandris
      Sentinels.ModObjectLimit(FourCC("Ewrd"), 1); //Maiev

      Sentinels.ModObjectLimit(FourCC("R00S"), Faction.UNLIMITED); //Priestess Adept Training
      Sentinels.ModObjectLimit(FourCC("R064"), Faction.UNLIMITED); //Sentinel Fortifications
      Sentinels.ModObjectLimit(FourCC("R01W"), Faction.UNLIMITED); //Trapper Adept Training
      Sentinels.ModObjectLimit(FourCC("R026"), Faction.UNLIMITED); //Elune's Power Infusion
      Sentinels.ModObjectLimit(FourCC("Reib"), Faction.UNLIMITED); //Improved Bows
      Sentinels.ModObjectLimit(FourCC("Reuv"), Faction.UNLIMITED); //Ultravision
      Sentinels.ModObjectLimit(FourCC("Remg"), Faction.UNLIMITED); //Upgraded Moon Glaive
      Sentinels.ModObjectLimit(FourCC("Roen"), Faction.UNLIMITED); //Ensnare
      Sentinels.ModObjectLimit(FourCC("R04E"), Faction.UNLIMITED); //Ysera's Gift (World Tree upgrade)
      Sentinels.ModObjectLimit(FourCC("R002"), Faction.UNLIMITED); //Blackwald Enhancement
      Sentinels.ModObjectLimit(FourCC("R03J"), Faction.UNLIMITED); //Wind Walk
      Sentinels.ModObjectLimit(FourCC("R018"), Faction.UNLIMITED); //Lightning Barrage

      Sentinels.AddGoldMine(preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(-22721, -13570)));

      //Powers
      var dummyPower = new DummyPower("Unspoiled Wilderness",
        "Every control point tower has a 24% movement speed aura in a 2500 area of effect",
        "ANA_HealingButterfliesFixed");
      Sentinels.AddPower(dummyPower);

      FactionManager.Register(Sentinels);
    }
  }
}