using System.Xml.Serialization;
using M2dXmlGenerator;

namespace Maple2.File.Parser.Xml.Skill;

public partial class SubConditionTarget {
    [M2dArray] public int[] hasBuffID = Array.Empty<int>();
    [M2dArray] public short[] hasBuffLevel = Array.Empty<short>();
    [M2dArray] public bool[] hasBuffOwner = Array.Empty<bool>();
    [M2dArray] public int[] checkActivateBuff = Array.Empty<int>();
    [M2dArray] public int[] hasBuffCount = Array.Empty<int>(); // csv map?
    [M2dArray] public string[] hasBuffCountCompare = Array.Empty<string>(); // csv map? CompareType
    [XmlAttribute] public int eventCondition; // 0,4,6,10,13
    [XmlAttribute] public int hasSkillID; // 0,10900131
    [XmlAttribute] public short hasSkillLevel; // 0,1,2,3,4,5
    [XmlAttribute] public int ignoreOwnerEvent; // 0,10500292
    [M2dArray] public int[] hasNotBuffID = Array.Empty<int>();
    [M2dArray] public bool[] hasNotBuffOwner = Array.Empty<bool>(); // KMS2/TMS2 only
    [M2dArray] public int[] eventSkillID = Array.Empty<int>();
    [M2dArray] public int[] eventIgnoreSkillID = Array.Empty<int>();
    [M2dArray] public int[] eventEffectID = Array.Empty<int>();
    [M2dArray] public string[] NpcRaceString = Array.Empty<string>();
    [M2dArray] public int[] NpcRanks = Array.Empty<int>();
    [M2dArray] public int[] NpcIDs = Array.Empty<int>();
    [XmlAttribute] public int targetCheckRange; // 0,150,250,300,350,400,450,600,750,800,900,1000,1200
    [XmlAttribute] public int targetCheckMinRange; // 0,351
    [XmlAttribute] public int targetInRangeCount; // 0,1,2,3,4,5,6,7,8
    [XmlAttribute] public int targetFriendly; // 0,1,2
    [XmlAttribute] public string targetCountSign = string.Empty;
    [M2dArray] public string[] requireStates = Array.Empty<string>(); // ObjectState
    [M2dArray] public string[] requireSubStates = Array.Empty<string>();
    [M2dArray] public string[] requireMasteryTypes = Array.Empty<string>(); // MasteryType
    [M2dArray] public int[] requireMasteryValues = Array.Empty<int>();

    [XmlElement] public List<CompareRange> compareStat;
}

public partial class SubConditionTargetNew {
    [M2dArray] public int[] hasBuffID = Array.Empty<int>();
    [M2dArray] public bool[] hasBuffOwner = Array.Empty<bool>();
    [M2dArray] public int[] hasBuffCount = Array.Empty<int>(); // csv map?
    [M2dArray] public int[] hasNotBuffID = Array.Empty<int>();
    [M2dArray] public bool[] hasNotBuffOwner = Array.Empty<bool>();

    [XmlElement] public List<CompareRange> compareStat;

}
