using System.Linq;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public partial struct TestPrefabSystem : ISystem {

   [BurstCompile]
   public void OnUpdate(ref SystemState state) {
       var tick = (float)SystemAPI.Time.DeltaTime;
       foreach(var (obj, xform) in SystemAPI.Query<RefRO<TestPrefab>, RefRW<LocalTransform>>()) {
              xform.ValueRW.Position += obj.ValueRO.DirVector * obj.ValueRO.Speed * tick;
       }
   }
}

[BurstCompile]
public partial struct PrefabMoveJob : IJobEntity {
    public float DeltaTime;
    public void Execute(Entity entity, ref TestPrefab obj, ref LocalTransform transform) {
        transform.Position += obj.DirVector * obj.Speed * DeltaTime;
    }    
}

/* 참고할 스크립트
[BurstCompile]
partial struct CannonBallSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<Execute.CannonBall>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();

        var cannonBallJob = new CannonBallJob
        { 
            ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged),
            DeltaTime = SystemAPI.Time.DeltaTime
        };

        cannonBallJob.Schedule();
    }
}

// IJobEntity relies on source generation to implicitly define a query from the signature of the Execute function.
[BurstCompile]
public partial struct CannonBallJob : IJobEntity
{
    public EntityCommandBuffer ECB;
    public float DeltaTime;

    void Execute(Entity entity, ref CannonBall cannonBall, ref LocalTransform transform)
    {
        var gravity = new float3(0.0f, -9.82f, 0.0f);
        var invertY = new float3(1.0f, -1.0f, 1.0f);

        transform.Position += cannonBall.Velocity * DeltaTime;

        // bounce on the ground
        if (transform.Position.y < 0.0f)
        {
            transform.Position *= invertY;
            cannonBall.Velocity *= invertY * 0.8f;
        }

        cannonBall.Velocity += gravity * DeltaTime;

        var speed = math.lengthsq(cannonBall.Velocity);
        if (speed < 0.1f)
        {
            ECB.DestroyEntity(entity);
        }
    }
}
*/