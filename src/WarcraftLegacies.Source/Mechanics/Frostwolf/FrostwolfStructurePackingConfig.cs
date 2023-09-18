using MacroTools.Mechanics;

namespace WarcraftLegacies.Source.Mechanics.Frostwolf
{
  /// <summary>
  /// Config file for setting up the packable structures for the Frostwolf faction
  /// </summary>
  public static class FrostwolfStructurePackingConfig
  {
    /// <summary>
    /// Setup each packable structure for the Frostwolf faction
    /// </summary>
    public static void Setup()
    {
      PackableStructure.Register(Constants.UNIT_OGRE_GREAT_HALL_FROSTWOLF_T1, Constants.UNIT_OOSC_PACK_KODO_FROSTWOLF, Constants.ABILITY_A00O_UNPACK_GREAT_HALL_PACK_KODO,@"buildings\Orc\GreatHall\GreatHall.mdx");
      PackableStructure.Register(Constants.UNIT_OSTR_STRONGHOLD_FROSTWOLF_T2, Constants.UNIT_OOSC_PACK_KODO_FROSTWOLF, Constants.ABILITY_A0EU_UNPACK_STRONGHOLD_PACK_KODO, @"buildings\Orc\GreatHall\GreatHall.mdx");
      PackableStructure.Register(Constants.UNIT_OFRT_FORTRESS_FROSTWOLF_T3, Constants.UNIT_OOSC_PACK_KODO_FROSTWOLF, Constants.ABILITY_A0ET_UNPACK_FORTRESS_PACK_KODO, @"buildings\Orc\GreatHall\GreatHall.mdx");
      PackableStructure.Register(Constants.UNIT_OFOR_WAR_MILL_FROSTWOLF_RESEARCH, Constants.UNIT_OOSC_PACK_KODO_FROSTWOLF, Constants.ABILITY_A0D9_UNPACK_WAR_MILL_PACK_KODO, @"buildings\Orc\WarMill\WarMill.mdx");
      PackableStructure.Register(Constants.UNIT_OALT_ALTAR_OF_STORMS_FROSTWOLF_ALTAR, Constants.UNIT_OOSC_PACK_KODO_FROSTWOLF, Constants.ABILITY_A0E4_UNPACK_ALTAR_OF_STORMS_PACK_KODO, @"buildings\Orc\AltarofStorms\AltarofStorms.mdx");
      PackableStructure.Register(Constants.UNIT_OBAR_WAR_CAMP_FROSTWOLF_BARRACKS, Constants.UNIT_OOSC_PACK_KODO_FROSTWOLF, Constants.ABILITY_A02U_UNPACK_WAR_CAMP_PACK_KODO, @"buildings\Orc\OrcBarracks\OrcBarracks.mdx");
      PackableStructure.Register(Constants.UNIT_OTTO_TAUREN_TOTEM_FROSTWOLF_SPECIALIST, Constants.UNIT_OOSC_PACK_KODO_FROSTWOLF, Constants.ABILITY_A09Z_UNPACK_TAUREN_TOTEM_PACK_KODO, @"buildings\Orc\TaurenTotem\TaurenTotem.mdx");
      PackableStructure.Register(Constants.UNIT_OSLD_SPIRIT_LODGE_FROSTWOLF_MAGIC, Constants.UNIT_OOSC_PACK_KODO_FROSTWOLF, Constants.ABILITY_A09H_UNPACK_SPIRIT_LODGE_PACK_KODO, @"buildings\Orc\SpiritLodge\SpiritLodge.mdx");
      PackableStructure.Register(Constants.UNIT_OTRB_BURROW_FROSTWOLF_FARM, Constants.UNIT_OOSC_PACK_KODO_FROSTWOLF, Constants.ABILITY_A0EV_UNPACK_BURROW_PACK_KODO, @"buildings\Orc\TrollBurrow\TrollBurrow.mdx");
      PackableStructure.Register(Constants.UNIT_OSHY_HORDE_PIER_FROSTWOLF_SHIPYARD, Constants.UNIT_OOSC_PACK_KODO_FROSTWOLF, Constants.ABILITY_A0EX_UNPACK_HORDE_PIER_PACK_KODO, @"buildings\other\GoblinShipyard\GoblinShipyard.mdx");
    }
  }
}