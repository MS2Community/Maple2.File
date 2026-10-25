using System.Reflection;
using System.Text;
using Maple2.File.Generator.Utils;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Maple2.File.Generator;

[Generator]
public class XmlVector3Generator : XmlGenerator {
    private static readonly SourceText attributeSource =
        Assembly.GetExecutingAssembly().LoadSource("M2dVector3Attribute.cs");
    private static readonly DiagnosticDescriptor typeError = new DiagnosticDescriptor(
        "FG00030",
        "M2dVector3Attribute can only be applied to System.Numerics.Vector3",
        "Invalid type {0} for M2dVector3Attribute in {1}",
        "Maple2.File.Generator",
        DiagnosticSeverity.Error,
        true
    );

    public XmlVector3Generator() : base(attributeSource, "M2dXmlGenerator", "M2dVector3") { }

    protected override string ProcessClass(GeneratorExecutionContext context, ISymbol @class,
        IEnumerable<IFieldSymbol> fields, INamedTypeSymbol attribute) {
        var builder = new SourceBuilder(@class.ContainingNamespace);
        builder.Imports.AddRange([
            "System",
            "System.Xml.Serialization",
        ]);
        builder.Classes.AddRange(@class.ContainingTypes().Select(symbol => symbol.Name));
        builder.Classes.Add(@class.Name);

        // create properties for each field
        builder.Code.AddRange(fields.Select(field => ProcessField(context, field, attribute)));

        return builder.Build();
    }

    private string ProcessField(GeneratorExecutionContext context, IFieldSymbol field, ISymbol attribute) {
        if (field.Type.ToDisplayString() != "System.Numerics.Vector3") {
            context.ReportDiagnostic(Diagnostic.Create(typeError, Location.None, field.Type, field.ToDisplayString()));
        }

        AttributeData attributeData = field.GetAttribute(attribute);
        string xmlAttributeName = attributeData.GetValueOrDefault("Name", field.Name);
        string fieldName = $"this.{field.Name}";

        var source = new StringBuilder();
        source.Append($@"
[XmlAttribute(""{xmlAttributeName}"")]
public string _{xmlAttributeName} {{
    get => global::System.FormattableString.Invariant($""{{{fieldName}.X}},{{{fieldName}.Y}},{{{fieldName}.Z}}"");
    set {{
        string[] split = value.Split(new[] {{',', ' '}}, StringSplitOptions.RemoveEmptyEntries);
        float x = split.Length > 0 ? float.Parse(split[0], global::System.Globalization.CultureInfo.InvariantCulture) : 0;
        float y = split.Length > 1 ? float.Parse(split[1], global::System.Globalization.CultureInfo.InvariantCulture) : 0;
        float z = split.Length > 2 ? float.Parse(split[2], global::System.Globalization.CultureInfo.InvariantCulture) : 0;
        {fieldName} =  new System.Numerics.Vector3(x, y, z);
    }}
}}");
        return source.ToString();
    }
}
