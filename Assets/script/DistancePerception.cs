using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistancePerception : Perception
{
    public override GameObject[] GetGameObjects()
    {
        List<GetGameObjects> result = new List<GameObject>();
        //get all colliders inside sphere
        var colliders = Physics.OverlapSphere(transform.position, maxDistance); 
        foreach (var collider in colliders)
        {   ///do not include outselves
            if (collider.gameObject == gameobject) continue; 
            //check for matching tag
            if(tagName ==""|| collider.tag==tagName)
            {   //check if within max angle range
                Vector3 direction = collider.transform.position - transform.position;
                float angle= Vector3.Angle(direction, transform.forward); 
                if(angle <= maxAngle)
                {   //add game object to result
                    result.Add(collider.gameObject);
                }
            }
           
        }



        return result.ToArray(); 

    }
}
