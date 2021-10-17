using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interacting : MonoBehaviour
{
    [SerializeField]
    KeyCode interactionKey = KeyCode.E;
    [SerializeField]
    float interactionRange = 2f;

    private void Update()
    {
        if(Input.GetKeyDown(interactionKey))
        {
            AttemptInteraction();
        }
    }

    private void AttemptInteraction()
    {
        var everythingExceptPlayers = ~(1 << LayerMask.NameToLayer("Player"));
        var layerMask = Physics.DefaultRaycastLayers & everythingExceptPlayers;

        var ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, interactionRange, layerMask))
        {
            var interactable = hit.collider.GetComponent<Interactable>();
            if(interactable != null)
            {
                interactable.Interact(this.gameObject);
            }
        }
    }
}
