using System.Linq;
using MacroTools.Extensions;
using MacroTools.UserInterface;
using WCSharp.Shared.Data;
using static War3Api.Common;

namespace WarcraftLegacies.Source.GameLogic
{
  public sealed class ScourgeInvasionDialogPresenter : ChoiceDialogPresenter<Rectangle?>
  {
    public ScourgeInvasionDialogPresenter(params Choice<Rectangle?>[] invasionTargets) : base(invasionTargets,
      "Pick invasion location")
    {
    }

    protected override void OnChoicePicked(player pickingPlayer, Choice<Rectangle?> choice)
    {
      HasChoiceBeenPicked = true;
      if (choice.Data == null)
        return;
      
      var invasionLocation = choice.Data;
      foreach (var unit in CreateGroup().EnumUnitsInRect(Regions.Northrend_Ambiance).EmptyToList()
                 .Where(x => x.OwningPlayer() == pickingPlayer))
      {
        if (IsUnitType(unit, UNIT_TYPE_STRUCTURE) || IsUnitType(unit, UNIT_TYPE_ANCIENT) ||
            IsUnitType(unit, UNIT_TYPE_PEON) ||
            GetResourceAmount(unit) > 0 || OrderId2String(GetUnitCurrentOrder(unit)) == "harvest") 
          continue;
        
        if (invasionLocation != null)
          SetUnitPosition(unit, invasionLocation.Center.X, invasionLocation.Center.Y);
      }

      if (invasionLocation != null)
        pickingPlayer.RepositionCamera(invasionLocation.Center);
    }

    /// <inheritdoc />
    protected override Choice<Rectangle?> GetDefaultChoice(player whichPlayer) => Choices.First();
  }
}