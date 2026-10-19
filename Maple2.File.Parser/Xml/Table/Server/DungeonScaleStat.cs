using System.Xml.Serialization;

namespace Maple2.File.Parser.Xml.Table.Server;

// ./data/server/table/Server/DungeonScaleStat.xml
// Per-dungeon counterpart of the scale* attributes in npc Stat. No dungeonroom column references it.
[XmlRoot("ms2")]
public class DungeonScaleStatRoot {
    [XmlElement] public List<DungeonScaleStat> DungeonScaleStat;
}

public partial class DungeonScaleStat {
    [XmlAttribute] public int dungeonScaleStatID;
    [XmlAttribute] public float scaleStatRate;
    [XmlAttribute] public long scaleBaseTap;
    [XmlAttribute] public long scaleBaseDef;
    [XmlAttribute] public float scaleBaseSpaRate;
}
