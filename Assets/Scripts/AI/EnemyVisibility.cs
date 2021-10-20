using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class EnemyVisibility : MonoBehaviour
{
    public Transform target = null;
    public float maxDistance = 10f;
    [Range(0, 360f)]
    public float angle = 45;
    [SerializeField]
    bool visualize = true;
    public bool  targetIsVisible { get; private set; }

    private void Update()
    {
        targetIsVisible = CheckVisibility();
        if(visualize)
        {
            var color = targetIsVisible ? Color.yellow : Color.white;
            GetComponent<Renderer>().material.color = color;
        }
    }

    public bool CheckVisibilityToPoint(Vector3 worldPoint)
    {
        var directionToTarget = worldPoint - transform.position;
        var degreesToTarget = Vector3.Angle(transform.forward, directionToTarget);

        var withinArc = degreesToTarget < (angle / 2f);
        if(withinArc == false)
        {
            return false;
        }

        var distanceToTarget = directionToTarget.magnitude;
        var rayDistance = Mathf.Min(maxDistance, distanceToTarget);
        var ray = new Ray(transform.position, directionToTarget);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            if (hit.collider.transform == target)
            {
                return true;
            }
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool CheckVisibility()
    {
        var directionToTarget = target.position - transform.position;
        var degreesToTarget = Vector3.Angle(transform.forward, directionToTarget);

        var withinArc = degreesToTarget < (angle / 2);
        if(!withinArc)
        {
            return false;
        }

        var distanceToTarget = directionToTarget.magnitude;
        var rayDistance = Mathf.Min(maxDistance, distanceToTarget);
        var ray = new Ray(transform.position, directionToTarget);

        RaycastHit hit;
        var canSee = false;
        if(Physics.Raycast(ray, out hit, rayDistance))
        {
            if(hit.collider.transform == target)
            {
                canSee = true;
            }
            Debug.DrawLine(transform.position, hit.point, Color.red);
        }
        else
        {
            Debug.DrawRay(transform.position, directionToTarget.normalized * rayDistance);
        }
        return canSee;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(EnemyVisibility))]
public class EnemyVisibilityEditor: Editor
{
    private void OnSceneGUI()
    {
        var visivility = target as EnemyVisibility;
        Handles.color = new Color(1, 1, 1, 0.1f);
        var forwardPointMinusHalfAngle = Quaternion.Euler(0, -visivility.angle / 2f, 0) * visivility.transform.forward;
        Vector3 arcStart = forwardPointMinusHalfAngle * visivility.maxDistance;
        Handles.DrawSolidArc(visivility.transform.position, Vector3.up, arcStart, visivility.angle, visivility.maxDistance);
        Handles.color = Color.white;
        Vector3 handlePosition = visivility.transform.position + visivility.transform.forward * visivility.maxDistance;
        visivility.maxDistance = Handles.ScaleValueHandle(visivility.maxDistance, handlePosition, visivility.transform.rotation, 1, Handles.ConeHandleCap, 0.25f);
    }
}
#endif
