using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleChecker : MonoBehaviour
{
    [SerializeField]
    Transform target;

    float angleBetween;

    private void FixedUpdate()
    {
        var directionToTarget = (target.position - transform.position).normalized;
        var dotProduct = Vector3.Dot(transform.forward, directionToTarget);
        var angle = Mathf.Acos(dotProduct);
        if (angle != angleBetween)
        {
            angleBetween = angle;
            print($"The angle between my forward direction and {target.name} {angle * Mathf.Rad2Deg}");
        }
    }
}
