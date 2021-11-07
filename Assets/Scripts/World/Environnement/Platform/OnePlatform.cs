using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class OnePlatform : MonoBehaviour
{
    [SerializeField] private float jumpForce = 10f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.y < 0 && collision.gameObject.CompareTag("Player")) // Vitesse relative au deux collisionneurs < 0 -- venant d'en dessous
        {
            Rigidbody rb = collision.collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 velocity = rb.velocity;
                velocity.y = jumpForce;
                rb.velocity = velocity;
            }
            transform.Find("JumpCheck").GetComponent<OneWayEffector>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
