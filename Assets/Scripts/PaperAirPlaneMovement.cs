using UnityEngine;

public class PaperAirPlaneMovement : MonoBehaviour
{
    [Header("Configuration")]
    public float timeGliding = 2.0f;
    public float timeDescending = 2.0f;
    private float descendingAcceleration = 1.0f;
    private float ascendingAcceleration = 0.2f;

    private Rigidbody rb;
    private Rigidbody cameraRb;
    private float timeElapsed = 0.0f;
    private bool isGliding = true;
    private float acceleration = 0.0f;
    private Vector3 startingPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        cameraRb = Camera.main.GetComponent<Rigidbody>();
        startingPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        timeElapsed += Time.deltaTime;
        if (isGliding && timeElapsed > timeGliding)
        {
            acceleration = -descendingAcceleration;
            isGliding = false;
            timeElapsed = 0.0f;
        }
        else if (!isGliding && timeElapsed > timeDescending)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0);
            acceleration = ascendingAcceleration;
            isGliding = true;
            timeElapsed = 0.0f;
        }
        rb.AddForce(Vector3.up * acceleration);
    }
}
