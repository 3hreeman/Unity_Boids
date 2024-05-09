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
        var cohesion = UpdateCohesionVector() * BoidsMain.Instance.CohesionWeight;
        var alignment = UpdateAlignmentVector() * BoidsMain.Instance.AlignmentWeight;
        var separation = UpdateSeparationVector() * BoidsMain.Instance.SeparationWeight;
        
        var targetVec = cohesion + alignment + separation;
        
        targetVec = Vector3.Lerp(transform.forward, targetVec, Time.deltaTime);
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
    
    private Vector3 UpdateCohesionVector() {
        // Cohesion
        Vector3 center = Vector3.zero;
        foreach (var boid in _nearBoidList) {
            center += boid.transform.position;
        }
        center /= Mathf.Max(1, _nearBoidList.Count);
        return center - transform.position;
    }
    
    private Vector3 UpdateAlignmentVector() {
        // Alignment
        Vector3 alignment = Vector3.zero;
        if(_nearBoidList.Count == 0) return alignment;
        
        foreach (var boid in _nearBoidList) {
            alignment += boid.transform.forward;
        }
        return alignment/_nearBoidList.Count;
    }
    
    public Vector3 UpdateSeparationVector() {
        // Separation
        Vector3 separation = Vector3.zero;
        if(_nearBoidList.Count == 0) return separation;
        
        foreach (var boid in _nearBoidList) {
            var diff = transform.position - boid.transform.position;
            separation += diff;
        }
        return separation/_nearBoidList.Count;
    }
}
