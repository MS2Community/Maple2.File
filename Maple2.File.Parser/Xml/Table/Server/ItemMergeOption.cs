using System.Xml.Serialization;
using M2dXmlGenerator;

namespace Maple2.File.Parser.Xml.Table.Server;

// ./data/server/table/Server/itemMergeOptionBase.xml
[XmlRoot("ms2")]
public class ItemMergeOptionRoot {
    [XmlElement] public List<MergeOption> mergeOption;
}

public partial class MergeOption {
    [XmlAttribute] public int id;

    [XmlElement] public List<Slot> slot;

    public partial class Slot {
        [XmlAttribute] public int part = 1;
        // The client table nests slots under <mergeStep step="N">; the server file has no such
        // level, so a slot names its step here. Retail rows carry none and are all step 1.
        [XmlAttribute] public int step = 1;
        // Some tables (the 370300xx/370400xx crystals) list their slots here instead of in part.
        [M2dArray] public int[] partLimit = Array.Empty<int>();
        [XmlAttribute] public long consumeMeso;
        [M2dArray] public string[] itemMaterial1 = Array.Empty<string>();
        [M2dArray] public string[] itemMaterial2 = Array.Empty<string>();
        [XmlElement] public List<Option> option;
    }

    public class Option {
        [XmlAttribute] public string optionName = string.Empty;
        [XmlAttribute] public int min;
        [XmlAttribute] public int idx0_max;
        [XmlAttribute] public int idx0_weight;
        [XmlAttribute] public int idx1_max;
        [XmlAttribute] public int idx1_weight;
        [XmlAttribute] public int idx2_max;
        [XmlAttribute] public int idx2_weight;
        [XmlAttribute] public int idx3_max;
        [XmlAttribute] public int idx3_weight;
        [XmlAttribute] public int idx4_max;
        [XmlAttribute] public int idx4_weight;
        [XmlAttribute] public int idx5_max;
        [XmlAttribute] public int idx5_weight;
        [XmlAttribute] public int idx6_max;
        [XmlAttribute] public int idx6_weight;
        [XmlAttribute] public int idx7_max;
        [XmlAttribute] public int idx7_weight;
        [XmlAttribute] public int idx8_max;
        [XmlAttribute] public int idx8_weight;
        [XmlAttribute] public int idx9_max;
        [XmlAttribute] public int idx9_weight;

        public (int, int) this[int i] => i switch {
            0 => (idx0_max, idx0_weight),
            1 => (idx1_max, idx1_weight),
            2 => (idx2_max, idx2_weight),
            3 => (idx3_max, idx3_weight),
            4 => (idx4_max, idx4_weight),
            5 => (idx5_max, idx5_weight),
            6 => (idx6_max, idx6_weight),
            7 => (idx7_max, idx7_weight),
            8 => (idx8_max, idx8_weight),
            9 => (idx9_max, idx9_weight),
            _ => throw new ArgumentOutOfRangeException(nameof(i), i, "Invalid option index."),
        };
    }
}
