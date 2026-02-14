using System.Drawing;
using System.Numerics;
using Maple2.File.Parser.Flat;
using Maple2.File.Parser.Flat.Convert;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Maple2.File.Tests;

[TestClass]
public class FlatTypeIndexSerializationTest {
    private static FlatTypeIndex BuildTestIndex() {
        // Build a FlatTypeIndex by serializing synthetic data, then deserializing.
        // We create types with mixins, properties of every type, and behaviors.
        using var ms = new MemoryStream();
        using (var writer = new BinaryWriter(ms, System.Text.Encoding.UTF8, leaveOpen: true)) {
            writer.Write((uint) 0x00495446); // magic "FTI\0"
            writer.Write(1); // version
            writer.Write("flat"); // root

            writer.Write(3); // type count

            // Type 0: BaseEntity (no mixins, one property, one behavior)
            WriteType(writer, "BaseEntity", 1, "flat/baseentity.flat",
                traits: new[] { "renderable" },
                mixinNames: Array.Empty<string>(),
                properties: new[] {
                    MakeBoolProperty("IsVisible", "prop-001", null, true),
                },
                behaviors: new[] {
                    MakeBehavior("OnSpawn", "beh-001", "Script", null, new[] { "startup" }),
                });

            // Type 1: SpawnPoint (mixes in BaseEntity, various property types)
            WriteType(writer, "SpawnPoint", 2, "flat/spawnpoint.flat",
                traits: Array.Empty<string>(),
                mixinNames: new[] { "BaseEntity" },
                properties: new[] {
                    MakeStringProperty("EntityName", "prop-010", "SpawnPoint", "MySpawn"),
                    MakeUInt32Property("SpawnId", "prop-011", null, 42),
                    MakeSInt32Property("Priority", "prop-012", null, -5),
                    MakeUInt16Property("Level", "prop-013", null, 60),
                    MakeFloat32Property("SpawnRadius", "prop-014", null, 3.5f),
                    MakeFloat64Property("Precision", "prop-015", null, 1.23456789),
                    MakePoint3Property("Position", "prop-016", null, new Vector3(1.0f, 2.0f, 3.0f)),
                    MakePoint2Property("Offset2D", "prop-017", null, new Vector2(10.5f, 20.5f)),
                    MakeColorProperty("Tint", "prop-018", null, Color.FromArgb(128, 64, 32)),
                    MakeColorAProperty("FullColor", "prop-019", null, Color.FromArgb(200, 100, 50, 25)),
                    MakeEntityRefProperty("TargetRef", "prop-020", "SpawnPoint", "some-guid-value"),
                    MakeAssetIdProperty("ModelAsset", "prop-021", null, "urn:llid:abc-123"),
                },
                behaviors: Array.Empty<(string, string, string, string, string[])>());

            // Type 2: AssocTest (tests all associative types + behavior with source)
            WriteType(writer, "AssocTest", 3, "flat/assoctest.flat",
                traits: new[] { "data", "complex" },
                mixinNames: new[] { "BaseEntity" },
                properties: new[] {
                    MakeAssocStringProperty("Labels", "prop-030", null,
                        new Dictionary<string, string> { ["a"] = "alpha", ["b"] = "beta" }),
                    MakeAssocPoint3Property("Waypoints", "prop-031", null,
                        new Dictionary<string, Vector3> {
                            ["start"] = new Vector3(0, 0, 0),
                            ["end"] = new Vector3(10, 20, 30),
                        }),
                    MakeAssocUInt32Property("Scores", "prop-032", null,
                        new Dictionary<string, uint> { ["player1"] = 100, ["player2"] = 200 }),
                    MakeAssocSInt32Property("Offsets", "prop-033", null,
                        new Dictionary<string, int> { ["x"] = -10, ["y"] = 20 }),
                },
                behaviors: new[] {
                    MakeBehavior("OnUpdate", "beh-010", "Lua", "AssocTest", Array.Empty<string>()),
                });
        }

        ms.Position = 0;
        using var reader = new BinaryReader(ms);
        return FlatTypeIndex.Deserialize(reader);
    }

    [TestMethod]
    public void RoundTrip_TypeCount() {
        FlatTypeIndex index = BuildTestIndex();
        Assert.AreEqual(3, index.GetAllTypes().Count());
    }

    [TestMethod]
    public void RoundTrip_TypeLookup() {
        FlatTypeIndex index = BuildTestIndex();

        Assert.IsNotNull(index.GetType("BaseEntity"));
        Assert.IsNotNull(index.GetType("SpawnPoint"));
        Assert.IsNotNull(index.GetType("AssocTest"));
        Assert.IsNull(index.GetType("NonExistent"));
    }

    [TestMethod]
    public void RoundTrip_TypeLookupCaseInsensitive() {
        FlatTypeIndex index = BuildTestIndex();

        Assert.IsNotNull(index.GetType("baseentity"));
        Assert.IsNotNull(index.GetType("SPAWNPOINT"));
        Assert.IsNotNull(index.GetType("assocTest"));
    }

    [TestMethod]
    public void RoundTrip_TypeFields() {
        FlatTypeIndex index = BuildTestIndex();

        FlatType baseEntity = index.GetType("BaseEntity");
        Assert.AreEqual("BaseEntity", baseEntity.Name);
        Assert.AreEqual((uint) 1, baseEntity.Id);
        Assert.AreEqual("flat/baseentity.flat", baseEntity.Path);
    }

    [TestMethod]
    public void RoundTrip_Traits() {
        FlatTypeIndex index = BuildTestIndex();

        FlatType baseEntity = index.GetType("BaseEntity");
        CollectionAssert.AreEqual(new[] { "renderable" }, baseEntity.Trait.ToArray());

        FlatType assocTest = index.GetType("AssocTest");
        CollectionAssert.AreEqual(new[] { "data", "complex" }, assocTest.Trait.ToArray());

        FlatType spawnPoint = index.GetType("SpawnPoint");
        Assert.AreEqual(0, spawnPoint.Trait.Count);
    }

    [TestMethod]
    public void RoundTrip_MixinResolution() {
        FlatTypeIndex index = BuildTestIndex();

        FlatType spawnPoint = index.GetType("SpawnPoint");
        Assert.AreEqual(1, spawnPoint.Mixin.Count);
        Assert.AreSame(index.GetType("BaseEntity"), spawnPoint.Mixin[0]);

        FlatType assocTest = index.GetType("AssocTest");
        Assert.AreEqual(1, assocTest.Mixin.Count);
        Assert.AreSame(index.GetType("BaseEntity"), assocTest.Mixin[0]);

        FlatType baseEntity = index.GetType("BaseEntity");
        Assert.AreEqual(0, baseEntity.Mixin.Count);
    }

    [TestMethod]
    public void RoundTrip_ParentChildRelationships() {
        FlatTypeIndex index = BuildTestIndex();

        // BaseEntity should have SpawnPoint and AssocTest as children
        List<FlatType> children = index.GetSubTypes("BaseEntity");
        Assert.AreEqual(2, children.Count);
        CollectionAssert.Contains(children.Select(c => c.Name).ToList(), "SpawnPoint");
        CollectionAssert.Contains(children.Select(c => c.Name).ToList(), "AssocTest");

        // SpawnPoint has no children
        Assert.AreEqual(0, index.GetSubTypes("SpawnPoint").Count);
    }

    [TestMethod]
    public void RoundTrip_BooleanProperty() {
        FlatTypeIndex index = BuildTestIndex();
        FlatProperty prop = index.GetType("BaseEntity").Properties["IsVisible"];
        Assert.AreEqual("Boolean", prop.Type);
        Assert.AreEqual(true, prop.Value);
        Assert.AreEqual("prop-001", prop.Id);
        Assert.IsNull(prop.Source);
    }

    [TestMethod]
    public void RoundTrip_StringProperty() {
        FlatTypeIndex index = BuildTestIndex();
        FlatProperty prop = index.GetType("SpawnPoint").Properties["EntityName"];
        Assert.AreEqual("String", prop.Type);
        Assert.AreEqual("MySpawn", prop.Value);
        Assert.AreEqual("SpawnPoint", prop.Source);
    }

    [TestMethod]
    public void RoundTrip_UInt32Property() {
        FlatTypeIndex index = BuildTestIndex();
        FlatProperty prop = index.GetType("SpawnPoint").Properties["SpawnId"];
        Assert.AreEqual("UInt32", prop.Type);
        Assert.AreEqual((uint) 42, prop.Value);
    }

    [TestMethod]
    public void RoundTrip_SInt32Property() {
        FlatTypeIndex index = BuildTestIndex();
        FlatProperty prop = index.GetType("SpawnPoint").Properties["Priority"];
        Assert.AreEqual("SInt32", prop.Type);
        Assert.AreEqual(-5, prop.Value);
    }

    [TestMethod]
    public void RoundTrip_UInt16Property() {
        FlatTypeIndex index = BuildTestIndex();
        FlatProperty prop = index.GetType("SpawnPoint").Properties["Level"];
        Assert.AreEqual("UInt16", prop.Type);
        Assert.AreEqual((ushort) 60, prop.Value);
    }

    [TestMethod]
    public void RoundTrip_Float32Property() {
        FlatTypeIndex index = BuildTestIndex();
        FlatProperty prop = index.GetType("SpawnPoint").Properties["SpawnRadius"];
        Assert.AreEqual("Float32", prop.Type);
        Assert.AreEqual(3.5f, prop.Value);
    }

    [TestMethod]
    public void RoundTrip_Float64Property() {
        FlatTypeIndex index = BuildTestIndex();
        FlatProperty prop = index.GetType("SpawnPoint").Properties["Precision"];
        Assert.AreEqual("Float64", prop.Type);
        Assert.AreEqual(1.23456789, prop.Value);
    }

    [TestMethod]
    public void RoundTrip_Point3Property() {
        FlatTypeIndex index = BuildTestIndex();
        FlatProperty prop = index.GetType("SpawnPoint").Properties["Position"];
        Assert.AreEqual("Point3", prop.Type);
        Assert.AreEqual(new Vector3(1.0f, 2.0f, 3.0f), prop.Value);
    }

    [TestMethod]
    public void RoundTrip_Point2Property() {
        FlatTypeIndex index = BuildTestIndex();
        FlatProperty prop = index.GetType("SpawnPoint").Properties["Offset2D"];
        Assert.AreEqual("Point2", prop.Type);
        Assert.AreEqual(new Vector2(10.5f, 20.5f), prop.Value);
    }

    [TestMethod]
    public void RoundTrip_ColorProperty() {
        FlatTypeIndex index = BuildTestIndex();
        FlatProperty prop = index.GetType("SpawnPoint").Properties["Tint"];
        Assert.AreEqual("Color", prop.Type);
        Color c = (Color) prop.Value;
        Assert.AreEqual(128, c.R);
        Assert.AreEqual(64, c.G);
        Assert.AreEqual(32, c.B);
        Assert.AreEqual(255, c.A); // Color type always has alpha=255
    }

    [TestMethod]
    public void RoundTrip_ColorAProperty() {
        FlatTypeIndex index = BuildTestIndex();
        FlatProperty prop = index.GetType("SpawnPoint").Properties["FullColor"];
        Assert.AreEqual("ColorA", prop.Type);
        Color c = (Color) prop.Value;
        Assert.AreEqual(200, c.A);
        Assert.AreEqual(100, c.R);
        Assert.AreEqual(50, c.G);
        Assert.AreEqual(25, c.B);
    }

    [TestMethod]
    public void RoundTrip_EntityRefProperty() {
        FlatTypeIndex index = BuildTestIndex();
        FlatProperty prop = index.GetType("SpawnPoint").Properties["TargetRef"];
        Assert.AreEqual("EntityRef", prop.Type);
        Assert.AreEqual("some-guid-value", prop.Value);
    }

    [TestMethod]
    public void RoundTrip_AssetIdProperty() {
        FlatTypeIndex index = BuildTestIndex();
        FlatProperty prop = index.GetType("SpawnPoint").Properties["ModelAsset"];
        Assert.AreEqual("AssetID", prop.Type);
        Assert.AreEqual("urn:llid:abc-123", prop.Value);
    }

    [TestMethod]
    public void RoundTrip_AssocStringProperty() {
        FlatTypeIndex index = BuildTestIndex();
        FlatProperty prop = index.GetType("AssocTest").Properties["Labels"];
        Assert.AreEqual("AssocString", prop.Type);
        var dict = (Dictionary<string, string>) prop.Value;
        Assert.AreEqual(2, dict.Count);
        Assert.AreEqual("alpha", dict["a"]);
        Assert.AreEqual("beta", dict["b"]);
    }

    [TestMethod]
    public void RoundTrip_AssocPoint3Property() {
        FlatTypeIndex index = BuildTestIndex();
        FlatProperty prop = index.GetType("AssocTest").Properties["Waypoints"];
        Assert.AreEqual("AssocPoint3", prop.Type);
        var dict = (Dictionary<string, Vector3>) prop.Value;
        Assert.AreEqual(2, dict.Count);
        Assert.AreEqual(new Vector3(0, 0, 0), dict["start"]);
        Assert.AreEqual(new Vector3(10, 20, 30), dict["end"]);
    }

    [TestMethod]
    public void RoundTrip_AssocUInt32Property() {
        FlatTypeIndex index = BuildTestIndex();
        FlatProperty prop = index.GetType("AssocTest").Properties["Scores"];
        Assert.AreEqual("AssocUInt32", prop.Type);
        var dict = (Dictionary<string, uint>) prop.Value;
        Assert.AreEqual(2, dict.Count);
        Assert.AreEqual((uint) 100, dict["player1"]);
        Assert.AreEqual((uint) 200, dict["player2"]);
    }

    [TestMethod]
    public void RoundTrip_AssocSInt32Property() {
        FlatTypeIndex index = BuildTestIndex();
        FlatProperty prop = index.GetType("AssocTest").Properties["Offsets"];
        Assert.AreEqual("AssocSInt32", prop.Type);
        var dict = (Dictionary<string, int>) prop.Value;
        Assert.AreEqual(2, dict.Count);
        Assert.AreEqual(-10, dict["x"]);
        Assert.AreEqual(20, dict["y"]);
    }

    [TestMethod]
    public void RoundTrip_PropertyTraits() {
        FlatTypeIndex index = BuildTestIndex();
        // The IsVisible property has trait "visual" added in the test data
        FlatProperty prop = index.GetType("BaseEntity").Properties["IsVisible"];
        CollectionAssert.AreEqual(new[] { "visual" }, prop.Trait.ToArray());
    }

    [TestMethod]
    public void RoundTrip_Behavior() {
        FlatTypeIndex index = BuildTestIndex();
        FlatBehavior beh = index.GetType("BaseEntity").Behaviors["OnSpawn"];
        Assert.AreEqual("OnSpawn", beh.Name);
        Assert.AreEqual("beh-001", beh.Id);
        Assert.AreEqual("Script", beh.Type);
        Assert.IsNull(beh.Source);
        CollectionAssert.AreEqual(new[] { "startup" }, beh.Trait.ToArray());
    }

    [TestMethod]
    public void RoundTrip_BehaviorWithSource() {
        FlatTypeIndex index = BuildTestIndex();
        FlatBehavior beh = index.GetType("AssocTest").Behaviors["OnUpdate"];
        Assert.AreEqual("OnUpdate", beh.Name);
        Assert.AreEqual("Lua", beh.Type);
        Assert.AreEqual("AssocTest", beh.Source);
        Assert.AreEqual(0, beh.Trait.Count);
    }

    [TestMethod]
    public void RoundTrip_SerializeDeserializeSymmetry() {
        // Build an index from raw binary, then serialize it, then deserialize again
        // and verify both produce the same results.
        FlatTypeIndex original = BuildTestIndex();

        using var ms = new MemoryStream();
        using (var writer = new BinaryWriter(ms, System.Text.Encoding.UTF8, leaveOpen: true)) {
            original.Serialize(writer);
        }

        ms.Position = 0;
        FlatTypeIndex roundTripped;
        using (var reader = new BinaryReader(ms)) {
            roundTripped = FlatTypeIndex.Deserialize(reader);
        }

        // Verify same types exist
        Assert.AreEqual(
            original.GetAllTypes().Count(),
            roundTripped.GetAllTypes().Count());

        foreach (FlatType originalType in original.GetAllTypes()) {
            FlatType rtType = roundTripped.GetType(originalType.Name);
            Assert.IsNotNull(rtType, $"Missing type: {originalType.Name}");
            Assert.AreEqual(originalType.Id, rtType.Id);
            Assert.AreEqual(originalType.Path, rtType.Path);
            Assert.AreEqual(originalType.Mixin.Count, rtType.Mixin.Count);
            Assert.AreEqual(originalType.Properties.Count, rtType.Properties.Count);
            Assert.AreEqual(originalType.Behaviors.Count, rtType.Behaviors.Count);
        }
    }

    [TestMethod]
    public void RoundTrip_HierarchyRebuilt() {
        FlatTypeIndex index = BuildTestIndex();

        // Hierarchy should contain types by path
        List<FlatType> flatTypes = index.Hierarchy.List("flat", false).ToList();
        Assert.AreEqual(3, flatTypes.Count);
    }

    [TestMethod]
    public void Deserialize_InvalidMagic_Throws() {
        using var ms = new MemoryStream();
        using (var writer = new BinaryWriter(ms, System.Text.Encoding.UTF8, leaveOpen: true)) {
            writer.Write((uint) 0xDEADBEEF);
            writer.Write(1);
        }

        ms.Position = 0;
        using var reader = new BinaryReader(ms);
        Assert.ThrowsException<InvalidDataException>(() => FlatTypeIndex.Deserialize(reader));
    }

    [TestMethod]
    public void Deserialize_InvalidVersion_Throws() {
        using var ms = new MemoryStream();
        using (var writer = new BinaryWriter(ms, System.Text.Encoding.UTF8, leaveOpen: true)) {
            writer.Write((uint) 0x00495446); // valid magic
            writer.Write(99); // invalid version
        }

        ms.Position = 0;
        using var reader = new BinaryReader(ms);
        Assert.ThrowsException<InvalidDataException>(() => FlatTypeIndex.Deserialize(reader));
    }

    [TestMethod]
    public void Deserialize_MissingMixin_Throws() {
        using var ms = new MemoryStream();
        using (var writer = new BinaryWriter(ms, System.Text.Encoding.UTF8, leaveOpen: true)) {
            writer.Write((uint) 0x00495446);
            writer.Write(1);
            writer.Write("flat");
            writer.Write(1); // only 1 type

            // Type that references a mixin that doesn't exist
            writer.Write("Orphan"); // name
            writer.Write((uint) 99); // id
            writer.Write("flat/orphan.flat"); // path
            writer.Write(0); // trait count
            writer.Write(1); // mixin count = 1
            writer.Write("NonExistentMixin"); // mixin name that doesn't exist
            writer.Write(0); // property count
            writer.Write(0); // behavior count
        }

        ms.Position = 0;
        using var reader = new BinaryReader(ms);
        Assert.ThrowsException<InvalidDataException>(() => FlatTypeIndex.Deserialize(reader));
    }

    [TestMethod]
    public void RoundTrip_EmptyIndex() {
        using var ms = new MemoryStream();
        using (var writer = new BinaryWriter(ms, System.Text.Encoding.UTF8, leaveOpen: true)) {
            writer.Write((uint) 0x00495446);
            writer.Write(1);
            writer.Write("flat");
            writer.Write(0); // zero types
        }

        ms.Position = 0;
        FlatTypeIndex index;
        using (var reader = new BinaryReader(ms)) {
            index = FlatTypeIndex.Deserialize(reader);
        }

        Assert.AreEqual(0, index.GetAllTypes().Count());
    }

    // --- Helpers for writing binary test data ---

    private static void WriteType(BinaryWriter writer, string name, uint id, string path,
        string[] traits, string[] mixinNames,
        (string Name, string Id, string Source, string Type, string[] Traits, Action<BinaryWriter> WriteValue)[] properties,
        (string Name, string Id, string Type, string Source, string[] Traits)[] behaviors) {
        writer.Write(name);
        writer.Write(id);
        writer.Write(path);

        writer.Write(traits.Length);
        foreach (string t in traits) writer.Write(t);

        writer.Write(mixinNames.Length);
        foreach (string m in mixinNames) writer.Write(m);

        writer.Write(properties.Length);
        foreach (var prop in properties) {
            writer.Write(prop.Name);
            writer.Write(prop.Id);
            bool hasSource = prop.Source != null;
            writer.Write(hasSource);
            if (hasSource) writer.Write(prop.Source);
            writer.Write(prop.Type);
            writer.Write(prop.Traits.Length);
            foreach (string t in prop.Traits) writer.Write(t);
            prop.WriteValue(writer);
        }

        writer.Write(behaviors.Length);
        foreach (var beh in behaviors) {
            writer.Write(beh.Name);
            writer.Write(beh.Id);
            writer.Write(beh.Type);
            bool hasSource = beh.Source != null;
            writer.Write(hasSource);
            if (hasSource) writer.Write(beh.Source);
            writer.Write(beh.Traits.Length);
            foreach (string t in beh.Traits) writer.Write(t);
        }
    }

    private static (string, string, string, string, string[], Action<BinaryWriter>) MakeBoolProperty(
        string name, string id, string source, bool value) =>
        (name, id, source, "Boolean", new[] { "visual" }, w => w.Write(value));

    private static (string, string, string, string, string[], Action<BinaryWriter>) MakeStringProperty(
        string name, string id, string source, string value) =>
        (name, id, source, "String", Array.Empty<string>(), w => w.Write(value));

    private static (string, string, string, string, string[], Action<BinaryWriter>) MakeEntityRefProperty(
        string name, string id, string source, string value) =>
        (name, id, source, "EntityRef", Array.Empty<string>(), w => w.Write(value));

    private static (string, string, string, string, string[], Action<BinaryWriter>) MakeAssetIdProperty(
        string name, string id, string source, string value) =>
        (name, id, source, "AssetID", Array.Empty<string>(), w => w.Write(value));

    private static (string, string, string, string, string[], Action<BinaryWriter>) MakeUInt32Property(
        string name, string id, string source, uint value) =>
        (name, id, source, "UInt32", Array.Empty<string>(), w => w.Write(value));

    private static (string, string, string, string, string[], Action<BinaryWriter>) MakeSInt32Property(
        string name, string id, string source, int value) =>
        (name, id, source, "SInt32", Array.Empty<string>(), w => w.Write(value));

    private static (string, string, string, string, string[], Action<BinaryWriter>) MakeUInt16Property(
        string name, string id, string source, ushort value) =>
        (name, id, source, "UInt16", Array.Empty<string>(), w => w.Write(value));

    private static (string, string, string, string, string[], Action<BinaryWriter>) MakeFloat32Property(
        string name, string id, string source, float value) =>
        (name, id, source, "Float32", Array.Empty<string>(), w => w.Write(value));

    private static (string, string, string, string, string[], Action<BinaryWriter>) MakeFloat64Property(
        string name, string id, string source, double value) =>
        (name, id, source, "Float64", Array.Empty<string>(), w => w.Write(value));

    private static (string, string, string, string, string[], Action<BinaryWriter>) MakePoint3Property(
        string name, string id, string source, Vector3 value) =>
        (name, id, source, "Point3", Array.Empty<string>(), w => { w.Write(value.X); w.Write(value.Y); w.Write(value.Z); });

    private static (string, string, string, string, string[], Action<BinaryWriter>) MakePoint2Property(
        string name, string id, string source, Vector2 value) =>
        (name, id, source, "Point2", Array.Empty<string>(), w => { w.Write(value.X); w.Write(value.Y); });

    private static (string, string, string, string, string[], Action<BinaryWriter>) MakeColorProperty(
        string name, string id, string source, Color value) =>
        (name, id, source, "Color", Array.Empty<string>(), w => { w.Write(value.R); w.Write(value.G); w.Write(value.B); });

    private static (string, string, string, string, string[], Action<BinaryWriter>) MakeColorAProperty(
        string name, string id, string source, Color value) =>
        (name, id, source, "ColorA", Array.Empty<string>(), w => { w.Write(value.A); w.Write(value.R); w.Write(value.G); w.Write(value.B); });

    private static (string, string, string, string, string[], Action<BinaryWriter>) MakeAssocStringProperty(
        string name, string id, string source, Dictionary<string, string> value) =>
        (name, id, source, "AssocString", Array.Empty<string>(), w => {
            w.Write(value.Count);
            foreach (var kvp in value) { w.Write(kvp.Key); w.Write(kvp.Value); }
        });

    private static (string, string, string, string, string[], Action<BinaryWriter>) MakeAssocPoint3Property(
        string name, string id, string source, Dictionary<string, Vector3> value) =>
        (name, id, source, "AssocPoint3", Array.Empty<string>(), w => {
            w.Write(value.Count);
            foreach (var kvp in value) { w.Write(kvp.Key); w.Write(kvp.Value.X); w.Write(kvp.Value.Y); w.Write(kvp.Value.Z); }
        });

    private static (string, string, string, string, string[], Action<BinaryWriter>) MakeAssocUInt32Property(
        string name, string id, string source, Dictionary<string, uint> value) =>
        (name, id, source, "AssocUInt32", Array.Empty<string>(), w => {
            w.Write(value.Count);
            foreach (var kvp in value) { w.Write(kvp.Key); w.Write(kvp.Value); }
        });

    private static (string, string, string, string, string[], Action<BinaryWriter>) MakeAssocSInt32Property(
        string name, string id, string source, Dictionary<string, int> value) =>
        (name, id, source, "AssocSInt32", Array.Empty<string>(), w => {
            w.Write(value.Count);
            foreach (var kvp in value) { w.Write(kvp.Key); w.Write(kvp.Value); }
        });

    private static (string Name, string Id, string Type, string Source, string[] Traits) MakeBehavior(
        string name, string id, string type, string source, string[] traits) =>
        (name, id, type, source, traits);
}

[TestClass]
public class AssetIndexSerializationTest {
    [TestMethod]
    public void RoundTrip_EmptyIndex() {
        using var ms = new MemoryStream();
        using (var writer = new BinaryWriter(ms, System.Text.Encoding.UTF8, leaveOpen: true)) {
            writer.Write((uint) 0x00495341);
            writer.Write(1);
            writer.Write(0); // llidLookup count
            writer.Write(0); // ntLookup count
        }

        ms.Position = 0;
        using var reader = new BinaryReader(ms);
        AssetIndex index = AssetIndex.Deserialize(reader);
        Assert.IsNotNull(index);
    }

    [TestMethod]
    public void RoundTrip_SerializeDeserialize() {
        // Build an AssetIndex from binary, serialize it, deserialize again
        AssetIndex original = BuildTestAssetIndex();

        using var ms = new MemoryStream();
        using (var writer = new BinaryWriter(ms, System.Text.Encoding.UTF8, leaveOpen: true)) {
            original.Serialize(writer);
        }

        ms.Position = 0;
        AssetIndex roundTripped;
        using (var reader = new BinaryReader(ms)) {
            roundTripped = AssetIndex.Deserialize(reader);
        }

        // Verify GetFields works the same on both — need all NtTagFiles present
        var (name1, path1, tags1) = original.GetFields("urn:llid:test-llid-1");
        var (name2, path2, tags2) = roundTripped.GetFields("urn:llid:test-llid-1");
        Assert.AreEqual(name1, name2);
        Assert.AreEqual(path1, path2);
        Assert.AreEqual(tags1, tags2);
        Assert.AreEqual("TestAsset", name2);
        Assert.AreEqual("/models/test.nif", path2);
        Assert.IsTrue(tags2.Contains("model"));
    }

    [TestMethod]
    public void Deserialize_InvalidMagic_Throws() {
        using var ms = new MemoryStream();
        using (var writer = new BinaryWriter(ms, System.Text.Encoding.UTF8, leaveOpen: true)) {
            writer.Write((uint) 0xDEADBEEF);
            writer.Write(1);
        }

        ms.Position = 0;
        using var reader = new BinaryReader(ms);
        Assert.ThrowsException<InvalidDataException>(() => AssetIndex.Deserialize(reader));
    }

    [TestMethod]
    public void Deserialize_InvalidVersion_Throws() {
        using var ms = new MemoryStream();
        using (var writer = new BinaryWriter(ms, System.Text.Encoding.UTF8, leaveOpen: true)) {
            writer.Write((uint) 0x00495341);
            writer.Write(99);
        }

        ms.Position = 0;
        using var reader = new BinaryReader(ms);
        Assert.ThrowsException<InvalidDataException>(() => AssetIndex.Deserialize(reader));
    }

    private static AssetIndex BuildTestAssetIndex() {
        // All NtTagFiles that GetFields iterates over
        string[] ntTagFiles = {
            "application", "cn", "dds", "emergent-flat-model", "emergent-world",
            "fx-shader-compiled", "gamebryo-animation", "gamebryo-scenegraph",
            "gamebryo-sequence-file", "image", "jp", "kr", "lua-behavior",
            "model", "png", "precache", "script", "shader", "x-shockwave-flash", "x-world",
        };

        using var ms = new MemoryStream();
        using (var writer = new BinaryWriter(ms, System.Text.Encoding.UTF8, leaveOpen: true)) {
            writer.Write((uint) 0x00495341); // magic
            writer.Write(1); // version

            // llidLookup: 1 entry mapping "test-llid-1" -> ["urn:uuid:abc-123"]
            writer.Write(1);
            writer.Write("test-llid-1");
            writer.Write(1);
            writer.Write("urn:uuid:abc-123");

            // ntLookup: name + relpath + all 20 tag files = 22 entries
            writer.Write(2 + ntTagFiles.Length);

            writer.Write("name");
            writer.Write(1);
            writer.Write("urn:uuid:abc-123");
            writer.Write("TestAsset");

            writer.Write("relpath");
            writer.Write(1);
            writer.Write("urn:uuid:abc-123");
            writer.Write("/models/test.nif");

            // Write all tag files; only "model" has the uuid
            foreach (string tag in ntTagFiles) {
                writer.Write(tag);
                if (tag == "model") {
                    writer.Write(1);
                    writer.Write("urn:uuid:abc-123");
                    writer.Write("true");
                } else {
                    writer.Write(0); // empty dict
                }
            }
        }

        ms.Position = 0;
        using var reader = new BinaryReader(ms);
        return AssetIndex.Deserialize(reader);
    }
}
