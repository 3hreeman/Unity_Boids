using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = UnityEngine.Random;

[UpdateInGroup(typeof(InitializationSystemGroup))]
public partial struct BoidsSpawnSystem : ISystem {
    
    public void OnCreate(ref SystemState state) {
        state.RequireForUpdate<BoidConfig>();
    }
    
    public void OnUpdate(ref SystemState state) {
        var config = SystemAPI.GetSingleton<BoidConfig>();
        var instances = state.EntityManager.Instantiate(config.Prefab, config.Count, Allocator.Temp);
        
        foreach (var entity in instances) {
            var tr = SystemAPI.GetComponentRW<LocalTransform>(entity);
            var obj = SystemAPI.GetComponentRW<BoidObject>(entity);
            
            float3 randomVec = Random.insideUnitSphere;
            randomVec *= config.SpawnRange;
            tr.ValueRW.Position = config.BasePosition + randomVec; 
            Debug.LogWarning($"{config.BasePosition} * {randomVec} = {tr.ValueRW.Position}");
            Quaternion randomRot = Quaternion.Euler(0, Random.Range(0, 360f), 0);
            tr.ValueRW.Rotation = randomRot;
            
            obj.ValueRW.TargetVector = new float3(0, 0, 0);
            obj.ValueRW.EgoVector = new float3(0, 0, 0);
        }

        state.Enabled = false;      //한번의 플로우가 끝나면 해당 시스템을 비활성화
    }
}