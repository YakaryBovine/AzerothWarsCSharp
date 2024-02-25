using MacroTools;
using MacroTools.FactionChoices;
using WarcraftLegacies.Source.Factions;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Setup
{
  public static class FactionChoiceDialogSetup
  {
    public static void Setup(PreplacedUnitSystem preplacedUnitSystem, ArtifactSetup artifactSetup, AllLegendSetup allLegendSetup)
    {
      new FactionChoiceDialogPresenter(new Zandalar(preplacedUnitSystem, allLegendSetup, artifactSetup),
        new Bilgewater(preplacedUnitSystem, allLegendSetup)).Run(Player(8));
      
      new FactionChoiceDialogPresenter(new Illidari(allLegendSetup, artifactSetup),
        new Sunfury(preplacedUnitSystem, allLegendSetup, artifactSetup)).Run(Player(15));
      
      new FactionChoiceDialogPresenter(new Dalaran(preplacedUnitSystem, artifactSetup, allLegendSetup),
        new Gilneas(preplacedUnitSystem, artifactSetup, allLegendSetup)).Run(Player(7));
    }
  }
}