using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidObject : MonoBehaviour {
    public float MoveSpd = 1.0f;
    List<BoidObject> _nearBoidList = new List<BoidObject>();
    public void Init(float spd) {
        MoveSpd = spd;
    }

    // Update is called once per frame
    void Update() {
        
    }
    
    private void FindNearBoids() {
        // Find near boids
        _nearBoidList.Clear();
        var colliders = Physics.OverlapSphere(this.transform.position, 5.0f, LayerMask.GetMask("BoidLayer"));
        foreach (var collider in colliders) {
            if (collider.gameObject != this.gameObject) {
                var boid = collider.gameObject.GetComponent<BoidObject>();
                if (boid != null) {
                    _nearBoidList.Add(boid);
                }
            }
        }
    }
}
