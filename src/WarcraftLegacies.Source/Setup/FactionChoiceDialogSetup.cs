using MacroTools;
using MacroTools.FactionChoices;
using WarcraftLegacies.Source.Factions;

namespace WarcraftLegacies.Source.Setup
{
  public static class FactionChoiceDialogSetup
  {
    public static void Setup(PreplacedUnitSystem preplacedUnitSystem, ArtifactSetup artifactSetup, AllLegendSetup allLegendSetup, SharedGoldMineManager sharedGoldMineManager)
    {
      //new FactionChoiceDialogPresenter(new Zandalar(preplacedUnitSystem, allLegendSetup, artifactSetup),
      //  new Bilgewater(preplacedUnitSystem, allLegendSetup)).Run(Player(8));

      new FactionChoiceDialogPresenter(new Illidari(allLegendSetup, artifactSetup),
        new Sunfury(preplacedUnitSystem, allLegendSetup, artifactSetup)).Run(Player(15));
      
      new FactionChoiceDialogPresenter(new Dalaran(preplacedUnitSystem, artifactSetup, allLegendSetup),
        new Gilneas(preplacedUnitSystem, artifactSetup, allLegendSetup)).Run(Player(7));

      new FactionChoiceDialogPresenter(new Sentinels(preplacedUnitSystem, allLegendSetup, artifactSetup, sharedGoldMineManager),
        new Draenei(preplacedUnitSystem, allLegendSetup, artifactSetup, sharedGoldMineManager)).Run(Player(18));
    }
  }
}