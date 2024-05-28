using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Jobs;
using Random = UnityEngine.Random;

public class TestMainForJob : MonoBehaviour {
    public GameObject prefab; // Inspector에서 할당할 프리팹
    public int TestCount = 10; // 생성할 오브젝트의 개수
    
    public JobHandle jobHandle;
    public TestJob job;
    
    private GameObject[] testObjects;

    private NativeArray<float3> DirVectors;
    private NativeArray<float> Speeds;
    private Transform[] Positions; 
    private TransformAccessArray PositionsAccess;
    
    // Start is called before the first frame update
    void Start() {
        testObjects = new GameObject[TestCount];
        DirVectors = new NativeArray<float3>(TestCount, Allocator.Persistent);
        Speeds = new NativeArray<float>(TestCount, Allocator.Persistent);
        Positions = new Transform[TestCount];
        for (int i = 0; i < TestCount; i++) {
            var obj = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            testObjects[i] = obj;
            DirVectors[i] = new float3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            Speeds[i] = Random.Range(1f, 5f);
            Positions[i] = obj.transform;
        }
        
        PositionsAccess = new TransformAccessArray(Positions);
        job = new TestJob() {
            DeltaTime = Time.deltaTime,
            DirVector = DirVectors,
            Speed = Speeds
        };
    }

    // Update is called once per frame
    void Update() {
        job.DeltaTime = Time.deltaTime;
        jobHandle = job.Schedule(PositionsAccess);
        jobHandle.Complete();
    }
}

[BurstCompile]
public struct TestJob : IJobParallelForTransform {
    public float DeltaTime;
    public NativeArray<float3> DirVector;
    public NativeArray<float> Speed;

    public void Execute(int index, TransformAccess transform) {
        // 위치 업데이트
        float3 position = transform.position;
        position += DirVector[index] * Speed[index] * DeltaTime;
        transform.position = position;   
    }
}