using System.Xml.Serialization;
using M2dXmlGenerator;

namespace Maple2.File.Parser.Xml.Table.Server;

// ./data/server/table/Server/globalEquipmentRankProbability.xml
// The rank an item drops at when its drop row names no grade and the item has no fixed rank. One row per
// npcRank, npcLevel and assistCondition; KR and CN override some npcRank 4 rows.
[XmlRoot("ms2")]
public partial class GlobalEquipmentRankProbabilityRoot {
    [M2dFeatureLocale(Name = "DropProbability", Selector = "npcRank|npcLevel|assistCondition")]
    private IList<GlobalEquipmentRankProbability> _dropProbability;
}

// The four columns are percentages of ranks 2 to 5; whatever they leave is rank 1. An empty cell is 0.
public partial class GlobalEquipmentRankProbability : IFeatureLocale {
    [XmlAttribute] public int npcRank;
    [XmlAttribute] public int npcLevel;
    [XmlAttribute] public int assistCondition;
    [XmlAttribute] public float Rare;
    [XmlAttribute] public float Elite;
    [XmlAttribute] public float Excellent;
    [XmlAttribute] public float Legendary;
}
