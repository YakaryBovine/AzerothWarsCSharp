using War3Net.Build.Common;
using War3Net.Build.Widget;

namespace Launcher.DataTransferObjects
{
  public class UnitDataDto
  {
    public int Flags { get; set; }
    public int OwnerId { get; set; }
    public int Unk1 { get; set; }
    public int Unk2 { get; set; }
    public int HP { get; set; }
    public int MP { get; set; }
    public int GoldAmount { get; set; }
    public int TargetAcquisition { get; set; }
    public int HeroLevel { get; set; }
    public int HeroStrength { get; set; }
    public int HeroAgility { get; set; }
    public int HeroIntelligence { get; set; }
    public InventoryItemData[] InventoryData { get; set; }
    public ModifiedAbilityData[] AbilityData { get; set; }
    public int CustomPlayerColorId { get; set; }
    public int WaygateDestinationRegionId { get; set; }
    public int TypeId { get; set; }
    public int Variation { get; set; }
    public Vector2Dto Position { get; set; }
    public double Rotation { get; set; }
    public Vector3Dto Scale { get; set; }
    public int SkinId { get; set; }
    public int MapItemTableId { get; set; }
    public RandomItemSet[] ItemTableSets { get; set; }
    public int CreationNumber { get; set; }
  }
}