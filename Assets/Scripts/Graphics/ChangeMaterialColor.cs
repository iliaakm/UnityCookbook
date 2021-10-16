using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterialColor : MonoBehaviour
{
    [SerializeField]
    Color fromColor = Color.white;
    [SerializeField]
    Color toColor = Color.green;

    [SerializeField]
    float speed = 1f;

    new Renderer renderer;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        float t = Mathf.Sin(Time.time * speed);
        t += 1;
        t /= 2;
        var newColor = Color.Lerp(fromColor, toColor, t);

        renderer.material.color = newColor;
    }
}
