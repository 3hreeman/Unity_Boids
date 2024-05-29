using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class BoidObjectAuthoring : MonoBehaviour {
    
    class Baker : Baker<BoidObjectAuthoring> {
        public override void Bake(BoidObjectAuthoring authoring) {
            var entity = GetEntity(TransformUsageFlags.Renderable);
            var data = new BoidObject() {
                TargetVector = new float3(0, 0, 0),
                EgoVector = new float3(0, 0, 0)
            };
            
            AddComponent(entity, data);
        }
    }
}

public struct BoidObject : IComponentData {
    public float3 TargetVector;
    public float3 EgoVector;
}





