using UnityEngine;

public class DestroyOffScreen : MonoBehaviour
{
    void Update()
    {

        if (gameObject.CompareTag("Background"))
        {
            if (Camera.main.WorldToViewportPoint(transform.position).y < -1f && !gameObject.GetComponent<Renderer>().isVisible)
            {
                FindObjectOfType<BackgroundGenerator>().GetComponent<BackgroundGenerator>().currentCubeNbr -= 1;
                Destroy(gameObject);
            }
        }
        else
        {
            if (Camera.main.WorldToViewportPoint(transform.position).y < -0.05f && !gameObject.GetComponent<Renderer>().isVisible)
            {
                FindObjectOfType<LevelGenerator>().GetComponent<LevelGenerator>().currentNumberPlateform -= 1;
                Destroy(gameObject);
            }
        }
    }
}
