using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitingCamera : MonoBehaviour
{
    [SerializeField]
    Transform target;
    [SerializeField]
    bool clipCamera;

    [SerializeField]
    float rotationSpeed = 120f;
    [SerializeField]
    float elevationSpeed = 120f;

    [SerializeField]
    float elevationMinLimit = -20f;
    [SerializeField]
    float elevationMaxLimit = 80f;

    [SerializeField]
    float distance = 5;
    [SerializeField]
    float distanceMin = 0.5f;
    [SerializeField]
    float distanceMax = 15f;

    float rotationAroundTarget = 0f;
    float elevationToTarget = 0f;

    private void Start()
    {
        Vector3 angles = transform.eulerAngles;
        rotationAroundTarget = angles.y;
        elevationToTarget = angles.x;

        if (target)
        {
            float currentDistance = (transform.position - target.position).magnitude;
            distance = Mathf.Clamp(currentDistance, distanceMin, distanceMax);
        }
    }

    private void LateUpdate()
    {
        if (target)
        {
            rotationAroundTarget += Input.GetAxis("Mouse X") * rotationSpeed * distance * 0.02f;
            elevationToTarget -= Input.GetAxis("Mouse Y") * elevationSpeed * 0.02f;
            elevationToTarget = ClampAngle(elevationToTarget, elevationMinLimit, elevationMaxLimit);

            Quaternion rotation = Quaternion.Euler(elevationToTarget, rotationAroundTarget, 0);
            distance -= (Input.GetAxis("Mouse ScrollWheel") * 5f);
            distance = Mathf.Clamp(distance, distanceMin, distanceMax);

            Vector3 negDistance = new Vector3(0f, 0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            if (clipCamera)
            {
                RaycastHit hitInfo;
                var ray = new Ray(target.position, position - target.position);
                var hit = Physics.Raycast(ray, out hitInfo, distance);
                if(hit)
                {
                    position = hitInfo.point;
                }
            }

            transform.position = position;
            transform.rotation = rotation;
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f) angle += 360f;
        if (angle > 360f) angle -= 360f;

        return Mathf.Clamp(angle, min, max);
    }
}
