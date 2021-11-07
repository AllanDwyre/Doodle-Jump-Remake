using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private Vector2 smoothSpeedClamp = new Vector2(9f, 11f);
    [SerializeField] private Material glowMat = null;
    [SerializeField] private Vector3 startPos = new Vector3();
    [SerializeField] private Vector3 endPos = new Vector3();

    private float smoothSpeed;
    private MeshRenderer meshRenderer;
    private Vector3 targetPos = new Vector3();

    private void Start()
    {
        smoothSpeed = Random.Range(smoothSpeedClamp.x, smoothSpeedClamp.y);
        targetPos = startPos;
    }
    private void FixedUpdate()
    {
        if (targetPos.x == transform.position.x && targetPos == startPos)
        {
            targetPos = endPos;
        }
        else if (targetPos.x == transform.position.x && targetPos == endPos)
        {
            targetPos = startPos;
        }

        Vector3 desiredPosition = new Vector3(targetPos.x, transform.position.y, transform.position.z);
        Vector3 smoothedPosition = Vector3.MoveTowards(transform.position, desiredPosition, smoothSpeed * Time.fixedDeltaTime);
        transform.position = smoothedPosition;
    }

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
