using System;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

public class JobUpdater : MonoBehaviour
{
    public BoidsMain boidsMain;

    private MovementJob movementJob;
    public JobHandle jobHandle;


    private void Update() {
        movementJob = new MovementJob {
            deltaTime = Time.deltaTime,
            movementData = new NativeArray<MovementData>(boidsMain.boidCount, Allocator.TempJob)
        };
        
        for (int i = 0; i < boidsMain.boidCount; i++) {
            boidsMain.boidUnits[i].UpdateMovementData();
            movementJob.movementData[i] = boidsMain.boidUnits[i].movementData;
        }
        
        jobHandle = movementJob.Schedule(boidsMain.boidCount, 64);
        
        for (int i = 0; i < boidsMain.boidCount; i++) {
            boidsMain.boidUnits[i].movementData = movementJob.movementData[i];
            boidsMain.boidUnits[i].UpdateMovement();
        }
        
    }

    private void OnDestroy() {
        jobHandle.Complete();
        movementJob.movementData.Dispose();
    }
}

public struct MovementData {
    public float3 boundCenter;
    public float boundRadius;
    
    public float speed;
    public float additionalSpeed;
    public float3 forward;
    public float3 position;
    public NativeList<float3> neighborVectors;
    
    public float3 targetVector;
    
    public float3 cohesionVector;
    public float cohesionWeight;
    
    public float3 alignmentVector;
    public float alignmentWeight;
    
    public float3 separationVector;
    public float separationWeight;

    public float3 boundsVector;
    public float boundsWeight;
    
    public float3 obstacleVector;
    public float obstacleWeight;
    
    public float3 egoVector;    
    public float egoWeight;

    public void Init(BoidsMain main, Vector3 pos, float spd) {
        boundCenter = main.transform.position;
        boundRadius = main.spawnRange;
        position = pos;
        speed = spd;
    }
    
    public void UpdateData(float addSpd, Vector3 pos, Vector3 fwd, NativeList<float3> neighbors) {
        additionalSpeed = addSpd;
        position = pos;
        forward = fwd;
        neighborVectors = neighbors;
    }
    
    public float3 CalculateCohesion() {
        float3 cohesion = float3.zero;
        if(neighborVectors.Length == 0) return cohesion;
        
        foreach (var neighborVector in neighborVectors) {
            cohesion += neighborVector;
        }
        cohesion /= neighborVectors.Length;
        cohesion -= position;
        cohesion = math.normalize(cohesion);
        
        cohesionVector = cohesion;
        return cohesionVector * cohesionWeight;
    }
    
    public float3 CalculateAlignment() {
        float3 alignment = new float3(0, 0, 1);
        if(neighborVectors.Length == 0) return alignment;
        
        foreach (var neighborVector in neighborVectors) {
            alignment += neighborVector;
        }
        alignment /= neighborVectors.Length;
        alignment = math.normalize(alignment);

        alignmentVector = alignment;
        return alignmentVector * alignmentWeight;
    }
    
    public float3 CalculateSeparation() {
        float3 separation = float3.zero;
        if(neighborVectors.Length == 0) return separation;
        
        foreach (var neighborVector in neighborVectors) {
            separation += neighborVector;
        }
        separation /= neighborVectors.Length;
        separation = math.normalize(separation);

        separationVector = separation;
        return separationVector * separationWeight;
    }

    public float3 CalculateBounds() {
        var offsetToCenter = boundCenter - position;
        boundsVector = Vector3.Magnitude(offsetToCenter) >= boundRadius ? math.normalize(offsetToCenter) : Vector3.zero;
        return boundsVector * boundsWeight;
    }
    
}

[BurstCompile]
public struct MovementJob : IJobParallelFor {
    public float deltaTime;
    
    public NativeArray<MovementData> movementData;

    //countinue point
    
    public void Execute(int index) {
        var data = movementData[index];
        
        var cohesion = data.CalculateCohesion() * data.cohesionWeight;
        var alignment = data.CalculateAlignment() * data.alignmentWeight;
        var separation = data.CalculateSeparation() * data.separationWeight;
        var bounds = data.CalculateBounds() * data.boundsWeight;
        
        var targetVec = cohesion + alignment + separation + bounds;
        
        targetVec = Vector3.Lerp(data.position, targetVec, deltaTime);
        data.position += targetVec * (data.speed + data.additionalSpeed) * deltaTime;
    }
    
}