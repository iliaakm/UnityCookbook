using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Interactable : MonoBehaviour
{
    public void Interact(GameObject fromObject)
    {
        print($"I've been interacted with by {fromObject}");
        GetComponent<Renderer>().material.color = Color.red;
    }
}
