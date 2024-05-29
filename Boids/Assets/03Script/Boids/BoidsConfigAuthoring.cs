using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class BoidsConfigAuthoring : MonoBehaviour {

    public float CellRadius;
    public float SeparationWeight;
    public float AlignmentWeight;
    public float TargetWeight;
    public float ObstacleAversionDistance;
    public float MoveSpeed;
    
    class Baker : Baker<BoidsConfigAuthoring> {
        public override void Bake(BoidsConfigAuthoring authoring) {
            var entity = GetEntity(TransformUsageFlags.Renderable | TransformUsageFlags.WorldSpace);
            var data = new BoidConfig() {
                CellRadius = authoring.CellRadius,
                SeparationWeight = authoring.SeparationWeight,
                AlignmentWeight = authoring.AlignmentWeight,
                TargetWeight = authoring.TargetWeight,
                ObstacleAversionDistance = authoring.ObstacleAversionDistance,
                MoveSpeed = authoring.MoveSpeed
            };
            
            AddComponent(entity, data);
        }
    }
}


public partial struct BoidConfig : IComponentData
{
    public float CellRadius;
    public float SeparationWeight;
    public float AlignmentWeight;
    public float TargetWeight;
    public float ObstacleAversionDistance;
    public float MoveSpeed;
}