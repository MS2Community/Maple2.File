using System.Xml.Serialization;
using M2dXmlGenerator;
using Maple2.File.Parser.Enum;
using DayOfWeek = Maple2.File.Parser.Enum.DayOfWeek;

namespace Maple2.File.Parser.Xml.Table;

// ./data/xml/table/na/dungeonroom.xml
[XmlRoot("ms2")]
public partial class DungeonRoomRoot {
    [M2dFeatureLocale(Selector = "dungeonRoomID")] private IList<DungeonRoom> _dungeonRoom;
}

public partial class DungeonRoom : IFeatureLocale {
    [XmlAttribute] public int dungeonRoomID;
    [XmlAttribute] public short dungeonLevel;
    [XmlAttribute] public DungeonPlayType playType;
    [M2dEnum] public DungeonGroupType groupType;
    [XmlAttribute] public int clearValue;
    [XmlAttribute] public DungeonCooldownType cooldownType;
    [XmlAttribute] public int cooldownValue;
    [XmlAttribute] public int sortOrder;
    [XmlAttribute] public int rewardCount;
    [XmlAttribute] public int subRewardCount;
    [XmlAttribute] public bool isAccountReward;
    [XmlAttribute] public int unionRewardID;
    [XmlAttribute] public int roundID;
    [XmlAttribute] public long rewardExp;
    [XmlAttribute] public float rewardExpRate;
    [XmlAttribute] public long rewardMeso;
    [XmlAttribute] public int scoreBonusId;
    [M2dArray] public int[] rewardLimitedDropBoxIds = Array.Empty<int>();
    //[M2dArray] public int[] rewardLimitedDropBoxIds2;
    [M2dArray] public int[] rewardUnlimitedDropBoxIds = Array.Empty<int>();
    [XmlAttribute] public int seasonRankRewardID;
    [XmlAttribute] public int lobbyFieldID;
    [M2dArray] public int[] fieldIDs = Array.Empty<int>();
    [XmlAttribute] public int durationTick;
    [XmlAttribute] public bool isExpireTimeOut;
    [XmlAttribute] public DungeonTimerType timerType;
    [XmlAttribute] public int minUserCount;
    [XmlAttribute] public int maxUserCount;
    [XmlAttribute] public int gearScore;
    [XmlAttribute] public int limitAchieveID;
    [XmlAttribute] public short limitPlayerLevel;
    [XmlAttribute] public bool limitVIP;
    [M2dArray] public DayOfWeek[] limitDayOfWeeks = Array.Empty<DayOfWeek>();
    [XmlAttribute] public bool limitMesoRevival;
    [XmlAttribute] public bool limitMeratRevival;
    [M2dArray] public int[] limitClearDungeon = Array.Empty<int>();
    [M2dArray] public int[] limitAdditionalEffects = Array.Empty<int>();
    [XmlAttribute] public bool limitRecommendWeapon;
    [XmlAttribute] public string dungeonBanner = string.Empty;
    [XmlAttribute] public string bossIcon = string.Empty;
    [XmlAttribute] public bool isUseBossIcon;
    [XmlAttribute] public DungeonBossRankingType BossRankingType;
    [XmlAttribute] public string openPeriodTag = string.Empty;  // 2019-05-31-00-00
    [XmlAttribute] public bool isPartyOnly;
    [XmlAttribute] public bool isChangeMaxUser;
    [XmlAttribute] public int playerCountFactorID;
    [XmlAttribute] public short customMonsterLevel;
    [XmlAttribute] public bool chaosDamageMeter;
    [XmlAttribute] public bool isMoveOutToBackupField;
    [XmlAttribute] public int defaultRevivalLimitCount;
    [XmlAttribute] public int dungeonHelperRequireClearCount;
    [XmlAttribute] public bool isUseRandomMatch;
    [XmlAttribute] public bool isLeaveAfterCloseReward;
    [M2dArray] public int[] partyMissions = Array.Empty<int>();
    [M2dArray] public int[] userMissions = Array.Empty<int>();
    [XmlAttribute] public int rankTableID;
    [XmlAttribute] public int representID;

    [XmlAttribute] public string clearType = string.Empty;
    [XmlAttribute] public int BossRanking;
    [XmlAttribute] public string openPeriodDate = string.Empty;
    [XmlAttribute] public bool isDisableFindHelper;
    [XmlAttribute] public int extraRewardTicketBaseCount; // 0
    // Absent from the GMS2 table, set on 18 rows of Lith's and 34 of KMS2's.
    [XmlAttribute] public int extraRewardTicketTableID;
    // No row sets these two in GMS2, KMS2 or Lith. PrivateMaple2 still maps both into its metadata.
    [XmlAttribute] public bool dungeonHelperManualChange;
    [XmlAttribute] public bool isUseFindMember;

    // Not in XML
    // enum extraRewardItemType
    [XmlAttribute][Obsolete("Unused")] public bool isLimitTime;
    [XmlAttribute][Obsolete("Unused")] public bool isTimeUp;
    [XmlAttribute][Obsolete("Unused")] public bool isRandomField;
    [M2dArray][Obsolete("Unused")] public int[] findHelperRewardDropBoxIds;
    [M2dArray][Obsolete("Unused")] public int[] findHelperExtraRewardDropBoxIds;
    [XmlAttribute][Obsolete("Unused")] public bool isDisableRandomMatch;
    [XmlAttribute][Obsolete("Unused")] public DungeonRequireRole requireRole;
}
