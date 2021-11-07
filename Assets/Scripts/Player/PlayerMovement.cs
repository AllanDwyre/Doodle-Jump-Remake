using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private Vector3 offset;

    private Rigidbody rb;
    private float movement = 0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxis("Horizontal") * speed;

        CheckDead();
    }

    private void CheckDead()
    {
        if (Camera.main.WorldToViewportPoint(transform.position).y < 0)
        {
            FindObjectOfType<GameManager>().GetComponent<GameManager>().EndGame();
            Destroy(this);
        }
    }

    private void FixedUpdate()
    {
        Vector3 velocity = rb.velocity;
        velocity.x = movement;
        rb.velocity = velocity;
    }
}
