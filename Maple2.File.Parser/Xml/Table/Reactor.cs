using System.Xml.Serialization;
using M2dXmlGenerator;

namespace Maple2.File.Parser.Xml.Table;

// ./data/xml/table/reactor.xml
[XmlRoot("ms2")]
public partial class ReactorRoot {
    [M2dFeatureLocale(Selector = "id")] private IList<Reactor> _reactor;
}

public partial class Reactor : IFeatureLocale {
    [XmlAttribute] public int id;
    [XmlAttribute] public float reactDistance;
    // Bracket-wrapped comma list, e.g. "[100, 101, 102, 103]". Consumers strip
    // the surrounding [] and split on ','; M2dArray cannot parse it directly
    // because int.Parse would choke on the leading '[' / trailing ']' tokens.
    [XmlAttribute] public string reactorStateList = string.Empty;
    [XmlAttribute] public string connectedEffect = string.Empty;
    // gameEventId links this reactor definition to a GameEvent whose eventType is
    // "reactor" (GMS2 client RE: reactor+0x70 read by CReactor_CheckReactorGameEvent).
    [XmlAttribute] public int gameEventId;
    // Escaped XML fragment: <items><v itemID="..." grade="..." count="..." /></items>.
    // Consumers deserialize this separately if they need the reward item list.
    [XmlAttribute] public string rewardItemListXml = string.Empty;
    [XmlAttribute] public int reactTimeMSec = 15000;
    [XmlAttribute] public string reactAnimation = string.Empty;
    [XmlAttribute] public string createAnimation = string.Empty;
}
