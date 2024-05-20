using UnityEngine;

public class TestMain : MonoBehaviour
{
    public GameObject prefab; // Inspector에서 할당할 프리팹
    public int TestCount = 10; // 생성할 오브젝트의 개수
    private TestObject[] testObjects;

    private void Start()
    {
        // TestCount에 따라 오브젝트 생성
        testObjects = new TestObject[TestCount];

        for (int i = 0; i < TestCount; i++)
        {
            // (0, 0, 0) 위치에 오브젝트 생성
            GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            testObjects[i] = obj.GetComponent<TestObject>();
        }
    }
}