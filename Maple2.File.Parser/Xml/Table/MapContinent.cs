using System.Xml.Serialization;

namespace Maple2.File.Parser.Xml.Table;

// ./data/xml/table/mapcontinenttable.xml
[XmlRoot("ms2")]
public class MapContinentRoot {
    [XmlElement] public List<MapContinent> continent;
}

public partial class MapContinent {
    [XmlAttribute] public int id;
    [XmlAttribute] public int hideOtherWorldQuest;
    [XmlAttribute] public int shadowContinent;
    [XmlAttribute] public int representWorldmapContinent;
}
