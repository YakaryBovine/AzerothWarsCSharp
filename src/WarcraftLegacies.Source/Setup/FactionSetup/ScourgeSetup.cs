using MacroTools;
using MacroTools.ArtifactSystem;
using MacroTools.FactionSystem;
using MacroTools.Powers;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Setup.FactionSetup
{
  public static class ScourgeSetup
  {
    public static Faction? Scourge { get; private set; }

    public static void Setup(PreplacedUnitSystem preplacedUnitSystem, Artifact helmOfDomination)
    {
      Scourge = new Faction("Scourge", PLAYER_COLOR_PURPLE, "|c00540081",
        @"ReplaceableTextures\CommandButtons\BTNRevenant.blp")
      {
        UndefeatedResearch = FourCC("R05K"),
        StartingGold = 200,
        StartingLumber = 700,
        FoodMaximum = 200,
        CinematicMusic = "ArthasTheme",
        ControlPointDefenderUnitTypeId = Constants.UNIT_U028_CONTROL_POINT_DEFENDER_SCOURGE,
        IntroText = @"You are playing as the the horrific Undead Scourge.

You begin in Northrend, a vast and isolated land; perfect to raise an army of undying warriors to destroy the living.

The local Nerubians have declared war on you. Destroy their decrepit holdings and kill their Queen to secure the continent.

Coordinate with the Burning Legion and use the Plague of Undeath to sweep Lordaeron away.

When the Plague hits Lordaeron, a great portal will be opened between Dragonblight and Scholomance."
      };

      //Buildings
      Scourge.ModObjectLimit(FourCC("unpl"), Faction.UNLIMITED); //Necropolis
      Scourge.ModObjectLimit(FourCC("unp1"), Faction.UNLIMITED); //Halls of the Dead
      Scourge.ModObjectLimit(FourCC("unp2"), Faction.UNLIMITED); //Black Citadel
      Scourge.ModObjectLimit(FourCC("uzig"), Faction.UNLIMITED); //Ziggurat
      Scourge.ModObjectLimit(FourCC("uzg1"), Faction.UNLIMITED); //Spirit Tower
      Scourge.ModObjectLimit(FourCC("uzg2"), Faction.UNLIMITED); //Nerubian Tower
      Scourge.ModObjectLimit(FourCC("uaod"), Faction.UNLIMITED); //Altar of Darkness
      Scourge.ModObjectLimit(FourCC("usep"), Faction.UNLIMITED); //Crypt
      Scourge.ModObjectLimit(FourCC("ugrv"), Faction.UNLIMITED); //Graveyard
      Scourge.ModObjectLimit(FourCC("uslh"), Faction.UNLIMITED); //Slaughterhouse
      Scourge.ModObjectLimit(FourCC("utod"), Faction.UNLIMITED); //Temple of the Damned
      Scourge.ModObjectLimit(FourCC("ubon"), Faction.UNLIMITED); //Boneyard
      Scourge.ModObjectLimit(FourCC("utom"), Faction.UNLIMITED); //Tomb of Relics
      Scourge.ModObjectLimit(FourCC("ushp"), Faction.UNLIMITED); //Undead Shipyard
      Scourge.ModObjectLimit(FourCC("u002"), Faction.UNLIMITED); //Improved Spirit Tower
      Scourge.ModObjectLimit(FourCC("u003"), Faction.UNLIMITED); //Improved Nerubian Tower

      //Units
      Scourge.ModObjectLimit(FourCC("uaco"), Faction.UNLIMITED); //Acolyte
      Scourge.ModObjectLimit(FourCC("ushd"), Faction.UNLIMITED); //Shade
      Scourge.ModObjectLimit(FourCC("ugho"), Faction.UNLIMITED); //Ghoul
      Scourge.ModObjectLimit(FourCC("uabo"), Faction.UNLIMITED); //Abomination
      Scourge.ModObjectLimit(FourCC("umtw"), 8); //Meat Wagon
      Scourge.ModObjectLimit(FourCC("ucry"), Faction.UNLIMITED); //Crypt Fiend
      Scourge.ModObjectLimit(FourCC("ugar"), 12); //Gargoyle
      Scourge.ModObjectLimit(FourCC("h02G"), Faction.UNLIMITED); //Vrykul Warrior
      Scourge.ModObjectLimit(FourCC("h061"), 12); //Vrykul Champion
      Scourge.ModObjectLimit(FourCC("h00P"), 4); //Mammoth rider
      Scourge.ModObjectLimit(FourCC("uban"), Faction.UNLIMITED); //Banshee
      Scourge.ModObjectLimit(FourCC("unec"), Faction.UNLIMITED); //Necromancer
      Scourge.ModObjectLimit(FourCC("uobs"), 4); //Obsidian Statue
      Scourge.ModObjectLimit(FourCC("ufro"), 4); //Frost Wyrm
      Scourge.ModObjectLimit(FourCC("h00H"), 6); //Death Knight
      Scourge.ModObjectLimit(FourCC("ubsp"), 6); //Destroyer
      Scourge.ModObjectLimit(FourCC("nfgl"), 2); //Plague Titan

      //Ship
      Scourge.ModObjectLimit(FourCC("ubot"), Faction.UNLIMITED); //Undead Transport Ship
      Scourge.ModObjectLimit(FourCC("h0AT"), Faction.UNLIMITED); //Scout
      Scourge.ModObjectLimit(FourCC("h0AW"), Faction.UNLIMITED); //Frigate
      Scourge.ModObjectLimit(FourCC("h0AM"), Faction.UNLIMITED); //Fireship
      Scourge.ModObjectLimit(FourCC("h0AZ"), Faction.UNLIMITED); //Galley
      Scourge.ModObjectLimit(FourCC("h0AQ"), Faction.UNLIMITED); //Boarding
      Scourge.ModObjectLimit(FourCC("h0BB"), Faction.UNLIMITED); //Juggernaut
      Scourge.ModObjectLimit(FourCC("h0B9"), 6); //Bombard

      //Demi-Heroes
      Scourge.ModObjectLimit(FourCC("ubdd"), 1); //Sapphiron
      Scourge.ModObjectLimit(FourCC("uswb"), 1); //Banshee
      Scourge.ModObjectLimit(FourCC("Uanb"), 1); //Anub'arak
      Scourge.ModObjectLimit(FourCC("U001"), 1); //Kel'thuzad
      Scourge.ModObjectLimit(FourCC("U00M"), 1); //Kel'thuzad (Ghost)
      Scourge.ModObjectLimit(FourCC("U00A"), 1); //Rivendare
      Scourge.ModObjectLimit(FourCC("Uktl"), 1); //Kel'thuzad (Lich)
      Scourge.ModObjectLimit(Constants.UNIT_UEAR_CHAMPION_OF_THE_SCOURGE_SCOURGE, 1);

      //Upgrades
      Scourge.ModObjectLimit(FourCC("Ruba"), Faction.UNLIMITED); //Banshee Adept Training
      Scourge.ModObjectLimit(FourCC("Rubu"), Faction.UNLIMITED); //Burrow
      Scourge.ModObjectLimit(FourCC("Ruex"), Faction.UNLIMITED); //Exhume Corpses
      Scourge.ModObjectLimit(FourCC("Rufb"), Faction.UNLIMITED); //Freezing Breath
      Scourge.ModObjectLimit(FourCC("Rugf"), Faction.UNLIMITED); //Ghoul Frenzy
      Scourge.ModObjectLimit(FourCC("Rune"), Faction.UNLIMITED); //Necromancer Adept Training
      Scourge.ModObjectLimit(FourCC("Ruwb"), Faction.UNLIMITED); //Web
      Scourge.ModObjectLimit(FourCC("R00Q"), Faction.UNLIMITED); //Chilling Aura
      Scourge.ModObjectLimit(Constants.UPGRADE_R01X_EPIDEMIC_SCOURGE, Faction.UNLIMITED);
      Scourge.ModObjectLimit(Constants.UPGRADE_R08T_REVENANTS_SCOURGE, Faction.UNLIMITED);
      Scourge.ModObjectLimit(FourCC("R01D"), Faction.UNLIMITED); //Piercing Screech
      Scourge.ModObjectLimit(FourCC("R06N"), Faction.UNLIMITED); //Improved Orb of Annihilation
      Scourge.ModObjectLimit(FourCC("Rusl"), Faction.UNLIMITED); //Skeletal Mastery
      Scourge.ModObjectLimit(FourCC("Rusm"), Faction.UNLIMITED); //Skeletal Longevity
      Scourge.ModObjectLimit(Constants.UPGRADE_R026_GARGOYLES_SCOURGE, Faction.UNLIMITED);
      Scourge.ModObjectLimit(Constants.UPGRADE_R02A_FROST_WYRMS_SCOURGE, Faction.UNLIMITED);
      Scourge.ModObjectLimit(Constants.UPGRADE_R04V_OBSIDIAN_DESTROYERS_SCOURGE, Faction.UNLIMITED);

      Scourge.ModObjectLimit(Constants.UPGRADE_R07X_MAKE_ARTHAS_THE_LICH_KING_SCOURGE, Faction.UNLIMITED);

      //Abilities
      Scourge.ModAbilityAvailability(Constants.ABILITY_A0WG_SPELL_SHIELD_SPELL_BOOK_ORANGE_ANTONIDAS_RED_LICH_KING, -1);
      Scourge.ModAbilityAvailability(Constants.ABILITY_A0K2_RAISE_DEAD_AUTO_CAST_RED_TEMPLE_OF_THE_DAMNED_OFF, -1);
      Scourge.ModAbilityAvailability(Constants.ABILITY_A09N_PERMANENT_IMMOLATION_SCOURGE_ICECROWN_OBELISK, -1);

      //Powers
      var visionPower = new VisionPower("All-Seeing",
        "Grants permanent vision over Northrend.",
        "Charm", new[]
        {
          Regions.Storm_Peaks,
          Regions.Central_Northrend,
          Regions.The_Basin,
          Regions.Ice_Crown,
          Regions.Fjord,
          Regions.Eastern_Northrend,
          Regions.Far_Eastern_Northrend,
          Regions.Coldarra,
          Regions.Borean_Tundra,
          Regions.IcecrownShipyard
        });
      Scourge.AddPower(visionPower);

      Scourge.AddGoldMine(preplacedUnitSystem.GetUnit(FourCC("ngol"), new Point(-4939, 18803)));

      FactionManager.Register(Scourge);
    }
  }
}