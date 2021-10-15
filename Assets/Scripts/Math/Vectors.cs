using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vectors : MonoBehaviour
{
    Vector3 someOtherObjectPosition;

    void CalculateForwardRelatedPosition()
    {
        var directionToOtherObject = someOtherObjectPosition = transform.position;
        var differenceFromMyForwardDirection = Vector3.Dot(transform.forward, directionToOtherObject);

        if(differenceFromMyForwardDirection > 0)
        {
            print("Object is in front of us");
        }
        else if(differenceFromMyForwardDirection < 0)
        {
            print("Object is behind us");
        }
        else
        {
            print("Both at same forward distance");
        }
    }

    void Move()
    {
        var moved = Vector3.MoveTowards(Vector3.zero, Vector3.one, 0.5f);
    }

    void Reflect()
    {
        var reflected = Vector3.Reflect(new Vector3(0.5f, 1f, 0f), Vector3.up);
    }

    void Rotation()
    {
        var rotation = Quaternion.Euler(90, 0, 0);
        var input = new Vector3(0, 0, 1);
        var result = rotation * input;
    }

    void Interpolate()
    {
        var identity = Quaternion.identity;
        var rotationX = Quaternion.Euler(90, 0, 0);
        var halfwayRotated = Quaternion.Slerp(identity, rotationX, 0.5f);
    }

}
