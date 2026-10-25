using System.Collections;
using System.Drawing;
using System.Globalization;
using System.Numerics;
using System.Text.RegularExpressions;

namespace Maple2.File.Parser.Flat;

public class FlatProperty {
    public string Name { get; set; }
    public string Id { get; set; }
    public string Source { get; set; }

    public readonly List<string> Trait = new();
    public string Type { get; set; }
    public object Value { get; set; }

    public static object ParseType(string type, string value) {
        switch (type) {
            case "Boolean":
                return bool.Parse(value);
            case "UInt16":
                return ushort.Parse(value);
            case "UInt32":
                return uint.Parse(value);
            case "SInt32":
                return int.Parse(value);
            case "Float32":
                return float.Parse(value, CultureInfo.InvariantCulture);
            case "Float64":
                return double.Parse(value, CultureInfo.InvariantCulture);
            case "Point3":
                float[] point3 = FloatList(value);
                return new Vector3(point3[0], point3[1], point3[2]);
            case "Point2":
                float[] point2 = FloatList(value);
                return new Vector2(point2[0], point2[1]);
            case "Color":
                float[] color = FloatList(value);
                return Color.FromArgb((int) (255 * color[0]), (int) (255 * color[1]), (int) (255 * color[2]));
            case "ColorA":
                float[] colorA = FloatList(value);
                return Color.FromArgb((int) (255 * colorA[3]), (int) (255 * colorA[0]), (int) (255 * colorA[1]), (int) (255 * colorA[2]));
            case "String":
            case "EntityRef": // GUID
            case "AssetID": // urn:llid:GUID
                //case "AttachedNifAsset": // Used only for Assoc
                return value;
            default:
                throw new ArgumentException($"Invalid Type: {type}");
        }
    }

    public static object ParseAssocType(string type, IEnumerable<(string Index, string Value)> values) {
        switch (type) {
            case "AssocString":
            case "AssocEntityRef":
            case "AssocAttachedNifAsset":
                Dictionary<string, string> assocString = new Dictionary<string, string>();
                foreach ((string index, string value) in values) {
                    assocString[index] = (string) ParseType("String", value);
                }

                return assocString;
            case "AssocPoint3":
                Dictionary<string, Vector3> assocPoint3 = new Dictionary<string, Vector3>();
                foreach ((string index, string value) in values) {
                    assocPoint3[index] = (Vector3) ParseType("Point3", value);
                }

                return assocPoint3;
            case "AssocUInt32":
                Dictionary<string, uint> assocUInt32 = new Dictionary<string, uint>();
                foreach ((string index, string value) in values) {
                    assocUInt32[index] = (uint) ParseType("UInt32", value);
                }

                return assocUInt32;
            case "AssocSInt32":
                Dictionary<string, int> assocSInt32 = new Dictionary<string, int>();
                foreach ((string index, string value) in values) {
                    assocSInt32[index] = (int) ParseType("SInt32", value);
                }

                return assocSInt32;
            default:
                throw new ArgumentException($"Invalid AssocType: {type}");
        }
    }

    private static float[] FloatList(string value) {
        string[] split = value.Split(", ");
        float[] result = new float[split.Length];
        for (int i = 0; i < split.Length; i++) {
            result[i] = float.Parse(split[i], CultureInfo.InvariantCulture);
        }

        return result;
    }

    public string ValueString() {
        if (!(Value is IDictionary dict)) {
            return $"{Value.GetType().Name}{{{Value}}}";
        }

        IEnumerable<string> entries = CastDict(dict)
            .Select(entry => $"{entry.Key}: {entry.Value.GetType().Name}({entry.Value})");
        return $"Dictionary{{{string.Join(',', entries)}}}";
    }

    public string ValueCodeString() {
        string value = System.Convert.ToString(Value, CultureInfo.InvariantCulture);
        if (Value is float) {
            value = Regex.Replace(value, "(\\d+\\.\\d+)", "$1f");
        }

        if (Value is Vector3) {
            value = Regex.Replace(value, "<(-?\\d+\\.?\\d*), (-?\\d+\\.?\\d*), (-?\\d+\\.?\\d*)>",
                "new Vector3($1, $2, $3)");
            value = Regex.Replace(value, "(\\d+\\.\\d+)", "$1f");
            value = value.Replace("new Vector3(0, 0, 0)", "default");
        }

        if (Value is Vector2) {
            value = Regex.Replace(value, "<(-?\\d+\\.?\\d*), (-?\\d+\\.?\\d*)>", "new Vector2($1, $2)");
            value = Regex.Replace(value, "(\\d+\\.\\d+)", "$1f");
            value = value.Replace("new Vector2(0, 0)", "default");
        }

        if (Value is Color) {
            value = Regex.Replace(value, "Color \\[A=(\\d+), R=(\\d+), G=(\\d+), B=(\\d+)\\]",
                "Color.FromArgb($1, $2, $3, $4)");
            value = value.Replace("Color.FromArgb(0, 0, 0, 0)", "default");
        }

        if (Value is string) {
            value = $"\"{value}\"";
        }

        value = Regex.Replace(value, "System\\.Collections\\.Generic\\.Dictionary`2\\[(.+)\\]",
            "new Dictionary<$1>()");
        if (Value is bool) {
            value = value.ToLower();
        }

        return value;
    }

    public bool ValueEquals(object other) {
        if (Equals(Value, other)) {
            return true;
        }

        if (Type.StartsWith("Assoc")) {
            if (Value is IDictionary dict1 && other is IDictionary dict2 && dict1.Count == dict2.Count) {
                return true;
            }
        }
        return false;
    }

    private IEnumerable<DictionaryEntry> CastDict(IDictionary dictionary) {
        foreach (DictionaryEntry entry in dictionary) {
            yield return entry;
        }
    }

    protected bool Equals(FlatProperty other) {
        return Name == other.Name && Id == other.Id && Type == other.Type && ValueEquals(other.Value);
    }

    public override bool Equals(object obj) {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((FlatProperty) obj);
    }

    public override int GetHashCode() {
        return HashCode.Combine(Name, Id, Type, Value);
    }

    public override string ToString() {
        return $"{Type}\t{Name}: {ValueString()})";
    }
}
