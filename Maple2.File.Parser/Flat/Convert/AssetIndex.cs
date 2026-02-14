using System.Diagnostics;
using System.Text.RegularExpressions;
using Maple2.File.IO;
using Maple2.File.IO.Crypto.Common;

namespace Maple2.File.Parser.Flat.Convert;

public class AssetIndex {
    private const uint MAGIC = 0x00495341;
    private const int VERSION = 1;

    private static Regex extractRegex = new("^<(urn:uuid:[0-9a-f-]+)> <.+> \"(.+)\".$");

    private readonly Dictionary<string, List<string>> llidLookup;
    private readonly Dictionary<string, Dictionary<string, string>> ntLookup;
    private static readonly string[] NtTagFiles = new string[] {
        "application",
        "cn",
        "dds",
        "emergent-flat-model",
        "emergent-world",
        "fx-shader-compiled",
        "gamebryo-animation",
        "gamebryo-scenegraph",
        "gamebryo-sequence-file",
        "image",
        "jp",
        "kr",
        "lua-behavior",
        "model",
        "png",
        "precache",
        "script",
        "shader",
        "x-shockwave-flash",
        "x-world",
    };
    private static readonly string[] NtFiles = new string[] {
        "llid",
        "name",
        "relpath",
    };

    public AssetIndex(M2dReader reader) {
        ntLookup = new Dictionary<string, Dictionary<string, string>>();
        llidLookup = new Dictionary<string, List<string>>();

        foreach (PackFileEntry entry in reader.Files.Where(entry => entry.Name.EndsWith(".nt"))) {
            string key = Path.GetFileNameWithoutExtension(entry.Name);
            if (!NtFiles.Contains(key) && !NtTagFiles.Contains(key)) {
                continue;
            }

            Dictionary<string, string> value = ParseNtFile(reader.GetString(entry));
            if (key == "llid") {
                foreach ((string k, string v) in value) {
                    if (!llidLookup.ContainsKey(v)) {
                        llidLookup.Add(v, new List<string>());
                    }

                    llidLookup[v].Add(k);
                }
            }

            ntLookup.Add(key!, value);
        }
    }

    private AssetIndex(Dictionary<string, List<string>> llidLookup, Dictionary<string, Dictionary<string, string>> ntLookup) {
        this.llidLookup = llidLookup;
        this.ntLookup = ntLookup;
    }

    public void Serialize(BinaryWriter writer) {
        writer.Write(MAGIC);
        writer.Write(VERSION);

        writer.Write(llidLookup.Count);
        foreach (var kvp in llidLookup) {
            writer.Write(kvp.Key ?? string.Empty);
            writer.Write(kvp.Value.Count);
            foreach (string llid in kvp.Value) {
                writer.Write(llid ?? string.Empty);
            }
        }

        writer.Write(ntLookup.Count);
        foreach (var kvp in ntLookup) {
            writer.Write(kvp.Key ?? string.Empty);
            writer.Write(kvp.Value.Count);
            foreach (var innerKvp in kvp.Value) {
                writer.Write(innerKvp.Key ?? string.Empty);
                writer.Write(innerKvp.Value ?? string.Empty);
            }
        }
    }

    public static AssetIndex Deserialize(BinaryReader reader) {
        uint magic = reader.ReadUInt32();
        if (magic != MAGIC) {
            throw new InvalidDataException($"Invalid AssetIndex magic: expected {MAGIC:X}, got {magic:X}");
        }

        int version = reader.ReadInt32();
        if (version != VERSION) {
            throw new InvalidDataException($"Unsupported AssetIndex version: {version}");
        }

        int llidCount = reader.ReadInt32();
        var llidLookup = new Dictionary<string, List<string>>(llidCount);
        for (int i = 0; i < llidCount; i++) {
            string key = reader.ReadString();
            int valueCount = reader.ReadInt32();
            var values = new List<string>(valueCount);
            for (int j = 0; j < valueCount; j++) {
                values.Add(reader.ReadString());
            }
            llidLookup[key] = values;
        }

        int ntCount = reader.ReadInt32();
        var ntLookup = new Dictionary<string, Dictionary<string, string>>(ntCount);
        for (int i = 0; i < ntCount; i++) {
            string key = reader.ReadString();
            int innerCount = reader.ReadInt32();
            var innerDict = new Dictionary<string, string>(innerCount);
            for (int j = 0; j < innerCount; j++) {
                innerDict[reader.ReadString()] = reader.ReadString();
            }
            ntLookup[key] = innerDict;
        }

        return new AssetIndex(llidLookup, ntLookup);
    }

    public (string Name, string Path, string Tags) GetFields(string llid) {
        llid = llid.Replace("urn:llid:", "");
        if (!llidLookup.TryGetValue(llid, out List<string> uuids)) {
            Console.WriteLine($"Failed to lookup metadata for: {llid}");
            return ("", "", "");
        }

        Debug.Assert(uuids.Count == 1, $"Failed to resolve llid:{llid} to uuid");
        string uuid = uuids.SingleOrDefault();

        var tags = new List<string>();
        foreach (string tagName in NtTagFiles) {
            if (ntLookup[tagName].ContainsKey(uuid)) {
                tags.Add(tagName);
            }
        }

        string name = ntLookup["name"][uuid];
        tags.Add(name);
        string path = ntLookup["relpath"][uuid];

        return (name, path, string.Join(':', tags));
    }

    private static Dictionary<string, string> ParseNtFile(string data) {
        var result = new Dictionary<string, string>();
        foreach (string line in data.Split("\n")) {
            if (string.IsNullOrWhiteSpace(line)) {
                continue;
            }

            Match match = extractRegex.Match(line);
            Debug.Assert(match.Success, $"failed to match: {line}");

            result.Add(match.Groups[1].Value, match.Groups[2].Value);
        }

        return result;
    }
}
