namespace WarcraftLegacies.Source.FactionMechanics.Scourge.Plague
{
  /// <summary>
  /// Specifies how many of each unit type to spawn during the Plague.
  /// </summary>
  public sealed class PlagueCauldronSummonParameter
  {
    /// <summary>
    /// How many of this unit to spawn around the Plague Cauldron.
    /// </summary>
    public int SummonCount { get; }
    
    /// <summary>
    /// What type of unit to spawn around the Plague Cauldron.
    /// </summary>
    public int SummonUnitTypeId { get; }

    public PlagueCauldronSummonParameter(int summonCount, int summonUnitTypeId)
    {
      SummonCount = summonCount;
      SummonUnitTypeId = summonUnitTypeId;
    }
  }
}