using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienForce : MonoBehaviour
{
    [SerializeField]
    float verticalForce = 1000;
    [SerializeField]
    float sideWaysForce = 1000;

    Rigidbody2D body;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        var vertical = Input.GetAxis("Vertical") * verticalForce;
        var horizontal = Input.GetAxis("Horizontal") * sideWaysForce;

        var force = new Vector2(horizontal, vertical) * Time.deltaTime;
        if (force.magnitude > 0)
        {
            body.AddForce(force);
        }
    }
}
