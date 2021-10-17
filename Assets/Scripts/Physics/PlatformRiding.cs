using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlatformRiding : MonoBehaviour
{
    CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        var capsulePoint1 = transform.position + new Vector3(0, (controller.height / 2f) - controller.radius, 0);
        var caspulePoint2 = transform.position - new Vector3(0, (controller.height / 2f) + controller.radius, 0);

        Collider[] overlappingColliders = new Collider[10];
        var overlapCount = Physics.OverlapCapsuleNonAlloc(capsulePoint1, caspulePoint2, controller.radius, overlappingColliders);

        for (int i = 0; i < overlapCount; i++)
        {
            var overlappingCollider = overlappingColliders[i];

            if (overlappingCollider == controller)
            {
                continue;
            }

            Vector3 direction;
            float distance;

            Physics.ComputePenetration(
                controller,
                transform.position,
                transform.rotation,
                overlappingCollider,
                overlappingCollider.transform.position,
                overlappingCollider.transform.rotation,
                out direction,
                out distance
            );

            direction.y = 0;

            transform.position += direction * distance;
        }

        var ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        float maxDistance = (controller.height / 2f) + 0.1f;

        if(Physics.Raycast(ray, out hit, maxDistance))
        {
            var platform = hit.collider.gameObject.GetComponent<MovingPlatform>();
            if(platform != null)
            {
                transform.position += platform.velocity * Time.deltaTime;
            }
        }
    }
}
