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

    // Values from the converted TMS2 data: Thief 10700191 and 10700181, Assassin 10800041, Priest 10400251 and
    // 10400243, Wizard 10300279.
    [TestMethod]
    public void TestBalanceAdditionalEffectFields() {
        const string xml = """
            <ms2>
              <level>
                <BasicProperty level="1" />
                <StatusProperty>
                  <SpecialAbility aevprate="0.03" cap_ignore_limit_extrarate="0.2" reduce_do_heal_hprate="0.9" cap_ignore_limitvalue="20" cad_ignore_limitvalue="200" />
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
        Assert.AreEqual(20, data.StatusProperty.SpecialAbility.cap_ignore_limitvalue);
        Assert.AreEqual(200, data.StatusProperty.SpecialAbility.cad_ignore_limitvalue);
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
    // TMS2 table/stance.xml, punch row, verbatim.
    [TestMethod]
    public void TestStance() {
        const string xml = """
            <ms2>
              <v stanceAdditionalID="11000171" hitCountAdditinoalID="11000172" damageSkillIDs="11000171,11000192,11000182,11000183,11000316,11000321,11000322,11000323" finishStanceSkillIDs="11000144,11000145,11000146">
                <level level="1" hitCount="0" multiplyDamageRate="0" maxAccDamageRate="0" />
                <level level="2" hitCount="20" multiplyDamageRate="300" maxAccDamageRate="50000" />
                <level level="3" hitCount="35" multiplyDamageRate="500" maxAccDamageRate="100000" />
              </v>
            </ms2>
            """;
        var serializer = new System.Xml.Serialization.XmlSerializer(typeof(Maple2.File.Parser.Xml.Table.StanceRoot));
        var root = (Maple2.File.Parser.Xml.Table.StanceRoot) serializer.Deserialize(new StringReader(xml))!;
        Maple2.File.Parser.Xml.Table.Stance stance = root.v.Single();

        Assert.AreEqual(11000171, stance.stanceAdditionalID);
        Assert.AreEqual(11000172, stance.hitCountAdditinoalID);
        CollectionAssert.AreEqual(new[] { 11000171, 11000192, 11000182, 11000183, 11000316, 11000321, 11000322, 11000323 }, stance.damageSkillIDs);
        CollectionAssert.AreEqual(new[] { 11000144, 11000145, 11000146 }, stance.finishStanceSkillIDs);
        Assert.AreEqual(3, stance.level.Count);
        Assert.AreEqual(2, stance.level[1].level);
        Assert.AreEqual(20, stance.level[1].hitCount);
        Assert.AreEqual(300, stance.level[1].multiplyDamageRate);
        Assert.AreEqual(100000, stance.level[2].maxAccDamageRate);
    }

    // Shockwave Punch 11000171 level 1 in TMS2 SkillData/110.xml.
    [TestMethod]
    public void TestSkillLevelStanceDamageRate() {
        WithCulture(CommaDecimalCulture, () => {
            const string xml = """<ms2><level value="1" stanceDamageRate="4.66" /></ms2>""";
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(Maple2.File.Parser.Xml.Skill.SkillData));
            var data = (Maple2.File.Parser.Xml.Skill.SkillData) serializer.Deserialize(new StringReader(xml))!;
            Assert.AreEqual(4.66f, data.level.Single().stanceDamageRate);
        });
    }

    // Wizard 10300252 (Little Meteor mark) and HeavyGunner 10600280 in TMS2 Additional/103.xml and 106.xml,
    // as the balance converter writes them in the old format.
    [TestMethod]
    public void TestSpreadAndRemoveRechargingCount() {
        const string xml = """
            <ms2>
              <level>
                <BasicProperty level="1" />
                <SpreadProperty type="1" count="1">
                  <rangeProperty rangeType="cylinder" distance="500" height="300" applyTarget="1" />
                </SpreadProperty>
                <RemoveRechargingCountProperty skillID="10600181" count="2" />
              </level>
            </ms2>
            """;
        var serializer = new System.Xml.Serialization.XmlSerializer(typeof(AdditionalEffectLevelData));
        var root = (AdditionalEffectLevelData) serializer.Deserialize(new StringReader(xml))!;
        AdditionalEffectData data = root.level.Single();

        Assert.AreEqual(1, data.SpreadProperty.type);
        Assert.AreEqual(1, data.SpreadProperty.count);
        Assert.AreEqual("cylinder", data.SpreadProperty.rangeProperty.rangeType);
        Assert.AreEqual(500f, data.SpreadProperty.rangeProperty.distance);
        Assert.AreEqual(300f, data.SpreadProperty.rangeProperty.height);
        Assert.AreEqual(1, data.SpreadProperty.rangeProperty.applyTarget);
        Assert.AreEqual(10600181, data.RemoveRechargingCountProperty.skillID);
        Assert.AreEqual(2, data.RemoveRechargingCountProperty.count);
    }

    [TestMethod]
    public void TestSpreadAndRemoveRechargingCountAbsent() {
        const string xml = """<ms2><level><BasicProperty level="1" /></level></ms2>""";
        var serializer = new System.Xml.Serialization.XmlSerializer(typeof(AdditionalEffectLevelData));
        var root = (AdditionalEffectLevelData) serializer.Deserialize(new StringReader(xml))!;
        AdditionalEffectData data = root.level.Single();

        Assert.IsNull(data.SpreadProperty);
        Assert.IsNull(data.RemoveRechargingCountProperty);
    }
}
