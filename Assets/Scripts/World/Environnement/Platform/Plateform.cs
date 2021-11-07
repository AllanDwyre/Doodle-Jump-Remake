using UnityEngine;

public class Plateform : MonoBehaviour
{
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private Material glowMat = null;
    private MeshRenderer meshRenderer;

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
                GlowOn();
            }
        }
    }

    private void GlowOn()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = glowMat;
    }
}
