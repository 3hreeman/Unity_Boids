using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = Unity.Mathematics.Random;

[UpdateInGroup(typeof(SimulationSystemGroup))]
public partial struct BoidObjectSystem : ISystem {

    [BurstCompile]
    public void OnUpdate(ref SystemState state) {
        var tick = (float)SystemAPI.Time.DeltaTime;

        foreach (var (boid, xform) in SystemAPI.Query<RefRW<BoidObject>, RefRW<LocalTransform>>()) {
            // CalculateVectors(boid, xform);
            var targetVector = CalculateVectors(boid, xform);
            targetVector = Vector3.Lerp(xform.ValueRO.Forward(), targetVector, tick);
            targetVector = targetVector.normalized;
            if(targetVector == Vector3.zero) {
                targetVector = boid.ValueRO.EgoVector;
            }
            boid.ValueRW.TargetVector = targetVector;
            xform.ValueRW.Position += (boid.ValueRO.TargetVector + boid.ValueRW.EgoVector)* boid.ValueRO.Speed * tick;
            xform.ValueRW.Rotation = Quaternion.LookRotation(boid.ValueRO.TargetVector);
        }
    }

    public void UpdateNeighbors(RefRW<BoidObject> boid, RefRW<LocalTransform> xform) {
        //주변 이웃을 찾아서 리스트 업데이트
        //업데이트를 매번 하지않고 틱을 두고 체크하도록
        
    }
    
    public Vector3 CalculateVectors(RefRW<BoidObject> boid, RefRW<LocalTransform> xform) {
        var boundsVector = CalculateBoundsVector(boid, xform);
        var cohesionVector = CalculateCohesionVector(boid, xform);
        var alignmentVector = CalculateAlignmentVector(boid, xform);
        var separationVector = CalculateSeparationVector(boid, xform);
        var targetVector = cohesionVector + alignmentVector + separationVector + boundsVector + boid.ValueRW.EgoVector;
        return targetVector;
    }
    
    public float3 CalculateCohesionVector(RefRW<BoidObject> boid, RefRW<LocalTransform> xform) {
        return Vector3.zero;
    }
    
    public float3 CalculateAlignmentVector(RefRW<BoidObject> boid, RefRW<LocalTransform> xform) {
        return Vector3.zero;
    }
    
    public float3 CalculateSeparationVector(RefRW<BoidObject> boid, RefRW<LocalTransform> xform) {
        return Vector3.zero;
    }
    
    private float3 CalculateBoundsVector(RefRW<BoidObject> boid, RefRW<LocalTransform> xform) {
        Vector3 offsetToCenter= new float3(0, 50, 0) - xform.ValueRW.Position;
        return offsetToCenter.magnitude >= 30 ? offsetToCenter.normalized : Vector3.zero;
    }
}