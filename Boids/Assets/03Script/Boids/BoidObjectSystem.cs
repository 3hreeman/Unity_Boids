using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public partial struct BoidObjectSystem : ISystem {
    
    
    public void OnUpdate(ref SystemState state) {
        var tick = (float)SystemAPI.Time.DeltaTime;
        foreach (var (boid, xform) in SystemAPI.Query<RefRO<BoidObject>, RefRW<LocalTransform>>()) {
            xform.ValueRW.Position += boid.ValueRO.TargetVector * boid.ValueRO.Speed * tick;
            xform.ValueRW.Rotation = Quaternion.LookRotation(boid.ValueRO.TargetVector);
        }
    }
}