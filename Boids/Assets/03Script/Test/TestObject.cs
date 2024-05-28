using Unity.Entities;
using UnityEngine;

public class TestObject : MonoBehaviour {
    public Vector3 TargetVector;
    public float speed = 5f;
    
    private void Start() {
        Init();
    }

    public void Init() {
        speed = Random.Range(1f, 5f);
        TargetVector = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }

    private void Update()
    {
        // TargetVector 방향으로 이동
        transform.position += TargetVector * speed * Time.deltaTime;
    }
}
