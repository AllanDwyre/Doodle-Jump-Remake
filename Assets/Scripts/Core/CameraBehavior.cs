using UnityEngine;

public enum CameraBehaviors
{
    InGameFollow,
    EndGameFollow
}
public class CameraBehavior : MonoBehaviour
{
    [SerializeField] private Vector3 offset = new Vector3(0, 0, 0);
    [SerializeField] private float smoothSpeed = 0.125f;
    private GameObject target;

    public CameraBehaviors cameraBehaviors = CameraBehaviors.InGameFollow;

    private Vector3 velocity = Vector3.zero;


    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        //if (cameraBehaviors == CameraBehaviors.InGameFollow)
        //{
        if (target.transform.position.y > transform.position.y)
        {
            Vector3 desiredPosition = new Vector3(transform.position.x, target.transform.position.y, transform.position.z);
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed * Time.fixedDeltaTime);
            transform.position = smoothedPosition;
        }
        //}
        //else if (cameraBehaviors == CameraBehaviors.EndGameFollow)
        //{
        //    Vector3 desiredPosition = new Vector3(transform.position.x, target.transform.position.y, transform.position.z);
        //    Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        //    transform.position = smoothedPosition;
        //}

    }
}
