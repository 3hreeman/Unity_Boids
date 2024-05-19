using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

public class JobUpdater : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[BurstCompile]
public struct MovementJob : IJobParallelFor {
    public float deltaTime;
    public float speed;
    public float additionalSpeed;

    public Vector3 targetVector;
    
    public Vector3 cohesionVector;
    public Vector3 alignmentVector;
    public Vector3 separationVector;

    public Vector3 boundsVector;
    public Vector3 obstacleVector;
    public Vector3 egoVector;

    //countinue point
    
    public void Execute(int index)
    {
    
    }
}