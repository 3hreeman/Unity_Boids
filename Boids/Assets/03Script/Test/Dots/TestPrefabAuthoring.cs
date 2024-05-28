using Unity.Entities;
using UnityEngine;

public class TestPrefabAuthoring : MonoBehaviour {
    public GameObject prefab;
}

public struct EntityPrefabComponent : IComponentData {
    public Entity Value;
}

public class TestPrefabBaker : Baker<TestPrefabAuthoring> {
    public override void Bake(TestPrefabAuthoring authoring) {
        Debug.LogWarning("GetPrefabBaker :: Bake!!");
        var entityPrefab = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic);
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        
        AddComponent(entity, new EntityPrefabComponent() {Value = entityPrefab});
    }
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