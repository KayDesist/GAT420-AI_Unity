using UnityEngine;

[CreateAssetMenu(fileName = "MovementData", menuName = "Scriptable Objects/MovementData")]
public class MovementData : ScriptableObject
{
     [Range(1,20)] public float minSpeed = 5;
     [Range(1,20)] public float maxSpeed = 5;
     [Range(1,20)] public float maxForce = 5;
}
