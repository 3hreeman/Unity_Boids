using System;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class TestPrefabAuthoring : MonoBehaviour {
    public float3 DirVector = new float3(0, 0, 1);
    public float Speed = 5f;
    
    class Baker : Baker<TestPrefabAuthoring> {
        public override void Bake(TestPrefabAuthoring authoring) {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            var data = new TestPrefab() {
                DirVector = authoring.DirVector,
                Speed = authoring.Speed
            };
            AddComponent(entity, data);
        }
    }
}

public struct TestPrefab : IComponentData {
    public float3 DirVector;
    public float Speed;
}

/*  //using EntityPrefabReference
public struct EntityPrefabReferenceComponent : IComponentData
{
    public EntityPrefabReference Value;
}

public class GetPrefabReferenceAuthoring : MonoBehaviour
{
    public GameObject Prefab;
}

public class GetPrefabReferenceBaker : Baker<GetPrefabReferenceAuthoring>
{
    public override void Bake(GetPrefabReferenceAuthoring authoring)
    {
        // Create an EntityPrefabReference from a GameObject. This will allow the
        // serialization process to serialize the prefab in its own entity scene
        // file instead of duplicating the prefab ECS content everywhere it is used
        var entityPrefab = new EntityPrefabReference(authoring.Prefab);
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new EntityPrefabReferenceComponent() {Value = entityPrefab});
    }
}
*/