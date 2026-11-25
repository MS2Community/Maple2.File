using System.Xml.Serialization;
using M2dXmlGenerator;

namespace Maple2.File.Parser.Xml.Table;

// ./data/xml/table/itemenchanttransform.xml
// Rows have no id. The client filters each row on its own and keys it by input item id, inputRank,
// inputLimitLevel, inputEnchantLevel and inputSlot, so no Selector here.
[XmlRoot("ms2")]
public partial class ItemEnchantTransformRoot {
    [M2dFeatureLocale] private IList<ItemEnchantTransform> _transform;
}

public partial class ItemEnchantTransform : IFeatureLocale {
    [XmlAttribute] public int inputRank;
    [XmlAttribute] public int inputLimitLevel;
    [XmlAttribute] public int inputEnchantLevel;
    [M2dArray] public int[] inputSlot = Array.Empty<int>();
    [M2dArray] public int[] inputItemId = Array.Empty<int>();
    [XmlAttribute] public long needMeso;
    [XmlAttribute] public int output1Id;
    [XmlAttribute] public int output1Rank;
    [XmlAttribute] public int output1Count;
    [XmlAttribute] public int output2Id;
    [XmlAttribute] public int output2Rank;
    [XmlAttribute] public int output2Count;
}
