using Maple2.File.Flat;
using Maple2.File.Flat.maplestory2library;
using Maple2.File.Parser.Flat;
using Maple2.File.Parser.MapXBlock;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Maple2.File.Tests;

// A few MS2InteractActor entities double as NPC spawn points and carry the SpawnPointNPC property
// set (SpawnPointID / NpcList / NpcCount / SpawnRadius). Neither the flat definition nor any mixin
// declares those, so RuntimeClassLookup patches them into the type index. See MS2Community/
// PrivateMaple2#270.
[TestClass]
public class InteractActorSpawnerTest {
    [TestMethod]
    public void MoonrabbitInteractActorCarriesSpawnerProperties() {
        var index = new FlatTypeIndex(TestUtils.ExportedReader);
        var parser = new XBlockParser(TestUtils.ExportedReader, index);

        IMS2InteractActor? moonrabbit = null;
        parser.ParseMap("80000022_bonus", entities => {
            moonrabbit = entities.OfType<IMS2InteractActor>()
                .FirstOrDefault(actor => actor.interactID == 11000119);
        });

        Assert.IsNotNull(moonrabbit, "80000022_bonus should contain interact actor 11000119");
        Assert.AreEqual(199, moonrabbit.SpawnPointID);
        Assert.AreEqual(1u, moonrabbit.NpcCount);
        Assert.AreEqual(150f, moonrabbit.SpawnRadius);
        CollectionAssert.AreEquivalent(
            new[] { "27000036" },
            moonrabbit.NpcList.Keys.ToArray());
        Assert.AreEqual("1", moonrabbit.NpcList["27000036"]);
    }

    // Diagnostic, not a regression guard: prints every interact actor in the game that carries
    // spawner data so the scope of the oddity is known rather than assumed. As of writing there is
    // exactly one. Parses every xblock, so it is opt-in.
    [TestMethod]
    [Ignore("Diagnostic sweep over every xblock; run manually.")]
    public void ReportAllInteractActorsWithSpawnerData() {
        var index = new FlatTypeIndex(TestUtils.ExportedReader);
        var parser = new XBlockParser(TestUtils.ExportedReader, index);

        var found = new List<string>();
        parser.Parallel().ForAll(map => {
            foreach (IMapEntity entity in map.entities) {
                if (entity is not IMS2InteractActor actor) {
                    continue;
                }
                if (actor.SpawnPointID == 0 && actor.NpcList.Count == 0) {
                    continue;
                }

                string npcs = string.Join(",", actor.NpcList.Select(kv => $"{kv.Key}x{kv.Value}"));
                lock (found) {
                    found.Add($"{map.xblock}: {entity.EntityName} interactID={actor.interactID} " +
                              $"spawnPoint={actor.SpawnPointID} radius={actor.SpawnRadius} " +
                              $"count={actor.NpcCount} npcs=[{npcs}]");
                }
            }
        });

        Console.WriteLine($"Interact actors with spawner data: {found.Count}");
        foreach (string line in found.OrderBy(x => x)) {
            Console.WriteLine(line);
        }
    }
}
