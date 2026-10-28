using System.Xml.Serialization;
using M2dXmlGenerator;

namespace Maple2.File.Parser.Xml.Table.Server;

// ./data/server/table/Server/shadowExpeditionDrop.xml
[XmlRoot("ms2")]
public class ShadowExpeditionDropRoot {
    [XmlElement] public List<ShadowExpeditionDrop> shadowExpeditionDrop;
}

// class is the killed NPC's basic class: 1 normal, 3 elite, 6 boss.
public partial class ShadowExpeditionDrop {
    [XmlAttribute] public int step;
    [XmlAttribute("class")] public int npcClass;
    [M2dArray] public int[] dropBoxId = Array.Empty<int>();
}
