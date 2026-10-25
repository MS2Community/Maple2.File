using System.Drawing;
using System.Globalization;
using System.Numerics;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Maple2.File.Parser.Flat.Convert;

[XmlRoot]
public class EntityModel {
    [XmlAttribute("version")] public int Version = 5;
    [XmlAttribute("ID")] public string Id;
    [XmlAttribute] public string Description = "";

    [XmlElement] public LMixins Mixins;
    [XmlElement] public LProperties Properties;
    [XmlElement] public LPropertyOverrides PropertyOverrides;
    [XmlElement] public LBehaviors Behaviors;
    [XmlElement] public LBehaviorOverrides BehaviorOverrides;
    [XmlElement] public LTraits Traits;
    [XmlElement] public LExtraDataList ExtraDataList = new();

    public EntityModel() {
        Mixins = new LMixins();
        Properties = new LProperties();
        PropertyOverrides = new LPropertyOverrides();
        Behaviors = new LBehaviors();
        BehaviorOverrides = new LBehaviorOverrides();
        Traits = new LTraits();
    }

    public class LMixins {
        [XmlElement] public List<Mixin> Mixin;
    }

    public class LProperties {
        [XmlElement] public List<Property> Property;
    }

    public class LPropertyOverrides {
        [XmlElement] public List<PropertyOverride> PropertyOverride = new();
    }

    public class LBehaviors {
        [XmlElement] public List<Behavior> Behavior;
    }

    public class LBehaviorOverrides {
        [XmlElement] public List<BehaviorOverride> BehaviorOverride = new();
    }

    public class LExtraDataList { }
}

[XmlRoot]
public class EntityModelPreset : EntityModel { }

public class Mixin {
    [XmlAttribute("SourceID")] public string SourceId;
    [XmlAttribute] public string SourceName;
}

public class Property {
    [XmlAttribute] public string Name;
    [XmlAttribute] public string Description = "";

    [XmlElement] public LTraits Traits = new();
    [XmlElement] public Value Value;
    // Restriction; Not copied to flat
    [XmlElement] public LRestriction Restriction = new();

    public class LRestriction { }
}

public class PropertyOverride {
    [XmlAttribute] public string Name;
    [XmlAttribute] public string Description = "";

    [XmlElement] public LTraitOverrides TraitOverrides = new();
    [XmlElement] public Value Value;
}

public class Behavior {
    [XmlAttribute] public string Name;
    [XmlAttribute] public string Description = "";
    [XmlAttribute] public string Target;

    [XmlElement] public LTraits Traits = new();
}

public class BehaviorOverride {
    [XmlAttribute] public string Name;
    [XmlAttribute] public string Description = "";
    [XmlAttribute] public string Target;

    [XmlElement] public LTraitOverrides TraitOverrides = new();
}

public class Value : IXmlSerializable {
    private readonly AssetIndex index;

    private readonly string type;
    private readonly object value;

    public Value() {
        type = "Unknown";
        value = "Invalid";
    }

    public Value(AssetIndex index, string type, object value) {
        this.index = index;
        this.type = type.Replace("AssetID", "Asset");
        this.value = value;
    }

    public XmlSchema GetSchema() {
        return null;
    }

    public void ReadXml(XmlReader reader) {
        throw new System.NotImplementedException();
    }

    public void WriteXml(XmlWriter writer) {
        WriteType(writer, type, value);
    }

    private void WriteType(XmlWriter writer, string valueType, object value) {
        if (valueType.StartsWith("Assoc")) {
            writer.WriteStartElement("Map");
            valueType = valueType.Replace("Assoc", "");
            writer.WriteAttributeString("ItemsDataType", valueType);

            writer.WriteStartElement("Items");
            foreach ((string key, Action<XmlWriter> valWriter) in GetMapTypeWriter(valueType, value)) {
                writer.WriteStartElement("Item");
                writer.WriteAttributeString("Key", key);
                valWriter(writer); // LastKnownName, LastKnownPath, LastKnownTags???
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.WriteEndElement();
            return;
        }

        GetTypeWriter(valueType, value)(writer);
    }

    private IEnumerable<(string, Action<XmlWriter>)> GetMapTypeWriter(string type, object value) {
        return value switch {
            Dictionary<string, string> strDict => strDict.Select(entry => (entry.Key, GetTypeWriter(type, entry.Value))),
            Dictionary<string, bool> boolDict => boolDict.Select(entry => (entry.Key, GetTypeWriter(type, entry.Value))),
            Dictionary<string, ushort> ushortDict => ushortDict.Select(entry => (entry.Key, GetTypeWriter(type, entry.Value))),
            Dictionary<string, uint> uintDict => uintDict.Select(entry => (entry.Key, GetTypeWriter(type, entry.Value))),
            Dictionary<string, int> intDict => intDict.Select(entry => (entry.Key, GetTypeWriter(type, entry.Value))),
            Dictionary<string, float> floatDict => floatDict.Select(entry => (entry.Key, GetTypeWriter(type, entry.Value))),
            Dictionary<string, double> doubleDict => doubleDict.Select(entry => (entry.Key, GetTypeWriter(type, entry.Value))),
            Dictionary<string, Vector3> vector3Dict => vector3Dict.Select(entry => (entry.Key, GetTypeWriter(type, entry.Value))),
            Dictionary<string, Vector2> vector2Dict => vector2Dict.Select(entry => (entry.Key, GetTypeWriter(type, entry.Value))),
            Dictionary<string, Color> colorDict => colorDict.Select(entry => (entry.Key, GetTypeWriter(type, entry.Value))),
            _ => throw new ArgumentException($"Invalid AssocType({value.GetType()}): {value}"),
        };
    }

    private Action<XmlWriter> GetTypeWriter(string type, object value) {
        return writer => {
            writer.WriteStartElement(type);
            switch (value) {
                case bool boolean:
                    writer.WriteAttributeString("Value", boolean ? "True" : "False");
                    break;
                case ushort:
                case uint:
                case ulong:
                case short:
                case int:
                case long:
                case float:
                case double:
                    writer.WriteAttributeString("Value", System.Convert.ToString(value, CultureInfo.InvariantCulture));
                    break;
                case Vector3 point3:
                    writer.WriteStartElement("X");
                    GetTypeWriter("Float32", point3.X)(writer);
                    writer.WriteEndElement();
                    writer.WriteStartElement("Y");
                    GetTypeWriter("Float32", point3.Y)(writer);
                    writer.WriteEndElement();
                    writer.WriteStartElement("Z");
                    GetTypeWriter("Float32", point3.Z)(writer);
                    writer.WriteEndElement();
                    break;
                case Vector2 point2:
                    writer.WriteStartElement("X");
                    GetTypeWriter("Float32", point2.X)(writer);
                    writer.WriteEndElement();
                    writer.WriteStartElement("Y");
                    GetTypeWriter("Float32", point2.Y)(writer);
                    writer.WriteEndElement();
                    break;
                case Color color:
                    writer.WriteAttributeString("R", color.R.ToString());
                    writer.WriteAttributeString("G", color.G.ToString());
                    writer.WriteAttributeString("B", color.B.ToString());
                    if (type == "ColorA") {
                        writer.WriteAttributeString("A", color.A.ToString());
                    }
                    break;
                case string:
                    switch (type) {
                        case "EntityRef":
                            writer.WriteStartElement("Name");
                            writer.WriteEndElement();
                            writer.WriteStartElement("Id");
                            writer.WriteString(value.ToString());
                            writer.WriteEndElement();
                            writer.WriteStartElement("TargetContainerHierarchy");
                            writer.WriteEndElement();
                            break;
                        case "Asset":
                            string llid = value.ToString();
                            (string name, string path, string tags) = index.GetFields(llid);

                            writer.WriteAttributeString("Value", llid);
                            writer.WriteStartElement("LastKnownName");
                            writer.WriteString(name);
                            writer.WriteEndElement();
                            writer.WriteStartElement("LastKnownPath");
                            writer.WriteString(path);
                            writer.WriteEndElement();
                            writer.WriteStartElement("LastKnownTags");
                            writer.WriteString(tags);
                            writer.WriteEndElement();
                            break;
                        case "AttachedNifAsset":
                            throw new ArgumentException("Cannot serialize AttachedNifAsset");
                        default:
                            writer.WriteAttributeString("Value", value.ToString());
                            break;
                    }
                    break;
                default:
                    throw new ArgumentException($"Invalid Type({value.GetType()}): {value}");
            }
            writer.WriteEndElement();
        };
    }
}

public class LTraits {
    [XmlElement] public List<Trait> Trait;
}

public class Trait {
    [XmlAttribute] public string Name;
}

public class LTraitOverrides {
    [XmlElement] public List<TraitOverride> TraitOverride = new();
}

public class TraitOverride {
    [XmlAttribute] public bool IsActive;

    [XmlElement] public Trait Trait;
}
