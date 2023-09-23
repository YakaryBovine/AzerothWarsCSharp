using MacroTools;
using WarcraftLegacies.Source.Quests.Ironforge;
using WarcraftLegacies.Source.Setup.FactionSetup;
using static War3Api.Common;

namespace WarcraftLegacies.Source.Setup.QuestSetup
{
  public static class IronforgeQuestSetup
  {
    public static void Setup(PreplacedUnitSystem preplacedUnitSystem, AllLegendSetup allLegendSetup)
    {
      var ironforge = IronforgeSetup.Ironforge;
      var newQuest = ironforge.AddQuest(new QuestThelsamar(preplacedUnitSystem, Regions.ThelUnlock));
      ironforge.StartingQuest = newQuest;
      ironforge.AddQuest(new QuestDunMorogh(preplacedUnitSystem));
      ironforge.AddQuest(new QuestDominion(Regions.IronforgeAmbient));
      ironforge.AddQuest(new QuestGnomeregan(Regions.Gnomergan, preplacedUnitSystem));
      ironforge.AddQuest(new QuestDarkIron(Regions.Shadowforge_City, allLegendSetup.FelHorde.BlackTemple, allLegendSetup.Ironforge.Magni));
      ironforge.AddQuest(new QuestWildhammer(allLegendSetup.Ironforge.Magni));

      var missingArtifacts = new int[]
      {
        Constants.ITEM_I01A_DEMON_SOUL,
        Constants.ITEM_I00F_GLOVES_OF_AHN_QIRAJ,
        Constants.ITEM_I00Z_THUNDERFURY,
        Constants.ITEM_I015_XAL_ATATH_BLADE_OF_THE_BLACK_EMPIRE,
        Constants.ITEM_I01T_FANDRAL_S_FLAMESCYTHE
      };
      ironforge.AddQuest(new QuestExpedition(missingArtifacts[GetRandomInt(0, missingArtifacts.Length - 1)]));
    }
  }
}