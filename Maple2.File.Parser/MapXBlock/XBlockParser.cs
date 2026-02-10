using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using Maple2.File.Flat;
using Maple2.File.IO;
using Maple2.File.IO.Crypto.Common;
using Maple2.File.Parser.Flat;

namespace Maple2.File.Parser.MapXBlock;

public class XBlockParser {
    private readonly M2dReader reader;
    private readonly XmlSerializer serializer;
    private readonly ClassLookup lookup;
    private readonly FlatTypeIndex index;
    private readonly HashSet<Type> includeEntities;

    // Stream of error logs
    public Action<string> OnError;

    public XBlockParser(M2dReader reader, FlatTypeIndex index) {
        this.reader = reader;
        this.index = index ?? new FlatTypeIndex(reader);

        serializer = new XmlSerializer(typeof(GameXBlock));
        lookup = new RuntimeClassLookup(this.index);
        includeEntities = new HashSet<Type>();
    }

    public bool Include(Type type) {
        if (!typeof(IMapEntity).IsAssignableFrom(type)) {
            return false;
        }

        includeEntities.Add(type);
        return true;
    }

    public ParallelQuery<(string xblock, IEnumerable<IMapEntity> entities)> Parallel() {
        return reader.Files
            .Where(file => file.Name.StartsWith("xblock/"))
            .AsParallel()
            .Select(file => {
                string xblock = Path.GetFileNameWithoutExtension(file.Name);
                return (xblock, ParseEntities(file));
            });
    }

    public void Parse(Action<string, IEnumerable<IMapEntity>> callback) {
        foreach (PackFileEntry file in reader.Files) {
            if (!file.Name.StartsWith("xblock/")) {
                continue;
            }

            string xblock = Path.GetFileNameWithoutExtension(file.Name);
            callback(xblock, ParseEntities(file));
        }
    }

    public void ParseMap(string xblock, Action<IEnumerable<IMapEntity>> callback) {
        PackFileEntry file = reader.Files
            .FirstOrDefault(file => file.Name.Equals($"xblock/{xblock}.xblock"));
        if (file == default) {
            return;
        }

        callback(ParseEntities(file));
    }

    public static IEnumerable<string> CheckParseOrder(ICollection<Type> types) {
        // Validate input types are all IMapEntity.
        foreach (Type parseType in types) {
            if (!typeof(IMapEntity).IsAssignableFrom(parseType)) {
                throw new ArgumentException($"Type {parseType.Name} is not IMapEntity");
            }
        }

        foreach (Type entityType in Assembly.GetAssembly(typeof(IMapEntity)).GetTypes()) {
            var assignable = new List<string>();
            foreach (Type parseType in types) {
                if (parseType.IsAssignableFrom(entityType)) {
                    assignable.Add(parseType.Name);
                }
            }

            if (assignable.Count > 1) {
                yield return $"Warning: {entityType.Name} is captured by {string.Join(", ", assignable)}";
            }
        }
    }

    private IEnumerable<IMapEntity> ParseEntities(PackFileEntry file) {
        if (serializer.Deserialize(reader.GetXmlReader(file)) is GameXBlock block) {
            var unknownModels = new HashSet<string>();
            return block.entitySet.entity
                .Where(entity => {
                    try {
                        Type mixinType = lookup.GetMixinType(entity.modelName);
                        return includeEntities.Count == 0 || includeEntities.Any(keep => keep.IsAssignableFrom(mixinType));
                    } catch {
                        return false;
                    }
                })
                .Select(entity => {
                    try {
                        return GetInstance(entity);
                    } catch (UnknownModelTypeException ex) {
                        // Reduce noise from this exception to once per file
                        if (unknownModels.Add(entity.modelName)) {
                            OnError?.Invoke(ex.Message);
                        }
                        return null;
                    }
                })
                .Where(entity => entity != null);
        }

        return Array.Empty<IMapEntity>();
    }

    private IMapEntity GetInstance(Entity entity) {
        Type entityType = lookup.GetClass(entity.modelName);

        var mapEntity = (IMapEntity) Activator.CreateInstance(entityType);
        if (mapEntity == null) {
            throw new InvalidOperationException($"Unable to create instance of: {entity.modelName}");
        }

        // Assigned inherited values
        FlatType baseType = index.GetType(entity.modelName);
        foreach (FlatProperty property in baseType.GetProperties()) {
            SetValue(entityType, mapEntity, property.Name, property.Value);
        }

        // Assign entity specific values
        foreach (Entity.Property property in entity.property) {
            // Not setting any values
            if (property.set.Count == 0) {
                continue;
            }

            FlatProperty modelProperty = baseType.GetProperty(property.name);
            if (modelProperty == null) {
                OnError?.Invoke($"Ignoring unknown property {property.name}");
                continue;
            }

            object value;
            if (modelProperty.Type.StartsWith("Assoc")) {
                value = FlatProperty.ParseAssocType(modelProperty.Type,
                    property.set.Select(set => (set.index, set.value)));
            } else {
                value = FlatProperty.ParseType(modelProperty.Type, property.set[0].value);
            }

            SetValue(entityType, mapEntity, property.name, value);
        }

        SetValue(entityType, mapEntity, "ModelName", entity.modelName);
        SetValue(entityType, mapEntity, "EntityId", entity.id);
        SetValue(entityType, mapEntity, "EntityName", entity.name);

        return mapEntity;
    }

    private void SetValue(Type type, IMapEntity entity, string name, object value) {
        FieldInfo field = lookup.GetField(type, name);
        if (field == null) {
            OnError?.Invoke($"Ignoring unknown field {name} on {type.Name}");
            return;
        }

        field.SetValue(entity, value);
    }

    public void ParseXml(XmlReader xmlReader, Action<IEnumerable<IMapEntity>> callback) {
        if (serializer.Deserialize(xmlReader) is GameXBlock block) {
            var unknownModels = new HashSet<string>();
            var entities = block.entitySet.entity
                .Where(entity => {
                    try {
                        Type mixinType = lookup.GetMixinType(entity.modelName);
                        return includeEntities.Count == 0 || includeEntities.Any(keep => keep.IsAssignableFrom(mixinType));
                    } catch {
                        return false;
                    }
                })
                .Select(entity => {
                    try {
                        return GetInstance(entity);
                    } catch (UnknownModelTypeException ex) {
                        if (unknownModels.Add(entity.modelName)) {
                            OnError?.Invoke(ex.Message);
                        }
                        return null;
                    }
                })
                .Where(entity => entity != null);

            callback(entities);
        } else {
            callback(Array.Empty<IMapEntity>());
        }
    }
}
