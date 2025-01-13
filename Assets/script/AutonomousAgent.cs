using UnityEngine;

public class AutonomousAgent : AIAgent
{
    public Perception perception; 

    public void Update()
    {

        Debug.DrawRay(transform.position, transform.forward * perception.maxDistance, Color.Green); 

        var gameObjects= perception.GetGameObjects(); 
        foreach (var go in gameObjects)
        {
            Debug.DrawLine(transform.position, go.transform.position, Color.red);
        }
    }

}
