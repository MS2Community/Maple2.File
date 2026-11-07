using System.Xml.Serialization;
using M2dXmlGenerator;

namespace Maple2.File.Parser.Xml.Table;

// ./data/xml/table/stance.xml, KMS2/TMS2 only (StanceTable::Load). GMS2 retail ships no such file.
[XmlRoot("ms2")]
public partial class StanceRoot {
    [XmlElement("v")] public List<Stance> v = [];
}

public partial class Stance {
    [XmlAttribute] public int stanceAdditionalID;
    // Retail's spelling.
    [XmlAttribute] public int hitCountAdditinoalID;
    [M2dArray] public int[] damageSkillIDs = Array.Empty<int>();
    [M2dArray] public int[] finishStanceSkillIDs = Array.Empty<int>();
    [XmlElement] public List<StanceLevel> level = [];
}

public partial class StanceLevel {
    [XmlAttribute] public int level;
    [XmlAttribute] public int hitCount;
    [XmlAttribute] public int multiplyDamageRate;
    [XmlAttribute] public int maxAccDamageRate;
}
