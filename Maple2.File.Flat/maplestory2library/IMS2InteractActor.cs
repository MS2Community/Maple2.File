using System.Numerics;
using Maple2.File.Flat.standardmodellibrary;

namespace Maple2.File.Flat.maplestory2library {
    public interface IMS2InteractActor : IActor, IMS2InteractObject, I3DProxy, IMS2MapProperties {
        string ModelName => "MS2InteractActor";
        string reactableSequenceName => "0";
        bool IsVisible => true;
        Vector3 Position => default;
        Vector3 Rotation => default;
        float Scale => 1;
        bool MinimapInVisible => true;
        bool Transparency => true;
        bool UseInstancing => false;
        // Manually added: 11000119_MS2InteractActor_Moonrabbit on 80000022_bonus doubles as an NPC
        // spawn point and carries the SpawnPointNPC property set. The flat definition and its
        // mixins do not declare these, so RuntimeClassLookup patches them into the type index.
        // They are real data and the server reads them, so they are not marked obsolete.
        IDictionary<string, string> NpcList => new Dictionary<string, string>();
        int SpawnPointID => 0;
        float SpawnRadius => 0;
        uint NpcCount => 0;
    }
}
