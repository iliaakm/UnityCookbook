using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    Vector3 direction =  Vector3.forward;
    [SerializeField]
    float speed;

    void Start()
    {
    }

    void Update()
    {
        var movement = direction * speed;
        movement *= Time.deltaTime;
        this.transform.Translate(movement);
    }

    IEnumerator Countdown(int times)
    {
        for (int i = times; i > 0; i--)
        {
            print($"{i}");

            yield return new WaitForSeconds(1);
        }
        print("Done counting!");
    }
}
