using System.Xml.Serialization;
using M2dXmlGenerator;

namespace Maple2.File.Parser.Xml.Table.Server;

// ./data/server/table/Server/shadowExpeditionDrop.xml
[XmlRoot("ms2")]
public class ShadowExpeditionDropRoot {
    [XmlElement] public List<ShadowExpeditionDrop> shadowExpeditionDrop;
}

public partial class ShadowExpeditionDrop {
    [XmlAttribute] public int step;
    // The killed NPC's basic class: 1, 3 or 6, the classes whose npc rankIcon is c, b and a (normal, elite, boss).
    [XmlAttribute("class")] public int @class;
    [M2dArray] public int[] dropBoxId = Array.Empty<int>();
}
