using System.ComponentModel;
using System.Xml.Serialization;
using M2dXmlGenerator;
using Maple2.File.Parser.Xml.Skill.Property;
// ReSharper disable InconsistentNaming
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Maple2.File.Parser.Xml.Skill;

// ./data/xml/skill/%02d/%08u.xml
[XmlRoot("ms2")]
public partial class SkillData : IFeatureLocale {
    [M2dFeatureLocale] private Basic _basic;
    [M2dFeatureLocale(Selector = "value")] private IList<SkillLevelData> _level;
}

public partial class SkillLevelData : IFeatureLocale {
    [XmlAttribute] public short value;
    // KMS2/TMS2: the rate one hit of this skill adds to its stance (table/stance.xml damageSkillIDs).
    [XmlAttribute] public float stanceDamageRate;

    [XmlElement] public BeginCondition beginCondition;
    [XmlElement] public ChangeSkill changeSkill;
    [XmlElement] public MagicControl magicControl;
    [XmlElement] public AutoTargeting autoTargeting;
    [XmlElement] public Push push;
    [XmlElement] public Consume consume;
    [XmlElement] public TargetItem targetItem;
    [XmlElement] public Upgrade upgrade;
    [XmlElement] public RegionSkill detectProperty;
    [XmlElement] public RegionSkill sensorProperty;
    [XmlElement] public SplashManualActiveProperty splashManualActiveProperty;
    [XmlElement] public SkillGlideData glideProperty;
    [XmlElement] public Combo combo;
    [XmlElement] public RecoveryProperty recoveryProperty;
    [XmlElement] public List<TriggerSkill> conditionSkill;
    [XmlElement] public List<SkillMotionData> motion;
}

public class SkillGlideData {
    [XmlAttribute] public float gravity;
    [XmlAttribute] public float heightLimit;
    [XmlAttribute] public float horizontalAccelerate;
    [XmlAttribute] public float horizontalVelocity;
    [XmlAttribute] public float verticalAccelerate;
    [XmlAttribute] public float verticalVelocity;
    [XmlAttribute] public float verticalVibrateAmplitude;
    [XmlAttribute] public float verticalVibrateFrequency;
    [XmlAttribute] public float glideVerticalAccelerateLimit;
    [XmlAttribute] public float glideHorizontalAccelerateLimit;
    [XmlAttribute] public bool bEffect;
    [XmlAttribute] public string effectRun = string.Empty;
    [XmlAttribute] public string effectIdle = string.Empty;
    [XmlAttribute] public string aniIdle = string.Empty;
    [XmlAttribute] public string aniLeft = string.Empty;
    [XmlAttribute] public string aniRight = string.Empty;
    [XmlAttribute] public string aniRun = string.Empty;
    [XmlAttribute] public bool onGroundDisable;
}

public class SkillMotionData {
    [XmlElement] public SkillMotionProperty motionProperty;
    [XmlElement] public List<SkillAttackData> attack;
}

public partial class SkillMotionProperty {
    [XmlAttribute] public string sequenceName = string.Empty;
    [XmlAttribute] public string motionEffect = string.Empty;
    [XmlAttribute] public string loopEffect = string.Empty;
    [XmlAttribute] public string endEffect = string.Empty;
    [XmlAttribute] public string splashInvokeEffect = string.Empty;
    [XmlAttribute] public string splashEndEffect = string.Empty;
    [M2dArray] public string[] strTagEffects = Array.Empty<string>();
    [XmlAttribute] public string selfInputCombo = string.Empty;
    [XmlAttribute] public string weaponSequenceName = string.Empty;
    [XmlAttribute] public string loopKeepEffect = string.Empty;
    [XmlAttribute] public string loopEndEffect = string.Empty;
    [XmlAttribute] public string splashDurationCastEffect = string.Empty;
    [XmlAttribute] public bool IgnoreOptimalHideCastEffect;
    [XmlAttribute] public bool IgnoreOptimalHideEndEffect;
    [XmlAttribute] public bool IgnoreOptimalHideSplashInvokeEffect;
    [XmlAttribute] public bool IgnoreOptimalHideSplashEndEffect;
    [XmlAttribute] public bool splashCastEffectStartDelay;
    [XmlAttribute] public int splashLifeTick; // 0,6000,8000,10000,12000,14000,16000,18000,20000,22000,24000,26000,28000
    [XmlAttribute] public int splashInvokeCoolTick; // 0,100
    [XmlAttribute] public float sequenceSpeed;
    [XmlAttribute] public float keyMove;
    [XmlAttribute] public int movetype; // 0,3
    [XmlAttribute] public float moveangle;
    [XmlAttribute] public float movevangle;
    [XmlAttribute] public float movedistance;
    [XmlAttribute] public int moveEaseType; // 0,1,2
    [XmlAttribute] public float moveMid;
    [XmlAttribute] public float moveHeight;
    [XmlAttribute] public int hide; // 0
    [XmlAttribute] public int ignoreCollisionGroups; // 0,4,16,20
    [XmlAttribute] public bool holdWeapon;
    [XmlAttribute] public int loopCount;
    [XmlAttribute] public bool faceTarget;
    [XmlAttribute] public bool doesPlayIdleOnSkillEnd;
    [XmlAttribute] public bool startNoneBlendDelay;
    [XmlAttribute] public bool ignoreASP;
    [XmlAttribute] public float pushCylinderVelH;
    [XmlAttribute] public float pushCylinderVelV;
    [XmlAttribute] public float pushCylinderOuter;
    [XmlAttribute] public float pushCylinderInner;
    [XmlAttribute] public float pushCylinderHeight;
    [XmlAttribute] public float aniSkipTime;
}

public partial class SkillAttackData {
    [XmlAttribute] public string point = string.Empty;
    [XmlAttribute] public int pointGroupID;
    [XmlAttribute] public int targetCount; // 0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,20,21,22,26,30,50,52,80,99,100,200,9999
    [XmlAttribute] public int magicPathID;
    [XmlAttribute] public int cubeMagicPathID;
    [XmlAttribute] public int direction = 3; // 0,1,2,3,4
    [XmlAttribute] public int sequence;
    [XmlAttribute] public int attacktype; // 0,1,2,3
    [XmlAttribute] public int attackMethodType; // 0,1
    [XmlAttribute] public int hitImmuneBreak; // 0,1,2,3,5,99
    [XmlAttribute] public float brokenForce;
    [XmlAttribute] public int brokenOffence; // 0,1,2,3,4,5,10,25,50,53,55,60,61,63,65,66,67,70,71,75,76,80,81,85,86,88,90,91,92,93,95,96,97,99
    [XmlAttribute] public int unrideOnHit; // 0,1,2,5
    [XmlAttribute] public int releaseObjectWeaponOnHit = 1; // 0,1,2,3,5
    [M2dArray] public int[] compulsionType = Array.Empty<int>();
    [XmlAttribute] public int grabTargetType; // 0,1,2,3
    [M2dArray] public string[] grabNodeCategory = Array.Empty<string>();

    [XmlElement] public PetTamingProperty petTamingProperty;
    [XmlElement] public SplashManualActiveProperty splashManualActiveProperty;
    [XmlElement] public PauseProperty pauseProperty;
    [XmlElement] public RegionSkill rangeProperty;
    [XmlElement] public ChainProperty chainProperty;
    [XmlElement] public ArrowProperty arrowProperty;
    [XmlElement] public DamageProperty damageProperty;
    [XmlElement] public List<TriggerSkill> conditionSkill;

    // Ignored by client.
    [XmlAttribute] public string compulsionHit = string.Empty;
}

public partial class SkillAttackDataNew {
    [XmlAttribute] public string point = string.Empty;
    [XmlAttribute] public int pointGroupID;
    [XmlAttribute] public int targetCount; // 0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,20,21,22,26,30,50,52,80,99,100,200,9999
    [XmlAttribute] public int magicPathID;
    [XmlAttribute] public int cubeMagicPathID;
    [XmlAttribute] public int direction = 3; // 0,1,2,3,4
    [XmlAttribute] public int sequence;
    [XmlAttribute] public int attacktype; // 0,1,2,3
    [XmlAttribute] public int attackMethodType; // 0,1
    [XmlAttribute] public int hitImmuneBreak; // 0,1,2,3,5,99
    [XmlAttribute] public float brokenForce;
    [XmlAttribute] public int brokenOffence; // 0,1,2,3,4,5,10,25,50,53,55,60,61,63,65,66,67,70,71,75,76,80,81,85,86,88,90,91,92,93,95,96,97,99
    [XmlAttribute] public int unrideOnHit; // 0,1,2,5
    [XmlAttribute] public int releaseObjectWeaponOnHit = 1; // 0,1,2,3,5
    [M2dArray] public int[] compulsionType = Array.Empty<int>();
    [XmlAttribute] public int grabTargetType; // 0,1,2,3
    [M2dArray] public string[] grabNodeCategory = Array.Empty<string>();
    [XmlAttribute] public int splashActiveSkillID;
    [XmlElement] public PauseProperty pause;
    [XmlElement] public RegionSkillNew range;
    [XmlElement] public ChainProperty chain;
    [XmlElement] public ArrowProperty arrow;
    [XmlElement] public DamagePropertyNew damage;
    [XmlElement] public List<ActionAdditional> actionAdditional;
    [XmlElement] public List<ActionSkill> actionSkill;

    // Ignored by client.
    [XmlAttribute] public string compulsionHit = string.Empty;
}


// KR version: ./data/xml/skilldata/%04d.xml
[XmlRoot("ms2")]
public partial class SkillDataNew : IFeatureLocale {
    [XmlElement("skill")] public List<SkillNew> Skills;
}

public partial class SkillNew {
    [XmlAttribute] public int id;
    [XmlAttribute] public int originID = 0;
    [XmlAttribute] public string comment = string.Empty;
    [XmlElement("basic")] public BasicNew basic;
    [M2dFeatureLocale(Selector = "value")] private IList<SkillLevelDataNew> _level;
}

public partial class SkillLevelDataNew : IFeatureLocale {
    [XmlAttribute] public short value;

    [XmlAttribute] public int sp = 0;
    [XmlAttribute] public int ep = 0;
    [XmlAttribute] public float hpRate;
    [XmlAttribute] public int cooldown = 0;
    [XmlAttribute] public int meso;
    [XmlAttribute] public float stanceDamageRate;
    [XmlAttribute] public string stringParam = string.Empty;
    [XmlElement] public RegionSkillNew detectProperty;
    [XmlElement] public List<ActionAdditional> actionAdditional;
    [XmlElement] public List<SkillMotionDataNew> motion;
}

public partial class SkillMotionDataNew {
    [XmlAttribute] public string sequenceName = string.Empty;
    [XmlAttribute] public string motionEffect = string.Empty;
    [XmlAttribute] public string loopEffect = string.Empty;
    [XmlAttribute] public string endEffect = string.Empty;
    [XmlAttribute] public string splashInvokeEffect = string.Empty;
    [XmlAttribute] public string splashEndEffect = string.Empty;
    [M2dArray] public string[] strTagEffects = Array.Empty<string>();
    [XmlAttribute] public string selfInputCombo = string.Empty;
    [XmlAttribute] public string weaponSequenceName = string.Empty;
    [XmlAttribute] public string loopKeepEffect = string.Empty;
    [XmlAttribute] public string loopEndEffect = string.Empty;
    [XmlAttribute] public string splashDurationCastEffect = string.Empty;
    [XmlAttribute] public bool IgnoreOptimalHideCastEffect;
    // [XmlAttribute] public bool IgnoreOptimalHideEndEffect;
    // [XmlAttribute] public bool IgnoreOptimalHideSplashInvokeEffect;
    // [XmlAttribute] public bool IgnoreOptimalHideSplashEndEffect;
    [XmlAttribute] public bool splashCastEffectStartDelay;
    [XmlAttribute] public int splashLifeTick; // 0,6000,8000,10000,12000,14000,16000,18000,20000,22000,24000,26000,28000
    [XmlAttribute] public int splashInvokeCoolTick; // 0,100
    [XmlAttribute] public float sequenceSpeed;
    [XmlAttribute] public float keyMove;
    [XmlAttribute] public int movetype; // 0,3
    [XmlAttribute] public float moveangle;
    // [XmlAttribute] public float movevangle;
    [XmlAttribute] public float movedistance;
    [XmlAttribute] public int moveEaseType; // 0,1,2
    // [XmlAttribute] public float moveMid;
    // [XmlAttribute] public float moveHeight;
    // [XmlAttribute] public int hide; // 0
    [XmlAttribute] public int ignoreCollisionGroups; // 0,4,16,20
    [XmlAttribute] public bool holdWeapon;
    [XmlAttribute] public int loopCount;
    [XmlAttribute] public bool faceTarget;
    [XmlAttribute] public bool doesPlayIdleOnSkillEnd;
    [XmlAttribute] public bool startNoneBlendDelay;
    // [XmlAttribute] public bool ignoreASP;
    [XmlAttribute] public float pushCylinderVelH;
    [XmlAttribute] public float pushCylinderVelV;
    [XmlAttribute] public float pushCylinderOuter;
    [XmlAttribute] public float pushCylinderInner;
    [XmlAttribute] public float pushCylinderHeight;
    [XmlAttribute] public float aniSkipTime;
    [XmlElement] public List<SkillAttackDataNew> attack;
}
