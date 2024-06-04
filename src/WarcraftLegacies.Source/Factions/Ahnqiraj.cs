using MacroTools;
using MacroTools.FactionSystem;
using WarcraftLegacies.Source.Quests.Cthun;
using MacroTools.PassiveAbilitySystem;
using MacroTools.ResearchSystems;
using MacroTools.Spells;
using MacroTools.SpellSystem;
using WarcraftLegacies.Shared;
using WarcraftLegacies.Shared.Config.Factions;
using WarcraftLegacies.Shared.Models.Abilities;
using WarcraftLegacies.Source.PassiveAbilities.DefensiveCocoon;
using WarcraftLegacies.Source.PassiveAbilities.Incubate;
using WarcraftLegacies.Source.PassiveAbilities.SpellConduction;
using WarcraftLegacies.Source.Researches;
using WarcraftLegacies.Source.Researches.Ahnqiraj;
using WarcraftLegacies.Source.Setup;

namespace WarcraftLegacies.Source.Factions
{
  public sealed class Ahnqiraj : Faction
  {
    private readonly AllLegendSetup _allLegendSetup;

    /// <inheritdoc />
    public Ahnqiraj(AllLegendSetup allLegendSetup) : base("Ahn'qiraj", PLAYER_COLOR_WHEAT, "|cffaaa050",
      @"ReplaceableTextures\CommandButtons\BTNCthunIcon.blp")
    {
      _allLegendSetup = allLegendSetup;
      ControlPointDefenderUnitTypeId = UNIT_N0DW_CONTROL_POINT_DEFENDER_CTHUN_TOWER;
      TraditionalTeam = TeamSetup.OldGods;
    }

    /// <inheritdoc />
    public override void OnRegistered()
    {
      RegisterObjectLimits();
      RegisterResearches();
      RegisterSpells();
      SharedFactionConfigSetup.AddSharedFactionConfig(this);
      RegisterQuests();
    }

    private void RegisterObjectLimits()
    {
      foreach (var (objectTypeId, objectLimit) in AhnqirajObjectLimitData.GetAllObjectLimits())
        ModObjectLimit(FourCC(objectTypeId), objectLimit);
    }

    private void RegisterQuests()
    {
      var newQuest = AddQuest(new QuestTitanJailors(Regions.QirajInsideUnlock));
      StartingQuest = newQuest;
      AddQuest(new QuestRebuildAhnqiraj(Regions.QirajOutsideUnlock));
      AddQuest(new QuestSlitheringForward(Regions.QirajOutpost1, Regions.QirajOutpost2, Regions.QirajOutpost3));
      AddQuest(new QuestTanarisOutpost(Regions.QirajOutpost5));
      AddQuest(new QuestEmperorConstruct());
      AddQuest(new QuestMockeryOfLife());
      AddQuest(new QuestWarOfTheShiftingSand(_allLegendSetup.Druids.Nordrassil));
      AddQuest(new QuestDeliciousMusculature(_allLegendSetup.Warsong.Orgrimmar));
      AddQuest(new QuestLitheMeat());
      AddQuest(new QuestFiendThousandFaces(_allLegendSetup.Neutral.YoggSaron));
    }

    private void RegisterResearches()
    {
      ResearchManager.Register(new Progenesis(UPGRADE_R003_PROGENESIS_C_THUN, 20)
      {
        TransformableUnitTypeIds = new int[]
        {
          UNIT_U019_WORKER_C_THUN_WORKER,
          UNIT_UCBD_BURROWED_WORKER_C_THUN_WORKER
        },
        TransformedUnitTypeId = UNIT_N06I_SOLDIER_C_THUN_SILITHID_WARRIOR
      });
      ResearchManager.RegisterIncompatibleSet(new BasicResearch(UPGRADE_ZBML_SPELL_CONDUCTION_C_THUN, 170),
        new RemoveAbilityResearch(UPGRADE_ZBHS_SHAPED_OBSIDIAN_C_THUN, 100)
      {
        RemovedAbility = ABILITY_A13J_SPELL_RESISTANCE_RIFLEMAN_OBSIDIAN_ERADICATOR
      });
    }

    private void RegisterSpells()
    {
      var cocoonHeroes = new int[]
      {
        UNIT_U02S_ANCIENT_SAND_WORM,
        UNIT_E005_THE_PROPHET,
        UNIT_U00Z_OBSIDIAN_DESTROYER
      };

      PassiveAbilityManager.Register(new DefensiveCocoonAbility(cocoonHeroes, ABILITY_ZBEG_DEFENSIVE_COCOON_AHN_QIRAJ)
      {
        MaximumHealthPercentage = 0.5f,
        Duration = 45,
        EggId = UNIT_ZBBG_COCOON_DEFENSIVE_COCOON,
        ReviveEffect = @"Abilities\Spells\Undead\RaiseSkeletonWarrior\RaiseSkeleton.mdl",
        RequiredResearch = UPGRADE_ZBEH_DEFENSIVE_COCOOON_AHN_QIRAJ
      });

      PassiveAbilityManager.Register(new Incubate(UNIT_H01N_VILE_CORRUPTER_C_THUN, ABILITY_ZBRD_INCUBATE_VILE_CORRUPTOR)
      {
        HatchedUnitTypeId = UNIT_N06I_SOLDIER_C_THUN_SILITHID_WARRIOR,
        MaturationDuration = new LeveledAbilityField<float>
        {
          Base = 540f,
          PerLevel = -240f
        }
      });

      SpellSystem.Register(new InstantKill(ABILITY_ZBBS_HATCH_INCUBATE)
      {
        Target = InstantKill.KillTarget.Self
      });
      
      PassiveAbilityManager.Register(new SpellConductionAbility(UNIT_SL2O_OBSIDIAN_ERADICATOR_CTHUN)
      {
        RedirectionPercentage = 0.35f,
        RedirectableAttackTypes = new attacktype[]
        {
          ATTACK_TYPE_NORMAL,
          ATTACK_TYPE_MAGIC
        },
        RequiredResearch = UPGRADE_ZBML_SPELL_CONDUCTION_C_THUN,
        Radius = 500
      });
    }
  }
}