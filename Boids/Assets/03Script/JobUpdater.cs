using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class JobUpdater : MonoBehaviour
{
    public BoidsMain boidsMain;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public struct MovementData {
    public Vector3 boundCenter;
    public float boundRadius;
    
    public float speed;
    public float additionalSpeed;
    public Vector3 forward;
    public Vector3 position;
    public List<Vector3> neighborVectors;

    
    public Vector3 targetVector;
    
    public Vector3 cohesionVector;
    public float cohesionWeight;
    
    public Vector3 alignmentVector;
    public float alignmentWeight;
    
    public Vector3 separationVector;
    public float separationWeight;

    public Vector3 boundsVector;
    public float boundsWeight;
    
    public Vector3 obstacleVector;
    public float obstacleWeight;
    
    public Vector3 egoVector;    
    public float egoWeight;

    public void Init(BoidsMain main, Vector3 pos, float spd) {
        boundCenter = main.transform.position;
        boundRadius = main.spawnRange;
        position = pos;
        speed = spd;
    }
    
    public void UpdateData(float addSpd, Vector3 pos, Vector3 fwd, List<Vector3> neighbors) {
        additionalSpeed = addSpd;
        position = pos;
        forward = fwd;
        neighborVectors = neighbors;
    }
    
    public Vector3 CalculateCohesion() {
        Vector3 cohesion = Vector3.zero;
        if(neighborVectors.Count == 0) return cohesion;
        
        foreach (var neighborVector in neighborVectors) {
            cohesion += neighborVector;
        }
        cohesion /= neighborVectors.Count;
        cohesion -= position;
        cohesion.Normalize();

        cohesionVector = cohesion;
        return cohesionVector * cohesionWeight;
    }
    
    public Vector3 CalculateAlignment() {
        Vector3 alignment = Vector3.forward;
        if(neighborVectors.Count == 0) return alignment;
        
        foreach (var neighborVector in neighborVectors) {
            alignment += neighborVector;
        }
        alignment /= neighborVectors.Count;
        alignment.Normalize();

        alignmentVector = alignment;
        return alignmentVector * alignmentWeight;
    }
    
    public Vector3 CalculateSeparation() {
        Vector3 separation = Vector3.zero;
        if(neighborVectors.Count == 0) return separation;
        
        foreach (var neighborVector in neighborVectors) {
            separation += neighborVector;
        }
        separation /= neighborVectors.Count;
        separation.Normalize();

        separationVector = separation;
        return separationVector * separationWeight;
    }

    public Vector3 CalculateBounds() {
        var offsetToCenter = boundCenter - position;
        boundsVector = offsetToCenter.magnitude >= boundRadius ? offsetToCenter.normalized : Vector3.zero;
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