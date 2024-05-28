using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = UnityEngine.Random;

public class ConfigAuthoring : MonoBehaviour {
    public GameObject Prefab;
    public int TestCount;    
    class Baker : Baker<ConfigAuthoring> {
        public override void Bake(ConfigAuthoring authoring) {
            var entityPrefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic);
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new Config() {
                Prefab = entityPrefab,
                TestCount = authoring.TestCount
            });
        }
    }

    public struct Config : IComponentData {
        public Entity Prefab;
        public int TestCount;
    }
}