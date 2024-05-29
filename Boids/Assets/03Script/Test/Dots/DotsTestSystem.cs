using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[UpdateInGroup(typeof(InitializationSystemGroup))]  //업데이트 타이밍 명시적 지정, 인스턴스 생성 처리를 다른 업데이트보다 먼저 실행되도록
public partial struct DotsTestSystem : ISystem {
    //TestMain에서 실행할 내용에 대한 구현이 들어가야함
    
    public void OnCreate(ref SystemState state) {
        state.RequireForUpdate<ConfigAuthoring.Config>();
        //이 시스템은 월드 내에 Config컴포넌트가 존재할 때만 실행됨
        //ECS의 SubScene은 비동기로 로드되기 때문에, 첫 프레임부터 Config가 존재함이 보장되지 않기 때문
    }

    public void OnUpdate(ref SystemState state) {
        var config = SystemAPI.GetSingleton<ConfigAuthoring.Config>();  //SystemAPI를 통해 월드 안에 오직 하나 존재하는 Config를 가져옴

        var instances = state.EntityManager.Instantiate(config.Prefab, config.TestCount, Allocator.Temp);
        foreach (var entity in instances) {
            var tr = SystemAPI.GetComponentRW<LocalTransform>(entity);
            var obj = SystemAPI.GetComponentRW<TestPrefab>(entity);
            
            tr.ValueRW.Position = float3.zero;
            obj.ValueRW.DirVector = new float3(UnityEngine.Random.Range(config.DirRange.x, config.DirRange.y), UnityEngine.Random.Range(config.DirRange.x, config.DirRange.y), UnityEngine.Random.Range(config.DirRange.x, config.DirRange.y));
            obj.ValueRW.Speed = UnityEngine.Random.Range(config.SpeedRange.x, config.SpeedRange.y);
        }
        state.Enabled = false;
    }
}
