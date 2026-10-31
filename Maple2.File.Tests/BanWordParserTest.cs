using Maple2.File.Parser;
using Maple2.File.Parser.Enum;
using Maple2.File.Parser.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Maple2.File.Tests;

[TestClass]
public class BanWordParserTest {
    // Before banwordall.xml had its own method, ParseBanWords returned both files: 6179 words.
    [TestMethod]
    public void TestBanWordParser() {
        var locale = Locale.NA;
        Filter.Load(TestUtils.XmlReader, locale.ToString(), "Live");
        var parser = new BanWordParser(TestUtils.XmlReader);

        var banWords = new List<string>();
        foreach ((int id, string name) in parser.ParseBanWords()) {
            Assert.IsTrue(id >= 0);
            Assert.IsFalse(string.IsNullOrEmpty(name));
            banWords.Add(name);
        }
        var banWordAll = parser.ParseBanWordAll().Select(word => word.Name).ToList();

        Assert.AreEqual(6179, banWords.Count + banWordAll.Count);
        // en/banword.xml has "nazi"; "nig" is only ever in banwordall.xml.
        CollectionAssert.Contains(banWords, "nazi");
        CollectionAssert.DoesNotContain(banWords, "nig");
        CollectionAssert.Contains(banWordAll, "nig");
        CollectionAssert.DoesNotContain(banWordAll, "nazi");
    }

    [TestMethod]
    public void TestUgcBanWordParser() {
        var locale = Locale.NA;
        Filter.Load(TestUtils.XmlReader, locale.ToString(), "Live");
        var parser = new BanWordParser(TestUtils.XmlReader);

        int count = 0;
        foreach ((int id, string name) in parser.ParseUgcBanWords()) {
            Assert.IsTrue(id >= 0);
            Assert.IsFalse(string.IsNullOrEmpty(name));
            count++;
        }
        Assert.AreEqual(1208, count);
    }
}
