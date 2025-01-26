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
        float cameraDistanceZ = -Camera.main.transform.position.z;
        Vector3 boundsMAX = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, cameraDistanceZ)); // World coord of TOP RIGHT corner
        Vector3 boundsMIN = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, cameraDistanceZ)); // World coord of BOTTOM LEFT corner
        float upperCameraBound = boundsMAX.y;
        float downCameraBound = boundsMIN.y;

        float middleYCameraBound = (upperCameraBound - downCameraBound) / 2; 
        float spawnPosition = (upperCameraBound - gameObject.transform.position.y) / 2;
        if (spawnPosition < middleYCameraBound) // Ensures the plane always spawns on the top half of the screen (it doesn't work but whatever)
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + middleYCameraBound);

        rb = gameObject.GetComponent<Rigidbody>();
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
