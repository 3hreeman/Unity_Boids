using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class ConfigAuthoring : MonoBehaviour {
    public GameObject Prefab;
    public int TestCount;
    public Vector2 SpeedRange;
    public Vector2 DirRange;
    
    class Baker : Baker<ConfigAuthoring> {
        public override void Bake(ConfigAuthoring authoring) {
            var entityPrefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic);
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new Config() {
                Prefab = entityPrefab,
                TestCount = authoring.TestCount,
                SpeedRange = authoring.SpeedRange,
                DirRange = authoring.DirRange
            });
        }
    }

    public struct Config : IComponentData {
        public Entity Prefab;
        public int TestCount;
        public float2 SpeedRange;
        public float2 DirRange;
    }
}