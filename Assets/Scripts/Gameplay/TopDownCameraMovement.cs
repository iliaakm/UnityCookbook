using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCameraMovement : MonoBehaviour
{
    [SerializeField]
    float movementSpeed = 20;
    [SerializeField]
    Vector2 minimumLimit = -Vector2.one;
    [SerializeField]
    Vector2 maximumLimit = Vector2.one;

    Bounds bounds
    {
        get
        {
            var cameraHeight = transform.position.y;
            Vector3 minLimit = new Vector3(minimumLimit.x, cameraHeight, minimumLimit.y);
            Vector3 maxLimit = new Vector3(maximumLimit.x, cameraHeight, maximumLimit.y);

            var newBounds = new Bounds();
            newBounds.min = minLimit;
            newBounds.max = maxLimit;

            return newBounds;
        }
    }

    private void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        var offset = new Vector3(horizontal, 0, vertical) * Time.deltaTime * movementSpeed;
        var newPosition = transform.position + offset;

        if (bounds.Contains(newPosition))
        {
            transform.position = newPosition;
        }
        else
        {
            transform.position = bounds.ClosestPoint(newPosition);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(bounds.center, bounds.size);
    }
}
