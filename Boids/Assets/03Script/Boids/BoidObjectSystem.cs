using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = Unity.Mathematics.Random;

[UpdateInGroup(typeof(SimulationSystemGroup))]
public partial struct BoidObjectSystem : ISystem {

    public void OnUpdate(ref SystemState state) {
        var tick = (float)SystemAPI.Time.DeltaTime;

        foreach (var (boid, xform) in SystemAPI.Query<RefRW<BoidObject>, RefRW<LocalTransform>>()) {
            // CalculateVectors(boid, xform);
            boid.ValueRW.TargetVector = CalculateBoundsVector(boid, xform);
            boid.ValueRW.EgoVector = UnityEngine.Random.insideUnitSphere;
            xform.ValueRW.Position += (boid.ValueRO.TargetVector + boid.ValueRW.EgoVector)* boid.ValueRO.Speed * tick;
            // xform.ValueRW.Rotation = Quaternion.LookRotation(boid.ValueRO.TargetVector);
        }
    }

    public void UpdateNeighbors(RefRW<BoidObject> boid, RefRW<LocalTransform> xform) {
        //주변 이웃을 찾아서 리스트 업데이트
        //업데이트를 매번 하지않고 틱을 두고 체크하도록
        
    }
    
    public void CalculateVectors(RefRW<BoidObject> boid, RefRW<LocalTransform> xform) {
        var boundsVector = CalculateBoundsVector(boid, xform);
        boid.ValueRW.TargetVector = boundsVector;
    }
    
    private Vector3 CalculateBoundsVector(RefRW<BoidObject> boid, RefRW<LocalTransform> xform) {
        Vector3 offsetToCenter= new float3(0, 50, 0) - xform.ValueRW.Position;
        return offsetToCenter.magnitude >= 30 ? offsetToCenter.normalized : Vector3.zero;
    }
}