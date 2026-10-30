using System.Xml.Serialization;
using M2dXmlGenerator;

namespace Maple2.File.Parser.Xml.Table;

// ./data/xml/table/shadowbuff.xml
[XmlRoot("ms2")]
public partial class ShadowBuffRoot {
    [M2dFeatureLocale(Selector = "id")] private IList<ShadowBuff> _ShadowBuff;
}

public partial class ShadowBuff : IFeatureLocale {
    [XmlAttribute] public int id;
    // Absent on the common traits, which every class lists.
    [XmlAttribute] public int jobCode;
    [XmlAttribute] public string iconPath = string.Empty;
    [XmlElement] public List<ShadowBuffLevel> ShadowBuffLevel;
}

public partial class ShadowBuffLevel {
    [XmlAttribute] public int level;
    // Retail's spelling. Additional effect ids, 10 digits, so they do not fit an int.
    [M2dArray] public long[] addifectGuids = Array.Empty<long>();
    // Absent on a class's free level 1.
    [XmlAttribute] public int levelUpCost;
}
