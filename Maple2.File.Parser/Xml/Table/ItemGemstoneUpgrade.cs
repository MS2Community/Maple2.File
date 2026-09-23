using System.Xml.Serialization;
using M2dXmlGenerator;

namespace Maple2.File.Parser.Xml.Table;

// ./data/xml/table/na/itemgemstoneupgrade.xml
[XmlRoot("ms2")]
public partial class ItemGemstoneUpgradeRoot {
    [M2dFeatureLocale(Selector = "ItemId|GemLevel")] private IList<ItemGemstoneUpgrade> _key;
}

public partial class ItemGemstoneUpgrade : IFeatureLocale {
    [XmlAttribute] public int ItemId;
    [XmlAttribute] public short GemLevel;
    [XmlAttribute] public int NextItemID;
    [M2dArray] public int[] EquipPart;
    [XmlAttribute] public string IconId = string.Empty;
    [M2dArray(Delimiter = ':')] public string[] IngredientItemID1;
    [XmlAttribute] public int IngredientCount1;
    [M2dArray(Delimiter = ':')] public string[] IngredientItemID2;
    [XmlAttribute] public int IngredientCount2;
    [M2dArray(Delimiter = ':')] public string[] IngredientItemID3;
    [XmlAttribute] public int IngredientCount3;
    [M2dArray(Delimiter = ':')] public string[] IngredientItemID4;
    [XmlAttribute] public int IngredientCount4;
}
