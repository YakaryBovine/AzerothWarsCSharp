using MacroTools.Extensions;
using MacroTools.FactionSystem;
using MacroTools.LegendSystem;
using MacroTools.ObjectiveSystem.Objectives.LegendBased;
using MacroTools.QuestSystem;
using WarcraftLegacies.Source.Powers;
using WarcraftLegacies.Source.Quests.Quelthalas;

namespace WarcraftLegacies.Source.Quests.Scourge
{
  public sealed class QuestKelthuzadLich : QuestData
  {
    private readonly Capital _sunwell;
    private readonly LegendaryHero _kelthuzad;
    private readonly Faction _quelthalas;
    private const int UnittypeKelthuzadLich = UNIT_UKTL_ARCHLICH_OF_THE_SCOURGE_SCOURGE_LICH;

    public QuestKelthuzadLich(Capital sunwell, LegendaryHero kelthuzad, Faction quelthalas) : base(
      "Into the Realm Eternal",
      "Kel'thuzad is the leader of the Cult of the Damned and an extraordinarily powerful necromancer. If he were to be brought to the Sunwell and submerged in its waters, he would be reanimated as an immortal Lich.",
      @"ReplaceableTextures\CommandButtons\BTNLichVersion2.blp")
    {
      _sunwell = sunwell;
      _kelthuzad = kelthuzad;
      _quelthalas = quelthalas;
      AddObjective(new ObjectiveControlCapital(sunwell, false));
      AddObjective(new ObjectiveLegendInRect(kelthuzad, Regions.Sunwell, "The Sunwell"));
      ResearchId = UPGRADE_R065_QUEST_COMPLETED_INTO_THE_REALM_ETERNAL;
      Global = true;
    }

    /// <inheritdoc />
    public override string RewardFlavour =>
      "The Necromancer Kel'thuzad has been immersed in the Sunwell and reborn as a Lich. The well, formerly a beacon of eternal light and power, has been twisted into a font of dark magic, spreading malevolence across the land.";

    /// <inheritdoc />
    protected override string RewardDescription =>
      "Permanently corrupt the Sunwell and turn Kel'thuzad into a Lich, causing his Dark Ritual ability to also summon a Revenant";

    /// <inheritdoc />
    protected override void OnComplete(Faction completingFaction)
    {
      _kelthuzad.UnitType = UnittypeKelthuzadLich;
      _kelthuzad.PermaDies = false;
      SetUnitState(_kelthuzad.Unit, UNIT_STATE_LIFE,
        GetUnitState(_kelthuzad.Unit, UNIT_STATE_MAX_LIFE));
      SetUnitState(_kelthuzad.Unit, UNIT_STATE_MANA,
        GetUnitState(_kelthuzad.Unit, UNIT_STATE_MAX_MANA));
      DestroyEffect(AddSpecialEffect("war3mapImported\\Soul Beam Blue.mdx",
        GetUnitX(_kelthuzad.Unit),
        GetUnitY(_kelthuzad.Unit)));
      DestroyEffect(AddSpecialEffect(@"Abilities\Spells\Undead\FrostNova\FrostNovaTarget.mdl",
        GetUnitX(_kelthuzad.Unit), GetUnitY(_kelthuzad.Unit)));

      CorruptSunwell();
    }

    private void CorruptSunwell()
    {
      RemoveAbilities();
      AddCorruptedAbilities();
      _sunwell.Essential = false;
      _sunwell.Unit?.SetName("Corrupted Sunwell");

      var corruptedSunwellPower = new CorruptedSunwell(0.2f);
      _quelthalas.AddPower(corruptedSunwellPower);
      var destroySunwellQuest =
        new QuestDestroyCorruptedSunwell(_sunwell, corruptedSunwellPower, _quelthalas.GetPowerByType<FontOfPower>()!);
      _quelthalas.AddQuest(destroySunwellQuest);
      _quelthalas.DisplayDiscovered(destroySunwellQuest);
    }

    private void RemoveAbilities()
    {
      _sunwell.Unit?
        .RemoveAbility(ABILITY_A0OC_EXTRACT_VIAL_ALL)
        .RemoveAbility(ABILITY_A0EP_SUMMON_GRANITE_GOLEMS_QUEL_THALAS_SUNWELL);
    }

    private void AddCorruptedAbilities()
    {
      _sunwell.Unit?
        .AddAbility(ABILITY_A00D_DESTROY_THE_CORRUPTED_SUNWELL_QUEL_THALAS_SUNWELL);
    }
  }
}