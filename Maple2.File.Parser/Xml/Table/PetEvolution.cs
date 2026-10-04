using System.Xml.Serialization;
using M2dXmlGenerator;

namespace Maple2.File.Parser.Xml.Table;

// ./data/xml/table/petevolution.xml
// Evolution points a pet needs, keyed by the pet item's levelLimit; GradeN is the cap at rarity N.
[XmlRoot("ms2")]
public partial class PetEvolutionRoot {
    [M2dFeatureLocale(Selector = "requireItemLevel")] private IList<PetEvolution> _petEvolution;
}

public partial class PetEvolution : IFeatureLocale {
    [XmlAttribute] public short requireItemLevel;
    [XmlAttribute] public int Grade1;
    [XmlAttribute] public int Grade2;
    [XmlAttribute] public int Grade3;
    [XmlAttribute] public int Grade4;
}
