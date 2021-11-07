using UnityEngine;

public class OneWayEffector : MonoBehaviour
{
    [SerializeField] private BoxCollider plateformCollider;
    private bool oneWay;


    // Update is called once per frame
    void Update()
    {
        if (oneWay)
        {
            plateformCollider.enabled = false;
        }
        else
        {
            plateformCollider.enabled = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            oneWay = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            oneWay = false;
        }
    }
}
