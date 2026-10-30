using System.Xml.Serialization;
using M2dXmlGenerator;

namespace Maple2.File.Parser.Xml.Table.Server;

// ./data/server/table/Server/additionalEffectGroup.xml
// An npc's <additionalEffect group="G,N"> names group G and how many of its rows to roll.
[XmlRoot("ms2")]
public class AdditionalEffectGroupRoot {
    [XmlElement] public List<AdditionalEffectGroup> group;
}

public partial class AdditionalEffectGroup {
    [XmlAttribute] public int groupId;
    [XmlElement] public List<Value> value;

    // One row is one buff. Two ids are a visible effect plus its hidden driver or splash helper.
    public partial class Value {
        [M2dArray] public int[] effectId = Array.Empty<int>();
        [M2dArray] public short[] effectLevel = Array.Empty<short>();
    }
}
