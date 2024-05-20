using UnityEngine;

public class TestObject : MonoBehaviour
{
    public Vector3 TargetVector { get; private set; }
    public float speed = 5f;

    private void Start() {
        speed = Random.Range(1f, 5f);
        // TargetVector를 랜덤하게 초기화
        TargetVector = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    private void Update()
    {
        // TargetVector 방향으로 이동
        transform.position += TargetVector * speed * Time.deltaTime;
    }
}