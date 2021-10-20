using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoissonDiskDemo : MonoBehaviour
{
    [SerializeField]
    Vector2 size = new Vector2(10, 10);
    [SerializeField]
    float cellSize = 0.5f;

    List<Vector3> points;

    private void Awake()
    {
        points = new List<Vector3>();
        var sampler = new PoissonDiskSampler(size.x, size.y, cellSize);

        foreach (var point in sampler.Samples())
        {
            points.Add(new Vector3(point.x, transform.position.y, point.y));
        }
    }

    private void OnDrawGizmos()
    {
        if(points == null)
        {
            return;
        }

        Gizmos.color = Color.white;
        foreach (var point in points)
        {
            Gizmos.DrawSphere(transform.position + point, 0.1f);
        }
    }
}
