using UnityEngine;

[CreateAssetMenu(fileName = "AutomonosAgentData", menuName = "Scriptable Objects/AutomonosAgentData")]
public class AutomonosAgentData : ScriptableObject
{
    [Range(0,30)] public float displacement;
    [Range(0,30)] public float radius;
    [Range(0,30)] public float distance;

    [Range(0, 30)] public float cohesionWeight;
    [Range(0, 30)] public float seperationWeight;
    [Range(0, 30)] public float seperationRadius;

    [Range(0, 30)] public float allignmentWeight;
}
