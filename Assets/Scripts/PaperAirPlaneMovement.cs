using UnityEngine;

public class PaperAirPlaneMovement : MonoBehaviour
{
    [Header("Configuration")]
    public float timeGliding = 2.0f;
    public float timeDescending = 2.0f;
    public float descendingAcceleration = 0.1f;
    public float ascendingAcceleration = 0.2f;

    private Rigidbody rb;
    private float timeElapsed = 0.0f;
    private bool isGliding = true;
    private float acceleration = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        if (gameObject.transform.localPosition.y < 0) // Ensures the plane always spawns on the top half of the screen
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, -gameObject.transform.localPosition.y); 
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
            acceleration = descendingAcceleration;
            isGliding = false;
            timeElapsed = 0.0f;
        }
        else if (!isGliding && timeElapsed > timeDescending)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0);
            acceleration = -ascendingAcceleration;
            isGliding = true;
            timeElapsed = 0.0f;
        }
        rb.AddForce(Vector3.up * acceleration);
    }
}
