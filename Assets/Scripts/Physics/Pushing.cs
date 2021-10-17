using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushing : MonoBehaviour
{
    public enum PushMode
    {
        NoPushing,
        DirectlySetVelocity,
        ApplyForces
    }

    [SerializeField]
    PushMode pushMode = PushMode.DirectlySetVelocity;
    [SerializeField]
    float pushPower = 5f;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (pushMode == PushMode.NoPushing)
        {
            return;
        }

        var hitRigidbody = hit.rigidbody;
        if (hitRigidbody == null || hitRigidbody.isKinematic)
        {
            return;
        }

        CharacterController controller = hit.controller;
        var footPosition = controller.transform.position.y - controller.center.y - controller.height / 2f;

        if (hit.point.y <= footPosition)
        {
            return;
        }

        switch (pushMode)
        {
            case PushMode.DirectlySetVelocity:
                hitRigidbody.velocity = controller.velocity;
                break;
            case PushMode.ApplyForces:
                Vector3 force = controller.velocity * pushPower;
                hitRigidbody.AddForceAtPosition(force, hit.point);
                break;
        }
    }
}
