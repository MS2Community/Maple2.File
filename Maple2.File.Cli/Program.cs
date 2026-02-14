using Maple2.File.IO;
using Maple2.File.Parser.Flat;

// Load .env from solution root
string solutionDir = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../.."));
string dotenv = Path.Combine(solutionDir, ".env");
if (File.Exists(dotenv)) {
    foreach (string line in File.ReadAllLines(dotenv)) {
        if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#")) continue;
        string[] parts = line.Split('=', 2, StringSplitOptions.TrimEntries);
        if (parts.Length == 2) Environment.SetEnvironmentVariable(parts[0], parts[1]);
    }
}

string? m2dPath = Environment.GetEnvironmentVariable("MS2_DATA_FOLDER");
if (string.IsNullOrEmpty(m2dPath)) {
    Console.WriteLine("MS2_DATA_FOLDER is not set. Set it in .env or as an environment variable.");
    return;
}

string exportedPath = Path.Combine(m2dPath, "Resource", "Exported.m2d");
Console.WriteLine($"Loading: {exportedPath}");

using var reader = new M2dReader(exportedPath);
var index = new FlatTypeIndex(reader);

Console.WriteLine("Type 'type <name>' to inspect a type, 'quit' to exit.");
Console.WriteLine("Example: type ugc_fi_prop_forsalea");
Console.WriteLine();

index.CliExplorer();
