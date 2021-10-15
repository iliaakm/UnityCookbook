using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeChecker : MonoBehaviour
{
    [SerializeField]
    Transform target;
    [SerializeField]
    float range = 5f;

    private bool targetWasInRange = false;

    private void Update()
    {
        var distance = (target.position - transform.position).magnitude;
        if(distance <= range && !targetWasInRange)
        {
            print($"Target {target.name} entered range!");
            targetWasInRange = true;
        }
        else if(distance > range && targetWasInRange)
        {
            print($"Target {target.name} exited range!");
            targetWasInRange = false;
        }
    }
}
