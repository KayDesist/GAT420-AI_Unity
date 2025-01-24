using System.Collections.Generic;
using UnityEngine;

public class DistancePerception : Perception
{
    public override GameObject[] GetGameObjects()
    {
        List<GameObject> result = new List<GameObject>();

        //get all colliders inside sphere
       var colliders =  Physics.OverlapSphere(transform.position,maxDistance);
        foreach (var collider in colliders)
        {
            //not us
            if (collider.gameObject == gameObject) continue;
            //check matching tag
            if (tagName == "" || collider.tag == tagName)
            {
                //check angle range
                Vector3 direction = collider.transform.position - transform.position;
                float angle = Vector3.Angle(direction, transform.forward);
                if (angle <= maxAngle)
                {
                    //add object to result
                    result.Add(collider.gameObject);
                }
            }  
        }
        return result.ToArray();
    }

}
