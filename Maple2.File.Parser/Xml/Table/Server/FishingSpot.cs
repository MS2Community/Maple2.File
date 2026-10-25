using System.Xml.Serialization;
using M2dXmlGenerator;

namespace Maple2.File.Parser.Xml.Table.Server;

// ./data/server/table/Server/fishingSpot.xml
[XmlRoot("ms2")]
public partial class FishingSpotRoot {
    [M2dFeatureLocale(Selector = "id")] private IList<FishingSpot> _spot;
}

public partial class FishingSpot : IFeatureLocale {
    [XmlAttribute] public int id;
    [XmlAttribute] public int minMastery;
    [XmlAttribute] public int maxMastery;
    [M2dArray] public string[] liquidType = Array.Empty<string>();
    [XmlAttribute] public int globalFishBoxID;
    [XmlAttribute] public int individualFishBoxID;
    [XmlAttribute] public int globalDropBoxId;
    [XmlAttribute] public int individualDropBoxId;
    [XmlAttribute] public int spotLevel;
    [XmlAttribute] public int spotDropRank;
}
