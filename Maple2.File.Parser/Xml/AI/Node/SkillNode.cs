using System.Numerics;

namespace Maple2.File.Parser.Xml.AI;

public class SkillNode : NodeEntry {
    public int idx;
    public int id;
    public short level;
    public int prob = 100;
    public bool sequence;
    public Vector3 facePos;
    // Absent means aim, not 0: 61% of skill nodes carry no faceTarget attribute, and retail aims
    // them at the target at cast start. Horus (AI_GriffonPharaoh01Boss) casts its attribute-less
    // idx 10 twice and the second turns ~124 degrees onto the player (GMS2 sniff "14-Horus's Nest").
    public int faceTarget = 1;
    public int faceTargetTick;
    public long initialCooltime;
    public long cooltime;
    public int limit;
    public bool isKeepBattle;
}
