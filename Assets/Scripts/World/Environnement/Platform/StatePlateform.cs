using UnityEngine;

public class StatePlateform : MonoBehaviour
{
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float smoothSpeed;
    [SerializeField] private float minDistance = 1.5f;
    Vector3 desiredPosition;
    private void Start()
    {
        desiredPosition = transform.position;
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
            }

            foreach (StatePlateform item in FindObjectsOfType<StatePlateform>())
            {
                item.ChangeXValue();
            }
        }
    }
    public void ChangeXValue()
    {
        float x;
        int rejection = 10;
        do
        {
            x = Random.Range(-4.0f, 4.0f);
            rejection--;
        } while (Mathf.Abs(x - transform.position.x) < minDistance && rejection > 0);
        desiredPosition = new Vector3(
             x,
            transform.position.y,
            transform.position.z);
    }


    private void FixedUpdate()
    {
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.fixedDeltaTime);
        transform.position = smoothedPosition;
    }

}
