using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;
using Maple2.File.IO;
using Maple2.File.IO.Crypto.Common;
using Maple2.File.Parser.Xml.String;

namespace Maple2.File.Parser;

public class BanWordParser {
    private readonly M2dReader xmlReader;
    private readonly XmlSerializer nameSerializer;

    public BanWordParser(M2dReader xmlReader) {
        this.xmlReader = xmlReader;
        nameSerializer = new XmlSerializer(typeof(StringMapping));
    }

    // banword.xml: the client refuses text that contains any of these.
    public IEnumerable<(int Id, string Name)> ParseBanWords() {
        return Parse(name => name.Contains("banword") && !name.Contains("banwordall") && !name.Contains("ugc"));
    }

    // banwordall.xml: the client refuses these only as a whole space-delimited word,
    // CBanTextChecker::FindBanWordAll in the KMS2 client, so they need their own list.
    public IEnumerable<(int Id, string Name)> ParseBanWordAll() {
        return Parse(name => name.Contains("banwordall"));
    }

    public IEnumerable<(int Id, string Name)> ParseUgcBanWords() {
        return Parse(name => name.Contains("ugcbanword"));
    }

    private IEnumerable<(int Id, string Name)> Parse(Func<string, bool> matchesFile) {
        int i = 0;
        foreach (PackFileEntry entry in xmlReader.Files.Where(entry => matchesFile(entry.Name))) {
            XmlReader reader = xmlReader.GetXmlReader(entry);
            var mapping = nameSerializer.Deserialize(reader) as StringMapping;

            Debug.Assert(mapping != null);

            Dictionary<int, string> banWords = mapping.key.ToDictionary(_ => i++, key => key.name);
            foreach (var banWord in banWords) {
                if (string.IsNullOrEmpty(banWord.Value)) {
                    continue;
                }
                yield return (banWord.Key, banWord.Value);
            }
        }
    }
}
