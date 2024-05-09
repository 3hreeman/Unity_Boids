using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidsMain : MonoBehaviour
{
    public static BoidsMain Instance;
    public GameObject boidPrefab;
    public int numberOfBoids;
    public List<GameObject> boids = new List<GameObject>();
    [Range(5, 20)] public float GenerateRadius = 10.0f;
    [Range(1, 10)] public float BoidSpeed = 3.0f;
    [Range(1, 10)] public float BoidRadius = 5.0f;
    
    public float CohesionWeight = 1;
    public float SeparationWeight = 1;
    public float AlignmentWeight = 1;
    
    public static float GlobalSpd => Instance.BoidSpeed;
    public static float GlobalRadius => Instance.BoidRadius
    ;
    // Start is called before the first frame update
    void Start() {
        Instance = this;
        CreateBoids();
    }
    
    private void CreateBoids() {
        for (int i = 0; i < numberOfBoids; i++) {
            var pos = Random.insideUnitSphere * GenerateRadius;
            var randomRotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
            GameObject boid = Instantiate(boidPrefab, pos, randomRotation);
            boid.transform.parent = this.transform;
            boid.GetComponent<BoidObject>().Init(BoidSpeed);
            boids.Add(boid);
        }
    }
}
