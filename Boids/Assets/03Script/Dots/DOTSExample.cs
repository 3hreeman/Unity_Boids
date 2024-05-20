using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct DotsData : IComponentData {
    public float3 position;
}

public class DOTSExample : MonoBehaviour {
    private EntityManager entityManager;

    private void Start() {
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        EntityArchetype boidArchetype = entityManager.CreateArchetype(
            typeof(DotsData)
        );
    }
}

[BurstCompile]
public partial struct MoveSystem : IJobEntity {
    public float deltaTime;
    public float3 boundCenter;
    public float boundRadius;
    
    public void Execute(ref DotsData movementData) {
       
    }
}