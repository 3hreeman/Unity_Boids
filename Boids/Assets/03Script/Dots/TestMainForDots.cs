using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = UnityEngine.Random;

public struct TestObjectData : IComponentData
{
    public float3 TargetVector;
    public float Speed;
}

public class TestMainForDots : MonoBehaviour {
    public GameObject prefab; // Inspector에서 할당할 프리팹
    public int TestCount = 10; // 생성할 오브젝트의 개수

    private EntityManager entityManager;
    private Entity prefabEntity;

    private void Start()
    {
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        // TestCount에 따라 엔티티 생성
        for (int i = 0; i < TestCount; i++)
        {
            Entity entity = entityManager.Instantiate(prefabEntity);

            // 초기 위치 설정
            entityManager.SetComponentData(entity, LocalTransform.FromPosition(float3.zero));

            // 랜덤한 방향 벡터 설정
            float3 targetVector = math.normalize(new float3(Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)));
            float spd = Random.Range(1f, 5f);
            entityManager.SetComponentData(entity, new TestObjectData
            {
                TargetVector = targetVector,
                Speed = spd
            });
        }
        
    }
}

public class PrefabBaker : Baker<TestObjectDataAuthoring> {
    public override void Bake(TestObjectDataAuthoring author) {
        Debug.LogWarning("PrefabBaker :: Bake!!");
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new TestObjectData() {
            TargetVector = math.normalize(new float3(Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f))),
            Speed = Random.Range(1f, 5f)
        });
    }
}
