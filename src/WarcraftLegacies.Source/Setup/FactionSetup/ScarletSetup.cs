using MacroTools;
using MacroTools.FactionSystem;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Setup.FactionSetup
{
  public static class ScarletSetup
  {
    public static Faction? ScarletCrusade { get; private set; }

    public static void Setup(PreplacedUnitSystem preplacedUnitSystem)
    {
      ScarletCrusade = new Faction("Militia", PLAYER_COLOR_MAROON, "|cff800000",
        @"ReplaceableTextures\CommandButtons\BTNPeasant.blp")
      {
        StartingGold = 150,
        StartingLumber = 500,
        IntroText = @"You are playing as the zealous |cff940000Scarlet Crusade|r.

The Cult of the Damned has mobilized and is quietly spreading corruption throughout Lordaeron.

Build towers to detect the hidden cultists moving through the Kingdom and burn any buildings they have corrupted.

Your soldiers are weaker when alone, but gain substantial bonuses when paired with a variety of unit-types. 

Fortify your strongholds against the storm to come and make ready to unleash the Crusade on those who would defile your lands."
      };

      //Structures
      ScarletCrusade.ModObjectLimit(FourCC("h07X"), Faction.UNLIMITED); //Town Hall
      ScarletCrusade.ModObjectLimit(FourCC("h07Y"), Faction.UNLIMITED); //Keep
      ScarletCrusade.ModObjectLimit(FourCC("h07Z"), Faction.UNLIMITED); //Castle
      ScarletCrusade.ModObjectLimit(FourCC("h088"), Faction.UNLIMITED); //Farm
      ScarletCrusade.ModObjectLimit(FourCC("h080"), Faction.UNLIMITED); //Altar of Kings
      ScarletCrusade.ModObjectLimit(FourCC("h081"), Faction.UNLIMITED); //Barracks
      ScarletCrusade.ModObjectLimit(FourCC("h083"), Faction.UNLIMITED); //Chapel
      ScarletCrusade.ModObjectLimit(FourCC("h084"), Faction.UNLIMITED); //Scout Tower
      ScarletCrusade.ModObjectLimit(FourCC("h085"), Faction.UNLIMITED); //Guard Tower
      ScarletCrusade.ModObjectLimit(FourCC("h087"), Faction.UNLIMITED); //Guard Tower (Improved)
      ScarletCrusade.ModObjectLimit(FourCC("h0AG"), Faction.UNLIMITED); //Alliance Shipyard
      ScarletCrusade.ModObjectLimit(FourCC("h086"), Faction.UNLIMITED); //Marketplace
      ScarletCrusade.ModObjectLimit(FourCC("h082"), Faction.UNLIMITED); //Aviary
      ScarletCrusade.ModObjectLimit(FourCC("h097"), Faction.UNLIMITED); //Training Camp
      ScarletCrusade.ModObjectLimit(FourCC("h09M"), Faction.UNLIMITED); //Training Camp 2
      ScarletCrusade.ModObjectLimit(FourCC("h09N"), Faction.UNLIMITED); //Training Camp 3
      ScarletCrusade.ModObjectLimit(FourCC("h09P"), Faction.UNLIMITED); //Training Camp 4
      ScarletCrusade.ModObjectLimit(FourCC("h09O"), Faction.UNLIMITED); //Training Camp 5
      ScarletCrusade.ModObjectLimit(FourCC("h09Q"), Faction.UNLIMITED); //Training Camp 6

      //Units
      ScarletCrusade.ModObjectLimit(FourCC("hpea"), Faction.UNLIMITED); //Peasant
      ScarletCrusade.ModObjectLimit(FourCC("hbot"), Faction.UNLIMITED); //Alliance Transport Ship
      ScarletCrusade.ModObjectLimit(FourCC("hdes"), Faction.UNLIMITED); //Alliance Frigate
      ScarletCrusade.ModObjectLimit(FourCC("hbsh"), 6); //Alliance Battle Ship
      ScarletCrusade.ModObjectLimit(FourCC("h08I"), Faction.UNLIMITED); //Crusader
      ScarletCrusade.ModObjectLimit(FourCC("h08M"), Faction.UNLIMITED); //Men-at-arms
      ScarletCrusade.ModObjectLimit(FourCC("h095"), Faction.UNLIMITED); //Bowman
      ScarletCrusade.ModObjectLimit(FourCC("h096"), Faction.UNLIMITED); //Light Cavalry
      ScarletCrusade.ModObjectLimit(FourCC("h08L"), Faction.UNLIMITED); //Cavalier
      ScarletCrusade.ModObjectLimit(FourCC("n068"), Faction.UNLIMITED); //Inquisitor
      ScarletCrusade.ModObjectLimit(FourCC("h06B"), 6); //Templar
      ScarletCrusade.ModObjectLimit(FourCC("h08J"), Faction.UNLIMITED); //Arbalest
      ScarletCrusade.ModObjectLimit(FourCC("h08K"), Faction.UNLIMITED); //Chaplain
      ScarletCrusade.ModObjectLimit(FourCC("n09N"), 6); //Bishop
      ScarletCrusade.ModObjectLimit(FourCC("n07N"), 6); //Airship
      ScarletCrusade.ModObjectLimit(FourCC("o04C"), 6); //Mortar
      ScarletCrusade.ModObjectLimit(FourCC("o00X"), 3); //Archangel
      ScarletCrusade.ModObjectLimit(FourCC("h09X"), 12); //Mounted Archer

      //Heroes
      ScarletCrusade.ModObjectLimit(FourCC("H08G"), 1); //Saiden
      ScarletCrusade.ModObjectLimit(FourCC("H08H"), 1); //Isilien
      ScarletCrusade.ModObjectLimit(FourCC("H00Y"), 1); //Brigitte
      ScarletCrusade.ModObjectLimit(FourCC("H0A2"), 1); //Renault
      ScarletCrusade.ModObjectLimit(FourCC("H09Z"), 1); //Tirion

      ScarletCrusade.ModObjectLimit(FourCC("h09H"), 1); //Herod
      ScarletCrusade.ModObjectLimit(FourCC("h05W"), 1); //Isilien

      //Upgrades
      ScarletCrusade.ModObjectLimit(FourCC("R05D"), Faction.UNLIMITED); //Chaplain Adept Training
      ScarletCrusade.ModObjectLimit(FourCC("R04F"), Faction.UNLIMITED); //Inquisitor traiing
      ScarletCrusade.ModObjectLimit(FourCC("Rhse"), Faction.UNLIMITED); //Magic Sentry
      ScarletCrusade.ModObjectLimit(FourCC("Rhlh"), Faction.UNLIMITED); //Improved Lumber Harvesting
      ScarletCrusade.ModObjectLimit(FourCC("Rhac"), Faction.UNLIMITED); //Improved Masonry
      ScarletCrusade.ModObjectLimit(FourCC("R06Q"), Faction.UNLIMITED); //Paladin Adept Training

      FactionManager.Register(ScarletCrusade);
    }
  }
}