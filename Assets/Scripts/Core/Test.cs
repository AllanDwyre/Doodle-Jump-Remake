using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float radius = 1;
    public Vector2 regionSize = Vector2.one;
    public int rejectionSample = 30;


    List<Vector2> points;


    // Start is called before the first frame update
    void OnValidate()
    {
        points = PoissonDiskSampling.GeneratePoints(radius, regionSize, rejectionSample);
    }

    // Update is called once per frame
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(regionSize / 2, regionSize);
        if (points != null)
        {
            foreach (Vector2 point in points)
            {
                Gizmos.DrawSphere(point, radius * .1f);
                Gizmos.DrawWireSphere(point, radius);
            }
        }
    }
}
