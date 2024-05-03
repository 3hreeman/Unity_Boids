using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidObject : MonoBehaviour {
    private float MoveSpd = 3;
    private float Radius = 5;
    public List<BoidObject> _nearBoidList = new List<BoidObject>();
    public void Init(float spd) {
        MoveSpd = spd;
    }

    // Update is called once per frame
    void Update() {
        FindNearBoids();
        UpdateCohesionVector();
        var targetVec = Vector3.Lerp(transform.forward, lastCohesionVector, Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(targetVec);
        transform.position += targetVec * BoidsMain.GlobalSpd * Time.deltaTime;
    }
    
    private void FindNearBoids() {
        // Find near boids
        _nearBoidList.Clear();
        var colliders = Physics.OverlapSphere(transform.position, BoidsMain.GlobalRadius, 1 << LayerMask.NameToLayer("BoidLayer"));
        foreach (var collider in colliders) {
            if (collider.gameObject != this.gameObject) {
                var boid = collider.gameObject.GetComponent<BoidObject>();
                if (boid != null) {
                    _nearBoidList.Add(boid);
                }
            }
        }
    }
    public Vector3 lastCohesionVector = Vector3.zero;
    private void UpdateCohesionVector() {
        // Cohesion
        Vector3 center = Vector3.zero;
        foreach (var boid in _nearBoidList) {
            center += boid.transform.position;
        }
        center /= Mathf.Max(1, _nearBoidList.Count);
        lastCohesionVector = center - transform.position;
    }
}
