using System.Globalization;
using System.Numerics;
using Maple2.File.Parser.Flat;
using Maple2.File.Parser.Flat.Convert;
using Maple2.File.Parser.Xml.AdditionalEffect;
using Maple2.File.Parser.Xml.Item;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Maple2.File.Tests;

// These tests need no game data, so they run without MS2_DATA_FOLDER.
[TestClass]
public class DataFreeParsingTest {
    // pt-BR reads "." as a thousands separator, so a culture-sensitive parse turns "1.5" into 15.
    private static readonly CultureInfo CommaDecimalCulture = new("pt-BR");

    private static void WithCulture(CultureInfo culture, Action action) {
        CultureInfo previous = CultureInfo.CurrentCulture;
        CultureInfo.CurrentCulture = culture;
        try {
            action();
        } finally {
            CultureInfo.CurrentCulture = previous;
        }
    }

    [TestMethod]
    public void TestParseNtFileAcceptsLfAndCrlf() {
        const string lf = "<urn:uuid:ba8762a8-e692-43f2-a0ba-0a1bfb1c42a9> <http://emergent.net/aweb/1.0/tag> \"application\".\n"
                          + "<urn:uuid:9e0ae6be-a0b1-4c2d-8e3f-000000000001> <http://emergent.net/aweb/1.0/tag> \"application\".\n";
        string crlf = lf.Replace("\n", "\r\n");

        foreach (string data in new[] { lf, crlf }) {
            Dictionary<string, string> result = AssetIndex.ParseNtFile(data);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("application", result["urn:uuid:ba8762a8-e692-43f2-a0ba-0a1bfb1c42a9"]);
        }
    }

    [TestMethod]
    public void TestFlatPropertyIgnoresCurrentCulture() {
        WithCulture(CommaDecimalCulture, () => {
            Assert.AreEqual(1.5f, FlatProperty.ParseType("Float32", "1.5"));
            Assert.AreEqual(1.5d, FlatProperty.ParseType("Float64", "1.5"));
            Assert.AreEqual(new Vector3(1.5f, -2.25f, 3f), FlatProperty.ParseType("Point3", "1.5, -2.25, 3"));

            var property = new FlatProperty { Name = "Scale", Type = "Float32", Value = 1.5f };
            Assert.AreEqual("1.5f", property.ValueCodeString());
        });
    }

    [TestMethod]
    public void TestGeneratedVector3IgnoresCurrentCulture() {
        WithCulture(CommaDecimalCulture, () => {
            var install = new Install { _physXdimension = "1.5,-2.25,3" };
            Assert.AreEqual(new Vector3(1.5f, -2.25f, 3f), install.physXdimension);
            Assert.AreEqual("1.5,-2.25,3", install._physXdimension);
        });
    }

    [TestMethod]
    public void TestGeneratedFloatArrayIgnoresCurrentCulture() {
        WithCulture(CommaDecimalCulture, () => {
            var property = new InvokeEffectProperty { _values = "0.5,1.25,-3" };
            CollectionAssert.AreEqual(new[] { 0.5f, 1.25f, -3f }, property.values);
            Assert.AreEqual("0.5,1.25,-3", property._values);
        });
    }

    // Values from the converted TMS2 data: Thief 10700191, Assassin 10800041, Priest 10400251, Wizard 10300279.
    [TestMethod]
    public void TestBalanceAdditionalEffectFields() {
        const string xml = """
            <ms2>
              <level>
                <BasicProperty level="1" />
                <StatusProperty>
                  <SpecialAbility aevprate="0.03" cap_ignore_limit_extrarate="0.2" reduce_do_heal_hprate="0.9" />
                </StatusProperty>
                <ModifySkillCooldownProperty skillIDs="10300271" values="-0.5" />
              </level>
            </ms2>
            """;
        var serializer = new System.Xml.Serialization.XmlSerializer(typeof(AdditionalEffectLevelData));
        var root = (AdditionalEffectLevelData) serializer.Deserialize(new StringReader(xml))!;
        AdditionalEffectData data = root.level.Single();

        Assert.AreEqual(0.03f, data.StatusProperty.SpecialAbility.aevprate);
        Assert.AreEqual(0.2f, data.StatusProperty.SpecialAbility.cap_ignore_limit_extrarate);
        Assert.AreEqual(0.9f, data.StatusProperty.SpecialAbility.reduce_do_heal_hprate);
        CollectionAssert.AreEqual(new[] { 10300271 }, data.ModifySkillCooldownProperty.skillIDs);
        CollectionAssert.AreEqual(new[] { -0.5f }, data.ModifySkillCooldownProperty.values);
    }

    // Wizard 10300222's converted conditionSkill target.
    [TestMethod]
    public void TestHasNotBuffOwner() {
        var target = new Maple2.File.Parser.Xml.Skill.SubConditionTarget { _hasNotBuffID = "10300227", _hasNotBuffOwner = "1" };
        CollectionAssert.AreEqual(new[] { 10300227 }, target.hasNotBuffID);
        CollectionAssert.AreEqual(new[] { true }, target.hasNotBuffOwner);
    }
}
