using System.Linq;
using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.UnitBased;
using MacroTools.QuestSystem;
using WarcraftLegacies.Source.Powers;

namespace WarcraftLegacies.Source.Quests.Quelthalas
{
  public sealed class QuestDestroyCorruptedSunwell : QuestData
  {
    private readonly Capital _sunwell;
    private readonly CorruptedSunwell _corruptedSunwellPower;
    private readonly FontOfPower _fontOfPower;

    /// <inheritdoc />
    public QuestDestroyCorruptedSunwell(Capital sunwell, CorruptedSunwell corruptedSunwellPower, FontOfPower fontOfPower) : base("Forever Dusk", 
      "The necrotic taint at the heart of the Sunwell now permeates not only our people, but all we have built. The sacrifice we must make is grave but inevitable: the Sunwell must be destroyed by Thalassian hands.",
      @"ReplaceableTextures\CommandButtons\BTNWispSplode.blp")
    {
      _sunwell = sunwell;
      _corruptedSunwellPower = corruptedSunwellPower;
      _fontOfPower = fontOfPower;
      AddObjective(new ObjectiveCastSpellFromUnit(ABILITY_A00D_DESTROY_THE_CORRUPTED_SUNWELL_QUEL_THALAS_SUNWELL, sunwell.Unit!));
    }
    
    /// <inheritdoc/>
    protected override string RewardDescription => "Lose the Corrupted Sunwell power";
    
    /// <inheritdoc/>
    public override string RewardFlavour
    {
      get
      {
        var firstActiveFont = _fontOfPower.GetActiveFonts().FirstOrDefault(x => x != _sunwell);
        return firstActiveFont != null
          ? $"The blighted tumour of the Sunwell has been excised from the Thalassian homeland. Its people have already turned their magical thirst elsewhere, drawing upon the magicks of {firstActiveFont.Name}."
          : "With the Sunwell destroyed, the people of Quel'thalas are freed from its necrotic influence. Yet still they yearn for its magical energies - this addiction must be sated another way.";
      }
    }

    /// <inheritdoc />
    protected override void OnComplete(Faction completingFaction)
    {
      completingFaction.RemovePower(_corruptedSunwellPower);
      _sunwell.Capturable = false;
      _sunwell.Unit!.Kill();
    }

    /// <inheritdoc />
    protected override void OnAdd(Faction faction) =>
      faction.ModAbilityAvailability(ABILITY_A00D_DESTROY_THE_CORRUPTED_SUNWELL_QUEL_THALAS_SUNWELL, 1);
  }
}