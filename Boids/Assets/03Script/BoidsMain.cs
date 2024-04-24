using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidsMain : MonoBehaviour
{
    public GameObject boidPrefab;
    public int numberOfBoids;
    public List<GameObject> boids = new List<GameObject>();
    public float GenerateRadius = 10.0f;
    // Start is called before the first frame update
    void Start() {
        CreateBoids();
    }
    
    private void CreateBoids() {
        for (int i = 0; i < numberOfBoids; i++) {
            var pos = Random.insideUnitSphere * GenerateRadius;
            var randomRotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
            GameObject boid = Instantiate(boidPrefab, pos, randomRotation);
            boid.transform.parent = this.transform;
            boids.Add(boid);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
