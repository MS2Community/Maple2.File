using Maple2.File.Parser;
using Maple2.File.Parser.Enum;
using Maple2.File.Parser.Tools;
using Maple2.File.Parser.Xml.Npc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Maple2.File.Tests;

[TestClass]
public class NpcParserTest {
    [TestMethod]
    public void TestNpcParser() {
        var locale = Locale.NA;
        Filter.Load(TestUtils.XmlReader, locale.ToString(), "Live");
        var parser = new NpcParser(TestUtils.XmlReader, "en");

        // parser.NameSerializer.UnknownElement += TestUtils.UnknownElementHandler;
        // parser.NameSerializer.UnknownAttribute += TestUtils.UnknownAttributeHandler;
        // parser.NpcSerializer.UnknownElement += TestUtils.UnknownElementHandler;
        // parser.NpcSerializer.UnknownAttribute += TestUtils.UnknownAttributeHandler;

        int count = 0;
        foreach ((int id, string name, NpcData data, List<EffectDummy> dummy) in parser.Parse()) {
            // Debug.WriteLine($"Parsing Npc: {id} ({name})");
            Assert.IsTrue(id > 0);
            Assert.IsNotNull(data);
            count++;
        }
        Assert.AreEqual(7535, count);
    }

    [TestMethod]
    public void TestNpcAdditionalEffectGroup() {
        var locale = Locale.NA;
        Filter.Load(TestUtils.XmlReader, locale.ToString(), "Live");
        var parser = new NpcParser(TestUtils.XmlReader, "en");

        // npc 49300001 (Destroyer), ShadowExpedition: <additionalEffect codes="92900999" levels="1" group="101,2" />
        // npc 21000001 (Orange Mushroom): group=""
        Dictionary<int, NpcData> npcs = parser.Parse()
            .Where(entry => entry.Id is 49300001 or 21000001)
            .ToDictionary(entry => entry.Id, entry => entry.Data);
        CollectionAssert.AreEqual(new[] { 92900999 }, npcs[49300001].additionalEffect.codes);
        CollectionAssert.AreEqual(new[] { 101, 2 }, npcs[49300001].additionalEffect.group);
        Assert.AreEqual(0, npcs[21000001].additionalEffect.group.Length);
    }

    [TestMethod]
    public void TestNpcParserNew() {
        var locale = Locale.KR;
        Filter.Load(TestUtils.XmlReader, locale.ToString(), "Live");
        var parser = new NpcParser(TestUtils.XmlReader, "kr");

        // parser.NameSerializer.UnknownElement += TestUtils.UnknownElementHandler;
        // parser.NameSerializer.UnknownAttribute += TestUtils.UnknownAttributeHandler;
        // parser.NpcSerializer.UnknownElement += TestUtils.UnknownElementHandler;
        // parser.NpcSerializer.UnknownAttribute += TestUtils.UnknownAttributeHandler;

        int count = 0;
        foreach ((int id, string name, NpcDataNew data, List<EffectDummy> dummy) in parser.ParseNew()) {
            // Debug.WriteLine($"Parsing Npc: {id} ({name})");
            Assert.IsTrue(id > 0);
            Assert.IsNotNull(data);
            count++;
        }
        Assert.AreEqual(10795, count);
    }

    [TestMethod]
    public void TestNpcNameParser() {
        var locale = Locale.NA;
        string language = "en";
        Filter.Load(TestUtils.XmlReader, locale.ToString(), "Live");
        var parser = new NpcParser(TestUtils.XmlReader, language);

        int count = 0;
        foreach ((int id, string name) in parser.ParseNpcNames()) {
            // Debug.WriteLine($"Parsing Npc Name: {id} ({name})");
            Assert.IsTrue(id > 0);
            Assert.IsNotNull(name);
            count++;
        }
        switch (language) {
            case "en":
                Assert.AreEqual(7114, count);
                break;
            case "kr":
                Assert.AreEqual(7850, count);
                break;
        }
    }
}

