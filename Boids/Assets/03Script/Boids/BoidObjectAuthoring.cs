using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class BoidObjectAuthoring : MonoBehaviour {
    
    class Baker : Baker<BoidObjectAuthoring> {
        public override void Bake(BoidObjectAuthoring authoring) {
            var entity = GetEntity(TransformUsageFlags.Renderable);
            var data = new BoidObject() {
                TargetVector = new float3(0, 0, 0),
            };
            
            var egoData = new BoidEgo() {
                EgoVector = new float3(0, 0, 0),
                NextUpdateTime = 0
            };
            
            var neighborData = new BoidNeighbor() {
                NeighborRange = 10,
                NeighborPositions = new NativeList<float3>()
            };
            
            var cohesionData = new BoidCohesion() {
                CohesionVector = new float3(0, 0, 0),
                CohesionWeight = 1f
            };
            
            AddComponent(entity, data);
            AddComponent(entity, egoData);
            AddComponent(entity, neighborData);
            AddComponent(entity, cohesionData);
        }
    }
}

public struct BoidObject : IComponentData {
    public float Speed;
    public float3 TargetVector;
}

public struct BoidEgo : IComponentData {
    public float3 EgoVector;
    public double NextUpdateTime;
}

public struct BoidNeighbor : IComponentData {
    public float NeighborRange;
    public NativeList<float3> NeighborPositions;
}

public struct BoidCohesion : IComponentData {
    public float3 CohesionVector;
    public float CohesionWeight;
}