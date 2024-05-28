using Unity.Burst;
using Unity.Entities;

public partial struct DotsTestSystem : ISystem {
    //TestMain에서 실행할 내용에 대한 구현이 들어가야함
    
    [BurstCompile]
    public void OnCreate(ref SystemState state) {
        
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state) {
        
    }
}
