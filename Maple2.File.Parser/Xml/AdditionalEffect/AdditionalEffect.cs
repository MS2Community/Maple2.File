using System.Xml.Serialization;
using Maple2.File.Parser.Xml.Skill;
using M2dXmlGenerator;

namespace Maple2.File.Parser.Xml.AdditionalEffect;

// ./data/xml/additionaleffect/%08d.xml
[XmlRoot("ms2")]
public partial class AdditionalEffectLevelData {
    [M2dFeatureLocale(Selector = "BasicProperty.level")] private IList<AdditionalEffectData> _level;
}

public partial class AdditionalEffectData : IFeatureLocale {
    [XmlElement] public BeginCondition beginCondition;
    [XmlElement] public BasicProperty BasicProperty;
    [XmlElement] public MotionProperty MotionProperty;
    [XmlElement] public CancelEffectProperty CancelEffectProperty;
    [XmlElement] public ImmuneEffectProperty ImmuneEffectProperty;
    [XmlElement] public ResetSkillCoolDownTimeProperty ResetSkillCoolDownTimeProperty;
    [XmlElement] public ModifyEffectDurationProperty ModifyEffectDurationProperty;
    [XmlElement] public ModifyOverlapCountProperty ModifyOverlapCountProperty;
    [XmlElement] public StatusProperty StatusProperty;
    [XmlElement] public FinalStatusProperty FinalStatusProperty;
    [XmlElement] public OffensiveProperty OffensiveProperty;
    [XmlElement] public DefensiveProperty DefensiveProperty;
    [XmlElement] public RecoveryProperty RecoveryProperty;
    [XmlElement] public ExpProperty ExpProperty;
    [XmlElement] public DotDamageProperty DotDamageProperty;
    [XmlElement] public DotBuffProperty DotBuffProperty;
    [XmlElement] public ConsumeProperty ConsumeProperty;
    [XmlElement] public ReflectProperty ReflectProperty;
    [XmlElement] public UIProperty UIProperty;
    [XmlElement] public ShieldProperty ShieldProperty;
    [XmlElement] public MesoGuardProperty MesoGuardProperty;
    [XmlElement] public InvokeEffectProperty InvokeEffectProperty;
    [XmlElement] public SpecialEffectProperty SpecialEffectProperty;
    [XmlElement] public RideeProperty RideeProperty;
    // KMS2/TMS2 only, the new format's ModifySkillCooldown under the XxxProperty naming. Absent in GMS2 data.
    [XmlElement] public ModifySkillCooldown ModifySkillCooldownProperty;
    // KMS2/TMS2 only, absent in GMS2 data. KMS2's own legacy export names the first SpreadProperty.
    [XmlElement] public Spread SpreadProperty;
    [XmlElement] public RemoveRechargingCount RemoveRechargingCountProperty;

    [XmlElement] public List<TriggerSkill> splashSkill;
    [XmlElement] public List<TriggerSkill> conditionSkill;
}

// ./data/xml/additional/%03d.xml
[XmlRoot("ms2")]
public partial class AdditionalEffectDataRootNew {
    [XmlElement] public List<AdditionalEffectLevelDataNew> additional;
}

public partial class AdditionalEffectLevelDataNew {
    [XmlAttribute] public int id;
    [XmlAttribute] public int skillGroupType = 0;
    [XmlAttribute] public int mainType = 0;
    [XmlAttribute] public bool isDebuff = false;
    [XmlAttribute] public int category;
    [M2dFeatureLocale(Selector = "level")] private IList<AdditionalEffectDataNew> _level;
}

public partial class AdditionalEffectDataNew : IFeatureLocale {
    [XmlAttribute] public short level = 1;
    [M2dArray] public string[] stringParam = Array.Empty<string>();
    [XmlElement] public Condition Condition;
    [XmlElement] public BasicProperty Basic;
    [XmlElement] public ModifySkillCooldown ModifySkillCooldown;
    [XmlElement] public MotionProperty Motion;
    [XmlElement] public CancelEffectProperty CancelEffect;
    [XmlElement] public ImmuneEffectProperty ImmuneEffect;
    [XmlElement] public ResetSkillCoolDownTimeProperty ResetSkillCoolDownTime;
    [XmlElement] public ModifyEffectDurationProperty ModifyEffectDuration;
    [XmlElement] public ModifyOverlapCountProperty ModifyOverlapCount;
    [XmlElement] public StatusPropertyNew Status;
    [XmlElement] public SpecialAbilityNew SpecialAbility;
    [XmlElement] public FinalStatusProperty FinalStatus;
    [XmlElement] public OffensiveProperty Offensive;
    [XmlElement] public DefensiveProperty Defensive;
    [XmlElement] public RecoveryProperty Recovery;
    [XmlElement] public ExpProperty Exp;
    [XmlElement] public DotDamageProperty DotDamage;
    [XmlElement] public DotBuffProperty DotBuff;
    [XmlElement] public ConsumeProperty Consume;
    [XmlElement] public ReflectProperty Reflect;
    [XmlElement] public UIProperty UI;
    [XmlElement] public RemoveRechargingCount RemoveRechargingCount;
    [XmlElement] public ShieldProperty Shield;
    [XmlElement] public MesoGuardProperty MesoGuard;
    [XmlElement] public InvokeEffectProperty InvokeEffect;
    [XmlElement] public SpecialEffectProperty SpecialEffect;
    [XmlElement] public RideeProperty Ridee;
    [XmlElement] public Spread Spread;

    [XmlElement] public List<ActionSkill> Skill;
    [XmlElement] public List<Additional> Additional;
}

public partial class Additional {
    [M2dArray] public int[] skillID = Array.Empty<int>();
    [M2dArray] public short[] level = Array.Empty<short>();
    [XmlAttribute] public int randomCast;
    [XmlAttribute] public int skillTarget;
    [XmlAttribute] public int ownerEvent;
    [XmlAttribute] public float probability;
    [XmlAttribute] public int ownerAdditionalID;
    [XmlAttribute] public int ownerAdditionalLevel;
    [XmlAttribute] public int ownerAdditionalCount;
    [M2dArray] public int[] ownerEventSkillID = Array.Empty<int>();
    [XmlAttribute] public bool ownerEventIgnore;
    [XmlAttribute] public bool activeByIntervalTick;
    [XmlAttribute] public bool ownerAdditionalMine;
    [XmlAttribute] public float targetHpLess;
    [XmlAttribute] public float ownerHpLess;
    [XmlAttribute] public float ownerSpLess;
    [XmlAttribute] public int ownerNoAdditionalID;
    [M2dArray] public int[] ownerEventAdditionalID = Array.Empty<int>();
    [XmlAttribute] public float ownerHpOver;
    [XmlAttribute] public int casterAdditionalID;
    [XmlAttribute] public int skillOwner;
    [XmlAttribute] public int targetAdditionalID;
    [XmlAttribute] public bool targetAdditionalMine;
    [XmlAttribute] public int targetNoAdditionalID;
    [XmlAttribute] public bool targetNpcOnly;
    [M2dArray] public int[] linkSkillID = Array.Empty<int>();
    [XmlAttribute] public bool isAllowDead;
    [XmlAttribute] public int ownerRange;
    [XmlAttribute] public int ownerRangeCountOver;
    [XmlAttribute] public int ownerRangeCountLess;
    [M2dArray] public int[] targetNpcIDs = Array.Empty<int>();
    [XmlAttribute] public int ownerEventNoSkillID;
    [XmlAttribute] public int casterEvent;
    [XmlAttribute] public int casterAdditionalCount;
}

public partial class Condition {
    [XmlAttribute] public bool onlyShadowWorld;
    [XmlAttribute] public bool allowBattleRidingState;
    [XmlAttribute] public bool onlyBattleRidingState;

    // Additional Effect ONLY
    [XmlAttribute] public int eventCondition;
    [XmlAttribute] public int equipHandR;
    [XmlAttribute] public int equipHandL;
    [XmlAttribute] public bool allowDeadState;
    [XmlAttribute] public int applyPvPZoneType;
    [XmlAttribute] public int hasNotBuffID;
    [XmlAttribute] public int hasBuffID;
    [XmlAttribute] public int hasBuffCount;
    [XmlAttribute] public bool checkActivateBuff;
    [XmlAttribute] public int hasBuffLevel;
    [M2dArray] public int[] usedSkillIDs = Array.Empty<int>();
    [M2dArray] public int[] fieldIDs = Array.Empty<int>();
    [M2dArray] public string[] requireStates = Array.Empty<string>();
    [M2dArray] public string[] requireSubStates = Array.Empty<string>();
    [M2dArray] public string[] requireMasteryTypes = Array.Empty<string>();
    [M2dArray] public int[] requireMasteryValues = Array.Empty<int>();
    [XmlAttribute] public int mapCategory;
    [XmlAttribute] public int mapContinent;
    [XmlAttribute] public string dungeonGroupType = string.Empty;
    [XmlAttribute] public float requireDurationWithoutMove;
    [XmlAttribute] public int jobCode;
    [XmlAttribute] public int targetAdditionalID;
    [XmlAttribute] public int targetCheckRange;
    [XmlAttribute] public int targetCheckMinRange;
    [XmlAttribute] public int targetFriendly;
    [XmlAttribute] public int targetInRangeCount;
    [XmlAttribute] public string targetCountSign = string.Empty;
    [XmlAttribute] public float casterHpRate;
    [XmlAttribute] public string casterHpOp = string.Empty;
    [XmlAttribute] public int casterAdditionalCount;
    [XmlAttribute] public int casterAdditionalID;
    [XmlElement] public HasNotAdditionalEffectGroup hasNotAdditionalEffectGroup;
    [XmlElement("owner")] public SubConditionTargetNew owner;

    // Skill ONLY
    [XmlAttribute] public int sp;
    [XmlAttribute] public bool useCoolDownShowUI;
    [XmlAttribute] public bool hasNotBuffOwner;
    [XmlAttribute] public bool invokeEffectFactor = true;
    [XmlElement] public List<WeaponNew> weapon;
    [XmlElement] public List<MapCodes> requireMapCodes;
    [XmlAttribute] public float defaultRechargingCooldownTime;
    [XmlAttribute] public bool onlyFlyableMap;
    [XmlAttribute] public float probability;
    [XmlAttribute] public int useTargetCountFactor;
    [XmlElement] public RequireEffect requireEffect;
    [XmlElement("caster")] public SubConditionTargetNew caster;
    [XmlElement("target")] public SubConditionTargetNew target;

    public class HasNotAdditionalEffectGroup {
        [XmlAttribute] public int id;
    }

    public class RequireEffect {
        [XmlAttribute] public int code;
    }

    public class WeaponNew {
        [XmlAttribute] public int lh;
        [XmlAttribute] public int rh;
    }

    public class MapCodes {
        [XmlAttribute] public int code;
    }
}
