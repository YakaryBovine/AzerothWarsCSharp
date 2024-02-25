using MacroTools.ControlPointSystem;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.ControlPointBased;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.Powers;
using MacroTools.QuestSystem;
using System.Collections.Generic;

namespace WarcraftLegacies.Source.Quests.Frostwolf
{
  /// <summary>
  /// Thrall becomes the World Shaman and gets cool stuff.
  /// </summary>
  public sealed class QuestWorldShaman : QuestData
  {
    private readonly LegendaryHero _thrall;

    /// <inheritdoc />
    public QuestWorldShaman(LegendaryHero thrall) : base("The World-Shaman",
      "The elements of Azeroth are in terrible disarray, and the situation only grows worse as rising conflicts threaten to tear our world apart. Thrall, as one of the most formidable Shamans of his time, must take up the mantle of the World-Shaman if he is to save his people - and the world.",
      @"ReplaceableTextures\CommandButtons\BTN_Lightning_Orc.blp")
    {
      _thrall = thrall;
      var controlPoints = new List<ControlPoint>
      {
        ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N028_DROWNED_REACHES),
        ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N02P_MAK_ARA),
        ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N04B_GISHAN_CAVERNS),
        ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N05Y_AZSUNA),
        ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N032_SURAMAR),
        ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N053_VAL_SHARAH),
        ControlPointManager.Instance.GetFromUnitType(Constants.UNIT_N05Z_STORMHEIM),
      };
      AddObjective(new ObjectiveLegendLevel(_thrall, 8));
      AddObjective(new ObjectiveChannelRect(Regions.MaelstromChannel, "the Maelstrom", _thrall, 120, 120, "Taming the Maelstrom"));
      AddObjective(new ObjectiveControlPoints(controlPoints, "on the Broken Isles and near the Maelstrom"));
    }

    /// <inheritdoc />
    public override string RewardFlavour =>
      "Thrall has stabilized the power of the Maelstrom and stored it within the Doomhammer. He is no longer merely the Warchief of the Horde; he is the World-Shaman of all Azeroth.";
    
    /// <inheritdoc />
    protected override string RewardDescription =>
      "Thrall gains 2000 experience and 10 Agility, and you gain the Power Maelstrom Spirit";

    /// <inheritdoc />
    protected override void OnComplete(Faction completingFaction)
    {
      _thrall.Unit?.SetName("World-Shaman")
        .AddHeroAttributes(0, 10, 0)
        .AddExperience(2000);
      completingFaction.AddPower(new MaelstromWeapon(0.12f, 100)
      {
        Effect = @"Doodads\Cinematic\Lightningbolt\Lightningbolt",
        ValidUnitTypes = new[]
        {
          Constants.UNIT_OPEO_PEON_FROSTWOLF_WARSONG_WORKER,
          Constants.UNIT_OGRU_GRUNT_FROSTWOLF,
          Constants.UNIT_OSHM_SHAMAN_FROSTWOLF,
          Constants.UNIT_O00A_FAR_SEER_FROSTWOLF_ELITE,
          Constants.UNIT_OTHR_WARCHIEF_OF_THE_HORDE_FROSTWOLF,
          Constants.UNIT_H00C_DREK_THAR_FROSTWOLF_DEMI,
          Constants.UNIT_H0CN_PACKLEADER_FROSTWOLF,
          Constants.UNIT_H0CO_MAMMOTH_WRANGLER_FROSTWOLF
        }
      });
    }
  }
}