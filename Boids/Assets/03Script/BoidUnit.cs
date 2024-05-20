using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoidUnit : MonoBehaviour {
    #region Variables & Initializer

    [Header("Info")] BoidsMain myBoids;
    NativeList<float3> nearBoidPosList = new NativeList<float3>();
    
    Vector3 targetVec;
    Vector3 egoVector;

    float additionalSpeed = 0;
    bool isEnemy;

    MeshRenderer myMeshRenderer;
    TrailRenderer myTrailRenderer;
    [SerializeField] private Color myColor;

    [Header("Neighbour")] [SerializeField] float obstacleDistance;
    [SerializeField] float FOVAngle = 120;
    [SerializeField] float maxNeighbourCount = 50;
    [SerializeField] float neighbourDistance = 10;

    [Header("ETC")] [SerializeField] LayerMask boidUnitLayer;
    [SerializeField] LayerMask obstacleLayer;

    Coroutine findNeighbourCoroutine;
    Coroutine calculateEgoVectorCoroutine;

    private Transform _transform;
    public MovementData movementData;
    public void InitializeUnit(BoidsMain _boids, float _speed, int _myIndex) {
        _transform = transform;
        myBoids = _boids;

        movementData = new MovementData();
        movementData.Init(myBoids, _transform.position, _speed);

        myTrailRenderer = GetComponentInChildren<TrailRenderer>();
        myMeshRenderer = GetComponentInChildren<MeshRenderer>();

        // set Color
        if (myBoids.randomColor) {
            myColor = new Color(Random.value, Random.value, Random.value);
            myMeshRenderer.material.color = myColor;
        }
        else if (myBoids.blackAndWhite) {
            float myIndexFloat = _myIndex;
            myColor = new Color(myIndexFloat / myBoids.boidCount, myIndexFloat / myBoids.boidCount,
                myIndexFloat / myBoids.boidCount, 1f);
        }
        else {
            myColor = myMeshRenderer.material.color;
        }

        // is Enemy?
        if (Random.Range(0, 1f) < myBoids.enemyPercentage) {
            myColor = new Color(1, 0, 0);
            isEnemy = true;
            transform.gameObject.layer = LayerMask.NameToLayer("Obstacle");
        }

        // findNeighbourCoroutine = StartCoroutine("FindNeighbourCoroutine");
        // calculateEgoVectorCoroutine = StartCoroutine("CalculateEgoVectorCoroutine");
    }

    #endregion

    public void UpdateMovementData() {
        movementData.UpdateData(additionalSpeed, _transform.position, _transform.forward, nearBoidPosList);
    }
    
    public void UpdateMovement() {
        transform.rotation = Quaternion.LookRotation(movementData.targetVector);
        transform.position = movementData.position;
    }

    #region Calculate Vectors

    IEnumerator CalculateEgoVectorCoroutine() {
        egoVector = Random.insideUnitSphere;
        yield return new WaitForSeconds(Random.Range(1, 3f));
        calculateEgoVectorCoroutine = StartCoroutine("CalculateEgoVectorCoroutine");
    }

    IEnumerator FindNeighbourCoroutine() {
        nearBoidPosList = new NativeList<float3>();

        Collider[] colls = Physics.OverlapSphere(transform.position, neighbourDistance, boidUnitLayer);
        for (int i = 0; i < colls.Length; i++) {
            if (Vector3.Angle(transform.forward, colls[i].transform.position - transform.position) <= FOVAngle) {
                nearBoidPosList.Add(colls[i].transform.position);
            }

            if (i > maxNeighbourCount) {
                break;
            }
        }

        yield return new WaitForSeconds(Random.Range(0.5f, 2f));
        findNeighbourCoroutine = StartCoroutine("FindNeighbourCoroutine");
    }

    #endregion
    
}