using System.Data;
using System.Drawing;
using System.Numerics;
using System.Xml;
using Maple2.File.IO;
using Maple2.File.IO.Crypto.Common;
using Maple2.File.Parser.Tools;

namespace Maple2.File.Parser.Flat;

public class FlatTypeIndex {
    private const uint MAGIC = 0x00495446;
    private const int VERSION = 1;

    public readonly HierarchyMap<FlatType> Hierarchy;

    private readonly string root;
    private readonly Dictionary<string, FlatTypeNode> typeNodes;

    public bool MinimizeProperties { get; set; } = false;

    public FlatTypeIndex(M2dReader reader, string root = "flat") {
        this.root = root;
        Hierarchy = new HierarchyMap<FlatType>();
        typeNodes = ReadTypeNodes(reader);

        foreach (FlatTypeNode typeNode in typeNodes.Values) {
            foreach (FlatType mixin in typeNode.Value.Mixin) {
                typeNodes[mixin.Name.ToLower()].Children.Add(typeNode);
            }
        }
    }

    private FlatTypeIndex(string root, Dictionary<string, FlatTypeNode> typeNodes) {
        this.root = root;
        this.typeNodes = typeNodes;
        Hierarchy = new HierarchyMap<FlatType>();

        foreach (FlatTypeNode typeNode in typeNodes.Values) {
            Hierarchy.Add(typeNode.Value.Path, typeNode.Value);
        }

        foreach (FlatTypeNode typeNode in typeNodes.Values) {
            foreach (FlatType mixin in typeNode.Value.Mixin) {
                typeNodes[mixin.Name.ToLower()].Children.Add(typeNode);
            }
        }
    }

    public void Serialize(BinaryWriter writer) {
        writer.Write(MAGIC);
        writer.Write(VERSION);
        writer.Write(root ?? string.Empty);
        writer.Write(typeNodes.Count);

        foreach (FlatTypeNode typeNode in typeNodes.Values) {
            SerializeFlatType(writer, typeNode.Value);
        }
    }

    public static FlatTypeIndex Deserialize(BinaryReader reader) {
        uint magic = reader.ReadUInt32();
        if (magic != MAGIC) {
            throw new InvalidDataException($"Invalid FlatTypeIndex magic: expected {MAGIC:X}, got {magic:X}");
        }

        int version = reader.ReadInt32();
        if (version != VERSION) {
            throw new InvalidDataException($"Unsupported FlatTypeIndex version: {version}");
        }

        string root = reader.ReadString();
        int typeCount = reader.ReadInt32();

        var typeNodes = new Dictionary<string, FlatTypeNode>(typeCount);
        var mixinNames = new Dictionary<FlatType, List<string>>(typeCount);

        for (int i = 0; i < typeCount; i++) {
            var (type, mixins) = DeserializeFlatType(reader);
            typeNodes[type.Name.ToLower()] = new FlatTypeNode(type);
            mixinNames[type] = mixins;
        }

        foreach (var (type, mixins) in mixinNames) {
            foreach (string mixinName in mixins) {
                string key = mixinName.ToLower();
                if (!typeNodes.TryGetValue(key, out FlatTypeNode mixinNode)) {
                    throw new InvalidDataException($"Mixin '{mixinName}' not found in cache (data corruption?)");
                }
                type.Mixin.Add(mixinNode.Value);
            }
        }

        return new FlatTypeIndex(root, typeNodes);
    }

    private static void SerializeFlatType(BinaryWriter writer, FlatType type) {
        writer.Write(type.Name);
        writer.Write(type.Id);
        writer.Write(type.Path ?? string.Empty);

        writer.Write(type.Trait.Count);
        foreach (string trait in type.Trait) {
            writer.Write(trait ?? string.Empty);
        }

        writer.Write(type.Mixin.Count);
        foreach (FlatType mixin in type.Mixin) {
            writer.Write(mixin.Name ?? string.Empty);
        }

        writer.Write(type.Properties.Count);
        foreach (FlatProperty property in type.Properties.Values) {
            SerializeProperty(writer, property);
        }

        writer.Write(type.Behaviors.Count);
        foreach (FlatBehavior behavior in type.Behaviors.Values) {
            SerializeBehavior(writer, behavior);
        }
    }

    private static (FlatType Type, List<string> MixinNames) DeserializeFlatType(BinaryReader reader) {
        string name = reader.ReadString();
        uint id = reader.ReadUInt32();
        string path = reader.ReadString();

        var type = new FlatType(name, id) {
            Path = path,
        };

        int traitCount = reader.ReadInt32();
        for (int i = 0; i < traitCount; i++) {
            type.Trait.Add(reader.ReadString());
        }

        int mixinCount = reader.ReadInt32();
        var mixinNames = new List<string>(mixinCount);
        for (int i = 0; i < mixinCount; i++) {
            mixinNames.Add(reader.ReadString());
        }

        int propertyCount = reader.ReadInt32();
        for (int i = 0; i < propertyCount; i++) {
            FlatProperty property = DeserializeProperty(reader);
            type.Properties.Add(property.Name, property);
        }

        int behaviorCount = reader.ReadInt32();
        for (int i = 0; i < behaviorCount; i++) {
            FlatBehavior behavior = DeserializeBehavior(reader);
            type.Behaviors.Add(behavior.Name, behavior);
        }

        return (type, mixinNames);
    }

    private static void SerializeProperty(BinaryWriter writer, FlatProperty property) {
        writer.Write(property.Name ?? string.Empty);
        writer.Write(property.Id ?? string.Empty);

        writer.Write(property.Source != null);
        if (property.Source != null) {
            writer.Write(property.Source);
        }

        writer.Write(property.Type ?? string.Empty);

        writer.Write(property.Trait.Count);
        foreach (string trait in property.Trait) {
            writer.Write(trait ?? string.Empty);
        }

        SerializePropertyValue(writer, property.Type, property.Value);
    }

    private static FlatProperty DeserializeProperty(BinaryReader reader) {
        string name = reader.ReadString();
        string id = reader.ReadString();
        bool hasSource = reader.ReadBoolean();
        string source = hasSource ? reader.ReadString() : null;
        string type = reader.ReadString();

        var property = new FlatProperty {
            Name = name,
            Id = id,
            Source = source,
            Type = type,
        };

        int traitCount = reader.ReadInt32();
        for (int i = 0; i < traitCount; i++) {
            property.Trait.Add(reader.ReadString());
        }

        property.Value = DeserializePropertyValue(reader, type);

        return property;
    }

    private static void SerializePropertyValue(BinaryWriter writer, string type, object value) {
        switch (type) {
            case "Boolean":
                writer.Write((bool) value);
                break;
            case "UInt16":
                writer.Write((ushort) value);
                break;
            case "UInt32":
                writer.Write((uint) value);
                break;
            case "SInt32":
                writer.Write((int) value);
                break;
            case "Float32":
                writer.Write((float) value);
                break;
            case "Float64":
                writer.Write((double) value);
                break;
            case "Point3": {
                Vector3 v = (Vector3) value;
                writer.Write(v.X);
                writer.Write(v.Y);
                writer.Write(v.Z);
                break;
            }
            case "Point2": {
                Vector2 v = (Vector2) value;
                writer.Write(v.X);
                writer.Write(v.Y);
                break;
            }
            case "Color": {
                Color c = (Color) value;
                writer.Write(c.R);
                writer.Write(c.G);
                writer.Write(c.B);
                break;
            }
            case "ColorA": {
                Color c = (Color) value;
                writer.Write(c.A);
                writer.Write(c.R);
                writer.Write(c.G);
                writer.Write(c.B);
                break;
            }
            case "String":
            case "EntityRef":
            case "AssetID":
                writer.Write((string) value ?? string.Empty);
                break;
            case "AssocString":
            case "AssocEntityRef":
            case "AssocAttachedNifAsset": {
                var dict = (Dictionary<string, string>) value;
                writer.Write(dict.Count);
                foreach (var kvp in dict) {
                    writer.Write(kvp.Key ?? string.Empty);
                    writer.Write(kvp.Value ?? string.Empty);
                }
                break;
            }
            case "AssocPoint3": {
                var dict = (Dictionary<string, Vector3>) value;
                writer.Write(dict.Count);
                foreach (var kvp in dict) {
                    writer.Write(kvp.Key ?? string.Empty);
                    writer.Write(kvp.Value.X);
                    writer.Write(kvp.Value.Y);
                    writer.Write(kvp.Value.Z);
                }
                break;
            }
            case "AssocUInt32": {
                var dict = (Dictionary<string, uint>) value;
                writer.Write(dict.Count);
                foreach (var kvp in dict) {
                    writer.Write(kvp.Key ?? string.Empty);
                    writer.Write(kvp.Value);
                }
                break;
            }
            case "AssocSInt32": {
                var dict = (Dictionary<string, int>) value;
                writer.Write(dict.Count);
                foreach (var kvp in dict) {
                    writer.Write(kvp.Key ?? string.Empty);
                    writer.Write(kvp.Value);
                }
                break;
            }
            default:
                throw new ArgumentException($"Unknown property type for serialization: {type}");
        }
    }

    private static object DeserializePropertyValue(BinaryReader reader, string type) {
        switch (type) {
            case "Boolean":
                return reader.ReadBoolean();
            case "UInt16":
                return reader.ReadUInt16();
            case "UInt32":
                return reader.ReadUInt32();
            case "SInt32":
                return reader.ReadInt32();
            case "Float32":
                return reader.ReadSingle();
            case "Float64":
                return reader.ReadDouble();
            case "Point3":
                return new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            case "Point2":
                return new Vector2(reader.ReadSingle(), reader.ReadSingle());
            case "Color":
                return Color.FromArgb(reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
            case "ColorA":
                return Color.FromArgb(reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
            case "String":
            case "EntityRef":
            case "AssetID":
                return reader.ReadString();
            case "AssocString":
            case "AssocEntityRef":
            case "AssocAttachedNifAsset": {
                int count = reader.ReadInt32();
                var dict = new Dictionary<string, string>(count);
                for (int i = 0; i < count; i++) {
                    dict[reader.ReadString()] = reader.ReadString();
                }
                return dict;
            }
            case "AssocPoint3": {
                int count = reader.ReadInt32();
                var dict = new Dictionary<string, Vector3>(count);
                for (int i = 0; i < count; i++) {
                    dict[reader.ReadString()] = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                }
                return dict;
            }
            case "AssocUInt32": {
                int count = reader.ReadInt32();
                var dict = new Dictionary<string, uint>(count);
                for (int i = 0; i < count; i++) {
                    dict[reader.ReadString()] = reader.ReadUInt32();
                }
                return dict;
            }
            case "AssocSInt32": {
                int count = reader.ReadInt32();
                var dict = new Dictionary<string, int>(count);
                for (int i = 0; i < count; i++) {
                    dict[reader.ReadString()] = reader.ReadInt32();
                }
                return dict;
            }
            default:
                throw new ArgumentException($"Unknown property type for deserialization: {type}");
        }
    }

    private static void SerializeBehavior(BinaryWriter writer, FlatBehavior behavior) {
        writer.Write(behavior.Name ?? string.Empty);
        writer.Write(behavior.Id ?? string.Empty);
        writer.Write(behavior.Type ?? string.Empty);

        writer.Write(behavior.Source != null);
        if (behavior.Source != null) {
            writer.Write(behavior.Source);
        }

        writer.Write(behavior.Trait.Count);
        foreach (string trait in behavior.Trait) {
            writer.Write(trait ?? string.Empty);
        }
    }

    private static FlatBehavior DeserializeBehavior(BinaryReader reader) {
        string name = reader.ReadString();
        string id = reader.ReadString();
        string type = reader.ReadString();
        bool hasSource = reader.ReadBoolean();
        string source = hasSource ? reader.ReadString() : null;

        int traitCount = reader.ReadInt32();
        var traits = new List<string>(traitCount);
        for (int i = 0; i < traitCount; i++) {
            traits.Add(reader.ReadString());
        }

        return new FlatBehavior {
            Name = name,
            Id = id,
            Type = type,
            Source = source,
            Trait = traits,
        };
    }

    public IEnumerable<FlatType> GetAllTypes() {
        return typeNodes.Values.Select(node => node.Value);
    }

    public FlatType GetType(string name) {
        return typeNodes.GetValueOrDefault(name.ToLower(), null)?.Value;
    }

    public List<FlatType> GetSubTypes(string name) {
        if (!typeNodes.ContainsKey(name.ToLower())) {
            return new List<FlatType>();
        }

        return typeNodes[name.ToLower()].Children
            .Select(node => node.Value)
            .ToList();
    }

    // Builds Index
    private Dictionary<string, FlatTypeNode> ReadTypeNodes(M2dReader reader) {
        Dictionary<string, XmlNode> xmlNodes = new Dictionary<string, XmlNode>();
        Dictionary<string, FlatTypeNode> types = new Dictionary<string, FlatTypeNode>();
        foreach (PackFileEntry entry in reader.Files) {
            if (!entry.Name.StartsWith(root)) continue;

            XmlDocument xmlDocument = reader.GetXmlDocument(entry);
            XmlNode node = xmlDocument.SelectSingleNode("model");
            if (node == null) {
                Console.WriteLine($"Missing model node for: {entry.Name}");
                continue;
            }

            if (node.Attributes?["name"] == null) {
                Console.WriteLine($"Missing name for: {entry.Name}");
                continue;
            }
            if (!uint.TryParse(node.Attributes?["id"]?.Value, out uint id)) {
                Console.WriteLine($"Missing id for: {entry.Name}");
                continue;
            }

            string name = node.Attributes["name"].Value;
            xmlNodes[name] = node;
            var type = new FlatType(name, id) {
                Path = entry.Name,
            };
            Hierarchy.Add(entry.Name, type);
            types[name.ToLower()] = new FlatTypeNode(type);
            //Console.WriteLine($"Created type: {type.Name}");
        }

        // Populate Mixin and Property for Types.
        foreach ((string name, XmlNode node) in xmlNodes) {
            FlatType type = types[name.ToLower()].Value;
            XmlNodeList traitNodes = node.SelectNodes("trait");
            foreach (XmlNode traitNode in traitNodes) {
                string traitName = traitNode.Attributes["value"].Value;
                type.Trait.Add(traitName);
            }

            XmlNodeList mixinNodes = node.SelectNodes("mixin");
            foreach (XmlNode mixinNode in mixinNodes) {
                string mixinName = mixinNode.Attributes["name"].Value;
                type.Mixin.Add(types[mixinName.ToLower()].Value);
            }

            XmlNodeList propNodes = node.SelectNodes("property");
            foreach (XmlNode propNode in propNodes) {
                if (propNode?.Attributes == null) {
                    throw new ConstraintException("Null value found for property node");
                }

                FlatProperty property;

                XmlNodeList setNodes = propNode.SelectNodes("set");
                string propName = propNode.Attributes["name"].Value;
                string propType = propNode.Attributes["type"].Value;
                string propId = propNode.Attributes["id"].Value;
                string propSource = propNode.Attributes["source"]?.Value;

                if (propType.StartsWith("Assoc")) {
                    List<(string, string)> values = new List<(string, string)>();
                    foreach (XmlNode setNode in setNodes) {
                        values.Add((setNode.Attributes["index"].Value, setNode.Attributes["value"].Value));
                    }

                    property = new FlatProperty {
                        Name = propName,
                        Type = propType,
                        Id = propId,
                        Source = propSource,
                        Value = FlatProperty.ParseAssocType(propType, values),
                    };
                } else {
                    string value = setNodes[0].Attributes["value"].Value;
                    property = new FlatProperty {
                        Name = propName,
                        Type = propType,
                        Id = propId,
                        Source = propSource,
                        Value = FlatProperty.ParseType(propType, value),
                    };
                }

                traitNodes = propNode.SelectNodes("trait");
                foreach (XmlNode traitNode in traitNodes) {
                    string traitName = traitNode.Attributes["value"].Value;
                    property.Trait.Add(traitName);
                }

                // Skip this check by default because it doesn't seem fully correct.
                if (MinimizeProperties) {
                    // Don't add this property if the same value is already inherited
                    FlatProperty inheritedProperty = type.GetProperty(property.Name);
                    if (inheritedProperty != null && inheritedProperty.ValueEquals(property.Value)) {
                        continue;
                    }
                }

                type.Properties.Add(property.Name, property);
            }

            XmlNodeList behaviorNodes = node.SelectNodes("behavior");
            foreach (XmlNode behaviorNode in behaviorNodes) {
                if (behaviorNode?.Attributes == null) {
                    throw new ConstraintException("Null value found for behavior node");
                }

                var behavior = new FlatBehavior {
                    Name = behaviorNode.Attributes["name"].Value,
                    Id = behaviorNode.Attributes["id"].Value,
                    Type = behaviorNode.Attributes["type"].Value,
                    Source = behaviorNode.Attributes["source"]?.Value,
                };

                traitNodes = behaviorNode.SelectNodes("trait");
                foreach (XmlNode traitNode in traitNodes) {
                    string traitName = traitNode.Attributes["value"].Value;
                    behavior.Trait.Add(traitName);
                }

                type.Behaviors.Add(behavior.Name, behavior);
            }
        }

        return types;
    }

    public void CliExplorer() {
        Console.WriteLine("Explorer is ready");
        while (true) {
            string[] input = (Console.ReadLine() ?? string.Empty).Split(" ", 2);

            switch (input[0]) {
                case "quit":
                    return;
                case "type":
                case "prop":
                case "properties":
                    if (input.Length < 2) {
                        Console.WriteLine("Invalid input.");
                    } else {
                        string name = input[1];
                        FlatType type = GetType(name);
                        if (type == null) {
                            Console.WriteLine($"Invalid type: {name}");
                            continue;
                        }

                        Console.WriteLine(type);
                        foreach (FlatProperty prop in type.GetProperties()) {
                            Console.WriteLine($"{prop.Type,22}{prop.Name,30}: {prop.ValueString()}");
                        }

                        Console.WriteLine("----------------------Inherited------------------------");
                        foreach (FlatProperty prop in type.GetInheritedProperties()) {
                            Console.WriteLine($"{prop.Type,22}{prop.Name,30}: {prop.ValueString()}");
                        }
                    }
                    break;
                case "sub":
                case "children":
                    if (input.Length < 2) {
                        Console.WriteLine("Invalid input.");
                    } else {
                        string name = input[1];
                        FlatType type = GetType(name);
                        if (type == null) {
                            Console.WriteLine($"Invalid type: {name}");
                            continue;
                        }

                        Console.WriteLine(type);
                        foreach (FlatType subType in GetSubTypes(name)) {
                            Console.WriteLine($"{subType.Name,30} : {string.Join(',', subType.Mixin.Select(sub => sub.Name))}");
                        }
                    }
                    break;
                case "find":
                    if (input.Length < 3) {
                        Console.WriteLine("Invalid input.");
                    } else {
                        string name = input[1];
                        FlatType type = GetType(name);
                        if (type == null) {
                            Console.WriteLine($"Invalid type: {name}");
                            continue;
                        }

                        string field = input[2];
                        if (type.Properties.ContainsKey(field)) {
                            Console.WriteLine(type.Name);
                        }

                        foreach (FlatType parent in type.Mixin) {
                            if (parent.Properties.ContainsKey(field)) {
                                Console.WriteLine(parent.Name);
                            }
                        }
                    }
                    break;
                case "ls":
                    try {
                        bool recursive = input.Contains("-r");
                        string path = input.FirstOrDefault(arg => arg != "ls" && arg != "-r");
                        Console.WriteLine(string.Join(", ", Hierarchy.List(path, recursive).Select(type => type.Name)));
                    } catch (DirectoryNotFoundException e) {
                        Console.WriteLine(e.Message);
                    }
                    break;
                case "lsdir":
                    try {
                        string path = input.FirstOrDefault(arg => arg != "lsdir");
                        Console.WriteLine(string.Join(", ", Hierarchy.ListDirectories(path)));
                    } catch (DirectoryNotFoundException e) {
                        Console.WriteLine(e.Message);
                    }
                    break;
                default:
                    Console.WriteLine($"Unknown command: {string.Join(' ', input)}");
                    break;
            }
        }
    }

    private class FlatTypeNode {
        public readonly FlatType Value;
        public readonly List<FlatTypeNode> Children;

        public FlatTypeNode(FlatType value) {
            Value = value;
            Children = new List<FlatTypeNode>();
        }
    }
}
