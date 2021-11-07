using UnityEngine;

public class PlayerOnScreenEdge : MonoBehaviour
{
    void Update()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);


        if (pos.x < 0.0)
        {
            Vector3 x = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, pos.y, pos.z));
            //Debug.Log("I am left of the camera's view.");
            transform.position = x;
        }
        if (pos.x > 1.0)
        {
            Vector3 x = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, pos.y, pos.z));
            //Debug.Log("I am right of the camera's view.");
            transform.position = x;
        }

    }
}
