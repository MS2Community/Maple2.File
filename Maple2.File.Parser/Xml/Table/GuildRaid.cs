using System.Xml.Serialization;
using M2dXmlGenerator;

namespace Maple2.File.Parser.Xml.Table;

// ./data/xml/table/guildraid.xml
[XmlRoot("ms2")]
public partial class GuildRaidRoot {
    [M2dFeatureLocale(Selector = "dungeonRoomID")] private IList<GuildRaid> _guildRaid;
}

public partial class GuildRaid : IFeatureLocale {
    [XmlAttribute] public int dungeonRoomID;
    [XmlAttribute] public int clearRaidPoint;
    [XmlAttribute] public float clearGuildExp;
    [XmlAttribute] public float clearGuildFund;
}
