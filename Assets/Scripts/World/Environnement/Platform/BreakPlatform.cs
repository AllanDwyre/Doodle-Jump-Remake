using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class BreakPlatform : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.y < 0 && collision.gameObject.CompareTag("Player")) // Vitesse relative au deux collisionneurs < 0 -- venant d'en dessous
        {
            transform.Find("JumpCheck").GetComponent<OneWayEffector>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().useGravity = true;

        }
    }
}
