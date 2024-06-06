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

        foreach(var config in SystemAPI.Query<RefRO<BoidConfig>>()) {
            var instances = state.EntityManager.Instantiate(config.ValueRO.Prefab, config.ValueRO.Count, Allocator.Temp);
            foreach (var entity in instances) {
                var tr = SystemAPI.GetComponentRW<LocalTransform>(entity);
                var obj = SystemAPI.GetComponentRW<BoidObject>(entity);
                var ego = SystemAPI.GetComponentRW<BoidEgo>(entity);
                obj.ValueRW.BasePosition = config.ValueRO.BasePosition;
                obj.ValueRW.BoundRange = config.ValueRO.SpawnRange;
                obj.ValueRW.CenterOffset = config.ValueRO.CenterOffset;
                
                float3 randomVec = Random.insideUnitSphere;
                randomVec *= config.ValueRO.SpawnRange;
                tr.ValueRW.Position = config.ValueRO.BasePosition + randomVec; 
                Quaternion randomRot = Quaternion.Euler(0, Random.Range(0, 360f), 0);
                tr.ValueRW.Rotation = randomRot;

                obj.ValueRW.Speed = Random.Range(config.ValueRO.SpeedRange.x, config.ValueRO.SpeedRange.y);
                obj.ValueRW.TargetVector = new float3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                ego.ValueRW.EgoVector = new float3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            }
        }
        
        /*
        var config = SystemAPI.GetSingleton<BoidConfig>();
        var instances = state.EntityManager.Instantiate(config.Prefab, config.Count, Allocator.Temp);

        foreach (var entity in instances) {
            var tr = SystemAPI.GetComponentRW<LocalTransform>(entity);
            var obj = SystemAPI.GetComponentRW<BoidObject>(entity);
            var ego = SystemAPI.GetComponentRW<BoidEgo>(entity);
            
            float3 randomVec = Random.insideUnitSphere;
            randomVec *= config.SpawnRange;
            tr.ValueRW.Position = config.BasePosition + randomVec; 
            Quaternion randomRot = Quaternion.Euler(0, Random.Range(0, 360f), 0);
            tr.ValueRW.Rotation = randomRot;

            obj.ValueRW.Speed = Random.Range(config.SpeedRange.x, config.SpeedRange.y);
            obj.ValueRW.TargetVector = new float3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            ego.ValueRW.EgoVector = new float3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        }
        */

        state.Enabled = false;      //한번의 플로우가 끝나면 해당 시스템을 비활성화
    }
}