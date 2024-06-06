using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class BoidsConfigAuthoring : MonoBehaviour {
    public GameObject BoidPrefab;
    public int BoidCount;
    public float SpawnRange;
    public float2 SpeedRange;
    public float CenterOffset;
    
    class Baker : Baker<BoidsConfigAuthoring> {
        public override void Bake(BoidsConfigAuthoring authoring) {
            var entity = GetEntity(TransformUsageFlags.Renderable | TransformUsageFlags.WorldSpace);
            var prefabToEntity = GetEntity(authoring.BoidPrefab, TransformUsageFlags.Dynamic);
            var data = new BoidConfig() {
                Prefab = prefabToEntity,
                BasePosition = authoring.transform.position,
                Count = authoring.BoidCount,
                SpawnRange = authoring.SpawnRange,
                SpeedRange = authoring.SpeedRange,
                CenterOffset = authoring.CenterOffset
            };
            
            AddComponent(entity, data);
        }
    }
}


public partial struct BoidConfig : IComponentData {
    public Entity Prefab;
    public float3 BasePosition;
    public int Count;
    public float SpawnRange;
    public float CenterOffset;
    public float2 SpeedRange;
}