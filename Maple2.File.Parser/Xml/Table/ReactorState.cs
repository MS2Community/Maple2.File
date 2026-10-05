using System.Xml.Serialization;

namespace Maple2.File.Parser.Xml.Table;

// ./data/xml/table/reactorstate.xml
[XmlRoot("ms2")]
public class ReactorStateRoot {
    [XmlElement] public List<ReactorState> reactorState;
}

public class ReactorState {
    [XmlAttribute] public int id;
    // Observed values: connect, sleep, reward.
    [XmlAttribute] public string type = string.Empty;
    [XmlAttribute] public int nextTimeSec;
    [XmlAttribute] public int expireTimeSec;
    [XmlAttribute] public string kfmName = string.Empty;
    [XmlAttribute] public string startAni = string.Empty;
    [XmlAttribute] public string idleAni = string.Empty;
    [XmlAttribute] public string actionStringKey = string.Empty;

    // Only present on states of type="connect"; absent (self-closing tag) on other states.
    [XmlElement] public CondItem condItem;

    public class CondItem {
        [XmlAttribute] public int code;
        [XmlAttribute] public int consume;
    }
}
