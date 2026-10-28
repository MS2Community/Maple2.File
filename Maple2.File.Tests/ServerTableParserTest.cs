using System.Numerics;
using Maple2.File.Parser;
using Maple2.File.Parser.Enum;
using Maple2.File.Parser.Xml.Table;
using Maple2.File.Parser.Xml.Table.Server;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Maple2.File.Tests;

[TestClass]
public class ServerTableParserTest {
    [TestMethod]
    public void TestNpcScriptCondition() {
        var parser = new ServerTableParser(TestUtils.ServerReader);

        foreach ((_, _) in parser.ParseNpcScriptCondition()) {
            continue;
        }
    }

    [TestMethod]
    public void TestQuestScriptCondition() {
        var parser = new ServerTableParser(TestUtils.ServerReader);

        foreach ((_, _) in parser.ParseQuestScriptCondition()) {
            continue;
        }
    }

    [TestMethod]
    public void TestNpcScriptFunction() {
        var parser = new ServerTableParser(TestUtils.ServerReader);

        foreach ((_, _) in parser.ParseNpcScriptFunction()) {
            continue;
        }
    }

    [TestMethod]
    public void TestQuestScriptFunction() {
        var parser = new ServerTableParser(TestUtils.ServerReader);

        foreach ((_, _) in parser.ParseQuestScriptFunction()) {
            continue;
        }
    }

    [TestMethod]
    public void TestJobConditionTable() {
        var parser = new ServerTableParser(TestUtils.ServerReader);

        foreach ((_, _) in parser.ParseJobConditionTable()) {
            continue;
        }
    }

    [TestMethod]
    public void TestScriptEventCondition() {
        var parser = new ServerTableParser(TestUtils.ServerReader);

        foreach ((_, _) in parser.ParseScriptEventCondition()) {
            continue;
        }
    }


    [TestMethod]
    public void TestUserStats() {
        var parser = new ServerTableParser(TestUtils.ServerReader);

        foreach ((_, _) in parser.ParseUserStat1()) {
            continue;
        }

        foreach ((_, _) in parser.ParseUserStat10()) {
            continue;
        }

        foreach ((_, _) in parser.ParseUserStat20()) {
            continue;
        }

        foreach ((_, _) in parser.ParseUserStat30()) {
            continue;
        }

        foreach ((_, _) in parser.ParseUserStat40()) {
            continue;
        }

        foreach ((_, _) in parser.ParseUserStat50()) {
            continue;
        }

        foreach ((_, _) in parser.ParseUserStat60()) {
            continue;
        }

        foreach ((_, _) in parser.ParseUserStat70()) {
            continue;
        }

        foreach ((_, _) in parser.ParseUserStat80()) {
            continue;
        }

        foreach ((_, _) in parser.ParseUserStat90()) {
            continue;
        }

        foreach ((_, _) in parser.ParseUserStat100()) {
            continue;
        }

        foreach ((_, _) in parser.ParseUserStat110()) {
            continue;
        }

        foreach ((_, _) in parser.ParseUserStat999()) {
            continue;
        }
    }

    [TestMethod]
    public void TestInstanceField() {
        var parser = new ServerTableParser(TestUtils.ServerReader);

        foreach ((_, _) in parser.ParseInstanceField()) {
            continue;
        }
    }

    [TestMethod]
    public void TestShopGameInfo() {
        var parser = new ServerTableParser(TestUtils.ServerReader);

        foreach ((_, _) in parser.ParseShopGameInfo()) {
            continue;
        }
    }

    [TestMethod]
    public void TestShopGame() {
        var parser = new ServerTableParser(TestUtils.ServerReader);

        foreach ((_, _) in parser.ParseShopGame()) {
            continue;
        }
    }

    [TestMethod]
    public void TestShopUgc() {
        var parser = new ServerTableParser(TestUtils.ServerReader);

        foreach ((_, _) in parser.ParseShopUgc()) {
            continue;
        }
    }

    [TestMethod]
    public void TestShopBeauty() {
        var parser = new ServerTableParser(TestUtils.ServerReader);

        foreach ((_, _) in parser.ParseShopBeauty()) {
            continue;
        }
    }

    [TestMethod]
    public void TestShopBeautyCoupon() {
        var parser = new ServerTableParser(TestUtils.ServerReader);

        foreach ((_, _) in parser.ParseShopBeautyCoupon()) {
            continue;
        }
    }

    [TestMethod]
    public void TestShopBeautySpecialHair() {
        var parser = new ServerTableParser(TestUtils.ServerReader);

        foreach ((_, _) in parser.ParseShopBeautySpecialHair()) {
            continue;
        }
    }

    [TestMethod]
    public void TestBonusGame() {
        var parser = new ServerTableParser(TestUtils.ServerReader);

        foreach ((_, _, _) in parser.ParseBonusGame()) {
            continue;
        }
    }

    [TestMethod]
    public void TestBonusGameDrop() {
        var parser = new ServerTableParser(TestUtils.ServerReader);

        foreach ((_, _, _) in parser.ParseBonusGameDrop()) {
            continue;
        }
    }

    [TestMethod]
    public void TestGlobalDropItemBox() {
        var parser = new ServerTableParser(TestUtils.ServerReader);

        foreach ((_, _) in parser.ParseGlobalDropItemBox()) {
            continue;
        }
    }

    [TestMethod]
    public void TestGlobalDropItemSet() {
        var parser = new ServerTableParser(TestUtils.ServerReader);

        foreach ((_, _) in parser.ParseGlobalDropItemSet()) {
            continue;
        }
    }

    [TestMethod]
    public void TestIndividualItemDrop() {
        var parser = new ServerTableParser(TestUtils.ServerReader);

        foreach ((_, _) in parser.ParseIndividualItemDrop()) {
            continue;
        }
    }

    [TestMethod]
    public void TestRoom() {
        var parser = new ServerTableParser(TestUtils.ServerReader);

        foreach ((_, _) in parser.ParseRoom()) {
            continue;
        }
    }

    [TestMethod]
    public void TestRandomRoom() {
        var parser = new ServerTableParser(TestUtils.ServerReader);

        foreach ((_, _) in parser.ParseRoomRandom()) {
            continue;
        }
    }

    [TestMethod]
    public void TestSpawnNpc() {
        var parser = new ServerTableParser(TestUtils.ServerReader);

        foreach ((_, _) in parser.ParseSpawnNpc()) {
            continue;
        }
    }

    [TestMethod]
    public void TestGroupSpawn() {
        var parser = new ServerTableParser(TestUtils.ServerReader);

        foreach ((_, _) in parser.ParseGroupSpawn()) {
            continue;
        }
    }

    [TestMethod]
    public void TestShadowExpeditionDrop() {
        var parser = new ServerTableParser(TestUtils.ServerReader);

        List<ShadowExpeditionDrop> drops = parser.ParseShadowExpeditionDrop().ToList();
        Assert.AreEqual(102, drops.Count);

        // <shadowExpeditionDrop step="1" class="1" dropBoxId="920001001" />
        Assert.AreEqual(1, drops[0].step);
        Assert.AreEqual(1, drops[0].npcClass);
        CollectionAssert.AreEqual(new[] { 920001001 }, drops[0].dropBoxId);

        // <shadowExpeditionDrop step="34" class="6" dropBoxId="920001002,920006034,920000034,820000034" />
        ShadowExpeditionDrop last = drops[^1];
        Assert.AreEqual(34, last.step);
        Assert.AreEqual(6, last.npcClass);
        CollectionAssert.AreEqual(new[] { 920001002, 920006034, 920000034, 820000034 }, last.dropBoxId);
    }

    [TestMethod]
    public void TestSpawnInteractObject() {
        var parser = new ServerTableParser(TestUtils.ServerReader);

        foreach ((_, _) in parser.ParseSpawnInteractObject()) {
            continue;
        }
    }

    [TestMethod]
    public void TestSpawnGroup() {
        var parser = new ServerTableParser(TestUtils.ServerReader);

        foreach ((_, _) in parser.ParseSpawnGroup()) {
            continue;
        }
    }

    [TestMethod]
    public void TestFish() {
        var parser = new ServerTableParser(TestUtils.ServerReader);

        foreach ((_, _) in parser.ParseFish()) {
            continue;
        }
    }

    [TestMethod]
    public void TestFishBox() {
        var parser = new ServerTableParser(TestUtils.ServerReader);

        foreach ((_, _) in parser.ParseGlobalFishBox()) {
            continue;
        }

        foreach ((_, _) in parser.ParseIndividualFishBox()) {
            continue;
        }
    }

    [TestMethod]
    public void TestFishingSpot() {
        var parser = new ServerTableParser(TestUtils.ServerReader);

        foreach ((_, _) in parser.ParseFishingSpot()) {
            continue;
        }
    }

    [TestMethod]
    public void TestFishLure() {
        var parser = new ServerTableParser(TestUtils.ServerReader);

        foreach ((_, _, _) in parser.ParseFishLure()) {
            continue;
        }
    }

    [TestMethod]
    public void TestAdventureIdExp() {
        var parser = new ServerTableParser(TestUtils.ServerReader);

        foreach ((_, _) in parser.ParseAdventureIdExp()) {
            continue;
        }
    }

    [TestMethod]
    public void TestAdventureExp() {
        var parser = new ServerTableParser(TestUtils.ServerReader);

        foreach ((_, _) in parser.ParseAdventureExp()) {
            continue;
        }
    }

    [TestMethod]
    public void TestTimeEventData() {
        var parser = new ServerTableParser(TestUtils.ServerReader);

        foreach ((_, _) in parser.ParseTimeEventData()) {
            continue;
        }
    }

    [TestMethod]
    public void TestOxQuiz() {
        var parser = new ServerTableParser(TestUtils.ServerReader);

        foreach ((_, _) in parser.ParseOxQuiz()) {
            continue;
        }
    }

    [TestMethod]
    public void TestGameEvent() {
        var parser = new ServerTableParser(TestUtils.ServerReader);

        foreach ((_, _) in parser.ParseGameEvent()) {
            continue;
        }
    }

    [TestMethod]
    public void TestUnlimitedEnchantOption() {
        var parser = new ServerTableParser(TestUtils.ServerReader);

        foreach ((_, _) in parser.ParseUnlimitedEnchantOption()) {
            continue;
        }
    }

    [TestMethod]
    public void TestItemMergeOption() {
        var parser = new ServerTableParser(TestUtils.ServerReader);

        foreach ((_, _) in parser.ParseItemMergeOption()) {
            continue;
        }
    }

    [TestMethod]
    public void TestEnchantOption() {
        var parser = new ServerTableParser(TestUtils.ServerReader);

        foreach ((_, _) in parser.ParseEnchantOption()) {
            continue;
        }
    }

    [TestMethod]
    public void TestMeretShop() {
        var parser = new ServerTableParser(TestUtils.ServerReader);

        foreach ((_, _) in parser.ParseShopMeret()) {
            continue;
        }
    }

    [TestMethod]
    public void TestMeretShopCustom() {
        var parser = new ServerTableParser(TestUtils.ServerReader);

        foreach ((_, _) in parser.ParseShopMeretCustom()) {
            continue;
        }
    }

    [TestMethod]
    public void TestItemOptionProbability() {
        var parser = new ServerTableParser(TestUtils.ServerReader);

        foreach ((_, _) in parser.ParseItemOptionProbability()) {
            continue;
        }
    }

    [TestMethod]
    public void TestItemOptionVariation() {
        var parser = new ServerTableParser(TestUtils.ServerReader);

        foreach ((_, _) in parser.ParseItemOptionVariation()) {
            continue;
        }
    }

    [TestMethod]
    public void TestItemOptionRandom() {
        var parser = new ServerTableParser(TestUtils.ServerReader);

        foreach ((_, _) in parser.ParseItemOptionRandom()) {
            continue;
        }
    }

    [TestMethod]
    public void TestConstants() {
        var parser = new ServerTableParser(TestUtils.ServerReader);

        foreach ((_, _) in parser.ParseConstants()) {
            continue;
        }
    }

    [TestMethod]
    public void TestNpcStatFactorByPlayerCount() {
        var parser = new ServerTableParser(TestUtils.ServerReader);

        foreach (NpcStatFactorByPlayerCount _ in parser.ParseNpcStatFactorByPlayerCount()) {
            continue;
        }
    }

    [TestMethod]
    public void TestNpcStatFactorByLevel() {
        var parser = new ServerTableParser(TestUtils.ServerReader);

        foreach (NpcStatFactorByLevel _ in parser.ParseNpcStatFactorByLevel()) {
            continue;
        }
    }

    [TestMethod]
    public void TestMaidGradeInfo() {
        var parser = new ServerTableParser(TestUtils.ServerReader);

        foreach ((_, _) in parser.ParseMaidGradeInfo()) {
            continue;
        }
    }

    [TestMethod]
    public void TestMaidRecipe() {
        var parser = new ServerTableParser(TestUtils.ServerReader);

        foreach ((_, _) in parser.ParseMaidRecipe()) {
            continue;
        }
    }

    [TestMethod]
    public void TestDefaultCharacterInfo() {
        var parser = new ServerTableParser(TestUtils.ServerReader);

        Dictionary<Gender, DefaultCharacterInfo> infos = parser.ParseDefaultCharacterInfo()
            .ToDictionary(entry => entry.Gender, entry => entry.Info);

        CollectionAssert.AreEquivalent(new[] { Gender.Male, Gender.Female }, infos.Keys.ToArray());

        foreach (DefaultCharacterInfo info in infos.Values) {
            Assert.IsTrue(info.skin.colorPaletteID > 0);
            // The client needs a default for every gender-locked appearance slot.
            CollectionAssert.AreEquivalent(new[] { "HR", "FA", "FD", "ER" },
                info.items.item.Select(item => item.slotHint).ToArray());
            Assert.IsTrue(info.items.item.All(item => item.id > 0));
        }

        // Slots with no palette override keep the sentinel rather than falling back to swatch 0.
        Assert.IsTrue(infos.Values
            .SelectMany(info => info.items.item)
            .Any(item => item.colorSN == -1));

        DefaultCharacterInfo.Item hair = infos[Gender.Female].items.item.First(item => item.slotHint == "HR");
        Assert.AreEqual(2, hair.controls.control.Count);
        Assert.AreNotEqual(0f, hair.controls.control[0].scale);
        Assert.AreNotEqual(Vector3.Zero, hair.controls.control[0].position);
        Assert.AreNotEqual(Vector3.Zero, hair.controls.control[0].rotation);
    }

    [TestMethod]
    public void TestDungeonScaleStat() {
        var parser = new ServerTableParser(TestUtils.ServerReader);

        Dictionary<int, DungeonScaleStat> scaleStats = parser.ParseDungeonScaleStat()
            .ToDictionary(entry => entry.Id, entry => entry.ScaleStat);

        // Retail row: <DungeonScaleStat dungeonScaleStatID="6001" scaleStatRate="8.0" scaleBaseTap="1911810"
        // scaleBaseDef="2552" scaleBaseSpaRate="0.35" />
        DungeonScaleStat row = scaleStats[6001];
        Assert.AreEqual(8.0f, row.scaleStatRate);
        Assert.AreEqual(1911810L, row.scaleBaseTap);
        Assert.AreEqual(2552L, row.scaleBaseDef);
        Assert.AreEqual(0.35f, row.scaleBaseSpaRate);
    }
}

