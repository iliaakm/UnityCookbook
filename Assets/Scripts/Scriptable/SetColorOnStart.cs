using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetColorOnStart : MonoBehaviour
{
    [SerializeField]
    ObjectColor objectColor;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        if (objectColor == null)
        {
            return;
        }

        GetComponent<Renderer>().material.color = objectColor.color;
    }
}
