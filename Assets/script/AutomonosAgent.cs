using UnityEngine;

public class AutomonosAgent : AIAgent
{
    [SerializeField] AutomonosAgentData data;

    [Header("Perception")]
    public Perception seekPerception;
    public Perception fleePerception;
    public Perception flockPerception;

     float angle;
    void Update()
    {
        //movement.ApplyForce(Vector3.forward * 10);

        //Debug.DrawRay(transform.position,transform.forward * perception.maxDistance,Color.green);

        //seek
        if(seekPerception != null)
        {
            var gameObjects = seekPerception.GetGameObjects();
            if (gameObjects.Length > 0)
            {
                Vector3 force = Seek(gameObjects[0]);
                movement.ApplyForce(force);
            }

            foreach (var go in gameObjects)
            {
                Debug.DrawLine(transform.position, go.transform.position, Color.red);
            }
        }
        //flee
        if(fleePerception != null)
        {
            var gameObjects = fleePerception.GetGameObjects();
            if (gameObjects.Length > 0)
            {
                Vector3 force = Flee(gameObjects[0]);
                movement.ApplyForce(force);
            }
        }
        //flock
        if(flockPerception != null)
        {
            var gameObjects = flockPerception.GetGameObjects();
            if(gameObjects.Length > 0)
            {
                movement.ApplyForce(Cohesion(gameObjects) * data.cohesionWeight);
                movement.ApplyForce(Seperation(gameObjects,data.seperationRadius) * data.seperationWeight);
                movement.ApplyForce(Allignment(gameObjects) * data.allignmentWeight);
                
            }
        }
        //wander
        if(movement.Acceleration.sqrMagnitude == 0)
        {
            Vector3 force = Wander();
            movement.ApplyForce(force);
        }

        Vector3 acceleration = movement.Acceleration;
        acceleration.y = 0;
        movement.Acceleration = acceleration;
        if(movement.Direction.sqrMagnitude != 0)
        {
            transform.rotation = Quaternion.LookRotation(movement.Direction);
        }
        

    }
    private Vector3 Cohesion(GameObject[] neighbors)
    {
        Vector3 positions = Vector3.zero;
        foreach (var neighbor in neighbors)
        {
            positions += neighbor.transform.position;
        }
        Vector3 center = positions / neighbors.Length;
        Vector3 direction = center - transform.position;

        Vector3 force = GetSteeringForce(direction);
        return force;
    }
    private Vector3 Seperation(GameObject[] neighbors,float radius)
    {
        return Vector3.zero;
    }
    private Vector3 Allignment(GameObject[] neighbors)
    {
        return Vector3.zero;
    }


    private Vector3 Seek(GameObject target)
    {
        Vector3 direction = target.transform.position - transform.position;
        Vector3 force = GetSteeringForce(direction);

        return force;
    }
    private Vector3 Flee(GameObject target)
    {
        Vector3 direction = target.transform.position - transform.position;
        Vector3 force = GetSteeringForce(-direction);

        return force;
    }
    
    private Vector3 Wander()
    {
        angle += Random.Range(-data.displacement, data.displacement);
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
        Vector3 point = rotation * (Vector3.forward * data.radius);

        Vector3 forward = movement.Direction * data.distance;
        Vector3 force = GetSteeringForce(forward + point);
        return force;
    }

    private Vector3 GetSteeringForce(Vector3 direction)
    {
        Vector3 desire = direction.normalized * movement.data.maxSpeed;
        Vector3 steer = desire - movement.Velocity;
        Vector3 force = Vector3.ClampMagnitude(steer, movement.data.maxForce);

        return force;
    }
}
