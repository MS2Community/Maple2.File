using Maple2.File.Parser.Xml.AI;
using System.Diagnostics;
using System.Globalization;
using System.Numerics;
using System.Reflection;
using Maple2.File.IO;
using Maple2.File.IO.Crypto.Common;
using Maple2.File.Parser.Tools;
using System.Xml;

namespace Maple2.File.Parser;

public class AiParser {
    private readonly M2dReader xmlReader;
    private readonly bool includeComments;

    public AiParser(M2dReader xmlReader, bool includeComments = false) {
        this.xmlReader = xmlReader;
        this.includeComments = includeComments;
    }

    public IEnumerable<(string AiName, NpcAi Data)> Parse() {
        foreach (PackFileEntry entry in xmlReader.Files.Where(entry => entry.Name.StartsWith("AI/"))) {
            string sanitized = Sanitizer.SanitizeAi(xmlReader.GetString(entry));
            var document = new XmlDocument();
            document.LoadXml(sanitized);

            var ai = new NpcAi();
            XmlNode? reservedNode = document.SelectSingleNode("npcAi/reserved");
            if (reservedNode != null) {
                XmlNode? commentNode = reservedNode.PreviousSibling;
                while (includeComments && commentNode is XmlComment { Value: not null } comment) {
                    commentNode = commentNode.PreviousSibling;

                    if (comment.Value.Trim() == "예약") continue; // Redundant comment
                    ai.Reserved.Insert(0, new Comment {
                        Value = comment.Value,
                    });
                }
                ai.Reserved.AddRange(ParseChildren(reservedNode));
            }
            XmlNode? battleNode = document.SelectSingleNode("npcAi/battle");
            if (battleNode != null) {
                ai.Battle.startAni = battleNode.Attributes?["startAni"]?.Value ?? string.Empty;
                ai.Battle.endAni = battleNode.Attributes?["endAni"]?.Value ?? string.Empty;
                ai.Battle.isBattle = battleNode.Attributes?["isBattle"]?.Value == "true";
                XmlNode? commentNode = battleNode.PreviousSibling;
                while (includeComments && commentNode is XmlComment { Value: not null } comment) {
                    commentNode = commentNode.PreviousSibling;

                    if (comment.Value.Trim() == "전투") continue; // Redundant comment
                    ai.Battle.Entries.Insert(0, new Comment {
                        Value = comment.Value,
                    });
                }
                ai.Battle.Entries.AddRange(ParseChildren(battleNode));
            }
            XmlNode? battleEndNode = document.SelectSingleNode("npcAi/battleEnd");
            if (battleEndNode != null) {
                ai.BattleEnd.onlyDead = battleEndNode.Attributes?["onlyDead"]?.Value == "true";
                XmlNode? commentNode = battleEndNode.PreviousSibling;
                while (includeComments && commentNode is XmlComment { Value: not null } comment) {
                    commentNode = commentNode.PreviousSibling;
                    ai.BattleEnd.Entries.Insert(0, new Comment {
                        Value = comment.Value,
                    });
                }
                ai.BattleEnd.Entries.AddRange(ParseChildren(battleEndNode));
            }
            XmlNode? aiPresetsNode = document.SelectSingleNode("npcAi/aiPresets");
            if (aiPresetsNode != null) {
                XmlNode? commentNode = aiPresetsNode.PreviousSibling;
                while (includeComments && commentNode is XmlComment { Value: not null } comment) {
                    commentNode = commentNode.PreviousSibling;

                    if (comment.Value.StartsWith("ai프리셋 모음")) continue; // Redundant comment
                    ai.AiPresets.Insert(0, new Comment {
                        Value = comment.Value,
                    });
                }
                ai.AiPresets.AddRange(ParseChildren(aiPresetsNode));
            }

            // removing the AI/ prefix because the <aiInfo path> attribute is relative to AI
            string aiName = entry.Name.Substring(3);

            yield return (aiName, ai);
        }
    }

    private IEnumerable<Entry> ParseChildren(XmlNode root) {
        foreach (XmlNode child in root.ChildNodes) {
            Entry? result = ParseNode(child);
            if (result == null) {
                continue;
            }

            result.Entries = ParseChildren(child).ToList();
            yield return result;
        }
    }

    private Entry? ParseNode(XmlNode xml) {
        if (xml is XmlComment comment) {
            return includeComments ? new Comment {
                Value = comment.Value,
            } : null;
        }

        Debug.Assert(xml.Attributes != null, $"Node has no attributes: {xml}");
        Debug.Assert(xml.Attributes["name"] != null, $"Node has no name: {xml}");

        string name = xml.Attributes["name"].Value.ToLower();
        if (xml.Name == "aiPreset") {
            return new AiPresetEntry {
                name = name,
            };
        }

        return name switch {
            // Nodes
            "trace" => ParseAttributes<TraceNode>(xml),
            "skill" => ParseAttributes<SkillNode>(xml),
            "teleport" => ParseAttributes<TeleportNode>(xml),
            "standby" => ParseAttributes<StandbyNode>(xml),
            "setdata" => ParseAttributes<SetDataNode>(xml),
            "target" => ParseAttributes<TargetNode>(xml),
            "say" => ParseAttributes<SayNode>(xml),
            "setvalue" => ParseAttributes<SetValueNode>(xml),
            "conditions" => ParseAttributes<ConditionsNode>(xml),
            "jump" => ParseAttributes<JumpNode>(xml),
            "select" => ParseAttributes<SelectNode>(xml),
            "move" => ParseAttributes<MoveNode>(xml),
            "summon" => ParseAttributes<SummonNode>(xml),
            "triggersetuservalue" => ParseAttributes<TriggerSetUserValueNode>(xml),
            "ride" => ParseAttributes<RideNode>(xml),
            "setslavevalue" => ParseAttributes<SetSlaveValueNode>(xml),
            "setmastervalue" => ParseAttributes<SetMasterValueNode>(xml),
            "runaway" => ParseAttributes<RunawayNode>(xml),
            "minimumhp" => ParseAttributes<MinimumHpNode>(xml),
            "buff" => ParseAttributes<BuffNode>(xml),
            "targeteffect" => ParseAttributes<TargetEffectNode>(xml),
            "showvibrate" => ParseAttributes<ShowVibrateNode>(xml),
            "sidepopup" => ParseAttributes<SidePopupNode>(xml),
            "setvaluerangetarget" => ParseAttributes<SetValueRangeTargetNode>(xml),
            "announce" => ParseAttributes<AnnounceNode>(xml),
            "modifyroomtime" => ParseAttributes<ModifyRoomTimeNode>(xml),
            "hidevibrateall" => ParseAttributes<HideVibrateAllNode>(xml),
            "triggermodifyuservalue" => ParseAttributes<TriggerModifyUserValueNode>(xml),
            "removeslaves" => ParseAttributes<RemoveSlavesNode>(xml),
            "createrandomroom" => ParseAttributes<CreateRandomRoomNode>(xml),
            "createinteractobject" => ParseAttributes<CreateInteractObjectNode>(xml),
            "removeme" => ParseAttributes<RemoveMeNode>(xml),
            "suicide" => ParseAttributes<SuicideNode>(xml),
            // Conditions
            "hpover" => ParseAttributes<HpOverCondition>(xml),
            "hpless" => ParseAttributes<HpLessCondition>(xml),
            "distanceover" => ParseAttributes<DistanceOverCondition>(xml),
            "distanceless" => ParseAttributes<DistanceLessCondition>(xml),
            "combattime" => ParseAttributes<CombatTimeCondition>(xml),
            "skillrange" => ParseAttributes<SkillRangeCondition>(xml),
            "extradata" => ParseAttributes<ExtraDataCondition>(xml),
            "slavecount" => ParseAttributes<SlaveCountCondition>(xml),
            "state" => ParseAttributes<StateCondition>(xml),
            "additional" => ParseAttributes<AdditionalCondition>(xml),
            "feature" => ParseAttributes<FeatureCondition>(xml),
            "true" => ParseAttributes<TrueCondition>(xml),
            _ => throw new InvalidOperationException($"Unsupported XmlNode with name: {name}")
        };
    }

    private static T ParseAttributes<T>(XmlNode xmlNode) where T : Entry {
        var result = (T) Activator.CreateInstance(typeof(T));
        foreach (XmlAttribute attrib in xmlNode.Attributes) {
            FieldInfo field = typeof(T).GetField(attrib.Name);

            if (field is null) {
                throw new MemberAccessException($"object of type '{typeof(T).Name}' doesn't have member '{attrib.Name}' from node '{xmlNode.LocalName}'");
            }

            switch (field.FieldType.Name) {
                case "Boolean":
                    if (!bool.TryParse(attrib.Value, out bool outBool)) {
                        if (!int.TryParse(attrib.Value, out int outBoolInt)) {
                            throw new InvalidDataException($"invalid bool '{attrib.Value}' for {typeof(T).Name} member {field.Name}");
                        }

                        outBool = outBoolInt != 0;
                    }

                    field.SetValue(result, outBool);
                    break;
                case "Int16":
                    if (!short.TryParse(attrib.Value, out short outShort)) {
                        throw new InvalidDataException($"invalid short '{attrib.Value}' for {typeof(T).Name} member {field.Name}");
                    }

                    field.SetValue(result, outShort);
                    break;
                case "Int32":
                    if (!int.TryParse(attrib.Value, out int outInt)) {
                        throw new InvalidDataException($"invalid int '{attrib.Value}' for {typeof(T).Name} member {field.Name}");
                    }

                    field.SetValue(result, outInt);
                    break;
                case "Int64":
                    if (!long.TryParse(attrib.Value, out long outLong)) {
                        throw new InvalidDataException($"invalid long '{attrib.Value}' for {typeof(T).Name} member {field.Name}");
                    }

                    field.SetValue(result, outLong);
                    break;
                case "Single":
                    if (!float.TryParse(attrib.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out float outFloat)) {
                        throw new InvalidDataException($"invalid float '{attrib.Value}' for {typeof(T).Name} member {field.Name}");
                    }

                    field.SetValue(result, outFloat);
                    break;
                case "Vector3":
                    float[] floatValues = Array.ConvertAll(attrib.Value.Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries), value => float.Parse(value, CultureInfo.InvariantCulture));

                    if (floatValues.Length != 3) {
                        throw new InvalidDataException($"invalid Vector3 '{attrib.Value}' for {typeof(T).Name} member {field.Name}");
                    }

                    field.SetValue(result, new Vector3(floatValues[0], floatValues[1], floatValues[2]));
                    break;
                case "String":
                    field.SetValue(result, attrib.Value);
                    break;
                case "Int32[]":
                    int[] intValues = Array.ConvertAll(attrib.Value.Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries), int.Parse);

                    field.SetValue(result, intValues);
                    break;
                default:
                    if (field.FieldType.IsArray && field.FieldType.GetElementType().IsEnum) {
                        Type? nestedType = field.FieldType.GetElementType();
                        if (nestedType is null) {
                            throw new InvalidDataException($"unhandled type of {field.FieldType.Name} on value '{attrib.Value}' for {typeof(T).Name} member {field.Name}");
                        }
                        // generates an object[] array of the enums. i couldn't find a way to generate the actual underlying enum type array directly because it kept resolving to object[]
                        object[] enumObjectValues = Array.ConvertAll(attrib.Value.Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries), value => System.Enum.Parse(nestedType, value));
                        // forcibly create an enum array and copy values over so that field.SetValue is happy & copies the array over to the member
                        dynamic enumValues = Array.CreateInstance(field.FieldType.GetElementType(), enumObjectValues.GetLength(0));
                        for (int i = 0; i < enumObjectValues.Length; ++i) {
                            enumValues[i] = (dynamic) enumObjectValues[i];
                        }
                        field.SetValue(result, enumValues);
                    } else if (field.FieldType.IsEnum) {
                        field.SetValue(result, System.Enum.Parse(field.FieldType, attrib.Value));
                    } else {
                        throw new InvalidDataException($"unhandled type of {field.FieldType.Name} on value '{attrib.Value}' for {typeof(T).Name} member {field.Name}");
                    }
                    break;
            }
        }

        return result;
    }
}
