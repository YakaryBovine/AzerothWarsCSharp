using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using War3Api.Object.Abilities;
using War3Api.Object.Enums;
using War3Net.Build.Object;
using War3Net.Common.Extensions;

namespace War3Api.Object
{
    public sealed class Unit : BaseObject
    {
        private readonly SimpleObjectDataModifications _modifications;
        public Unit(UnitType unitType): this((int)unitType)
        {
        }

        public Unit(UnitType unitType, int newId): this((int)unitType, newId)
        {
        }

        public Unit(UnitType unitType, string newRawcode): this((int)unitType, newRawcode)
        {
        }

        public Unit(UnitType unitType, ObjectDatabaseBase db): this((int)unitType, db)
        {
        }

        public Unit(UnitType unitType, int newId, ObjectDatabaseBase db): this((int)unitType, newId, db)
        {
        }

        public Unit(UnitType unitType, string newRawcode, ObjectDatabaseBase db): this((int)unitType, newRawcode, db)
        {
        }

        private Unit(int oldId): base(oldId)
        {
            _modifications = new(this);
        }

        private Unit(int oldId, int newId): base(oldId, newId)
        {
            _modifications = new(this);
        }

        private Unit(int oldId, string newRawcode): base(oldId, newRawcode)
        {
            _modifications = new(this);
        }

        private Unit(int oldId, ObjectDatabaseBase db): base(oldId, db)
        {
            _modifications = new(this);
        }

        private Unit(int oldId, int newId, ObjectDatabaseBase db): base(oldId, newId, db)
        {
            _modifications = new(this);
        }

        private Unit(int oldId, string newRawcode, ObjectDatabaseBase db): base(oldId, newRawcode, db)
        {
            _modifications = new(this);
        }

        public SimpleObjectDataModifications Modifications => _modifications;
        public string ArtRequiredAnimationNamesRaw
        {
            get => _modifications.GetModification(1768841589).ValueAsString;
            set => _modifications[1768841589] = new SimpleObjectDataModification{Id = 1768841589, Type = ObjectDataType.String, Value = value};
        }

        public bool IsArtRequiredAnimationNamesModified => _modifications.ContainsKey(1768841589);
        public IEnumerable<string> ArtRequiredAnimationNames
        {
            get => ArtRequiredAnimationNamesRaw.ToIEnumerableString(this);
            set => ArtRequiredAnimationNamesRaw = value.ToRaw(null, 20);
        }

        public string ArtIconGameInterfaceRaw
        {
            get => _modifications.GetModification(1868786037).ValueAsString;
            set => _modifications[1868786037] = new SimpleObjectDataModification{Id = 1868786037, Type = ObjectDataType.String, Value = value};
        }

        public bool IsArtIconGameInterfaceModified => _modifications.ContainsKey(1868786037);
        public string ArtIconGameInterface
        {
            get => ArtIconGameInterfaceRaw.ToString(this);
            set => ArtIconGameInterfaceRaw = value.ToRaw(null, null);
        }

        public string ArtRequiredAnimationNamesAttachmentsRaw
        {
            get => _modifications.GetModification(1885430133).ValueAsString;
            set => _modifications[1885430133] = new SimpleObjectDataModification{Id = 1885430133, Type = ObjectDataType.String, Value = value};
        }

        public bool IsArtRequiredAnimationNamesAttachmentsModified => _modifications.ContainsKey(1885430133);
        public IEnumerable<string> ArtRequiredAnimationNamesAttachments
        {
            get => ArtRequiredAnimationNamesAttachmentsRaw.ToIEnumerableString(this);
            set => ArtRequiredAnimationNamesAttachmentsRaw = value.ToRaw(null, 20);
        }

        public string ArtRequiredAttachmentLinkNamesRaw
        {
            get => _modifications.GetModification(1886151029).ValueAsString;
            set => _modifications[1886151029] = new SimpleObjectDataModification{Id = 1886151029, Type = ObjectDataType.String, Value = value};
        }

        public bool IsArtRequiredAttachmentLinkNamesModified => _modifications.ContainsKey(1886151029);
        public IEnumerable<string> ArtRequiredAttachmentLinkNames
        {
            get => ArtRequiredAttachmentLinkNamesRaw.ToIEnumerableString(this);
            set => ArtRequiredAttachmentLinkNamesRaw = value.ToRaw(null, 20);
        }

        public string TextTooltipAwaken
        {
            get => _modifications.GetModification(1953980789).ValueAsString;
            set => _modifications[1953980789] = new SimpleObjectDataModification{Id = 1953980789, Type = ObjectDataType.String, Value = value};
        }

        public bool IsTextTooltipAwakenModified => _modifications.ContainsKey(1953980789);
        public string ArtRequiredBoneNamesRaw
        {
            get => _modifications.GetModification(1919967861).ValueAsString;
            set => _modifications[1919967861] = new SimpleObjectDataModification{Id = 1919967861, Type = ObjectDataType.String, Value = value};
        }

        public bool IsArtRequiredBoneNamesModified => _modifications.ContainsKey(1919967861);
        public IEnumerable<string> ArtRequiredBoneNames
        {
            get => ArtRequiredBoneNamesRaw.ToIEnumerableString(this);
            set => ArtRequiredBoneNamesRaw = value.ToRaw(null, 20);
        }

        public string SoundConstructionRaw
        {
            get => _modifications.GetModification(1819501173).ValueAsString;
            set => _modifications[1819501173] = new SimpleObjectDataModification{Id = 1819501173, Type = ObjectDataType.String, Value = value};
        }

        public bool IsSoundConstructionModified => _modifications.ContainsKey(1819501173);
        public string SoundConstruction
        {
            get => SoundConstructionRaw.ToString(this);
            set => SoundConstructionRaw = value.ToRaw(null, null);
        }

        public string TechtreeStructuresBuiltRaw
        {
            get => _modifications.GetModification(1769300597).ValueAsString;
            set => _modifications[1769300597] = new SimpleObjectDataModification{Id = 1769300597, Type = ObjectDataType.String, Value = value};
        }

        public bool IsTechtreeStructuresBuiltModified => _modifications.ContainsKey(1769300597);
        public IEnumerable<Unit> TechtreeStructuresBuilt
        {
            get => TechtreeStructuresBuiltRaw.ToIEnumerableUnit(this);
            set => TechtreeStructuresBuiltRaw = value.ToRaw(null, null);
        }

        public int ArtButtonPositionX
        {
            get => _modifications.GetModification(2020631157).ValueAsInt;
            set => _modifications[2020631157] = new SimpleObjectDataModification{Id = 2020631157, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsArtButtonPositionXModified => _modifications.ContainsKey(2020631157);
        public int ArtButtonPositionY
        {
            get => _modifications.GetModification(2037408373).ValueAsInt;
            set => _modifications[2037408373] = new SimpleObjectDataModification{Id = 2037408373, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsArtButtonPositionYModified => _modifications.ContainsKey(2037408373);
        public string ArtCasterUpgradeArtRaw
        {
            get => _modifications.GetModification(1635083125).ValueAsString;
            set => _modifications[1635083125] = new SimpleObjectDataModification{Id = 1635083125, Type = ObjectDataType.String, Value = value};
        }

        public bool IsArtCasterUpgradeArtModified => _modifications.ContainsKey(1635083125);
        public string ArtCasterUpgradeArt
        {
            get => ArtCasterUpgradeArtRaw.ToString(this);
            set => ArtCasterUpgradeArtRaw = value.ToRaw(null, null);
        }

        public string TextCasterUpgradeNamesRaw
        {
            get => _modifications.GetModification(1853186933).ValueAsString;
            set => _modifications[1853186933] = new SimpleObjectDataModification{Id = 1853186933, Type = ObjectDataType.String, Value = value};
        }

        public bool IsTextCasterUpgradeNamesModified => _modifications.ContainsKey(1853186933);
        public IEnumerable<string> TextCasterUpgradeNames
        {
            get => TextCasterUpgradeNamesRaw.ToIEnumerableString(this);
            set => TextCasterUpgradeNamesRaw = value.ToRaw(null, 32);
        }

        public string TextCasterUpgradeTipsRaw
        {
            get => _modifications.GetModification(1953850229).ValueAsString;
            set => _modifications[1953850229] = new SimpleObjectDataModification{Id = 1953850229, Type = ObjectDataType.String, Value = value};
        }

        public bool IsTextCasterUpgradeTipsModified => _modifications.ContainsKey(1953850229);
        public IEnumerable<string> TextCasterUpgradeTips
        {
            get => TextCasterUpgradeTipsRaw.ToIEnumerableString(this);
            set => TextCasterUpgradeTipsRaw = value.ToRaw(null, null);
        }

        public string TechtreeDependencyEquivalentsRaw
        {
            get => _modifications.GetModification(1885693045).ValueAsString;
            set => _modifications[1885693045] = new SimpleObjectDataModification{Id = 1885693045, Type = ObjectDataType.String, Value = value};
        }

        public bool IsTechtreeDependencyEquivalentsModified => _modifications.ContainsKey(1885693045);
        public IEnumerable<Unit> TechtreeDependencyEquivalents
        {
            get => TechtreeDependencyEquivalentsRaw.ToIEnumerableUnit(this);
            set => TechtreeDependencyEquivalentsRaw = value.ToRaw(null, null);
        }

        public string TextDescription
        {
            get => _modifications.GetModification(1936024681).ValueAsString;
            set => _modifications[1936024681] = new SimpleObjectDataModification{Id = 1936024681, Type = ObjectDataType.String, Value = value};
        }

        public bool IsTextDescriptionModified => _modifications.ContainsKey(1936024681);
        public string TextNameEditorSuffix
        {
            get => _modifications.GetModification(1718840949).ValueAsString;
            set => _modifications[1718840949] = new SimpleObjectDataModification{Id = 1718840949, Type = ObjectDataType.String, Value = value};
        }

        public bool IsTextNameEditorSuffixModified => _modifications.ContainsKey(1718840949);
        public string TextHotkeyRaw
        {
            get => _modifications.GetModification(1953458293).ValueAsString;
            set => _modifications[1953458293] = new SimpleObjectDataModification{Id = 1953458293, Type = ObjectDataType.String, Value = value};
        }

        public bool IsTextHotkeyModified => _modifications.ContainsKey(1953458293);
        public char TextHotkey
        {
            get => TextHotkeyRaw.ToChar(this);
            set => TextHotkeyRaw = value.ToRaw(null, null);
        }

        public int SoundLoopingFadeInRate
        {
            get => _modifications.GetModification(1768320117).ValueAsInt;
            set => _modifications[1768320117] = new SimpleObjectDataModification{Id = 1768320117, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsSoundLoopingFadeInRateModified => _modifications.ContainsKey(1768320117);
        public int SoundLoopingFadeOutRate
        {
            get => _modifications.GetModification(1868983413).ValueAsInt;
            set => _modifications[1868983413] = new SimpleObjectDataModification{Id = 1868983413, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsSoundLoopingFadeOutRateModified => _modifications.ContainsKey(1868983413);
        public string TechtreeItemsMadeRaw
        {
            get => _modifications.GetModification(1768648053).ValueAsString;
            set => _modifications[1768648053] = new SimpleObjectDataModification{Id = 1768648053, Type = ObjectDataType.String, Value = value};
        }

        public bool IsTechtreeItemsMadeModified => _modifications.ContainsKey(1768648053);
        public IEnumerable<Item> TechtreeItemsMade
        {
            get => TechtreeItemsMadeRaw.ToIEnumerableItem(this);
            set => TechtreeItemsMadeRaw = value.ToRaw(null, 12);
        }

        public float CombatAttack1ProjectileArc
        {
            get => _modifications.GetModification(828468597).ValueAsFloat;
            set => _modifications[828468597] = new SimpleObjectDataModification{Id = 828468597, Type = ObjectDataType.Unreal, Value = value};
        }

        public bool IsCombatAttack1ProjectileArcModified => _modifications.ContainsKey(828468597);
        public float CombatAttack2ProjectileArc
        {
            get => _modifications.GetModification(845245813).ValueAsFloat;
            set => _modifications[845245813] = new SimpleObjectDataModification{Id = 845245813, Type = ObjectDataType.Unreal, Value = value};
        }

        public bool IsCombatAttack2ProjectileArcModified => _modifications.ContainsKey(845245813);
        public string CombatAttack1ProjectileArtRaw
        {
            get => _modifications.GetModification(1831952757).ValueAsString;
            set => _modifications[1831952757] = new SimpleObjectDataModification{Id = 1831952757, Type = ObjectDataType.String, Value = value};
        }

        public bool IsCombatAttack1ProjectileArtModified => _modifications.ContainsKey(1831952757);
        public string CombatAttack1ProjectileArt
        {
            get => CombatAttack1ProjectileArtRaw.ToString(this);
            set => CombatAttack1ProjectileArtRaw = value.ToRaw(null, null);
        }

        public string CombatAttack2ProjectileArtRaw
        {
            get => _modifications.GetModification(1832018293).ValueAsString;
            set => _modifications[1832018293] = new SimpleObjectDataModification{Id = 1832018293, Type = ObjectDataType.String, Value = value};
        }

        public bool IsCombatAttack2ProjectileArtModified => _modifications.ContainsKey(1832018293);
        public string CombatAttack2ProjectileArt
        {
            get => CombatAttack2ProjectileArtRaw.ToString(this);
            set => CombatAttack2ProjectileArtRaw = value.ToRaw(null, null);
        }

        public int CombatAttack1ProjectileHomingEnabledRaw
        {
            get => _modifications.GetModification(828927349).ValueAsInt;
            set => _modifications[828927349] = new SimpleObjectDataModification{Id = 828927349, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsCombatAttack1ProjectileHomingEnabledModified => _modifications.ContainsKey(828927349);
        public bool CombatAttack1ProjectileHomingEnabled
        {
            get => CombatAttack1ProjectileHomingEnabledRaw.ToBool(this);
            set => CombatAttack1ProjectileHomingEnabledRaw = value.ToRaw(null, null);
        }

        public int CombatAttack2ProjectileHomingEnabledRaw
        {
            get => _modifications.GetModification(845704565).ValueAsInt;
            set => _modifications[845704565] = new SimpleObjectDataModification{Id = 845704565, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsCombatAttack2ProjectileHomingEnabledModified => _modifications.ContainsKey(845704565);
        public bool CombatAttack2ProjectileHomingEnabled
        {
            get => CombatAttack2ProjectileHomingEnabledRaw.ToBool(this);
            set => CombatAttack2ProjectileHomingEnabledRaw = value.ToRaw(null, null);
        }

        public int CombatAttack1ProjectileSpeed
        {
            get => _modifications.GetModification(2050056565).ValueAsInt;
            set => _modifications[2050056565] = new SimpleObjectDataModification{Id = 2050056565, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsCombatAttack1ProjectileSpeedModified => _modifications.ContainsKey(2050056565);
        public int CombatAttack2ProjectileSpeed
        {
            get => _modifications.GetModification(2050122101).ValueAsInt;
            set => _modifications[2050122101] = new SimpleObjectDataModification{Id = 2050122101, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsCombatAttack2ProjectileSpeedModified => _modifications.ContainsKey(2050122101);
        public string SoundMovementRaw
        {
            get => _modifications.GetModification(1819503989).ValueAsString;
            set => _modifications[1819503989] = new SimpleObjectDataModification{Id = 1819503989, Type = ObjectDataType.String, Value = value};
        }

        public bool IsSoundMovementModified => _modifications.ContainsKey(1819503989);
        public string SoundMovement
        {
            get => SoundMovementRaw.ToString(this);
            set => SoundMovementRaw = value.ToRaw(null, null);
        }

        public string TextName
        {
            get => _modifications.GetModification(1835101813).ValueAsString;
            set => _modifications[1835101813] = new SimpleObjectDataModification{Id = 1835101813, Type = ObjectDataType.String, Value = value};
        }

        public bool IsTextNameModified => _modifications.ContainsKey(1835101813);
        public string TextProperNamesRaw
        {
            get => _modifications.GetModification(1869770869).ValueAsString;
            set => _modifications[1869770869] = new SimpleObjectDataModification{Id = 1869770869, Type = ObjectDataType.String, Value = value};
        }

        public bool IsTextProperNamesModified => _modifications.ContainsKey(1869770869);
        public IEnumerable<string> TextProperNames
        {
            get => TextProperNamesRaw.ToIEnumerableString(this);
            set => TextProperNamesRaw = value.ToRaw(null, 32);
        }

        public string SoundRandomRaw
        {
            get => _modifications.GetModification(1819505269).ValueAsString;
            set => _modifications[1819505269] = new SimpleObjectDataModification{Id = 1819505269, Type = ObjectDataType.String, Value = value};
        }

        public bool IsSoundRandomModified => _modifications.ContainsKey(1819505269);
        public string SoundRandom
        {
            get => SoundRandomRaw.ToString(this);
            set => SoundRandomRaw = value.ToRaw(null, null);
        }

        public int TechtreeRequirementsTiersUsed
        {
            get => _modifications.GetModification(1668379253).ValueAsInt;
            set => _modifications[1668379253] = new SimpleObjectDataModification{Id = 1668379253, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsTechtreeRequirementsTiersUsedModified => _modifications.ContainsKey(1668379253);
        public string TechtreeRequirementsRaw
        {
            get => _modifications.GetModification(1902473845).ValueAsString;
            set => _modifications[1902473845] = new SimpleObjectDataModification{Id = 1902473845, Type = ObjectDataType.String, Value = value};
        }

        public bool IsTechtreeRequirementsModified => _modifications.ContainsKey(1902473845);
        public IEnumerable<Tech> TechtreeRequirements
        {
            get => TechtreeRequirementsRaw.ToIEnumerableTech(this);
            set => TechtreeRequirementsRaw = value.ToRaw(null, null);
        }

        public string TechtreeRequirementsTier2Raw
        {
            get => _modifications.GetModification(829518453).ValueAsString;
            set => _modifications[829518453] = new SimpleObjectDataModification{Id = 829518453, Type = ObjectDataType.String, Value = value};
        }

        public bool IsTechtreeRequirementsTier2Modified => _modifications.ContainsKey(829518453);
        public IEnumerable<Tech> TechtreeRequirementsTier2
        {
            get => TechtreeRequirementsTier2Raw.ToIEnumerableTech(this);
            set => TechtreeRequirementsTier2Raw = value.ToRaw(null, null);
        }

        public string TechtreeRequirementsTier3Raw
        {
            get => _modifications.GetModification(846295669).ValueAsString;
            set => _modifications[846295669] = new SimpleObjectDataModification{Id = 846295669, Type = ObjectDataType.String, Value = value};
        }

        public bool IsTechtreeRequirementsTier3Modified => _modifications.ContainsKey(846295669);
        public IEnumerable<Tech> TechtreeRequirementsTier3
        {
            get => TechtreeRequirementsTier3Raw.ToIEnumerableTech(this);
            set => TechtreeRequirementsTier3Raw = value.ToRaw(null, null);
        }

        public string TechtreeRequirementsTier4Raw
        {
            get => _modifications.GetModification(863072885).ValueAsString;
            set => _modifications[863072885] = new SimpleObjectDataModification{Id = 863072885, Type = ObjectDataType.String, Value = value};
        }

        public bool IsTechtreeRequirementsTier4Modified => _modifications.ContainsKey(863072885);
        public IEnumerable<Tech> TechtreeRequirementsTier4
        {
            get => TechtreeRequirementsTier4Raw.ToIEnumerableTech(this);
            set => TechtreeRequirementsTier4Raw = value.ToRaw(null, null);
        }

        public string TechtreeRequirementsTier5Raw
        {
            get => _modifications.GetModification(879850101).ValueAsString;
            set => _modifications[879850101] = new SimpleObjectDataModification{Id = 879850101, Type = ObjectDataType.String, Value = value};
        }

        public bool IsTechtreeRequirementsTier5Modified => _modifications.ContainsKey(879850101);
        public IEnumerable<Tech> TechtreeRequirementsTier5
        {
            get => TechtreeRequirementsTier5Raw.ToIEnumerableTech(this);
            set => TechtreeRequirementsTier5Raw = value.ToRaw(null, null);
        }

        public string TechtreeRequirementsTier6Raw
        {
            get => _modifications.GetModification(896627317).ValueAsString;
            set => _modifications[896627317] = new SimpleObjectDataModification{Id = 896627317, Type = ObjectDataType.String, Value = value};
        }

        public bool IsTechtreeRequirementsTier6Modified => _modifications.ContainsKey(896627317);
        public IEnumerable<Tech> TechtreeRequirementsTier6
        {
            get => TechtreeRequirementsTier6Raw.ToIEnumerableTech(this);
            set => TechtreeRequirementsTier6Raw = value.ToRaw(null, null);
        }

        public string TechtreeRequirementsTier7Raw
        {
            get => _modifications.GetModification(913404533).ValueAsString;
            set => _modifications[913404533] = new SimpleObjectDataModification{Id = 913404533, Type = ObjectDataType.String, Value = value};
        }

        public bool IsTechtreeRequirementsTier7Modified => _modifications.ContainsKey(913404533);
        public IEnumerable<Tech> TechtreeRequirementsTier7
        {
            get => TechtreeRequirementsTier7Raw.ToIEnumerableTech(this);
            set => TechtreeRequirementsTier7Raw = value.ToRaw(null, null);
        }

        public string TechtreeRequirementsTier8Raw
        {
            get => _modifications.GetModification(930181749).ValueAsString;
            set => _modifications[930181749] = new SimpleObjectDataModification{Id = 930181749, Type = ObjectDataType.String, Value = value};
        }

        public bool IsTechtreeRequirementsTier8Modified => _modifications.ContainsKey(930181749);
        public IEnumerable<Tech> TechtreeRequirementsTier8
        {
            get => TechtreeRequirementsTier8Raw.ToIEnumerableTech(this);
            set => TechtreeRequirementsTier8Raw = value.ToRaw(null, null);
        }

        public string TechtreeRequirementsTier9Raw
        {
            get => _modifications.GetModification(946958965).ValueAsString;
            set => _modifications[946958965] = new SimpleObjectDataModification{Id = 946958965, Type = ObjectDataType.String, Value = value};
        }

        public bool IsTechtreeRequirementsTier9Modified => _modifications.ContainsKey(946958965);
        public IEnumerable<Tech> TechtreeRequirementsTier9
        {
            get => TechtreeRequirementsTier9Raw.ToIEnumerableTech(this);
            set => TechtreeRequirementsTier9Raw = value.ToRaw(null, null);
        }

        public string TechtreeRequirementsLevelsRaw
        {
            get => _modifications.GetModification(1634824821).ValueAsString;
            set => _modifications[1634824821] = new SimpleObjectDataModification{Id = 1634824821, Type = ObjectDataType.String, Value = value};
        }

        public bool IsTechtreeRequirementsLevelsModified => _modifications.ContainsKey(1634824821);
        public IEnumerable<int> TechtreeRequirementsLevels
        {
            get => TechtreeRequirementsLevelsRaw.ToIEnumerableInt(this);
            set => TechtreeRequirementsLevelsRaw = value.ToRaw(null, null);
        }

        public string TechtreeResearchesAvailableRaw
        {
            get => _modifications.GetModification(1936028277).ValueAsString;
            set => _modifications[1936028277] = new SimpleObjectDataModification{Id = 1936028277, Type = ObjectDataType.String, Value = value};
        }

        public bool IsTechtreeResearchesAvailableModified => _modifications.ContainsKey(1936028277);
        public IEnumerable<Upgrade> TechtreeResearchesAvailable
        {
            get => TechtreeResearchesAvailableRaw.ToIEnumerableUpgrade(this);
            set => TechtreeResearchesAvailableRaw = value.ToRaw(null, null);
        }

        public int TechtreeRevivesDeadHeroesRaw
        {
            get => _modifications.GetModification(1986359925).ValueAsInt;
            set => _modifications[1986359925] = new SimpleObjectDataModification{Id = 1986359925, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsTechtreeRevivesDeadHeroesModified => _modifications.ContainsKey(1986359925);
        public bool TechtreeRevivesDeadHeroes
        {
            get => TechtreeRevivesDeadHeroesRaw.ToBool(this);
            set => TechtreeRevivesDeadHeroesRaw = value.ToRaw(null, null);
        }

        public string TextTooltipRevive
        {
            get => _modifications.GetModification(1919972469).ValueAsString;
            set => _modifications[1919972469] = new SimpleObjectDataModification{Id = 1919972469, Type = ObjectDataType.String, Value = value};
        }

        public bool IsTextTooltipReviveModified => _modifications.ContainsKey(1919972469);
        public string ArtIconScoreScreenRaw
        {
            get => _modifications.GetModification(1769173877).ValueAsString;
            set => _modifications[1769173877] = new SimpleObjectDataModification{Id = 1769173877, Type = ObjectDataType.String, Value = value};
        }

        public bool IsArtIconScoreScreenModified => _modifications.ContainsKey(1769173877);
        public string ArtIconScoreScreen
        {
            get => ArtIconScoreScreenRaw.ToString(this);
            set => ArtIconScoreScreenRaw = value.ToRaw(null, null);
        }

        public string TechtreeItemsSoldRaw
        {
            get => _modifications.GetModification(1768256373).ValueAsString;
            set => _modifications[1768256373] = new SimpleObjectDataModification{Id = 1768256373, Type = ObjectDataType.String, Value = value};
        }

        public bool IsTechtreeItemsSoldModified => _modifications.ContainsKey(1768256373);
        public IEnumerable<Item> TechtreeItemsSold
        {
            get => TechtreeItemsSoldRaw.ToIEnumerableItem(this);
            set => TechtreeItemsSoldRaw = value.ToRaw(null, 12);
        }

        public string TechtreeUnitsSoldRaw
        {
            get => _modifications.GetModification(1969582965).ValueAsString;
            set => _modifications[1969582965] = new SimpleObjectDataModification{Id = 1969582965, Type = ObjectDataType.String, Value = value};
        }

        public bool IsTechtreeUnitsSoldModified => _modifications.ContainsKey(1969582965);
        public IEnumerable<Unit> TechtreeUnitsSold
        {
            get => TechtreeUnitsSoldRaw.ToIEnumerableUnit(this);
            set => TechtreeUnitsSoldRaw = value.ToRaw(null, 12);
        }

        public string ArtSpecialRaw
        {
            get => _modifications.GetModification(1634759541).ValueAsString;
            set => _modifications[1634759541] = new SimpleObjectDataModification{Id = 1634759541, Type = ObjectDataType.String, Value = value};
        }

        public bool IsArtSpecialModified => _modifications.ContainsKey(1634759541);
        public IEnumerable<string> ArtSpecial
        {
            get => ArtSpecialRaw.ToIEnumerableString(this);
            set => ArtSpecialRaw = value.ToRaw(null, null);
        }

        public string ArtTargetRaw
        {
            get => _modifications.GetModification(1633776757).ValueAsString;
            set => _modifications[1633776757] = new SimpleObjectDataModification{Id = 1633776757, Type = ObjectDataType.String, Value = value};
        }

        public bool IsArtTargetModified => _modifications.ContainsKey(1633776757);
        public IEnumerable<string> ArtTarget
        {
            get => ArtTargetRaw.ToIEnumerableString(this);
            set => ArtTargetRaw = value.ToRaw(null, null);
        }

        public string TextTooltipBasic
        {
            get => _modifications.GetModification(1885959285).ValueAsString;
            set => _modifications[1885959285] = new SimpleObjectDataModification{Id = 1885959285, Type = ObjectDataType.String, Value = value};
        }

        public bool IsTextTooltipBasicModified => _modifications.ContainsKey(1885959285);
        public string TechtreeUnitsTrainedRaw
        {
            get => _modifications.GetModification(1634890869).ValueAsString;
            set => _modifications[1634890869] = new SimpleObjectDataModification{Id = 1634890869, Type = ObjectDataType.String, Value = value};
        }

        public bool IsTechtreeUnitsTrainedModified => _modifications.ContainsKey(1634890869);
        public IEnumerable<Unit> TechtreeUnitsTrained
        {
            get => TechtreeUnitsTrainedRaw.ToIEnumerableUnit(this);
            set => TechtreeUnitsTrainedRaw = value.ToRaw(null, null);
        }

        public string TechtreeHeroRevivalLocationsRaw
        {
            get => _modifications.GetModification(1635152501).ValueAsString;
            set => _modifications[1635152501] = new SimpleObjectDataModification{Id = 1635152501, Type = ObjectDataType.String, Value = value};
        }

        public bool IsTechtreeHeroRevivalLocationsModified => _modifications.ContainsKey(1635152501);
        public IEnumerable<Unit> TechtreeHeroRevivalLocations
        {
            get => TechtreeHeroRevivalLocationsRaw.ToIEnumerableUnit(this);
            set => TechtreeHeroRevivalLocationsRaw = value.ToRaw(null, null);
        }

        public string TextTooltipExtended
        {
            get => _modifications.GetModification(1651864693).ValueAsString;
            set => _modifications[1651864693] = new SimpleObjectDataModification{Id = 1651864693, Type = ObjectDataType.String, Value = value};
        }

        public bool IsTextTooltipExtendedModified => _modifications.ContainsKey(1651864693);
        public string TechtreeUpgradesToRaw
        {
            get => _modifications.GetModification(1953527157).ValueAsString;
            set => _modifications[1953527157] = new SimpleObjectDataModification{Id = 1953527157, Type = ObjectDataType.String, Value = value};
        }

        public bool IsTechtreeUpgradesToModified => _modifications.ContainsKey(1953527157);
        public IEnumerable<Unit> TechtreeUpgradesTo
        {
            get => TechtreeUpgradesToRaw.ToIEnumerableUnit(this);
            set => TechtreeUpgradesToRaw = value.ToRaw(null, 12);
        }

        public string AbilitiesNormalRaw
        {
            get => _modifications.GetModification(1768055157).ValueAsString;
            set => _modifications[1768055157] = new SimpleObjectDataModification{Id = 1768055157, Type = ObjectDataType.String, Value = value};
        }

        public bool IsAbilitiesNormalModified => _modifications.ContainsKey(1768055157);
        public IEnumerable<Ability> AbilitiesNormal
        {
            get => AbilitiesNormalRaw.ToIEnumerableAbility(this);
            set => AbilitiesNormalRaw = value.ToRaw(null, null);
        }

        public string AbilitiesDefaultActiveAbilityRaw
        {
            get => _modifications.GetModification(1633772661).ValueAsString;
            set => _modifications[1633772661] = new SimpleObjectDataModification{Id = 1633772661, Type = ObjectDataType.String, Value = value};
        }

        public bool IsAbilitiesDefaultActiveAbilityModified => _modifications.ContainsKey(1633772661);
        public Ability AbilitiesDefaultActiveAbility
        {
            get => AbilitiesDefaultActiveAbilityRaw.ToAbility(this);
            set => AbilitiesDefaultActiveAbilityRaw = value.ToRaw(null, null);
        }

        public string AbilitiesHeroRaw
        {
            get => _modifications.GetModification(1650550901).ValueAsString;
            set => _modifications[1650550901] = new SimpleObjectDataModification{Id = 1650550901, Type = ObjectDataType.String, Value = value};
        }

        public bool IsAbilitiesHeroModified => _modifications.ContainsKey(1650550901);
        public IEnumerable<Ability> AbilitiesHero
        {
            get => AbilitiesHeroRaw.ToIEnumerableAbility(this);
            set => AbilitiesHeroRaw = value.ToRaw(null, 5);
        }

        public int StatsStartingAgility
        {
            get => _modifications.GetModification(1768382837).ValueAsInt;
            set => _modifications[1768382837] = new SimpleObjectDataModification{Id = 1768382837, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsStatsStartingAgilityModified => _modifications.ContainsKey(1768382837);
        public float StatsAgilityPerLevel
        {
            get => _modifications.GetModification(1885823349).ValueAsFloat;
            set => _modifications[1885823349] = new SimpleObjectDataModification{Id = 1885823349, Type = ObjectDataType.Unreal, Value = value};
        }

        public bool IsStatsAgilityPerLevelModified => _modifications.ContainsKey(1885823349);
        public int StatsBuildTime
        {
            get => _modifications.GetModification(1684824693).ValueAsInt;
            set => _modifications[1684824693] = new SimpleObjectDataModification{Id = 1684824693, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsStatsBuildTimeModified => _modifications.ContainsKey(1684824693);
        public int StatsGoldBountyAwardedNumberOfDice
        {
            get => _modifications.GetModification(1768186485).ValueAsInt;
            set => _modifications[1768186485] = new SimpleObjectDataModification{Id = 1768186485, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsStatsGoldBountyAwardedNumberOfDiceModified => _modifications.ContainsKey(1768186485);
        public int StatsGoldBountyAwardedBase
        {
            get => _modifications.GetModification(1633837685).ValueAsInt;
            set => _modifications[1633837685] = new SimpleObjectDataModification{Id = 1633837685, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsStatsGoldBountyAwardedBaseModified => _modifications.ContainsKey(1633837685);
        public int StatsGoldBountyAwardedSidesPerDie
        {
            get => _modifications.GetModification(1769169525).ValueAsInt;
            set => _modifications[1769169525] = new SimpleObjectDataModification{Id = 1769169525, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsStatsGoldBountyAwardedSidesPerDieModified => _modifications.ContainsKey(1769169525);
        public int StatsLumberBountyAwardedNumberOfDice
        {
            get => _modifications.GetModification(1684171893).ValueAsInt;
            set => _modifications[1684171893] = new SimpleObjectDataModification{Id = 1684171893, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsStatsLumberBountyAwardedNumberOfDiceModified => _modifications.ContainsKey(1684171893);
        public int StatsLumberBountyAwardedBase
        {
            get => _modifications.GetModification(1633840245).ValueAsInt;
            set => _modifications[1633840245] = new SimpleObjectDataModification{Id = 1633840245, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsStatsLumberBountyAwardedBaseModified => _modifications.ContainsKey(1633840245);
        public int StatsLumberBountyAwardedSidesPerDie
        {
            get => _modifications.GetModification(1935830133).ValueAsInt;
            set => _modifications[1935830133] = new SimpleObjectDataModification{Id = 1935830133, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsStatsLumberBountyAwardedSidesPerDieModified => _modifications.ContainsKey(1935830133);
        public float PathingCollisionSize
        {
            get => _modifications.GetModification(1819239285).ValueAsFloat;
            set => _modifications[1819239285] = new SimpleObjectDataModification{Id = 1819239285, Type = ObjectDataType.Unreal, Value = value};
        }

        public bool IsPathingCollisionSizeModified => _modifications.ContainsKey(1819239285);
        public int CombatDefenseBase
        {
            get => _modifications.GetModification(1717920885).ValueAsInt;
            set => _modifications[1717920885] = new SimpleObjectDataModification{Id = 1717920885, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsCombatDefenseBaseModified => _modifications.ContainsKey(1717920885);
        public string CombatDefenseTypeRaw
        {
            get => _modifications.GetModification(2037671029).ValueAsString;
            set => _modifications[2037671029] = new SimpleObjectDataModification{Id = 2037671029, Type = ObjectDataType.String, Value = value};
        }

        public bool IsCombatDefenseTypeModified => _modifications.ContainsKey(2037671029);
        public DefenseType CombatDefenseType
        {
            get => CombatDefenseTypeRaw.ToDefenseType(this);
            set => CombatDefenseTypeRaw = value.ToRaw(null, null);
        }

        public int CombatDefenseUpgradeBonus
        {
            get => _modifications.GetModification(1886741621).ValueAsInt;
            set => _modifications[1886741621] = new SimpleObjectDataModification{Id = 1886741621, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsCombatDefenseUpgradeBonusModified => _modifications.ContainsKey(1886741621);
        public int StatsFoodProduced
        {
            get => _modifications.GetModification(1634559605).ValueAsInt;
            set => _modifications[1634559605] = new SimpleObjectDataModification{Id = 1634559605, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsStatsFoodProducedModified => _modifications.ContainsKey(1634559605);
        public int StatsFoodCost
        {
            get => _modifications.GetModification(1869571701).ValueAsInt;
            set => _modifications[1869571701] = new SimpleObjectDataModification{Id = 1869571701, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsStatsFoodCostModified => _modifications.ContainsKey(1869571701);
        public int StatsGoldCost
        {
            get => _modifications.GetModification(1819240309).ValueAsInt;
            set => _modifications[1819240309] = new SimpleObjectDataModification{Id = 1819240309, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsStatsGoldCostModified => _modifications.ContainsKey(1819240309);
        public int StatsRepairGoldCost
        {
            get => _modifications.GetModification(1919903605).ValueAsInt;
            set => _modifications[1919903605] = new SimpleObjectDataModification{Id = 1919903605, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsStatsRepairGoldCostModified => _modifications.ContainsKey(1919903605);
        public int StatsHitPointsMaximumBase
        {
            get => _modifications.GetModification(1836083317).ValueAsInt;
            set => _modifications[1836083317] = new SimpleObjectDataModification{Id = 1836083317, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsStatsHitPointsMaximumBaseModified => _modifications.ContainsKey(1836083317);
        public int StatsStartingIntelligence
        {
            get => _modifications.GetModification(1953393013).ValueAsInt;
            set => _modifications[1953393013] = new SimpleObjectDataModification{Id = 1953393013, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsStatsStartingIntelligenceModified => _modifications.ContainsKey(1953393013);
        public float StatsIntelligencePerLevel
        {
            get => _modifications.GetModification(1886284149).ValueAsFloat;
            set => _modifications[1886284149] = new SimpleObjectDataModification{Id = 1886284149, Type = ObjectDataType.Unreal, Value = value};
        }

        public bool IsStatsIntelligencePerLevelModified => _modifications.ContainsKey(1886284149);
        public int StatsIsABuildingRaw
        {
            get => _modifications.GetModification(1734632053).ValueAsInt;
            set => _modifications[1734632053] = new SimpleObjectDataModification{Id = 1734632053, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsStatsIsABuildingModified => _modifications.ContainsKey(1734632053);
        public bool StatsIsABuilding
        {
            get => StatsIsABuildingRaw.ToBool(this);
            set => StatsIsABuildingRaw = value.ToRaw(null, null);
        }

        public int StatsLevel
        {
            get => _modifications.GetModification(1986358389).ValueAsInt;
            set => _modifications[1986358389] = new SimpleObjectDataModification{Id = 1986358389, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsStatsLevelModified => _modifications.ContainsKey(1986358389);
        public int StatsLumberCost
        {
            get => _modifications.GetModification(1836412021).ValueAsInt;
            set => _modifications[1836412021] = new SimpleObjectDataModification{Id = 1836412021, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsStatsLumberCostModified => _modifications.ContainsKey(1836412021);
        public int StatsRepairLumberCost
        {
            get => _modifications.GetModification(1920298101).ValueAsInt;
            set => _modifications[1920298101] = new SimpleObjectDataModification{Id = 1920298101, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsStatsRepairLumberCostModified => _modifications.ContainsKey(1920298101);
        public int StatsManaInitialAmount
        {
            get => _modifications.GetModification(1768975733).ValueAsInt;
            set => _modifications[1768975733] = new SimpleObjectDataModification{Id = 1768975733, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsStatsManaInitialAmountModified => _modifications.ContainsKey(1768975733);
        public int StatsManaMaximum
        {
            get => _modifications.GetModification(1836084597).ValueAsInt;
            set => _modifications[1836084597] = new SimpleObjectDataModification{Id = 1836084597, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsStatsManaMaximumModified => _modifications.ContainsKey(1836084597);
        public int MovementSpeedMaximum
        {
            get => _modifications.GetModification(1935764853).ValueAsInt;
            set => _modifications[1935764853] = new SimpleObjectDataModification{Id = 1935764853, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsMovementSpeedMaximumModified => _modifications.ContainsKey(1935764853);
        public int MovementSpeedMinimum
        {
            get => _modifications.GetModification(1936289141).ValueAsInt;
            set => _modifications[1936289141] = new SimpleObjectDataModification{Id = 1936289141, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsMovementSpeedMinimumModified => _modifications.ContainsKey(1936289141);
        public int StatsNeutralBuildingValidAsRandomBuildingRaw
        {
            get => _modifications.GetModification(1919053429).ValueAsInt;
            set => _modifications[1919053429] = new SimpleObjectDataModification{Id = 1919053429, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsStatsNeutralBuildingValidAsRandomBuildingModified => _modifications.ContainsKey(1919053429);
        public bool StatsNeutralBuildingValidAsRandomBuilding
        {
            get => StatsNeutralBuildingValidAsRandomBuildingRaw.ToBool(this);
            set => StatsNeutralBuildingValidAsRandomBuildingRaw = value.ToRaw(null, null);
        }

        public int StatsSightRadiusNight
        {
            get => _modifications.GetModification(1852404597).ValueAsInt;
            set => _modifications[1852404597] = new SimpleObjectDataModification{Id = 1852404597, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsStatsSightRadiusNightModified => _modifications.ContainsKey(1852404597);
        public string PathingPlacementRequiresRaw
        {
            get => _modifications.GetModification(1885433973).ValueAsString;
            set => _modifications[1885433973] = new SimpleObjectDataModification{Id = 1885433973, Type = ObjectDataType.String, Value = value};
        }

        public bool IsPathingPlacementRequiresModified => _modifications.ContainsKey(1885433973);
        public IEnumerable<PathingType> PathingPlacementRequires
        {
            get => PathingPlacementRequiresRaw.ToIEnumerablePathingType(this);
            set => PathingPlacementRequiresRaw = value.ToRaw(null, null);
        }

        public string StatsPrimaryAttributeRaw
        {
            get => _modifications.GetModification(1634889845).ValueAsString;
            set => _modifications[1634889845] = new SimpleObjectDataModification{Id = 1634889845, Type = ObjectDataType.String, Value = value};
        }

        public bool IsStatsPrimaryAttributeModified => _modifications.ContainsKey(1634889845);
        public AttributeType StatsPrimaryAttribute
        {
            get => StatsPrimaryAttributeRaw.ToAttributeType(this);
            set => StatsPrimaryAttributeRaw = value.ToRaw(null, null);
        }

        public float StatsHitPointsRegenerationRate
        {
            get => _modifications.GetModification(1919969397).ValueAsFloat;
            set => _modifications[1919969397] = new SimpleObjectDataModification{Id = 1919969397, Type = ObjectDataType.Unreal, Value = value};
        }

        public bool IsStatsHitPointsRegenerationRateModified => _modifications.ContainsKey(1919969397);
        public float StatsManaRegeneration
        {
            get => _modifications.GetModification(1919970677).ValueAsFloat;
            set => _modifications[1919970677] = new SimpleObjectDataModification{Id = 1919970677, Type = ObjectDataType.Unreal, Value = value};
        }

        public bool IsStatsManaRegenerationModified => _modifications.ContainsKey(1919970677);
        public string StatsHitPointsRegenerationTypeRaw
        {
            get => _modifications.GetModification(1953654901).ValueAsString;
            set => _modifications[1953654901] = new SimpleObjectDataModification{Id = 1953654901, Type = ObjectDataType.String, Value = value};
        }

        public bool IsStatsHitPointsRegenerationTypeModified => _modifications.ContainsKey(1953654901);
        public RegenType StatsHitPointsRegenerationType
        {
            get => StatsHitPointsRegenerationTypeRaw.ToRegenType(this);
            set => StatsHitPointsRegenerationTypeRaw = value.ToRaw(null, null);
        }

        public int StatsRepairTime
        {
            get => _modifications.GetModification(1836348021).ValueAsInt;
            set => _modifications[1836348021] = new SimpleObjectDataModification{Id = 1836348021, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsStatsRepairTimeModified => _modifications.ContainsKey(1836348021);
        public int MovementGroupSeparationEnabledRaw
        {
            get => _modifications.GetModification(1869640309).ValueAsInt;
            set => _modifications[1869640309] = new SimpleObjectDataModification{Id = 1869640309, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsMovementGroupSeparationEnabledModified => _modifications.ContainsKey(1869640309);
        public bool MovementGroupSeparationEnabled
        {
            get => MovementGroupSeparationEnabledRaw.ToBool(this);
            set => MovementGroupSeparationEnabledRaw = value.ToRaw(null, null);
        }

        public int MovementGroupSeparationGroupNumber
        {
            get => _modifications.GetModification(1735422581).ValueAsInt;
            set => _modifications[1735422581] = new SimpleObjectDataModification{Id = 1735422581, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsMovementGroupSeparationGroupNumberModified => _modifications.ContainsKey(1735422581);
        public int MovementGroupSeparationParameter
        {
            get => _modifications.GetModification(1886417525).ValueAsInt;
            set => _modifications[1886417525] = new SimpleObjectDataModification{Id = 1886417525, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsMovementGroupSeparationParameterModified => _modifications.ContainsKey(1886417525);
        public int MovementGroupSeparationPriority
        {
            get => _modifications.GetModification(1919971957).ValueAsInt;
            set => _modifications[1919971957] = new SimpleObjectDataModification{Id = 1919971957, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsMovementGroupSeparationPriorityModified => _modifications.ContainsKey(1919971957);
        public string PathingPlacementPreventedByRaw
        {
            get => _modifications.GetModification(1918988405).ValueAsString;
            set => _modifications[1918988405] = new SimpleObjectDataModification{Id = 1918988405, Type = ObjectDataType.String, Value = value};
        }

        public bool IsPathingPlacementPreventedByModified => _modifications.ContainsKey(1918988405);
        public IEnumerable<PathingType> PathingPlacementPreventedBy
        {
            get => PathingPlacementPreventedByRaw.ToIEnumerablePathingType(this);
            set => PathingPlacementPreventedByRaw = value.ToRaw(null, null);
        }

        public int StatsSightRadiusDay
        {
            get => _modifications.GetModification(1684632437).ValueAsInt;
            set => _modifications[1684632437] = new SimpleObjectDataModification{Id = 1684632437, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsStatsSightRadiusDayModified => _modifications.ContainsKey(1684632437);
        public int MovementSpeedBase
        {
            get => _modifications.GetModification(1937141109).ValueAsInt;
            set => _modifications[1937141109] = new SimpleObjectDataModification{Id = 1937141109, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsMovementSpeedBaseModified => _modifications.ContainsKey(1937141109);
        public int StatsStockMaximum
        {
            get => _modifications.GetModification(1634562933).ValueAsInt;
            set => _modifications[1634562933] = new SimpleObjectDataModification{Id = 1634562933, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsStatsStockMaximumModified => _modifications.ContainsKey(1634562933);
        public int StatsStockReplenishInterval
        {
            get => _modifications.GetModification(1735553909).ValueAsInt;
            set => _modifications[1735553909] = new SimpleObjectDataModification{Id = 1735553909, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsStatsStockReplenishIntervalModified => _modifications.ContainsKey(1735553909);
        public int StatsStockStartDelay
        {
            get => _modifications.GetModification(1953723253).ValueAsInt;
            set => _modifications[1953723253] = new SimpleObjectDataModification{Id = 1953723253, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsStatsStockStartDelayModified => _modifications.ContainsKey(1953723253);
        public int StatsStockInitialAfterStartDelay
        {
            get => _modifications.GetModification(1953067893).ValueAsInt;
            set => _modifications[1953067893] = new SimpleObjectDataModification{Id = 1953067893, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsStatsStockInitialAfterStartDelayModified => _modifications.ContainsKey(1953067893);
        public int StatsStartingStrength
        {
            get => _modifications.GetModification(1920234357).ValueAsInt;
            set => _modifications[1920234357] = new SimpleObjectDataModification{Id = 1920234357, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsStatsStartingStrengthModified => _modifications.ContainsKey(1920234357);
        public float StatsStrengthPerLevel
        {
            get => _modifications.GetModification(1886679925).ValueAsFloat;
            set => _modifications[1886679925] = new SimpleObjectDataModification{Id = 1886679925, Type = ObjectDataType.Unreal, Value = value};
        }

        public bool IsStatsStrengthPerLevelModified => _modifications.ContainsKey(1886679925);
        public string EditorTilesetsRaw
        {
            get => _modifications.GetModification(1818850421).ValueAsString;
            set => _modifications[1818850421] = new SimpleObjectDataModification{Id = 1818850421, Type = ObjectDataType.String, Value = value};
        }

        public bool IsEditorTilesetsModified => _modifications.ContainsKey(1818850421);
        public IEnumerable<Tileset> EditorTilesets
        {
            get => EditorTilesetsRaw.ToIEnumerableTileset(this);
            set => EditorTilesetsRaw = value.ToRaw(null, null);
        }

        public string StatsUnitClassificationRaw
        {
            get => _modifications.GetModification(1887007861).ValueAsString;
            set => _modifications[1887007861] = new SimpleObjectDataModification{Id = 1887007861, Type = ObjectDataType.String, Value = value};
        }

        public bool IsStatsUnitClassificationModified => _modifications.ContainsKey(1887007861);
        public IEnumerable<UnitClassification> StatsUnitClassification
        {
            get => StatsUnitClassificationRaw.ToIEnumerableUnitClassification(this);
            set => StatsUnitClassificationRaw = value.ToRaw(null, null);
        }

        public string TechtreeUpgradesUsedRaw
        {
            get => _modifications.GetModification(1919381621).ValueAsString;
            set => _modifications[1919381621] = new SimpleObjectDataModification{Id = 1919381621, Type = ObjectDataType.String, Value = value};
        }

        public bool IsTechtreeUpgradesUsedModified => _modifications.ContainsKey(1919381621);
        public IEnumerable<Upgrade> TechtreeUpgradesUsed
        {
            get => TechtreeUpgradesUsedRaw.ToIEnumerableUpgrade(this);
            set => TechtreeUpgradesUsedRaw = value.ToRaw(null, null);
        }

        public float PathingAIPlacementRadius
        {
            get => _modifications.GetModification(1919050101).ValueAsFloat;
            set => _modifications[1919050101] = new SimpleObjectDataModification{Id = 1919050101, Type = ObjectDataType.Unreal, Value = value};
        }

        public bool IsPathingAIPlacementRadiusModified => _modifications.ContainsKey(1919050101);
        public string PathingAIPlacementTypeRaw
        {
            get => _modifications.GetModification(1952604533).ValueAsString;
            set => _modifications[1952604533] = new SimpleObjectDataModification{Id = 1952604533, Type = ObjectDataType.String, Value = value};
        }

        public bool IsPathingAIPlacementTypeModified => _modifications.ContainsKey(1952604533);
        public AiBuffer PathingAIPlacementType
        {
            get => PathingAIPlacementTypeRaw.ToAiBuffer(this);
            set => PathingAIPlacementTypeRaw = value.ToRaw(null, null);
        }

        public int StatsCanBuildOnRaw
        {
            get => _modifications.GetModification(1868718965).ValueAsInt;
            set => _modifications[1868718965] = new SimpleObjectDataModification{Id = 1868718965, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsStatsCanBuildOnModified => _modifications.ContainsKey(1868718965);
        public bool StatsCanBuildOn
        {
            get => StatsCanBuildOnRaw.ToBool(this);
            set => StatsCanBuildOnRaw = value.ToRaw(null, null);
        }

        public int StatsCanFleeRaw
        {
            get => _modifications.GetModification(1701602933).ValueAsInt;
            set => _modifications[1701602933] = new SimpleObjectDataModification{Id = 1701602933, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsStatsCanFleeModified => _modifications.ContainsKey(1701602933);
        public bool StatsCanFlee
        {
            get => StatsCanFleeRaw.ToBool(this);
            set => StatsCanFleeRaw = value.ToRaw(null, null);
        }

        public int StatsSleepsRaw
        {
            get => _modifications.GetModification(1701606261).ValueAsInt;
            set => _modifications[1701606261] = new SimpleObjectDataModification{Id = 1701606261, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsStatsSleepsModified => _modifications.ContainsKey(1701606261);
        public bool StatsSleeps
        {
            get => StatsSleepsRaw.ToBool(this);
            set => StatsSleepsRaw = value.ToRaw(null, null);
        }

        public int StatsTransportedSize
        {
            get => _modifications.GetModification(1918985077).ValueAsInt;
            set => _modifications[1918985077] = new SimpleObjectDataModification{Id = 1918985077, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsStatsTransportedSizeModified => _modifications.ContainsKey(1918985077);
        public float ArtDeathTimeseconds
        {
            get => _modifications.GetModification(1836344437).ValueAsFloat;
            set => _modifications[1836344437] = new SimpleObjectDataModification{Id = 1836344437, Type = ObjectDataType.Unreal, Value = value};
        }

        public bool IsArtDeathTimesecondsModified => _modifications.ContainsKey(1836344437);
        public int CombatDeathTypeRaw
        {
            get => _modifications.GetModification(1634034805).ValueAsInt;
            set => _modifications[1634034805] = new SimpleObjectDataModification{Id = 1634034805, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsCombatDeathTypeModified => _modifications.ContainsKey(1634034805);
        public DeathType CombatDeathType
        {
            get => CombatDeathTypeRaw.ToDeathType(this);
            set => CombatDeathTypeRaw = value.ToRaw(null, null);
        }

        public int ArtUseExtendedLineOfSightRaw
        {
            get => _modifications.GetModification(1936682101).ValueAsInt;
            set => _modifications[1936682101] = new SimpleObjectDataModification{Id = 1936682101, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsArtUseExtendedLineOfSightModified => _modifications.ContainsKey(1936682101);
        public bool ArtUseExtendedLineOfSight
        {
            get => ArtUseExtendedLineOfSightRaw.ToBool(this);
            set => ArtUseExtendedLineOfSightRaw = value.ToRaw(null, null);
        }

        public int StatsFormationRank
        {
            get => _modifications.GetModification(1919903349).ValueAsInt;
            set => _modifications[1919903349] = new SimpleObjectDataModification{Id = 1919903349, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsStatsFormationRankModified => _modifications.ContainsKey(1919903349);
        public int StatsCanBeBuiltOnRaw
        {
            get => _modifications.GetModification(1868720501).ValueAsInt;
            set => _modifications[1868720501] = new SimpleObjectDataModification{Id = 1868720501, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsStatsCanBeBuiltOnModified => _modifications.ContainsKey(1868720501);
        public bool StatsCanBeBuiltOn
        {
            get => StatsCanBeBuiltOnRaw.ToBool(this);
            set => StatsCanBeBuiltOnRaw = value.ToRaw(null, null);
        }

        public float MovementHeightMinimum
        {
            get => _modifications.GetModification(1719037301).ValueAsFloat;
            set => _modifications[1719037301] = new SimpleObjectDataModification{Id = 1719037301, Type = ObjectDataType.Unreal, Value = value};
        }

        public bool IsMovementHeightMinimumModified => _modifications.ContainsKey(1719037301);
        public float MovementHeight
        {
            get => _modifications.GetModification(1752591733).ValueAsFloat;
            set => _modifications[1752591733] = new SimpleObjectDataModification{Id = 1752591733, Type = ObjectDataType.Unreal, Value = value};
        }

        public bool IsMovementHeightModified => _modifications.ContainsKey(1752591733);
        public string MovementTypeRaw
        {
            get => _modifications.GetModification(1953918325).ValueAsString;
            set => _modifications[1953918325] = new SimpleObjectDataModification{Id = 1953918325, Type = ObjectDataType.String, Value = value};
        }

        public bool IsMovementTypeModified => _modifications.ContainsKey(1953918325);
        public MoveType MovementType
        {
            get => MovementTypeRaw.ToMoveType(this);
            set => MovementTypeRaw = value.ToRaw(null, null);
        }

        public int TextProperNamesUsed
        {
            get => _modifications.GetModification(1970434165).ValueAsInt;
            set => _modifications[1970434165] = new SimpleObjectDataModification{Id = 1970434165, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsTextProperNamesUsedModified => _modifications.ContainsKey(1970434165);
        public int ArtOrientationInterpolation
        {
            get => _modifications.GetModification(1769107317).ValueAsInt;
            set => _modifications[1769107317] = new SimpleObjectDataModification{Id = 1769107317, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsArtOrientationInterpolationModified => _modifications.ContainsKey(1769107317);
        public string PathingPathingMapRaw
        {
            get => _modifications.GetModification(1952542837).ValueAsString;
            set => _modifications[1952542837] = new SimpleObjectDataModification{Id = 1952542837, Type = ObjectDataType.String, Value = value};
        }

        public bool IsPathingPathingMapModified => _modifications.ContainsKey(1952542837);
        public string PathingPathingMap
        {
            get => PathingPathingMapRaw.ToString(this);
            set => PathingPathingMapRaw = value.ToRaw(null, null);
        }

        public int StatsPointValue
        {
            get => _modifications.GetModification(1768910965).ValueAsInt;
            set => _modifications[1768910965] = new SimpleObjectDataModification{Id = 1768910965, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsStatsPointValueModified => _modifications.ContainsKey(1768910965);
        public int StatsPriority
        {
            get => _modifications.GetModification(1769107573).ValueAsInt;
            set => _modifications[1769107573] = new SimpleObjectDataModification{Id = 1769107573, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsStatsPriorityModified => _modifications.ContainsKey(1769107573);
        public float ArtPropulsionWindowdegrees
        {
            get => _modifications.GetModification(2003988597).ValueAsFloat;
            set => _modifications[2003988597] = new SimpleObjectDataModification{Id = 2003988597, Type = ObjectDataType.Unreal, Value = value};
        }

        public bool IsArtPropulsionWindowdegreesModified => _modifications.ContainsKey(2003988597);
        public string StatsRaceRaw
        {
            get => _modifications.GetModification(1667330677).ValueAsString;
            set => _modifications[1667330677] = new SimpleObjectDataModification{Id = 1667330677, Type = ObjectDataType.String, Value = value};
        }

        public bool IsStatsRaceModified => _modifications.ContainsKey(1667330677);
        public UnitRace StatsRace
        {
            get => StatsRaceRaw.ToUnitRace(this);
            set => StatsRaceRaw = value.ToRaw(null, null);
        }

        public float PathingPlacementRequiresWaterRadius
        {
            get => _modifications.GetModification(2002874485).ValueAsFloat;
            set => _modifications[2002874485] = new SimpleObjectDataModification{Id = 2002874485, Type = ObjectDataType.Unreal, Value = value};
        }

        public bool IsPathingPlacementRequiresWaterRadiusModified => _modifications.ContainsKey(2002874485);
        public string CombatTargetedAsRaw
        {
            get => _modifications.GetModification(1918989429).ValueAsString;
            set => _modifications[1918989429] = new SimpleObjectDataModification{Id = 1918989429, Type = ObjectDataType.String, Value = value};
        }

        public bool IsCombatTargetedAsModified => _modifications.ContainsKey(1918989429);
        public IEnumerable<Target> CombatTargetedAs
        {
            get => CombatTargetedAsRaw.ToIEnumerableTarget(this);
            set => CombatTargetedAsRaw = value.ToRaw(null, null);
        }

        public float MovementTurnRate
        {
            get => _modifications.GetModification(1920363893).ValueAsFloat;
            set => _modifications[1920363893] = new SimpleObjectDataModification{Id = 1920363893, Type = ObjectDataType.Unreal, Value = value};
        }

        public bool IsMovementTurnRateModified => _modifications.ContainsKey(1920363893);
        public string CombatArmorTypeRaw
        {
            get => _modifications.GetModification(1836212597).ValueAsString;
            set => _modifications[1836212597] = new SimpleObjectDataModification{Id = 1836212597, Type = ObjectDataType.String, Value = value};
        }

        public bool IsCombatArmorTypeModified => _modifications.ContainsKey(1836212597);
        public ArmorType CombatArmorType
        {
            get => CombatArmorTypeRaw.ToArmorType(this);
            set => CombatArmorTypeRaw = value.ToRaw(null, null);
        }

        public float ArtAnimationBlendTimeseconds
        {
            get => _modifications.GetModification(1701601909).ValueAsFloat;
            set => _modifications[1701601909] = new SimpleObjectDataModification{Id = 1701601909, Type = ObjectDataType.Real, Value = value};
        }

        public bool IsArtAnimationBlendTimesecondsModified => _modifications.ContainsKey(1701601909);
        public int ArtTintingColor3Blue
        {
            get => _modifications.GetModification(1651270517).ValueAsInt;
            set => _modifications[1651270517] = new SimpleObjectDataModification{Id = 1651270517, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsArtTintingColor3BlueModified => _modifications.ContainsKey(1651270517);
        public string ArtShadowTextureBuildingRaw
        {
            get => _modifications.GetModification(1651012469).ValueAsString;
            set => _modifications[1651012469] = new SimpleObjectDataModification{Id = 1651012469, Type = ObjectDataType.String, Value = value};
        }

        public bool IsArtShadowTextureBuildingModified => _modifications.ContainsKey(1651012469);
        public string ArtShadowTextureBuilding
        {
            get => ArtShadowTextureBuildingRaw.ToString(this);
            set => ArtShadowTextureBuildingRaw = value.ToRaw(null, null);
        }

        public int EditorCategorizationCampaignRaw
        {
            get => _modifications.GetModification(1835098997).ValueAsInt;
            set => _modifications[1835098997] = new SimpleObjectDataModification{Id = 1835098997, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsEditorCategorizationCampaignModified => _modifications.ContainsKey(1835098997);
        public bool EditorCategorizationCampaign
        {
            get => EditorCategorizationCampaignRaw.ToBool(this);
            set => EditorCategorizationCampaignRaw = value.ToRaw(null, null);
        }

        public int ArtAllowCustomTeamColorRaw
        {
            get => _modifications.GetModification(1667462261).ValueAsInt;
            set => _modifications[1667462261] = new SimpleObjectDataModification{Id = 1667462261, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsArtAllowCustomTeamColorModified => _modifications.ContainsKey(1667462261);
        public bool ArtAllowCustomTeamColor
        {
            get => ArtAllowCustomTeamColorRaw.ToBool(this);
            set => ArtAllowCustomTeamColorRaw = value.ToRaw(null, null);
        }

        public int EditorCanDropItemsOnDeathRaw
        {
            get => _modifications.GetModification(1869767797).ValueAsInt;
            set => _modifications[1869767797] = new SimpleObjectDataModification{Id = 1869767797, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsEditorCanDropItemsOnDeathModified => _modifications.ContainsKey(1869767797);
        public bool EditorCanDropItemsOnDeath
        {
            get => EditorCanDropItemsOnDeathRaw.ToBool(this);
            set => EditorCanDropItemsOnDeathRaw = value.ToRaw(null, null);
        }

        public int ArtElevationSamplePoints
        {
            get => _modifications.GetModification(1953523061).ValueAsInt;
            set => _modifications[1953523061] = new SimpleObjectDataModification{Id = 1953523061, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsArtElevationSamplePointsModified => _modifications.ContainsKey(1953523061);
        public float ArtElevationSampleRadius
        {
            get => _modifications.GetModification(1685218677).ValueAsFloat;
            set => _modifications[1685218677] = new SimpleObjectDataModification{Id = 1685218677, Type = ObjectDataType.Real, Value = value};
        }

        public bool IsArtElevationSampleRadiusModified => _modifications.ContainsKey(1685218677);
        public string ArtModelFileRaw
        {
            get => _modifications.GetModification(1818520949).ValueAsString;
            set => _modifications[1818520949] = new SimpleObjectDataModification{Id = 1818520949, Type = ObjectDataType.String, Value = value};
        }

        public bool IsArtModelFileModified => _modifications.ContainsKey(1818520949);
        public string ArtModelFile
        {
            get => ArtModelFileRaw.ToString(this);
            set => ArtModelFileRaw = value.ToRaw(null, null);
        }

        public int ArtModelFileExtraVersionsRaw
        {
            get => _modifications.GetModification(1919252085).ValueAsInt;
            set => _modifications[1919252085] = new SimpleObjectDataModification{Id = 1919252085, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsArtModelFileExtraVersionsModified => _modifications.ContainsKey(1919252085);
        public VersionFlags ArtModelFileExtraVersions
        {
            get => ArtModelFileExtraVersionsRaw.ToVersionFlags(this);
            set => ArtModelFileExtraVersionsRaw = value.ToRaw(0, 3);
        }

        public float ArtFogOfWarSampleRadius
        {
            get => _modifications.GetModification(1685218933).ValueAsFloat;
            set => _modifications[1685218933] = new SimpleObjectDataModification{Id = 1685218933, Type = ObjectDataType.Real, Value = value};
        }

        public bool IsArtFogOfWarSampleRadiusModified => _modifications.ContainsKey(1685218933);
        public int ArtTintingColor2Green
        {
            get => _modifications.GetModification(1735156597).ValueAsInt;
            set => _modifications[1735156597] = new SimpleObjectDataModification{Id = 1735156597, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsArtTintingColor2GreenModified => _modifications.ContainsKey(1735156597);
        public int EditorDisplayAsNeutralHostileRaw
        {
            get => _modifications.GetModification(1936681077).ValueAsInt;
            set => _modifications[1936681077] = new SimpleObjectDataModification{Id = 1936681077, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsEditorDisplayAsNeutralHostileModified => _modifications.ContainsKey(1936681077);
        public bool EditorDisplayAsNeutralHostile
        {
            get => EditorDisplayAsNeutralHostileRaw.ToBool(this);
            set => EditorDisplayAsNeutralHostileRaw = value.ToRaw(null, null);
        }

        public int EditorPlaceableInEditorRaw
        {
            get => _modifications.GetModification(1701734773).ValueAsInt;
            set => _modifications[1701734773] = new SimpleObjectDataModification{Id = 1701734773, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsEditorPlaceableInEditorModified => _modifications.ContainsKey(1701734773);
        public bool EditorPlaceableInEditor
        {
            get => EditorPlaceableInEditorRaw.ToBool(this);
            set => EditorPlaceableInEditorRaw = value.ToRaw(null, null);
        }

        public float ArtMaximumPitchAngledegrees
        {
            get => _modifications.GetModification(1886940533).ValueAsFloat;
            set => _modifications[1886940533] = new SimpleObjectDataModification{Id = 1886940533, Type = ObjectDataType.Real, Value = value};
        }

        public bool IsArtMaximumPitchAngledegreesModified => _modifications.ContainsKey(1886940533);
        public float ArtMaximumRollAngledegrees
        {
            get => _modifications.GetModification(1920494965).ValueAsFloat;
            set => _modifications[1920494965] = new SimpleObjectDataModification{Id = 1920494965, Type = ObjectDataType.Real, Value = value};
        }

        public bool IsArtMaximumRollAngledegreesModified => _modifications.ContainsKey(1920494965);
        public float ArtScalingValue
        {
            get => _modifications.GetModification(1633907573).ValueAsFloat;
            set => _modifications[1633907573] = new SimpleObjectDataModification{Id = 1633907573, Type = ObjectDataType.Real, Value = value};
        }

        public bool IsArtScalingValueModified => _modifications.ContainsKey(1633907573);
        public int StatsNeutralBuildingShowsMinimapIconRaw
        {
            get => _modifications.GetModification(1835167349).ValueAsInt;
            set => _modifications[1835167349] = new SimpleObjectDataModification{Id = 1835167349, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsStatsNeutralBuildingShowsMinimapIconModified => _modifications.ContainsKey(1835167349);
        public bool StatsNeutralBuildingShowsMinimapIcon
        {
            get => StatsNeutralBuildingShowsMinimapIconRaw.ToBool(this);
            set => StatsNeutralBuildingShowsMinimapIconRaw = value.ToRaw(null, null);
        }

        public int StatsHeroHideHeroInterfaceIconRaw
        {
            get => _modifications.GetModification(1651009653).ValueAsInt;
            set => _modifications[1651009653] = new SimpleObjectDataModification{Id = 1651009653, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsStatsHeroHideHeroInterfaceIconModified => _modifications.ContainsKey(1651009653);
        public bool StatsHeroHideHeroInterfaceIcon
        {
            get => StatsHeroHideHeroInterfaceIconRaw.ToBool(this);
            set => StatsHeroHideHeroInterfaceIconRaw = value.ToRaw(null, null);
        }

        public int StatsHeroHideHeroMinimapDisplayRaw
        {
            get => _modifications.GetModification(1835559029).ValueAsInt;
            set => _modifications[1835559029] = new SimpleObjectDataModification{Id = 1835559029, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsStatsHeroHideHeroMinimapDisplayModified => _modifications.ContainsKey(1835559029);
        public bool StatsHeroHideHeroMinimapDisplay
        {
            get => StatsHeroHideHeroMinimapDisplayRaw.ToBool(this);
            set => StatsHeroHideHeroMinimapDisplayRaw = value.ToRaw(null, null);
        }

        public int StatsHeroHideHeroDeathMessageRaw
        {
            get => _modifications.GetModification(1684564085).ValueAsInt;
            set => _modifications[1684564085] = new SimpleObjectDataModification{Id = 1684564085, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsStatsHeroHideHeroDeathMessageModified => _modifications.ContainsKey(1684564085);
        public bool StatsHeroHideHeroDeathMessage
        {
            get => StatsHeroHideHeroDeathMessageRaw.ToBool(this);
            set => StatsHeroHideHeroDeathMessageRaw = value.ToRaw(null, null);
        }

        public int StatsHideMinimapDisplayRaw
        {
            get => _modifications.GetModification(1836017781).ValueAsInt;
            set => _modifications[1836017781] = new SimpleObjectDataModification{Id = 1836017781, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsStatsHideMinimapDisplayModified => _modifications.ContainsKey(1836017781);
        public bool StatsHideMinimapDisplay
        {
            get => StatsHideMinimapDisplayRaw.ToBool(this);
            set => StatsHideMinimapDisplayRaw = value.ToRaw(null, null);
        }

        public float ArtOccluderHeight
        {
            get => _modifications.GetModification(1667460981).ValueAsFloat;
            set => _modifications[1667460981] = new SimpleObjectDataModification{Id = 1667460981, Type = ObjectDataType.Unreal, Value = value};
        }

        public bool IsArtOccluderHeightModified => _modifications.ContainsKey(1667460981);
        public int ArtTintingColor1Red
        {
            get => _modifications.GetModification(1919705973).ValueAsInt;
            set => _modifications[1919705973] = new SimpleObjectDataModification{Id = 1919705973, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsArtTintingColor1RedModified => _modifications.ContainsKey(1919705973);
        public float ArtAnimationRunSpeed
        {
            get => _modifications.GetModification(1853190773).ValueAsFloat;
            set => _modifications[1853190773] = new SimpleObjectDataModification{Id = 1853190773, Type = ObjectDataType.Real, Value = value};
        }

        public bool IsArtAnimationRunSpeedModified => _modifications.ContainsKey(1853190773);
        public float ArtSelectionScale
        {
            get => _modifications.GetModification(1668510581).ValueAsFloat;
            set => _modifications[1668510581] = new SimpleObjectDataModification{Id = 1668510581, Type = ObjectDataType.Real, Value = value};
        }

        public bool IsArtSelectionScaleModified => _modifications.ContainsKey(1668510581);
        public int ArtScaleProjectilesRaw
        {
            get => _modifications.GetModification(1650684789).ValueAsInt;
            set => _modifications[1650684789] = new SimpleObjectDataModification{Id = 1650684789, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsArtScaleProjectilesModified => _modifications.ContainsKey(1650684789);
        public bool ArtScaleProjectiles
        {
            get => ArtScaleProjectilesRaw.ToBool(this);
            set => ArtScaleProjectilesRaw = value.ToRaw(null, null);
        }

        public int ArtSelectionCircleOnWaterRaw
        {
            get => _modifications.GetModification(2003137397).ValueAsInt;
            set => _modifications[2003137397] = new SimpleObjectDataModification{Id = 2003137397, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsArtSelectionCircleOnWaterModified => _modifications.ContainsKey(2003137397);
        public bool ArtSelectionCircleOnWater
        {
            get => ArtSelectionCircleOnWaterRaw.ToBool(this);
            set => ArtSelectionCircleOnWaterRaw = value.ToRaw(null, null);
        }

        public float ArtSelectionCircleHeight
        {
            get => _modifications.GetModification(2053927797).ValueAsFloat;
            set => _modifications[2053927797] = new SimpleObjectDataModification{Id = 2053927797, Type = ObjectDataType.Real, Value = value};
        }

        public bool IsArtSelectionCircleHeightModified => _modifications.ContainsKey(2053927797);
        public float ArtShadowImageHeight
        {
            get => _modifications.GetModification(1751675765).ValueAsFloat;
            set => _modifications[1751675765] = new SimpleObjectDataModification{Id = 1751675765, Type = ObjectDataType.Real, Value = value};
        }

        public bool IsArtShadowImageHeightModified => _modifications.ContainsKey(1751675765);
        public int ArtHasWaterShadowRaw
        {
            get => _modifications.GetModification(1919447925).ValueAsInt;
            set => _modifications[1919447925] = new SimpleObjectDataModification{Id = 1919447925, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsArtHasWaterShadowModified => _modifications.ContainsKey(1919447925);
        public bool ArtHasWaterShadow
        {
            get => ArtHasWaterShadowRaw.ToBool(this);
            set => ArtHasWaterShadowRaw = value.ToRaw(null, null);
        }

        public float ArtShadowImageWidth
        {
            get => _modifications.GetModification(2003334005).ValueAsFloat;
            set => _modifications[2003334005] = new SimpleObjectDataModification{Id = 2003334005, Type = ObjectDataType.Real, Value = value};
        }

        public bool IsArtShadowImageWidthModified => _modifications.ContainsKey(2003334005);
        public float ArtShadowImageCenterX
        {
            get => _modifications.GetModification(2020111221).ValueAsFloat;
            set => _modifications[2020111221] = new SimpleObjectDataModification{Id = 2020111221, Type = ObjectDataType.Real, Value = value};
        }

        public bool IsArtShadowImageCenterXModified => _modifications.ContainsKey(2020111221);
        public float ArtShadowImageCenterY
        {
            get => _modifications.GetModification(2036888437).ValueAsFloat;
            set => _modifications[2036888437] = new SimpleObjectDataModification{Id = 2036888437, Type = ObjectDataType.Real, Value = value};
        }

        public bool IsArtShadowImageCenterYModified => _modifications.ContainsKey(2036888437);
        public int EditorCategorizationSpecialRaw
        {
            get => _modifications.GetModification(1701868405).ValueAsInt;
            set => _modifications[1701868405] = new SimpleObjectDataModification{Id = 1701868405, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsEditorCategorizationSpecialModified => _modifications.ContainsKey(1701868405);
        public bool EditorCategorizationSpecial
        {
            get => EditorCategorizationSpecialRaw.ToBool(this);
            set => EditorCategorizationSpecialRaw = value.ToRaw(null, null);
        }

        public int ArtTeamColorRaw
        {
            get => _modifications.GetModification(1868788853).ValueAsInt;
            set => _modifications[1868788853] = new SimpleObjectDataModification{Id = 1868788853, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsArtTeamColorModified => _modifications.ContainsKey(1868788853);
        public TeamColor ArtTeamColor
        {
            get => ArtTeamColorRaw.ToTeamColor(this);
            set => ArtTeamColorRaw = value.ToRaw(null, null);
        }

        public int EditorHasTilesetSpecificDataRaw
        {
            get => _modifications.GetModification(1936946293).ValueAsInt;
            set => _modifications[1936946293] = new SimpleObjectDataModification{Id = 1936946293, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsEditorHasTilesetSpecificDataModified => _modifications.ContainsKey(1936946293);
        public bool EditorHasTilesetSpecificData
        {
            get => EditorHasTilesetSpecificDataRaw.ToBool(this);
            set => EditorHasTilesetSpecificDataRaw = value.ToRaw(null, null);
        }

        public string ArtGroundTextureRaw
        {
            get => _modifications.GetModification(1935832437).ValueAsString;
            set => _modifications[1935832437] = new SimpleObjectDataModification{Id = 1935832437, Type = ObjectDataType.String, Value = value};
        }

        public bool IsArtGroundTextureModified => _modifications.ContainsKey(1935832437);
        public string ArtGroundTexture
        {
            get => ArtGroundTextureRaw.ToString(this);
            set => ArtGroundTextureRaw = value.ToRaw(null, null);
        }

        public string ArtShadowImageUnitRaw
        {
            get => _modifications.GetModification(1969779573).ValueAsString;
            set => _modifications[1969779573] = new SimpleObjectDataModification{Id = 1969779573, Type = ObjectDataType.String, Value = value};
        }

        public bool IsArtShadowImageUnitModified => _modifications.ContainsKey(1969779573);
        public ShadowImage ArtShadowImageUnit
        {
            get => ArtShadowImageUnitRaw.ToShadowImage(this);
            set => ArtShadowImageUnitRaw = value.ToRaw(null, null);
        }

        public string SoundUnitSoundSetRaw
        {
            get => _modifications.GetModification(1684960117).ValueAsString;
            set => _modifications[1684960117] = new SimpleObjectDataModification{Id = 1684960117, Type = ObjectDataType.String, Value = value};
        }

        public bool IsSoundUnitSoundSetModified => _modifications.ContainsKey(1684960117);
        public string SoundUnitSoundSet
        {
            get => SoundUnitSoundSetRaw.ToString(this);
            set => SoundUnitSoundSetRaw = value.ToRaw(null, null);
        }

        public int EditorUseClickHelperRaw
        {
            get => _modifications.GetModification(1751348597).ValueAsInt;
            set => _modifications[1751348597] = new SimpleObjectDataModification{Id = 1751348597, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsEditorUseClickHelperModified => _modifications.ContainsKey(1751348597);
        public bool EditorUseClickHelper
        {
            get => EditorUseClickHelperRaw.ToBool(this);
            set => EditorUseClickHelperRaw = value.ToRaw(null, null);
        }

        public float ArtAnimationWalkSpeed
        {
            get => _modifications.GetModification(1818326901).ValueAsFloat;
            set => _modifications[1818326901] = new SimpleObjectDataModification{Id = 1818326901, Type = ObjectDataType.Real, Value = value};
        }

        public bool IsArtAnimationWalkSpeedModified => _modifications.ContainsKey(1818326901);
        public float CombatAcquisitionRange
        {
            get => _modifications.GetModification(1902338421).ValueAsFloat;
            set => _modifications[1902338421] = new SimpleObjectDataModification{Id = 1902338421, Type = ObjectDataType.Unreal, Value = value};
        }

        public bool IsCombatAcquisitionRangeModified => _modifications.ContainsKey(1902338421);
        public string CombatAttack1AttackTypeRaw
        {
            get => _modifications.GetModification(1949393269).ValueAsString;
            set => _modifications[1949393269] = new SimpleObjectDataModification{Id = 1949393269, Type = ObjectDataType.String, Value = value};
        }

        public bool IsCombatAttack1AttackTypeModified => _modifications.ContainsKey(1949393269);
        public AttackType CombatAttack1AttackType
        {
            get => CombatAttack1AttackTypeRaw.ToAttackType(this);
            set => CombatAttack1AttackTypeRaw = value.ToRaw(null, null);
        }

        public string CombatAttack2AttackTypeRaw
        {
            get => _modifications.GetModification(1949458805).ValueAsString;
            set => _modifications[1949458805] = new SimpleObjectDataModification{Id = 1949458805, Type = ObjectDataType.String, Value = value};
        }

        public bool IsCombatAttack2AttackTypeModified => _modifications.ContainsKey(1949458805);
        public AttackType CombatAttack2AttackType
        {
            get => CombatAttack2AttackTypeRaw.ToAttackType(this);
            set => CombatAttack2AttackTypeRaw = value.ToRaw(null, null);
        }

        public float CombatAttack1AnimationBackswingPoint
        {
            get => _modifications.GetModification(829645429).ValueAsFloat;
            set => _modifications[829645429] = new SimpleObjectDataModification{Id = 829645429, Type = ObjectDataType.Unreal, Value = value};
        }

        public bool IsCombatAttack1AnimationBackswingPointModified => _modifications.ContainsKey(829645429);
        public float CombatAttack2AnimationBackswingPoint
        {
            get => _modifications.GetModification(846422645).ValueAsFloat;
            set => _modifications[846422645] = new SimpleObjectDataModification{Id = 846422645, Type = ObjectDataType.Unreal, Value = value};
        }

        public bool IsCombatAttack2AnimationBackswingPointModified => _modifications.ContainsKey(846422645);
        public float ArtAnimationCastBackswing
        {
            get => _modifications.GetModification(1935827829).ValueAsFloat;
            set => _modifications[1935827829] = new SimpleObjectDataModification{Id = 1935827829, Type = ObjectDataType.Unreal, Value = value};
        }

        public bool IsArtAnimationCastBackswingModified => _modifications.ContainsKey(1935827829);
        public float ArtAnimationCastPoint
        {
            get => _modifications.GetModification(1953522549).ValueAsFloat;
            set => _modifications[1953522549] = new SimpleObjectDataModification{Id = 1953522549, Type = ObjectDataType.Unreal, Value = value};
        }

        public bool IsArtAnimationCastPointModified => _modifications.ContainsKey(1953522549);
        public float CombatAttack1CooldownTime
        {
            get => _modifications.GetModification(1664180597).ValueAsFloat;
            set => _modifications[1664180597] = new SimpleObjectDataModification{Id = 1664180597, Type = ObjectDataType.Unreal, Value = value};
        }

        public bool IsCombatAttack1CooldownTimeModified => _modifications.ContainsKey(1664180597);
        public float CombatAttack2CooldownTime
        {
            get => _modifications.GetModification(1664246133).ValueAsFloat;
            set => _modifications[1664246133] = new SimpleObjectDataModification{Id = 1664246133, Type = ObjectDataType.Unreal, Value = value};
        }

        public bool IsCombatAttack2CooldownTimeModified => _modifications.ContainsKey(1664246133);
        public float CombatAttack1DamageLossFactor
        {
            get => _modifications.GetModification(829187189).ValueAsFloat;
            set => _modifications[829187189] = new SimpleObjectDataModification{Id = 829187189, Type = ObjectDataType.Unreal, Value = value};
        }

        public bool IsCombatAttack1DamageLossFactorModified => _modifications.ContainsKey(829187189);
        public float CombatAttack2DamageLossFactor
        {
            get => _modifications.GetModification(845964405).ValueAsFloat;
            set => _modifications[845964405] = new SimpleObjectDataModification{Id = 845964405, Type = ObjectDataType.Unreal, Value = value};
        }

        public bool IsCombatAttack2DamageLossFactorModified => _modifications.ContainsKey(845964405);
        public int CombatAttack1DamageNumberOfDice
        {
            get => _modifications.GetModification(1680957813).ValueAsInt;
            set => _modifications[1680957813] = new SimpleObjectDataModification{Id = 1680957813, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsCombatAttack1DamageNumberOfDiceModified => _modifications.ContainsKey(1680957813);
        public int CombatAttack2DamageNumberOfDice
        {
            get => _modifications.GetModification(1681023349).ValueAsInt;
            set => _modifications[1681023349] = new SimpleObjectDataModification{Id = 1681023349, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsCombatAttack2DamageNumberOfDiceModified => _modifications.ContainsKey(1681023349);
        public int CombatAttack1DamageBase
        {
            get => _modifications.GetModification(1647403381).ValueAsInt;
            set => _modifications[1647403381] = new SimpleObjectDataModification{Id = 1647403381, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsCombatAttack1DamageBaseModified => _modifications.ContainsKey(1647403381);
        public int CombatAttack2DamageBase
        {
            get => _modifications.GetModification(1647468917).ValueAsInt;
            set => _modifications[1647468917] = new SimpleObjectDataModification{Id = 1647468917, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsCombatAttack2DamageBaseModified => _modifications.ContainsKey(1647468917);
        public float CombatAttack1AnimationDamagePoint
        {
            get => _modifications.GetModification(829449333).ValueAsFloat;
            set => _modifications[829449333] = new SimpleObjectDataModification{Id = 829449333, Type = ObjectDataType.Unreal, Value = value};
        }

        public bool IsCombatAttack1AnimationDamagePointModified => _modifications.ContainsKey(829449333);
        public float CombatAttack2AnimationDamagePoint
        {
            get => _modifications.GetModification(846226549).ValueAsFloat;
            set => _modifications[846226549] = new SimpleObjectDataModification{Id = 846226549, Type = ObjectDataType.Unreal, Value = value};
        }

        public bool IsCombatAttack2AnimationDamagePointModified => _modifications.ContainsKey(846226549);
        public int CombatAttack1DamageUpgradeAmount
        {
            get => _modifications.GetModification(829777013).ValueAsInt;
            set => _modifications[829777013] = new SimpleObjectDataModification{Id = 829777013, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsCombatAttack1DamageUpgradeAmountModified => _modifications.ContainsKey(829777013);
        public int CombatAttack2DamageUpgradeAmount
        {
            get => _modifications.GetModification(846554229).ValueAsInt;
            set => _modifications[846554229] = new SimpleObjectDataModification{Id = 846554229, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsCombatAttack2DamageUpgradeAmountModified => _modifications.ContainsKey(846554229);
        public int CombatAttack1AreaOfEffectFullDamage
        {
            get => _modifications.GetModification(1714512245).ValueAsInt;
            set => _modifications[1714512245] = new SimpleObjectDataModification{Id = 1714512245, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsCombatAttack1AreaOfEffectFullDamageModified => _modifications.ContainsKey(1714512245);
        public int CombatAttack2AreaOfEffectFullDamage
        {
            get => _modifications.GetModification(1714577781).ValueAsInt;
            set => _modifications[1714577781] = new SimpleObjectDataModification{Id = 1714577781, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsCombatAttack2AreaOfEffectFullDamageModified => _modifications.ContainsKey(1714577781);
        public int CombatAttack1AreaOfEffectMediumDamage
        {
            get => _modifications.GetModification(1748066677).ValueAsInt;
            set => _modifications[1748066677] = new SimpleObjectDataModification{Id = 1748066677, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsCombatAttack1AreaOfEffectMediumDamageModified => _modifications.ContainsKey(1748066677);
        public int CombatAttack2AreaOfEffectMediumDamage
        {
            get => _modifications.GetModification(1748132213).ValueAsInt;
            set => _modifications[1748132213] = new SimpleObjectDataModification{Id = 1748132213, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsCombatAttack2AreaOfEffectMediumDamageModified => _modifications.ContainsKey(1748132213);
        public float CombatAttack1DamageFactorMedium
        {
            get => _modifications.GetModification(828663925).ValueAsFloat;
            set => _modifications[828663925] = new SimpleObjectDataModification{Id = 828663925, Type = ObjectDataType.Unreal, Value = value};
        }

        public bool IsCombatAttack1DamageFactorMediumModified => _modifications.ContainsKey(828663925);
        public float CombatAttack2DamageFactorMedium
        {
            get => _modifications.GetModification(845441141).ValueAsFloat;
            set => _modifications[845441141] = new SimpleObjectDataModification{Id = 845441141, Type = ObjectDataType.Unreal, Value = value};
        }

        public bool IsCombatAttack2DamageFactorMediumModified => _modifications.ContainsKey(845441141);
        public float ArtProjectileImpactZSwimming
        {
            get => _modifications.GetModification(2054383989).ValueAsFloat;
            set => _modifications[2054383989] = new SimpleObjectDataModification{Id = 2054383989, Type = ObjectDataType.Unreal, Value = value};
        }

        public bool IsArtProjectileImpactZSwimmingModified => _modifications.ContainsKey(2054383989);
        public float ArtProjectileImpactZ
        {
            get => _modifications.GetModification(2053990773).ValueAsFloat;
            set => _modifications[2053990773] = new SimpleObjectDataModification{Id = 2053990773, Type = ObjectDataType.Unreal, Value = value};
        }

        public bool IsArtProjectileImpactZModified => _modifications.ContainsKey(2053990773);
        public float ArtProjectileLaunchZSwimming
        {
            get => _modifications.GetModification(2054384757).ValueAsFloat;
            set => _modifications[2054384757] = new SimpleObjectDataModification{Id = 2054384757, Type = ObjectDataType.Unreal, Value = value};
        }

        public bool IsArtProjectileLaunchZSwimmingModified => _modifications.ContainsKey(2054384757);
        public float ArtProjectileLaunchX
        {
            get => _modifications.GetModification(2020633717).ValueAsFloat;
            set => _modifications[2020633717] = new SimpleObjectDataModification{Id = 2020633717, Type = ObjectDataType.Unreal, Value = value};
        }

        public bool IsArtProjectileLaunchXModified => _modifications.ContainsKey(2020633717);
        public float ArtProjectileLaunchY
        {
            get => _modifications.GetModification(2037410933).ValueAsFloat;
            set => _modifications[2037410933] = new SimpleObjectDataModification{Id = 2037410933, Type = ObjectDataType.Unreal, Value = value};
        }

        public bool IsArtProjectileLaunchYModified => _modifications.ContainsKey(2037410933);
        public float ArtProjectileLaunchZ
        {
            get => _modifications.GetModification(2054188149).ValueAsFloat;
            set => _modifications[2054188149] = new SimpleObjectDataModification{Id = 2054188149, Type = ObjectDataType.Unreal, Value = value};
        }

        public bool IsArtProjectileLaunchZModified => _modifications.ContainsKey(2054188149);
        public int CombatMinimumAttackRange
        {
            get => _modifications.GetModification(1852662133).ValueAsInt;
            set => _modifications[1852662133] = new SimpleObjectDataModification{Id = 1852662133, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsCombatMinimumAttackRangeModified => _modifications.ContainsKey(1852662133);
        public int CombatAttack1AreaOfEffectSmallDamage
        {
            get => _modifications.GetModification(1899061621).ValueAsInt;
            set => _modifications[1899061621] = new SimpleObjectDataModification{Id = 1899061621, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsCombatAttack1AreaOfEffectSmallDamageModified => _modifications.ContainsKey(1899061621);
        public int CombatAttack2AreaOfEffectSmallDamage
        {
            get => _modifications.GetModification(1899127157).ValueAsInt;
            set => _modifications[1899127157] = new SimpleObjectDataModification{Id = 1899127157, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsCombatAttack2AreaOfEffectSmallDamageModified => _modifications.ContainsKey(1899127157);
        public float CombatAttack1DamageFactorSmall
        {
            get => _modifications.GetModification(828666229).ValueAsFloat;
            set => _modifications[828666229] = new SimpleObjectDataModification{Id = 828666229, Type = ObjectDataType.Unreal, Value = value};
        }

        public bool IsCombatAttack1DamageFactorSmallModified => _modifications.ContainsKey(828666229);
        public float CombatAttack2DamageFactorSmall
        {
            get => _modifications.GetModification(845443445).ValueAsFloat;
            set => _modifications[845443445] = new SimpleObjectDataModification{Id = 845443445, Type = ObjectDataType.Unreal, Value = value};
        }

        public bool IsCombatAttack2DamageFactorSmallModified => _modifications.ContainsKey(845443445);
        public int CombatAttack1Range
        {
            get => _modifications.GetModification(1915838837).ValueAsInt;
            set => _modifications[1915838837] = new SimpleObjectDataModification{Id = 1915838837, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsCombatAttack1RangeModified => _modifications.ContainsKey(1915838837);
        public int CombatAttack2Range
        {
            get => _modifications.GetModification(1915904373).ValueAsInt;
            set => _modifications[1915904373] = new SimpleObjectDataModification{Id = 1915904373, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsCombatAttack2RangeModified => _modifications.ContainsKey(1915904373);
        public float CombatAttack1RangeMotionBuffer
        {
            get => _modifications.GetModification(828535413).ValueAsFloat;
            set => _modifications[828535413] = new SimpleObjectDataModification{Id = 828535413, Type = ObjectDataType.Unreal, Value = value};
        }

        public bool IsCombatAttack1RangeMotionBufferModified => _modifications.ContainsKey(828535413);
        public float CombatAttack2RangeMotionBuffer
        {
            get => _modifications.GetModification(845312629).ValueAsFloat;
            set => _modifications[845312629] = new SimpleObjectDataModification{Id = 845312629, Type = ObjectDataType.Unreal, Value = value};
        }

        public bool IsCombatAttack2RangeMotionBufferModified => _modifications.ContainsKey(845312629);
        public int CombatAttack1ShowUIRaw
        {
            get => _modifications.GetModification(829781877).ValueAsInt;
            set => _modifications[829781877] = new SimpleObjectDataModification{Id = 829781877, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsCombatAttack1ShowUIModified => _modifications.ContainsKey(829781877);
        public bool CombatAttack1ShowUI
        {
            get => CombatAttack1ShowUIRaw.ToBool(this);
            set => CombatAttack1ShowUIRaw = value.ToRaw(null, null);
        }

        public int CombatAttack2ShowUIRaw
        {
            get => _modifications.GetModification(846559093).ValueAsInt;
            set => _modifications[846559093] = new SimpleObjectDataModification{Id = 846559093, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsCombatAttack2ShowUIModified => _modifications.ContainsKey(846559093);
        public bool CombatAttack2ShowUI
        {
            get => CombatAttack2ShowUIRaw.ToBool(this);
            set => CombatAttack2ShowUIRaw = value.ToRaw(null, null);
        }

        public int CombatAttack1DamageSidesPerDie
        {
            get => _modifications.GetModification(1932616053).ValueAsInt;
            set => _modifications[1932616053] = new SimpleObjectDataModification{Id = 1932616053, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsCombatAttack1DamageSidesPerDieModified => _modifications.ContainsKey(1932616053);
        public int CombatAttack2DamageSidesPerDie
        {
            get => _modifications.GetModification(1932681589).ValueAsInt;
            set => _modifications[1932681589] = new SimpleObjectDataModification{Id = 1932681589, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsCombatAttack2DamageSidesPerDieModified => _modifications.ContainsKey(1932681589);
        public float CombatAttack1DamageSpillDistance
        {
            get => _modifications.GetModification(828666741).ValueAsFloat;
            set => _modifications[828666741] = new SimpleObjectDataModification{Id = 828666741, Type = ObjectDataType.Unreal, Value = value};
        }

        public bool IsCombatAttack1DamageSpillDistanceModified => _modifications.ContainsKey(828666741);
        public float CombatAttack2DamageSpillDistance
        {
            get => _modifications.GetModification(845443957).ValueAsFloat;
            set => _modifications[845443957] = new SimpleObjectDataModification{Id = 845443957, Type = ObjectDataType.Unreal, Value = value};
        }

        public bool IsCombatAttack2DamageSpillDistanceModified => _modifications.ContainsKey(845443957);
        public float CombatAttack1DamageSpillRadius
        {
            get => _modifications.GetModification(829584245).ValueAsFloat;
            set => _modifications[829584245] = new SimpleObjectDataModification{Id = 829584245, Type = ObjectDataType.Unreal, Value = value};
        }

        public bool IsCombatAttack1DamageSpillRadiusModified => _modifications.ContainsKey(829584245);
        public float CombatAttack2DamageSpillRadius
        {
            get => _modifications.GetModification(846361461).ValueAsFloat;
            set => _modifications[846361461] = new SimpleObjectDataModification{Id = 846361461, Type = ObjectDataType.Unreal, Value = value};
        }

        public bool IsCombatAttack2DamageSpillRadiusModified => _modifications.ContainsKey(846361461);
        public string CombatAttack1AreaOfEffectTargetsRaw
        {
            get => _modifications.GetModification(1882284405).ValueAsString;
            set => _modifications[1882284405] = new SimpleObjectDataModification{Id = 1882284405, Type = ObjectDataType.String, Value = value};
        }

        public bool IsCombatAttack1AreaOfEffectTargetsModified => _modifications.ContainsKey(1882284405);
        public IEnumerable<Target> CombatAttack1AreaOfEffectTargets
        {
            get => CombatAttack1AreaOfEffectTargetsRaw.ToIEnumerableTarget(this);
            set => CombatAttack1AreaOfEffectTargetsRaw = value.ToRaw(null, null);
        }

        public string CombatAttack2AreaOfEffectTargetsRaw
        {
            get => _modifications.GetModification(1882349941).ValueAsString;
            set => _modifications[1882349941] = new SimpleObjectDataModification{Id = 1882349941, Type = ObjectDataType.String, Value = value};
        }

        public bool IsCombatAttack2AreaOfEffectTargetsModified => _modifications.ContainsKey(1882349941);
        public IEnumerable<Target> CombatAttack2AreaOfEffectTargets
        {
            get => CombatAttack2AreaOfEffectTargetsRaw.ToIEnumerableTarget(this);
            set => CombatAttack2AreaOfEffectTargetsRaw = value.ToRaw(null, null);
        }

        public int CombatAttack1MaximumNumberOfTargets
        {
            get => _modifications.GetModification(828601461).ValueAsInt;
            set => _modifications[828601461] = new SimpleObjectDataModification{Id = 828601461, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsCombatAttack1MaximumNumberOfTargetsModified => _modifications.ContainsKey(828601461);
        public int CombatAttack2MaximumNumberOfTargets
        {
            get => _modifications.GetModification(845378677).ValueAsInt;
            set => _modifications[845378677] = new SimpleObjectDataModification{Id = 845378677, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsCombatAttack2MaximumNumberOfTargetsModified => _modifications.ContainsKey(845378677);
        public string CombatAttack1TargetsAllowedRaw
        {
            get => _modifications.GetModification(1731289461).ValueAsString;
            set => _modifications[1731289461] = new SimpleObjectDataModification{Id = 1731289461, Type = ObjectDataType.String, Value = value};
        }

        public bool IsCombatAttack1TargetsAllowedModified => _modifications.ContainsKey(1731289461);
        public IEnumerable<Target> CombatAttack1TargetsAllowed
        {
            get => CombatAttack1TargetsAllowedRaw.ToIEnumerableTarget(this);
            set => CombatAttack1TargetsAllowedRaw = value.ToRaw(null, null);
        }

        public string CombatAttack2TargetsAllowedRaw
        {
            get => _modifications.GetModification(1731354997).ValueAsString;
            set => _modifications[1731354997] = new SimpleObjectDataModification{Id = 1731354997, Type = ObjectDataType.String, Value = value};
        }

        public bool IsCombatAttack2TargetsAllowedModified => _modifications.ContainsKey(1731354997);
        public IEnumerable<Target> CombatAttack2TargetsAllowed
        {
            get => CombatAttack2TargetsAllowedRaw.ToIEnumerableTarget(this);
            set => CombatAttack2TargetsAllowedRaw = value.ToRaw(null, null);
        }

        public int CombatAttacksEnabledRaw
        {
            get => _modifications.GetModification(1852137845).ValueAsInt;
            set => _modifications[1852137845] = new SimpleObjectDataModification{Id = 1852137845, Type = ObjectDataType.Int, Value = value};
        }

        public bool IsCombatAttacksEnabledModified => _modifications.ContainsKey(1852137845);
        public AttackBits CombatAttacksEnabled
        {
            get => CombatAttacksEnabledRaw.ToAttackBits(this);
            set => CombatAttacksEnabledRaw = value.ToRaw(null, null);
        }

        public string CombatAttack1WeaponTypeRaw
        {
            get => _modifications.GetModification(1999724917).ValueAsString;
            set => _modifications[1999724917] = new SimpleObjectDataModification{Id = 1999724917, Type = ObjectDataType.String, Value = value};
        }

        public bool IsCombatAttack1WeaponTypeModified => _modifications.ContainsKey(1999724917);
        public WeaponType CombatAttack1WeaponType
        {
            get => CombatAttack1WeaponTypeRaw.ToWeaponType(this);
            set => CombatAttack1WeaponTypeRaw = value.ToRaw(null, null);
        }

        public string CombatAttack2WeaponTypeRaw
        {
            get => _modifications.GetModification(1999790453).ValueAsString;
            set => _modifications[1999790453] = new SimpleObjectDataModification{Id = 1999790453, Type = ObjectDataType.String, Value = value};
        }

        public bool IsCombatAttack2WeaponTypeModified => _modifications.ContainsKey(1999790453);
        public WeaponType CombatAttack2WeaponType
        {
            get => CombatAttack2WeaponTypeRaw.ToWeaponType(this);
            set => CombatAttack2WeaponTypeRaw = value.ToRaw(null, null);
        }

        public string CombatAttack1WeaponSoundRaw
        {
            get => _modifications.GetModification(829645685).ValueAsString;
            set => _modifications[829645685] = new SimpleObjectDataModification{Id = 829645685, Type = ObjectDataType.String, Value = value};
        }

        public bool IsCombatAttack1WeaponSoundModified => _modifications.ContainsKey(829645685);
        public CombatSound CombatAttack1WeaponSound
        {
            get => CombatAttack1WeaponSoundRaw.ToCombatSound(this);
            set => CombatAttack1WeaponSoundRaw = value.ToRaw(null, null);
        }

        public string CombatAttack2WeaponSoundRaw
        {
            get => _modifications.GetModification(846422901).ValueAsString;
            set => _modifications[846422901] = new SimpleObjectDataModification{Id = 846422901, Type = ObjectDataType.String, Value = value};
        }

        public bool IsCombatAttack2WeaponSoundModified => _modifications.ContainsKey(846422901);
        public CombatSound CombatAttack2WeaponSound
        {
            get => CombatAttack2WeaponSoundRaw.ToCombatSound(this);
            set => CombatAttack2WeaponSoundRaw = value.ToRaw(null, null);
        }

        public string AbilitiesNormalSkinRaw
        {
            get => _modifications.GetModification(1935827317).ValueAsString;
            set => _modifications[1935827317] = new SimpleObjectDataModification{Id = 1935827317, Type = ObjectDataType.String, Value = value};
        }

        public bool IsAbilitiesNormalSkinModified => _modifications.ContainsKey(1935827317);
        public IEnumerable<Ability> AbilitiesNormalSkin
        {
            get => AbilitiesNormalSkinRaw.ToIEnumerableAbility(this);
            set => AbilitiesNormalSkinRaw = value.ToRaw(null, null);
        }

        public string AbilitiesHeroSkinRaw
        {
            get => _modifications.GetModification(1935763573).ValueAsString;
            set => _modifications[1935763573] = new SimpleObjectDataModification{Id = 1935763573, Type = ObjectDataType.String, Value = value};
        }

        public bool IsAbilitiesHeroSkinModified => _modifications.ContainsKey(1935763573);
        public IEnumerable<Ability> AbilitiesHeroSkin
        {
            get => AbilitiesHeroSkinRaw.ToIEnumerableAbility(this);
            set => AbilitiesHeroSkinRaw = value.ToRaw(null, null);
        }
    public string ArtPortraitModelFile
    {
      get => _modifications.GetModification(1919905909).ValueAsString;
      set => _modifications[1919905909] = new SimpleObjectDataModification { Id = 1919905909, Type = ObjectDataType.String, Value = value };
    }

    public bool IsArtPortraitModelFileModified => _modifications.ContainsKey(1919905909);



    public static explicit operator SimpleObjectModification(Unit unit) => new SimpleObjectModification{OldId = unit.OldId, NewId = unit.NewId, Modifications = unit.Modifications.ToList()};
        internal override bool TryGetSimpleModifications([NotNullWhen(true)] out SimpleObjectDataModifications? modifications)
        {
            modifications = _modifications;
            return true;
        }

        public void AddModifications(List<SimpleObjectDataModification> modifications)
        {
            foreach (var modification in modifications)
                _modifications[modification.Id] = modification;
        }
    }
}