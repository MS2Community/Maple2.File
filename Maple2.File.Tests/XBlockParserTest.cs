using System.Diagnostics;
using Maple2.File.Flat;
using Maple2.File.Flat.maplestory2library;
using Maple2.File.Parser.Flat;
using Maple2.File.Parser.MapXBlock;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Maple2.File.Tests;

[TestClass]
public class XBlockParserTest {
    // [TestMethod]
    // public void TestConversion() {
    // var flatConverter = new FlatToModel(TestUtils.ExportedReader, TestUtils.AssetMetadataReader);
    // flatConverter.Convert();

    // var xblockConverter = new XBlockToBlock(TestUtils.ExportedReader);
    // xblockConverter.Convert();
    // }

    [TestMethod]
    public void TestXBlockParser() {
        var index = new FlatTypeIndex(TestUtils.ExportedReader);
        var parser = new XBlockParser(TestUtils.ExportedReader, index);
        // Console.WriteLine(index.GetType("Portal_entrance").GetProperty("frontOffset"));

        parser.ParseMap("02000070_in", entities => {
            foreach (IMapEntity? entity in entities) {
                if (entity is IPortal portal) {
                    Console.WriteLine(entity.EntityName);
                    Console.WriteLine(portal.ModelName);
                    Console.WriteLine(portal.frontOffset);
                }
            }
        });
    }

    [TestMethod]
    public void TestXBlockParsersShareIndex() {
        var index = new FlatTypeIndex(TestUtils.ExportedReader);
        _ = new XBlockParser(TestUtils.ExportedReader, index);
        var second = new XBlockParser(TestUtils.ExportedReader, index);

        int count = 0;
        second.ParseMap("02000070_in", entities => count = entities.Count());
        Assert.IsTrue(count > 0);
    }

    [Ignore]
    public void TestXBlockParserAll() {
        var sw = new Stopwatch();
        sw.Start();
        var index = new FlatTypeIndex(TestUtils.ExportedReader);
        Console.WriteLine($"Index built in {sw.ElapsedMilliseconds}ms");

        sw.Restart();
        int xblocks = 0;
        var parser = new XBlockParser(TestUtils.ExportedReader, index);
        ParallelQuery<int> results = parser.Parallel().SelectMany(map => {
            xblocks++;
            return Process(map.entities);
        });
        Console.WriteLine($"Total results: {results.Count()}");
        Console.WriteLine($"Parser completed {xblocks} in {sw.ElapsedMilliseconds}ms");

        IEnumerable<int> Process(IEnumerable<IMapEntity> entities) {
            foreach (IMapEntity? _ in entities) {
                yield return 0;
            }
        }
    }
}

